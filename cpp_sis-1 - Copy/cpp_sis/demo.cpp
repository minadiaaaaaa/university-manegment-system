/*
 * =====================================================
 * University SIS - نظام إدارة الطلاب
 * =====================================================
 * يوضح: Struct, Class, Array, Vector, Pointer, File I/O
 * =====================================================
 */

#include <iostream>
#include <fstream>
#include <vector>
#include <string>

using namespace std;

// ===== Structure for Student =====
struct Student {
    int id;
    string name;
    string department;
    float gpa;
};

// ===== Class StudentManager =====
class StudentManager {
private:
    vector<Student> students;    // vector
    Student arr[100];            // array
    int count = 0;

public:
    void addStudent();
    void displayStudents();
    void saveToFile();
    void loadFromFile();
};

// ===== Functions Implementation =====

void StudentManager::addStudent() {
    Student s;
    cout << "Enter ID: ";
    cin >> s.id;
    cin.ignore();
    cout << "Enter Name: ";
    getline(cin, s.name);
    cout << "Enter Department: ";
    getline(cin, s.department);
    cout << "Enter GPA: ";
    cin >> s.gpa;
    
    students.push_back(s);    // vector
    arr[count++] = s;         // array
    
    cout << "Student added successfully!\n";
}

void StudentManager::displayStudents() {
    cout << "\n--- Students List ---\n";
    
    for (int i = 0; i < count; i++) {
        Student* ptr = &arr[i];     // pointer
        cout << "ID: " << ptr->id
             << ", Name: " << ptr->name
             << ", Dept: " << ptr->department
             << ", GPA: " << ptr->gpa << endl;
    }
}

void StudentManager::saveToFile() {
    ofstream file("students.txt");
    for (auto s : students) {
        file << s.id << "," << s.name << ","
             << s.department << "," << s.gpa << endl;
    }
    file.close();
    cout << "Data saved successfully!\n";
}

void StudentManager::loadFromFile() {
    ifstream file("students.txt");
    if (!file) {
        cout << "File not found!\n";
        return;
    }
    
    students.clear();
    count = 0;
    
    Student s;
    while (file >> s.id) {
        file.ignore();
        getline(file, s.name, ',');
        getline(file, s.department, ',');
        file >> s.gpa;
        
        students.push_back(s);
        arr[count++] = s;
    }
    
    file.close();
    cout << "Data loaded successfully!\n";
}

// ===== Main =====
int main() {
    StudentManager manager;
    int choice;
    
    do {
        cout << "\n--- University SIS ---\n";
        cout << "1. Add Student\n";
        cout << "2. Display Students\n";
        cout << "3. Save to File\n";
        cout << "4. Load from File\n";
        cout << "5. Exit\n";
        cout << "Choice: ";
        cin >> choice;
        
        switch (choice) {
            case 1: manager.addStudent(); break;
            case 2: manager.displayStudents(); break;
            case 3: manager.saveToFile(); break;
            case 4: manager.loadFromFile(); break;
            case 5: cout << "Exiting...\n"; break;
            default: cout << "Invalid choice!\n";
        }
    } while (choice != 5);
    
    return 0;
}
