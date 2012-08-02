#include "globals.hpp"

#if SIZEOF_ON_MEMBER_OBJECTS > 0

#include <memory>

struct SomeType
{
    float member;
};

void use_sizeof_on_member_objects()
{
    size_t b = sizeof(SomeType::member);
    
    if (&b) {}
}

#endif
