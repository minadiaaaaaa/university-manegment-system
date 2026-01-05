# 🎓 SIS University System - ERD
## Entity Relationship Diagram - نظام إدارة الجامعة

```mermaid
erDiagram
    %% ============================
    %% STUDENT
    %% ============================
    STUDENT {
        int Student_ID PK
        string Name
        string National_ID
        string Email
        string Phone
        string Gender
        date DOB
        string Address
        int Year
        int Term
        string Track
        int Section_ID FK
        string Seat_Number
        string Status
        string Portal_Username
        string Portal_Password
    }

    %% ============================
    %% SECTION
    %% ============================
    SECTION {
        int Section_ID PK
        int Year
        int Term
        string Track
        int Section_Number
    }

    SECTION ||--o{ STUDENT : "has_students"

    %% ============================
    %% COURSE
    %% ============================
    COURSE {
        int Course_ID PK
        string Course_Name
        string Course_Code
        int Year
        int Term
        string Track
        int Credit_Hours
    }

    %% ============================
    %% INSTRUCTOR
    %% ============================
    INSTRUCTOR {
        int Instructor_ID PK
        string Name
        string Email
        string Phone
        string Specialization
        string Title
    }

    %% ============================
    %% ENROLLMENT
    %% ============================
    ENROLLMENT {
        int Enrollment_ID PK
        int Student_ID FK
        int Course_ID FK
        string Academic_Year
        int Semester
        string Status
    }

    STUDENT ||--o{ ENROLLMENT : "enrolls"
    COURSE ||--o{ ENROLLMENT : "enrollment_of"

    %% ============================
    %% GRADE
    %% ============================
    GRADE {
        int Grade_ID PK
        int Enrollment_ID FK
        float Assignment1 "20%"
        float Assignment2 "30%"
        float CourseWork "20%"
        float Final "30%"
        float Total
        string Letter_Grade "Excellent/Very Good/Good/Pass/Fail"
        float GPA_Points
    }

    ENROLLMENT ||--|| GRADE : "grade_for"

    %% ============================
    %% COURSE OFFERING
    %% ============================
    COURSE_OFFERING {
        int Offering_ID PK
        int Course_ID FK
        int Instructor_ID FK
        int Section_ID FK
        string Academic_Year
        int Semester
    }

    COURSE ||--o{ COURSE_OFFERING : "offered_as"
    INSTRUCTOR ||--o{ COURSE_OFFERING : "teaches"
    SECTION ||--o{ COURSE_OFFERING : "assigned_to"

    %% ============================
    %% SCHEDULE
    %% ============================
    SCHEDULE {
        int Schedule_ID PK
        int Offering_ID FK
        string Day
        string Time
        int Room_ID FK
    }

    COURSE_OFFERING ||--o{ SCHEDULE : "has_schedule"

    %% ============================
    %% CLASSROOM
    %% ============================
    CLASSROOM {
        int Room_ID PK
        string Room_Code
        int Capacity
        string Building
        string Room_Type
    }

    CLASSROOM ||--o{ SCHEDULE : "located_in"

    %% ============================
    %% EXAM
    %% ============================
    EXAM {
        int Exam_ID PK
        int Course_ID FK
        string Exam_Type
        date Exam_Date
        string Exam_Time
        int Room_ID FK
    }

    COURSE ||--o{ EXAM : "has_exams"
    CLASSROOM ||--o{ EXAM : "exam_in"

    %% ============================
    %% ATTENDANCE
    %% ============================
    ATTENDANCE {
        int Attendance_ID PK
        int Student_ID FK
        int Course_ID FK
        date Date
        string Status
    }

    STUDENT ||--o{ ATTENDANCE : "has_attendance"
    COURSE ||--o{ ATTENDANCE : "tracks_attendance"

    %% ============================
    %% PAYMENT
    %% ============================
    PAYMENT {
        int Payment_ID PK
        int Student_ID FK
        float Amount
        string Method
        date Date
        string Status
    }

    STUDENT ||--o{ PAYMENT : "makes"

    %% ============================
    %% STUDENT_PORTAL
    %% ============================
    STUDENT_PORTAL {
        int Portal_ID PK
        int Student_ID FK
        datetime Last_Login
        string Notifications
    }

    STUDENT ||--|| STUDENT_PORTAL : "has_portal"

    %% ============================
    %% NEWS (Standalone)
    %% ============================
    NEWS {
        int News_ID PK
        string Title
        string Description
        date Date
        string Posted_By
        string Category
    }

    %% ============================
    %% ADMIN
    %% ============================
    ADMIN {
        int Admin_ID PK
        string Username
        string Password
        string Full_Name
        string Email
        string Role
    }
```

---

## 🔗 Relationships Summary

