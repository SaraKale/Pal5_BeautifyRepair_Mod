using BespokeFusion;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using 仙剑五美化修复Mod;
using System.Security.Cryptography;

namespace 仙剑五美化修复Mod
{
    public partial class MainWindow : Window
    {
        // ==========================
        // 战斗UI界面隐藏
        //
        // 所需文件：
        // BattleWinUIHIde\flash.ini 到 Config\Data\flash.ini 
        // Texture\Effect\BXB 到 Texture\Effect\BXB
        // Texture\Effect\YF 到 Texture\Effect\YF
        // 【也可以不需要这个图】Texture\UI\jnbj@.dds 到 Texture\UI\UIP5\jnbj@.dds 
        // Texture\UI\shuzi@.dds 到 Texture\UI\UIP5\shuzi@.dds
        // BattleWinUIHIde\BattleWinDlg1280.xml 到 UI\BattleWinDlg1280.xml
        // BattleWinUIHIde\BattleWinDlg1440.xml 到 UI\BattleWinDlg1440.xml
        // ==========================

        // --------------------------
        //  应用确定按钮
        //  Tab_BattleWinUIHIde_Button_Click
        // --------------------------

        private void Tab_BattleWinUIHIde_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // ==========================
                // 游戏路径判断
                // ==========================
                if (Pal5_GamePath == null || string.IsNullOrWhiteSpace(Pal5_GamePath.Text))
                {
                    ShowMsg(L.Get("Msg_HideBattleUI_Title"), L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                    return;
                }

                string gamePath = Pal5_GamePath.Text.Trim();
                string exePath = System.IO.Path.Combine(gamePath, "Pal5.exe");

                // ==========================
                // 判断是否真的是游戏目录
                // ==========================
                if (!File.Exists(exePath))
                {
                    ShowMsg(L.Get("Msg_HideBattleUI_Title"), L.Get("Msg_NoPal5exePath_error"), MessageBoxImage.Warning);
                    return;
                }

                // ==========================
                // 判断 Pal5Mod_BeautifyRepair 资源文件夹是否存在
                // ==========================
                string supportDir = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Pal5Mod_BeautifyRepair"
                );

                if (!Directory.Exists(supportDir))
                {
                    ShowMsg(L.Get("Msg_HideBattleUI_Title"), L.Get("Msg_NoMODfolder"), 
                        MessageBoxImage.Warning);
                    return;
                }

                // ==========================
                // 定义源路径 
                // ==========================
                string sourceFile1 = Path.Combine(supportDir, "BattleWinUIHIde","flash.ini");
               // string sourceFile2 = Path.Combine(supportDir, "Config", "uvlist-1080P", "uvlist.tb");
                string sourceFile3 = Path.Combine(supportDir, "Texture", "UI", "shuzi@.dds");
                string sourceFile4 = Path.Combine(supportDir, "BattleWinUIHIde", "BattleWinDlg1280.xml");
                string sourceFile5 = Path.Combine(supportDir, "BattleWinUIHIde", "BattleWinDlg1440.xml");
                //string sourceFile6 = Path.Combine(supportDir, "Config", "Data","Data-SC", "msdata.xml");

                string sourceDir1 = Path.Combine(supportDir, "Texture","Effect","BXB");
                string sourceDir2 = Path.Combine(supportDir, "Texture", "Effect", "YF");
               // string sourceDir3 = Path.Combine(supportDir, "UI-SC", "UI-1920x1080");
               // string sourceDir4 = Path.Combine(supportDir, "Texture", "UI", "Pal5Mod-SC");

                // ==========================
                // 定义目标路径
                // ==========================
                string targetFile1 = Path.Combine(gamePath, "Config", "Data", "flash.ini");
                //string targetFile2 = Path.Combine(gamePath, "Config", "uvlist.tb");
                string targetFile3 = Path.Combine(gamePath, "Texture", "UI", "UIP5", "shuzi@.dds");
                string targetFile4 = Path.Combine(gamePath, "UI", "BattleWinDlg1280.xml");
                string targetFile5 = Path.Combine(gamePath, "UI", "BattleWinDlg1440.xml");
               // string targetFile6 = Path.Combine(gamePath, "Config", "Data", "msdata.xml");

