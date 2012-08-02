#include "globals.hpp"

#if UNIFORM_INITIALIZATION > 0

struct BasicStruct { int x; double y; };
struct AltStruct
{
    AltStruct(int x, double y) : x_{x}, y_{y} {}
private:
    int x_; double y_;
};

void use_uniform_initialization()
{
    BasicStruct var1{5,3.2};
    AltStruct var2{2,4.3};
    
    if (&var1 && &var2) {}
}

#endif