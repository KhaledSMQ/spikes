#include "globals.hpp"

#if OBJECT_CONSTRUCTION_CONSTRUCTORS_CALLING_CONSTRUCTORS > 0

class SomeType
{
    int number;
public:
    SomeType(int new_number) : number(new_number) {}
    SomeType() : SomeType(42) {}
};

void use_object_construction_constructors_calling_constructors()
{
    SomeType bare;
    SomeType with_argument(37);
}

#endif