/*
 * Student Menu - قائمة الطالب
 * تعرض خيارات للطالب بعد تسجيل الدخول
 */
#include "../include/StudentMenu.h"
#include "../include/ConsoleHelper.h"
#include <iostream>
#include <iomanip>

void StudentMenu::show(DatabaseManager& db, Student* student) {
    while (true) {
        std::cout << "\n============================================\n";
        std::cout << "              STUDENT PORTAL                \n";
        std::cout << "============================================\n";
        std::cout << "  Welcome: " << student->getName() << "\n";
        std::cout << "  Level: " << student->getLevel() << " | Term: " << student->getSemester() 
                  << " | Track: " << student->getTrack() << "\n";
        std::cout << "============================================\n";
        std::cout << "  1. My Profile                             \n";
        std::cout << "  2. My Courses                             \n";
        std::cout << "  3. My Grades                              \n";
        std::cout << "  4. My Section Info                        \n";
        std::cout << "  5. My Payments                            \n";
        std::cout << "  6. Available Courses                      \n";
        std::cout << "  7. Register for Course                    \n";
        std::cout << "  8. All Courses List                       \n";
        std::cout << "  0. Logout                                 \n";
        std::cout << "============================================\n";
        std::cout << "Choice: ";
        
        int choice;
        if (!(std::cin >> choice)) { 
            ConsoleHelper::clearInput(); 
            continue; 
        }
        if (choice == 0) break;

        switch (choice) {
            case 1: 
                student->displayInfo();
                break;
                
            case 2: 
                db.listStudentEnrollments(student->getId()); 
                break;
                
            case 3:
                db.showStudentGrades(student->getId());
                break;
                
            case 4:
                db.showStudentSection(student->getId());
                break;
                
            case 5:
                db.showStudentPayments(student->getId());
                break;
                
            case 6:
                db.listAvailableCourses(student->getLevel(), student->getSemester(), student->getTrack());
                break;
                
            case 7: {
                char addMore;
                do {
                    std::cout << "\n--- Course Registration ---\n";
                    db.listAvailableCourses(student->getLevel(), student->getSemester(), student->getTrack());
                    std::cout << "Enter Course Code (or 0 to cancel): ";
                    std::string code;
                    std::cin >> code;
                    if (code != "0") {
                        db.enrollStudent(student->getId(), code);
                    }
                    
                    std::cout << "\nRegister for another course? (n to stop): ";
                    std::cin >> addMore;
                } while (addMore != 'n' && addMore != 'N');
                break;
            }
            
            case 8: 
                db.listCourses(); 
                break;
                
            default:
                std::cout << "Invalid choice.\n";
        }
        
        std::cout << "\nPress Enter to continue...";
        std::cin.ignore();
        std::cin.get();
    }
}

