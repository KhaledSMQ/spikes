#include "globals.hpp"

#if TYPE_TRAITS_METAPROGRAMMING > 0

#include <type_traits>
// First way of operating.
template< bool B > struct Algorithm {
    template<class T1, class T2> static int do_it (T1 &a, T2 &b)  { return a+b; }
};
 
// Second way of operating.
template<> struct Algorithm<true> {
    template<class T1, class T2> static int do_it (T1 a, T2 b)  { return a+b; }
};
 
// Instantiating 'elaborate' will automatically instantiate the correct way to operate.
template<class T1, class T2>
int elaborate (T1 A, T2 B)
{
    // Use the second way only if 'T1' is an integer and if 'T2' is
    // in floating point, otherwise use the first way.
    return Algorithm<std::is_integral<T1>::value && std::is_floating_point<T2>::value>::do_it( A, B ) ;
}

#endif