/*
 * DatabaseManager - واجهة عالية المستوى لقاعدة البيانات
 * يستخدم Database class ويوفر واجهة أبسط للقوائم
 */
#ifndef DATABASEMANAGER_H
#define DATABASEMANAGER_H

#include "Database.h"
#include "Grade.h"
#include <iostream>
#include <iomanip>

class DatabaseManager {
private:
    Database db;

public:
    DatabaseManager() : db("university.db") {}

    // ========== Authentication ==========
    Student* loginStudent(const std::string& e, const std::string& p) { 
        return db.authenticateStudent(e, p); 
    }
    
    bool loginAdmin(const std::string& u, const std::string& p) { 
        return db.authenticateAdmin(u, p); 
    }

    // ========== Student Operations ==========
    void addStudent(const Student& s) { 
        if (db.addStudent(s)) {
            std::cout << "Student '" << s.getName() << "' added successfully.\n";
        } else {
            std::cout << "Failed to add student. Check track: SW, Network, or General.\n";
        }
    }
    
    void listStudents() {
        auto list = db.getAllStudents();
        std::cout << "\n============================================\n";
        std::cout << "              STUDENT LIST                  \n";
        std::cout << "============================================\n";
        std::cout << std::left << std::setw(6) << "ID" 
                  << std::setw(20) << "Name" 
                  << std::setw(25) << "Email" 
                  << std::setw(10) << "Level" << "\n";
        std::cout << "--------------------------------------------\n";
        
        for (const auto& s : list) {
            std::cout << std::left << std::setw(6) << s.getId() 
                      << std::setw(20) << s.getName().substr(0, 18)
                      << std::setw(25) << s.getEmail().substr(0, 23)
                      << std::setw(10) << s.getLevel() << "\n";
        }
        std::cout << "============================================\n";
        std::cout << "Total: " << list.size() << " students\n";
    }

    // ========== Course Operations ==========
    void addCourse(const Course& c) { 
        if (db.addCourse(c)) {
            std::cout << "Course '" << c.getCode() << "' added successfully.\n";
        } else {
            std::cout << "Failed to add course. Check track: SW, Network, or General.\n";
        }
    }

    void listCourses() {
        auto list = db.getAllCourses();
        std::cout << "\n============================================\n";
        std::cout << "              COURSE LIST                   \n";
        std::cout << "============================================\n";
        
        int currentYear = 0;
        for (const auto& c : list) {
            if (c.getYear() != currentYear) {
                currentYear = c.getYear();
                std::cout << "--- Year " << currentYear << " ---\n";
            }
            std::cout << std::left << std::setw(10) << c.getCode()
                      << std::setw(30) << c.getName().substr(0, 28)
                      << "T" << c.getTerm() << " | " << c.getTrack() 
                      << " | " << c.getCreditHours() << "hrs\n";
        }
        std::cout << "============================================\n";
        std::cout << "Total: " << list.size() << " courses\n";
    }

    void listAvailableCourses(int level, int term, const std::string& track) {
        auto list = db.getCoursesByLevelAndTerm(level, term, track);
        
        std::cout << "\n============================================\n";
        std::cout << " Available Courses - Year " << level << " Term " << term << "\n";
        std::cout << "============================================\n";
        
        if (list.empty()) {
            std::cout << "No courses available for your level/term/track.\n";
        } else {
            std::cout << std::left << std::setw(10) << "Code" 
                      << std::setw(35) << "Name" 
                      << std::setw(10) << "Credits" << "\n";
            std::cout << "--------------------------------------------\n";
            
            for (const auto& c : list) {
                std::cout << std::left << std::setw(10) << c.getCode()
                          << std::setw(35) << c.getName().substr(0, 33)
                          << std::setw(10) << c.getCreditHours() << "\n";
            }
        }
        std::cout << "============================================\n";
    }

    // ========== Enrollment Operations ==========
    void enrollStudent(int sId, const std::string& cCode) {
        db.enrollStudent(sId, cCode);
    }

