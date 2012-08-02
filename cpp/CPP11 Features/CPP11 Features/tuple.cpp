#include "globals.hpp"

#if TUPLE > 0

#include <tuple>
void use_tuple()
{
    std::tuple<int,double> blah(3,7.5);
}

#endif