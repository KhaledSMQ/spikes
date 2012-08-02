#include "globals.hpp"

#if OBJECT_CONSTRUCTION_IMPROVEMENT_USING_BASE_CONSTRUCTOR > 0

class BaseClass
{
    int val_;
public:
    BaseClass(int value) : val_(value) {}
};
 
class DerivedClass : public BaseClass
{
public:
    using BaseClass::BaseClass;
};

void use_object_construction_improvement_using_base_constructor()
{
    DerivedClass d(3);
}

#endif