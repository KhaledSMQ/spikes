#include "globals.hpp"

#if WRAPPER_REFERENCE > 0

#include <iostream>
#include <functional>
// This function will obtain a reference to the parameter 'r' and increment it.
void f (int &r)  { r++; }
 
// Template function.
template<class F, class P> void g (F f, P t)  { f(t); }
 
void use_wrapper_reference()
{
    int i = 0 ;
    g (f, i) ;  // 'g<void (int &r), int>' is instantiated
               // then 'i' will not be modified.
    std::cout << i << std::endl;  // Output -> 0
 
    g (f, std::ref(i));  // 'g<void(int &r),reference_wrapper<int>>' is instantiated
                    // then 'i' will be modified.
    std::cout << i << std::endl;  // Output -> 1
}

#endif