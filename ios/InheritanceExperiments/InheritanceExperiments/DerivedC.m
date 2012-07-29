//
//  DerivedC.m
//  GeneralExperiments
//
//  Created by Paulo Mouat on 10/3/10.
//  Copyright 2010 __MyCompanyName__. All rights reserved.
//

#import "DerivedC.h"


@implementation DerivedC

-(int) getCalculatedNumber
{
    return number * 3;
}

-(int) methodThatExistsOnlyInDerivedC
{
    return number * 2.5;
}

@end
