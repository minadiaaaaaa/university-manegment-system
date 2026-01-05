/*
 * نظام إدارة الجامعة - كلاس قاعدة البيانات
 * تم عمله بواسطة: Mina Diaa
 * التاريخ: ديسمبر 2024
 * الوصف: التعامل مع قاعدة البيانات SQLite
 */
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using UniversitySystem.Models;

namespace UniversitySystem.Data
{
    // =============== كلاس المساعد لقاعدة البيانات ===============
    public class DatabaseHelper
    {
        private readonly string _connectionString;
        private readonly string _dbPath;

        public DatabaseHelper(string dbPath = "university.db")
        {
            _dbPath = dbPath;
            _connectionString = $"Data Source={dbPath};Version=3;";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            // Create database if not exists
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);
            }

            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();

            // Try to read schema from project directory (relative path from bin)
            string schemaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "schema.sql");
            
            if (File.Exists(schemaPath))
            {
                string schema = File.ReadAllText(schemaPath);
                ExecuteNonQuery(schema, conn);
            }
            else
            {
                // Fallback: create tables inline
                CreateAllTables(conn);
            }
            
            // Always add sample data if faculties table is empty
            try
            {
                if (GetCount("faculties", conn) == 0)
                {
                    InitializeSampleData(conn);
                }
            }
            catch
            {
                // If table doesn't exist yet, create and initialize
                CreateAllTables(conn);
                InitializeSampleData(conn);
            }
        }

        private void CreateAllTables(SQLiteConnection conn)
        {
            string sql = @"
                -- Faculties
                CREATE TABLE IF NOT EXISTS faculties (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    name_ar TEXT,
                    code TEXT UNIQUE,
                    dean_name TEXT
                );

                -- Departments
                CREATE TABLE IF NOT EXISTS departments (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    name_ar TEXT,
                    code TEXT UNIQUE,
                    faculty_id INTEGER,
                    head_name TEXT,
                    FOREIGN KEY (faculty_id) REFERENCES faculties(id)
                );

                -- Students
                CREATE TABLE IF NOT EXISTS students (
                    id INTEGER PRIMARY KEY,
                    name TEXT NOT NULL,
                    name_ar TEXT,
                    email TEXT UNIQUE NOT NULL,
                    password TEXT NOT NULL,
                    national_id TEXT,
                    department_id INTEGER,
                    level INTEGER DEFAULT 1,
                    semester INTEGER DEFAULT 1,
                    track TEXT DEFAULT 'General',
                    seat_number INTEGER DEFAULT 0,
                    enrollment_year TEXT,
                    phone TEXT,
                    address TEXT,
                    birth_date TEXT,
                    gender TEXT,
                    balance REAL DEFAULT 0,
                    gpa REAL DEFAULT 0,
                    status TEXT DEFAULT 'active',
                    created_at TEXT DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (department_id) REFERENCES departments(id)
                );

                -- Admins
                CREATE TABLE IF NOT EXISTS admins (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    username TEXT UNIQUE NOT NULL,
                    password TEXT NOT NULL,
                    full_name TEXT,
                    email TEXT,
                    role TEXT DEFAULT 'admin',
                    permissions TEXT,
                    is_active INTEGER DEFAULT 1
                );

                -- Instructors
                CREATE TABLE IF NOT EXISTS instructors (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    name_ar TEXT,
                    email TEXT UNIQUE,
                    password TEXT DEFAULT '123456',
                    phone TEXT,
                    department_id INTEGER,
                    title TEXT,
                    specialization TEXT,
                    is_active INTEGER DEFAULT 1,
                    FOREIGN KEY (department_id) REFERENCES departments(id)
                );

                -- Courses
                CREATE TABLE IF NOT EXISTS courses (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    code TEXT UNIQUE NOT NULL,
                    name TEXT NOT NULL,
                    name_ar TEXT,
                    description TEXT,
                    credit_hours INTEGER DEFAULT 3,
                    lecture_hours INTEGER DEFAULT 2,
                    lab_hours INTEGER DEFAULT 2,
                    department_id INTEGER,
                    level INTEGER DEFAULT 1,
                    semester INTEGER DEFAULT 1,
                    course_type TEXT DEFAULT 'mandatory',
                    prerequisites TEXT,
                    max_students INTEGER DEFAULT 50,
                    fees REAL DEFAULT 0,
                    is_active INTEGER DEFAULT 1,
                    FOREIGN KEY (department_id) REFERENCES departments(id)
                );

                -- Registrations
                CREATE TABLE IF NOT EXISTS registrations (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    student_id INTEGER NOT NULL,
                    course_id INTEGER NOT NULL,
                    instructor_id INTEGER,
                    academic_year TEXT,
                    semester INTEGER,
                    registration_date TEXT DEFAULT CURRENT_TIMESTAMP,
                    status TEXT DEFAULT 'registered',
                    FOREIGN KEY (student_id) REFERENCES students(id),
                    FOREIGN KEY (course_id) REFERENCES courses(id),
                    FOREIGN KEY (instructor_id) REFERENCES instructors(id)
                );

                -- Grades (comprehensive)
                CREATE TABLE IF NOT EXISTS grades (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    student_id INTEGER NOT NULL,
                    course_id INTEGER NOT NULL,
                    academic_year TEXT,
                    semester INTEGER,
                    assignment1_score REAL DEFAULT 0,
                    assignment1_max REAL DEFAULT 10,
                    assignment2_score REAL DEFAULT 0,
                    assignment2_max REAL DEFAULT 10,
                    quiz1_score REAL DEFAULT 0,
                    quiz1_max REAL DEFAULT 10,
                    quiz2_score REAL DEFAULT 0,
                    quiz2_max REAL DEFAULT 10,
                    midterm_score REAL DEFAULT 0,
                    midterm_max REAL DEFAULT 20,
                    practical_score REAL DEFAULT 0,
                    practical_max REAL DEFAULT 20,
                    year_work_score REAL DEFAULT 0,
                    year_work_max REAL DEFAULT 40,
                    final_exam_score REAL DEFAULT 0,
                    final_max REAL DEFAULT 60,
                    total_score REAL DEFAULT 0,
                    letter_grade TEXT,
                    grade_points REAL DEFAULT 0,
                    mercy_points REAL DEFAULT 0,
                    mercy_reason TEXT,
                    attendance_percentage REAL DEFAULT 100,
                    status TEXT DEFAULT 'in_progress',
                    notes TEXT,
                    FOREIGN KEY (student_id) REFERENCES students(id),
                    FOREIGN KEY (course_id) REFERENCES courses(id)
                );

                -- Assignments
                CREATE TABLE IF NOT EXISTS assignments (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    course_id INTEGER NOT NULL,
                    title TEXT NOT NULL,
                    description TEXT,
                    assignment_number INTEGER,
                    max_score REAL DEFAULT 10,
                    due_date TEXT,
                    academic_year TEXT,
                    semester INTEGER,
                    is_active INTEGER DEFAULT 1,
                    FOREIGN KEY (course_id) REFERENCES courses(id)
                );

                -- Assignment Submissions
                CREATE TABLE IF NOT EXISTS assignment_submissions (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    assignment_id INTEGER NOT NULL,
                    student_id INTEGER NOT NULL,
                    submission_date TEXT DEFAULT CURRENT_TIMESTAMP,
                    file_url TEXT,
                    score REAL,
                    feedback TEXT,
                    status TEXT DEFAULT 'submitted',
                    FOREIGN KEY (assignment_id) REFERENCES assignments(id),
                    FOREIGN KEY (student_id) REFERENCES students(id)
                );

                -- Schedule
                CREATE TABLE IF NOT EXISTS schedule (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    course_id INTEGER NOT NULL,
                    instructor_id INTEGER,
                    day_of_week TEXT NOT NULL,
                    start_time TEXT NOT NULL,
                    end_time TEXT NOT NULL,
                    room TEXT,
                    building TEXT,
                    class_type TEXT DEFAULT 'lecture',
                    academic_year TEXT,
                    semester INTEGER,
                    section TEXT,
                    FOREIGN KEY (course_id) REFERENCES courses(id),
                    FOREIGN KEY (instructor_id) REFERENCES instructors(id)
                );

                -- Exam Schedule
                CREATE TABLE IF NOT EXISTS exam_schedule (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    course_id INTEGER NOT NULL,
                    exam_type TEXT NOT NULL,
                    exam_date TEXT NOT NULL,
                    start_time TEXT,
                    end_time TEXT,
                    duration_minutes INTEGER,
                    room TEXT,
                    academic_year TEXT,
                    semester INTEGER,
                    FOREIGN KEY (course_id) REFERENCES courses(id)
                );

                -- Attendance
                CREATE TABLE IF NOT EXISTS attendance (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    student_id INTEGER NOT NULL,
                    course_id INTEGER NOT NULL,
                    schedule_id INTEGER,
                    date TEXT NOT NULL,
                    status TEXT DEFAULT 'present',
                    notes TEXT,
                    FOREIGN KEY (student_id) REFERENCES students(id),
                    FOREIGN KEY (course_id) REFERENCES courses(id)
                );

                -- Payments
                CREATE TABLE IF NOT EXISTS payments (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    student_id INTEGER NOT NULL,
                    amount REAL NOT NULL,
                    payment_date TEXT DEFAULT CURRENT_TIMESTAMP,
                    payment_type TEXT,
                    payment_method TEXT,
                    receipt_number TEXT,
                    academic_year TEXT,
                    semester INTEGER,
                    description TEXT,
                    status TEXT DEFAULT 'pending',
                    due_date TEXT,
                    FOREIGN KEY (student_id) REFERENCES students(id)
                );

                -- Fees
                CREATE TABLE IF NOT EXISTS fees (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    name_ar TEXT,
                    amount REAL NOT NULL,
                    fee_type TEXT,
                    department_id INTEGER,
                    level INTEGER,
                    academic_year TEXT,
                    is_mandatory INTEGER DEFAULT 1
                );

                -- News
                CREATE TABLE IF NOT EXISTS news (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    title TEXT NOT NULL,
                    title_ar TEXT,
                    content TEXT,
                    author_id INTEGER,
                    category TEXT,
                    target_audience TEXT,
                    publish_date TEXT DEFAULT CURRENT_TIMESTAMP,
                    expire_date TEXT,
                    is_pinned INTEGER DEFAULT 0,
                    is_active INTEGER DEFAULT 1
                );

                -- Academic Years
                CREATE TABLE IF NOT EXISTS academic_years (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    year_name TEXT NOT NULL,
                    start_date TEXT,
                    end_date TEXT,
                    is_current INTEGER DEFAULT 0,
                    semester1_start TEXT,
                    semester1_end TEXT,
                    semester2_start TEXT,
                    semester2_end TEXT
                );

                -- Settings
                CREATE TABLE IF NOT EXISTS settings (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    key TEXT UNIQUE NOT NULL,
                    value TEXT,
                    description TEXT
                );

                -- Indexes
                CREATE INDEX IF NOT EXISTS idx_students_email ON students(email);
                CREATE INDEX IF NOT EXISTS idx_students_department ON students(department_id);
                CREATE INDEX IF NOT EXISTS idx_grades_student ON grades(student_id);
                CREATE INDEX IF NOT EXISTS idx_grades_course ON grades(course_id);
            ";

            ExecuteNonQuery(sql, conn);
        }

        private void InitializeSampleData(SQLiteConnection conn)
        {
            string sql = @"
                -- Faculties (Only 2: Industry and Energy, Health Sciences)
                INSERT OR IGNORE INTO faculties (id, name, name_ar, code) 
                VALUES (1, 'Faculty of Industry and Energy', 'كلية الصناعة والطاقة', 'FIE');
                INSERT OR IGNORE INTO faculties (id, name, name_ar, code) 
                VALUES (2, 'Faculty of Health Sciences', 'كلية العلوم الصحية', 'FHS');

                -- Departments
                INSERT OR IGNORE INTO departments (id, name, name_ar, code, faculty_id) 
                VALUES (1, 'Information Technology', 'تكنولوجيا المعلومات', 'IT', 1);
                INSERT OR IGNORE INTO departments (id, name, name_ar, code, faculty_id) 
                VALUES (2, 'Prosthetics', 'الأطراف الصناعية', 'PROS', 2);
                INSERT OR IGNORE INTO departments (id, name, name_ar, code, faculty_id) 
                VALUES (3, 'Mechatronics', 'الميكاترونكس', 'MECH', 1);

                -- Students (sample)
                INSERT OR IGNORE INTO students (id, name, name_ar, email, password, department_id, level, seat_number) 
                VALUES (1, 'Ahmed Mohamed', 'أحمد محمد', 'ahmed@university.edu', '123456', 1, 1, 1001);
                INSERT OR IGNORE INTO students (id, name, name_ar, email, password, department_id, level, seat_number) 
                VALUES (2, 'Sara Ali', 'سارة علي', 'sara@university.edu', '123456', 1, 2, 1002);
            ";

            ExecuteNonQuery(sql, conn);
        }

        // ==================== HELPER METHODS ====================

        private void ExecuteNonQuery(string sql, SQLiteConnection conn)
        {
            try
            {
                using var cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error in sample data: {ex.Message}", "DB Error");
            }
        }

        private long GetCount(string table, SQLiteConnection conn)
        {
            using var cmd = new SQLiteCommand($"SELECT COUNT(*) FROM {table}", conn);
            return (long)cmd.ExecuteScalar()!;
        }

        private SQLiteConnection GetConnection()
        {
            var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            return conn;
        }

        // ==================== AUTHENTICATION ====================

        public Student? AuthenticateStudent(string email, string password)
        {
            using var conn = GetConnection();
            var cmd = new SQLiteCommand(
                "SELECT * FROM students WHERE email = @email AND password = @password", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Read columns dynamically to handle both old and new schema
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string studentEmail = "";
                string pwd = "";
                int level = 1;
                int semester = 1;
                string track = "General";
                int seatNumber = 0;
                string department = "";

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string colName = reader.GetName(i).ToLower();
                    if (!reader.IsDBNull(i))
                    {
                        if (colName == "email") studentEmail = reader.GetString(i);
                        else if (colName == "password") pwd = reader.GetString(i);
                        else if (colName == "level") level = reader.GetInt32(i);
                        else if (colName == "semester") semester = reader.GetInt32(i);
                        else if (colName == "track") track = reader.GetString(i);
                        else if (colName == "seat_number") seatNumber = reader.GetInt32(i);
                        else if (colName == "department_id") department = GetDepartmentName(reader.GetInt32(i));
                    }
                }

                return new Student
                {
                    Id = id,
                    Name = name,
                    Email = studentEmail,
                    Password = pwd,
                    Department = department,
                    Level = level,
                    Semester = semester,
                    Track = track,
                    SeatNumber = seatNumber
                };
            }
            return null;
        }

        public bool AuthenticateAdmin(string username, string password)
        {
            using var conn = GetConnection();
            var cmd = new SQLiteCommand(
                "SELECT COUNT(*) FROM admins WHERE username = @user AND password = @pass", conn);
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@pass", password);
            return (long)cmd.ExecuteScalar()! > 0;
        }
        // ==================== PROFESSOR AUTHENTICATION ====================

        public Instructor? AuthenticateProfessor(string email, string password)
        {
            using var conn = GetConnection();
            var cmd = new SQLiteCommand(
                "SELECT instructor_id, name, email, phone, specialization, title FROM instructors WHERE email = @email AND password = @password AND is_active = 1", conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Instructor
                {
                    Id = reader.GetInt32(0),
                    Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Email = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Phone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Specialization = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Title = reader.IsDBNull(5) ? "" : reader.GetString(5)
                };
            }
            return null;
        }

        public List<Course> GetProfessorCourses(int instructorId)
        {
            var courses = new List<Course>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT DISTINCT c.course_id, c.course_code, c.course_name, c.credit_hours, c.year, c.term,
                           (SELECT COUNT(*) FROM enrollments e WHERE e.course_id = c.course_id) as student_count
                    FROM course_offerings co
                    JOIN courses c ON co.course_id = c.course_id
                    WHERE co.instructor_id = @id
                    ORDER BY c.year, c.term", conn);
                cmd.Parameters.AddWithValue("@id", instructorId);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        Code = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Name = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        CreditHours = reader.IsDBNull(3) ? 3 : reader.GetInt32(3),
                        Level = reader.IsDBNull(4) ? 1 : reader.GetInt32(4),
                        Semester = reader.IsDBNull(5) ? 1 : reader.GetInt32(5)
                    });
                }
            }
            catch { }
            return courses;
        }

        public List<Student> GetStudentsInCourse(string courseCode)
        {
            var students = new List<Student>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT s.id, s.name, s.email, s.seat_number,
                           COALESCE(g.s1, 0) as s1, COALESCE(g.s2, 0) as s2, 
                           COALESCE(g.final_exam, 0) as final_exam
                    FROM enrollments e
                    JOIN students s ON e.student_id = s.id
                    JOIN courses c ON e.course_id = c.course_id
                    LEFT JOIN grades g ON e.enrollment_id = g.enrollment_id
                    WHERE c.course_code = @code
                    ORDER BY s.name", conn);
                cmd.Parameters.AddWithValue("@code", courseCode);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Email = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        SeatNumber = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                        S1 = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                        S2 = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                        FinalExam = reader.IsDBNull(6) ? 0 : reader.GetDouble(6)
                    });
                }
            }
            catch { }
            return students;
        }

        public bool RecordAttendance(int studentId, string courseCode, string date, string status)
        {
            using var conn = GetConnection();
            try
            {
                // Get course_id
                var getCourseCmd = new SQLiteCommand("SELECT course_id FROM courses WHERE course_code = @code", conn);
                getCourseCmd.Parameters.AddWithValue("@code", courseCode);
                var result = getCourseCmd.ExecuteScalar();
                if (result == null) return false;
                int courseId = Convert.ToInt32(result);

                var cmd = new SQLiteCommand(@"
                    INSERT OR REPLACE INTO attendance (student_id, course_id, date, status)
                    VALUES (@sid, @cid, @date, @status)", conn);
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@cid", courseId);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@status", status);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }

        public bool UpdateStudentGrade(int studentId, string courseCode, string gradeType, double score)
        {
            using var conn = GetConnection();
            try
            {
                // Get enrollment_id
                var getEnrollCmd = new SQLiteCommand(@"
                    SELECT e.enrollment_id FROM enrollments e
                    JOIN courses c ON e.course_id = c.course_id
                    WHERE e.student_id = @sid AND c.course_code = @code", conn);
                getEnrollCmd.Parameters.AddWithValue("@sid", studentId);
                getEnrollCmd.Parameters.AddWithValue("@code", courseCode);
                var result = getEnrollCmd.ExecuteScalar();
                if (result == null) return false;
                int enrollmentId = Convert.ToInt32(result);

                // Check if grade exists
                var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM grades WHERE enrollment_id = @eid", conn);
                checkCmd.Parameters.AddWithValue("@eid", enrollmentId);
                bool exists = (long)checkCmd.ExecuteScalar()! > 0;

                string sql;
                if (exists)
                {
                    sql = $"UPDATE grades SET {gradeType} = @score, total = s1 + s2 + final_exam WHERE enrollment_id = @eid";
                }
                else
                {
                    sql = $"INSERT INTO grades (enrollment_id, {gradeType}) VALUES (@eid, @score)";
                }

                var cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@eid", enrollmentId);
                cmd.Parameters.AddWithValue("@score", score);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }

        public List<(int StudentId, string Name, string Date, string Status)> GetCourseAttendance(string courseCode)
        {
            var records = new List<(int, string, string, string)>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT s.id, s.name, a.date, a.status
                    FROM attendance a
                    JOIN students s ON a.student_id = s.id
                    JOIN courses c ON a.course_id = c.course_id
                    WHERE c.course_code = @code
                    ORDER BY a.date DESC, s.name", conn);
                cmd.Parameters.AddWithValue("@code", courseCode);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    records.Add((
                        reader.GetInt32(0),
                        reader.IsDBNull(1) ? "" : reader.GetString(1),
                        reader.IsDBNull(2) ? "" : reader.GetString(2),
                        reader.IsDBNull(3) ? "" : reader.GetString(3)
                    ));
                }
            }
            catch { }
            return records;
        }

        // ==================== DEPARTMENTS ====================

        public string GetDepartmentName(int id)
        {
            using var conn = GetConnection();
            var cmd = new SQLiteCommand("SELECT name FROM departments WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            var result = cmd.ExecuteScalar();
            return result?.ToString() ?? "";
        }

        // ==================== STUDENTS ====================

        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();
            using var conn = GetConnection();
            var cmd = new SQLiteCommand(@"
                SELECT s.*, d.name as dept_name 
                FROM students s 
                LEFT JOIN departments d ON s.department_id = d.id", conn);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                students.Add(new Student
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(3),
                    Department = reader.IsDBNull(reader.GetOrdinal("dept_name")) ? "" : reader.GetString(reader.GetOrdinal("dept_name")),
                    Level = reader.IsDBNull(7) ? 1 : reader.GetInt32(7),
                    SeatNumber = reader.IsDBNull(9) ? 0 : reader.GetInt32(9)
                });
            }
            return students;
        }

        public List<Student> GetAllStudentsWithFaculty()
        {
            var students = new List<Student>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT s.*, d.name as dept_name, f.name as faculty_name 
                    FROM students s 
                    LEFT JOIN departments d ON s.department_id = d.id
                    LEFT JOIN faculties f ON d.faculty_id = f.id
                    ORDER BY f.name, d.name, s.name", conn);
                
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Email = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        Department = reader.IsDBNull(reader.GetOrdinal("dept_name")) ? "" : reader.GetString(reader.GetOrdinal("dept_name")),
                        FacultyName = reader.IsDBNull(reader.GetOrdinal("faculty_name")) ? "" : reader.GetString(reader.GetOrdinal("faculty_name")),
                        Level = reader.IsDBNull(7) ? 1 : reader.GetInt32(7),
                        SeatNumber = reader.IsDBNull(9) ? 0 : reader.GetInt32(9)
                    });
                }
            }
            catch { }
            return students;
        }

        // Generate student ID: Year prefix (based on level) + sequential number
        // Level 1 -> 2024xxx, Level 2 -> 2023xxx, etc. (enrollment year)
        public int GenerateStudentId(int level)
        {
            // Calculate enrollment year based on level
            // Current year is 2024
            // Level 1 enrolled in 2024 -> ID starts with 2024
            // Level 2 enrolled in 2023 -> ID starts with 2023
            // Level 3 enrolled in 2022 -> ID starts with 2022
            // Level 4 enrolled in 2021 -> ID starts with 2021
            int currentYear = 2024; // Fixed to 2024 as base year
            int enrollmentYear = currentYear - (level - 1);
            
            using var conn = GetConnection();
            try
            {
                // Get the max ID for this enrollment year prefix
                // IDs are in format: YYYYnnn (e.g., 2024001, 2024002, 2023001)
                int yearPrefix = enrollmentYear * 1000; // e.g., 2024000
                int nextYearPrefix = (enrollmentYear + 1) * 1000; // e.g., 2025000
                
                var cmd = new SQLiteCommand(@"
                    SELECT MAX(id) FROM students 
                    WHERE id >= @minId AND id < @maxId", conn);
                cmd.Parameters.AddWithValue("@minId", yearPrefix);
                cmd.Parameters.AddWithValue("@maxId", nextYearPrefix);
                
                var result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    return yearPrefix + 1; // First student of this year: e.g., 2023001
                }
                else
                {
                    return Convert.ToInt32(result) + 1; // Next in sequence
                }
            }
            catch
            {
                // Fallback: use timestamp-based ID
                return enrollmentYear * 1000 + new Random().Next(1, 999);
            }
        }

        public bool AddStudent(Student student)
        {
            using var conn = GetConnection();
            try
            {
                // Generate ID if not set
                int studentId = student.Id > 0 ? student.Id : GenerateStudentId(student.Level);
                
                var cmd = new SQLiteCommand(@"
                    INSERT INTO students (id, name, email, password, department_id, level, seat_number)
                    VALUES (@id, @name, @email, @password, @dept, @level, @seat)", conn);
                
                cmd.Parameters.AddWithValue("@id", studentId);
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@password", student.Password);
                cmd.Parameters.AddWithValue("@dept", 1);
                cmd.Parameters.AddWithValue("@level", student.Level);
                cmd.Parameters.AddWithValue("@seat", student.SeatNumber);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }

        public bool UpdateStudent(Student student)
        {
            using var conn = GetConnection();
            var cmd = new SQLiteCommand(@"
                UPDATE students SET name=@name, email=@email, level=@level, seat_number=@seat 
                WHERE id=@id", conn);
            
            cmd.Parameters.AddWithValue("@id", student.Id);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@email", student.Email);
            cmd.Parameters.AddWithValue("@level", student.Level);
            cmd.Parameters.AddWithValue("@seat", student.SeatNumber);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteStudent(int id)
        {
            using var conn = GetConnection();
            var cmd = new SQLiteCommand("DELETE FROM students WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool AddStudentWithDept(Student student, int departmentId, string nameAr, string nationalId)
        {
            using var conn = GetConnection();
            try
            {
                // Generate ID if not set
                int studentId = student.Id > 0 ? student.Id : GenerateStudentId(student.Level);
                
                var cmd = new SQLiteCommand(@"
                    INSERT INTO students (id, name, name_ar, email, password, national_id, department_id, 
                        level, semester, track, phone, enrollment_year)
                    VALUES (@id, @name, @nameAr, @email, @password, @nationalId, @deptId, 
                        @level, @semester, @track, @phone, @year)", conn);
                
                cmd.Parameters.AddWithValue("@id", studentId);
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@nameAr", nameAr ?? "");
                cmd.Parameters.AddWithValue("@email", student.Email);
                cmd.Parameters.AddWithValue("@password", student.Password);
                cmd.Parameters.AddWithValue("@nationalId", nationalId ?? "");
                cmd.Parameters.AddWithValue("@deptId", departmentId);
                cmd.Parameters.AddWithValue("@level", student.Level);
                cmd.Parameters.AddWithValue("@semester", student.Semester);
                cmd.Parameters.AddWithValue("@track", student.Track ?? "General");
                cmd.Parameters.AddWithValue("@phone", student.Phone ?? "");
                cmd.Parameters.AddWithValue("@year", (DateTime.Now.Year - (student.Level - 1)).ToString());

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"DB Error: {ex.Message}", "Debug", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        // ==================== COURSES ====================

        public List<Course> GetAllCourses()
        {
            var courses = new List<Course>();
            using var conn = GetConnection();
            
            // Check if we're using new schema (courses table with course_name) or old (name)
            var cmd = new SQLiteCommand(@"
                SELECT * FROM courses 
                WHERE is_active = 1 OR is_active IS NULL 
                ORDER BY year, term, track", conn);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                // Try to get course_name first (new schema), then name (old schema)
                string courseName = "";
                string courseCode = "";
                int creditHours = 3;
                int level = 1;
                int semester = 1;
                
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string colName = reader.GetName(i).ToLower();
                    if (colName == "course_name" && !reader.IsDBNull(i))
                        courseName = reader.GetString(i);
                    else if (colName == "name" && !reader.IsDBNull(i) && string.IsNullOrEmpty(courseName))
                        courseName = reader.GetString(i);
                    else if (colName == "course_code" && !reader.IsDBNull(i))
                        courseCode = reader.GetString(i);
                    else if (colName == "code" && !reader.IsDBNull(i) && string.IsNullOrEmpty(courseCode))
                        courseCode = reader.GetString(i);
                    else if (colName == "credit_hours" && !reader.IsDBNull(i))
                        creditHours = reader.GetInt32(i);
                    else if (colName == "year" && !reader.IsDBNull(i))
                        level = reader.GetInt32(i);
                    else if (colName == "level" && !reader.IsDBNull(i))
                        level = reader.GetInt32(i);
                    else if (colName == "term" && !reader.IsDBNull(i))
                        semester = reader.GetInt32(i);
                    else if (colName == "semester" && !reader.IsDBNull(i))
                        semester = reader.GetInt32(i);
                }
                
                courses.Add(new Course
                {
                    Code = courseCode,
                    Name = courseName,
                    CreditHours = creditHours,
                    Instructor = "",
                    Department = "",
                    Semester = semester,
                    Level = level
                });
            }
            return courses;
        }

        // Get courses for a specific year and term (for student registration)
        public List<Course> GetCoursesByLevelAndTerm(int level, int term, string track)
        {
            var courses = new List<Course>();
            using var conn = GetConnection();
            
            // For Year 1 and 2, all students take General track courses
            // For Year 3 and 4, students take courses based on their track
            string trackFilter = (level <= 2) ? "General" : track;
            
            var cmd = new SQLiteCommand(@"
                SELECT course_id, course_name, course_code, credit_hours, year, term, track
                FROM courses 
                WHERE year = @level AND term = @term AND track = @track
                ORDER BY course_name", conn);
            
            cmd.Parameters.AddWithValue("@level", level);
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@track", trackFilter);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                courses.Add(new Course
                {
                    Code = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    CreditHours = reader.IsDBNull(3) ? 3 : reader.GetInt32(3),
                    Level = reader.IsDBNull(4) ? level : reader.GetInt32(4),
                    Semester = reader.IsDBNull(5) ? term : reader.GetInt32(5)
                });
            }
            return courses;
        }

        // Get carryover courses (courses from previous terms not registered/passed)
        public List<Course> GetCarryoverCourses(int studentId, int currentLevel, int currentTerm, string track)
        {
            var carryoverCourses = new List<Course>();
            using var conn = GetConnection();

            // Calculate all previous terms for the student
            // For example, if student is in Year 2 Term 1, check: Year 1 Term 1, Year 1 Term 2
            for (int year = 1; year < currentLevel; year++)
            {
                for (int term = 1; term <= 2; term++)
                {
                    AddUnregisteredCourses(conn, studentId, year, term, track, carryoverCourses);
                }
            }

            // Also check previous term in current year
            if (currentTerm == 2)
            {
                AddUnregisteredCourses(conn, studentId, currentLevel, 1, track, carryoverCourses);
            }

            return carryoverCourses;
        }

        private void AddUnregisteredCourses(SQLiteConnection conn, int studentId, int year, int term, string track, List<Course> courses)
        {
            string trackFilter = (year <= 2) ? "General" : track;

            var cmd = new SQLiteCommand(@"
                SELECT c.course_id, c.course_name, c.course_code, c.credit_hours, c.year, c.term
                FROM courses c
                WHERE c.year = @year AND c.term = @term AND c.track = @track
                AND c.course_id NOT IN (
                    SELECT e.course_id FROM enrollments e 
                    WHERE e.student_id = @sid AND e.status IN ('Enrolled', 'Passed')
                )
                ORDER BY c.course_name", conn);

            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@track", trackFilter);
            cmd.Parameters.AddWithValue("@sid", studentId);

            try
            {
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        Code = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        CreditHours = reader.IsDBNull(3) ? 3 : reader.GetInt32(3),
                        Level = reader.IsDBNull(4) ? year : reader.GetInt32(4),
                        Semester = reader.IsDBNull(5) ? term : reader.GetInt32(5)
                    });
                }
            }
            catch { }
        }

        // Register a student for a course
        public bool RegisterForCourse(int studentId, string courseCode)
        {
            using var conn = GetConnection();
            try
            {
                // Get course_id from course_code
                var getCourseCmd = new SQLiteCommand(
                    "SELECT course_id FROM courses WHERE course_code = @code", conn);
                getCourseCmd.Parameters.AddWithValue("@code", courseCode);
                var result = getCourseCmd.ExecuteScalar();
                if (result == null) return false;
                int courseId = Convert.ToInt32(result);

                // Check if already registered
                var checkCmd = new SQLiteCommand(@"
                    SELECT COUNT(*) FROM enrollments 
                    WHERE student_id = @sid AND course_id = @cid", conn);
                checkCmd.Parameters.AddWithValue("@sid", studentId);
                checkCmd.Parameters.AddWithValue("@cid", courseId);
                if ((long)checkCmd.ExecuteScalar()! > 0) return false; // Already registered

                // Register the student
                var insertCmd = new SQLiteCommand(@"
                    INSERT INTO enrollments (student_id, course_id, academic_year, semester, status)
                    VALUES (@sid, @cid, '2024-2025', 1, 'Enrolled')", conn);
                insertCmd.Parameters.AddWithValue("@sid", studentId);
                insertCmd.Parameters.AddWithValue("@cid", courseId);
                return insertCmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }

        // ==================== COURSE OFFERINGS (Assign Instructor to Course) ====================

        public List<(int OfferingId, string CourseName, string CourseCode, string InstructorName, string AcademicYear)> GetAllCourseOfferings()
        {
            var list = new List<(int, string, string, string, string)>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT co.offering_id, c.course_name, c.course_code, i.name, co.academic_year
                    FROM course_offerings co
                    JOIN courses c ON co.course_id = c.course_id
                    JOIN instructors i ON co.instructor_id = i.instructor_id
                    ORDER BY co.academic_year DESC, c.course_name", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add((
                        reader.GetInt32(0),
                        reader.IsDBNull(1) ? "" : reader.GetString(1),
                        reader.IsDBNull(2) ? "" : reader.GetString(2),
                        reader.IsDBNull(3) ? "" : reader.GetString(3),
                        reader.IsDBNull(4) ? "" : reader.GetString(4)
                    ));
                }
            }
            catch { }
            return list;
        }

        public bool AssignCourseToInstructor(int courseId, int instructorId, string academicYear, int semester)
        {
            using var conn = GetConnection();
            try
            {
                // Check if already assigned
                var checkCmd = new SQLiteCommand(@"
                    SELECT COUNT(*) FROM course_offerings 
                    WHERE course_id = @cid AND instructor_id = @iid AND academic_year = @year", conn);
                checkCmd.Parameters.AddWithValue("@cid", courseId);
                checkCmd.Parameters.AddWithValue("@iid", instructorId);
                checkCmd.Parameters.AddWithValue("@year", academicYear);
                if ((long)checkCmd.ExecuteScalar()! > 0) return false;

                var cmd = new SQLiteCommand(@"
                    INSERT INTO course_offerings (course_id, instructor_id, section_id, academic_year, semester)
                    VALUES (@cid, @iid, 1, @year, @sem)", conn);
                cmd.Parameters.AddWithValue("@cid", courseId);
                cmd.Parameters.AddWithValue("@iid", instructorId);
                cmd.Parameters.AddWithValue("@year", academicYear);
                cmd.Parameters.AddWithValue("@sem", semester);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }

        public bool RemoveCourseOffering(int offeringId)
        {
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand("DELETE FROM course_offerings WHERE offering_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", offeringId);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }

        public List<(int Id, string Name)> GetAllCoursesSimple()
        {
            var list = new List<(int, string)>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand("SELECT course_id, course_name FROM courses WHERE is_active = 1 ORDER BY course_name", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add((reader.GetInt32(0), reader.IsDBNull(1) ? "" : reader.GetString(1)));
            }
            catch { }
            return list;
        }

        public List<(int Id, string Name)> GetAllInstructorsSimple()
        {
            var list = new List<(int, string)>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand("SELECT instructor_id, name FROM instructors WHERE is_active = 1 ORDER BY name", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add((reader.GetInt32(0), reader.IsDBNull(1) ? "" : reader.GetString(1)));
            }
            catch { }
            return list;
        }


        // Get registered courses for a student
        public List<Course> GetRegisteredCourses(int studentId)
        {
            var courses = new List<Course>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT c.course_code, c.course_name, c.credit_hours, c.year, c.term, e.status
                    FROM enrollments e
                    JOIN courses c ON e.course_id = c.course_id
                    WHERE e.student_id = @sid
                    ORDER BY c.course_name", conn);
                cmd.Parameters.AddWithValue("@sid", studentId);
                
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        Code = reader.IsDBNull(0) ? "" : reader.GetString(0),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        CreditHours = reader.IsDBNull(2) ? 3 : reader.GetInt32(2),
                        Level = reader.IsDBNull(3) ? 1 : reader.GetInt32(3),
                        Semester = reader.IsDBNull(4) ? 1 : reader.GetInt32(4)
                    });
                }
            }
            catch { }
            return courses;
        }

        // Check if student is registered for a course
        public bool IsRegisteredForCourse(int studentId, string courseCode)
        {
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT COUNT(*) FROM enrollments e
                    JOIN courses c ON e.course_id = c.course_id
                    WHERE e.student_id = @sid AND c.course_code = @code", conn);
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@code", courseCode);
                return (long)cmd.ExecuteScalar()! > 0;
            }
            catch
            {
                return false;
            }
        }

        // ==================== GRADES ====================

        public List<Grade> GetGradesByStudent(int studentId)
        {
            var grades = new List<Grade>();
            using var conn = GetConnection();
            
            try
            {
                // JOIN through enrollments table to get grades with course info
                var cmd = new SQLiteCommand(@"
                    SELECT g.grade_id, c.course_code, c.course_name, c.credit_hours,
                           COALESCE(g.s1, 0) as s1, 
                           COALESCE(g.s2, 0) as s2, 
                           COALESCE(g.final_exam, 0) as final_exam,
                           COALESCE(g.total, 0) as total,
                           COALESCE(g.letter_grade, '') as letter_grade
                    FROM grades g 
                    JOIN enrollments e ON g.enrollment_id = e.enrollment_id
                    JOIN courses c ON e.course_id = c.course_id 
                    WHERE e.student_id = @id
                    ORDER BY c.course_name", conn);
                cmd.Parameters.AddWithValue("@id", studentId);
                
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    double s1 = reader.GetDouble(4);
                    double s2 = reader.GetDouble(5);
                    double finalExam = reader.GetDouble(6);
                    double total = reader.GetDouble(7);
                    int creditHours = reader.IsDBNull(3) ? 3 : reader.GetInt32(3);
                    double maxScore = creditHours * 50.0;
                    double percent = maxScore > 0 ? (total / maxScore) * 100 : 0;
                    
                    // Calculate Pearson grade based on percentage
                    string pearson;
                    if (percent >= 100) pearson = "D";
                    else if (percent >= 80) pearson = "M";
                    else if (percent >= 60) pearson = "P";
                    else pearson = "F";
                    
                    // Calculate Letter grade based on percentage (not from DB)
                    string letterGrade;
                    if (percent >= 85) letterGrade = "Excellent";
                    else if (percent >= 75) letterGrade = "Very Good";
                    else if (percent >= 65) letterGrade = "Good";
                    else if (percent >= 50) letterGrade = "Pass";
                    else letterGrade = "Fail";
                    
                    grades.Add(new Grade
                    {
                        Id = reader.GetInt32(0),
                        StudentId = studentId,
                        CourseCode = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        S1Score = s1,
                        S2Score = s2,
                        YearWorkScore = 0,
                        FinalExamScore = finalExam,
                        TotalScore = total,
                        LetterGrade = letterGrade,  // Calculated, not from DB
                        PearsonGrade = pearson
                    });
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"GetGradesByStudent Error: {ex.Message}", "Debug");
            }
            return grades;
        }

        // ==================== MERCY GRADING (نظام الرأفة) ====================

        public class MercyCandidate
        {
            public int GradeId { get; set; }
            public int StudentId { get; set; }
            public string StudentName { get; set; } = "";
            public string CourseName { get; set; } = "";
            public double CurrentTotal { get; set; }
            public double PassingGrade { get; set; }
            public double Difference { get; set; }
            public bool Applied { get; set; }
        }

        // Get students who are close to passing (within mercy range)
        // mercyRange and passingGrade are PERCENTAGES (e.g., 5 and 60)
        public List<MercyCandidate> GetMercyCandidates(double mercyRange = 5, double passingGrade = 60)
        {
            var candidates = new List<MercyCandidate>();
            using var conn = GetConnection();
            try
            {
                // Get grades with course credit hours to calculate percentage
                var cmd = new SQLiteCommand(@"
                    SELECT g.grade_id, e.student_id, s.name, c.course_name,
                           COALESCE(g.s1, 0) + COALESCE(g.s2, 0) + COALESCE(g.final_exam, 0) as total,
                           COALESCE(g.mercy_applied, 0) as mercy_applied,
                           c.credit_hours
                    FROM grades g
                    JOIN enrollments e ON g.enrollment_id = e.enrollment_id
                    JOIN students s ON e.student_id = s.id
                    JOIN courses c ON e.course_id = c.course_id
                    ORDER BY total DESC", conn);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    double total = reader.GetDouble(4);
                    int creditHours = reader.IsDBNull(6) ? 3 : reader.GetInt32(6);
                    double maxScore = creditHours * 50.0;  // Each hour = 50 points
                    double percent = maxScore > 0 ? (total / maxScore) * 100 : 0;
                    
                    // Check if percentage is within mercy range (e.g., 55% to 60%)
                    double minPercent = passingGrade - mercyRange;
                    if (percent >= minPercent && percent < passingGrade)
                    {
                        // Calculate points needed to reach passing grade
                        double passingPoints = (passingGrade / 100.0) * maxScore;
                        double pointsNeeded = passingPoints - total;
                        
                        candidates.Add(new MercyCandidate
                        {
                            GradeId = reader.GetInt32(0),
                            StudentId = reader.GetInt32(1),
                            StudentName = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            CourseName = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            CurrentTotal = percent,  // Store as percentage for display
                            PassingGrade = passingGrade,
                            Difference = pointsNeeded,  // Store as actual points needed
                            Applied = reader.GetInt32(5) == 1
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"GetMercyCandidates Error: {ex.Message}", "Debug");
            }
            return candidates;
        }

        // Apply mercy grade to a student
        public bool ApplyMercyGrade(int gradeId, double mercyPoints)
        {
            using var conn = GetConnection();
            try
            {
                // Add mercy points to final exam, recalculate total, and mark as applied
                var cmd = new SQLiteCommand(@"
                    UPDATE grades 
                    SET final_exam = COALESCE(final_exam, 0) + @mercy,
                        total = COALESCE(s1, 0) + COALESCE(s2, 0) + COALESCE(final_exam, 0) + @mercy,
                        mercy_applied = 1,
                        mercy_points = @mercy
                    WHERE grade_id = @id", conn);
                cmd.Parameters.AddWithValue("@mercy", mercyPoints);
                cmd.Parameters.AddWithValue("@id", gradeId);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }

        // Apply mercy to all eligible candidates
        public int ApplyMercyToAll(double mercyRange = 5, double passingGrade = 50)
        {
            var candidates = GetMercyCandidates(mercyRange, passingGrade);
            int applied = 0;
            foreach (var c in candidates)
            {
                if (!c.Applied && ApplyMercyGrade(c.GradeId, c.Difference))
                    applied++;
            }
            return applied;
        }

        // ==================== NEWS ====================

        public List<News> GetActiveNews()
        {
            var newsList = new List<News>();
            using var conn = GetConnection();
            var cmd = new SQLiteCommand(
                "SELECT news_id, title, description, date, posted_by, category, is_pinned, is_active FROM news WHERE is_active=1 ORDER BY is_pinned DESC, news_id DESC", conn);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                newsList.Add(new News
                {
                    Id = reader.GetInt32(0),
                    Title = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    Content = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Author = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    PublishDate = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    IsPinned = reader.IsDBNull(6) ? false : reader.GetInt32(6) == 1
                });
            }
            return newsList;
        }

        // ==================== SCHEDULE ====================

        public List<ScheduleItem> GetScheduleByStudent(int studentId)
        {
            var schedule = new List<ScheduleItem>();
            // Schedule table might not exist in new schema, return empty
            return schedule;
        }

        // ==================== INSTRUCTORS ====================

        public List<Instructor> GetAllInstructors()
        {
            var instructors = new List<Instructor>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand("SELECT * FROM instructors WHERE is_active = 1 OR is_active IS NULL ORDER BY name", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    instructors.Add(new Instructor
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Email = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Phone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        Specialization = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        Title = reader.IsDBNull(5) ? "" : reader.GetString(5)
                    });
                }
            }
            catch { }
            return instructors;
        }

        public bool AddInstructor(string name, string email, string phone, string title, string specialization)
        {
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    INSERT INTO instructors (name, email, phone, title, specialization, is_active)
                    VALUES (@name, @email, @phone, @title, @spec, 1)", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@spec", specialization);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }

        public bool DeleteInstructor(int id)
        {
            using var conn = GetConnection();
            var cmd = new SQLiteCommand("UPDATE instructors SET is_active = 0 WHERE instructor_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        // ==================== CLASSROOMS / HALLS ====================

        public List<Classroom> GetAllClassrooms()
        {
            var classrooms = new List<Classroom>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand("SELECT * FROM classrooms ORDER BY room_code", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    classrooms.Add(new Classroom
                    {
                        Id = reader.GetInt32(0),
                        Code = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Capacity = reader.IsDBNull(2) ? 50 : reader.GetInt32(2),
                        Building = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        RoomType = reader.IsDBNull(4) ? "Hall" : reader.GetString(4)
                    });
                }
            }
            catch { }
            return classrooms;
        }

        public List<Classroom> GetLabsOnly()
        {
            var labs = new List<Classroom>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand("SELECT * FROM classrooms WHERE room_type = 'Lab' ORDER BY room_code", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    labs.Add(new Classroom
                    {
                        Id = reader.GetInt32(0),
                        Code = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Capacity = reader.IsDBNull(2) ? 50 : reader.GetInt32(2),
                        Building = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        RoomType = "Lab"
                    });
                }
            }
            catch { }
            return labs;
        }

        public bool AddClassroom(string code, string building, int capacity, string roomType)
        {
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    INSERT INTO classrooms (room_code, capacity, building, room_type)
                    VALUES (@code, @capacity, @building, @type)", conn);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@capacity", capacity);
                cmd.Parameters.AddWithValue("@building", building);
                cmd.Parameters.AddWithValue("@type", roomType);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }

        // ==================== GRADE ENTRY ====================

        public bool SaveGrade(int studentId, string courseCode, double s1, double s2, double cw, double final)
        {
            using var conn = GetConnection();
            try
            {
                // Get course_id AND credit_hours from course_code
                var getCourseCmd = new SQLiteCommand("SELECT course_id, credit_hours FROM courses WHERE course_code = @code", conn);
                getCourseCmd.Parameters.AddWithValue("@code", courseCode);
                using var courseReader = getCourseCmd.ExecuteReader();
                if (!courseReader.Read()) return false;
                int courseId = courseReader.GetInt32(0);
                int creditHours = courseReader.IsDBNull(1) ? 3 : courseReader.GetInt32(1);

                // Get enrollment_id
                var getEnrollCmd = new SQLiteCommand(@"
                    SELECT enrollment_id FROM enrollments 
                    WHERE student_id = @sid AND course_id = @cid", conn);
                getEnrollCmd.Parameters.AddWithValue("@sid", studentId);
                getEnrollCmd.Parameters.AddWithValue("@cid", courseId);
                var enrollResult = getEnrollCmd.ExecuteScalar();
                
                int enrollmentId;
                if (enrollResult == null)
                {
                    // Create enrollment if not exists
                    var insertEnrollCmd = new SQLiteCommand(@"
                        INSERT INTO enrollments (student_id, course_id, academic_year, semester, status)
                        VALUES (@sid, @cid, '2024-2025', 1, 'Enrolled')", conn);
                    insertEnrollCmd.Parameters.AddWithValue("@sid", studentId);
                    insertEnrollCmd.Parameters.AddWithValue("@cid", courseId);
                    insertEnrollCmd.ExecuteNonQuery();
                    enrollmentId = (int)conn.LastInsertRowId;
                }
                else
                {
                    enrollmentId = Convert.ToInt32(enrollResult);
                }

                // Calculate total and percentage based on credit hours
                double total = s1 + s2 + final;
                double maxScore = creditHours * 50.0;  // Each hour = 50 points
                double percent = maxScore > 0 ? (total / maxScore) * 100 : 0;
                string letterGrade = GetLetterGrade(percent);


                // Check if grade exists
                var checkCmd = new SQLiteCommand("SELECT grade_id FROM grades WHERE enrollment_id = @eid", conn);
                checkCmd.Parameters.AddWithValue("@eid", enrollmentId);
                var existingId = checkCmd.ExecuteScalar();

                if (existingId != null)
                {
                    // Update existing grade
                    var updateCmd = new SQLiteCommand(@"
                        UPDATE grades SET s1 = @s1, s2 = @s2, final_exam = @final, total = @total, letter_grade = @grade
                        WHERE grade_id = @id", conn);
                    updateCmd.Parameters.AddWithValue("@s1", s1);
                    updateCmd.Parameters.AddWithValue("@s2", s2);
                    updateCmd.Parameters.AddWithValue("@final", final);
                    updateCmd.Parameters.AddWithValue("@total", total);
                    updateCmd.Parameters.AddWithValue("@grade", letterGrade);
                    updateCmd.Parameters.AddWithValue("@id", existingId);
                    return updateCmd.ExecuteNonQuery() > 0;
                }
                else
                {
                    // Insert new grade
                    var insertCmd = new SQLiteCommand(@"
                        INSERT INTO grades (enrollment_id, s1, s2, final_exam, total, letter_grade)
                        VALUES (@eid, @s1, @s2, @final, @total, @grade)", conn);
                    insertCmd.Parameters.AddWithValue("@eid", enrollmentId);
                    insertCmd.Parameters.AddWithValue("@s1", s1);
                    insertCmd.Parameters.AddWithValue("@s2", s2);
                    insertCmd.Parameters.AddWithValue("@final", final);
                    insertCmd.Parameters.AddWithValue("@total", total);
                    insertCmd.Parameters.AddWithValue("@grade", letterGrade);
                    return insertCmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex) 
            { 
                System.Windows.Forms.MessageBox.Show($"SaveGrade Error: {ex.Message}", "Debug Error");
                return false; 
            }
        }

        private string GetLetterGrade(double score)
        {
            return score switch
            {
                >= 85 => "Excellent",
                >= 75 => "Very Good",
                >= 65 => "Good",
                >= 50 => "Pass",
                _ => "Fail"
            };
        }

        public List<(string StudentName, string CourseName, double S1, double S2, double Final, double Total, string Grade)> GetAllGradesForDisplay()
        {
            var list = new List<(string, string, double, double, double, double, string)>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT s.name, c.course_name, 
                           COALESCE(g.s1, 0), COALESCE(g.s2, 0), COALESCE(g.final_exam, 0),
                           COALESCE(g.total, 0), COALESCE(g.letter_grade, '')
                    FROM grades g
                    JOIN enrollments e ON g.enrollment_id = e.enrollment_id
                    JOIN students s ON e.student_id = s.id
                    JOIN courses c ON e.course_id = c.course_id
                    ORDER BY s.name, c.course_name", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add((
                        reader.IsDBNull(0) ? "" : reader.GetString(0),
                        reader.IsDBNull(1) ? "" : reader.GetString(1),
                        reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                        reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                        reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                        reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                        reader.IsDBNull(6) ? "" : reader.GetString(6)
                    ));
                }
            }
            catch { }
            return list;
        }

        // ==================== PAYMENTS (نظام المدفوعات) ====================

        // Get tuition fee based on student level
        public double GetTuitionFee(int level)
        {
            // Year 1 & 2: 15,000 EGP, Year 3 & 4: 20,000 EGP
            return level <= 2 ? 15000.0 : 20000.0;
        }

        // Get student payments
        public List<(int Id, double Amount, string PaymentDate, string PaymentType, string Status, string Description)> GetStudentPayments(int studentId)
        {
            var list = new List<(int, double, string, string, string, string)>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT id, amount, payment_date, payment_type, status, description
                    FROM payments WHERE student_id = @sid
                    ORDER BY payment_date DESC", conn);
                cmd.Parameters.AddWithValue("@sid", studentId);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add((
                        reader.GetInt32(0),
                        reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                        reader.IsDBNull(2) ? "" : reader.GetString(2),
                        reader.IsDBNull(3) ? "" : reader.GetString(3),
                        reader.IsDBNull(4) ? "" : reader.GetString(4),
                        reader.IsDBNull(5) ? "" : reader.GetString(5)
                    ));
                }
            }
            catch { }
            return list;
        }

        // Get total paid amount
        public double GetTotalPaid(int studentId)
        {
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT COALESCE(SUM(amount), 0) FROM payments 
                    WHERE student_id = @sid AND status = 'paid'", conn);
                cmd.Parameters.AddWithValue("@sid", studentId);
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDouble(result) : 0;
            }
            catch { return 0; }
        }

        // Add a new payment
        public bool AddPayment(int studentId, double amount, string paymentType, string description)
        {
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    INSERT INTO payments (student_id, amount, payment_type, description, status, academic_year, semester)
                    VALUES (@sid, @amount, @type, @desc, 'paid', '2024-2025', 1)", conn);
                cmd.Parameters.AddWithValue("@sid", studentId);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@type", paymentType);
                cmd.Parameters.AddWithValue("@desc", description);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch { return false; }
        }

        // Get payment summary for a student
        public (double TotalFee, double TotalPaid, double Remaining) GetPaymentSummary(int studentId, int level)
        {
            double fee = GetTuitionFee(level);
            double paid = GetTotalPaid(studentId);
            return (fee, paid, fee - paid);
        }

        // ==================== ATTENDANCE ====================

        public bool MarkAttendance(int studentId, string courseCode, string date, string status)
        {
            using var conn = GetConnection();
            try
            {
                // Get course_id from course_code
                var getCourseCmd = new SQLiteCommand(
                    "SELECT course_id FROM courses WHERE course_code = @code", conn);
                getCourseCmd.Parameters.AddWithValue("@code", courseCode);
                var result = getCourseCmd.ExecuteScalar();
                if (result == null) return false;
                int courseId = Convert.ToInt32(result);

                // Check if attendance record exists for this date
                var checkCmd = new SQLiteCommand(@"
                    SELECT id FROM attendance 
                    WHERE student_id = @sid AND course_id = @cid AND date = @date", conn);
                checkCmd.Parameters.AddWithValue("@sid", studentId);
                checkCmd.Parameters.AddWithValue("@cid", courseId);
                checkCmd.Parameters.AddWithValue("@date", date);
                var existingId = checkCmd.ExecuteScalar();

                if (existingId != null)
                {
                    // Update existing
                    var updateCmd = new SQLiteCommand(@"
                        UPDATE attendance SET status = @status WHERE id = @id", conn);
                    updateCmd.Parameters.AddWithValue("@status", status);
                    updateCmd.Parameters.AddWithValue("@id", existingId);
                    return updateCmd.ExecuteNonQuery() > 0;
                }
                else
                {
                    // Insert new
                    var insertCmd = new SQLiteCommand(@"
                        INSERT INTO attendance (student_id, course_id, date, status)
                        VALUES (@sid, @cid, @date, @status)", conn);
                    insertCmd.Parameters.AddWithValue("@sid", studentId);
                    insertCmd.Parameters.AddWithValue("@cid", courseId);
                    insertCmd.Parameters.AddWithValue("@date", date);
                    insertCmd.Parameters.AddWithValue("@status", status);
                    return insertCmd.ExecuteNonQuery() > 0;
                }
            }
            catch { return false; }
        }

        // ==================== FACULTIES ====================

        public List<Faculty> GetAllFaculties()
        {
            var faculties = new List<Faculty>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand("SELECT id, name, name_ar, code FROM faculties ORDER BY id", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    faculties.Add(new Faculty
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        NameAr = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Code = reader.IsDBNull(3) ? "" : reader.GetString(3)
                    });
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error loading faculties: {ex.Message}", "Debug");
            }
            return faculties;
        }

        // ==================== DEPARTMENTS ====================

        public List<Department> GetAllDepartments()
        {
            var departments = new List<Department>();
            using var conn = GetConnection();
            try
            {
                var cmd = new SQLiteCommand(@"
                    SELECT d.*, f.name as faculty_name 
                    FROM departments d 
                    LEFT JOIN faculties f ON d.faculty_id = f.id 
                    ORDER BY d.faculty_id, d.id", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    departments.Add(new Department
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        NameAr = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Code = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        FacultyId = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                        FacultyName = reader.IsDBNull(reader.GetOrdinal("faculty_name")) ? "" : reader.GetString(reader.GetOrdinal("faculty_name"))
                    });
                }
            }
            catch { }
            return departments;
        }
    }

    // Additional model for schedule
    public class ScheduleItem
    {
        public int Id { get; set; }
        public string CourseCode { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string InstructorName { get; set; } = "";
        public string DayOfWeek { get; set; } = "";
        public string StartTime { get; set; } = "";
        public string EndTime { get; set; } = "";
        public string Room { get; set; } = "";
        public string ClassType { get; set; } = "";
    }

    // Instructor model
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Title { get; set; } = "";
        public string Specialization { get; set; } = "";
    }

    // Classroom model
    public class Classroom
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Building { get; set; } = "";
        public int Capacity { get; set; }
        public string RoomType { get; set; } = "Hall";
    }

    // Faculty (College) model
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string NameAr { get; set; } = "";
        public string Code { get; set; } = "";
    }

    // Department model
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string NameAr { get; set; } = "";
        public string Code { get; set; } = "";
        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = "";
    }
}
