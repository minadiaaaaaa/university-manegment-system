/*
 * add_password.cpp - Adds password column to instructors table
 * Replaces: add_password.py
 * 
 * Compile: g++ src/add_password.cpp src/sqlite3.c -o add_password.exe -Iinclude
 * Run: ./add_password.exe
 */
#include <iostream>
#include <sqlite3.h>
#include <string>

int main() {
    sqlite3* db;
    char* errMsg = 0;
    int rc;

    rc = sqlite3_open("university.db", &db);
    if (rc) {
        std::cerr << "Can't open database: " << sqlite3_errmsg(db) << std::endl;
        return 1;
    }

    // Add password column
    std::string sql = "ALTER TABLE instructors ADD COLUMN password TEXT DEFAULT 'pass123'";
    rc = sqlite3_exec(db, sql.c_str(), nullptr, nullptr, &errMsg);
    
    if (rc == SQLITE_OK) {
        std::cout << "Password column added successfully!\n";
    } else {
        std::cout << "Note: " << errMsg << "\n";
        sqlite3_free(errMsg);
    }

    // Show instructors with passwords
    std::cout << "\nInstructors:\n";
    sql = "SELECT instructor_id, name, email, password FROM instructors";
    sqlite3_stmt* stmt;
    sqlite3_prepare_v2(db, sql.c_str(), -1, &stmt, nullptr);
    
    while (sqlite3_step(stmt) == SQLITE_ROW) {
        int id = sqlite3_column_int(stmt, 0);
        const char* name = (const char*)sqlite3_column_text(stmt, 1);
        const char* email = (const char*)sqlite3_column_text(stmt, 2);
        const char* pass = (const char*)sqlite3_column_text(stmt, 3);
        
        std::cout << "(" << id << ", '" 
                  << (name ? name : "") << "', '" 
                  << (email ? email : "") << "', '" 
                  << (pass ? pass : "") << "')\n";
    }
    sqlite3_finalize(stmt);

    sqlite3_close(db);
    return 0;
}
