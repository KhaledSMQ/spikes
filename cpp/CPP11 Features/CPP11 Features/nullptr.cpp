#include "globals.hpp"

#if NULLPTR > 0

void foo(char* howdy) { if (howdy) howdy[0]='c'; }

void use_null_ptr()
{
    char *pc = nullptr;
    int *ic = nullptr;
    bool b = nullptr;
    foo(nullptr);
    
    if (&pc && &ic && b) {}
}

#endif