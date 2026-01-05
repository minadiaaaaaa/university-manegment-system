/*
 * setup_dr_ali.cpp - Sets up courses and enrollments for Dr. Mohamed Ali
 * Replaces: temp_setup_ali.py
 * 
 * Compile: g++ src/setup_dr_ali.cpp src/sqlite3.c -o setup_dr_ali.exe -Iinclude
 * Run: ./setup_dr_ali.exe
 */
#include <iostream>
#include <sqlite3.h>
#include <string>
#include <vector>

int main() {
    sqlite3* db;
    char* errMsg = 0;
    int rc;

    rc = sqlite3_open("university.db", &db);
    if (rc) {
        std::cerr << "Can't open database: " << sqlite3_errmsg(db) << std::endl;
        return 1;
    }
    std::cout << "Opened database successfully\n";

    int instructor_id = 2; // Dr. Mohamed Ali
    std::string academic_year = "2024-2025";

    // 1. Clear current assignments
    std::string clearSql = "DELETE FROM course_offerings WHERE instructor_id = ?";
    sqlite3_stmt* stmt;
    sqlite3_prepare_v2(db, clearSql.c_str(), -1, &stmt, nullptr);
    sqlite3_bind_int(stmt, 1, instructor_id);
    sqlite3_step(stmt);
    sqlite3_finalize(stmt);
    std::cout << "Cleared previous assignments for Dr. Ali.\n";

    // 2. Select 3 courses
    std::vector<std::string> codes = {"CCNA301", "DC302", "NA301"};
    int assigned = 0;
    std::vector<int> assigned_course_ids;

    for (const auto& code : codes) {
        std::string findSql = "SELECT course_id, course_name, year, term, track FROM courses WHERE course_code = ?";
        sqlite3_prepare_v2(db, findSql.c_str(), -1, &stmt, nullptr);
        sqlite3_bind_text(stmt, 1, code.c_str(), -1, SQLITE_TRANSIENT);

        if (sqlite3_step(stmt) == SQLITE_ROW) {
            int course_id = sqlite3_column_int(stmt, 0);
            const char* name = (const char*)sqlite3_column_text(stmt, 1);
            int year = sqlite3_column_int(stmt, 2);
            int term = sqlite3_column_int(stmt, 3);
            const char* track_c = (const char*)sqlite3_column_text(stmt, 4);
            std::string track = track_c ? track_c : "";
            std::string courseName = name ? name : "";
            sqlite3_finalize(stmt);

            // Find/Create section
            std::string findSecSql = "SELECT section_id FROM sections WHERE year = ? AND term = ? AND track = ?";
            sqlite3_prepare_v2(db, findSecSql.c_str(), -1, &stmt, nullptr);
            sqlite3_bind_int(stmt, 1, year);
            sqlite3_bind_int(stmt, 2, term);
            sqlite3_bind_text(stmt, 3, track.c_str(), -1, SQLITE_TRANSIENT);

            int section_id = -1;
            if (sqlite3_step(stmt) == SQLITE_ROW) {
                section_id = sqlite3_column_int(stmt, 0);
            }
            sqlite3_finalize(stmt);

            if (section_id == -1) {
                std::string createSecSql = "INSERT INTO sections (year, term, track, section_number) VALUES (?, ?, ?, 1)";
                sqlite3_prepare_v2(db, createSecSql.c_str(), -1, &stmt, nullptr);
                sqlite3_bind_int(stmt, 1, year);
                sqlite3_bind_int(stmt, 2, term);
                sqlite3_bind_text(stmt, 3, track.c_str(), -1, SQLITE_TRANSIENT);
                sqlite3_step(stmt);
                section_id = (int)sqlite3_last_insert_rowid(db);
                sqlite3_finalize(stmt);
            }

            // Insert offering
            std::string insertSql = "INSERT INTO course_offerings (course_id, instructor_id, section_id, academic_year, semester) VALUES (?, ?, ?, ?, ?)";
            sqlite3_prepare_v2(db, insertSql.c_str(), -1, &stmt, nullptr);
            sqlite3_bind_int(stmt, 1, course_id);
            sqlite3_bind_int(stmt, 2, instructor_id);
            sqlite3_bind_int(stmt, 3, section_id);
            sqlite3_bind_text(stmt, 4, academic_year.c_str(), -1, SQLITE_TRANSIENT);
            sqlite3_bind_int(stmt, 5, term);
            sqlite3_step(stmt);
            sqlite3_finalize(stmt);

            assigned++;
            assigned_course_ids.push_back(course_id);
            std::cout << "Assigned " << code << " (" << courseName << ")\n";
        } else {
            sqlite3_finalize(stmt);
            std::cout << "Course " << code << " not found.\n";
        }
    }

    // 3. Enroll students
    std::string findStudentsSql = "SELECT id, name FROM students WHERE track = 'Network' OR track = 'General' LIMIT 5";
    sqlite3_prepare_v2(db, findStudentsSql.c_str(), -1, &stmt, nullptr);

    struct StudentData { int id; std::string name; };
    std::vector<StudentData> students;
    while (sqlite3_step(stmt) == SQLITE_ROW) {
        StudentData s;
        s.id = sqlite3_column_int(stmt, 0);
        const char* n = (const char*)sqlite3_column_text(stmt, 1);
        s.name = n ? n : "";
        students.push_back(s);
    }
    sqlite3_finalize(stmt);

    for (const auto& s : students) {
        for (int c_id : assigned_course_ids) {
            std::string enrollSql = "INSERT OR IGNORE INTO enrollments (student_id, course_id, academic_year, semester, status) VALUES (?, ?, ?, 1, 'Enrolled')";
            sqlite3_prepare_v2(db, enrollSql.c_str(), -1, &stmt, nullptr);
            sqlite3_bind_int(stmt, 1, s.id);
            sqlite3_bind_int(stmt, 2, c_id);
            sqlite3_bind_text(stmt, 3, academic_year.c_str(), -1, SQLITE_TRANSIENT);
            rc = sqlite3_step(stmt);
            sqlite3_finalize(stmt);
            if (rc == SQLITE_DONE) {
                std::cout << "Enrolled " << s.name << " in Course ID " << c_id << "\n";
            }
        }
    }

    std::cout << "\nSuccessfully finished! Assigned " << assigned << " courses and enrolled students.\n";
    sqlite3_close(db);
    return 0;
}
