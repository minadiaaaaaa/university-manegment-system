#ifndef STUDENTMENU_H
#define STUDENTMENU_H

#include "DatabaseManager.h"
#include "Student.h"

class StudentMenu {
public:
    static void show(DatabaseManager& db, Student* student);
};

#endif
