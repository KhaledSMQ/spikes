#include "globals.hpp"

#if ALTERNATIVE_FUNCTION_SYNTAX > 0

template<class Lhs, class Rhs>
  auto adding_func(const Lhs &lhs, const Rhs &rhs) -> decltype(lhs+rhs) {return lhs + rhs;}
struct SomeStruct  {
    auto func_name(int x, int y) -> int;
};
 
auto SomeStruct::func_name(int x, int y) -> int {
    return x + y;
}

void use_alternative_function_syntax()
{
    double res = adding_func(3.0,4.7);
    SomeStruct s;
    double other = s.func_name(3.7,4.2);
    
    if (res > 0 && other > 0) {}
}
    
#endif