#import <Foundation/Foundation.h>
#import "BaseA.h"
#import "DerivedC.h"

void SimpleClassInteraction()
{
    NSLog(@"SimpleClassInteraction()");
    
    BaseA *baseA = [[BaseA alloc] initWithNumber:10];
    
    NSLog(@"The number is %i", [baseA getNumber]);
    
    [baseA add: 5];
    NSLog(@"The number is %i", [baseA getNumber]);
}

void DerivedClassInteraction()
{
    NSLog(@"DerivedClassInteraction()");
    
    // call method in base class to initialize instance
    DerivedC *instance = [[DerivedC alloc] initWithNumber:20];
    
    NSLog(@"The number is %i", [instance getNumber]);
    
    [instance add: 5];
    NSLog(@"The number is %i", [instance getNumber]);
}

void DerivedClassInteractionThroughBase()
{
    NSLog(@"DerivedClassInteractionThroughBase()");
    
    BaseA *instance = [[BaseA alloc] initWithNumber:30];
    NSLog(@"The number is %i", [instance getCalculatedNumber]);
    
    instance = [[DerivedC alloc] initWithNumber:40];
    // call getCalculatedNumber polymorphically
    NSLog(@"The number is %i", [instance getCalculatedNumber]);
    
    // the below generates a compile-time warning but behaves correctly at runtime
    // Note: Not anymore (XCode 4.4)
    //NSLog(@"The number is %i", [instance methodThatExistsOnlyInDerivedC]);
    
    // the below avoids the warning, obviously
    NSLog(@"The number is %i", [(DerivedC*)instance methodThatExistsOnlyInDerivedC]);
}

void GenericInteractionThroughId()
{
    NSLog(@"GenericInteractionThroughId()");
    
    id instance = [[BaseA alloc] initWithNumber:30];
    NSLog(@"The number is %i", [instance getCalculatedNumber]);
    
    instance = [[DerivedC alloc] initWithNumber:40];
    // call getCalculatedNumber polymorphically
    NSLog(@"The number is %i", [instance getCalculatedNumber]);
    
    // call a method that exists only in DerivedC
    NSLog(@"The number is %i", [instance methodThatExistsOnlyInDerivedC]);
}

int main (int argc, const char * argv[])
{
    @autoreleasepool {
    
        SimpleClassInteraction();
        DerivedClassInteraction();
        DerivedClassInteractionThroughBase();
        GenericInteractionThroughId();
    
    }
    
    return 0;
}
