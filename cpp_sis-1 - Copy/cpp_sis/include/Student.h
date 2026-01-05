/*
 * Student Class - نظام إدارة الطلاب
 * يوضح استخدام Pointers و OOP في C++
 * 
 * شرح OOP:
 * - Encapsulation: البيانات private والوصول عبر getters/setters
 * - يمكن استخدام Pointer للـ Student: Student* s = new Student();
 */
#ifndef STUDENT_H
#define STUDENT_H

#include <string>
#include <iostream>

class Student {
private:
    // ========== Private Members (Encapsulation) ==========
    int id;
    std::string name;
    std::string nameAr;           // الاسم بالعربي
    std::string email;
    std::string password;
    std::string nationalId;       // الرقم القومي
    int departmentId;
    std::string department;       // اسم القسم
    std::string facultyName;      // اسم الكلية
    int level;                    // السنة (1-4)
    int semester;                 // الترم (1 أو 2)
    std::string track;            // المسار: SW / Network / General
    int seatNumber;               // رقم الجلوس
    std::string enrollmentYear;   // سنة الالتحاق
    std::string phone;
    std::string address;
    double balance;               // رصيد الطالب
    double gpa;
    std::string status;           // active / graduated / suspended

public:
    // ========== Constructors ==========
    
    // Default Constructor
    Student() 
        : id(0), departmentId(1), level(1), semester(1), 
          seatNumber(0), balance(0.0), gpa(0.0), 
          track("General"), status("active") {}

    // Constructor with basic info
    Student(int id, const std::string& name, const std::string& email, 
            const std::string& password, const std::string& dept)
        : id(id), name(name), email(email), password(password), 
          department(dept), departmentId(1), level(1), semester(1), 
          seatNumber(0), balance(0.0), gpa(0.0), 
          track("General"), status("active") {}

    // Full Constructor (مثل GUI)
    Student(int id, const std::string& name, const std::string& email,
            const std::string& password, int deptId, int level, int semester,
            const std::string& track, int seatNum)
        : id(id), name(name), email(email), password(password),
          departmentId(deptId), level(level), semester(semester),
          track(track), seatNumber(seatNum), balance(0.0), gpa(0.0),
          status("active") {}

    // ========== Getters (للقراءة فقط) ==========
    int getId() const { return id; }
    std::string getName() const { return name; }
    std::string getNameAr() const { return nameAr; }
    std::string getEmail() const { return email; }
    std::string getPassword() const { return password; }
    std::string getNationalId() const { return nationalId; }
    int getDepartmentId() const { return departmentId; }
    std::string getDepartment() const { return department; }
    std::string getFacultyName() const { return facultyName; }
    int getLevel() const { return level; }
    int getSemester() const { return semester; }
    std::string getTrack() const { return track; }
    int getSeatNumber() const { return seatNumber; }
    std::string getEnrollmentYear() const { return enrollmentYear; }
    std::string getPhone() const { return phone; }
    std::string getAddress() const { return address; }
    double getBalance() const { return balance; }
    double getGpa() const { return gpa; }
    std::string getStatus() const { return status; }

    // ========== Setters (للتعديل) ==========
    void setId(int val) { id = val; }
    void setName(const std::string& val) { name = val; }
    void setNameAr(const std::string& val) { nameAr = val; }
    void setEmail(const std::string& val) { email = val; }
    void setPassword(const std::string& val) { password = val; }
    void setNationalId(const std::string& val) { nationalId = val; }
    void setDepartmentId(int val) { departmentId = val; }
    void setDepartment(const std::string& val) { department = val; }
    void setFacultyName(const std::string& val) { facultyName = val; }
    void setLevel(int val) { level = val; }
    void setSemester(int val) { semester = val; }
    void setTrack(const std::string& val) { track = val; }
    void setSeatNumber(int val) { seatNumber = val; }
    void setEnrollmentYear(const std::string& val) { enrollmentYear = val; }
    void setPhone(const std::string& val) { phone = val; }
    void setAddress(const std::string& val) { address = val; }
    void setBalance(double val) { balance = val; }
    void setGpa(double val) { gpa = val; }
    void setStatus(const std::string& val) { status = val; }

    // ========== Methods ==========
    
    // عرض معلومات الطالب
    void displayInfo() const {
        std::cout << "\n============================================\n";
        std::cout << "              STUDENT PROFILE               \n";
        std::cout << "============================================\n";
        std::cout << "  ID: " << id << "\n";
        std::cout << "  Name: " << name << "\n";
        std::cout << "  Email: " << email << "\n";
        std::cout << "  Department: " << department << "\n";
        std::cout << "  Level: " << level << " | Semester: " << semester << "\n";
        std::cout << "  Track: " << track << "\n";
        std::cout << "  Seat Number: " << seatNumber << "\n";
        std::cout << "  GPA: " << gpa << "\n";
        std::cout << "============================================\n";
    }

    // Serialization للحفظ
    std::string toString() const {
        return std::to_string(id) + "|" + name + "|" + email + "|" + password + "|" + 
               department + "|" + std::to_string(level) + "|" + std::to_string(semester) + 
               "|" + track + "|" + std::to_string(gpa);
    }

    // Overload << operator للطباعة
    friend std::ostream& operator<<(std::ostream& os, const Student& s) {
        os << s.name << " (" << s.email << ")";
        return os;
    }
};

#endif
