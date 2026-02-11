using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VirtualDesktopHelper
{
    public static class IniFileHelper
    {
        // 导入Windows API函数
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool WritePrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFileName);

        private const string CONFIG_FILE_NAME = "config.ini";
        private static string ConfigFilePath => System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONFIG_FILE_NAME);

        /// <summary>
        /// 从INI文件读取指定节和键的值
        /// </summary>
        /// <param name="section">节名称</param>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>读取到的值，如果不存在则返回默认值</returns>
        public static string ReadValue(string section, string key, string defaultValue)
        {
            const int bufferSize = 255;
            StringBuilder sb = new StringBuilder(bufferSize);
            GetPrivateProfileString(section, key, defaultValue, sb, sb.Capacity, ConfigFilePath);
            return sb.ToString();
        }

        /// <summary>
        /// 写入值到INI文件的指定节和键
        /// </summary>
        /// <param name="section">节名称</param>
        /// <param name="key">键名称</param>
        /// <param name="value">要写入的值</param>
        public static void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, ConfigFilePath);
        }

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        /// <returns>配置文件的完整路径</returns>
        public static string GetConfigFilePath()
        {
            return ConfigFilePath;
        }
    }
}