| From | To | Relationship | Description |
|------|-----|--------------|-------------|
| Section | Student | 1:N | Section contains many students |
| Student | Enrollment | 1:N | Student enrolls in many courses |
| Course | Enrollment | 1:N | Course has many enrollments |
| Enrollment | Grade | 1:1 | Each enrollment has one grade record |
| Course | Course_Offering | 1:N | Course offered multiple times |
| Instructor | Course_Offering | 1:N | Instructor teaches many offerings |
| Section | Course_Offering | 1:N | Section has many offerings |
| Course_Offering | Schedule | 1:N | Offering has multiple schedule slots |
| Classroom | Schedule | 1:N | Classroom used in many schedules |
| Course | Exam | 1:N | Course has multiple exams (S1, S2, Final) |
| Student | Attendance | 1:N | Student has attendance records |
| Course | Attendance | 1:N | Course tracks attendance |
| Student | Payment | 1:N | Student makes payments |
| Student | Student_Portal | 1:1 | Student has one portal |
| News | - | Standalone | News is independent |

---

## 📚 Course Structure by Year & Track

### 🔵 Year 1 (General Track)
| Term 1 | Term 2 |
|--------|--------|
| Physics I | MS Office |
| IT Essentials | Introduction to IoT |
| Python Programming | Mathematics II |
| Intro to Cyber Security | Cyber Security Essentials |
| Mathematics I | Programming Essentials in C |
| English I | Technical English II |

### 🟧 Year 2 (General Track)
| Term 1 | Term 2 |
|--------|--------|
| Linux | Java I |
| Intro to Database | CCNA I |
| Web Programming I | Data Structure |
| Programming in C++ | Database Programming |
| Digital Electronics | Web Programming II |
| Operating Systems | CCNA R&S I |

### 🟩 Year 3 - Software Track
| Term 1 | Term 2 |
|--------|--------|
| Microprocessor | Mobile Programming I |
| Computer Graphics | Software Engineering |
| Advanced Programming in C | Network Programming |
| Data Communication | Algorithm |
| Computer Architecture | Advanced Programming in C++ |
| Java Programming II | Embedded System |

### 🟫 Year 3 - Network Track
| Term 1 | Term 2 |
|--------|--------|
| CCNA II | CCNA R&S III |
| Microprocessor | Software Engineering |
| Java Programming II | Distributed System |
| Data Communication | Network Programming |
| Computer Architecture | Embedded System |
| Network Administration | Distributed Systems II |

### 🟥 Year 4 - Software Track
| Term 1 | Term 2 |
|--------|--------|
| Mobile Programming II | IoT Security |
| CCNA II | Robotics |
| Windows Programming I | Windows Programming II |
| Artificial Intelligence | Machine Learning |
| IoT Architecture | Big Data & Analytics |
| Signal Processing | Entrepreneurship |

### 🟪 Year 4 - Network Track
| Term 1 | Term 2 |
|--------|--------|
| Server Administration | IoT Security |
| CCNA R&S IV | Machine Learning |
| Cyber Security Operations | CCNP Switch |
| Encryption Algorithm | CCNP Route |
| Artificial Intelligence | Big Data & Analytics |
| IoT Architecture | Entrepreneurship |

---

## ✅ Entity Summary

| Entity | Primary Key | Description |
|--------|-------------|-------------|
| **STUDENT** | Student_ID | Student information with Track (SW/Network/General) |
| **COURSE** | Course_ID | All courses for 4 years |
| **ENROLLMENT** | Enrollment_ID | Student course registration |
| **GRADE** | Grade_ID | Ass1(20%), Ass2(30%), CW(20%), Final(30%) |
| **EXAM** | Exam_ID | Exam schedule (S1/S2/Final) |
| **INSTRUCTOR** | Instructor_ID | Instructor information |
| **COURSE_OFFERING** | Offering_ID | Course + Instructor + Section |
| **SECTION** | Section_ID | Section info (Year, Term, Track) |
| **SCHEDULE** | Schedule_ID | Class schedule |
| **CLASSROOM** | Room_ID | Room information (Hall/Lab) |
| **ATTENDANCE** | Attendance_ID | Student attendance tracking |
| **PAYMENT** | Payment_ID | Student payments |
| **STUDENT_PORTAL** | Portal_ID | Portal login info |
| **NEWS** | News_ID | News (Standalone) |
| **ADMIN** | Admin_ID | System administrators |

---

## 📊 Grade Distribution

### Practical Courses (100/150 marks)
| Component | Percentage |
|-----------|------------|
| Assignment 1 | 20% |
| Assignment 2 | 30% |
| Course Work (CW) | 20% |
| Final Exam | 30% |

### Theoretical Courses (100 marks)
| Component | Percentage |
|-----------|------------|
| Assignment 1 | 20% |
| Assignment 2 | 20% |
| Final Exam | 60% |

### Grade Evaluation
| Percentage | Letter Grade |
|------------|--------------|
| ≥ 85% | Excellent |
| ≥ 75% | Very Good |
| ≥ 65% | Good |
| ≥ 60% | Pass |
| < 60% | Fail |
