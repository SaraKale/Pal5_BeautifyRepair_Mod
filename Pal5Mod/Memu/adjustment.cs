using BespokeFusion;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Security.Cryptography;

namespace 仙剑五美化修复Mod
{

    public partial class MainWindow : Window
    {

        // ==========================
        //  身体比例调整和肤色增白
        //
        //  所需文件：
        //  简体：
        //  Texture\NpcP5 到 Texture\NpcP5
        //  Config\Data\Data-SC\cdata.xml 到 Config\Data\cdata.xml
        //  Config\Data\Data-SC\msdata.xml 到 Config\Data\msdata.xml
        //  Config\Data\Data-SC\npcdata.xml 到 Config\Data\npcdata.xml
        //  繁体：
        //  Texture\NpcP5 到 Texture\NpcP5
        //  Config\Data\Data-TC\cdata.xml 到 Config\Data\cdata.xml
        //  Config\Data\Data-SC\msdata.xml 到 Config\Data\msdata.xml
        //  Config\Data\Data-TC\npcdata.xml 到 Config\Data\npcdata.xml
        // ==========================

        // ==========================
        //  应用确定按钮 
        //  Tab_adjustment_Button
        // ==========================

        private void Tab_adjustment_Button_Click(object sender, RoutedEventArgs e)
        {
            string basePath = Pal5_GamePath.Text;

            // ---------------------------
            // 仙剑五
            //
            // 脚本：
            // ..\Config\Data\cdata.xml
            // ..\Config\Data\msdata.xml
            // ..\Config\Data\npcdata.xml                                             
            // 贴图：
            // ..\Texture\NpcP5
            // ---------------------------

            // --------------------------
            //  下拉表选项1
            //  仙剑奇侠传五
            // --------------------------
            if (bodypathlist.SelectedIndex == 0)
            {
                // 检查游戏路径和 Pal5Mod_BeautifyRepair 文件夹
                string Msg_Adjustment_Title = L.Get("Msg_Adjustment_Title");
                if (!CheckGamePath("Msg_Adjustment_Title") || !CheckModResource("Msg_Adjustment_Title"))
                    return;

                // --------------------------
                //  简体中文按钮
                // --------------------------
                if (adjustment_SC.IsChecked == true)
                    {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\cdata.xml";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\npcdata.xml";
                            string sourceFile3 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory = @"Pal5Mod_BeautifyRepair\Texture\NpcP5";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\Data\cdata.xml";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\npcdata.xml";
                            string targetFile3 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory = Pal5_GamePath.Text + @"\Texture\NpcP5";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                basePath
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile3));
                            Directory.CreateDirectory(targetDirectory);

                            // 复制文件,ture为覆盖文件
                            // CopyFileIfDifferent(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);
                            CopyFileIfDifferent(sourceFile3, targetFile3);

                            // 复制文件夹
                            // CopyFolderIfDifferent(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory, targetDirectory);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_TrueSuccess"),MessageBoxImage.Information);
                        }

                        // --------------------------
                        //  繁体中文按钮
                        // --------------------------
                        else if (adjustment_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\cdata.xml";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\npcdata.xml";
                            string sourceFile3 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory = @"Pal5Mod_BeautifyRepair\Texture\NpcP5";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\Data\cdata.xml";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\npcdata.xml";
                            string targetFile3 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";

                            string targetDirectory = Pal5_GamePath.Text + @"\Texture\NpcP5";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                basePath
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile3));
                            Directory.CreateDirectory(targetDirectory);

                            // 复制文件,ture为覆盖文件
                            // CopyFileIfDifferent(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);
                            CopyFileIfDifferent(sourceFile3, targetFile3);

                            // 复制文件夹
                            // CopyFolderIfDifferent(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory, targetDirectory);

