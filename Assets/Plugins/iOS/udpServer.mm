//
//  udpServer.m
//  udpServer
//
//  Created by Siroberto Scerbo on 4/19/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "udpServer.h"

@implementation udpServer

- (void) setPort:(int) port {
    udpPort = port;
    NSLog(@"Port Changed");
}

- (int) startStop {
    if (isRunning)
	{
		// STOP udp echo server
		
		[udpSocket close];
		isRunning = false;
	}
	else
	{
		// START udp echo server
		udpSocket = [[GCDAsyncUdpSocket alloc] initWithDelegate:self delegateQueue:dispatch_get_main_queue()];
        
		int port = udpPort;
		if (port < 0 || port > 65535)
		{
			port = 0;
            return 0;
		}
		
		NSError *error = nil;
		
		if (![udpSocket bindToPort:port error:&error])
		{
			msg = [[NSString alloc] initWithFormat:@"Error starting server (bind): %@", error];
            NSLog(@"Error (bind)");
			return 0;
		}
		if (![udpSocket beginReceiving:&error])
		{
			[udpSocket close];
			
			msg = [[NSString alloc] initWithFormat:@"Error starting server (recv): %@", error];
            NSLog(@"Error (recieve)");
			return 0;
		}
		isRunning = YES;
        NSLog(@"Success");
	}
    return 1;
}

- (void)udpSocket:(GCDAsyncUdpSocket *)sock didReceiveData:(NSData *)data fromAddress:(NSData *)address withFilterContext:(id)filterContext {
	if (!isRunning) return;
	
	msg = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
}

- (NSString*) getMsg {
    NSLog(@"Message Request");
    if (msg == nil) {
        msg = [[NSString alloc] initWithString:@""];
    }
    NSString *returnMsg = [[NSString alloc] initWithString:msg];
    msg = @"";
    return returnMsg;
}

@end

static udpServer* server;

// Helper method to create C string copy
char* MakeStringCopy (const char* string)
{
	if (string == NULL)
		return NULL;
	
	char* res = (char*)malloc(strlen(string) + 1);
	strcpy(res, string);
	return res;
}

extern "C" {
    void _init(int port) {
        server = [udpServer alloc];
        [server setPort:port];
    }
    
    int _startStop() {
        return [server startStop];
    }
    
    const char* _getMsg() {
        return MakeStringCopy([[server getMsg] UTF8String]);
    }
    
}
