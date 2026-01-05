/*
 * نظام إدارة الجامعة - الموديلات (الكلاسات الأساسية)
 * تم عمله بواسطة: Mina Diaa
 * التاريخ: ديسمبر 2024
 * الوصف: تعريف كلاسات الطالب، المادة، الدرجات، المدفوعات، الأخبار
 */
namespace UniversitySystem.Models
{
    // =============== كلاس الطالب ===============
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Department { get; set; } = "";
        public string FacultyName { get; set; } = "";
        public int Level { get; set; } = 1;
        public int Semester { get; set; } = 1;
        public string Track { get; set; } = "General";
        public int SeatNumber { get; set; }
        public string EnrollmentYear { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public double Balance { get; set; }
        
        // Grade properties for professor forms
        public double S1 { get; set; }
        public double S2 { get; set; }
        public double FinalExam { get; set; }

        public override string ToString() => $"{Name} ({Email})";
    }

    public class Course
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public int CreditHours { get; set; } = 3;
        public string Instructor { get; set; } = "";
        public string Department { get; set; } = "";
        public int Semester { get; set; } = 1;
        public int Level { get; set; } = 1;
        public string Track { get; set; } = "General";
        public int MaxStudents { get; set; } = 50;
        public int CurrentEnrolled { get; set; }
        public double Fees { get; set; }

        public override string ToString() => $"{Code} - {Name}";
    }

    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string CourseCode { get; set; } = "";
        public double S1Score { get; set; }      // 10%
        public double S2Score { get; set; }      // 10%
        public double YearWorkScore { get; set; } // 20%
        public double FinalExamScore { get; set; } // 60%
        public double TotalScore { get; set; }
        public string LetterGrade { get; set; } = "";
        public string PearsonGrade { get; set; } = "";  // نظام Pearson: D, M, P, F
        public int Semester { get; set; }
        public string AcademicYear { get; set; } = "";

        public void CalculateTotal()
        {
            TotalScore = S1Score + S2Score + YearWorkScore + FinalExamScore;
            CalculateLetterGrade();
            CalculatePearsonGrade();  // حساب درجة Pearson
        }

        public void CalculateLetterGrade()
        {
            LetterGrade = TotalScore switch
            {
                >= 90 => "A+",
                >= 85 => "A",
                >= 80 => "B+",
                >= 75 => "B",
                >= 70 => "C+",
                >= 65 => "C",
                >= 60 => "D+",
                >= 50 => "D",
                _ => "F"
            };
        }

        // =============== نظام Pearson للتقييم ===============
        // D = Distinction (امتياز) - 80% وأعلى
        // M = Merit (جيد جداً) - 65% إلى 79%
        // P = Pass (ناجح) - 50% إلى 64%
        // F = Fail (راسب) - أقل من 50%
        public void CalculatePearsonGrade()
        {
            PearsonGrade = TotalScore switch
            {
                >= 80 => "D",   // Distinction
                >= 65 => "M",   // Merit
                >= 50 => "P",   // Pass
                _ => "F"        // Fail
            };
        }

        public string GetPearsonDescription()
        {
            return PearsonGrade switch
            {
                "D" => "Distinction (امتياز)",
                "M" => "Merit (جيد جداً)",
                "P" => "Pass (ناجح)",
                _ => "Fail (راسب)"
            };
        }

        public double GetGPA()
        {
            return LetterGrade switch
            {
                "A+" => 4.0,
                "A" => 3.7,
                "B+" => 3.3,
                "B" => 3.0,
                "C+" => 2.7,
                "C" => 2.3,
                "D+" => 2.0,
                "D" => 1.0,
                _ => 0.0
            };
        }
    }

    public class Payment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public double Amount { get; set; }
        public string PaymentDate { get; set; } = "";
        public PaymentType Type { get; set; }
        public PaymentStatus Status { get; set; }
        public string Description { get; set; } = "";
        public string ReceiptNumber { get; set; } = "";
        public int Semester { get; set; }
        public string AcademicYear { get; set; } = "";
    }

    public enum PaymentType
    {
        TuitionFees,
        BookFees,
        LabFees,
        RegistrationFees,
        OtherFees
    }

    public enum PaymentStatus
    {
        Paid,
        Pending,
        Overdue,
        Cancelled
    }

    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public string Author { get; set; } = "";
        public string PublishDate { get; set; } = "";
        public int Category { get; set; }
        public bool IsPinned { get; set; }
        public bool IsActive { get; set; } = true;
        public string TargetDepartment { get; set; } = "";
    }
}
