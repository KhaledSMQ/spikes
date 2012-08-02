#include "globals.hpp"

#if HASH_TABLES > 0

#include <unordered_set>

void use_hash_tables()
{
    std::unordered_set s;
    
    if (&s) {}
}

#endif