




/*
 * Admin Menu - لوحة تحكم الأدمن
 * تتيح إدارة الطلاب والمواد والكليات والأقسام والقاعات
 */
#include "../include/AdminMenu.h"
#include "../include/ConsoleHelper.h"
#include <iostream>
#include <iomanip>

void AdminMenu::show(DatabaseManager& db) {
    while (true) {
        std::cout << "\n============================================\n";
        std::cout << "              ADMIN PANEL                   \n";
        std::cout << "============================================\n";
        std::cout << "  --- Students & Courses ---                \n";
        std::cout << "  1. List All Students                      \n";
        std::cout << "  2. Add New Student                        \n";
        std::cout << "  3. List All Courses                       \n";
        std::cout << "  4. Add New Course                         \n";
        std::cout << "  --- Faculties & Departments ---           \n";
        std::cout << "  5. List Faculties                         \n";
        std::cout << "  6. List Departments                       \n";
        std::cout << "  --- Instructors ---                       \n";
        std::cout << "  7. List Instructors                       \n";
        std::cout << "  8. Add New Instructor                     \n";
        std::cout << "  9. Assign Course to Instructor            \n";
        std::cout << "  --- Classrooms & Labs ---                 \n";
        std::cout << "  10. List Classrooms & Labs                \n";
        std::cout << "  11. Add New Classroom/Lab                 \n";
        std::cout << "  --- Attendance & Grades ---               \n";
        std::cout << "  12. View Attendance Report                \n";
        std::cout << "  13. Mercy Grading (Pearson P/M/D)         \n";
        std::cout << "  --- News ---                              \n";
        std::cout << "  14. Manage News                           \n";
        std::cout << "  0. Logout                                 \n";
        std::cout << "============================================\n";
        std::cout << "Choice: ";
        
        int choice;
        if (!(std::cin >> choice)) { ConsoleHelper::clearInput(); continue; }
        if (choice == 0) break;

        switch (choice) {
            case 1: 
                db.listStudents(); 
                break;
                
            case 2: {
                char addMore;
                do {
                    std::cout << "\n--- ADD NEW STUDENT ---\n";
                    
                    std::string name, email, pass, track;
                    int level, semester;
                    
                    std::cout << "Name: "; 
                    std::cin >> std::ws; 
                    std::getline(std::cin, name);
                    
                    std::cout << "Email: "; 
                    std::cin >> email;
                    
                    std::cout << "Password: "; 
                    std::cin >> pass;
                    
                    std::cout << "Level (1-4): "; 
                    std::cin >> level;
                    
                    std::cout << "Semester (1-2): ";
                    std::cin >> semester;
                    
                    std::cout << "Track (General/SW/Network): ";
                    std::cin >> track;
                    
                    Student newStudent;
                    newStudent.setName(name);
                    newStudent.setEmail(email);
                    newStudent.setPassword(pass);
                    newStudent.setLevel(level);
                    newStudent.setSemester(semester);
                    newStudent.setTrack(track);
                    newStudent.setDepartmentId(1);
                    
                    db.addStudent(newStudent);
                    
                    
                    std::cout << "\nEnter n to add new student or 0 to exit: ";
                    if (!(std::cin >> addMore)) { std::cin.clear(); std::cin.ignore(10000, '\n'); break; }
                } while (addMore == 'n' || addMore == 'N');
                break;
            }
            
            case 3: 
                db.listCourses(); 
                break;
                
            case 4: {
                char addMore;
                do {
                    std::cout << "\n--- ADD NEW COURSE ---\n";
                    
                    std::string code, name, track;
                    int year, term, credits;
                    
                    std::cout << "Course Code: "; 
                    std::cin >> code;
                    
                    std::cout << "Course Name: "; 
                    std::cin >> std::ws; 
                    std::getline(std::cin, name);
                    
                    std::cout << "Year (1-4): ";
                    std::cin >> year;
                    
                    std::cout << "Term (1-2): ";
                    std::cin >> term;
                    
                    std::cout << "Track (General/SW/Network): ";
                    std::cin >> track;
                    
                    std::cout << "Credit Hours: ";
                    std::cin >> credits;
                    
                    Course newCourse(0, code, name, year, term, track, credits);
                    db.addCourse(newCourse);
                    
                    
                    std::cout << "\nEnter n to add new course or 0 to exit: ";
                    if (!(std::cin >> addMore)) { std::cin.clear(); std::cin.ignore(10000, '\n'); break; }
                } while (addMore == 'n' || addMore == 'N');
                break;
            }
            
            case 5:
                db.listFaculties();
                break;
                
            case 6:
                db.listDepartments();
                break;
                
            case 7:
                db.listInstructors();
                break;
                
            case 8: {
                char addMore;
                do {
                    std::cout << "\n--- ADD NEW INSTRUCTOR ---\n";
                    
                    std::string name, email, phone, spec, title;
                    
                    std::cout << "Name: "; 
                    std::cin >> std::ws; 
                    std::getline(std::cin, name);
                    
                    std::cout << "Email: "; 
                    std::cin >> email;
                    
                    std::cout << "Phone: "; 
                    std::cin >> phone;
                    
                    std::cout << "Specialization: "; 
                    std::cin >> std::ws; 
                    std::getline(std::cin, spec);
                    
                    std::cout << "Title (Dr./Prof./Eng.): "; 
                    std::cin >> title;
                    
                    db.addNewInstructor(name, email, phone, spec, title);
                    
                    
                    std::cout << "\nEnter n to add new instructor or 0 to exit: ";
                    if (!(std::cin >> addMore)) { std::cin.clear(); std::cin.ignore(10000, '\n'); break; }
                } while (addMore == 'n' || addMore == 'N');
                break;
            }
            
            // ========== 9. ASSIGN COURSE TO INSTRUCTOR (من GUI) ==========
            case 9: {
                char addMore;
                do {
                    std::cout << "\n--- ASSIGN COURSE TO INSTRUCTOR ---\n\n";
                    
                    // Show instructors
                    db.listInstructors();
                    std::cout << "\nEnter Instructor ID: ";
                    int instructorId;
                    std::cin >> instructorId;
                    
                    // Show courses
                    db.listCourses();
                    std::cout << "\nEnter Course Code: ";
                    std::string courseCode;
                    std::cin >> courseCode;
                    
                    db.assignCourseToInstructor(instructorId, courseCode);
                    
                    
                    std::cout << "\nEnter n to assign new course or 0 to exit: ";
                    if (!(std::cin >> addMore)) { std::cin.clear(); std::cin.ignore(10000, '\n'); break; }
                } while (addMore == 'n' || addMore == 'N');
                break;
            }
            
            case 10:
                db.listClassrooms();
                break;
                
            case 11: {
                char addMore;
                do {
                    std::cout << "\n--- ADD NEW CLASSROOM/LAB ---\n";
                    
                    std::string code, building, type;
                    int capacity;
                    
                    std::cout << "Room Code: "; 
                    std::cin >> code;
                    
                    std::cout << "Capacity: "; 
                    std::cin >> capacity;
                    
                    std::cout << "Building: "; 
                    std::cin >> std::ws; 
                    std::getline(std::cin, building);
                    
                    std::cout << "Type (Hall/Lab/Lecture Room): "; 
                    std::cin >> type;
                    
                    db.addNewClassroom(code, capacity, building, type);
                    
                    
                    std::cout << "\nEnter n to add new classroom or 0 to exit: ";
                    if (!(std::cin >> addMore)) { std::cin.clear(); std::cin.ignore(10000, '\n'); break; }
                } while (addMore == 'n' || addMore == 'N');
                break;
            }
            
            // ========== 12. ATTENDANCE REPORT (من GUI) ==========
            case 12: {
                std::cout << "\n--- ATTENDANCE REPORT ---\n";
                db.listCourses();
                std::cout << "\nEnter Course Code: ";
                std::string courseCode;
                std::cin >> courseCode;
                db.showAttendanceReport(courseCode);
                break;
            }
            
            // ========== 13. MERCY GRADING - PEARSON (من GUI) ==========
            case 13: {
                std::cout << "\n======================================\n";
                std::cout << "     MERCY GRADING (P/M/D)           \n";
                std::cout << "======================================\n";
                std::cout << "Pass = 60% (P grade)\n";
                std::cout << "Mercy applies to students 55-59%\n";
                std::cout << "Converts F -> P (Fail to Pass)\n";
                std::cout << "======================================\n\n";
                
                db.listCourses();
                std::cout << "\nEnter Course Code: ";
                std::string courseCode;
                std::cin >> courseCode;
                
                std::cout << "Enter Mercy Range % (e.g. 5): ";
                double mercyRange;
                std::cin >> mercyRange;
                
                db.applyMercyGrades(courseCode, mercyRange);
                break;
            }
            
            // ========== 14. NEWS MANAGEMENT (من GUI) ==========
            case 14: {
                std::cout << "\n--- NEWS MANAGEMENT ---\n";
                std::cout << "1. View All News\n";
                std::cout << "2. Add New Announcement\n";
                std::cout << "Choice: ";
                
                int newsChoice;
                std::cin >> newsChoice;
                
                if (newsChoice == 1) {
                    db.listNews();
                } else if (newsChoice == 2) {
                    char addMore;
                    do {
                        std::cout << "\nTitle: ";
                        std::string title;
                        std::cin >> std::ws;
                        std::getline(std::cin, title);
                        
                        std::cout << "Content: ";
                        std::string content;
                        std::getline(std::cin, content);
                        
                        db.addNews(title, content);
                        std::cout << "News added successfully!\n";
                        
                        
                        std::cout << "\nEnter n to add new announcement or 0 to exit: ";
                        if (!(std::cin >> addMore)) { std::cin.clear(); std::cin.ignore(10000, '\n'); break; }
                    } while (addMore == 'n' || addMore == 'N');
                }
                break;
            }
            
            default:
                std::cout << "Invalid choice.\n";
        }
        
        std::cout << "\nPress Enter to continue...";
        std::cin.ignore();
        std::cin.get();
    }
}

