using BespokeFusion;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Security.Cryptography;

namespace 仙剑五美化修复Mod
{
    public partial class MainWindow : Window
    {
        // 选择分辨率值
        private string selectedResolutionValue = "";

        // ==========================
        //  UI界面分辨率扩展
        //
        //  所需文件：
        //  Config\uvlist-1080P\uvlist.tb 到 Config\uvlist.tb
        //  Config\Data\Data-SC\msdata.xml 到 Config\Data\msdata.xml
        //  Texture\UI\Pal5Mod-SC\*.*  到 Texture\UI\Pal5Mod\
        //  UI-SC\UI-1920x1080\*.* 到 UI\
        //
        //	每个分辨率复制到对应的选项
        //	3840x2160分辨率文字显示不全，需调整文字大小。推荐方法是调整程序的高DPI设置，设置为“系统（增强）”，使用分辨率1080P。
        //  或者借助第三方窗口化工具，看看能不能整合一起。
        //	另外也要在游戏目录下的Config\option.ini文件中编辑分辨率值：resolution=19201080
        //
        //  BUG:
        //  游戏运行后人物有不明黑线和抖动问题，是跟抗锯齿有关，打开config.exe设置关闭抗锯齿即可。
        //  这问题出现在高性能显卡情况下，强烈建议关闭游戏自带的抗锯齿设置，会导致卡顿且效果不佳，请用显卡驱动的抗锯齿功能来辅助。
        //  代码新增加一个在option.ini文件修改MultiSample值为1
        //  游戏原exe文件会闪退，只能用去除steam包装的exe来运行。一定要右键”以管理员身份运行“。
        // ==========================

        /*****************************
         
        正确代码逻辑：
        从第二个if开始前面要加else if判断，最后加else结尾才能结束。

        if (Directory.Exists("Pal5Mod_BeautifyRepair"))
        {
            if (UIlist.SelectedIndex == 0)
            {
                // 第一个选项的代码逻辑
            }
            else if (UIlist.SelectedIndex == 1)
            {
                // 第二个选项的代码逻辑
            }
            else
            {
                // 其他选项的代码逻辑
            }
        }
        else
        {
            // 目录不存在时的代码逻辑
        }

        ***************************/

