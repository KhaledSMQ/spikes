//
//  BaseA.m
//  GeneralExperiments
//
//  Created by Paulo Mouat on 10/3/10.
//  Copyright 2010 __MyCompanyName__. All rights reserved.
//

#import "BaseA.h"

@implementation BaseA

@synthesize number;

-(id) initWithNumber: (int)aNumber
{
    self = [super init];
    if (!self)
        return nil;
    self.number = aNumber; 
    return self;
}

-(int) getNumber
{
    return number;
}

-(int) getCalculatedNumber
{
    return number * 2;
}

-(void) add:(int)aNumber
{
    number += aNumber;
}

@end
