#include "globals.hpp"

#if EXPLICITLY_DEFAULTED_SPECIAL_MEMBER_FUNCTIONS > 0

struct SomeType
{
    int val_;
    SomeType() = default; //The default constructor is explicitly stated.
    SomeType(int value) : val_(value){};
};

void use_explicitly_defaulted_special_member_functions()
{
    SomeType s;
    
    if (&s) {}
}

#endif