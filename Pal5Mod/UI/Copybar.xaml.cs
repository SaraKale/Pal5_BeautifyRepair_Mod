using BespokeFusion;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using 仙剑五美化修复Mod;

namespace copybar.Pal5Mod.UI
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Copybar : Window
    {
        // 文件路径
        private string sourceFolderPath;
        private string destinationFolderPath;

        private MainWindow mainWindow;
        public Copybar(MainWindow mainWindow)
        {
            InitializeComponent();
            // 引入主窗口和控件
            this.mainWindow = mainWindow;
        }

        // ==============================
        // 文件拷贝进度条 ProgressBar
        // 关联语音修复 VoiceRepair.cs
        // 参考文章：https://blog.csdn.net/TianXuanZhiZi_IT/article/details/128330298
        // ==============================

        /*****************************
          
        关于异步复制文件
        异步操作：在处理大量文件或大文件时，使用异步操作可以提高应用程序的响应性能。
        可以将文件复制操作改为异步方法，并使用async和await关键字来处理异步操作。
        以前的后台控件BackgroundWorker已过时，不推荐使用，新的方法是推荐使用async和await来复制文件。

        *******************************/

        // 读取文件路径位置
        public Copybar(string sourceFolderPath, string destinationFolderPath)
        {
            InitializeComponent();

            this.sourceFolderPath = sourceFolderPath;
            this.destinationFolderPath = destinationFolderPath;

            StartCopy();
        }

        // 开始复制文件
        private async void StartCopy()
        {
            try
            {
                await CopyFilesAsync(sourceFolderPath, destinationFolderPath);

                // 消息框提示
                ShowMsg(
                    "语音修复",
                    "修复成功！\n\n如果游戏正在运行，请关闭游戏再重启运行查看效果。",
                    MessageBoxImage.Information
                );

                // 文件复制完成后，关闭进度条窗口
                this.Close();
            }
            catch (Exception ex)
            {
                // 处理异常，显示错误消息框
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        // 异步操作更新进度条
        private async Task CopyFilesAsync(string sourceDir, string destDir)
        {
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            string[] files = Directory.GetFiles(sourceDir);

            int totalFiles = files.Length;
            int copiedFiles = 0;

            foreach (string file in files)
            {
                string destFile = System.IO.Path.Combine(destDir, System.IO.Path.GetFileName(file));
                await CopyFileAsync(file, destFile);

                copiedFiles++;

                // 更新ProgressBar进度条和进度文本
                double progressPercentage = (double)copiedFiles / totalFiles * 100;
                Dispatcher.Invoke(() =>
                {
                    copyBarWait.Value = progressPercentage;
                    // 更新显示进度百分比文本 FilePercentage
                    FilePercentage.Text = $"{progressPercentage:F2}%";
                });
            }

            string[] subdirectories = Directory.GetDirectories(sourceDir);

            foreach (string subdir in subdirectories)
            {
                string destSubdir = System.IO.Path.Combine(destDir, System.IO.Path.GetFileName(subdir));
                await CopyFilesAsync(subdir, destSubdir);
            }
        }

        private Task CopyFileAsync(string sourceFile, string destFile)
        {
            return Task.Run(() => File.Copy(sourceFile, destFile, true));
        }

        // 统一消息提示框方法
        private MessageBoxResult ShowMsg(
            string title,
            string message,
            MessageBoxImage icon = MessageBoxImage.Information,
            MessageBoxButton button = MessageBoxButton.OK)
        {
            return MessageBox.Show(message, title, button, icon);
        }
    }
}
