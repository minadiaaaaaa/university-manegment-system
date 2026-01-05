#ifndef SYSTEMMANAGER_H
#define SYSTEMMANAGER_H

#include "Database.h"
#include <iostream>

class SystemManager {
private:
    Database db;

public:
    SystemManager() : db("university.db") {
    }

    // --- Auth ---
    Student* loginStudent(std::string email, std::string password) {
        return db.authenticateStudent(email, password);
    }

    bool loginAdmin(std::string username, std::string password) {
        return db.authenticateAdmin(username, password);
    }

    // --- Students ---
    void addStudent(const Student& s) {
        db.addStudent(s);
        std::cout << "Student added to Database.\n";
    }

    void listStudents() {
        auto list = db.getAllStudents();
        std::cout << "\n--- Student List ---\n";
        for (const auto& s : list) {
            std::cout << "ID: " << s.id << " | Name: " << s.name << " | Email: " << s.email << "\n";
        }
        std::cout << "--------------------\n";
    }

    // --- Courses ---
    void addCourse(const Course& c) {
        db.addCourse(c);
        std::cout << "Course added to Database.\n";
    }

    void listCourses() {
        auto list = db.getAllCourses();
        std::cout << "\n--- Course List ---\n";
        for (const auto& c : list) {
            std::cout << "Code: " << c.code << " | Name: " << c.name << " | Credits: " << c.creditHours << "\n";
        }
        std::cout << "--------------------\n";
    }

    // --- Enrollments ---
    void enrollStudent(int studentId, std::string courseCode) {
        db.enrollStudent(studentId, courseCode);
        std::cout << "Enrollment processed.\n";
    }
    
    // Unused in this version: listEnrollments (admin view) - implementation omitted for brevity

    void listEnrollments() {
        std::cout << "Feature not fully ported to SQL details yet (requires complex joins).\n";
    }

    double calculateGPA(double score) {
        if (score >= 90) return 4.0;
        if (score >= 85) return 3.7;
        if (score >= 80) return 3.3;
        if (score >= 75) return 3.0;
        if (score >= 70) return 2.7;
        if (score >= 65) return 2.3;
        if (score >= 60) return 2.0;
        if (score >= 50) return 1.0;
        return 0.0;
    }

    void listStudentEnrollments(int studentId) {
        auto list = db.getStudentEnrollments(studentId);
        std::cout << "\n--- My Courses ---\n";
        
        bool found = false;
        double totalPoints = 0;
        int totalHours = 0; // Simplified credit hours assumption (all 3 for now, logic needed to fetch)

        for (const auto& e : list) {
            std::cout << "Course: " << e.courseCode 
                      << " | Total Score: " << (e.grade < 0 ? "N/A" : std::to_string(e.grade));
            
            if (e.grade >= 0) {
                 double gpa = calculateGPA(e.grade);
                 std::cout << " | GPA: " << gpa;
                 totalPoints += gpa * 3; // Assuming 3 credits
                 totalHours += 3;
            }
            std::cout << "\n";
            found = true;
        }

        if (found && totalHours > 0) {
            std::cout << "--- Est. GPA: " << (totalPoints / totalHours) << " ---\n";
        } else if (!found) {
            std::cout << "No enrollments found.\n";
        }
        std::cout << "------------------\n";
    }
};

#endif