    void listStudentEnrollments(int studentId) {
        auto list = db.getStudentEnrollments(studentId);
        
        std::cout << "\n============================================\n";
        std::cout << "              MY COURSES                    \n";
        std::cout << "============================================\n";
        
        if (list.empty()) {
            std::cout << "You haven't enrolled in any courses yet.\n";
        } else {
            double totalPoints = 0;
            int totalHours = 0;
            
            for (const auto& e : list) {
                std::cout << std::left << std::setw(10) << e.courseCode
                          << std::setw(30) << e.courseName.substr(0, 28);
                
                if (e.grade < 0) {
                    std::cout << std::setw(8) << "N/A";
                } else {
                    std::cout << std::setw(8) << e.grade;
                    totalPoints += calculateGPA(e.grade) * 3;
                    totalHours += 3;
                }
                std::cout << " | " << e.status << "\n";
            }
            
            if (totalHours > 0) {
                std::cout << "--------------------------------------------\n";
                std::cout << "GPA: " << std::fixed << std::setprecision(2) 
                          << (totalPoints / totalHours) << "\n";
            }
        }
        std::cout << "============================================\n";
    }

    // ========== Grade Operations ==========
    void showStudentGrades(int studentId) {
        auto list = db.getStudentGrades(studentId);
        
        std::cout << "\n============================================\n";
        std::cout << "              MY GRADES                     \n";
        std::cout << "============================================\n";
        
        if (list.empty()) {
            std::cout << "No grades available yet.\n";
        } else {
            std::cout << "Course      | S1   | S2   | Final | Total | Grade\n";
            std::cout << "--------------------------------------------\n";
            
            double totalGPA = 0;
            int count = 0;
            
            for (const auto& g : list) {
                std::cout << std::left << std::setw(12) << g.getCourseCode()
                          << "| " << std::setw(5) << g.getS1()
                          << "| " << std::setw(5) << g.getS2()
                          << "| " << std::setw(6) << g.getFinalExam()
                          << "| " << std::setw(6) << g.getTotal()
                          << "| " << std::setw(6) << g.getLetterGrade() << "\n";
                
                totalGPA += g.getGradePoints();
                count++;
            }
            
            if (count > 0) {
                std::cout << "--------------------------------------------\n";
                std::cout << "Cumulative GPA: " << std::fixed << std::setprecision(2) 
                          << (totalGPA / count) << "\n";
            }
        }
        std::cout << "============================================\n";
    }

    // ========== GPA Calculation ==========
    double calculateGPA(double score) {
        if (score >= 90) return 4.0;
        if (score >= 85) return 3.7;
        if (score >= 80) return 3.3;
        if (score >= 75) return 3.0;
        if (score >= 70) return 2.7;
        if (score >= 65) return 2.3;
        if (score >= 60) return 2.0;
        if (score >= 50) return 1.0;
        return 0.0;
    }

    std::string getLetterGrade(double score) {
        if (score >= 90) return "A+";
        if (score >= 85) return "A";
        if (score >= 80) return "B+";
        if (score >= 75) return "B";
        if (score >= 70) return "C+";
        if (score >= 65) return "C";
        if (score >= 60) return "D+";
        if (score >= 50) return "D";
        return "F";
    }

    // ========== Professor Operations ==========
    
    Database::ProfessorData* loginProfessor(const std::string& email, const std::string& password) {
        return db.authenticateProfessor(email, password);
    }
    
    void listProfessorCourses(int professorId) {
        auto list = db.getProfessorCourses(professorId);
        std::cout << "\n============================================\n";
        std::cout << "              MY COURSES                    \n";
        std::cout << "============================================\n";
        
        if (list.empty()) {
            std::cout << "No courses assigned.\n";
        } else {
            std::cout << std::left << std::setw(10) << "Code" 
                      << std::setw(30) << "Name" 
                      << std::setw(8) << "Year"
                      << std::setw(8) << "Term"
                      << std::setw(10) << "Students" << "\n";
            std::cout << "--------------------------------------------\n";
            
            for (const auto& c : list) {
                std::cout << std::left << std::setw(10) << c.courseCode
                          << std::setw(30) << c.courseName.substr(0, 28)
                          << std::setw(8) << c.year
                          << std::setw(8) << c.term
                          << std::setw(10) << c.studentCount << "\n";
            }
        }
        std::cout << "============================================\n";
    }
    
    // Get professor courses as vector (for numbered list selection)
    std::vector<Database::ProfessorCourse> getProfessorCourses(int professorId) {
        return db.getProfessorCourses(professorId);
    }
    
    // Get students in course as vector (for numbered list selection)
    std::vector<Database::CourseStudent> getStudentsInCourse(const std::string& courseCode) {
        return db.getStudentsInCourse(courseCode);
    }
    
