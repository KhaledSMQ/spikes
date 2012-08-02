#include "globals.hpp"

#if REGEX > 0

#include <regex>

void use_regex()
{
    std::regex rgx("[\t\n]");
}

#endif