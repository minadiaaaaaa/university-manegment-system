/*
 * Database Class - طبقة التعامل مع قاعدة البيانات
 * يستخدم SQLite ويطابق schema.sql
 * 
 * شرح Pointer:
 * - نستخدم Student* لإرجاع nullptr إذا فشل الـ authentication
 * - يجب دائماً delete الـ pointer بعد الانتهاء منه
 */
#ifndef DATABASE_H
#define DATABASE_H

#include <sqlite3.h>
#include <string>
#include <vector>
#include <iostream>
#include "Student.h"
#include "Course.h"
#include "Grade.h"
#include "Enrollment.h"

class Database {
private:
    sqlite3* db;           // Pointer للـ database connection
    std::string dbPath;

    // Helper: Get text safely (handles NULL)
    std::string safeGetText(sqlite3_stmt* stmt, int col) {
        const char* text = reinterpret_cast<const char*>(sqlite3_column_text(stmt, col));
        return text ? std::string(text) : "";
    }

    // Helper: Get department name by ID
    std::string getDepartmentName(int deptId) {
        std::string sql = "SELECT name FROM departments WHERE id = ?;";
        sqlite3_stmt* stmt;
        std::string result = "";
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmt, 1, deptId);
            if (sqlite3_step(stmt) == SQLITE_ROW) {
                result = safeGetText(stmt, 0);
            }
        }
        sqlite3_finalize(stmt);
        return result;
    }

