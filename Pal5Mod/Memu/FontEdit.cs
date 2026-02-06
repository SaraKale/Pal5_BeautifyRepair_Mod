using BespokeFusion;
// 字体列表子窗口
using FontlistBox.Pal5Mod.UI;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Security.Cryptography;

namespace 仙剑五美化修复Mod
{
    public partial class MainWindow : Window
    {

        // ==========================
        //   字体修改 
        //
        //   有三种选择：
        //   1、推荐几个字体，然后复制字体文件到游戏目录，然后修改fontinfo.ini的Name=后面的值为字体名称
        //   2、从系统已安装的字体列表选择，也是同样复制字体文件和写入ini文件
        //   3、从外部文件选择，让用户自己选择喜欢的字体文件
        // ==========================

        // --------------------------
        //  推荐字体应用按钮 
        //  Tab_fontedit_Button
        // --------------------------
        private void Tab_fontedit_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 检查游戏路径
                if (!CheckGamePath())
                    return;

                // -------------------------
                // 判断 Pal5Mod_BeautifyRepair 资源文件夹是否存在
                // -------------------------
                string supportDir = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Pal5Mod_BeautifyRepair"
                );

                if (!Directory.Exists(supportDir))
                {
                    ShowMsg(L.Get("Msg_FontEditTitle"), L.Get("Msg_NoMODfolder"),MessageBoxImage.Warning);
                    return;
                }

