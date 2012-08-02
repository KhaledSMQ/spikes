#include "globals.hpp"

#if NEW_STRING_LITERALS > 0

void use_new_string_literals()
{
    auto s0 = u8"I'm a UTF-8 string.";
    auto s1 = u"A UTF-16 string";
    auto s2 = U"This is a UTF-32 string";
    auto s3 = R"(The raw string)";

    if (s0 && s1 && s2 && s3){}
}

#endif