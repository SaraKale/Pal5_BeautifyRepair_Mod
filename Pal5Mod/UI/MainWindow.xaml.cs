using BespokeFusion;
// 文件复制进度条 UI\Copybar.xaml
using copybar.Pal5Mod.UI;
// 主题UI框架
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
// UI/AboutMe.xaml 关于界面
using Pal5Mod_BeautifyRepair.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WPFLocalizeExtension.Engine; // 多语言扩展
using WPFLocalizeExtension.Extensions;
using System.Runtime.InteropServices; // 控制台

namespace 仙剑五美化修复Mod
{
    // ==================
    //  主窗口 MainWindow.xaml
    // ==================

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        // 写入ini配置文件和路径
        private const string ConfigFilePath = "Pal5ModConfig.ini";
        private const string Pal5PathKey = "Pal5Path"; // 仙剑五
        private const string Pal5QPathKey = "Pal5QPath"; // 仙剑五前传
        private const string LanguageKey = "Language"; // 界面语言配置键（持久化）

        // option.ini配置文件
        private const string IniFilePath = "\\Config\\option.ini";
        // 全屏选项 关联UIdesign.cs
        private const string IsWindowKey = "isWindow";
        // 分辨率选项 关联UIdesign.cs
        private const string ResolutionKey = "resolution";

        public MainWindow()
        {
            InitializeComponent();
            // 在窗口加载时读取路径配置文件
            LoadGamePathFromConfig();
            // 启动时读取语言配置
            LoadLanguageFromConfig();
        }

        // 设置传递属性到子窗口-字体选择列表 UI\Fontlistbox.xaml
        public string FontBoxText
        {
            get { return Pal5_GamePath.Text; }
            set { Pal5_GamePath.Text = value; }
        }
        // 设置传递属性到子窗口-文件复制进度 UI\Copybar.xaml
        public string Copybartext
        {
            get { return Pal5_GamePath.Text; }
            set { Pal5_GamePath.Text = value; }
        }

        // ==========================
        //  游戏根目录浏览选择
        // ==========================

        // --------------------------
        //  仙剑五
        //  Pal5_GamePathbotton
        // --------------------------

        private void Pal5_GamePathbotton_Click(object sender, RoutedEventArgs e)
        {
            // --------------------------------
            // 调用Windows API方法
            // 安装：工具——Nuget包管理器——程序包管理器控制台，输入：Install-Package WindowsAPICodePack-Shell
            // 然后引用：using Microsoft.WindowsAPICodePack.Dialogs;
            // https://www.nuget.org/packages/WindowsAPICodePack-Shell
            // --------------------------------

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Title = L.Get("Pal5_SelectGamePath"); // 标题，请选择仙五根目录
            dialog.IsFolderPicker = true;   // 设置为选择文件夹
            // dialog.InitialDirectory = currentDirectory; // 初始文件夹
            dialog.AddToMostRecentlyUsedList = false; // 添加到最近使用的列表
            dialog.AllowNonFileSystemItems = false; // 允许非文件系统项
            // dialog.DefaultDirectory = currentDirectory; // 默认目录
            dialog.EnsureFileExists = false; // 确保文件存在
            dialog.EnsurePathExists = true; // 确定路径存在
            dialog.EnsureReadOnly = false; // 确定只读
            dialog.EnsureValidNames = true; // 有效名称
            dialog.Multiselect = false; // 多选
            dialog.ShowPlacesList = true; // 显示位置列表

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // 将路径写入ini配置文件
                SaveGamePathToConfig(dialog.FileName, Pal5PathKey);
                // 如果选择浏览就会在文本框显示路径
                Pal5_GamePath.Text = dialog.FileName;
            }
            else
            {
                // 用户取消了操作，直接返回
                return;
            }

