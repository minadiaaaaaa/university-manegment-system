/*
 * SIS CLI - نظام إدارة الطلاب المتكامل
 * مربوط بقاعدة البيانات SQLite
 * 
 * يوضح استخدام:
 * - Pointers: Student* s = db.loginStudent(...)
 * - OOP: classes للـ Student, Course, Grade
 * - Arrays: float grades[5], Student arr[100]
 * - Vectors: vector<Student> students
 * - File I/O: ofstream, ifstream
 * - Database: SQLite connection
 */
#include <iostream>
#include <iomanip>
#include <fstream>
#include <vector>
#include <string>
#include "include/DatabaseManager.h"
#include "include/AdminMenu.h"
#include "include/StudentMenu.h"
#include "include/ProfessorMenu.h"
#include "include/Professor.h"
#include "include/ConsoleHelper.h"

using namespace std;



// ====================================================================
//                     MAIN FUNCTION - DATABASE CONNECTED
// ====================================================================

int main() {
    // اتصال بقاعدة البيانات
    DatabaseManager db;
    
    cout << "\n";
    cout << "============================================================\n";
    cout << "     STUDENT INFORMATION SYSTEM - CLI                       \n";
    cout << "     Database Connected | OOP | Pointers | Arrays           \n";
    cout << "============================================================\n";
    
    int choice;
    do {
        cout << "\n--- MAIN MENU ---\n";
        cout << "1. Admin Login\n";
        cout << "2. Student Login\n";
        cout << "3. Professor Login\n";
        cout << "0. Exit\n";
        cout << "Choice: "; cin >> choice;
        
        if (choice == 1) {
            // ========== Admin Login ==========
            cout << "\n--- Admin Login ---\n";
            string u, p;
            cout << "Username: "; cin >> u;
            cout << "Password: "; cin >> p;
            
            if (db.loginAdmin(u, p)) {
                cout << "Login successful!\n";
                AdminMenu::show(db);
            } else {
                cout << "Invalid credentials!\n";
            }
            
        } else if (choice == 2) {
            // ========== Student Login ==========
            cout << "\n--- Student Login ---\n";
            string e, p;
            cout << "Email: "; cin >> e;
            cout << "Password: "; cin >> p;
            
            // Pointer usage: loginStudent returns Student*
            Student* s = db.loginStudent(e, p);
            
            if (s != nullptr) {
                cout << "Welcome, " << s->getName() << "!\n";
                StudentMenu::show(db, s);
                
                // Memory cleanup - important!
                delete s;
            } else {
                cout << "Invalid email or password!\n";
            }
            
        } else if (choice == 3) {
            // ========== Professor Login ==========
            cout << "\n--- Professor Login ---\n";
            string email, password;
            cout << "Email: "; cin >> email;
            cout << "Password: "; cin >> password;
            
            // Pointer usage: loginProfessor returns ProfessorData*
            auto* profData = db.loginProfessor(email, password);
            
            if (profData != nullptr) {
                // Create Professor object from data
                Professor* prof = new Professor();
                prof->setId(profData->id);
                prof->setName(profData->name);
                prof->setEmail(profData->email);
                prof->setPhone(profData->phone);
                prof->setSpecialization(profData->specialization);
                prof->setTitle(profData->title);
                
                cout << "Welcome, " << prof->getTitle() << " " << prof->getName() << "!\n";
                ProfessorMenu::show(db, prof);
                
                // Memory cleanup
                delete prof;
                delete profData;
            } else {
                cout << "Invalid email or password!\n";
            }
            

        } else if (choice == 0) {
            cout << "Goodbye!\n";
        }
        
    } while (choice != 0);
    
    return 0;
}