        // ==========================
        // 应用确定按钮
        // Tab_UIdesign_Button
        // ==========================
        private void Tab_UIdesign_Button_Click(object sender, RoutedEventArgs e)
        {
            // 更新全屏/窗口值
            // 在点击确认按钮时更新option.ini文件中的配置
            UpdateFullScreenIniValue(FullScreen.IsChecked == true ? "0" : "1");

            // 更新分辨率值
            UpdateresolutionOption(selectedResolutionValue);

            // 更新抗锯齿值 MultiSample=1
            UpdateMultiSampleValue("1");

            // 检查游戏路径和 Pal5Mod_BeautifyRepair 文件夹
            string Msg_UIresolution_Title = L.Get("Msg_UIresolution_Title");
            if (!CheckGamePath("Msg_UIresolution_Title") || !CheckModResource("Msg_UIresolution_Title"))
                return;

            //-------------------------
            // 1024 X 768（4:3）
            // uvlist.tb要用768P的
            //-------------------------
            if (UIlist.SelectedIndex == 0)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-768P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1024x768";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"),MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-768P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1024x768";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"),MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 1280 X 720（16:9）
                    // uvlist.tb要用768P的
                    //-------------------------
                    else if (UIlist.SelectedIndex == 1)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-768P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1280x720";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-768P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1280x720";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 1280 X 800（16:10）
                    // uvlist.tb要用768P的
                    //-------------------------
                    else if (UIlist.SelectedIndex == 2)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-768P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1280x800";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-768P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1280x800";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 1280 X 1024（5:4）
                    // uvlist.tb要用1080P的
                    //-------------------------
                    else if (UIlist.SelectedIndex == 3)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1280x1024";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1280x1024";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 1366 X 768（16:9）
                    // uvlist.tb要用768P的
                    //-------------------------
                    else if (UIlist.SelectedIndex == 4)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-768P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1366x768";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-768P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1366x768";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 1440 X 900（16:10）
                    // uvlist.tb要用1080P的
                    //-------------------------
                    else if (UIlist.SelectedIndex == 5)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1440x900";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1440x900";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                     }

                    //-------------------------
                    // 1600 X 900（16:9）
                    // uvlist.tb要用1080P的
                    //-------------------------
                    else if (UIlist.SelectedIndex == 6)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1600x900";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1600x900";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 1600 X 1200（4:3）
                    // uvlist.tb要用1080P的
                    //-------------------------
                    else if (UIlist.SelectedIndex == 7)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1600x1200";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                     //-------------------------
                     // 繁体中文单选框
                     //-------------------------
                     else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1600x1200";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 1680 X 1050（16:10）
                    // uvlist.tb要用1080P的
                    //-------------------------
                    else if (UIlist.SelectedIndex == 8)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1680x1050";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1680x1050";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 1920 X 1080（16:9）
                    // uvlist.tb要用1080P的
                    //-------------------------
                    else if (UIlist.SelectedIndex == 9)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1920x1080";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1920x1080";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 2560 X 1440（16:9）
                    // uvlist.tb要用1080P的
                    //-------------------------
                    else if (UIlist.SelectedIndex == 10)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-2560x1440";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-2560x1440";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        // ---------------------------------------

                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    // =====================================

                    // 如果分辨率没有选择
                    else
                    {
                        ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedAllresolution"), MessageBoxImage.Information);
                    }

            // ===========================
        }


        // ==========================
        // 恢复默认按钮
        // Tab_UIdesign_Button2
        // ==========================
        private void Tab_UIdesign_Button2_Click(object sender, RoutedEventArgs e)
        {
            // 更新全屏/窗口值
            // 在点击确认按钮时更新 option.ini 文件中的配置
            UpdateFullScreenIniValue(FullScreen.IsChecked == true ? "0" : "1");

            // 分辨率值
            UpdateresolutionOption(selectedResolutionValue);

            // 更新抗锯齿值 MultiSample=1
            UpdateMultiSampleValue("1");

            // 检查游戏路径和 Pal5Mod_BeautifyRepair 文件夹
            string Msg_UIresolution_Title = L.Get("Msg_UIresolution_Title");
            if (!CheckGamePath("Msg_UIresolution_Title") || !CheckModResource("Msg_UIresolution_Title"))
                return;

            //-------------------------
            // 分辨率下拉表选项 0-10
            //-------------------------
            if (UIlist.SelectedIndex == 0 ||
                UIlist.SelectedIndex == 1 ||
                UIlist.SelectedIndex == 2 ||
                UIlist.SelectedIndex == 3 ||
                UIlist.SelectedIndex == 4 ||
                UIlist.SelectedIndex == 5 ||
                UIlist.SelectedIndex == 6 ||
                UIlist.SelectedIndex == 7 ||
                UIlist.SelectedIndex == 8 ||
                UIlist.SelectedIndex == 9 ||
                UIlist.SelectedIndex == 10)
                    {
                        //-------------------------
                        // 简体中文单选框
                        //-------------------------
                        if (UIdesignlist_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Backup\Config\uvlist.tb";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\Backup\UI-SC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(targetDirectory1);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文单选框
                        //-------------------------
                        else if (UIdesignlist_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Backup\Config\uvlist.tb";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\Backup\UI-TC";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(targetDirectory1);

                            // 复制文件，ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_UIresolution_Success"), MessageBoxImage.Information);
                        }

                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    // =====================================

                    // 如果分辨率没有选择
                    else
                    {
                        ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoSelectedAllresolution"), MessageBoxImage.Information);
                    }

                // ===========================
            }


        // =========================
        //  选择项
        // =========================

        // 简体中文单选项
        private void UIdesignlist_SC_Checked(object sender, RoutedEventArgs e)
        {

        }
        // 繁体中文单选项
        private void UIdesignlist_TC_Checked(object sender, RoutedEventArgs e)
        {

        }

        // ==========================
        // 分辨率设置 UIlist
        // Config\option.ini文件中编辑：isWindow=0
        // ==========================

        // 选取列表中的分辨率
        private void UIlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox comboBox = (System.Windows.Controls.ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            if (selectedItem != null)
            {
                string resolution = selectedItem.Content.ToString();
                selectedResolutionValue = GetResolutionValue(resolution);
            }
        }
        // 根据选项的内容返回相应的分辨率值
        private string GetResolutionValue(string resolution)
        {
            switch (resolution)
            {
                case "1024 X 768（4:3）":
                    return "10240768";
                case "1280 X 720（16:9）":
                    return "12800720";
                case "1280 X 800（16:10）":
                    return "12800800";
                case "1280 X 1024（5:4）":
                    return "12801024";
                case "1366 X 768（16:9）":
                    return "13660768";
                case "1440 X 900（16:10）":
                    return "14400900";
                case "1600 X 900（16:9）":
                    return "16000900";
                case "1600 X 1200（4:3）":
                    return "16001200";
                case "1680 X 1050（16:10）":
                    return "16801050";
                case "1920 X 1080（16:9）":
                    return "19201080";
                case "2560 X 1440（16:9）":
                    return "25601440";
                default:
                    return string.Empty;
            }
        }
        // 更新option.ini内容，并修改resolution值
        private void UpdateresolutionOption(string resolutionValue)
        {
            try
            {
                // 读取option.ini文件的内容
                string[] lines = File.ReadAllLines(Pal5_GamePath.Text + IniFilePath);

                // 更新resolution值
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("resolution="))
                    {
                        lines[i] = "resolution=" + resolutionValue;
                        break;
                    }
                }
                // 将更新后的内容写回option.ini文件
                File.WriteAllLines(Pal5_GamePath.Text + IniFilePath, lines);
                //MessageBox.Show("option.ini文件已更新！");
            }
            catch (IOException)
            {
                //MessageBox.Show("无法更新option.ini文件：" + ex.Message);
            }
        }

        // ==========================
        // 全屏设置 FullScreen
        // Config\option.ini文件中编辑：isWindow=0
        // ==========================
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 在窗口加载时读取 option.ini 文件中的配置
            ReadFullScreenIniValue();
        }

        // 点击全屏
        private void FullScreen_Checked(object sender, RoutedEventArgs e)
        {
            // 如果选择了正确的路径
            if (File.Exists(Pal5_GamePath.Text + "\\Pal5.exe"))
            {
                // 在 CheckBox 状态改变时更新 option.ini 文件中的配置
                UpdateFullScreenIniValue("0");
            }
            else
            {   // 如果路径未选择
                ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                return;
            }
        }

        // 更新全屏配置
        private void FullScreen_Unchecked(object sender, RoutedEventArgs e)
        {
            // 如果选择了正确的路径
            if (File.Exists(Pal5_GamePath.Text + "\\Pal5.exe"))
            {
                // 在 CheckBox 状态改变时更新 option.ini 文件中的配置
                UpdateFullScreenIniValue("1");
            }
            else
            {   // 如果路径未选择
                ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                return;
            }
        }

        // 读取ini文件
        private void ReadFullScreenIniValue()
        {
            // 如果选择了正确的路径
            if (File.Exists(Pal5_GamePath.Text + "\\Pal5.exe"))
            {
                if (!File.Exists(Pal5_GamePath.Text + IniFilePath))
                    return;

                string[] lines = File.ReadAllLines(Pal5_GamePath.Text + IniFilePath, Encoding.UTF8);

                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();

                    if (trimmedLine.StartsWith($"{IsWindowKey}="))
                    {
                        string value = trimmedLine.Substring(IsWindowKey.Length + 1);
                        FullScreen.IsChecked = value == "0";
                        return;
                    }
                }
            }
            // 如果路径未选择
            else
            {
                ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
            }
        }

        /******************************
            当取消点击CheckBox时，将更新isWindow的值为1。
            当选中CheckBox时，将更新isWindow的值为0。
            isWindow=0 全屏
            isWindow=1 窗口
            更新option.ini文件中的isWindow值
        *****************************/
        private void UpdateFullScreenIniValue(string newValue)
        {
            // 如果选择了正确的路径
            if (File.Exists(Pal5_GamePath.Text + "\\Pal5.exe"))
            {
                // BUG：需要在前面加上判断正确路径的代码，否则会找不到文件
                string[] lines = File.ReadAllLines(Pal5_GamePath.Text + IniFilePath, Encoding.UTF8);

                for (int i = 0; i < lines.Length; i++)
                {
                    string trimmedLine = lines[i].Trim();

                    if (trimmedLine.StartsWith($"{IsWindowKey}="))
                    {
                        lines[i] = $"{IsWindowKey}={newValue}";
                        File.WriteAllLines(Pal5_GamePath.Text + IniFilePath, lines, Encoding.UTF8);
                        return;
                    }
                }
                // 如果没有找到 isWindow 的键，则添加一行
                lines[lines.Length - 1] += Environment.NewLine + $"{IsWindowKey}={newValue}";
                File.WriteAllLines(Pal5_GamePath.Text + IniFilePath, lines, Encoding.UTF8);
            }
            else
            {   // 如果路径未选择
                ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                return;
            }
        }


        // ==========================
        // 抗锯齿设置
        // MultiSample=1
        // ==========================

        // 更新option.ini内容，并修改MultiSample值为1
        private void UpdateMultiSampleValue(string newValue)
        {
            try
            {
                // 读取option.ini文件的内容
                string[] lines = File.ReadAllLines(Pal5_GamePath.Text + IniFilePath);

                // 更新MultiSample值
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("MultiSample="))
                    {
                        lines[i] = "MultiSample=" + newValue;
                        break;
                    }
                }

                // 如果没有找到 MultiSample 的键，则添加一行
                if (!lines.Any(line => line.StartsWith("MultiSample=")))
                {
                    Array.Resize(ref lines, lines.Length + 1);
                    lines[lines.Length - 1] = "MultiSample=" + newValue;
                }

                // 将更新后的内容写回option.ini文件
                File.WriteAllLines(Pal5_GamePath.Text + IniFilePath, lines);
                //MessageBox.Show("option.ini文件已更新！");
            }
            catch (IOException)
            {
                //MessageBox.Show("无法更新option.ini文件：" + ex.Message);
            }
        }


        // ---------------------------
        // 运行游戏
        // RunGame
        //
        // bug：给Pal5.exe加了管理员权限后，就无法打开了。还是隐藏了这个控件。
        // ---------------------------
        private void RunGame_Click(object sender, RoutedEventArgs e)
        {
            // 游戏路径
            string exeFilePath = Path.Combine(Pal5_GamePath.Text, "Pal5.exe");

            // 如果选择了正确的路径
            try
            {
                if (File.Exists(Path.Combine(Pal5_GamePath.Text, "Pal5.exe")))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = exeFilePath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                ShowMsg(
                    "UI界面分辨率扩展",
                    "运行游戏时出错：" + ex.Message,
                    MessageBoxImage.Error
                );
            }
        }

        // ---------------------------
        // 运行配置程序
        // RunConfig
        // ---------------------------
        private void RunConfig_Click(object sender, RoutedEventArgs e)
        {

            // 如果选择了正确的路径
            if (File.Exists(Path.Combine(Pal5_GamePath.Text, "Pal5.exe")))
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = Path.Combine(Pal5_GamePath.Text, "config.exe"),
                    UseShellExecute = true
                    // 不要写 Verb = "runas"
                };

                Process.Start(psi);
            }
            else
            {
                ShowMsg(L.Get("Msg_UIresolution_Title"), L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
            }
        }

        // =================================

    }
}
