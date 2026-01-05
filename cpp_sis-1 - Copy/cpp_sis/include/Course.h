/*
 * Course Class - نظام إدارة المواد الدراسية
 * يطابق schema.sql تماماً
 */
#ifndef COURSE_H
#define COURSE_H

#include <string>
#include <iostream>

class Course {
private:
    int courseId;
    std::string code;
    std::string name;
    std::string nameAr;
    int year;                     // السنة الدراسية (1-4)
    int term;                     // الترم (1 أو 2)
    std::string track;            // المسار: SW / Network / General
    int creditHours;
    std::string description;
    std::string instructor;
    int maxStudents;
    int currentEnrolled;
    double fees;
    bool isActive;

public:
    // ========== Constructors ==========
    Course() 
        : courseId(0), year(1), term(1), creditHours(3), 
          maxStudents(50), currentEnrolled(0), fees(0.0), 
          isActive(true), track("General") {}

    Course(const std::string& code, const std::string& name, int credits, 
           const std::string& instructor)
        : courseId(0), code(code), name(name), creditHours(credits), 
          instructor(instructor), year(1), term(1), maxStudents(50), 
          currentEnrolled(0), fees(0.0), isActive(true), track("General") {}

    // Full Constructor
    Course(int id, const std::string& code, const std::string& name,
           int year, int term, const std::string& track, int credits)
        : courseId(id), code(code), name(name), year(year), term(term),
          track(track), creditHours(credits), maxStudents(50),
          currentEnrolled(0), fees(0.0), isActive(true) {}

    // ========== Getters ==========
    int getCourseId() const { return courseId; }
    std::string getCode() const { return code; }
    std::string getName() const { return name; }
    std::string getNameAr() const { return nameAr; }
    int getYear() const { return year; }
    int getTerm() const { return term; }
    std::string getTrack() const { return track; }
    int getCreditHours() const { return creditHours; }
    std::string getDescription() const { return description; }
    std::string getInstructor() const { return instructor; }
    int getMaxStudents() const { return maxStudents; }
    int getCurrentEnrolled() const { return currentEnrolled; }
    double getFees() const { return fees; }
    bool getIsActive() const { return isActive; }

    // ========== Setters ==========
    void setCourseId(int val) { courseId = val; }
    void setCode(const std::string& val) { code = val; }
    void setName(const std::string& val) { name = val; }
    void setNameAr(const std::string& val) { nameAr = val; }
    void setYear(int val) { year = val; }
    void setTerm(int val) { term = val; }
    void setTrack(const std::string& val) { track = val; }
    void setCreditHours(int val) { creditHours = val; }
    void setDescription(const std::string& val) { description = val; }
    void setInstructor(const std::string& val) { instructor = val; }
    void setMaxStudents(int val) { maxStudents = val; }
    void setCurrentEnrolled(int val) { currentEnrolled = val; }
    void setFees(double val) { fees = val; }
    void setIsActive(bool val) { isActive = val; }

    // ========== Methods ==========
    void displayInfo() const {
        std::cout << "├─ " << code << " | " << name 
                  << " | Year " << year << " Term " << term 
                  << " | " << track << " | " << creditHours << " hrs\n";
    }

    std::string toString() const {
        return code + "|" + name + "|" + std::to_string(creditHours) + "|" + 
               instructor + "|" + std::to_string(year) + "|" + std::to_string(term) + 
               "|" + track;
    }

    friend std::ostream& operator<<(std::ostream& os, const Course& c) {
        os << c.code << " - " << c.name;
        return os;
    }
};

#endif
