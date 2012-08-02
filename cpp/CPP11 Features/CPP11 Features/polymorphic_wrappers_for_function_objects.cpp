#include "globals.hpp"

#if POLYMORPHIC_WRAPPERS_FOR_FUNCTION_OBJECTS > 0

#include <algorithm>
#include <functional>

void use_polymorphic_wrappers_for_function_objects()
{
    std::function<int (int, int)> func;  // Wrapper creation using
                                     // template class 'function'.
    std::plus<int> add;  // 'plus' is declared as 'template<class T> T plus( T, T ) ;'
                     // then 'add' is type 'int add( int x, int y )'.
    func = add;  // OK - Parameters and return types are the same.
     
    int a = func (1, 2);  // NOTE: if the wrapper 'func' does not refer to any function,
                          // the exception 'std::bad_function_call' is thrown.
     
    std::function<bool (short, short)> func2 ;
    if(!func2)
    { // True because 'func2' has not yet been assigned a function.
        bool adjacent(long x, long y);
        func2 = &adjacent ;  // OK - Parameters and return types are convertible.
     
        struct Test {
            bool operator()(short x, short y);
        };
        Test car;
        func = std::ref(car);  // 'std::ref' is a template function that returns the wrapper
                         // of member function 'operator()' of struct 'car'.
    }
    
    func = func2;  // OK - Parameters and return types are convertible.
    
    if (&a) {}
}

#endif