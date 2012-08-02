#include "globals.hpp"

#if SHARED_PTR > 0

#include <memory>

void use_shared_ptr()
{
    std::shared_ptr<int> p(new int[3]);
}

#endif