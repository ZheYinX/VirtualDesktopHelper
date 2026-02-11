using System;
using System.Runtime.InteropServices;
using System.Security;

namespace VirtualDesktopHelper
{
	/// <summary>
	/// Windows API 辅助类，封装常用的Win32 API调用
	/// </summary>
	public static class Window_WinApiHelper
	{
		#region 窗口操作相关API

		/// <summary>
		/// 获取当前活动窗口的句柄
		/// </summary>
		/// <returns>当前活动窗口的句柄（HWND），如果失败返回IntPtr.Zero</returns>
		/// <example>
		/// <code>
		/// IntPtr hwnd = WinApiHelper.GetForegroundWindow();
		/// if (hwnd != IntPtr.Zero)
		/// {
		///     Console.WriteLine($"当前活动窗口句柄: {hwnd.ToInt64()}");
		/// }
		/// </code>
		/// </example>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetForegroundWindow();

		/// <summary>
		/// 将指定句柄的窗口设置为活动窗口
		/// </summary>
		/// <param name="hWnd">要设置为活动窗口的句柄</param>
		/// <returns>如果窗口被成功激活返回true，否则返回false</returns>
		/// <example>
		/// <code>
		/// IntPtr hwnd = WinApiHelper.GetForegroundWindow();
		/// // 其他操作...
		/// bool success = WinApiHelper.SetForegroundWindow(hwnd);
		/// if (success)
		/// {
		///     Console.WriteLine("窗口已成功激活");
		/// }
		/// </code>
		/// </example>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		/// <summary>
		/// 获取桌面窗口的句柄
		/// </summary>
		/// <returns>桌面窗口的句柄（HWND）</returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetDesktopWindow();

		/// <summary>
		/// 获取Shell桌面窗口的句柄（即桌面图标所在的窗口）
		/// </summary>
		/// <returns>Shell桌面窗口的句柄，如果失败返回IntPtr.Zero</returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetShellWindow();

		/// <summary>
		/// 检查指定窗口是否可见
		/// </summary>
		/// <param name="hWnd">窗口句柄</param>
		/// <returns>如果窗口可见返回true，否则返回false</returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		/// <summary>
		/// 检查指定句柄是否为有效的窗口句柄
		/// </summary>
		/// <param name="hWnd">要检查的句柄</param>
		/// <returns>如果是有效窗口句柄返回true，否则返回false</returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool IsWindow(IntPtr hWnd);

		#endregion

		#region 扩展方法

		/// <summary>
		/// 安全地获取当前活动窗口的句柄，包含错误处理
		/// </summary>
		/// <returns>当前活动窗口的句柄，如果失败返回IntPtr.Zero</returns>
		/// <exception cref="SecurityException">如果调用WinAPI时发生安全异常</exception>
		public static IntPtr UGetForegroundWindow()
		{
			IntPtr hwnd = GetForegroundWindow();
			if (hwnd == IntPtr.Zero)
			{
				int errorCode = Marshal.GetLastWin32Error();
				if (errorCode != 0)
				{
					throw new InvalidOperationException($"获取当前活动窗口失败，错误码: {errorCode}");
				}
			}
			return hwnd;
		}

		/// <summary>
		/// 安全地将指定句柄的窗口设置为活动窗口，包含错误处理
		/// </summary>
		/// <param name="hWnd">要设置为活动窗口的句柄</param>
		/// <returns>如果窗口被成功激活返回true，否则返回false</returns>
		/// <exception cref="ArgumentException">如果句柄无效</exception>
		/// <exception cref="SecurityException">如果调用WinAPI时发生安全异常</exception>
		public static bool USetForegroundWindow(IntPtr hWnd)
		{
			try
			{
				// 参数验证
				if (hWnd == IntPtr.Zero)
				{
					throw new ArgumentException("窗口句柄不能为空", nameof(hWnd));
				}

				if (!IsWindow(hWnd))
				{
					throw new ArgumentException("无效的窗口句柄", nameof(hWnd));
				}

				bool result = SetForegroundWindow(hWnd);
				if (!result)
				{
					int errorCode = Marshal.GetLastWin32Error();
					if (errorCode != 0)
					{
						throw new InvalidOperationException($"设置活动窗口失败，错误码: {errorCode}");
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				// 可以根据需要记录日志
				Console.WriteLine($"安全设置活动窗口失败: {ex.Message}");
				return false;
			}
		}

		/// <summary>
		/// 判断指定句柄是否为桌面窗口句柄
		/// </summary>
		/// <param name="hWnd">要检查的窗口句柄</param>
		/// <returns>如果是桌面窗口句柄返回true，否则返回false</returns>
		/// <example>
		/// <code>
		/// IntPtr hwnd = WinApiHelper.GetForegroundWindow();
		/// bool isDesktop = WinApiHelper.IsDesktopWindow(hwnd);
		/// Console.WriteLine($"当前窗口是否为桌面: {isDesktop}");
		/// </code>
		/// </example>
		public static bool IsDesktopWindow(IntPtr hWnd)
		{
			try
			{
				if (hWnd == IntPtr.Zero)
				{
					return false;
				}

				// 检查是否为桌面窗口（包含所有图标和背景的窗口）
				IntPtr desktopHwnd = GetDesktopWindow();
				if (hWnd == desktopHwnd)
				{
					return true;
				}

				// 检查是否为Shell桌面窗口（即Explorer.exe创建的桌面窗口）
				IntPtr shellHwnd = GetShellWindow();
				if (hWnd == shellHwnd)
				{
					return true;
				}

				return false;
			}
			catch (Exception ex)
			{
				// 可以根据需要记录日志
				Console.WriteLine($"判断是否为桌面窗口失败: {ex.Message}");
				return false;
			}
		}

		/// <summary>
		/// 判断当前活动窗口是否为桌面窗口
		/// </summary>
		/// <returns>如果当前活动窗口是桌面窗口返回true，否则返回false</returns>
		public static bool IsCurrentWindowDesktop()
		{
			IntPtr hwnd = UGetForegroundWindow();
			return IsDesktopWindow(hwnd);
		}

		#endregion
	}
}