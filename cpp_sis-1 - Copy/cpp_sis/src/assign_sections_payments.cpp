/*
 * assign_sections_payments.cpp - Assigns sections and payments to students
 * Replaces: assign_sections_payments.py
 * 
 * Compile: g++ src/assign_sections_payments.cpp src/sqlite3.c -o assign_sections.exe -Iinclude
 * Run: ./assign_sections.exe
 */
#include <iostream>
#include <sqlite3.h>
#include <string>
#include <vector>
#include <map>
#include <algorithm>
#include <cstdio>

struct Student {
    int id;
    std::string name;
    int level;
    int semester;
    std::string track;
};

struct Payment {
    double amount;
    std::string method;
    std::string date;
    std::string status;
    std::string desc;
};

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

    // 1. Get all students
    std::string sql = "SELECT id, name, level, semester, track FROM students WHERE name != '' ORDER BY name";
    sqlite3_stmt* stmt;
    sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr);

    std::vector<Student> students;
    while (sqlite3_step(stmt) == SQLITE_ROW) {
        Student s;
        s.id = sqlite3_column_int(stmt, 0);
        const char* name = (const char*)sqlite3_column_text(stmt, 1);
        s.name = name ? name : "";
        s.level = sqlite3_column_int(stmt, 2);
        s.semester = sqlite3_column_int(stmt, 3);
        const char* track = (const char*)sqlite3_column_text(stmt, 4);
        s.track = track ? track : "";
        students.push_back(s);
    }
    sqlite3_finalize(stmt);

    std::cout << "Found " << students.size() << " students\n";
    std::cout << "==================================================\n";

    // 2. Group students by level + semester + track
    std::map<std::string, std::vector<Student*>> groups;
    for (auto& s : students) {
        std::string key = std::to_string(s.level) + "_" + std::to_string(s.semester) + "_" + s.track;
        groups[key].push_back(&s);
    }

    // 3. For each group, assign sections
    for (auto& pair : groups) {
        auto& group = pair.second;
        if (group.empty()) continue;

        int level = group[0]->level;
        int semester = group[0]->semester;
        std::string track = group[0]->track;

        // Find section
        std::string findSql = "SELECT section_id, section_number FROM sections WHERE year = ? AND term = ? AND track = ? ORDER BY section_number";
        sqlite3_prepare_v2(db, findSql.c_str(), -1, &stmt, nullptr);
        sqlite3_bind_int(stmt, 1, level);
        sqlite3_bind_int(stmt, 2, semester);
        sqlite3_bind_text(stmt, 3, track.c_str(), -1, SQLITE_TRANSIENT);

        std::vector<int> sectionIds;
        while (sqlite3_step(stmt) == SQLITE_ROW) {
            sectionIds.push_back(sqlite3_column_int(stmt, 0));
        }
        sqlite3_finalize(stmt);

        if (sectionIds.empty()) {
            std::cout << "Creating section for Level " << level << ", Semester " << semester << ", Track " << track << "\n";
            std::string createSql = "INSERT INTO sections (year, term, track, section_number) VALUES (?, ?, ?, 1)";
            sqlite3_prepare_v2(db, createSql.c_str(), -1, &stmt, nullptr);
            sqlite3_bind_int(stmt, 1, level);
            sqlite3_bind_int(stmt, 2, semester);
            sqlite3_bind_text(stmt, 3, track.c_str(), -1, SQLITE_TRANSIENT);
            sqlite3_step(stmt);
            sectionIds.push_back((int)sqlite3_last_insert_rowid(db));
            sqlite3_finalize(stmt);
        }

        // Sort students alphabetically
        std::sort(group.begin(), group.end(), [](Student* a, Student* b) {
            return a->name < b->name;
        });

        // Assign students to sections
        int studentsPerSection = 30;
        for (size_t i = 0; i < group.size(); i++) {
            size_t sectionIndex = i / studentsPerSection;
            if (sectionIndex >= sectionIds.size()) sectionIndex = sectionIds.size() - 1;
            int assignedSection = sectionIds[sectionIndex];

            std::string updateSql = "UPDATE students SET section_id = ? WHERE id = ?";
            sqlite3_prepare_v2(db, updateSql.c_str(), -1, &stmt, nullptr);
            sqlite3_bind_int(stmt, 1, assignedSection);
            sqlite3_bind_int(stmt, 2, group[i]->id);
            sqlite3_step(stmt);
            sqlite3_finalize(stmt);

            std::cout << "Assigned '" << group[i]->name << "' to Section ID " << assignedSection << "\n";
        }
    }

    // 4. Add payments
    std::cout << "\n==================================================\n";
    std::cout << "Adding payments for all students...\n";
    std::cout << "==================================================\n";

    Payment payments[] = {
        {5000.00, "Cash", "2024-09-15", "Paid", "Tuition Fee - Term 1"},
        {500.00, "Card", "2024-09-20", "Paid", "Registration Fee"},
        {1500.00, "Bank Transfer", "2024-10-01", "Paid", "Lab Equipment Fee"},
        {5000.00, "Cash", "2025-01-10", "Pending", "Tuition Fee - Term 2"}
    };

    for (const auto& s : students) {
        // Check if student has payments
        std::string checkSql = "SELECT COUNT(*) FROM payments WHERE student_id = ?";
        sqlite3_prepare_v2(db, checkSql.c_str(), -1, &stmt, nullptr);
        sqlite3_bind_int(stmt, 1, s.id);
        sqlite3_step(stmt);
        int count = sqlite3_column_int(stmt, 0);
        sqlite3_finalize(stmt);

        if (count == 0) {
            for (int i = 0; i < 4; i++) {
                char receipt[32];
                snprintf(receipt, sizeof(receipt), "RCP-%03d-%03d", s.id, i + 1);

                std::string insertSql = "INSERT INTO payments (student_id, amount, method, date, status, description, receipt_number) VALUES (?, ?, ?, ?, ?, ?, ?)";
                sqlite3_prepare_v2(db, insertSql.c_str(), -1, &stmt, nullptr);
                sqlite3_bind_int(stmt, 1, s.id);
                sqlite3_bind_double(stmt, 2, payments[i].amount);
                sqlite3_bind_text(stmt, 3, payments[i].method.c_str(), -1, SQLITE_TRANSIENT);
                sqlite3_bind_text(stmt, 4, payments[i].date.c_str(), -1, SQLITE_TRANSIENT);
                sqlite3_bind_text(stmt, 5, payments[i].status.c_str(), -1, SQLITE_TRANSIENT);
                sqlite3_bind_text(stmt, 6, payments[i].desc.c_str(), -1, SQLITE_TRANSIENT);
                sqlite3_bind_text(stmt, 7, receipt, -1, SQLITE_TRANSIENT);
                sqlite3_step(stmt);
                sqlite3_finalize(stmt);
            }
            std::cout << "Added 4 payments for '" << s.name << "'\n";
        } else {
            std::cout << "'" << s.name << "' already has " << count << " payments - skipped\n";
        }
    }

    std::cout << "\n==================================================\n";
    std::cout << "DONE! All sections and payments assigned.\n";
    std::cout << "==================================================\n";

    sqlite3_close(db);
    return 0;
}
