using BespokeFusion;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Security.Cryptography;

// 文件复制进度条子窗口
using copybar.Pal5Mod.UI;

namespace 仙剑五美化修复Mod
{
    public partial class MainWindow : Window
    {
        // 2026-01-19：已放弃这个功能，文件过大，不适合打包进程序，也没时间去修复了。

        // ==========================
        //  语音修复
        //
        //  所需文件：
        //  Sound\speech\*.* 到 Sound\speech\
        // ==========================

        // --------------------------
        //  应用确定按钮
        //  Tab_voice_Button
        // --------------------------
        private void Tab_voice_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // -------------------------
                // 游戏路径判断
                // -------------------------
                if (Pal5_GamePath == null || string.IsNullOrWhiteSpace(Pal5_GamePath.Text))
                {
                    ShowMsg("语音修复", "你还没有选择游戏路径呢！请在左菜单第一个选项选择一下路径吧～", MessageBoxImage.Warning);
                    return;
                }

                string gamePath = Pal5_GamePath.Text.Trim();
                string exePath = System.IO.Path.Combine(gamePath, "Pal5.exe");

                // -------------------------
                // 判断是否真的是游戏目录
                // -------------------------
                if (!File.Exists(exePath))
                {
                    ShowMsg("语音修复", "选择的目录中未找到 Pal5.exe，请确认是否为正确的游戏目录。", MessageBoxImage.Warning);
                    return;
                }

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
                    ShowMsg(
                        "语音修复",
                        "没有找到 Pal5Mod_BeautifyRepair 文件夹，请将其放在程序同级目录中。",
                        MessageBoxImage.Warning
                    );
                    return;
                }

                // 源文件夹路径
                string sourceFolderPath = "Pal5Mod_BeautifyRepair\\Sound\\speech";
                // 目标文件夹路径
                string destinationFolderPath = Pal5_GamePath.Text + "\\Sound\\speech2";
                // 创建目标文件夹（如果不存在）
                Directory.CreateDirectory(destinationFolderPath);

                // 弹出新窗口，提示文件复制进度
                // 链接Copybar.xaml文件
                Copybar copybarWindow = new Copybar(sourceFolderPath, destinationFolderPath);
                // 新窗口初始位置在中间
                copybarWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                copybarWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "程序发生异常",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }

        // ================================
}
    


