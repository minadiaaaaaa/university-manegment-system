#ifndef ADMIN_H
#define ADMIN_H

#include <string>

class Admin {
public:
    std::string username;
    std::string password;

    Admin(std::string user, std::string pass) : username(user), password(pass) {}

    std::string toString() const {
        return username + "|" + password;
    }
};

#endif
