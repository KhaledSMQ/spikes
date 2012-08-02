#include "globals.hpp"

#if INITIALIZER_LISTS > 0

#include <vector>

struct Object
{
    float first;
    int second;
};

void use_initializer_lists() {
    Object working;
    working.first=0.45f; working.second=7;
    Object scalar = { 0.43f, 10 };
    Object blah[] {{ 1.0f, 10 },{43.2f,29}};
    
    if (&scalar && &blah){}
}

#endif