#include "globals.hpp"

#if LAMBDA_FUNCTIONS > 0

#include <algorithm>

void use_lambda_functions()
{
    int my_array[5] = {1,2,3,4,5};
    std::for_each(my_array,my_array+5,
    [](int var) { return 2*var; });
}

#endif
