#include "globals.hpp"

#if ALIAS_TEMPLATES > 0

template<typename First,typename Second, int third> class SomeType;
template<typename Second>

using typedefname= SomeType<float,Second,5>;

#endif