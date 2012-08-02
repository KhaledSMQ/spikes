#include "globals.hpp"

#if TYPE_INFERENCE_AUTO > 0

void use_type_inference_auto()
{
    auto variable = 5;
    
    if (&variable) {}
}

#endif