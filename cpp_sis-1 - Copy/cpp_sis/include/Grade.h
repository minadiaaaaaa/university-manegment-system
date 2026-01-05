/*
 * Grade Class - نظام الدرجات
 * يطابق schema.sql و GUI Models
 * 
 * شرح: هذا الكلاس يحسب الدرجات والـ GPA تلقائياً
 */
#ifndef GRADE_H
#define GRADE_H

#include <string>
#include <iostream>

class Grade {
private:
    int gradeId;
    int enrollmentId;
    int studentId;
    int courseId;
    std::string courseCode;
    std::string courseName;
    
    // درجات السعي والفاينال
    double s1;              // السعي الأول (Midterm 1)
    double s2;              // السعي الثاني (Midterm 2)
    double finalExam;       // الفاينال
    double total;           // المجموع الكلي
    std::string letterGrade;
    double gradePoints;     // نقاط GPA
    
    std::string academicYear;
    int semester;
    std::string status;     // in_progress / completed / failed

public:
    // ========== Constructor ==========
    Grade() 
        : gradeId(0), enrollmentId(0), studentId(0), courseId(0),
          s1(0.0), s2(0.0), finalExam(0.0), total(0.0), 
          gradePoints(0.0), semester(1), status("in_progress") {}

    // ========== Getters ==========
    int getGradeId() const { return gradeId; }
    int getEnrollmentId() const { return enrollmentId; }
    int getStudentId() const { return studentId; }
    int getCourseId() const { return courseId; }
    std::string getCourseCode() const { return courseCode; }
    std::string getCourseName() const { return courseName; }
    double getS1() const { return s1; }
    double getS2() const { return s2; }
    double getFinalExam() const { return finalExam; }
    double getTotal() const { return total; }
    std::string getLetterGrade() const { return letterGrade; }
    double getGradePoints() const { return gradePoints; }
    std::string getAcademicYear() const { return academicYear; }
    int getSemester() const { return semester; }
    std::string getStatus() const { return status; }

    // ========== Setters ==========
    void setGradeId(int val) { gradeId = val; }
    void setEnrollmentId(int val) { enrollmentId = val; }
    void setStudentId(int val) { studentId = val; }
    void setCourseId(int val) { courseId = val; }
    void setCourseCode(const std::string& val) { courseCode = val; }
    void setCourseName(const std::string& val) { courseName = val; }
    void setS1(double val) { s1 = val; }
    void setS2(double val) { s2 = val; }
    void setFinalExam(double val) { finalExam = val; }
    void setAcademicYear(const std::string& val) { academicYear = val; }
    void setSemester(int val) { semester = val; }
    void setStatus(const std::string& val) { status = val; }

    // ========== حساب الدرجات ==========
    
    // حساب المجموع الكلي
    void calculateTotal() {
        total = s1 + s2 + finalExam;
        calculateLetterGrade();
        calculateGradePoints();
    }

    // تحديد التقدير (Letter Grade)
    void calculateLetterGrade() {
        if (total >= 90) letterGrade = "A+";
        else if (total >= 85) letterGrade = "A";
        else if (total >= 80) letterGrade = "B+";
        else if (total >= 75) letterGrade = "B";
        else if (total >= 70) letterGrade = "C+";
        else if (total >= 65) letterGrade = "C";
        else if (total >= 60) letterGrade = "D+";
        else if (total >= 50) letterGrade = "D";
        else letterGrade = "F";
        
        // تحديث الحالة
        if (total >= 50) status = "completed";
        else status = "failed";
    }

    // حساب نقاط GPA
    void calculateGradePoints() {
        if (letterGrade == "A+") gradePoints = 4.0;
        else if (letterGrade == "A") gradePoints = 3.7;
        else if (letterGrade == "B+") gradePoints = 3.3;
        else if (letterGrade == "B") gradePoints = 3.0;
        else if (letterGrade == "C+") gradePoints = 2.7;
        else if (letterGrade == "C") gradePoints = 2.3;
        else if (letterGrade == "D+") gradePoints = 2.0;
        else if (letterGrade == "D") gradePoints = 1.0;
        else gradePoints = 0.0;
    }

    // ========== Display ==========
    void displayInfo() const {
        std::cout << "├─ " << courseCode << " | " << courseName << "\n";
        std::cout << "│  S1: " << s1 << " | S2: " << s2 << " | Final: " << finalExam << "\n";
        std::cout << "│  Total: " << total << " | Grade: " << letterGrade 
                  << " | GPA: " << gradePoints << "\n";
    }

    // للتصدير
    std::string toString() const {
        return std::to_string(studentId) + "|" + courseCode + "|" + 
               std::to_string(s1) + "|" + std::to_string(s2) + "|" + 
               std::to_string(finalExam) + "|" + std::to_string(total) + "|" + letterGrade;
    }
};

#endif
