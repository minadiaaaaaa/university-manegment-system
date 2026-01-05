#include <iostream>
#include <sqlite3.h>
#include <string>
#include <vector>
#include <iomanip>

int main() {
    sqlite3* db;
    char* errMsg = 0;
    int rc;

    // Open Database
    rc = sqlite3_open("university.db", &db);
    if (rc) {
        std::cerr << "Can't open database: " << sqlite3_errmsg(db) << std::endl;
        return 1;
    }
    std::cout << "Opened database successfully\n";

    int studentId = 7; // Target student

    // 1. Force Update Grade for CCNA301 to A+ (95 Total)
    // This ensures we have a known good grade to calculate GPA from
    std::cout << "Updating Grade for CCNA301 to A+...\n";
    std::string updateGradeSql = R"(
        UPDATE grades 
        SET s1=30, s2=30, final_exam=35, total=95, letter_grade='A+', grade_points=4.0, updated_at=datetime('now')
        WHERE enrollment_id IN (
            SELECT e.enrollment_id FROM enrollments e
            JOIN courses c ON e.course_id = c.course_id
            WHERE e.student_id = 7 AND c.course_code = 'CCNA301'
        );
    )";
    
    rc = sqlite3_exec(db, updateGradeSql.c_str(), NULL, 0, &errMsg);
    if (rc != SQLITE_OK) {
        std::cerr << "Grade Update Error: " << errMsg << "\n";
        sqlite3_free(errMsg);
    } else {
        std::cout << "Grade updated to A+.\n";
    }

    // 2. Calculate Cumulative GPA
    std::cout << "Calculating Cumulative GPA...\n";
    std::string calcGpaSql = "SELECT grade_points FROM grades g "
                             "JOIN enrollments e ON g.enrollment_id = e.enrollment_id "
                             "WHERE e.student_id = 7 AND g.total > 0;";
    
    sqlite3_stmt* stmt;
    sqlite3_prepare_v2(db, calcGpaSql.c_str(), -1, &stmt, NULL);
    
    double totalPoints = 0;
    int count = 0;
    
    while (sqlite3_step(stmt) == SQLITE_ROW) {
        totalPoints += sqlite3_column_double(stmt, 0);
        count++;
    }
    sqlite3_finalize(stmt);
    
    double gpa = (count > 0) ? (totalPoints / count) : 0.0;
    std::cout << "Calculated GPA: " << std::fixed << std::setprecision(2) << gpa << " (from " << count << " courses)\n";

    // 3. Update Profile (students table)
    std::cout << "Updating Student Profile GPA...\n";
    std::string updateProfileSql = "UPDATE students SET gpa = " + std::to_string(gpa) + " WHERE id = 7;";
    
    rc = sqlite3_exec(db, updateProfileSql.c_str(), NULL, 0, &errMsg);
    if (rc != SQLITE_OK) {
        std::cerr << "Profile Update Failed: " << errMsg << "\n";
        sqlite3_free(errMsg);
    } else {
        std::cout << "Profile GPA updated successfully!\n";
    }
    
    // 4. Verify
    std::cout << "\n--- Verification ---\n";
    sqlite3_prepare_v2(db, "SELECT name, gpa FROM students WHERE id=7", -1, &stmt, NULL);
    if (sqlite3_step(stmt) == SQLITE_ROW) {
        const char* name = (const char*)sqlite3_column_text(stmt, 0);
        double dbGpa = sqlite3_column_double(stmt, 1);
        std::cout << "Student: " << (name ? name : "N/A") << "\n";
        std::cout << "Stored GPA: " << std::fixed << std::setprecision(2) << dbGpa << "\n";
    }
    sqlite3_finalize(stmt);

    sqlite3_close(db);
    return 0;
}
