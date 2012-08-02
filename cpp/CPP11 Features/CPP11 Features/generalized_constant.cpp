#include "globals.hpp"

#if GENERALIZED_CONSTANT > 0

#include <vector>

constexpr int get_five() { return 5; }

void use_generalized_constant()
{
    int some_value[get_five()+7];
    
    if (&some_value) {}
}

#endif