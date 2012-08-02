#include "globals.hpp"

#if USER_DEFINED_LITERALS > 0

#include <string>

struct Sequence
{
    std::string blah_;
    Sequence(const char* literal_string) : blah(literal_string) {}
};

Sequence operator "" seq(const char* literal) { return Sequence(literal); }

void use_user_defined_literals(int argc, char* argv[])
{
    auto my_seq = ACGT_seq;
}

#endif