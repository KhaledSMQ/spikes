#include "globals.hpp"

#if STATIC_ASSERT > 0

#include <memory>

void use_static_assert()
{
    static_assert(sizeof(size_t)==sizeof(int*), "pointer and size_t do not match");
}

#endif