                string targetDir1 = Path.Combine(gamePath, "Texture", "Effect", "BXB");
                string targetDir2 = Path.Combine(gamePath, "Texture", "Effect", "YF");
                // string targetDir3 = Path.Combine(gamePath, "UI");
                // string targetDir4 = Path.Combine(gamePath, "Texture", "UI", "Pal5Mod");

                // 复制 Pal5.exe + 自动勾选管理员运行
                CopyExeAndSetAdmin(
                    @"Pal5Mod_BeautifyRepair\Pal5.exe",
                    Pal5_GamePath.Text
                );

                // ==========================
                // 创建目标目录
                // ==========================
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile1));
               // Directory.CreateDirectory(Path.GetDirectoryName(targetFile2));
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile3));
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile4));
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile5));
               // Directory.CreateDirectory(Path.GetDirectoryName(targetFile6));

                Directory.CreateDirectory(targetDir1);
                Directory.CreateDirectory(targetDir2);
                // Directory.CreateDirectory(targetDir3);
                //Directory.CreateDirectory(targetDir4);

                // ==========================
                // 复制文件
                // CopyFileIfDifferent(sourceFile1, targetFile1,true);
                // ==========================
                CopyFileIfDifferent(sourceFile1, targetFile1);
               // CopyFileIfDifferent(sourceFile2, targetFile2);
                CopyFileIfDifferent(sourceFile3, targetFile3);
                CopyFileIfDifferent(sourceFile4, targetFile4);
                CopyFileIfDifferent(sourceFile5, targetFile5);
                // CopyFileIfDifferent(sourceFile6, targetFile6);

                // ==========================
                //  复制文件夹
                // CopyFolderIfDifferent(sourceDirectory, targetDirectory);
                // ==========================
                CopyFolderIfDifferent(sourceDir1, targetDir1);
                CopyFolderIfDifferent(sourceDir2, targetDir2);
               // CopyFolderIfDifferent(sourceDir3, targetDir3);
               // CopyFolderIfDifferent(sourceDir4, targetDir4);

                // ==========================
                // 成功提示
                // ==========================
                ShowMsg(L.Get("Msg_HideBattleUI_Title"), L.Get("Msg_TrueSuccess"),
                    MessageBoxImage.Information
                );

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), L.Get("Msg_Programexception"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // ==========================
        // 恢复默认文件
        //
        // 所需文件：
        // Backup\BattleWinUIHIde\flash.ini 到 Config\Data\flash.ini 
        // Backup\Texture\Effect\BXB 到 Texture\Effect\BXB
        // Backup\Texture\Effect\YF 到 Texture\Effect\YF
        // 【也可以不需要这个图】Backup\Texture\UI\jnbj@.dds 到 Texture\UI\UIP5\jnbj@.dds 
        // Backup\Texture\shuzi@.dds 到 Texture\UI\UIP5\shuzi@.dds
        // Backup\BattleWinUIHIde\BattleWinDlg1280.xml 到 UI\BattleWinDlg1280.xml
        // Backup\BattleWinUIHIde\BattleWinDlg1440.xml 到 UI\BattleWinDlg1440.xml
        // ==========================

        // --------------------------
        //  恢复默认按钮
        //  Tab_BattleWinUIHIde_Button2_Click
        // --------------------------
        private void Tab_BattleWinUIHIde_Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // ==========================
                // 游戏路径判断
                // ==========================
                if (Pal5_GamePath == null || string.IsNullOrWhiteSpace(Pal5_GamePath.Text))
                {
                    ShowMsg(L.Get("Msg_HideBattleUI_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Warning);
                    return;
                }

                string gamePath = Pal5_GamePath.Text.Trim();
                string exePath = System.IO.Path.Combine(gamePath, "Pal5.exe");

                // ==========================
                // 判断是否真的是游戏目录
                // ==========================
                if (!File.Exists(exePath))
                {
                    ShowMsg(L.Get("Msg_HideBattleUI_Title"), L.Get("Msg_NoPal5exePath_error"), MessageBoxImage.Warning);
                    return;
                }

                // ==========================
                // 判断 Pal5Mod_BeautifyRepair 资源文件夹是否存在
                // ==========================
                string supportDir = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Pal5Mod_BeautifyRepair"
                );

                if (!Directory.Exists(supportDir))
                {
                    ShowMsg(L.Get("Msg_HideBattleUI_Title"), L.Get("Msg_NoMODfolder"),
                        MessageBoxImage.Warning);
                    return;
                }

                // ==========================
                // 定义源路径
                // ==========================
                string sourceFile1 = Path.Combine(supportDir, "Backup", "BattleWinUIHIde", "flash.ini");
                //string sourceFile2 = Path.Combine(supportDir, "Backup", "Texture", "UI", "jnbj@.dds");
                string sourceFile3 = Path.Combine(supportDir, "Backup", "Texture", "shuzi@.dds");
                string sourceFile4 = Path.Combine(supportDir, "Backup", "BattleWinUIHIde", "BattleWinDlg1280.xml");
                string sourceFile5 = Path.Combine(supportDir, "Backup", "BattleWinUIHIde", "BattleWinDlg1440.xml");

                string sourceDir1 = Path.Combine(supportDir, "Backup", "Texture", "Effect", "BXB");
                string sourceDir2 = Path.Combine(supportDir, "Backup", "Texture", "Effect", "YF");

                // ==========================
                // 定义目标路径
                // ==========================
                string targetFile1 = Path.Combine(gamePath, "Config", "Data", "flash.ini");
               //string targetFile2 = Path.Combine(gamePath, "Texture", "UI", "UIP5", "jnbj@.dds");
                string targetFile3 = Path.Combine(gamePath, "Texture", "UI", "UIP5", "shuzi@.dds");
                string targetFile4 = Path.Combine(gamePath, "UI", "BattleWinDlg1280.xml");
                string targetFile5 = Path.Combine(gamePath, "UI", "BattleWinDlg1440.xml");

                string targetDir1 = Path.Combine(gamePath, "Texture", "Effect", "BXB");
                string targetDir2 = Path.Combine(gamePath, "Texture", "Effect", "YF");

                // 复制 Pal5.exe + 自动勾选管理员运行
                CopyExeAndSetAdmin(
                    @"Pal5Mod_BeautifyRepair\Pal5.exe",
                    Pal5_GamePath.Text
                );

                // ==========================
                // 创建目标目录
                // ==========================
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile1));
                //Directory.CreateDirectory(Path.GetDirectoryName(targetFile2));
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile3));
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile4));
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile5));

                Directory.CreateDirectory(targetDir1);
                Directory.CreateDirectory(targetDir2);

                // ==========================
                // 复制文件
                // File.Copy(sourceFile1, targetFile1,true);
                // ==========================
                CopyFileIfDifferent(sourceFile1, targetFile1);
                //CopyFileIfDifferent(sourceFile2, targetFile2);
                CopyFileIfDifferent(sourceFile3, targetFile3);
                CopyFileIfDifferent(sourceFile4, targetFile4);
                CopyFileIfDifferent(sourceFile5, targetFile5);

                // ==========================
                //  复制文件夹
                // CopyFolder(sourceDirectory, targetDirectory);
                // ==========================
                CopyFolderIfDifferent(sourceDir1, targetDir1);
                CopyFolderIfDifferent(sourceDir2, targetDir2);

                // ==========================
                // 成功提示
                // ==========================
                ShowMsg(L.Get("Msg_HideBattleUI_Title"),L.Get("Msg_TrueSuccess"),
                    MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), L.Get("Msg_Programexception"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // =============== END ================
    }
}

