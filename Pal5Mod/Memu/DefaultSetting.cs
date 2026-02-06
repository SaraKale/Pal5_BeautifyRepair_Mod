using BespokeFusion;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Security.Cryptography;
using System.Runtime.InteropServices; // 控制台

namespace 仙剑五美化修复Mod
{
    public partial class MainWindow : Window
    {

        // ==========================
        //  恢复初始默认设置
        //  
        //  所需文件：
        //  Backup\Config\uvlist.tb 到 onfig\uvlist.tb 
        //  Backup\Config\Data-SC\*.* 到  Config\Data\
        //  Backup\Texture\NpcP5\*.* 到 Texture\NpcP5\
        //  Backup\UI-SC\*.* 到 UI\
        //  Config\Data\fontinfo-default\*.* Config\Data\
        // ==========================

        // --------------------------
        // 应用确定按钮
        // Tab_default_Button
        // --------------------------
        private void Tab_default_Button_Click(object sender, RoutedEventArgs e)
        {
            // ① 检查路径和 Pal5Mod_BeautifyRepair 文件夹
            string Msg_Restoredefaultsettings = L.Get("Msg_Restoredefaultsettings");
            if (!CheckGamePath("Msg_Restoredefaultsettings") || !CheckModResource("Msg_Restoredefaultsettings"))
                return;

            //-------------------------
            // 简体中文单选框
            //-------------------------
            if (Default_SC.IsChecked == true)
                {
                        // 源文件
                        // AppDomain.CurrentDomain.BaseDirectory 永远从 exe 所在目录找资源
                        string sourceFile1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Backup", "Config","uvlist.tb");
                        string sourceDirectory1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Backup", "Config", "Data-SC");
                        string sourceDirectory2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Backup", "Texture", "NpcP5");
                        string sourceDirectory3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Backup", "UI-SC");
                        string sourceDirectory4 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Config", "Data", "fontinfo-default");

                        // 目标路径和文件夹
                        string targetFile1 = Path.Combine(Pal5_GamePath.Text, "Config", "uvlist.tb");
                        string targetDirectory1 = Path.Combine(Pal5_GamePath.Text, "Config", "Data");
                        string targetDirectory2 = Path.Combine(Pal5_GamePath.Text, "Texture", "NpcP5");
                        string targetDirectory3 = Path.Combine(Pal5_GamePath.Text, "UI");
                        string targetDirectory4 = Path.Combine(Pal5_GamePath.Text, "Config", "Data");

                        // 创建目标文件夹（如果不存在）
                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                        Directory.CreateDirectory(targetDirectory1);
                        Directory.CreateDirectory(targetDirectory2);
                        Directory.CreateDirectory(targetDirectory3);
                        Directory.CreateDirectory(targetDirectory4);

                        // 复制文件，ture为覆盖文件
                        // CopyFileIfDifferent(sourceFile1, targetFile1,true);
                        CopyFileIfDifferent(sourceFile1, targetFile1);

                        // 复制文件夹
                        // CopyFolderIfDifferent(sourceDirectory, targetDirectory);
                        CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                        CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                        CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);
                        CopyFolderIfDifferent(sourceDirectory4, targetDirectory4);

                        // 消息框提示
                        ShowMsg(L.Get("Msg_Restoredefaultsettings"), L.Get("Msg_Restoredefaultsettings_describe"),
                            MessageBoxImage.Information
                        );
                    }

                    //-------------------------
                    // 繁体中文单选框
                    //-------------------------
                    else if (Default_TC.IsChecked == true)
                    {
                        // 源文件
                        // AppDomain.CurrentDomain.BaseDirectory 永远从 exe 所在目录找资源
                        string sourceFile1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Backup", "Config","uvlist.tb");
                        string sourceDirectory1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Backup", "Config", "Data-TC");
                        string sourceDirectory2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Backup", "Texture", "NpcP5");
                        string sourceDirectory3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Backup", "UI-TC");
                        string sourceDirectory4 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Config", "Data", "fontinfo-default");

                        // 目标路径和文件夹
                        string targetFile1 = Path.Combine(Pal5_GamePath.Text, "Config", "uvlist.tb");
                        string targetDirectory1 = Path.Combine(Pal5_GamePath.Text, "Config", "Data");
                        string targetDirectory2 = Path.Combine(Pal5_GamePath.Text, "Texture", "NpcP5");
                        string targetDirectory3 = Path.Combine(Pal5_GamePath.Text, "UI");
                        string targetDirectory4 = Path.Combine(Pal5_GamePath.Text, "Config", "Data");

                        // 创建目标文件夹（如果不存在）
                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                        Directory.CreateDirectory(targetDirectory1);
                        Directory.CreateDirectory(targetDirectory2);
                        Directory.CreateDirectory(targetDirectory3);
                        Directory.CreateDirectory(targetDirectory4);

                        // 复制文件，ture为覆盖文件
                        // CopyFileIfDifferent(sourceFile1, targetFile1,true);
                        CopyFileIfDifferent(sourceFile1, targetFile1);

                        // 复制文件夹
                        // CopyFolderIfDifferent(sourceDirectory, targetDirectory);
                        CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                        CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                        CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);
                        CopyFolderIfDifferent(sourceDirectory4, targetDirectory4);

                        // 消息框提示
                        ShowMsg(L.Get("Msg_Restoredefaultsettings"), L.Get("Msg_Restoredefaultsettings_describe"),
                        MessageBoxImage.Information
                        );
                    }

                    // 如果语言都未选择
                    else
                    {
                        ShowMsg(("Msg_Restoredefaultsettings"), L.Get("Msg_NoSelectedLang"),
                            MessageBoxImage.Information
                        );
                    }

            // ===============================
        }



        // =========================
        //  选择项
        // =========================

        // 简体中文单选项
        private void Default_SC_Checked(object sender, RoutedEventArgs e)
        {

        }
        // 繁体中文单选项
        private void Default_TC_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
