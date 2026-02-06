<h1 align="center">Pal5 BeautifyRepair Mod</h1>

<p align="center">
<font size="16px">仙剑五美化修复Mod</font><br />
</p>
 
<p align="center">
  <img src="Pal5Mod\UI\Pal5ico.png" align="middle" width = "150"/>
    <br /><br />
    <a href="https://github.com/SaraKale/Pal5_BeautifyRepair_Mod/releases"><img src="https://img.shields.io/github/v/release/SaraKale/Pal5_BeautifyRepair_Mod?color=ffa"></a>
    <a href=""><img src="https://img.shields.io/badge/NET Framework-4.8-aff.svg"></a>
    <a href=""><img src="https://img.shields.io/badge/os-win-blue.svg"></a>
</p>

<p align="center">
language：<a href="README_EN.md">English</a> | <a href="README_TC.md">繁體中文</a> 
</p>
<br />

>【注意】目前还是**测试版**，仍然会有问题，例如闪退、图标空白、剧情不会继续等，玩游戏之前请右键 Pal5.exe 程序“以管理员身份运行”运行游戏尝试。

## 介绍

本MOD是用于增强《仙剑奇侠传五》游戏体验，做了UI界面分辨率扩展、字体修改、调整消息对话框字号、身材比例调整、隐藏战斗UI界面等实用功能，以便于想要重温的玩家给予更好的体验，希望能给你带来帮助。

适用于 **steam/Wegame/方块游戏** 平台，目前游戏下载已经是解压方式了，其他平台是否适用还请您尝试。

![](images/SoftwareScreen.jpg)

## 下载

请选择下面任意节点下载。

