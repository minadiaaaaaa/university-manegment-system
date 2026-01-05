/*
 * assign_courses.cpp - Assigns Network courses to an instructor
 * Replaces: temp_assign_courses.py
 * 
 * Compile: g++ src/assign_courses.cpp src/sqlite3.c -o assign_courses.exe -Iinclude
 * Run: ./assign_courses.exe
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

    // 1. Get Network courses
    std::string sql = "SELECT course_id, course_name, year, term, track FROM courses WHERE track = 'Network'";
    sqlite3_stmt* stmt;
    
    rc = sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr);
    if (rc != SQLITE_OK) {
        std::cerr << "SQL Error: " << sqlite3_errmsg(db) << std::endl;
        sqlite3_close(db);
        return 1;
    }

    struct CourseData {
        int id;
        std::string name;
        int year;
        int term;
        std::string track;
    };
    std::vector<CourseData> courses;

    while (sqlite3_step(stmt) == SQLITE_ROW) {
        CourseData c;
        c.id = sqlite3_column_int(stmt, 0);
        const char* name = (const char*)sqlite3_column_text(stmt, 1);
        c.name = name ? name : "";
        c.year = sqlite3_column_int(stmt, 2);
        c.term = sqlite3_column_int(stmt, 3);
        const char* track = (const char*)sqlite3_column_text(stmt, 4);
        c.track = track ? track : "";
        courses.push_back(c);
    }
    sqlite3_finalize(stmt);

    std::cout << "Found " << courses.size() << " Network courses.\n";

    int assigned_count = 0;

    for (const auto& course : courses) {
        // 2. Find section for this course
        std::string findSectionSql = "SELECT section_id FROM sections WHERE year = ? AND term = ? AND track = ? LIMIT 1";
        sqlite3_prepare_v2(db, findSectionSql.c_str(), -1, &stmt, nullptr);
        sqlite3_bind_int(stmt, 1, course.year);
        sqlite3_bind_int(stmt, 2, course.term);
        sqlite3_bind_text(stmt, 3, course.track.c_str(), -1, SQLITE_TRANSIENT);

        int section_id = -1;
        if (sqlite3_step(stmt) == SQLITE_ROW) {
            section_id = sqlite3_column_int(stmt, 0);
        }
        sqlite3_finalize(stmt);

        // Create section if not found
        if (section_id == -1) {
            std::cout << "Creating section for " << course.name << "...\n";
            std::string createSql = "INSERT INTO sections (year, term, track, section_number) VALUES (?, ?, ?, 1)";
            sqlite3_prepare_v2(db, createSql.c_str(), -1, &stmt, nullptr);
            sqlite3_bind_int(stmt, 1, course.year);
            sqlite3_bind_int(stmt, 2, course.term);
            sqlite3_bind_text(stmt, 3, course.track.c_str(), -1, SQLITE_TRANSIENT);
            if (sqlite3_step(stmt) == SQLITE_DONE) {
                section_id = (int)sqlite3_last_insert_rowid(db);
                std::cout << " -> Created Section ID " << section_id << "\n";
            }
            sqlite3_finalize(stmt);
        }

        if (section_id == -1) continue;

        // 3. Insert course offering
        std::string insertSql = "INSERT INTO course_offerings (course_id, instructor_id, section_id, academic_year, semester) VALUES (?, ?, ?, ?, ?)";
        sqlite3_prepare_v2(db, insertSql.c_str(), -1, &stmt, nullptr);
        sqlite3_bind_int(stmt, 1, course.id);
        sqlite3_bind_int(stmt, 2, instructor_id);
        sqlite3_bind_int(stmt, 3, section_id);
        sqlite3_bind_text(stmt, 4, academic_year.c_str(), -1, SQLITE_TRANSIENT);
        sqlite3_bind_int(stmt, 5, course.term);

        rc = sqlite3_step(stmt);
        sqlite3_finalize(stmt);

        if (rc == SQLITE_DONE) {
            assigned_count++;
            std::cout << "Assigned: " << course.name << " -> Section " << section_id << "\n";
        } else if (rc == SQLITE_CONSTRAINT) {
            std::cout << "Skipped " << course.name << " (Already assigned)\n";
        } else {
            std::cerr << "Error assigning " << course.name << ": " << sqlite3_errmsg(db) << "\n";
        }
    }

    std::cout << "\nSuccessfully assigned " << assigned_count << " courses to Dr. Mohamed Ali.\n";
    sqlite3_close(db);
    return 0;
}
