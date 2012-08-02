#include "globals.hpp"

#if EXPLICIT_FINAL > 0

struct Base2
{
    int blah_;
    virtual void f() final {blah_=7;};
};

struct Base3 : public Base2
{
    virtual void g() { blah_=3;}
};

#endif