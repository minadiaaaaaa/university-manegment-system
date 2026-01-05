/*
 * check_schema.cpp - Shows table schema
 * Replaces: check_schema.py
 * 
 * Compile: g++ src/check_schema.cpp src/sqlite3.c -o check_schema.exe -Iinclude
 * Run: ./check_schema.exe
 */
#include <iostream>
#include <sqlite3.h>
#include <string>

int main() {
    sqlite3* db;
    int rc;

    rc = sqlite3_open("university.db", &db);
    if (rc) {
        std::cerr << "Can't open database: " << sqlite3_errmsg(db) << std::endl;
        return 1;
    }

    // Show table schema
    std::string sql = "SELECT sql FROM sqlite_master WHERE name='instructors'";
    sqlite3_stmt* stmt;
    
    std::cout << "Table Schema:\n";
    sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr);
    if (sqlite3_step(stmt) == SQLITE_ROW) {
        const char* schema = (const char*)sqlite3_column_text(stmt, 0);
        std::cout << (schema ? schema : "Table not found") << "\n";
    }
    sqlite3_finalize(stmt);

    std::cout << "\n==================================================\n";

    // Show all columns
    std::cout << "\nAll Columns:\n";
    sql = "PRAGMA table_info(instructors)";
    sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr);
    while (sqlite3_step(stmt) == SQLITE_ROW) {
        int cid = sqlite3_column_int(stmt, 0);
        const char* name = (const char*)sqlite3_column_text(stmt, 1);
        const char* type = (const char*)sqlite3_column_text(stmt, 2);
        std::cout << "  " << cid << ": " << (name ? name : "") << " (" << (type ? type : "") << ")\n";
    }
    sqlite3_finalize(stmt);

    sqlite3_close(db);
    return 0;
}
