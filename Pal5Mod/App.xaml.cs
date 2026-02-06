using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using WPFLocalizeExtension.Engine; 

namespace 仙剑五美化修复Mod
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        // 检查是否以管理员权限运行 + 多语言初始化
        protected override void OnStartup(StartupEventArgs e)
        {
            //AllocConsole(); // 开启控制台
            //Console.WriteLine("控制台初始化成功！");
            //Console.WriteLine("WPF 即将启动…");

            // ① 管理员自检
            if (!IsRunAsAdmin())
            {
                RestartAsAdmin();
                return; // 非常重要
            }

            // ② 多语言核心初始化：检测系统文化 → 配置WPFLocalizeExtension库的文化
            InitLocalizationBySystemCulture();

            // 执行基类初始化
            base.OnStartup(e);
        }

        /// <summary>
        /// 根据系统区域语言初始化WPFLocalizeExtension的本地化文化
        /// 规则：简体中文→zh-CN | 港澳台→zh-TW | 其他→en
        /// </summary>
        private void InitLocalizationBySystemCulture()
        {
            // 获取系统当前UI文化（系统区域语言设置）
            CultureInfo systemUiCulture = CultureInfo.CurrentUICulture;
            string targetCultureCode = "en"; // 默认英文兜底

            // 自定义匹配规则
            if (systemUiCulture.Name.StartsWith("zh-"))
            {
                switch (systemUiCulture.Name)
                {
                    case "zh-CN":
                        targetCultureCode = "zh-CN";
                        break;
                    case "zh-TW":
                    case "zh-HK":
                    case "zh-MO":
                        targetCultureCode = "zh-TW";
                        break;
                    // 其他中文变体（如zh-SG）也指向简体中文，可选
                    default:
                        targetCultureCode = "zh-CN";
                        break;
                }
            }
            // 3. 非中文系统：统一指向英文（en）
            else
            {
                targetCultureCode = "en";
            }

            // 核心操作：设置WPFLocalizeExtension库的全局文化（库唯一识别的文化设置）
            LocalizeDictionary.Instance.Culture = new CultureInfo(targetCultureCode);
        }

        // 管理员检测函数（原有逻辑，保留）
        static bool IsRunAsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        // 自提权重启（原有逻辑，保留）
        static void RestartAsAdmin()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = Assembly.GetExecutingAssembly().Location,
                UseShellExecute = true,
                Verb = "runas"
            };

            try
            {
                Process.Start(psi);
            }
            catch
            {
                // 用户点了“否”，什么都不做
                return;
            }

            Environment.Exit(0);
        }


        // =======================================
    }

}
