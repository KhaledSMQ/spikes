//
//  BaseA.h
//  GeneralExperiments
//
//  Created by Paulo Mouat on 10/3/10.
//  Copyright 2010 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>


@interface BaseA : NSObject
{
    int number;
}

@property int number;

-(id) initWithNumber: (int)aNumber;
-(int) getNumber;
-(int) getCalculatedNumber;
-(void) add: (int)aNumber;

@end
