using BespokeFusion;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using 仙剑五美化修复Mod;
//using FontFamily = System.Drawing.FontFamily;

namespace FontlistBox.Pal5Mod.UI
{
    /// <summary>
    /// Fontlistbox.xaml 的交互逻辑
    /// </summary>
    public partial class Fontlistbox : Window
    {
        private MainWindow mainWindow;

        public Fontlistbox(MainWindow mainWindow)
        {
            InitializeComponent();
            LoadFonts();

            // 引入主窗口和控件
            this.mainWindow = mainWindow;
            //string text = mainWindow.FontBoxText;

        }

        //--------------------------
        // 从系统已安装的字体列表选择
        //-------------------------

        /***************************

            字体位置：
            windows XP/7/8/10：C:\Windows\Fonts
            或者：%WINDIR%/Fonts
            注册表：HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts

            已测试不能通过添加完整路径引用，游戏不识别字体，只能复制文件过去到游戏Config\Data目录再改变ini文件配置。
            推荐ttf文件，otf虽然支持但不太清晰，可能要加载字体渲染引擎？
            字体家族属性：InstalledFontCollection

            参考文章：
            WPF加载系统本地字体列表示例：https://blog.csdn.net/dnazhd/article/details/106117267

        ****************************/

        private void LoadFonts()
        {
            // 获取系统中已安装的字体名称
            List<string> fontNames = new List<string>();

            foreach (System.Windows.Media.FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                Typeface typeface = new Typeface(fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
                GlyphTypeface glyphTypeface;
                if (typeface.TryGetGlyphTypeface(out glyphTypeface))
                {
                    // 获取字体名称
                    string fontName;
                    if (glyphTypeface.Win32FamilyNames.TryGetValue(System.Globalization.CultureInfo.CurrentUICulture, out fontName) ||
                        glyphTypeface.Win32FamilyNames.TryGetValue(System.Globalization.CultureInfo.CurrentCulture, out fontName) ||
                        glyphTypeface.Win32FamilyNames.TryGetValue(System.Globalization.CultureInfo.GetCultureInfo("en-us"), out fontName))
                    {
                        fontNames.Add(fontName);
                    }
                    else
                    {
                        fontNames.Add(fontFamily.Source);
                    }
                }
                else
                {
                    fontNames.Add(fontFamily.Source);
                }
            }
            fontNames.Sort();

            // 将字体名称添加到ListBox中
            foreach (string fontName in fontNames.Distinct())
            {
                fontListBox.Items.Add(fontName);
            }
        }

        // 字体列表
        private void FontListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 检测是否选择了项
            if (fontListBox.SelectedItem != null)
            {
                copyfontButton.IsEnabled = true;
                // FontPath.Text = fontFilePath;
                // 点击字体的同时改变字体预览
                string fontName = fontListBox.SelectedItem as string;
                DisplayFontCh.FontFamily = new System.Windows.Media.FontFamily(fontName);
                DisplayFontEn.FontFamily = new System.Windows.Media.FontFamily(fontName);
                DisplayFontCh.Text = "字体预览";
                DisplayFontEn.Text = "FontDisplay";
            }
            else
            {
                copyfontButton.IsEnabled = false;
            }
        }

        //-------------------------
        // 确定应用按钮
        //-------------------------
        private void CopyFontButton_Click(object sender, RoutedEventArgs e)
        {

            // 获取选中的字体名称
            string fontName = fontListBox.SelectedItem as string;

            // 获取字体文件路径
            System.Windows.Media.FontFamily fontFamily = new System.Windows.Media.FontFamily(fontName);
            Typeface typeface = new Typeface(fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
            GlyphTypeface glyphTypeface;

            if (typeface.TryGetGlyphTypeface(out glyphTypeface))
            {
                string fontFilePath = glyphTypeface.FontUri.OriginalString;

                // 将字体文件复制到目录Config\Data中并重命名
                string targetPath = mainWindow.Pal5_GamePath.Text + "\\Config\\Data\\" + "FontInfoName" + System.IO.Path.GetExtension(fontFilePath);
                // 创建目标文件夹（如果不存在）
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(targetPath));
                // 复制字体文件
                File.Copy(fontFilePath, targetPath, true);

                // 文件名写入fontinfo.ini
                List<string> fontSections = new List<string> { "[Font0]", "[Font1]", "[Font2]", "[Font3]", "[Font4]" };

                // 引用主窗口GamePath路径，写入Name后面的值
                string filePath = mainWindow.Pal5_GamePath.Text + "\\Config\\Data\\fontinfo.ini";

                // 文件编码，中文文件名称要用GB2312，其他情况用UTF-8
                // 或者将字体重命名英文名称再写入ini，否则游戏会无法显示字体
                // using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.GetEncoding("GB2312"))) 

                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (string section in fontSections)
                    {
                        writer.WriteLine(section);
                        writer.WriteLine("Name=" + "FontInfoName" + System.IO.Path.GetExtension(fontFilePath)); //重命名后的文件名
                        // writer.WriteLine("Name=" + System.IO.Path.GetFileName(fontFilePath)); //复制文件名
                    }

                    // 关闭读写
                    writer.Close();

                    // 消息框提示
                    ShowMsg(
                        "字体修改",
                        "字体更改成功！\n\n如果游戏正在运行，请关闭游戏再重启运行查看效果。\n如果未生效请再重启游戏或者更换字体。",
                        MessageBoxImage.Information
                    );
                }
            }
        }

        //-------------------------
        // 取消按钮
        // Fontclouse
        //-------------------------
        private void Fontclouse_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 字体路径
        private void FontPath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

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
    }
}
