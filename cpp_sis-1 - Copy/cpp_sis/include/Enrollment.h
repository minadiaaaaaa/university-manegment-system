#ifndef ENROLLMENT_H
#define ENROLLMENT_H

#include <string>

class Enrollment {
public:
    int studentId;
    std::string courseCode;
    double grade; // e.g., 0-100

    Enrollment(int sid, std::string code, double g = -1.0) 
        : studentId(sid), courseCode(code), grade(g) {}

    std::string toString() const {
        return std::to_string(studentId) + "|" + courseCode + "|" + std::to_string(grade);
    }
};

#endif
