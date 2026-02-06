using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Pal5Mod_BeautifyRepair.UI
{
    /// <summary>
    /// AboutMe.xaml 的交互逻辑
    /// </summary>
    public partial class AboutMe : Window
    {
        public AboutMe()
        {
            InitializeComponent();
        }


        // ==========================
        //  关于界面
        //  超链接点击跳转网页
        // ==========================

        // github
        private void aboutgithub_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
        private void aboutgithub_MouseMove(object sender, MouseEventArgs e)
        {
            aboutgithub.Foreground = new SolidColorBrush(Colors.Salmon); //鼠标经过颜色
        }
        private void aboutgithub_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutgithub.Foreground = new SolidColorBrush(Colors.CornflowerBlue); //文字默认颜色
        }
        // gitee
        private void aboutgitee_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
        private void aboutgitee_MouseMove(object sender, MouseEventArgs e)
        {
            aboutgitee.Foreground = new SolidColorBrush(Colors.Salmon); //鼠标经过颜色
        }
        private void aboutgitee_MouseLeave(object sender, MouseEventArgs e)
        {
            aboutgitee.Foreground = new SolidColorBrush(Colors.CornflowerBlue); //文字默认颜色
        }
    }
}