                // ---------------
                // 寒蝉正楷体
                // ---------------
                if (font_Chillkai.IsChecked == true)
                {
                    // 源文件
                    // AppDomain.CurrentDomain.BaseDirectory 永远从 exe 所在目录找资源
                    string sourceFile1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Config", "Data", "fontinfo-Chillkai", "fontinfo.ini");
                    string sourceFile2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Config", "Data", "fontinfo-Chillkai", "FontInfoName.ttf");

                    // 目标路径
                    string targetFile1 = Path.Combine(Pal5_GamePath.Text, "Config", "Data", "fontinfo.ini");
                    string targetFile2 = Path.Combine(Pal5_GamePath.Text, "Config", "Data", "FontInfoName.ttf");

                    // 复制 Pal5.exe + 自动勾选管理员运行
                    CopyExeAndSetAdmin(
                        @"Pal5Mod_BeautifyRepair\Pal5.exe",
                        Pal5_GamePath.Text
                    );

                    // 创建目标文件夹（如果不存在）
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));

                    // 复制文件,ture为覆盖文件
                    CopyFileIfDifferent(sourceFile1, targetFile1);
                    CopyFileIfDifferent(sourceFile2, targetFile2);

                    // 消息框提示
                    ShowMsg(L.Get("Msg_FontEditTitle"), L.Get("Msg_FontEditSuccess"),MessageBoxImage.Information);
                }

                // ---------------
                // 霞鹜文楷
                // ---------------
                else if (font_xiawu.IsChecked == true)
                {
                    // 源文件
                    // AppDomain.CurrentDomain.BaseDirectory 永远从 exe 所在目录找资源
                    string sourceFile1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Config", "Data", "fontinfo-xiawu", "fontinfo.ini");
                    string sourceFile2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Config", "Data", "fontinfo-xiawu", "FontInfoName.ttf");

                    // 目标路径
                    string targetFile1 = Path.Combine(Pal5_GamePath.Text, "Config", "Data", "fontinfo.ini");
                    string targetFile2 = Path.Combine(Pal5_GamePath.Text, "Config", "Data", "FontInfoName.ttf");

                    // 复制 Pal5.exe + 自动勾选管理员运行
                    CopyExeAndSetAdmin(
                        @"Pal5Mod_BeautifyRepair\Pal5.exe",
                        Pal5_GamePath.Text
                    );

                    // 创建目标文件夹（如果不存在）
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));

                    // 复制文件,ture为覆盖文件
                    CopyFileIfDifferent(sourceFile1, targetFile1);
                    CopyFileIfDifferent(sourceFile2, targetFile2);

                    // 消息框提示
                    ShowMsg(L.Get("Msg_FontEditTitle"), L.Get("Msg_FontEditSuccess"), MessageBoxImage.Information);
                }

                // ---------------
                // 华康唐风隶（默认字体）
                // ---------------
                else if (font_huakang.IsChecked == true)
                {
                    // 源文件
                    // AppDomain.CurrentDomain.BaseDirectory 永远从 exe 所在目录找资源
                    string sourceFile1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Config", "Data", "fontinfo-default", "fontinfo.ini");
                    string sourceFile2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pal5Mod_BeautifyRepair", "Config", "Data", "fontinfo-default", "DFPGBTL5.TTF");

                    // 目标路径
                    string targetFile1 = Path.Combine(Pal5_GamePath.Text, "Config", "Data", "fontinfo.ini");
                    string targetFile2 = Path.Combine(Pal5_GamePath.Text, "Config", "Data", "DFPGBTL5.TTF");

                    // 复制 Pal5.exe + 自动勾选管理员运行
                    CopyExeAndSetAdmin(
                        @"Pal5Mod_BeautifyRepair\Pal5.exe",
                        Pal5_GamePath.Text
                    );

                    // 创建目标文件夹（如果不存在）
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile1));
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetFile2));

                    // 复制文件,ture为覆盖文件
                    CopyFileIfDifferent(sourceFile1, targetFile1);
                    CopyFileIfDifferent(sourceFile2, targetFile2);

                    // 消息框提示
                    ShowMsg(L.Get("Msg_FontEditTitle"), L.Get("Msg_FontEditSuccess"), MessageBoxImage.Information);
                }

                // 如果都未选择字体，提示：你还没有选择字体呢！选择一下再按应用吧！
                else
                {
                    ShowMsg(L.Get("Msg_FontEditTitle"), L.Get("Msg_NOselectFont"), MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                // 程序异常提示
                MessageBox.Show(ex.ToString(), L.Get("Msg_Programexception"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // --------------------------
        //  从已安装的字体列表选择
        //  Buttonsystem
        // --------------------------
        private void Buttonsystem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 检查游戏路径
                if (!CheckGamePath())
                    return;

                // -------------------------
                // 判断 Pal5Mod_BeautifyRepair 资源文件夹是否存在
                // -------------------------
                // 判断资源文件夹
                string supportDir = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Pal5Mod_BeautifyRepair"
                );

                if (!Directory.Exists(supportDir))
                {
                    ShowMsg(L.Get("Msg_FontEditTitle"),L.Get("Msg_NoMODfolder"),MessageBoxImage.Warning);
                    return;
                }

                // 弹出新窗口，链接Fontlistbox.xaml文件
                Fontlistbox fontlist = new Fontlistbox(this);
                // 新窗口初始位置在中间
                fontlist.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                fontlist.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), L.Get("Msg_Programexception"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        // --------------------------
        //  外部文件选择
        //  Tab_fontedit_list
        // --------------------------
        private void Tab_fontedit_list_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 检查游戏路径
                if (!CheckGamePath())
                    return;

                // -------------------------
                // 判断 Pal5Mod_BeautifyRepair 资源文件夹是否存在
                // -------------------------
                string supportDir = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Pal5Mod_BeautifyRepair"
                );

                if (!Directory.Exists(supportDir))
                {
                    ShowMsg(L.Get("Msg_FontEditTitle"), L.Get("Msg_NoMODfolder"), MessageBoxImage.Warning);
                    return;
                }

                // 选择外部字体文件
                CommonOpenFileDialog openFileDialog = new CommonOpenFileDialog();
                openFileDialog.Title = L.Get("Msg_SelectedFontFile_Title"); // 请选择字体文件
                openFileDialog.Filters.Add(new CommonFileDialogFilter("字体文件", "*.ttf;*.otf;*.ttc"));
                if (openFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    // 选择文件名
                    string fileName = openFileDialog.FileName;

                    // 复制选中的文件到游戏Config\Data目录
                    string targetPath = Pal5_GamePath.Text + "\\Config\\Data\\FontInfoName" + System.IO.Path.GetExtension(fileName);
                    // 创建目标文件夹（如果不存在）
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetPath));
                    // 复制字体文件
                    File.Copy(fileName, targetPath, true);

                    // 文件名写入fontinfo.ini
                    List<string> fontSections = new List<string> { "[Font0]", "[Font1]", "[Font2]", "[Font3]", "[Font4]" };

                    // 文件编码，中文文件名称要用GB2312，其他情况用UTF-8
                    // 或者将字体重命名英文名称再写入ini，否则游戏会无法显示字体
                    //StreamWriter writer = new StreamWriter(Pal5_GamePath.Text + "\\Config\\Data\\fontinfo.ini", false, Encoding.GetEncoding("GB2312"));

                    using (StreamWriter writer = new StreamWriter(
                        Pal5_GamePath.Text + "\\Config\\Data\\fontinfo.ini",
                        false,
                        Encoding.UTF8))
                      {
                        foreach (string section in fontSections)
                        {
                            writer.WriteLine(section);
                            writer.WriteLine("Name=FontInfoName" + Path.GetExtension(fileName));
                        }
                      }

                    // 关闭读写
                    //writer.Close();

                    // 消息框提示
                    ShowMsg(L.Get("Msg_FontEditTitle"),L.Get("Msg_FontEditSuccess"),MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), L.Get("Msg_Programexception"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 游戏路径统一检查函数
        private bool CheckGamePath()
        {
            if (Pal5_GamePath == null || string.IsNullOrWhiteSpace(Pal5_GamePath.Text))
            {
                ShowMsg(L.Get("Msg_FontEditTitle"), L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                return false;
            }

            string exePath = Path.Combine(Pal5_GamePath.Text.Trim(), "Pal5.exe");
            if (!File.Exists(exePath))
            {
                ShowMsg(L.Get("Msg_FontEditTitle"), L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // ========= END ==============
    }
}