            // 判断目录下是否存在Pal5.exe，并提示在下方提示文本 控件名：Pathtishi
            if (File.Exists(Pal5_GamePath.Text + "\\Pal5.exe"))
            {
                Pal5_Pathtishi.Text = L.Get("GeneralPathText_PathOK"); // 已识别到游戏目录
                Pal5_Pathtishi.Foreground = Brushes.Green;
            }
            else
            {
                Pal5_Pathtishi.Text = L.Get("GeneralPathText_NoPath"); // 未识别到游戏目录，请重新选择
                Pal5_Pathtishi.Foreground = Brushes.Red;
            }
        }

        // 路径文本框 Pal5_GamePath
        private void Pal5_GamePath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // --------------------------
        //  仙五前传路径 
        //  PAL5QPathbotton
        // --------------------------
        private void PAL5QPathbotton_ClicK(object sender, RoutedEventArgs e)
        {
            // 打开目录
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.Title = L.Get("Pal5Q_SelectGamePath"); // 标题，请选择仙五前传根目录
            dialog.IsFolderPicker = true;   // 设置为选择文件夹
            // dialog.InitialDirectory = currentDirectory; // 初始文件夹
            dialog.AddToMostRecentlyUsedList = false; // 添加到最近使用的列表
            dialog.AllowNonFileSystemItems = false; // 允许非文件系统项
            // dialog.DefaultDirectory = currentDirectory; // 默认目录
            dialog.EnsureFileExists = false; // 确保文件存在
            dialog.EnsurePathExists = true; // 确定路径存在
            dialog.EnsureReadOnly = false; // 确定只读
            dialog.EnsureValidNames = true; // 有效名称
            dialog.Multiselect = false; // 多选
            dialog.ShowPlacesList = true; // 显示位置列表

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // 将路径写入ini配置文件
                SaveGamePathToConfig(dialog.FileName, Pal5QPathKey);
                // 如果选择浏览就会在文本框显示路径
                PAL5QPath.Text = dialog.FileName;
            }
            else
            {
                // 用户取消了操作，直接返回
                return;
            }

            // 判断目录下是否存在Pal5Q.exe，并提示在下方提示文本 控件名：PAL5Q_Pathtishi
            if (File.Exists(PAL5QPath.Text + "\\Pal5Q.exe"))
            {
                PAL5Q_Pathtishi.Text = L.Get("GeneralPathText_PathOK"); // 已识别到游戏目录
                PAL5Q_Pathtishi.Foreground = Brushes.Green;
            }
            else
            {
                PAL5Q_Pathtishi.Text = L.Get("GeneralPathText_NoPath"); // 未识别到游戏目录，请重新选择
                PAL5Q_Pathtishi.Foreground = Brushes.Red;
            }
        }

        // 路径文本框 Pal5Q_GamePath
        private void PAL5Q_GamePath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        // --------------------------
        //  路径拖放事件
        // --------------------------
        // 仙剑五
        private void Pal5_GamePath_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (paths.Length > 0 && (Directory.Exists(paths[0]) || File.Exists(paths[0])))
                    e.Effects = DragDropEffects.Copy;
                else
                    e.Effects = DragDropEffects.None;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }
        // 仙五前传
        private void PAL5QPath_DragOver(object sender, DragEventArgs e)
        {
            // 与上面相同的逻辑
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (paths.Length > 0 && (Directory.Exists(paths[0]) || File.Exists(paths[0])))
                    e.Effects = DragDropEffects.Copy;
                else
                    e.Effects = DragDropEffects.None;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        // ---------------------------------
        // Drop 处理（支持拖文件或文件夹；拖文件时取其父目录）
        // ----------------------------------
        // 仙剑五
        private void Pal5_GamePath_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (paths.Length == 0) return;

            string raw = paths[0];
            string folder = Directory.Exists(raw) ? raw : (File.Exists(raw) ? Path.GetDirectoryName(raw) : null);
            if (string.IsNullOrWhiteSpace(folder)) return;

            Pal5_GamePath.Text = folder;
            SaveGamePathToConfig(folder, Pal5PathKey);

            if (File.Exists(Path.Combine(folder, "Pal5.exe")))
            {
                Pal5_Pathtishi.Text = L.Get("GeneralPathText_PathOK"); // 已识别到游戏目录
                Pal5_Pathtishi.Foreground = Brushes.Green;
            }
            else
            {
                Pal5_Pathtishi.Text = L.Get("GeneralPathText_NoPath"); // 未识别到游戏目录，请重新选择
                Pal5_Pathtishi.Foreground = Brushes.Red;
            }
        }
        // 仙五前传
        private void PAL5QPath_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (paths.Length == 0) return;

            string raw = paths[0];
            string folder = Directory.Exists(raw) ? raw : (File.Exists(raw) ? Path.GetDirectoryName(raw) : null);
            if (string.IsNullOrWhiteSpace(folder)) return;

            PAL5QPath.Text = folder;
            SaveGamePathToConfig(folder, Pal5QPathKey);

            if (File.Exists(Path.Combine(folder, "Pal5Q.exe")))
            {
                PAL5Q_Pathtishi.Text = L.Get("GeneralPathText_PathOK"); // 已识别到游戏目录
                PAL5Q_Pathtishi.Foreground = Brushes.Green;
            }
            else
            {
                PAL5Q_Pathtishi.Text = L.Get("GeneralPathText_NoPath"); // 未识别到游戏目录，请重新选择
                PAL5Q_Pathtishi.Foreground = Brushes.Red;
            }
        }

        // ---------------------------------
        // 失焦时保存并校验（支持用户手动输入路径或输入可执行文件路径）
        // ----------------------------------
        // 仙剑五
        private void Pal5_GamePath_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = Pal5_GamePath.Text?.Trim();
            if (string.IsNullOrWhiteSpace(text)) return;

            string folder = Directory.Exists(text) ? text : (File.Exists(text) ? Path.GetDirectoryName(text) : null);
            if (string.IsNullOrWhiteSpace(folder))
            {
                Pal5_Pathtishi.Text = L.Get("GeneralFocus_Pathnotexist"); // 路径不存在，请重新输入或选择
                Pal5_Pathtishi.Foreground = Brushes.Red;
                return;
            }

            Pal5_GamePath.Text = folder;
            SaveGamePathToConfig(folder, Pal5PathKey);

            if (File.Exists(Path.Combine(folder, "Pal5.exe")))
            {
                Pal5_Pathtishi.Text = L.Get("GeneralPathText_PathOK"); // 已识别到游戏目录
                Pal5_Pathtishi.Foreground = Brushes.Green;
            }
            else
            {
                Pal5_Pathtishi.Text = L.Get("GeneralPathText_NoPath"); // 未识别到游戏目录，请重新选择
                Pal5_Pathtishi.Foreground = Brushes.Red;
            }
        }
        // 仙五前传
        private void PAL5Q_GamePath_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = PAL5QPath.Text?.Trim();
            if (string.IsNullOrWhiteSpace(text)) return;

            string folder = Directory.Exists(text) ? text : (File.Exists(text) ? Path.GetDirectoryName(text) : null);
            if (string.IsNullOrWhiteSpace(folder))
            {
                PAL5Q_Pathtishi.Text = L.Get("GeneralFocus_Pathnotexist"); // 路径不存在，请重新输入或选择
                PAL5Q_Pathtishi.Foreground = Brushes.Red;
                return;
            }

            PAL5QPath.Text = folder;
            SaveGamePathToConfig(folder, Pal5QPathKey);

            if (File.Exists(Path.Combine(folder, "Pal5Q.exe")))
            {
                PAL5Q_Pathtishi.Text = L.Get("GeneralPathText_PathOK"); // 已识别到游戏目录
                PAL5Q_Pathtishi.Foreground = Brushes.Green;
            }
            else
            {
                PAL5Q_Pathtishi.Text = L.Get("GeneralPathText_NoPath"); // 未识别到游戏目录，请重新选择
                PAL5Q_Pathtishi.Foreground = Brushes.Red;
            }
        }

        // ---------------------------------
        // 按 Enter 时触发失焦处理（方便输入后直接回车保存）
        // ----------------------------------
        // 仙剑五
        private void Pal5_GamePath_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // 移除焦点以触发 LostFocus 事件
                var element = sender as UIElement;
                element?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
        }
        // 仙五前传
        private void PAL5Q_GamePath_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var element = sender as UIElement;
                element?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
        }

        // --------------------------
        //  配置文件设置
        // --------------------------

        // 保存更改的路径到配置文件
        private void SaveGamePathToConfig(string newPathValue, string pathKey)
        {
            string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFilePath);
            try
            {
                if (!File.Exists(configPath))
                {
                    File.WriteAllLines(configPath, new[]
                    {
                        $"{Pal5PathKey}=",
                        $"{Pal5QPathKey}=",
                        $"{LanguageKey}=en"
                    }, Encoding.UTF8);
                }

                var lines = File.ReadAllLines(configPath, Encoding.UTF8).ToList();
                bool found = false;
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith($"{pathKey}="))
                    {
                        lines[i] = $"{pathKey}={newPathValue}";
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    lines.Add($"{pathKey}={newPathValue}");
                }
                File.WriteAllLines(configPath, lines, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                // 记录或显示错误
            }
        }

        // 读取 Pal5ModConfig.ini 中某个 key 的值（若不存在返回 defaultValue）
        private string ReadConfigValue(string key, string defaultValue = "")
        {
            string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFilePath);
            if (!File.Exists(configPath))
                return defaultValue;

            try
            {
                var lines = File.ReadAllLines(configPath, Encoding.UTF8);
                foreach (var line in lines)
                {
                    if (line.StartsWith($"{key}=", StringComparison.OrdinalIgnoreCase))
                    {
                        return line.Substring(line.IndexOf('=') + 1);
                    }
                }
            }
            catch
            {
                // 忽略读取错误
            }
            return defaultValue;
        }

        // 再次打开程序读取配置文件
        private void LoadGamePathFromConfig()
        {
            // 仙剑五
            string Pal5PathKey = "Pal5Path";
            // 仙剑五前传
            string Pal5QPathKey = "Pal5QPath";

            // 检查仙剑五的路径是否存在于配置文件中
            if (File.Exists(ConfigFilePath))
            {
                // 读取配置文件
                var configFileLines = File.ReadAllLines(ConfigFilePath).ToList();
                // 检查仙剑五的路径是否存在
                foreach (string line in configFileLines)
                {
                    if (line.StartsWith($"{Pal5PathKey}="))
                    {
                        // 获取仙剑五的路径
                        Pal5_GamePath.Text = line.Replace($"{Pal5PathKey}=", "");
                        break;
                    }
                }
                // 检查仙剑五前传的路径是否存在
                foreach (string line in configFileLines)
                {
                    if (line.StartsWith($"{Pal5QPathKey}="))
                    {
                        // 获取仙剑五前传的路径
                        PAL5QPath.Text = line.Replace($"{Pal5QPathKey}=", "");
                        break;
                    }
                }
            }

            // 判断仙剑五目录是否有Pal5.exe，并提示在下方提示文本
            string rootPath = Pal5_GamePath.Text.Trim();
            string fullPath = System.IO.Path.Combine(rootPath, "Pal5.exe");

            if (File.Exists(fullPath))
            {
                Pal5_Pathtishi.Text = L.Get("GeneralPathText_PathOK");
                Pal5_Pathtishi.Foreground = Brushes.Green;
            }
            else
            {
                Pal5_Pathtishi.Text = L.Get("GeneralPathText_NoPath");
                Pal5_Pathtishi.Foreground = Brushes.Red;
            }

            // 判断仙剑五前传目录是否有Pal5Q.exe，并提示在下方提示文本
            if (File.Exists(PAL5QPath.Text + "\\Pal5Q.exe"))
            {
                PAL5Q_Pathtishi.Text = L.Get("GeneralPathText_PathOK"); // 已识别到游戏目录
                PAL5Q_Pathtishi.Foreground = Brushes.Green;
            }
            else
            {
                PAL5Q_Pathtishi.Text = L.Get("GeneralPathText_NoPath"); // 未识别到游戏目录，请重新选择
                PAL5Q_Pathtishi.Foreground = Brushes.Red;
            }
        }


        // ==========================
        // 复制文件/文件夹通用代码
        // CopyFolderIfDifferent(sourceDirectory1, targetDirectory1);
        // ==========================

        // 复制文件夹
        private static void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            foreach (string file in Directory.GetFiles(sourceFolder))
            {
                string fileName = System.IO.Path.GetFileName(file);
                string destFile = System.IO.Path.Combine(destFolder, fileName);
                File.Copy(file, destFile);
            }

            foreach (string folder in Directory.GetDirectories(sourceFolder))
            {
                string fileName = System.IO.Path.GetFileName(folder);
                string destFolderName = System.IO.Path.Combine(destFolder, fileName);
                CopyFolder(folder, destFolderName);
            }
        }

        // 核心方法1：仅当文件不同时复制单个文件
        private void CopyFileIfDifferent(string sourcePath, string targetPath)
        {
            // 源文件不存在则直接返回（或抛出异常，根据你的需求）
            if (!File.Exists(sourcePath))
            {
                Console.WriteLine($"源文件不存在：{sourcePath}");
                return;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);

            // 目标文件不存在，直接复制
            if (!File.Exists(targetPath))
            {
                File.Copy(sourcePath, targetPath, true);
                Console.WriteLine($"文件不存在，已复制：{targetPath}");
                return;
            }

            // 先对比文件大小（快速筛选，大小不同直接复制）
            var s = new FileInfo(sourcePath);
            var d = new FileInfo(targetPath);
            if (s.Length != d.Length)
            {
                File.Copy(sourcePath, targetPath, true);
                Console.WriteLine($"文件大小不同，已覆盖：{targetPath}");
                return;
            }

            // 大小相同，再对比MD5（精准校验）
            if (GetFileMD5(sourcePath) != GetFileMD5(targetPath))
            {
                File.Copy(sourcePath, targetPath, true);
                Console.WriteLine($"文件MD5不同，已覆盖：{targetPath}");
            }
            else
            {
                Console.WriteLine($"文件内容一致，跳过复制：{targetPath}");
            }
        }

        // 核心方法2：仅当文件不同时复制整个文件夹（递归）
        private void CopyFolderIfDifferent(string sourceDir, string targetDir)
        {
            // 源目录不存在则返回
            if (!Directory.Exists(sourceDir))
            {
                Console.WriteLine($"源目录不存在：{sourceDir}");
                return;
            }

            Directory.CreateDirectory(targetDir); 

            // 遍历源目录下的所有文件
            foreach (string filePath in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(filePath);
                string targetFilePath = Path.Combine(targetDir, fileName);
                CopyFileIfDifferent(filePath, targetFilePath); // 复用单个文件的校验逻辑
            }

            // 递归处理子目录
            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string subDirName = Path.GetFileName(subDir);
                string targetSubDir = Path.Combine(targetDir, subDirName);
                Directory.CreateDirectory(targetSubDir); // 确保子目录存在
                CopyFolderIfDifferent(subDir, targetSubDir);
            }
        }

        // 辅助方法：计算文件的MD5哈希值
        private string GetFileMD5(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = new FileStream( // 防止被占用
                    filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    // 将字节数组转换为十六进制字符串
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        // ==========================
        //  多语言切换事件
        //  使用 WPFLocalizeExtension 库
        // ==========================

        // ini读取语言配置
        private void LoadLanguageFromConfig()
        {
            // 从 ini 读取语言，如果没有配置，默认用简体中文 "zh-CN"
            string savedLang = ReadConfigValue(LanguageKey, "zh-CN");
            // 应用语言
            LocalizeDictionary.Instance.Culture = new System.Globalization.CultureInfo(savedLang);
        }

        // 多语言菜单切换事件
        private void Lang_SC_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage("zh-CN");
        }
        private void Lang_TC_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage("zh-TW");
        }
        private void Lang_EN_Click(object sender, RoutedEventArgs e)
        {
            SetLanguage("en");
        }

        // 统一的语言切换方法
        private void SetLanguage(string cultureCode)
        {
            // 修改本地化扩展的当前文化
            LocalizeDictionary.Instance.Culture = new System.Globalization.CultureInfo(cultureCode);

            // 写入ini配置
            SaveGamePathToConfig(cultureCode, LanguageKey);
        }

        // 多语言资源统一调用类
        public static class L
        {
            public static string Get(string key)
            {
                return LocExtension.GetLocalizedValue<string>($"Pal5Mod_BeautifyRepair:Resources.Resources:{key}");
            }
        }

        // ==========================
        //  关于界面
        //  超链接点击跳转网页
        // ==========================

        // github
        private void github_Click(object sender, RoutedEventArgs e)
        {
            var link = sender as Hyperlink;
            OpenUrl(link?.NavigateUri?.AbsoluteUri);
        }
        private void github_MouseMove(object sender, MouseEventArgs e)
        {
            github.Foreground = new SolidColorBrush(Colors.Salmon); //鼠标经过颜色
        }
        private void github_MouseLeave(object sender, MouseEventArgs e)
        {
            github.Foreground = new SolidColorBrush(Colors.CornflowerBlue); //文字默认颜色
        }
        // gitee
        private void gitee_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            OpenUrl(link.NavigateUri?.AbsoluteUri);
        }
        private void gitee_MouseMove(object sender, MouseEventArgs e)
        {
            gitee.Foreground = new SolidColorBrush(Colors.Salmon); //鼠标经过颜色
        }
        private void gitee_MouseLeave(object sender, MouseEventArgs e)
        {
            gitee.Foreground = new SolidColorBrush(Colors.CornflowerBlue); //文字默认颜色
        }

        // ----------------------
        // 仙剑五怀旧版Mod1.06c  by:情姐姐
        // huaijiuMod
        // ----------------------
        private void huaijiuMod_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            OpenUrl(link.NavigateUri?.AbsoluteUri);
        }
        private void huaijiuMod_MouseMove(object sender, MouseEventArgs e)
        {
            huaijiuMod.Foreground = new SolidColorBrush(Colors.Salmon); //鼠标经过颜色
        }
        private void huaijiuMod_MouseLeave(object sender, MouseEventArgs e)
        {
            huaijiuMod.Foreground = new SolidColorBrush(Colors.CornflowerBlue); //文字默认颜色
        }

        // ----------------------
        // uvlist修改器 v2.0 by:百度贴吧—风靡义磊
        // uvlistTool
        // ----------------------
        private void uvlistTool_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            OpenUrl(link.NavigateUri?.AbsoluteUri);
        }
        private void uvlistTool_MouseMove(object sender, MouseEventArgs e)
        {
            uvlistTool.Foreground = new SolidColorBrush(Colors.Salmon); //鼠标经过颜色
        }
        private void uvlistTool_MouseLeave(object sender, MouseEventArgs e)
        {
            uvlistTool.Foreground = new SolidColorBrush(Colors.CornflowerBlue); //文字默认颜色
        }

        // ==========================
        // 滤镜插件-关联作者
        // ==========================
        //private void reshadeCN_Click(object sender, RoutedEventArgs e)
        //{
        //    Hyperlink link = sender as Hyperlink;
        //    OpenUrl(link.NavigateUri?.AbsoluteUri);
        //}
        //private void reshadeCN_MouseMove(object sender, MouseEventArgs e)
        //{
        //    reshadeCN.Foreground = new SolidColorBrush(Colors.Salmon); //鼠标经过颜色
        //}

        //private void reshadeCN_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    reshadeCN.Foreground = new SolidColorBrush(Colors.CornflowerBlue); //文字默认颜色
        //}

        // ==========================
        // 语音修复-官网链接
        // ==========================
        //private void voicelink_Click(object sender, RoutedEventArgs e)
        //{
        //    Hyperlink link = sender as Hyperlink;
        //    OpenUrl(link.NavigateUri?.AbsoluteUri);
        //}
        //private void voicelink_MouseMove(object sender, MouseEventArgs e)
        //{
        //    voicelink.Foreground = new SolidColorBrush(Colors.Salmon); //鼠标经过颜色
        //}
        //private void voicelink_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    voicelink.Foreground = new SolidColorBrush(Colors.CornflowerBlue); //文字默认颜色
        //}


        // ==========================
        // 菜单栏超链接
        // ==========================

        // 项目仓库-github
        private void Github_Click(object sender, RoutedEventArgs e)
        {
            // 在此处指定要打开的网页链接
            string url = "https://github.com/sarakale/Pal5_BeautifyRepair_Mod";

            // 使用默认浏览器打开网页
            Process.Start(url);
        }

        // 项目仓库-gitee
        private void Gitee_Click(object sender, RoutedEventArgs e)
        {
            // 在此处指定要打开的网页链接
            string url = "https://gitee.com/sarakale/Pal5_BeautifyRepair_Mod";

            // 使用默认浏览器打开网页
            Process.Start(url);
        }

        // 关于界面
        // 链接AboutMe.xaml
        private void AboutMe_Click(object sender, RoutedEventArgs e)
        {
            // 打开AboutMe.xaml窗口
            AboutMe newWindow = new AboutMe();
            // 新窗口初始位置在中间
            newWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newWindow.Show();
        }

        // 帮助手册
        private void Helpdoc_Click(object sender, RoutedEventArgs e)
        {
            // 打开帮助文档 html 网页
            Process.Start("Pal5Mod_BeautifyRepair\\HelpDoc\\HelpDoc_SC.html");
        }


        // 统一打开网址方法
        private void OpenUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;
            try
            {
                var psi = new ProcessStartInfo(url) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                // 根据项目风格展示错误（例如日志或消息框）
            }
        }

        // ==========================
        // 统一消息提示框
        // ==========================

        // 未选择路径提示
        //private void ShowNoPathMsg()
        //{
        //    CustomMaterialMessageBox msg = new CustomMaterialMessageBox
        //    {
        //        TxtMessage = { Text = "你还没有选择游戏路径呢！请在上面选择一下路径吧～", Foreground = Brushes.Black, FontSize = 18 }, //内容
        //        TxtTitle = { Text = "战斗UI界面隐藏", Foreground = Brushes.Black, FontSize = 18 }, //标题
        //        BtnOk = { Content = "确定", Background = Brushes.CornflowerBlue }, //确定按钮
        //        BtnCancel = { Content = "取消", Visibility = Visibility.Hidden }, //取消按钮
        //        MainContentControl = { Background = Brushes.White }, //布局背景颜色
        //        TitleBackgroundPanel = { Background = Brushes.Salmon },  //标题背景颜色
        //        BorderBrush = Brushes.Salmon, //边框颜色
        //    };
        //    msg.Show();
        //}

        //// 缺少Pal5Mod_BeautifyRepair资源文件夹提示
        //private void ShowMissingSupportMsg()
        //{
        //    CustomMaterialMessageBox msg = new CustomMaterialMessageBox
        //    {
        //        TxtMessage = { Text = "没有找到Pal5Mod_BeautifyRepair文件夹，请将Pal5Mod_BeautifyRepair文件夹放在“仙剑五美化修复Mod.exe”同级目录里。", Foreground = Brushes.Black, FontSize = 18 }, //内容
        //        TxtTitle = { Text = "战斗UI界面隐藏", Foreground = Brushes.Black, FontSize = 18 }, //标题
        //        BtnOk = { Content = "确定", Background = Brushes.CornflowerBlue }, //确定按钮
        //        BtnCancel = { Content = "取消", Visibility = Visibility.Hidden }, //取消按钮
        //        MainContentControl = { Background = Brushes.White }, //布局背景颜色
        //        TitleBackgroundPanel = { Background = Brushes.Salmon },  //标题背景颜色
        //        BorderBrush = Brushes.Salmon, //边框颜色
        //    };
        //    msg.Show();
        //}

        //// 应用成功提示
        //private void ShowSuccessMsg()
        //{
        //    CustomMaterialMessageBox msg = new CustomMaterialMessageBox
        //    {
        //        TxtMessage = { Text = "应用成功！\n\n如果游戏正在运行，请关闭游戏再重启运行查看效果。", Foreground = Brushes.Black, FontSize = 18 }, //内容
        //        TxtTitle = { Text = "战斗UI界面隐藏", FontSize = 18, Foreground = Brushes.White }, //标题
        //        BtnOk = { Content = "确定", Background = Brushes.CornflowerBlue }, //确定按钮
        //        BtnCancel = { Content = "取消", Visibility = Visibility.Hidden }, //取消按钮
        //        MainContentControl = { Background = Brushes.White }, //布局背景颜色
        //        TitleBackgroundPanel = { Background = Brushes.CornflowerBlue },  //标题背景颜色
        //        BorderBrush = Brushes.LightSteelBlue, //边框颜色
        //    };
        //    msg.Show();
        //}

        // 统一消息提示框方法
        // 不要使用 MaterialDesignThemes 主题的消息框，会造成闪退，建议使用系统自带的MessageBox
        private MessageBoxResult ShowMsg(
            string title,
            string message,
            MessageBoxImage icon = MessageBoxImage.Information,
            MessageBoxButton button = MessageBoxButton.OK)
        {
            return MessageBox.Show(message, title, button, icon);
        }

        // 统一检查路径函数-仙剑五
        private bool CheckGamePath(string title)
        {
            if (Pal5_GamePath == null || string.IsNullOrWhiteSpace(Pal5_GamePath.Text))
            {
                // 你还没有选择游戏路径呢！请在左菜单第一个选项选择一下路径吧～
                ShowMsg(title, L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                return false;
            }

            string exePath = Path.Combine(
                Pal5_GamePath.Text.Trim(),
                "Pal5.exe"
            );

            if (!File.Exists(exePath))
            {
                // 你还没有选择游戏路径呢！请在左菜单第一个选项选择一下路径吧～
                ShowMsg(title, L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // 统一检查路径函数-仙剑五前传
        private bool CheckPal5QPath(string title)
        {
            if (PAL5QPath == null || string.IsNullOrWhiteSpace(PAL5QPath.Text))
            {
                // 你还没有选择游戏路径呢！请在左菜单第一个选项选择一下路径吧～
                ShowMsg(title, L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                return false;
            }

            string exePath = Path.Combine(
                PAL5QPath.Text.Trim(),
                "Pal5Q.exe"
            );

            if (!File.Exists(exePath))
            {
                // 你还没有选择游戏路径呢！请在左菜单第一个选项选择一下路径吧～
                ShowMsg(title, L.Get("Msg_NoPathSelected"), MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // 统一检查Mod资源文件夹函数
        private bool CheckModResource(string title)
        {
            string modDir = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Pal5Mod_BeautifyRepair"
            );

            if (!Directory.Exists(modDir))
            {
                // 没有找到 Pal5Mod_BeautifyRepair 文件夹，请将其放在程序同级目录中。
                ShowMsg(title, L.Get("Msg_NoMODfolder"), MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // 统一文件复制方法
        private void CopyFileSafe(string source, string target)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(target));
            File.Copy(source, target, true);
        }

        private void CopyDirSafe(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);
            CopyFolderIfDifferent(sourceDir, targetDir); // 你已有的方法
        }

        // 设置“以管理员身份运行”方法
        static void SetRunAsAdmin(string exeFullPath)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                @"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers"))
            {
                key.SetValue(exeFullPath, "~ RUNASADMIN", RegistryValueKind.String);
            }
        }

        // 复制exe并设置“以管理员身份运行”
        static void CopyExeAndSetAdmin(string sourceExe, string targetDir)
        {

            Directory.CreateDirectory(targetDir);

            string targetExe = Path.Combine(targetDir, Path.GetFileName(sourceExe));
            File.Copy(sourceExe, targetExe,true);

            // 设置“以管理员身份运行”
            SetRunAsAdmin(targetExe);
        }

        // 复制 Pal5.exe（不设管理员）
        static void CopyPal5Exe(string sourceExe, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            string targetExe = Path.Combine(targetDir, "Pal5.exe");
            File.Copy(sourceExe, targetExe, true);

            // 确保 Pal5.exe 没有管理员兼容性
            RemoveRunAsAdmin(targetExe);
        }

        // 移除“以管理员身份运行”方法
        static void RemoveRunAsAdmin(string exeFullPath)
            {
                using (var key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers", true))
                {
                    key?.DeleteValue(exeFullPath, false);
                }
            }


        // ============================
    }


    // ============================
}

