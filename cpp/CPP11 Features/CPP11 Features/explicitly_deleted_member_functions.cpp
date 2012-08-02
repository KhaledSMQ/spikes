#include "globals.hpp"

#if EXPLICITLY_DELETED_MEMBER_FUNCTIONS > 0

struct NonCopyable
{
    NonCopyable & operator=(const NonCopyable&) = delete;
    NonCopyable(const NonCopyable&) = delete;
    NonCopyable() = default;
};

void use_explicitly_deleted_member_functions()
{
    NonCopyable n;
    if (&n) {}
}

#endif