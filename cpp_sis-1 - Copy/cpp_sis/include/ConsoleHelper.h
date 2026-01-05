#ifndef CONSOLEHELPER_H
#define CONSOLEHELPER_H

#include <iostream>
#include <string>

class ConsoleHelper {
public:
    static void clearInput() {
        std::cin.clear();
        std::cin.ignore(10000, '\n'); 
    }
};

#endif
