/*
 * Professor Menu - قائمة الأستاذ
 * تتيح للأستاذ إدارة المواد والحضور والدرجات
 */
#ifndef PROFESSORMENU_H
#define PROFESSORMENU_H

#include "DatabaseManager.h"
#include "Professor.h"

class ProfessorMenu {
public:
    static void show(DatabaseManager& db, Professor* professor);
};

#endif
