阅读说明:
--------------------------------------------------------
------------------------UnityPro------------------------
--------------------------------------------------------

一.简易untiy项目


--------------------------------------------------------
------------------------iOSPro--------------------------
--------------------------------------------------------

一.iOS和unity交互小框架,大部分都是用原生代码实现,减少项目接口时与第三方冲突,可根据需要自行更改为速度快的第三方.

二.iOSNative文件夹为交互类,使用非常方便,直接拖入工程即可.
(示例工程就是文档中Unity工程导出,可以对比unity中代码查看)

三.iOSNative目录详解
1.CustomAppController 入口
2.CustomSDKFuncsID 配置不同Macros,适配iOS多个target项目,不懂的自行百度
3.CustomSDKFuncs 交互全部接口类,写一起方便管理.
4.CustomPlatformInteraction 这里是与交互核心代码
5.Tools 总结绝大部分原生方法
6.ChannelSDK 重写CustomSDKFuncs,实现一个项目多版本管理切换.(iOS的Macros Target)


--------------------------------------------------------
----------------------AndroidPro------------------------
--------------------------------------------------------

一.Android的小框架也很有意思