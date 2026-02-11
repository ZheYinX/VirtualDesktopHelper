using System;
using System.Threading;
using System.Windows.Forms;

namespace VirtualDesktopHelper
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主要入口点
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 为单实例创建命名互斥锁
            // 互斥锁名称确保唯一性，防止程序多开
            bool isNewInstance;
            Mutex mutex = new Mutex(true, "VirtualDesktopHelper_SingleInstance", out isNewInstance);

            if (!isNewInstance)
            {
                // 另一个实例已在运行
                MessageBox.Show("虚拟桌面助手的一个实例已在运行！", 
                    "应用程序已在运行", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
				// 初始化应用程序配置（高DPI、默认字体等）
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				// 启动主窗口
				Application.Run(new MainForm());
            }
            finally
            {
                // 应用程序退出时释放互斥锁，确保资源正确释放
                if (mutex != null)
                {
                    mutex.ReleaseMutex();
                    mutex.Close();
                }
            }
        }
    }
}