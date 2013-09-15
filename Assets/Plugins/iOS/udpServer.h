//
//  udpServer.h
//  udpServer
//
//  Created by Siroberto Scerbo on 4/19/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "GCDAsyncUdpSocket.h"

@interface udpServer : NSObject {
    int udpPort;
    NSString *msg;
    
    BOOL isRunning;
    GCDAsyncUdpSocket *udpSocket;
}

- (void) setPort: (int) port;
- (int) startStop;
- (NSString*) getMsg;

@end
