using System;
using System.Runtime.InteropServices;

namespace VirtualDesktopHelper
{
    /// <summary>
    /// 定义Windows操作系统版本枚举
    /// </summary>
    public enum WindowsVersion
    {
        /// <summary>
        /// Windows 10操作系统
        /// </summary>
        Win10,
        
        /// <summary>
        /// Windows 11操作系统（版本低于24H2）
        /// </summary>
        Win11,
        
        /// <summary>
        /// Windows 11操作系统（版本24H2或更高）
        /// </summary>
        Win11_24h2,
        
        /// <summary>
        /// 其他不支持的系统版本（如低于Windows 10 1809的版本）
        /// </summary>
        Other
    }

    /// <summary>
    /// 操作系统版本检测工具类
    /// </summary>
    public static class OSVersionDetector
    {
        #region API声明
        
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool VerifyVersionInfo(ref OSVERSIONINFOEX lpVersionInfo, uint dwTypeMask, ulong dwlConditionMask);

        [StructLayout(LayoutKind.Sequential)]
        private struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public ushort wServicePackMajor;
            public ushort wServicePackMinor;
            public ushort wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int RtlGetVersion(out RTL_OSVERSIONINFOEX lpVersionInformation);

        [StructLayout(LayoutKind.Sequential)]
        private struct RTL_OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public ushort wServicePackMajor;
            public ushort wServicePackMinor;
            public ushort wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }
        
        #endregion

        /// <summary>
        /// 检测当前运行的Windows操作系统版本
        /// </summary>
        /// <returns>返回对应的WindowsVersion枚举值</returns>
        /// <exception cref="InvalidOperationException">当无法获取操作系统版本信息时抛出</exception>
        public static WindowsVersion GetWindowsVersion()
        {
            try
            {
                // 获取精确的内部版本号
                int buildNumber = GetBuildNumber();
                
                // 版本判断标准：Windows 10 1809 (内部版本号17763) 是最低支持的版本
                // 返回"Other"枚举值表示系统版本低于Windows 10 1809，不被支持
                
                if (buildNumber < 17763)
                {
                    // 内部版本号小于17763，为低于Windows 10 1809的不支持版本
                    return WindowsVersion.Other;
                }
                else if (buildNumber < 22000)
                {
                    // 内部版本号在17763-21999之间，为Windows 10 (1809及之后版本)
                    return WindowsVersion.Win10;
                }
                else if (buildNumber < 26100)
                {
                    // 内部版本号在22000-26099之间，为Windows 11（低于24H2）
                    return WindowsVersion.Win11;
                }
                else
                {
                    // 内部版本号26100及以上，为Windows 11 24H2或更高版本
                    return WindowsVersion.Win11_24h2;
                }
            }
            catch (Exception ex)
            {
                // 封装异常信息
                throw new InvalidOperationException("检测操作系统版本失败", ex);
            }
        }
        
        /// <summary>
        /// 使用非注册表方法获取Windows内部版本号
        /// </summary>
        /// <returns>返回Windows内部版本号</returns>
        /// <exception cref="InvalidOperationException">当无法获取内部版本号时抛出</exception>
        public static int GetBuildNumber()
        {
            // 方法1: 使用RtlGetVersion API获取精确的内部版本号（最可靠）
            try
            {
                RTL_OSVERSIONINFOEX osVersionInfo = new RTL_OSVERSIONINFOEX();
                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(osVersionInfo);
                
                int result = RtlGetVersion(out osVersionInfo);
                if (result >= 0)
                {
                    return osVersionInfo.dwBuildNumber;
                }
            }
            catch (Exception apiEx)
            {
                System.Diagnostics.Debug.WriteLine($"RtlGetVersion API调用失败: {apiEx.Message}");
            }
            
            // 方法2: 使用Environment.OSVersion作为回退方案
            try
            {
                Version osVersion = Environment.OSVersion.Version;
                if (osVersion.Major == 10)
                {
                    return osVersion.Build;
                }
            }
            catch (Exception envEx)
            {
                System.Diagnostics.Debug.WriteLine($"Environment获取失败: {envEx.Message}");
            }
            
            // 如果以上方法都失败，抛出异常
            throw new InvalidOperationException("无法获取有效的Windows内部版本号");
        }
    }
}