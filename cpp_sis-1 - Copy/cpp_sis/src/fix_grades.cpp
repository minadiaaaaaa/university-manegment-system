#include <iostream>
#include <sqlite3.h>
#include <string>

// Simple error checking helper
void checkDB(int result, char* errMsg) {
    if (result != SQLITE_OK) {
        std::cerr << "SQL Error: " << errMsg << std::endl;
        sqlite3_free(errMsg);
    }
}

int main() {
    sqlite3* db;
    char* errMsg = 0;
    int rc;

    // Open Database
    rc = sqlite3_open("university.db", &db);
    if (rc) {
        std::cerr << "Can't open database: " << sqlite3_errmsg(db) << std::endl;
        return 0;
    }
    std::cout << "Opened database successfully\n";

    // 1. Get Course ID for CCNA301
    // We assume it exists from previous steps, but let's be sure
    // We will hardcode the logic to update Grade for Student 7, Course CCNA301 (Offering ?)
    
    // We need to find the enrollment_id first
    // Query: Get enrollment_id for Student 7 and Course 'CCNA301'
    std::string sqlFindEnroll = "SELECT e.enrollment_id FROM enrollments e "
                                "JOIN courses c ON e.course_id = c.course_id "
                                "WHERE e.student_id = 7 AND c.course_code = 'CCNA301';";
    
    sqlite3_stmt* stmt;
    rc = sqlite3_prepare_v2(db, sqlFindEnroll.c_str(), -1, &stmt, NULL);
    
    int enrollmentId = -1;
    if (rc == SQLITE_OK && sqlite3_step(stmt) == SQLITE_ROW) {
        enrollmentId = sqlite3_column_int(stmt, 0);
    }
    sqlite3_finalize(stmt);

    if (enrollmentId == -1) {
        std::cout << "Student 7 is not enrolled in CCNA301. Attempting to enroll...\n";
        // Need to find course_id first
        int courseId = -1;
        sqlite3_prepare_v2(db, "SELECT course_id FROM courses WHERE course_code='CCNA301'", -1, &stmt, NULL);
        if (sqlite3_step(stmt) == SQLITE_ROW) courseId = sqlite3_column_int(stmt, 0);
        sqlite3_finalize(stmt);

        if (courseId == -1) {
             std::cerr << "Course CCNA301 not found! Cannot enroll.\n";
             sqlite3_close(db);
             return 1;
        }

        std::string sqlEnroll = "INSERT INTO enrollments (student_id, course_id, academic_year, semester, status) "
                                "VALUES (7, " + std::to_string(courseId) + ", '2024-2025', 1, 'Enrolled');";
        rc = sqlite3_exec(db, sqlEnroll.c_str(), NULL, 0, &errMsg);
        if (rc != SQLITE_OK) {
            std::cerr << "Enrollment failed: " << errMsg << "\n";
            sqlite3_free(errMsg);
            sqlite3_close(db);
            return 1;
        }
        enrollmentId = (int)sqlite3_last_insert_rowid(db);
        std::cout << "Enrolled Student 7 in CCNA301 (Enrollment ID: " << enrollmentId << ")\n";
    } else {
        std::cout << "Found Enrollment ID: " << enrollmentId << "\n";
    }

    // 2. Insert/Update Grade
    // Let's give them a good grade: S1=25, S2=25, Final=40 => Total=90 (A+)
    double s1 = 25.0;
    double s2 = 25.0;
    double finalExam = 40.0;
    double total = s1 + s2 + finalExam;
    std::string letter = "A+";
    double points = 4.0;

    // Check if grade exists
    bool gradeExists = false;
    sqlite3_prepare_v2(db, ("SELECT grade_id FROM grades WHERE enrollment_id=" + std::to_string(enrollmentId)).c_str(), -1, &stmt, NULL);
    if (sqlite3_step(stmt) == SQLITE_ROW) gradeExists = true;
    sqlite3_finalize(stmt);

    std::string sqlGrade;
    if (gradeExists) {
        sqlGrade = "UPDATE grades SET s1=" + std::to_string(s1) + 
                   ", s2=" + std::to_string(s2) + 
                   ", final_exam=" + std::to_string(finalExam) + 
                   ", total=" + std::to_string(total) + 
                   ", letter_grade='" + letter + "'" +
                   ", grade_points=" + std::to_string(points) +
                   ", updated_at=datetime('now') WHERE enrollment_id=" + std::to_string(enrollmentId) + ";";
    } else {
        sqlGrade = "INSERT INTO grades (enrollment_id, s1, s2, final_exam, total, letter_grade, grade_points, updated_at) "
                   "VALUES (" + std::to_string(enrollmentId) + ", " + 
                   std::to_string(s1) + ", " + std::to_string(s2) + ", " + 
                   std::to_string(finalExam) + ", " + std::to_string(total) + ", '" + 
                   letter + "', " + std::to_string(points) + ", datetime('now'));";
    }

    rc = sqlite3_exec(db, sqlGrade.c_str(), NULL, 0, &errMsg);
    if (rc != SQLITE_OK) {
        std::cerr << "Grade Update Failed: " << errMsg << "\n";
        sqlite3_free(errMsg);
    } else {
        std::cout << "Grade updated successfully! (Total: " << total << ", GPA: " << points << ")\n";
    }

    sqlite3_close(db);
    return 0;
}
