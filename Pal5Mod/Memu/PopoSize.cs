using BespokeFusion;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Security.Cryptography;

namespace 仙剑五美化修复Mod
{
    public partial class MainWindow : Window
    {

        // ==========================
        //  调整消息对话框字号
        //
        //  所需文件：
        //  Config\uvlist-1080P\uvlist.tb 到 Config\uvlist.tb
        //  Config\Data\Data-SC\msdata.xml 到 Config\Data\msdata.xml
        //  UI-SC\UI-1920x1080\*.* 到 UI\
        //  Texture\UI\Pal5Mod-SC\*.*  到 Texture\UI\Pal5Mod\
        //
        //  18号：UI\Message-size18\*.* 到 UI\
        //  28号：UI\Message-size28\*.* 到 UI\
        //  38号：UI\Message-size38\*.* 到 UI\
        //  48号：UI\Message-size48\*.* 到 UI\
        //
        //  注意uvlist.tb有可能和UI界面扩展有冲突，最好是先应用UI界面后再修改消息框字号
        // ==========================

        //-------------------------
        // 应用确定按钮
        // Tab_poposize_Button
        //-------------------------
        private void Tab_poposize_Button_Click(object sender, RoutedEventArgs e)
        {
            // 更新抗锯齿值 MultiSample=1
            UpdateMultiSampleValue("1");

            // ① 检查路径和 Pal5Mod_BeautifyRepair 文件夹
            string Msg_PopoSize_Title = L.Get("Msg_PopoSize_Title");
            if (!CheckGamePath("Msg_PopoSize_Title") || !CheckModResource("Msg_PopoSize_Title"))
                return;

            //-------------------------
            // 小号 18号 默认字号
            //-------------------------
            if (radio_Small.IsChecked == true)
                    {
                        //-------------------------
                        // 简体中文复选框
                        //-------------------------
                        if (poposize_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1920x1080";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\UI-SC\Message-size18";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";
                            string targetDirectory3 = Pal5_GamePath.Text + @"\UI";

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
                            Directory.CreateDirectory(targetDirectory3);

                            // 复制文件,ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_PopoSizeSuccess"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文复选框
                        //-------------------------
                        else if (poposize_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1920x1080";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\UI-TC\Message-size18";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";
                            string targetDirectory3 = Pal5_GamePath.Text + @"\UI";

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
                            Directory.CreateDirectory(targetDirectory3);

                            // 复制文件,ture为覆盖文件
                             // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_PopoSizeSuccess"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_PopoSize_Title"),L.Get("Msg_NoSelectedLang"),MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 中号 28号 
                    //-------------------------
                    else if (radio_in.IsChecked == true)
                    {
                        //-------------------------
                        // 简体中文复选框
                        //-------------------------
                        if (poposize_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1920x1080";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\UI-SC\Message-size28";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";
                            string targetDirectory3 = Pal5_GamePath.Text + @"\UI";

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
                            Directory.CreateDirectory(targetDirectory3);

                            // 复制文件,ture为覆盖文件
                             // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                             // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_PopoSizeSuccess"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文复选框
                        //-------------------------
                        else if (poposize_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1920x1080";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\UI-TC\Message-size28";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";
                            string targetDirectory3 = Pal5_GamePath.Text + @"\UI";

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
                            Directory.CreateDirectory(targetDirectory3);

                            // 复制文件,ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                             // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_PopoSizeSuccess"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 大号 38号 
                    //-------------------------
                    else if (radio_large.IsChecked == true)
                    {
                        //-------------------------
                        // 简体中文复选框
                        //-------------------------
                        if (poposize_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1920x1080";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\UI-SC\Message-size38";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";
                            string targetDirectory3 = Pal5_GamePath.Text + @"\UI";

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
                            Directory.CreateDirectory(targetDirectory3);

                            // 复制文件,ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_PopoSizeSuccess"), MessageBoxImage.Information);
                        }

                        //-------------------------
                        // 繁体中文复选框
                        //-------------------------
                        else if (poposize_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1920x1080";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\UI-TC\Message-size38";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";
                            string targetDirectory3 = Pal5_GamePath.Text + @"\UI";

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
                            Directory.CreateDirectory(targetDirectory3);

                            // 复制文件,ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                            // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_PopoSizeSuccess"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    //-------------------------
                    // 特大号 48号 
                    //-------------------------
                    else if (radio_Extralarge.IsChecked == true)
                    {
                        //-------------------------
                        // 简体中文复选框
                        //-------------------------
                        if (poposize_SC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-SC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-SC\UI-1920x1080";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-SC";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\UI-SC\Message-size48";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";
                            string targetDirectory3 = Pal5_GamePath.Text + @"\UI";

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
                            Directory.CreateDirectory(targetDirectory3);

                            // 复制文件,ture为覆盖文件
                            // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                             // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_PopoSizeSuccess"), MessageBoxImage.Information);
                        }

                     //-------------------------
                     // 繁体中文复选框
                     //-------------------------
                     else if (poposize_TC.IsChecked == true)
                        {
                            // 源文件和文件夹
                            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Config\uvlist-1080P\uvlist.tb";
                            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Config\Data\Data-TC\msdata.xml";
                            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\UI-TC\UI-1920x1080";
                            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Texture\UI\Pal5Mod-TC";
                            string sourceDirectory3 = @"Pal5Mod_BeautifyRepair\UI-TC\Message-size48";

                            // 目标路径和文件夹
                            string targetFile1 = Pal5_GamePath.Text + @"\Config\uvlist.tb";
                            string targetFile2 = Pal5_GamePath.Text + @"\Config\Data\msdata.xml";
                            string targetDirectory1 = Pal5_GamePath.Text + @"\UI";
                            string targetDirectory2 = Pal5_GamePath.Text + @"\Texture\UI\Pal5Mod";
                            string targetDirectory3 = Pal5_GamePath.Text + @"\UI";

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
                            Directory.CreateDirectory(targetDirectory3);

                            // 复制文件,ture为覆盖文件
                             // File.Copy(sourceFile1, targetFile1,true);
                            CopyFileIfDifferent(sourceFile1, targetFile1);
                            CopyFileIfDifferent(sourceFile2, targetFile2);

                            // 复制文件夹
                             // CopyFolder(sourceDirectory, targetDirectory);
                            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
                            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);
                            CopyFolderIfDifferent(sourceDirectory3, targetDirectory3);

                            // 消息框提示
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_PopoSizeSuccess"), MessageBoxImage.Information);
                        }
                        // 如果语言都未选择
                        else
                        {
                            ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_NoSelectedLang"), MessageBoxImage.Information);
                        }
                    }

                    // ============================

                    // 如果字号都未选择
                    else
                    {
                        ShowMsg(L.Get("Msg_PopoSize_Title"), L.Get("Msg_NoSelectAllFontSize"), MessageBoxImage.Information);
                    }

            // ============================
        }

        //// 滑轮条数字判断 Slider控件
        //int num = 25; // 需要检查的数字
        //if (num >= 16 && num <= 28)
        //{
        //    Console.WriteLine("数字在16-28之间");
        //}
        //else if (num >= 30 && num <= 40)
        //{
        //    Console.WriteLine("数字在30-40之间");
        //}
        //else
        //{
        //    Console.WriteLine("数字不在16-28或30-40之间");
        //}

        // 简体中文复选框
        private void poposize_SC_Checked(object sender, RoutedEventArgs e)
        {

        }
        // 繁体中文复选框
        private void poposize_TC_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
