/*
 * Professor Class - كلاس الأستاذ/المحاضر
 * يحتوي على بيانات المحاضر الشخصية
 */
#ifndef PROFESSOR_H
#define PROFESSOR_H

#include <string>
#include <iostream>

class Professor {
private:
    int id;
    std::string name;
    std::string email;
    std::string password;
    std::string phone;
    std::string specialization;
    std::string title;  // Dr., Prof., Eng.
    bool isActive;

public:
    Professor() : id(0), isActive(true) {}
    
    Professor(int id, const std::string& name, const std::string& email, 
              const std::string& specialization = "", const std::string& title = "")
        : id(id), name(name), email(email), specialization(specialization), 
          title(title), isActive(true) {}

    // Getters
    int getId() const { return id; }
    std::string getName() const { return name; }
    std::string getEmail() const { return email; }
    std::string getPassword() const { return password; }
    std::string getPhone() const { return phone; }
    std::string getSpecialization() const { return specialization; }
    std::string getTitle() const { return title; }
    bool getIsActive() const { return isActive; }

    // Setters
    void setId(int val) { id = val; }
    void setName(const std::string& val) { name = val; }
    void setEmail(const std::string& val) { email = val; }
    void setPassword(const std::string& val) { password = val; }
    void setPhone(const std::string& val) { phone = val; }
    void setSpecialization(const std::string& val) { specialization = val; }
    void setTitle(const std::string& val) { title = val; }
    void setIsActive(bool val) { isActive = val; }

    // Display info
    void displayInfo() const {
        std::cout << "\n============================================\n";
        std::cout << "           PROFESSOR PROFILE                \n";
        std::cout << "============================================\n";
        std::cout << "  ID: " << id << "\n";
        std::cout << "  Name: " << title << " " << name << "\n";
        std::cout << "  Email: " << email << "\n";
        std::cout << "  Phone: " << phone << "\n";
        std::cout << "  Specialization: " << specialization << "\n";
        std::cout << "  Status: " << (isActive ? "Active" : "Inactive") << "\n";
        std::cout << "============================================\n";
    }
};

#endif