                            // 消息框提示
                             ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_TrueSuccess"),MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_NoSelectedLang"),MessageBoxImage.Information
                            );
                        }

                // =====================================
            }


            // ---------------------------
            // 仙剑五前传
            // 
            // 脚本：
            // ..\Config\Data\msdata.xml
            // ..\Config\Data\msdata_easy.xml
            // ..\Config\Data\msdata_hard.xml
            // ..\Config\Data\npcdata.xml
            // 贴图：
            // ..\Texture\NpcP5\DLC\JYF
            // ..\Texture\NpcP5\DLC\tangyoyo
            // ..\Texture\NpcP5\DLC\xiaoman
            // ..\Texture\NpcP5\longU
            // ---------------------------

            // --------------------------
            //  下拉表选项2
            //  仙剑奇侠传五前传
            // --------------------------
            else if (bodypathlist.SelectedIndex == 1)
            {
                // 检查游戏路径和 Pal5Mod_BeautifyRepair 文件夹
                string Msg_Adjustment_Title = L.Get("Msg_Adjustment_Title");
                if (!CheckPal5QPath("Msg_Adjustment_Title") || !CheckModResource("Msg_Adjustment_Title"))
                    return;

                // --------------------------
                //  简体中文按钮
                // --------------------------
                if (adjustment_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\Data\Pal5QData-SC\msdata.xml";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Pal5QData-SC\msdata_easy.xml";
                            string sourceFile3 = @"Pal5Mod_BeautifyRepair\Config\Data\Pal5QData-SC\msdata_hard.xml";
                            string sourceFile4 = @"Pal5Mod_BeautifyRepair\Config\Data\Pal5QData-SC\npcdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\Texture\NpcP5\JYF";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\NpcP5\tangyoyo";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\Texture\NpcP5\xiaoman";
                            string sourceDirectory4 = @"Pal5Mod_BeautifyRepair\Texture\NpcP5\longU";

                            // 目标路径和文件夹
                            string targetFile1 = PAL5QPath.Text + @"\Config\Data\msdata.xml";
                            string targetFile2 = PAL5QPath.Text + @"\Config\Data\msdata_easy.xml";
                            string targetFile3 = PAL5QPath.Text + @"\Config\Data\msdata_hard.xml";
                            string targetFile4 = PAL5QPath.Text + @"\Config\Data\npcdata.xml";
                            string targetDirectory1 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\JYF";
                            string targetDirectory2 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\tangyoyo";
                            string targetDirectory3 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\xiaoman";
                            string targetDirectory4 = PAL5QPath.Text + @"\Texture\NpcP5\longU";

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile3));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile4));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);
                            Directory.CreateDirectory(targetDirectory3);
                            Directory.CreateDirectory(targetDirectory4);

                            // 复制文件,ture为覆盖文件
                            // CopyFileIfDifferent(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);
                            CopyFileIfDifferent(sourceFile3, targetFile3);
                            CopyFileIfDifferent(sourceFile4, targetFile4);

                            // 复制文件夹
                            // CopyFolderIfDifferent(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);
                            CopyFolderIfDifferent(sourceDirectory4, targetDirectory4);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_TrueSuccess"),MessageBoxImage.Information);
                        }

                        // --------------------------
                        //  繁体中文按钮
                        // --------------------------
                        else if (adjustment_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\Data\Pal5QData-TC\msdata.xml";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Pal5QData-TC\msdata_easy.xml";
                            string sourceFile3 = @"Pal5Mod_BeautifyRepair\Config\Data\Pal5QData-TC\msdata_hard.xml";
                            string sourceFile4 = @"Pal5Mod_BeautifyRepair\Config\Data\Pal5QData-TC\npcdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\Texture\NpcP5\JYF";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\NpcP5\tangyoyo";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\Texture\NpcP5\xiaoman";
                            string sourceDirectory4 = @"Pal5Mod_BeautifyRepair\Texture\NpcP5\longU";

                            // 目标路径和文件夹
                            string targetFile1 = PAL5QPath.Text + @"\Config\Data\msdata.xml";
                            string targetFile2 = PAL5QPath.Text + @"\Config\Data\msdata_easy.xml";
                            string targetFile3 = PAL5QPath.Text + @"\Config\Data\msdata_hard.xml";
                            string targetFile4 = PAL5QPath.Text + @"\Config\Data\npcdata.xml";
                            string targetDirectory1 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\JYF";
                            string targetDirectory2 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\tangyoyo";
                            string targetDirectory3 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\xiaoman";
                            string targetDirectory4 = PAL5QPath.Text + @"\Texture\NpcP5\longU";

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile3));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile4));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);
                            Directory.CreateDirectory(targetDirectory3);
                            Directory.CreateDirectory(targetDirectory4);

                            // 复制文件,ture为覆盖文件
                            // CopyFileIfDifferent(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);
                            CopyFileIfDifferent(sourceFile3, targetFile3);
                            CopyFileIfDifferent(sourceFile4, targetFile4);

                            // 复制文件夹
                             // CopyFolderIfDifferent(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);
                            CopyFolderIfDifferent(sourceDirectory4, targetDirectory4);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_TrueSuccess"),MessageBoxImage.Information);
                           }

                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_NoSelectedLang"),MessageBoxImage.Information);
                        }
                    }

                // =====================================
        }


        // 下拉表选项
        private void bodylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // =============================
        //  恢复默认按钮 
        //  Tab_adjustment_Button2
        //
        //  所需文件：
        //  简体：
        //  Backup\Texture\NpcP5 到 Texture\NpcP5
        //  Backup\Config\Data\Data-SC\cdata.xml 到 Config\Data\cdata.xml
        //  Backup\Config\Data\Data-SC\npcdata.xml 到 Config\Data\npcdata.xml
        //  繁体：
        //  Backup\Texture\NpcP5 到 Texture\NpcP5
        //  Backup\Config\Data\Data-TC\cdata.xml 到 Config\Data\cdata.xml
        //  Backup\Config\Data\Data-TC\npcdata.xml 到 Config\Data\npcdata.xml
        // =============================
        private void Tab_adjustment_Button2_Click(object sender, RoutedEventArgs e)
        {

            // ---------------------------
            // 仙剑五
            //
            // 脚本：
            // ..\Backup\Config\Data\cdata.xml
            // ..\Backup\Config\Data\npcdata.xml                                             
            // 贴图：
            // ..\Backup\Texture\NpcP5
            // ---------------------------

            // --------------------------
            //  下拉表选项1
            //  仙剑奇侠传五
            // --------------------------
            if (bodypathlist.SelectedIndex == 0)
            {
                // 检查游戏路径和 Pal5Mod_BeautifyRepair 文件夹
                string Msg_Adjustment_Title = L.Get("Msg_Adjustment_Title");
                if (!CheckGamePath("Msg_Adjustment_Title") || !CheckModResource("Msg_Adjustment_Title"))
                    return;

                // --------------------------
                //  简体中文按钮
                // --------------------------
                if (adjustment_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Backup\Config\Data-SC\cdata.xml";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Backup\Config\Data-SC\npcdata.xml";
                            string sourceDirectory = @"Pal5Mod_BeautifyRepair\Backup\Texture\NpcP5";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\Data\cdata.xml";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\npcdata.xml";
                            string targetDirectory = Pal5_GamePath.Text + @"\Texture\NpcP5";

                            // 复制 Pal5.exe + 自动勾选管理员运行
                            CopyExeAndSetAdmin(
                                @"Pal5Mod_BeautifyRepair\Pal5.exe",
                                Pal5_GamePath.Text
                            );

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory);

                            // 复制文件,ture为覆盖文件
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            CopyFolderIfDifferent(sourceDirectory, targetDirectory);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_TrueSuccess"),MessageBoxImage.Information);
                        }

                        // --------------------------
                        //  繁体中文按钮
                        // --------------------------
                        else if (adjustment_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Backup\Config\Data-TC\cdata.xml";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Backup\Config\Data-TC\npcdata.xml";
                            string sourceDirectory = @"Pal5Mod_BeautifyRepair\Backup\Texture\NpcP5";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\Data\cdata.xml";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\npcdata.xml";
                            string targetDirectory = Pal5_GamePath.Text + @"\Texture\NpcP5";

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));
                            Directory.CreateDirectory(targetDirectory);

                            // 复制文件,ture为覆盖文件
                            // CopyFileIfDifferent(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                             // CopyFolderIfDifferent(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory, targetDirectory);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_TrueSuccess"),MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_NoSelectedLang"),MessageBoxImage.Information);
                        }

                // =====================================
            }


            // ---------------------------
            // 仙剑五前传
            // 
            // 脚本：
            // ..\Backup\Config\Data\npcdata.xml
            // 贴图：
            // ..\Backup\Texture\NpcP5\DLC\JYF
            // ..\Backup\Texture\NpcP5\DLC\tangyoyo
            // ..\Backup\Texture\NpcP5\DLC\xiaoman
            // ..\Backup\Texture\NpcP5\longU
            // ---------------------------

            // --------------------------
            //  下拉表选项2
            //  仙剑奇侠传五前传
            // --------------------------
            else if (bodypathlist.SelectedIndex == 1)
            {
                // 检查游戏路径和 Pal5Mod_BeautifyRepair 文件夹
                string Msg_Adjustment_Title = L.Get("Msg_Adjustment_Title");
                if (!CheckPal5QPath("Msg_Adjustment_Title") || !CheckModResource("Msg_Adjustment_Title"))
                    return;

                // --------------------------
                //  简体中文按钮
                // --------------------------
                if (adjustment_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Backup\Config\Pal5QData-SC\npcdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\Backup\Texture\NpcP5\JYF";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Backup\Texture\NpcP5\tangyoyo";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\Backup\Texture\NpcP5\xiaoman";
                            string sourceDirectory4 = @"Pal5Mod_BeautifyRepair\Backup\Texture\NpcP5\longU";

                            // 目标路径和文件夹
                            string targetFile1 = PAL5QPath.Text + @"\Config\Data\npcdata.xml";
                            string targetDirectory1 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\JYF";
                            string targetDirectory2 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\tangyoyo";
                            string targetDirectory3 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\xiaoman";
                            string targetDirectory4 = PAL5QPath.Text + @"\Texture\NpcP5\longU";

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);
                            Directory.CreateDirectory(targetDirectory3);
                            Directory.CreateDirectory(targetDirectory4);

                            // 复制文件,ture为覆盖文件
                            // CopyFileIfDifferent(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);

                            // 复制文件夹
                             // CopyFolderIfDifferent(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);
                            CopyFolderIfDifferent(sourceDirectory4, targetDirectory4);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_TrueSuccess"),MessageBoxImage.Information);
                        }

                        // --------------------------
                        //  繁体中文按钮
                        // --------------------------
                        else if (adjustment_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Backup\Config\Pal5QData-TC\npcdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\Backup\Texture\NpcP5\JYF";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Backup\Texture\NpcP5\tangyoyo";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\Backup\Texture\NpcP5\xiaoman";
                            string sourceDirectory4 = @"Pal5Mod_BeautifyRepair\Backup\Texture\NpcP5\longU";

                            // 目标路径和文件夹
                            string targetFile1 = PAL5QPath.Text + @"\Config\Data\npcdata.xml";
                            string targetDirectory1 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\JYF";
                            string targetDirectory2 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\tangyoyo";
                            string targetDirectory3 = PAL5QPath.Text + @"\Texture\NpcP5\DLC\xiaoman";
                            string targetDirectory4 = PAL5QPath.Text + @"\Texture\NpcP5\longU";

                            // 创建目标文件夹（如果不存在）
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                            Directory.CreateDirectory(targetDirectory1);
                            Directory.CreateDirectory(targetDirectory2);
                            Directory.CreateDirectory(targetDirectory3);
                            Directory.CreateDirectory(targetDirectory4);

                            // 复制文件,ture为覆盖文件
                            // CopyFileIfDifferent(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);

                            // 复制文件夹
                             // CopyFolderIfDifferent(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);
                            CopyFolderIfDifferent(sourceDirectory4, targetDirectory4);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_TrueSuccess"),MessageBoxImage.Information);
                        }

                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_NoSelectedLang"),MessageBoxImage.Information);
                }

                // =====================================

            }

            // ============================

            // 如果仙五和五前路径都未选择
            else
            {
                ShowMsg(L.Get("Msg_Adjustment_Title"),L.Get("Msg_NoSelectAllPath"),MessageBoxImage.Information);
            }

        }

        //简体中文按钮
        private void adjustment_SC_Checked(object sender, RoutedEventArgs e)
        {

        }
        //繁体中文按钮
        private void adjustment_TC_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}

