#include "globals.hpp"

#if TYPE_INFERENCE_DECLTYPE > 0

void use_type_inference_decltype()
{
    int some_int;
    decltype(some_int) other_int = 5;
    
    if (&some_int && &other_int){}
}

#endif