/*
 * Professor Menu - قائمة الأستاذ
 * تتيح للأستاذ:
 * - عرض المواد المعينة له
 * - عرض قائمة الطلاب في كل مادة
 * - تسجيل الحضور والغياب
 * - إدخال وتعديل الدرجات
 */
#include "../include/ProfessorMenu.h"
#include "../include/ConsoleHelper.h"
#include <iostream>
#include <iomanip>

void ProfessorMenu::show(DatabaseManager& db, Professor* professor) {
    while (true) {
        std::cout << "\n============================================\n";
        std::cout << "           PROFESSOR PORTAL                 \n";
        std::cout << "============================================\n";
        std::cout << "  Welcome: " << professor->getTitle() << " " << professor->getName() << "\n";
        std::cout << "  Specialization: " << professor->getSpecialization() << "\n";
        std::cout << "============================================\n";
        std::cout << "  1. My Profile                             \n";
        std::cout << "  2. My Courses                             \n";
        std::cout << "  3. Student List (by Course)               \n";
        std::cout << "  4. Record Attendance                      \n";
        std::cout << "  5. Enter/Edit Grades                      \n";
        std::cout << "  6. View Attendance Report                 \n";
        std::cout << "  7. Academic Schedule                      \n";
        std::cout << "  8. Pearson Grading (P/M/D)                \n";
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
                professor->displayInfo();
                break;
                
            case 2: 
                db.listProfessorCourses(professor->getId()); 
                break;
                
            case 3: {
                std::cout << "\nEnter Course Code: ";
                std::string code;
                std::cin >> code;
                db.listStudentsInCourse(code);
                break;
            }
            
            case 4: {
                char addMore;
                do {
                    std::cout << "\n--- RECORD ATTENDANCE ---\n";
                    std::cout << "Enter Course Code: ";
                    std::string code;
                    std::cin >> code;
                    
                    std::cout << "Enter Date (YYYY-MM-DD): ";
                    std::string date;
                    std::cin >> date;
                    
                    db.recordAttendanceForCourse(code, date);
                    
                    std::cout << "\nEnter n to record for another course or 0 to exit: ";
                    if (!(std::cin >> addMore)) { std::cin.clear(); std::cin.ignore(10000, '\n'); break; }
                } while (addMore == 'n' || addMore == 'N');
                break;
            }
            
            case 5: {
                char addMore;
                do {
                    std::cout << "\n--- ENTER/EDIT GRADES ---\n";
                    db.listProfessorCourses(professor->getId());
                    
                    std::cout << "\nEnter Course Code: ";
                    std::string code;
                    std::cin >> code;
                    
                    std::cout << "Enter Student ID: ";
                    int studentId;
                    std::cin >> studentId;
                    
                    std::cout << "\nSelect Grade Type:\n";
                    std::cout << "1. S1 (Assignment 1 / Midterm 1)\n";
                    std::cout << "2. S2 (Assignment 2 / Midterm 2)\n";
                    std::cout << "3. Final Exam\n";
                    std::cout << "Choice: ";
                    int gradeType;
                    std::cin >> gradeType;
                    
                    std::cout << "Enter Score: ";
                    double score;
                    std::cin >> score;
                    
                    db.updateStudentGrade(studentId, code, gradeType, score);
                    
                    std::cout << "\nEnter n to enter another grade or 0 to exit: ";
                    if (!(std::cin >> addMore)) { std::cin.clear(); std::cin.ignore(10000, '\n'); break; }
                } while (addMore == 'n' || addMore == 'N');
                break;
            }
            
            case 6: {
                std::cout << "\n--- ATTENDANCE REPORT ---\n";
                std::cout << "Enter Course Code: ";
                std::string code;
                std::cin >> code;
                db.showAttendanceReport(code);
                break;
            }
            
            case 7:
                db.showProfessorSchedule(professor->getId());
                break;
            
            case 8: {
                // ===== PEARSON GRADING SYSTEM (Like GUI) =====
                std::cout << "\n======================================\n";
                std::cout << "     PEARSON GRADING SYSTEM (P/M/D)  \n";
                std::cout << "======================================\n";
                std::cout << "Each hour = 50 points\n";
                std::cout << "D = Distinction (100%)\n";
                std::cout << "M = Merit (80%+)\n";
                std::cout << "P = Pass (60%+)\n";
                std::cout << "F = Fail (<60%)\n";
                std::cout << "======================================\n\n";
                
                // Get professor courses as numbered list
                auto courses = db.getProfessorCourses(professor->getId());
                
                if (courses.empty()) {
                    std::cout << "No courses assigned to you.\n";
                    break;
                }
                
                std::cout << "Your Courses:\n";
                std::cout << "----------------------------------------\n";
                for (size_t i = 0; i < courses.size(); i++) {
                    std::cout << "  " << (i + 1) << ". " << courses[i].courseCode 
                              << " - " << courses[i].courseName 
                              << " (" << courses[i].studentCount << " students)\n";
                }
                std::cout << "----------------------------------------\n";
                
                std::cout << "\nSelect course number (1-" << courses.size() << "): ";
                int courseChoice;
                if (!(std::cin >> courseChoice) || courseChoice < 1 || courseChoice > (int)courses.size()) {
                    std::cout << "Error: Invalid choice!\n";
                    std::cin.clear();
                    std::cin.ignore(10000, '\n');
                    break;
                }
                
                std::string code = courses[courseChoice - 1].courseCode;
                std::cout << "\nSelected: " << code << " - " << courses[courseChoice - 1].courseName << "\n";
                
                std::cout << "Enter Credit Hours (2 or 3): ";
                int creditHours;
                if (!(std::cin >> creditHours) || (creditHours != 2 && creditHours != 3)) {
                    std::cout << "Error: Please enter 2 or 3 only!\n";
                    std::cin.clear();
                    std::cin.ignore(10000, '\n');
                    break;
                }
                int maxScore = creditHours * 50;
                std::cout << "Max Score: " << maxScore << " points\n\n";
                
                // Get all students in this course
                auto students = db.getStudentsInCourse(code);
                if (students.empty()) {
                    std::cout << "No students enrolled in this course.\n";
                    break;
                }
                
                // Structure to store grades for all students
                struct StudentGrade {
                    int id;
                    std::string name;
                    double a1, a2, final_exam, total, percent;
                    std::string pearson;
                };
                std::vector<StudentGrade> grades;
                
                std::cout << "======================================\n";
                std::cout << "  Enter grades for ALL " << students.size() << " students\n";
                std::cout << "======================================\n\n";
                
                // Enter grades for each student
                for (size_t i = 0; i < students.size(); i++) {
                    StudentGrade sg;
                    sg.id = students[i].studentId;
                    sg.name = students[i].name;
                    
                    std::cout << "[" << (i + 1) << "/" << students.size() << "] " << sg.name << "\n";
                    std::cout << "  A1: "; std::cin >> sg.a1;
                    std::cout << "  A2: "; std::cin >> sg.a2;
                    std::cout << "  Final: "; std::cin >> sg.final_exam;
                    
                    // Validate
                    if (sg.a1 < 0) sg.a1 = 0;
                    if (sg.a2 < 0) sg.a2 = 0;
                    if (sg.final_exam < 0) sg.final_exam = 0;
                    
                    sg.total = sg.a1 + sg.a2 + sg.final_exam;
                    sg.percent = (sg.total / maxScore) * 100;
                    if (sg.percent > 100) sg.percent = 100;
                    
                    if (sg.percent >= 100) sg.pearson = "D";
                    else if (sg.percent >= 80) sg.pearson = "M";
                    else if (sg.percent >= 60) sg.pearson = "P";
                    else sg.pearson = "F";
                    
                    grades.push_back(sg);
                    std::cout << "  -> Total: " << sg.total << " (" << sg.percent << "%) = " << sg.pearson << "\n\n";
                }
                
                // Show summary table
                std::cout << "\n==================== SUMMARY ====================\n";
                std::cout << std::left << std::setw(20) << "Student" 
                          << std::setw(6) << "A1" 
                          << std::setw(6) << "A2" 
                          << std::setw(8) << "Final"
                          << std::setw(8) << "Total"
                          << std::setw(8) << "%"
                          << std::setw(6) << "Grade" << "\n";
                std::cout << "-------------------------------------------------\n";
                
                for (const auto& g : grades) {
                    std::cout << std::left << std::setw(20) << g.name.substr(0, 18)
                              << std::setw(6) << g.a1
                              << std::setw(6) << g.a2
                              << std::setw(8) << g.final_exam
                              << std::setw(8) << g.total
                              << std::setw(8) << std::fixed << std::setprecision(1) << g.percent
                              << std::setw(6) << g.pearson << "\n";
                }
                std::cout << "=================================================\n\n";
                
                // Confirm save
                std::cout << "Save all grades? (y/n): ";
                char confirm;
                std::cin >> confirm;
                
                if (confirm == 'y' || confirm == 'Y') {
                    int saved = 0;
                    for (const auto& g : grades) {
                        db.updateStudentGrade(g.id, code, 1, g.a1);
                        db.updateStudentGrade(g.id, code, 2, g.a2);
                        db.updateStudentGrade(g.id, code, 3, g.final_exam);
                        saved++;
                    }
                    std::cout << "\n✅ Saved grades for " << saved << " students!\n";
                } else {
                    std::cout << "\n❌ Cancelled. No grades saved.\n";
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
