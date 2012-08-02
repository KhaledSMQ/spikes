#include "globals.hpp"

#if STRONGLY_TYPED_ENUM > 0

enum class Enum2 : unsigned int {Val1, Val2};
enum class Enum3 : unsigned long {Val1=17, Val2};
    
#endif