    void listStudentsInCourse(const std::string& courseCode) {
        auto list = db.getStudentsInCourse(courseCode);
        std::cout << "\n============================================\n";
        std::cout << "         STUDENTS IN " << courseCode << "                \n";
        std::cout << "============================================\n";
        
        if (list.empty()) {
            std::cout << "No students enrolled.\n";
        } else {
            std::cout << std::left << std::setw(6) << "ID" 
                      << std::setw(25) << "Name" 
                      << std::setw(8) << "S1"
                      << std::setw(8) << "S2"
                      << std::setw(8) << "Final"
                      << std::setw(8) << "Total" << "\n";
            std::cout << "--------------------------------------------\n";
            
            for (const auto& s : list) {
                std::cout << std::left << std::setw(6) << s.studentId
                          << std::setw(25) << s.name.substr(0, 23)
                          << std::setw(8) << s.s1
                          << std::setw(8) << s.s2
                          << std::setw(8) << s.finalExam
                          << std::setw(8) << s.total << "\n";
            }
        }
        std::cout << "============================================\n";
    }
    
    void recordAttendanceForCourse(const std::string& courseCode, const std::string& date) {
        int courseId = db.getCourseIdByCode(courseCode);
        if (courseId == -1) {
            std::cout << "Course not found.\n";
            return;
        }
        
        auto students = db.getStudentsInCourse(courseCode);
        if (students.empty()) {
            std::cout << "No students enrolled.\n";
            return;
        }
        
        std::cout << "\nRecording attendance for " << date << ":\n";
        std::cout << "(P=Present, A=Absent, L=Late, E=Excused)\n\n";
        
        for (const auto& s : students) {
            std::cout << s.name << " [" << s.studentId << "]: ";
            char status;
            std::cin >> status;
            
            std::string statusStr;
            switch (toupper(status)) {
                case 'P': statusStr = "Present"; break;
                case 'A': statusStr = "Absent"; break;
                case 'L': statusStr = "Late"; break;
                case 'E': statusStr = "Excused"; break;
                default: statusStr = "Present";
            }
            
            db.recordAttendance(s.studentId, courseId, date, statusStr);
        }
        std::cout << "\nAttendance recorded successfully!\n";
    }
    
    void showAttendanceReport(const std::string& courseCode) {
        auto list = db.getCourseAttendance(courseCode);
        std::cout << "\n============================================\n";
        std::cout << "         ATTENDANCE REPORT - " << courseCode << "         \n";
        std::cout << "============================================\n";
        
        if (list.empty()) {
            std::cout << "No attendance records.\n";
        } else {
            std::cout << std::left << std::setw(6) << "ID" 
                      << std::setw(25) << "Name" 
                      << std::setw(12) << "Date"
                      << std::setw(10) << "Status" << "\n";
            std::cout << "--------------------------------------------\n";
            
            for (const auto& a : list) {
                std::cout << std::left << std::setw(6) << a.studentId
                          << std::setw(25) << a.studentName.substr(0, 23)
                          << std::setw(12) << a.date
                          << std::setw(10) << a.status << "\n";
            }
        }
        std::cout << "============================================\n";
    }
    
    void updateStudentGrade(int studentId, const std::string& courseCode, int gradeType, double score) {
        if (db.updateGrade(studentId, courseCode, gradeType, score)) {
            std::cout << "Grade updated successfully!\n";
        } else {
            std::cout << "Failed to update grade.\n";
        }
    }
    
    void showProfessorSchedule(int professorId) {
        auto list = db.getProfessorSchedule(professorId);
        std::cout << "\n============================================\n";
        std::cout << "              MY SCHEDULE                   \n";
        std::cout << "============================================\n";
        
        if (list.empty()) {
            std::cout << "No schedule available.\n";
        } else {
            std::cout << std::left << std::setw(12) << "Day" 
                      << std::setw(12) << "Time" 
                      << std::setw(10) << "Code"
                      << std::setw(25) << "Course"
                      << std::setw(10) << "Room" << "\n";
            std::cout << "--------------------------------------------\n";
            
            std::string lastDay = "";
            for (const auto& s : list) {
                if (s.day != lastDay) {
                    if (!lastDay.empty()) std::cout << "--------------------------------------------\n";
                    lastDay = s.day;
                }
                std::cout << std::left << std::setw(12) << s.day
                          << std::setw(12) << s.time
                          << std::setw(10) << s.courseCode
                          << std::setw(25) << s.courseName.substr(0, 23)
                          << std::setw(10) << s.room << "\n";
            }
        }
        std::cout << "============================================\n";
    }

    // ========== Admin: Classrooms ==========
    
