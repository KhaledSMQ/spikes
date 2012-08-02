#include "globals.hpp"

#if RANGE_BASED_FOR_LOOP > 0

void use_range_based_for_loop()
{
    int my_array[5] = {1,2,3,4,5};
    for (int&x : my_array)
    {
        x *= 2;
    }
}

#endif
