<h1 align="center">Pal5 BeautifyRepair Mod</h1>

<p align="center">
<font size="10px">仙劍五美化修復Mod</font><br />
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

>【注意】目前還是**測試版**，仍然會有問題，例如閃退、圖標空白、劇情不會繼續等，玩遊戲之前請右鍵 Pal5.exe 程序「以管理員身份運行」運行遊戲嘗試。

## 介紹

本MOD是用於增強《仙劍奇俠傳五》遊戲體驗，做了UI介面解析度擴展、字體修改、調整訊息對話框字號、身材比例調整、隱藏戰鬥UI介面等實用功能，以便於想要重溫的玩家給予更好的體驗，希望能給你帶來幫助。

適用於 **steam/Wegame/方塊遊戲** 平台，目前遊戲下載已經是解壓方式了，其他平台是否適用還請您嘗試。

![](images/SoftwareScreen.jpg)

## 下載

請選擇下面任意節點下載。

|   節點    |                                 鏈接                                 | 
| :------: | :------------------------------------------: | 
|  Github  | [releases](https://github.com/sarakale/Pal5_BeautifyRepair_Mod/releases) |
|  Gitee   | [releases](https://gitee.com/sarakale/Pal5_BeautifyRepair_Mod/releases)  |
|  123盤  |         [鏈接](https://www.123865.com/s/xITljv-41WJH?pwd=PAL5#) 密碼:PAL5         |
|  迅雷雲盤  |         [鏈接](https://pan.xunlei.com/s/VOklcmByg_M83Uzg_bgZ0noGA1?pwd=63dy#) 密碼:63dy         |
|  百度網盤 |         [鏈接](https://pan.baidu.com/s/1tAwyLE711LKhhaz6fmgDnA?pwd=PAL5) 密碼:PAL5         |
|  mediafire |         [鏈接](https://www.mediafire.com/file/0wsi24e3apvc0z0/Pal5_BeautifyRepair_Mod_Beta_v0.0.1.zip/file)        |

## 運行環境

作業系統要求：Windows 7 SP1 / 8 / 10 / 11

需要有 Microsoft .NET Framework 4.8 運行環境  
下載：https://dotnet.microsoft.com/zh-cn/download/dotnet-framework/net48

由於仙劍五簡體版和繁體版都已經不再支援XP和Vista系統，官方要求已變成Win 7/8/10了，但繁體版作業系統仍支援Windows Vista。
steam在2024年1月1日起，停止對 Win7/8/8.1 系統的支援。在該日期後，Steam客戶端將無法在上述版本的Windows系統上運行。其他平台不受影響，具體請根據情況升級系統。

## 編譯構建

我的開發環境：  
系統：Windows 10  
環境：[Visual Studio 2022](https://visualstudio.microsoft.com/)  
框架：.NET Framework 4.8  

需要安裝Nuget包：  
 - [MaterialDesignThemes](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)
 - [WPFLocalizeExtension](https://github.com/XAMLMarkupExtensions/WPFLocalizeExtension)

更多見 `packages.config`包清單安裝。
然後直接運行 `Pal5Mod_BeautifyRepair.sln` 編譯即可。

或者其他方式編譯，例如**dotnet**編譯：
```
dotnet build Pal5Mod_BeautifyRepair.csproj --framework net48
```

## 使用方法

雙擊運行 `Pal5 BeautifyRepair Mod.exe` 程序即可。

在選單列「介面語言/Language」可以切換你喜歡的語言。目前有：簡體中文、繁體中文、英語，如需要其他語言請告知我，我會添加上。

## 功能介紹

### UI介面解析度擴展

此功能會將UI介面擴展至1080P以及2K高解析度，更方便瀏覽介面。由於遊戲扇形選單內容範圍太小，在高解析度下觀看很不方便，考慮很久還是嘗試改成了長方形全展開的形式，這樣能看到很多內容，基本上將UI位置重構了，如果不喜歡可以恢復預設介面。
3840x2160（4K）解析度文字顯示不全，推薦方案是選擇1920x1080解析度，使用全螢幕模式，或者自行調整遊戲程式的高DPI設置。
選擇一種解析度後遊戲介面就會一直是新的介面了，無需再重新應用。如果要切換其他解析度，需要重新選擇調整，否則會有UI錯位。

支援解析度：
1024 X 768（4:3）
1280 X 720（16:9）
1280 X 800（16:10）
1280 X 1024（5:4）
1366 X 768（16:9）
1440 X 900（16:10）
1600 X 900（16:9）
1600 X 1200（4:3）
1680 X 1050（16:10）
1920 X 1080（16:9）
2560 X 1440（16:9）

4:3 UI介面截圖：
![](images/UI-1280-1.jpg)
![](images/UI-1280-2.jpg)

16:9 UI介面截圖：
![](images/UI-1440-1.jpg)
![](images/UI-1440-2.jpg)

### 字體修改

原字體「華康唐風隸」不便於閱讀，找了類似粗體的字體，經過挑選覺得「寒蟬正楷體」或「霞鶩文楷」是最合適的，也可以選擇系統已經安裝的字體，或者選擇外部字體，無論哪個方式都可以選擇你喜歡的字體。
有些字體不受支援，推薦選擇 `.ttf` 字體文件，如發現遊戲內沒有顯示文字或顯示不全，更換別的字體即可。
免費字體下載：https://www.maoken.com/all-fonts-imgs

寒蟬正楷體
![](images/Font-ChillKai.jpg)

霞鶩文楷
![](images/Font-xiawu.jpg)

華康唐風隸（預設字體）
![](images/Font-default.jpg)

### 調整訊息對話框字號

可調整訊息對話框的字號大小，主要涉及到NPC對話框、選擇框、劇情訊息框、存讀取遊戲/返回介面/退出遊戲提示框。有小/中/大/特大號選擇，如果遇到錯位需要重新啟動遊戲或者請再次嘗試安裝。
注意要應用這個功能需要先應用**UI介面解析度擴展**功能再去調整訊息框字號，否則會有衝突。
48號有些文字會被裁剪，推薦使用28、38號。

小（預設，18號）
![](images/Message-size18.jpg)

中（推薦，28號）
![](images/Message-size28.jpg)

大（38號）
![](images/Message-size38.jpg)

特大號（48號）
![](images/Message-size48.jpg)

### 身材比例調整和膚色美白

此功能會調整四主角的模型身材比例，並且給膚色增白。
同樣也適用於仙五前傳DLC《夢華幻鬥》四主角的登場。

原版
![](images/characterbody-1.jpg)

調整後
![](images/characterbody-2.jpg)

### 隱藏戰鬥UI介面

用於隱藏戰鬥UI介面相關，方便錄像。做了這些處理：戰鬥勝利介面隱藏，如獲得經驗、獲取物品等；狀態名稱隱藏，傷害數字隱藏。

原版
![](images/HideUI-1.jpg)

隱藏後
![](images/HideUI-2.jpg)
     
### 恢復初始設置

將遊戲配置恢復到初始設置，若是前面功能應用失敗可以嘗試恢復初始設置。
     
 	 
## 借物MOD補丁/工具

[仙劍五懷舊版Mod1.06c](https://bbs.3dmgame.com/thread-2854641-1-1.html)  by:情姐姐
[uvlist修改器 v2.0](https://tieba.baidu.com/p/3680752738?red_tag=3514355147) by:百度貼吧—風靡義磊

感謝前人的貢獻我才能在基礎上整合，在此感謝！

## 特別感謝

AI代碼輔助：ChatGPT
感謝測試：清風澗碎花弄影、水月澄明

## 許可證

使用 [CC BY-NC 4.0](LICENSE) 許可證