|   节点    |                                 链接                                 | 
| :------: | :------------------------------------------: | 
|  Github  | [releases](https://github.com/sarakale/Pal5_BeautifyRepair_Mod/releases) |
|  Gitee   | [releases](https://gitee.com/sarakale/Pal5_BeautifyRepair_Mod/releases)  |
|  123盘  |         [链接](https://www.123865.com/s/xITljv-41WJH?pwd=PAL5#) 密码:PAL5         |
|  迅雷云盘  |         [链接](https://pan.xunlei.com/s/VOklcmByg_M83Uzg_bgZ0noGA1?pwd=63dy#) 密码:63dy         |
|  百度网盘 |         [链接](https://pan.baidu.com/s/1tAwyLE711LKhhaz6fmgDnA?pwd=PAL5) 密码:PAL5         |
|  mediafire |         [链接](https://www.mediafire.com/file/0wsi24e3apvc0z0/Pal5_BeautifyRepair_Mod_Beta_v0.0.1.zip/file)        |

## 运行环境

操作系统要求：Windows 7 SP1 / 8 / 10 / 11

需要有 Microsoft .NET Framework 4.8 运行环境  
下载：https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/net48

由于仙剑五简体版和繁体版都已经不再支持XP和Vista系统，官方要求已变成Win 7/8/10了，但繁体版操作系统仍支持Windows Vista。  
steam在2024年1月1日起，停止对 Win7/8/8.1 系统的支持。在该日期后，Steam客户端将无法在上述版本的Windows系统上运行。其他平台不受影响，具体请根据情况升级系统。

## 编译构建

我的开发环境：  
系统：Windows 10  
环境：[Visual Studio 2022](https://visualstudio.microsoft.com/)  
框架：.NET Framework 4.8  

需要安装Nuget包：  
 - [MaterialDesignThemes](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
 - [WPFLocalizeExtension](https://github.com/XAMLMarkupExtensions/WPFLocalizeExtension)

更多见 `packages.config`包清单安装。  
然后直接运行 `Pal5Mod_BeautifyRepair.sln` 编译即可。

或者其他方式编译，例如**dotnet**编译：
```
dotnet build Pal5Mod_BeautifyRepair.csproj --framework net48
```

## 使用方法

双击运行 `Pal5 BeautifyRepair Mod.exe` 程序即可。

在菜单栏“界面语言/Language”可以切换你喜欢的语言。目前有：简体中文、繁体中文、英语，如需要其他语言请告知我，我会添加上。

## 功能介绍

### UI界面分辨率扩展

此功能会将UI界面扩展至1080P以及2K高分辨率，更方便浏览界面。由于游戏扇形菜单内容范围太小，在高分辨率下观看很不方便，考虑很久还是尝试改成了长方形全展开的形式，这样能看到很多内容，基本上将UI位置重构了，如果不喜欢可以恢复默认界面。
3840x2160（4K）分辨率文字显示不全，推荐方案是选择1920x1080分辨率，使用全屏模式，或者自行调整游戏程序的高DPI设置。  
选择一种分辨率后游戏界面就会一直是新的界面了，无需再重新应用。如果要切换其他分辨率，需要重新选择调整，否则会有UI错位。  

支持分辨率：
- 1024 X 768（4:3）
- 1280 X 720（16:9）
- 1280 X 800（16:10）
- 1280 X 1024（5:4）
- 1366 X 768（16:9）
- 1440 X 900（16:10）
- 1600 X 900（16:9）
- 1600 X 1200（4:3）
- 1680 X 1050（16:10）
- 1920 X 1080（16:9）
- 2560 X 1440（16:9）

4:3 UI界面截图：
![](images/UI-1280-1.jpg)
![](images/UI-1280-2.jpg)

16:9 UI界面截图：
![](images/UI-1440-1.jpg)
![](images/UI-1440-2.jpg)

### 字体修改

原字体“华康唐风隶”不便于阅读，找了类似粗体的字体，经过挑选觉得“寒蝉正楷体”或“霞鹜文楷”是最合适的，也可以选择系统已经安装的字体，或者选择外部字体，无论哪个方式都可以选择你喜欢的字体。  
有些字体不受支持，推荐选择 `.ttf` 字体文件，如发现游戏内没有显示文字或显示不全，更换别的字体即可。  
免费字体下载：https://www.maoken.com/all-fonts-imgs

寒蝉正楷体
![](images/Font-ChillKai.jpg)

霞鹜文楷
![](images/Font-xiawu.jpg)

华康唐风隶（默认字体）
![](images/Font-default.jpg)

### 调整消息对话框字号

可调整消息对话框的字号大小，主要涉及到NPC对话框、选择框、剧情消息框、存读取游戏/返回界面/退出游戏提示框。有小/中/大/特大号选择，如果遇到错位需要重启游戏或者请再次尝试安装。  
注意要应用这个功能需要先应用**UI界面分辨率扩展**功能再去调整消息框字号，否则会有冲突。  
48号有些文字会被裁剪，推荐使用28、38号。  

小（默认，18号）
![](images/Message-size18.jpg)

中（推荐，28号）
![](images/Message-size28.jpg)

大（38号）
![](images/Message-size38.jpg)

特大号（48号）
![](images/Message-size48.jpg)

### 身材比例调整和肤色美白

此功能会调整四主角的模型身材比例，并且给肤色增白。  
同样也适用于仙五前传DLC《梦华幻斗》四主角的登场。

原版
![](images/characterbody-1.jpg)

调整后
![](images/characterbody-2.jpg)

### 隐藏战斗UI界面

用于隐藏战斗UI界面相关，方便录像。做了这些处理：战斗胜利界面隐藏，如获得经验、获取物品等；状态名称隐藏，伤害数字隐藏。

原版
![](images/HideUI-1.jpg)

隐藏后
![](images/HideUI-2.jpg)
     
### 恢复初始设置

将游戏配置恢复到初始设置，若是前面功能应用失败可以尝试恢复初始设置。
     
	 
## 借物MOD补丁/工具

[仙剑五怀旧版Mod1.06c](https://bbs.3dmgame.com/thread-2854641-1-1.html)  by:情姐姐  
[uvlist修改器 v2.0](https://tieba.baidu.com/p/3680752738?red_tag=3514355147) by:百度贴吧—风靡义磊

感谢前人的贡献我才能在基础上整合，在此感谢！

## 特别感谢

AI代码辅助：ChatGPT  
感谢测试：清风涧碎花弄影、水月澄明

## 许可证

使用 [CC BY-NC 4.0](LICENSE) 许可证
