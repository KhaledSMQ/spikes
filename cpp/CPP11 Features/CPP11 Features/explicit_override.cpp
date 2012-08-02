#include "globals.hpp"

#if EXPLICIT_OVERRIDE > 0

struct Base
{
    float val_;
    virtual void some_func(float val) { val_=val* 2; }
};
 
struct Derived : Base
{
    virtual void some_func(float val) override { val_=val * 4; }
};

void use_explicit_override()
{
    Derived d;
    d.some_func(3);
}

#endif