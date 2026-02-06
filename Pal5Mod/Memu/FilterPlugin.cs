using BespokeFusion;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Security.Cryptography;

namespace 仙剑五美化修复Mod
{

    public partial class MainWindow : Window
    {
        // ==========================
        //  滤镜截图插件
        //
        //  所需文件：
        //  Pal5Mod_BeautifyRepair\Reshade\d3d9.dll 到 根目录\d3d9.dll
        //  Pal5Mod_BeautifyRepair\Reshade\ReShade.ini 根目录\ReShade.ini
        //  Pal5Mod_BeautifyRepair\Reshade\ReShadePreset.ini 根目录\ReShadePreset.ini
        //  Pal5Mod_BeautifyRepair\Reshade\reshade-presets\*.* 根目录\reshade-presets\
        //  Pal5Mod_BeautifyRepair\Reshade\reshade-shaders\*.* 根目录\reshade-shaders\
        //
        // ==========================

        // --------------------------
        //  应用确定按钮
        //  Tab_Filter_Button
        // --------------------------
        private void Tab_Filter_Button_Click(object sender, RoutedEventArgs e)
        {
            // ==========================
            // 游戏路径判断
            // ==========================
            if (Pal5_GamePath == null || string.IsNullOrWhiteSpace(Pal5_GamePath.Text))
            {
                ShowMsg("滤镜截图插件", "你还没有选择游戏路径呢！请在左菜单第一个选项选择一下路径吧～", MessageBoxImage.Warning);
                return;
            }

            string gamePath = Pal5_GamePath.Text.Trim();
            string exePath = System.IO.Path.Combine(gamePath, "Pal5.exe");

            // ==========================
            // 判断是否真的是游戏目录
            // ==========================
            if (!File.Exists(exePath))
            {
                ShowMsg("滤镜截图插件", "选择的目录中未找到 Pal5.exe，请确认是否为正确的游戏目录。", MessageBoxImage.Warning);
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
                ShowMsg(
                    "战斗UI界面隐藏",
                    "没有找到 Pal5Mod_BeautifyRepair 文件夹，请将其放在程序同级目录中。",
                    MessageBoxImage.Warning
                );
                return;
            }

            // ==========================
            // 定义源路径
            // ==========================
            string sourceFile1 = @"Pal5Mod_BeautifyRepair\Reshade\d3d9.dll";
            string sourceFile2 = @"Pal5Mod_BeautifyRepair\Reshade\DefaultPreset.ini";
            string sourceFile3 = @"Pal5Mod_BeautifyRepair\Reshade\ReShade.ini";
            string sourceDirectory1 = @"Pal5Mod_BeautifyRepair\Reshade\reshade-presets";
            string sourceDirectory2 = @"Pal5Mod_BeautifyRepair\Reshade\reshade-shaders";

            // ==========================
            // 定义目标路径
            // ==========================
            string targetFile1 = Pal5_GamePath.Text + @"\d3d9.dll";
            string targetFile2 = Pal5_GamePath.Text + @"\DefaultPreset.ini";
            string targetFile3 = Pal5_GamePath.Text + @"\ReShade.ini";
            string targetDirectory1 = Pal5_GamePath.Text + @"\reshade-presets";
            string targetDirectory2 = Pal5_GamePath.Text + @"\reshade-shaders";

            // ==========================
            // 创建目标目录
            // ==========================
            Directory.CreateDirectory(Path.GetDirectoryName(targetFile1));
            Directory.CreateDirectory(Path.GetDirectoryName(targetFile2));
            Directory.CreateDirectory(Path.GetDirectoryName(targetFile3));

            Directory.CreateDirectory(sourceDirectory1);
            Directory.CreateDirectory(sourceDirectory2);

            // ==========================
            // 复制文件
            // ==========================
            CopyFileIfDifferent(sourceFile1, targetFile1);
            CopyFileIfDifferent(sourceFile2, targetFile2);
            CopyFileIfDifferent(sourceFile3, targetFile3);

            // ==========================
            //  复制文件夹
            // ==========================
            CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
            CopyFolderIfDifferent(sourceDirectory2, targetDirectory2);

            // ==========================
            // 成功提示
            // ==========================
            ShowMsg(
                "滤镜截图插件",
                "应用成功！\n\n如果游戏正在运行，请关闭游戏后重新启动查看效果。",
                MessageBoxImage.Information
                 );

        }

        // =================================
    }
}