    void listClassrooms() {
        auto list = db.getAllClassrooms();
        std::cout << "\n============================================\n";
        std::cout << "         CLASSROOMS & LABS                  \n";
        std::cout << "============================================\n";
        
        std::cout << std::left << std::setw(12) << "Code" 
                  << std::setw(12) << "Type" 
                  << std::setw(10) << "Capacity"
                  << std::setw(15) << "Building" << "\n";
        std::cout << "--------------------------------------------\n";
        
        for (const auto& c : list) {
            std::cout << std::left << std::setw(12) << c.roomCode
                      << std::setw(12) << c.roomType
                      << std::setw(10) << c.capacity
                      << std::setw(15) << c.building << "\n";
        }
        std::cout << "============================================\n";
        std::cout << "Total: " << list.size() << " rooms\n";
    }
    
    void addNewClassroom(const std::string& code, int capacity, const std::string& building, const std::string& type) {
        if (db.addClassroom(code, capacity, building, type)) {
            std::cout << "Classroom added successfully!\n";
        } else {
            std::cout << "Failed to add classroom.\n";
        }
    }

    // ========== Admin: Faculties ==========
    
    void listFaculties() {
        auto list = db.getAllFaculties();
        std::cout << "\n============================================\n";
        std::cout << "              FACULTIES                     \n";
        std::cout << "============================================\n";
        
        std::cout << std::left << std::setw(6) << "ID" 
                  << std::setw(8) << "Code" 
                  << std::setw(35) << "Name" << "\n";
        std::cout << "--------------------------------------------\n";
        
        for (const auto& f : list) {
            std::cout << std::left << std::setw(6) << f.id
                      << std::setw(8) << f.code
                      << std::setw(35) << f.name << "\n";
        }
        std::cout << "============================================\n";
    }

    // ========== Admin: Departments ==========
    
    void listDepartments() {
        auto list = db.getAllDepartments();
        std::cout << "\n============================================\n";
        std::cout << "              DEPARTMENTS                   \n";
        std::cout << "============================================\n";
        
        std::cout << std::left << std::setw(6) << "ID" 
                  << std::setw(8) << "Code" 
                  << std::setw(25) << "Name" 
                  << std::setw(20) << "Faculty" << "\n";
        std::cout << "--------------------------------------------\n";
        
        for (const auto& d : list) {
            std::cout << std::left << std::setw(6) << d.id
                      << std::setw(8) << d.code
                      << std::setw(25) << d.name.substr(0, 23)
                      << std::setw(20) << d.facultyName.substr(0, 18) << "\n";
        }
        std::cout << "============================================\n";
    }

    // ========== Admin: Instructors ==========
    
    void listInstructors() {
        auto list = db.getAllInstructors();
        std::cout << "\n============================================\n";
        std::cout << "              INSTRUCTORS                   \n";
        std::cout << "============================================\n";
        
        std::cout << std::left << std::setw(6) << "ID" 
                  << std::setw(8) << "Title"
                  << std::setw(20) << "Name" 
                  << std::setw(25) << "Email" << "\n";
        std::cout << "--------------------------------------------\n";
        
        for (const auto& i : list) {
            std::cout << std::left << std::setw(6) << i.id
                      << std::setw(8) << i.title
                      << std::setw(20) << i.name.substr(0, 18)
                      << std::setw(25) << i.email.substr(0, 23) << "\n";
        }
        std::cout << "============================================\n";
        std::cout << "Total: " << list.size() << " instructors\n";
    }
    
    void addNewInstructor(const std::string& name, const std::string& email, 
                          const std::string& phone, const std::string& spec, const std::string& title) {
        if (db.addInstructor(name, email, phone, spec, title)) {
            std::cout << "Instructor added successfully!\n";
        } else {
            std::cout << "Failed to add instructor.\n";
        }
    }

    // ========== Student: Payments ==========
    
    void showStudentPayments(int studentId) {
        auto list = db.getStudentPayments(studentId);
        std::cout << "\n============================================\n";
        std::cout << "              MY PAYMENTS                   \n";
        std::cout << "============================================\n";
        
        if (list.empty()) {
            std::cout << "No payment records.\n";
        } else {
            double total = 0;
            std::cout << std::left << std::setw(12) << "Date" 
                      << std::setw(12) << "Amount"
                      << std::setw(12) << "Method"
                      << std::setw(10) << "Status" << "\n";
            std::cout << "--------------------------------------------\n";
            
            for (const auto& p : list) {
                std::cout << std::left << std::setw(12) << p.date
                          << std::setw(12) << p.amount
                          << std::setw(12) << p.method
                          << std::setw(10) << p.status << "\n";
                if (p.status == "Paid") total += p.amount;
            }
            std::cout << "--------------------------------------------\n";
            std::cout << "Total Paid: " << total << " EGP\n";
        }
        std::cout << "============================================\n";
    }

