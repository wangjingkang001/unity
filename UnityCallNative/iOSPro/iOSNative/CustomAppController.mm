//
//  CustomAppController.m
//  Unity-iPhone
//
//  Created by lufei on 2018/6/4.
//

#import "CustomAppController.h"

#import "CustomSDKFuncsID.h"

// 该宏可自动挂载该类为APPDelegate的执行类，无需设置main.mm（必须放在实现之前）
IMPL_APP_CONTROLLER_SUBCLASS(CustomAppController)

@interface CustomAppController ()

@property(nonatomic,strong) CustomSDKFuncs *target;

@end

@implementation CustomAppController

- (UIInterfaceOrientationMask)application:(UIApplication *)application supportedInterfaceOrientationsForWindow:(UIWindow *)window {
    [super application:application supportedInterfaceOrientationsForWindow:window];
    _target = [CustomSDKFuncsID getSDKFuncs];
    [_target setUIViewController:self.rootViewController];
    return [_target application:application supportedInterfaceOrientationsForWindow:window];
}

- (void)application:(UIApplication*)application didReceiveLocalNotification:(UILocalNotification*)notification {
    [super application:application didReceiveLocalNotification:notification];
    [_target application:application didReceiveLocalNotification:notification];
}

- (void)application:(UIApplication*)application didReceiveRemoteNotification:(NSDictionary*)userInfo {
    [super application:application didReceiveRemoteNotification:userInfo];
    [_target application:application didReceiveRemoteNotification:userInfo];
}

- (void)application:(UIApplication*)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData*)deviceToken {
    [super application:application didRegisterForRemoteNotificationsWithDeviceToken:deviceToken];
    [_target application:application didRegisterForRemoteNotificationsWithDeviceToken:deviceToken];
}

- (void)application:(UIApplication*)application didFailToRegisterForRemoteNotificationsWithError:(NSError*)error {
    [super application:application didFailToRegisterForRemoteNotificationsWithError:error];
    [_target application:application didFailToRegisterForRemoteNotificationsWithError:error];
}

- (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation {
    [super application:application openURL:url sourceApplication:sourceApplication annotation:annotation];
    return [_target application:application openURL:url sourceApplication:sourceApplication annotation:annotation];
}

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    [super application:application didFinishLaunchingWithOptions:launchOptions];
    return [_target application:application didFinishLaunchingWithOptions:launchOptions];
}

- (void)applicationDidEnterBackground:(UIApplication*)application {
    [super applicationDidEnterBackground:application];
    [_target applicationDidEnterBackground:application];
}

- (void)applicationWillEnterForeground:(UIApplication*)application {
    [super applicationWillEnterForeground:application];
    [_target applicationWillEnterForeground:application];
}

- (void)applicationDidBecomeActive:(UIApplication*)application {
    [super applicationDidBecomeActive:application];
    [_target applicationDidBecomeActive:application];
}

- (void)applicationWillResignActive:(UIApplication *)application {
    [super applicationWillResignActive:application];
    [_target applicationWillResignActive:application];
}

- (void)applicationWillTerminate:(UIApplication*)application {
    [super applicationWillTerminate:application];
    [_target applicationWillTerminate:application];
}

- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url {
    [super application:application handleOpenURL:url];
    return [_target application:application handleOpenURL:url];
}

- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url options:(NSDictionary<NSString *,id> *)options {
    [super application:application openURL:url options:options];
    return [_target application:application openURL:url options:options];
}

#if __IPHONE_OS_VERSION_MAX_ALLOWED <= __IPHONE_8_0
- (void)application:(UIApplication *)application didRegisterUserNotificationSettings:(UIUserNotificationSettings *)notificationSettings {
    [super application:application didRegisterUserNotificationSettings:notificationSettings];
    [_target application:application didRegisterUserNotificationSettings:notificationSettings];
}
#endif
@end

