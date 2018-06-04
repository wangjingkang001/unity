//
//  CustomPlatformInteraction.m
//  Unity-iPhone
//
//  Created by lufei on 2018/6/4.
//

#import "CustomPlatformInteraction.h"

static NSString *gameObjectName;
static NSString *callbackName;

@implementation CustomPlatformInteraction

#pragma mark - Unity调iOS
extern "C" const char * unityCall(int code,const char *jsonString) {
    NSString *msg = @"";
    if (jsonParam != NULL) {
        msg = [[NSString alloc] initWithCString:jsonParam encoding:NSUTF8StringEncoding];
        NSLog(@"###UnityCall：%d--%@",code,msg);
    }

    NSString *returnVal = @"";
    NSDictionary *resultDict = kuyou_nilDict;
    switch (code) {
        case U_INIT:
            resultDict = [[KYBasePlatformID getSDKFuncs] pressSVIP:model];
            break;
        default:
            break;
    }
    
    if ([resultDict count]) {
        returnVal = [[NSString alloc] initWithData:[NSJSONSerialization dataWithJSONObject:resultDict options:NSJSONWritingPrettyPrinted error:nil] encoding:NSUTF8StringEncoding];
    }
    return strdup([returnVal UTF8String]);
}

#pragma mark - iOS调Unity
+ (void)callback:(int)status {
    [self callback:status vals:@""];
}

+ (void)callback:(int)status vals:(NSString *)vals {
    
    if (([gameObjectName length] > 0) && ([callbackName length] > 0)) {
        //        NSString *params = [NSString stringWithFormat:@"{\"code\":\"%d\",\"vals\":\"%@\"}",status,vals];
        //        params = [params stringByReplacingOccurrencesOfString:@"\"" withString:@"\\\""];
        //        NSString * command = [NSString stringWithFormat:@"%@(\"%@\")", callbackID, params];
        //
        //    #warning application周期方法第一次执行完毕后，才可以以下调用该方法
        //        // 注：SendMessage: object Button not found! 可以尝试一下其余模块
        //        UnitySendMessage("Button", "SdkCallback", [command UTF8String]);
        NSString *code = [NSString stringWithFormat:@"%d",status];
        //        NSString *codeStr = [NSString stringWithFormat:@"%@",code];
        NSDictionary *resultDict = @{@"code":code,@"value":vals};
        //        if ([code isEqualToString:@"11002"]) {
        //            resultDict = @{@"code":code,@"vals":vals};
        //        }
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:resultDict options:NSJSONWritingPrettyPrinted error:nil];
        NSString *jsonStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        jsonStr = [jsonStr stringByReplacingOccurrencesOfString:@" " withString:@""];
        NSLog(@"###iOS callback:%@",jsonStr);
        UnitySendMessage([gameObjectName UTF8String], [callbackName UTF8String], [jsonStr UTF8String]);
    }
}

#pragma mark - 内部方法
+ (NSArray *)strToList:(NSString *)value {
    if(!value) return NULL;
    return [value componentsSeparatedByString:@"#@$"];
}

+ (NSString *)pkgParamsToStr:(NSArray *)arrs {
    return [arrs componentsJoinedByString:@"#@$"];
}


@end