    // ========== Student: Section ==========
    
    void showStudentSection(int studentId) {
        auto info = db.getStudentSection(studentId);
        std::cout << "\n============================================\n";
        std::cout << "              MY SECTION                    \n";
        std::cout << "============================================\n";
        
        if (info.sectionId == 0) {
            std::cout << "Not assigned to any section.\n";
        } else {
            std::cout << "  Section #: " << info.sectionNumber << "\n";
            std::cout << "  Year: " << info.year << "\n";
            std::cout << "  Term: " << info.term << "\n";
            std::cout << "  Track: " << info.track << "\n";
            std::cout << "  Students in Section: " << info.studentCount << "\n";
        }
        std::cout << "============================================\n";
    }

    // ========== Assign Course to Instructor (من GUI) ==========
    void assignCourseToInstructor(int instructorId, const std::string& courseCode) {
        int courseId = db.getCourseIdByCode(courseCode);
        if (courseId == -1) {
            std::cout << "Course not found.\n";
            return;
        }
        
        if (db.assignCourseOffering(instructorId, courseId)) {
            std::cout << "Course assigned successfully!\n";
        } else {
            std::cout << "Failed to assign course.\n";
        }
    }
    
    // ========== Mercy Grading - Pearson (من GUI) ==========
    void applyMercyGrades(const std::string& courseCode, double mercyRange) {
        auto students = db.getStudentsInCourse(courseCode);
        
        std::cout << "\n--- MERCY CANDIDATES (below 60%, within " << mercyRange << "% range) ---\n";
        std::cout << std::left << std::setw(20) << "Student" 
                  << std::setw(10) << "Total" 
                  << std::setw(10) << "%" 
                  << std::setw(10) << "Before"
                  << std::setw(10) << "After" << "\n";
        std::cout << "--------------------------------------------\n";
        
        int count = 0;
        double passingGrade = 60.0;
        double lowerBound = passingGrade - mercyRange;
        
        for (const auto& s : students) {
            // Assuming max score is 150 (3 credit hours * 50)
            double percent = (s.total / 150.0) * 100;
            
            if (percent >= lowerBound && percent < passingGrade) {
                std::string before = "F";
                std::string after = "P";
                
                std::cout << std::left << std::setw(20) << s.name.substr(0, 18)
                          << std::setw(10) << s.total
                          << std::setw(10) << std::fixed << std::setprecision(1) << percent
                          << std::setw(10) << before
                          << std::setw(10) << after << "\n";
                count++;
            }
        }
        
        if (count == 0) {
            std::cout << "No students qualify for mercy.\n";
        } else {
            std::cout << "--------------------------------------------\n";
            std::cout << "Total candidates: " << count << "\n\n";
            
            std::cout << "Apply mercy to all? (y/n): ";
            char confirm;
            std::cin >> confirm;
            
            if (confirm == 'y' || confirm == 'Y') {
                // Update their grades to pass
                for (const auto& s : students) {
                    double percent = (s.total / 150.0) * 100;
                    if (percent >= lowerBound && percent < passingGrade) {
                        // Add mercy points to reach 60%
                        double needed = (passingGrade / 100.0 * 150) - s.total;
                        double newTotal = s.total + needed;
                        db.updateStudentGrade(s.studentId, courseCode, 3, s.finalExam + needed);
                    }
                }
                std::cout << "Mercy applied to " << count << " students! They now have P (Pass) grade.\n";
            }
        }
    }
    
    // ========== News Management (من GUI) ==========
    void listNews() {
        auto list = db.getAllNews();
        std::cout << "\n============================================\n";
        std::cout << "              ANNOUNCEMENTS                 \n";
        std::cout << "============================================\n";
        
        if (list.empty()) {
            std::cout << "No announcements.\n";
        } else {
            for (const auto& n : list) {
                std::cout << "[" << n.date << "] " << n.title << "\n";
                std::cout << "  " << n.content << "\n\n";
            }
        }
        std::cout << "============================================\n";
    }
    
    void addNews(const std::string& title, const std::string& content) {
        db.insertNews(title, content);
    }
};

#endif