public:
    // ========== Constructor / Destructor ==========
    Database(const std::string& path) : dbPath(path), db(nullptr) {
        if (sqlite3_open(dbPath.c_str(), &db) != SQLITE_OK) {
            std::cerr << "Database Error: " << sqlite3_errmsg(db) << "\n";
        } else {
            std::cout << "Connected to: " << dbPath << "\n";
            // Enable WAL mode for better concurrency
            sqlite3_exec(db, "PRAGMA journal_mode=WAL;", nullptr, nullptr, nullptr);
            // Set busy timeout to 5 seconds to prevent "database is locked" errors
            sqlite3_busy_timeout(db, 5000);
        }
    }

    ~Database() {
        if (db) {
            sqlite3_close(db);
            std::cout << "Database closed.\n";
        }
    }

    // ========== Authentication ==========
    
    /*
     * مثال على استخدام Pointer:
     * الدالة تُرجع Student* لأنها قد تُرجع nullptr إذا فشل تسجيل الدخول
     * 
     * Student* s = db.authenticateStudent("email", "pass");
     * if (s != nullptr) {
     *     // استخدم s->getName() مثلاً
     *     delete s;  // مهم! تحرير الذاكرة
     * }
     */
    Student* authenticateStudent(const std::string& email, const std::string& password) {
        std::string sql = R"(
            SELECT s.id, s.name, s.email, s.password, s.department_id,
                   s.level, s.semester, s.track, s.seat_number, s.gpa,
                   d.name as dept_name, f.name as faculty_name
            FROM students s
            LEFT JOIN departments d ON s.department_id = d.id
            LEFT JOIN faculties f ON d.faculty_id = f.id
            WHERE s.email = ? AND s.password = ?;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            std::cerr << "SQL Error: " << sqlite3_errmsg(db) << "\n";
            return nullptr;
        }
        
        sqlite3_bind_text(stmt, 1, email.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 2, password.c_str(), -1, SQLITE_TRANSIENT);

        Student* s = nullptr;
        if (sqlite3_step(stmt) == SQLITE_ROW) {
            s = new Student();  // Dynamic allocation - Pointer!
            s->setId(sqlite3_column_int(stmt, 0));
            s->setName(safeGetText(stmt, 1));
            s->setEmail(safeGetText(stmt, 2));
            s->setPassword(safeGetText(stmt, 3));
            s->setDepartmentId(sqlite3_column_int(stmt, 4));
            s->setLevel(sqlite3_column_int(stmt, 5));
            s->setSemester(sqlite3_column_int(stmt, 6));
            s->setTrack(safeGetText(stmt, 7));
            s->setSeatNumber(sqlite3_column_int(stmt, 8));
            s->setGpa(sqlite3_column_double(stmt, 9));
            s->setDepartment(safeGetText(stmt, 10));
            s->setFacultyName(safeGetText(stmt, 11));
        }
        sqlite3_finalize(stmt);
        return s;
    }

    bool authenticateAdmin(const std::string& username, const std::string& password) {
        std::string sql = "SELECT COUNT(*) FROM admins WHERE username = ? AND password = ?;";
        sqlite3_stmt* stmt;
        bool authenticated = false;
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_text(stmt, 1, username.c_str(), -1, SQLITE_TRANSIENT);
            sqlite3_bind_text(stmt, 2, password.c_str(), -1, SQLITE_TRANSIENT);
            
            if (sqlite3_step(stmt) == SQLITE_ROW) {
                authenticated = sqlite3_column_int(stmt, 0) > 0;
            }
        }
        sqlite3_finalize(stmt);
        return authenticated;
    }

    // ========== Students ==========
    
    // Helper: Find or create section for student based on level, semester, track
    int findOrCreateSection(int level, int semester, const std::string& track) {
        // First, try to find an existing section
        std::string findSql = R"(
            SELECT section_id FROM sections 
            WHERE year = ? AND term = ? AND track = ?
            ORDER BY section_number LIMIT 1;
        )";
        sqlite3_stmt* stmt;
        int sectionId = -1;
        
        if (sqlite3_prepare_v2(db, findSql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmt, 1, level);
            sqlite3_bind_int(stmt, 2, semester);
            sqlite3_bind_text(stmt, 3, track.c_str(), -1, SQLITE_TRANSIENT);
            if (sqlite3_step(stmt) == SQLITE_ROW) {
                sectionId = sqlite3_column_int(stmt, 0);
            }
        }
        sqlite3_finalize(stmt);
        
        // If no section found, create one
        if (sectionId == -1) {
            std::string createSql = R"(
                INSERT INTO sections (year, term, track, section_number) VALUES (?, ?, ?, 1);
            )";
            if (sqlite3_prepare_v2(db, createSql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
                sqlite3_bind_int(stmt, 1, level);
                sqlite3_bind_int(stmt, 2, semester);
                sqlite3_bind_text(stmt, 3, track.c_str(), -1, SQLITE_TRANSIENT);
                if (sqlite3_step(stmt) == SQLITE_DONE) {
                    sectionId = (int)sqlite3_last_insert_rowid(db);
                }
            }
            sqlite3_finalize(stmt);
        }
        return sectionId;
    }
    
    // Helper: Add default payments for a new student
    void addDefaultPayments(int studentId) {
        std::string sql = R"(
            INSERT INTO payments (student_id, amount, method, date, status, description, receipt_number)
            VALUES (?, ?, ?, ?, ?, ?, ?);
        )";
        
        struct PaymentData {
            double amount;
            const char* method;
            const char* date;
            const char* status;
            const char* desc;
        };
        
        PaymentData payments[] = {
            {5000.00, "Cash", "2024-09-15", "Paid", "Tuition Fee - Term 1"},
            {500.00, "Card", "2024-09-20", "Paid", "Registration Fee"},
            {1500.00, "Bank Transfer", "2024-10-01", "Paid", "Lab Equipment Fee"},
            {5000.00, "Cash", "2025-01-10", "Pending", "Tuition Fee - Term 2"}
        };
        
        for (int i = 0; i < 4; i++) {
            sqlite3_stmt* stmt;
            if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
                char receipt[32];
                snprintf(receipt, sizeof(receipt), "RCP-%03d-%03d", studentId, i + 1);
                
                sqlite3_bind_int(stmt, 1, studentId);
                sqlite3_bind_double(stmt, 2, payments[i].amount);
                sqlite3_bind_text(stmt, 3, payments[i].method, -1, SQLITE_STATIC);
                sqlite3_bind_text(stmt, 4, payments[i].date, -1, SQLITE_STATIC);
                sqlite3_bind_text(stmt, 5, payments[i].status, -1, SQLITE_STATIC);
                sqlite3_bind_text(stmt, 6, payments[i].desc, -1, SQLITE_STATIC);
                sqlite3_bind_text(stmt, 7, receipt, -1, SQLITE_TRANSIENT);
                sqlite3_step(stmt);
            }
            sqlite3_finalize(stmt);
        }
    }

    bool addStudent(const Student& s) {
        // Find or create appropriate section
        int sectionId = findOrCreateSection(s.getLevel(), s.getSemester(), s.getTrack());
        
        std::string sql = R"(
            INSERT INTO students (name, email, password, department_id, level, semester, track, seat_number, section_id)
            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?);
        )";
        sqlite3_stmt* stmt;
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            std::cerr << "SQL Error: " << sqlite3_errmsg(db) << "\n";
            return false;
        }
        
        sqlite3_bind_text(stmt, 1, s.getName().c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 2, s.getEmail().c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 3, s.getPassword().c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_int(stmt, 4, s.getDepartmentId());
        sqlite3_bind_int(stmt, 5, s.getLevel());
        sqlite3_bind_int(stmt, 6, s.getSemester());
        sqlite3_bind_text(stmt, 7, s.getTrack().c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_int(stmt, 8, s.getSeatNumber());
        sqlite3_bind_int(stmt, 9, sectionId);
        
        int result = sqlite3_step(stmt);
        sqlite3_finalize(stmt);
        
        if (result != SQLITE_DONE) {
            std::cerr << "Error: " << sqlite3_errmsg(db) << "\n";
            return false;
        }
        
        // Get the new student ID and add default payments
        int newStudentId = (int)sqlite3_last_insert_rowid(db);
        addDefaultPayments(newStudentId);
        
        std::cout << "Student added with Section ID: " << sectionId << " and 4 default payments.\n";
        return true;
    }

    bool assignStudentToSection(int studentId, int sectionId) {
        std::string sql = "UPDATE students SET section_id = ? WHERE id = ?;";
        sqlite3_stmt* stmt;
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            std::cerr << "SQL Error: " << sqlite3_errmsg(db) << "\n";
            return false;
        }
        
        sqlite3_bind_int(stmt, 1, sectionId);
        sqlite3_bind_int(stmt, 2, studentId);
        
        int result = sqlite3_step(stmt);
        sqlite3_finalize(stmt);
        
        return result == SQLITE_DONE;
    }

    bool hasPayments(int studentId) {
        std::string sql = "SELECT COUNT(*) FROM payments WHERE student_id = ?;";
        sqlite3_stmt* stmt;
        int count = 0;
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmt, 1, studentId);
            if (sqlite3_step(stmt) == SQLITE_ROW) {
                count = sqlite3_column_int(stmt, 0);
            }
        }
        sqlite3_finalize(stmt);
        return count > 0;
    }

    std::vector<Student> getAllStudents() {
        std::vector<Student> list;
        std::string sql = R"(
            SELECT s.id, s.name, s.email, s.level, s.seat_number, s.track, d.name
            FROM students s
            LEFT JOIN departments d ON s.department_id = d.id
            ORDER BY s.name;
        )";
        sqlite3_stmt* stmt;
        sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr);
        
        while (sqlite3_step(stmt) == SQLITE_ROW) {
            Student s;
            s.setId(sqlite3_column_int(stmt, 0));
            s.setName(safeGetText(stmt, 1));
            s.setEmail(safeGetText(stmt, 2));
            s.setLevel(sqlite3_column_int(stmt, 3));
            s.setSeatNumber(sqlite3_column_int(stmt, 4));
            s.setTrack(safeGetText(stmt, 5));
            s.setDepartment(safeGetText(stmt, 6));
            list.push_back(s);
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // ========== Courses ==========
    
    std::vector<Course> getAllCourses() {
        std::vector<Course> list;
        std::string sql = "SELECT course_id, course_code, course_name, year, term, track, credit_hours FROM courses ORDER BY year, term;";
        sqlite3_stmt* stmt;
        sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr);
        
        while (sqlite3_step(stmt) == SQLITE_ROW) {
            Course c(
                sqlite3_column_int(stmt, 0),          // id
                safeGetText(stmt, 1),                 // code
                safeGetText(stmt, 2),                 // name
                sqlite3_column_int(stmt, 3),          // year
                sqlite3_column_int(stmt, 4),          // term
                safeGetText(stmt, 5),                 // track
                sqlite3_column_int(stmt, 6)           // credits
            );
            list.push_back(c);
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // المواد حسب السنة والترم والمسار (مثل GUI)
    std::vector<Course> getCoursesByLevelAndTerm(int level, int term, const std::string& track) {
        std::vector<Course> list;
        
        // للسنة 1 و 2، كل الطلاب يأخذون General
        std::string trackFilter = (level <= 2) ? "General" : track;
        
        std::string sql = R"(
            SELECT course_id, course_code, course_name, year, term, track, credit_hours 
            FROM courses 
            WHERE year = ? AND term = ? AND track = ?
            ORDER BY course_name;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmt, 1, level);
            sqlite3_bind_int(stmt, 2, term);
            sqlite3_bind_text(stmt, 3, trackFilter.c_str(), -1, SQLITE_TRANSIENT);
            
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                Course c(
                    sqlite3_column_int(stmt, 0),
                    safeGetText(stmt, 1),
                    safeGetText(stmt, 2),
                    sqlite3_column_int(stmt, 3),
                    sqlite3_column_int(stmt, 4),
                    safeGetText(stmt, 5),
                    sqlite3_column_int(stmt, 6)
                );
                list.push_back(c);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }

    bool addCourse(const Course& c) {
        std::string sql = R"(
            INSERT INTO courses (course_code, course_name, year, term, track, credit_hours)
            VALUES (?, ?, ?, ?, ?, ?);
        )";
        sqlite3_stmt* stmt;
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            std::cerr << "SQL Error: " << sqlite3_errmsg(db) << "\n";
            return false;
        }
        
        sqlite3_bind_text(stmt, 1, c.getCode().c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 2, c.getName().c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_int(stmt, 3, c.getYear());
        sqlite3_bind_int(stmt, 4, c.getTerm());
        sqlite3_bind_text(stmt, 5, c.getTrack().c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_int(stmt, 6, c.getCreditHours());
        
        int result = sqlite3_step(stmt);
        sqlite3_finalize(stmt);
        
        if (result != SQLITE_DONE) {
            std::cerr << "Error: " << sqlite3_errmsg(db) << "\n";
            return false;
        }
        return true;
    }

    // ========== Enrollments ==========
    
    bool enrollStudent(int studentId, const std::string& courseCode) {
        // Get course_id from code
        std::string sqlId = "SELECT course_id FROM courses WHERE course_code = ?;";
        sqlite3_stmt* stmtId;
        sqlite3_prepare_v2(db, sqlId.c_str(), -1, &stmtId, nullptr);
        sqlite3_bind_text(stmtId, 1, courseCode.c_str(), -1, SQLITE_TRANSIENT);
        
        int courseId = -1;
        if (sqlite3_step(stmtId) == SQLITE_ROW) {
            courseId = sqlite3_column_int(stmtId, 0);
        }
        sqlite3_finalize(stmtId);

        if (courseId == -1) {
            std::cout << "Course not found: " << courseCode << "\n";
            return false;
        }

        // Check if already enrolled
        std::string checkSql = "SELECT COUNT(*) FROM enrollments WHERE student_id = ? AND course_id = ?;";
        sqlite3_stmt* checkStmt;
        sqlite3_prepare_v2(db, checkSql.c_str(), -1, &checkStmt, nullptr);
        sqlite3_bind_int(checkStmt, 1, studentId);
        sqlite3_bind_int(checkStmt, 2, courseId);
        
        bool alreadyEnrolled = false;
        if (sqlite3_step(checkStmt) == SQLITE_ROW) {
            alreadyEnrolled = sqlite3_column_int(checkStmt, 0) > 0;
        }
        sqlite3_finalize(checkStmt);

        if (alreadyEnrolled) {
            std::cout << "Already enrolled in this course.\n";
            return false;
        }

        // Enroll
        std::string sql = R"(
            INSERT INTO enrollments (student_id, course_id, academic_year, semester, status)
            VALUES (?, ?, '2024-2025', 1, 'Enrolled');
        )";
        sqlite3_stmt* stmt;
        sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr);
        sqlite3_bind_int(stmt, 1, studentId);
        sqlite3_bind_int(stmt, 2, courseId);
        
        int result = sqlite3_step(stmt);
        sqlite3_finalize(stmt);
        
        if (result == SQLITE_DONE) {
            std::cout << "Successfully enrolled in: " << courseCode << "\n";
            return true;
        }
        return false;
    }

    // ========== Grades ==========
    
    std::vector<Grade> getStudentGrades(int studentId) {
        std::vector<Grade> list;
        std::string sql = R"(
            SELECT g.grade_id, g.enrollment_id, e.course_id, c.course_code, c.course_name,
                   g.s1, g.s2, g.final_exam, g.total, g.letter_grade, g.grade_points
            FROM grades g
            JOIN enrollments e ON g.enrollment_id = e.enrollment_id
            JOIN courses c ON e.course_id = c.course_id
            WHERE e.student_id = ?
            ORDER BY c.year, c.term;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmt, 1, studentId);
            
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                Grade g;
                g.setGradeId(sqlite3_column_int(stmt, 0));
                g.setEnrollmentId(sqlite3_column_int(stmt, 1));
                g.setCourseId(sqlite3_column_int(stmt, 2));
                g.setCourseCode(safeGetText(stmt, 3));
                g.setCourseName(safeGetText(stmt, 4));
                g.setS1(sqlite3_column_double(stmt, 5));
                g.setS2(sqlite3_column_double(stmt, 6));
                g.setFinalExam(sqlite3_column_double(stmt, 7));
                g.calculateTotal();  // حساب المجموع والـ GPA
                list.push_back(g);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // ========== Enrollment View ==========
    struct EnrollmentView {
        std::string courseCode;
        std::string courseName;
        double grade;
        std::string status;
    };

    std::vector<EnrollmentView> getStudentEnrollments(int studentId) {
        std::vector<EnrollmentView> list;
        std::string sql = R"(
            SELECT c.course_code, c.course_name, COALESCE(g.total, -1), e.status
            FROM enrollments e 
            JOIN courses c ON e.course_id = c.course_id 
            LEFT JOIN grades g ON e.enrollment_id = g.enrollment_id 
            WHERE e.student_id = ?
            ORDER BY c.year, c.term;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmt, 1, studentId);
            
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                EnrollmentView ev;
                ev.courseCode = safeGetText(stmt, 0);
                ev.courseName = safeGetText(stmt, 1);
                ev.grade = sqlite3_column_double(stmt, 2);
                ev.status = safeGetText(stmt, 3);
                list.push_back(ev);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // ========== Professor Authentication ==========
    
    struct ProfessorData {
        int id;
        std::string name;
        std::string email;
        std::string phone;
        std::string specialization;
        std::string title;
    };
    
    ProfessorData* authenticateProfessor(const std::string& email, const std::string& password) {
        std::string sql = R"(
            SELECT instructor_id, name, email, phone, specialization, title
            FROM instructors
            WHERE email = ? AND password = ? AND is_active = 1;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            return nullptr;
        }
        
        sqlite3_bind_text(stmt, 1, email.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 2, password.c_str(), -1, SQLITE_TRANSIENT);

        ProfessorData* p = nullptr;
        if (sqlite3_step(stmt) == SQLITE_ROW) {
            p = new ProfessorData();
            p->id = sqlite3_column_int(stmt, 0);
            p->name = safeGetText(stmt, 1);
            p->email = safeGetText(stmt, 2);
            p->phone = safeGetText(stmt, 3);
            p->specialization = safeGetText(stmt, 4);
            p->title = safeGetText(stmt, 5);
        }
        sqlite3_finalize(stmt);
        return p;
    }

    // ========== Professor Courses ==========
    
    struct ProfessorCourse {
        int courseId;
        std::string courseCode;
        std::string courseName;
        int year;
        int term;
        std::string track;
        int studentCount;
    };
    
    std::vector<ProfessorCourse> getProfessorCourses(int professorId) {
        std::vector<ProfessorCourse> list;
        std::string sql = R"(
            SELECT c.course_id, c.course_code, c.course_name, c.year, c.term, c.track,
                   (SELECT COUNT(*) FROM enrollments e WHERE e.course_id = c.course_id) as student_count
            FROM course_offerings co
            JOIN courses c ON co.course_id = c.course_id
            WHERE co.instructor_id = ?
            ORDER BY c.year, c.term;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmt, 1, professorId);
            
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                ProfessorCourse pc;
                pc.courseId = sqlite3_column_int(stmt, 0);
                pc.courseCode = safeGetText(stmt, 1);
                pc.courseName = safeGetText(stmt, 2);
                pc.year = sqlite3_column_int(stmt, 3);
                pc.term = sqlite3_column_int(stmt, 4);
                pc.track = safeGetText(stmt, 5);
                pc.studentCount = sqlite3_column_int(stmt, 6);
                list.push_back(pc);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // ========== Students in Course ==========
    
    struct CourseStudent {
        int studentId;
        std::string name;
        std::string email;
        int seatNumber;
        double s1;
        double s2;
        double finalExam;
        double total;
    };
    
    std::vector<CourseStudent> getStudentsInCourse(const std::string& courseCode) {
        std::vector<CourseStudent> list;
        std::string sql = R"(
            SELECT s.id, s.name, s.email, s.seat_number,
                   COALESCE(g.s1, 0), COALESCE(g.s2, 0), 
                   COALESCE(g.final_exam, 0), COALESCE(g.total, 0)
            FROM enrollments e
            JOIN students s ON e.student_id = s.id
            JOIN courses c ON e.course_id = c.course_id
            LEFT JOIN grades g ON e.enrollment_id = g.enrollment_id
            WHERE c.course_code = ?
            ORDER BY s.name;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_text(stmt, 1, courseCode.c_str(), -1, SQLITE_TRANSIENT);
            
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                CourseStudent cs;
                cs.studentId = sqlite3_column_int(stmt, 0);
                cs.name = safeGetText(stmt, 1);
                cs.email = safeGetText(stmt, 2);
                cs.seatNumber = sqlite3_column_int(stmt, 3);
                cs.s1 = sqlite3_column_double(stmt, 4);
                cs.s2 = sqlite3_column_double(stmt, 5);
                cs.finalExam = sqlite3_column_double(stmt, 6);
                cs.total = sqlite3_column_double(stmt, 7);
                list.push_back(cs);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // ========== Attendance Operations ==========
    
    bool recordAttendance(int studentId, int courseId, const std::string& date, const std::string& status) {
        std::string sql = R"(
            INSERT OR REPLACE INTO attendance (student_id, course_id, date, status)
            VALUES (?, ?, ?, ?);
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            return false;
        }
        
        sqlite3_bind_int(stmt, 1, studentId);
        sqlite3_bind_int(stmt, 2, courseId);
        sqlite3_bind_text(stmt, 3, date.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 4, status.c_str(), -1, SQLITE_TRANSIENT);
        
        int result = sqlite3_step(stmt);
        sqlite3_finalize(stmt);
        return result == SQLITE_DONE;
    }
    
    int getCourseIdByCode(const std::string& code) {
        std::string sql = "SELECT course_id FROM courses WHERE course_code = ?;";
        sqlite3_stmt* stmt;
        int courseId = -1;
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_text(stmt, 1, code.c_str(), -1, SQLITE_TRANSIENT);
            if (sqlite3_step(stmt) == SQLITE_ROW) {
                courseId = sqlite3_column_int(stmt, 0);
            }
        }
        sqlite3_finalize(stmt);
        return courseId;
    }
    
    struct AttendanceRecord {
        int studentId;
        std::string studentName;
        std::string date;
        std::string status;
    };
    
    std::vector<AttendanceRecord> getCourseAttendance(const std::string& courseCode) {
        std::vector<AttendanceRecord> list;
        std::string sql = R"(
            SELECT s.id, s.name, a.date, a.status
            FROM attendance a
            JOIN students s ON a.student_id = s.id
            JOIN courses c ON a.course_id = c.course_id
            WHERE c.course_code = ?
            ORDER BY a.date DESC, s.name;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_text(stmt, 1, courseCode.c_str(), -1, SQLITE_TRANSIENT);
            
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                AttendanceRecord ar;
                ar.studentId = sqlite3_column_int(stmt, 0);
                ar.studentName = safeGetText(stmt, 1);
                ar.date = safeGetText(stmt, 2);
                ar.status = safeGetText(stmt, 3);
                list.push_back(ar);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // ========== Grade Update ==========
    
    bool updateGrade(int studentId, const std::string& courseCode, int gradeType, double score) {
        // First get enrollment_id
        std::string sqlEnroll = R"(
            SELECT e.enrollment_id FROM enrollments e
            JOIN courses c ON e.course_id = c.course_id
            WHERE e.student_id = ? AND c.course_code = ?;
        )";
        
        sqlite3_stmt* stmtEnroll;
        int enrollmentId = -1;
        if (sqlite3_prepare_v2(db, sqlEnroll.c_str(), -1, &stmtEnroll, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmtEnroll, 1, studentId);
            sqlite3_bind_text(stmtEnroll, 2, courseCode.c_str(), -1, SQLITE_TRANSIENT);
            if (sqlite3_step(stmtEnroll) == SQLITE_ROW) {
                enrollmentId = sqlite3_column_int(stmtEnroll, 0);
            }
        }
        sqlite3_finalize(stmtEnroll);
        
        if (enrollmentId == -1) {
            std::cout << "Student not enrolled in this course.\n";
            return false;
        }
        
        // Insert or update grade
        std::string field;
        switch (gradeType) {
            case 1: field = "s1"; break;
            case 2: field = "s2"; break;
            case 3: field = "final_exam"; break;
            default: return false;
        }
        
        // Check if grade record exists
        std::string checkSql = "SELECT grade_id FROM grades WHERE enrollment_id = ?;";
        sqlite3_stmt* checkStmt;
        bool exists = false;
        if (sqlite3_prepare_v2(db, checkSql.c_str(), -1, &checkStmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(checkStmt, 1, enrollmentId);
            exists = sqlite3_step(checkStmt) == SQLITE_ROW;
        }
        sqlite3_finalize(checkStmt);
        
        std::string sql;
        if (exists) {
            sql = "UPDATE grades SET " + field + " = ?, total = COALESCE(s1,0) + COALESCE(s2,0) + COALESCE(final_exam,0), updated_at = datetime('now') WHERE enrollment_id = ?;";
        } else {
            // Insert new grade record with all fields initialized to 0
            sql = "INSERT INTO grades (enrollment_id, s1, s2, final_exam, total, updated_at) VALUES (?, 0, 0, 0, 0, datetime('now'));";
        }
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            std::cerr << "SQL Error in updateGrade (prepare): " << sqlite3_errmsg(db) << "\n";
            return false;
        }
        
        if (exists) {
            sqlite3_bind_double(stmt, 1, score);
            sqlite3_bind_int(stmt, 2, enrollmentId);
        } else {
            sqlite3_bind_int(stmt, 1, enrollmentId);
        }
        
        int result = sqlite3_step(stmt);
        if (result != SQLITE_DONE) {
             std::cerr << "SQL Error in updateGrade (step): " << sqlite3_errmsg(db) << "\n";
        }
        sqlite3_finalize(stmt);
        
        // If we just inserted a new record, now update the specific field
        if (!exists && result == SQLITE_DONE) {
            std::string updateField = "UPDATE grades SET " + field + " = ?, total = COALESCE(s1,0) + COALESCE(s2,0) + COALESCE(final_exam,0) WHERE enrollment_id = ?;";
            sqlite3_stmt* updateStmt;
            if (sqlite3_prepare_v2(db, updateField.c_str(), -1, &updateStmt, nullptr) == SQLITE_OK) {
                sqlite3_bind_double(updateStmt, 1, score);
                sqlite3_bind_int(updateStmt, 2, enrollmentId);
                result = sqlite3_step(updateStmt);
            }
            sqlite3_finalize(updateStmt);
        }
        
        // Update total for existing records
        if (result == SQLITE_DONE) {
            std::string updateTotal = "UPDATE grades SET total = COALESCE(s1,0) + COALESCE(s2,0) + COALESCE(final_exam,0) WHERE enrollment_id = ?;";
            sqlite3_stmt* totalStmt;
            if (sqlite3_prepare_v2(db, updateTotal.c_str(), -1, &totalStmt, nullptr) == SQLITE_OK) {
                sqlite3_bind_int(totalStmt, 1, enrollmentId);
                sqlite3_step(totalStmt);
            }
            sqlite3_finalize(totalStmt);
        }
        
        return result == SQLITE_DONE;
    }

    // ========== Schedule ==========
    
    struct ScheduleEntry {
        std::string courseCode;
        std::string courseName;
        std::string day;
        std::string time;
        std::string room;
    };
    
    std::vector<ScheduleEntry> getProfessorSchedule(int professorId) {
        std::vector<ScheduleEntry> list;
        std::string sql = R"(
            SELECT c.course_code, c.course_name, sc.day, sc.time, COALESCE(cl.room_code, 'TBD')
            FROM schedules sc
            JOIN course_offerings co ON sc.offering_id = co.offering_id
            JOIN courses c ON co.course_id = c.course_id
            LEFT JOIN classrooms cl ON sc.room_id = cl.room_id
            WHERE co.instructor_id = ?
            ORDER BY CASE sc.day 
                WHEN 'Saturday' THEN 1 WHEN 'Sunday' THEN 2 WHEN 'Monday' THEN 3 
                WHEN 'Tuesday' THEN 4 WHEN 'Wednesday' THEN 5 WHEN 'Thursday' THEN 6 
            END, sc.time;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmt, 1, professorId);
            
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                ScheduleEntry se;
                se.courseCode = safeGetText(stmt, 0);
                se.courseName = safeGetText(stmt, 1);
                se.day = safeGetText(stmt, 2);
                se.time = safeGetText(stmt, 3);
                se.room = safeGetText(stmt, 4);
                list.push_back(se);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // ========== Admin: Classrooms & Labs ==========
    
    struct Classroom {
        int roomId;
        std::string roomCode;
        int capacity;
        std::string building;
        std::string roomType;
    };
    
    std::vector<Classroom> getAllClassrooms() {
        std::vector<Classroom> list;
        std::string sql = "SELECT room_id, room_code, capacity, building, room_type FROM classrooms ORDER BY room_type, room_code;";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                Classroom c;
                c.roomId = sqlite3_column_int(stmt, 0);
                c.roomCode = safeGetText(stmt, 1);
                c.capacity = sqlite3_column_int(stmt, 2);
                c.building = safeGetText(stmt, 3);
                c.roomType = safeGetText(stmt, 4);
                list.push_back(c);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }
    
    bool addClassroom(const std::string& code, int capacity, const std::string& building, const std::string& type) {
        std::string sql = "INSERT INTO classrooms (room_code, capacity, building, room_type) VALUES (?, ?, ?, ?);";
        sqlite3_stmt* stmt;
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            return false;
        }
        
        sqlite3_bind_text(stmt, 1, code.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_int(stmt, 2, capacity);
        sqlite3_bind_text(stmt, 3, building.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 4, type.c_str(), -1, SQLITE_TRANSIENT);
        
        int result = sqlite3_step(stmt);
        sqlite3_finalize(stmt);
        return result == SQLITE_DONE;
    }

    // ========== Admin: Faculties ==========
    
    struct Faculty {
        int id;
        std::string name;
        std::string nameAr;
        std::string code;
    };
    
    std::vector<Faculty> getAllFaculties() {
        std::vector<Faculty> list;
        std::string sql = "SELECT id, name, name_ar, code FROM faculties;";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                Faculty f;
                f.id = sqlite3_column_int(stmt, 0);
                f.name = safeGetText(stmt, 1);
                f.nameAr = safeGetText(stmt, 2);
                f.code = safeGetText(stmt, 3);
                list.push_back(f);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // ========== Admin: Departments ==========
    
    struct Department {
        int id;
        std::string name;
        std::string nameAr;
        std::string code;
        int facultyId;
        std::string facultyName;
    };
    
    std::vector<Department> getAllDepartments() {
        std::vector<Department> list;
        std::string sql = R"(
            SELECT d.id, d.name, d.name_ar, d.code, d.faculty_id, f.name
            FROM departments d
            LEFT JOIN faculties f ON d.faculty_id = f.id;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                Department d;
                d.id = sqlite3_column_int(stmt, 0);
                d.name = safeGetText(stmt, 1);
                d.nameAr = safeGetText(stmt, 2);
                d.code = safeGetText(stmt, 3);
                d.facultyId = sqlite3_column_int(stmt, 4);
                d.facultyName = safeGetText(stmt, 5);
                list.push_back(d);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // ========== Admin: Instructors ==========
    
    struct Instructor {
        int id;
        std::string name;
        std::string email;
        std::string phone;
        std::string specialization;
        std::string title;
    };
    
    std::vector<Instructor> getAllInstructors() {
        std::vector<Instructor> list;
        std::string sql = "SELECT instructor_id, name, email, phone, specialization, title FROM instructors WHERE is_active = 1;";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                Instructor i;
                i.id = sqlite3_column_int(stmt, 0);
                i.name = safeGetText(stmt, 1);
                i.email = safeGetText(stmt, 2);
                i.phone = safeGetText(stmt, 3);
                i.specialization = safeGetText(stmt, 4);
                i.title = safeGetText(stmt, 5);
                list.push_back(i);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }
    
    bool addInstructor(const std::string& name, const std::string& email, const std::string& phone,
                       const std::string& specialization, const std::string& title) {
        std::string sql = "INSERT INTO instructors (name, email, phone, specialization, title) VALUES (?, ?, ?, ?, ?);";
        sqlite3_stmt* stmt;
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            return false;
        }
        
        sqlite3_bind_text(stmt, 1, name.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 2, email.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 3, phone.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 4, specialization.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 5, title.c_str(), -1, SQLITE_TRANSIENT);
        
        int result = sqlite3_step(stmt);
        sqlite3_finalize(stmt);
        return result == SQLITE_DONE;
    }

    // ========== Student: Payment Info ==========
    
    struct PaymentInfo {
        int paymentId;
        double amount;
        std::string method;
        std::string date;
        std::string status;
        std::string description;
    };
    
    std::vector<PaymentInfo> getStudentPayments(int studentId) {
        std::vector<PaymentInfo> list;
        std::string sql = R"(
            SELECT payment_id, amount, method, date, status, description
            FROM payments
            WHERE student_id = ?
            ORDER BY date DESC;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmt, 1, studentId);
            
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                PaymentInfo p;
                p.paymentId = sqlite3_column_int(stmt, 0);
                p.amount = sqlite3_column_double(stmt, 1);
                p.method = safeGetText(stmt, 2);
                p.date = safeGetText(stmt, 3);
                p.status = safeGetText(stmt, 4);
                p.description = safeGetText(stmt, 5);
                list.push_back(p);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }

    // ========== Student: Section Info ==========
    
    struct SectionInfo {
        int sectionId;
        int year;
        int term;
        std::string track;
        int sectionNumber;
        int studentCount;
    };
    
    SectionInfo getStudentSection(int studentId) {
        SectionInfo info = {0, 0, 0, "", 0, 0};
        std::string sql = R"(
            SELECT sec.section_id, sec.year, sec.term, sec.track, sec.section_number,
                   (SELECT COUNT(*) FROM students WHERE section_id = sec.section_id) as count
            FROM students s
            JOIN sections sec ON s.section_id = sec.section_id
            WHERE s.id = ?;
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            sqlite3_bind_int(stmt, 1, studentId);
            
            if (sqlite3_step(stmt) == SQLITE_ROW) {
                info.sectionId = sqlite3_column_int(stmt, 0);
                info.year = sqlite3_column_int(stmt, 1);
                info.term = sqlite3_column_int(stmt, 2);
                info.track = safeGetText(stmt, 3);
                info.sectionNumber = sqlite3_column_int(stmt, 4);
                info.studentCount = sqlite3_column_int(stmt, 5);
            }
        }
        sqlite3_finalize(stmt);
        return info;
    }

    // ========== Assign Course to Instructor ==========
    bool assignCourseOffering(int instructorId, int courseId) {
        std::string sql = R"(
            INSERT INTO course_offerings (course_id, instructor_id, academic_year, semester)
            VALUES (?, ?, '2024-2025', 1);
        )";
        
        sqlite3_stmt* stmt;
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            return false;
        }
        
        sqlite3_bind_int(stmt, 1, courseId);
        sqlite3_bind_int(stmt, 2, instructorId);
        
        int result = sqlite3_step(stmt);
        sqlite3_finalize(stmt);
        return result == SQLITE_DONE;
    }

    // ========== News Management ==========
    struct NewsItem {
        int id;
        std::string title;
        std::string content;
        std::string date;
    };
    
    std::vector<NewsItem> getAllNews() {
        std::vector<NewsItem> list;
        std::string sql = "SELECT id, title, content, date FROM news ORDER BY date DESC LIMIT 20;";
        sqlite3_stmt* stmt;
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) == SQLITE_OK) {
            while (sqlite3_step(stmt) == SQLITE_ROW) {
                NewsItem n;
                n.id = sqlite3_column_int(stmt, 0);
                n.title = safeGetText(stmt, 1);
                n.content = safeGetText(stmt, 2);
                n.date = safeGetText(stmt, 3);
                list.push_back(n);
            }
        }
        sqlite3_finalize(stmt);
        return list;
    }
    
    bool insertNews(const std::string& title, const std::string& content) {
        std::string sql = "INSERT INTO news (title, content, date) VALUES (?, ?, datetime('now'));";
        sqlite3_stmt* stmt;
        
        if (sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr) != SQLITE_OK) {
            return false;
        }
        
        sqlite3_bind_text(stmt, 1, title.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_text(stmt, 2, content.c_str(), -1, SQLITE_TRANSIENT);
        
        int result = sqlite3_step(stmt);
        sqlite3_finalize(stmt);
        return result == SQLITE_DONE;
    }

    // ========== Update Student Grade with Course Code ==========
    bool updateStudentGrade(int studentId, const std::string& courseCode, int gradeType, double score) {
        return updateGrade(studentId, courseCode, gradeType, score);
    }
};

#endif
