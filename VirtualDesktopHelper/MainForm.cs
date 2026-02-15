using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualDesktop.Unify;

namespace VirtualDesktopHelper
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 热键存储（每种功能一个主键 + 修饰键）
		/// </summary>
		/// <remarks>
		/// 左移窗口到前一个虚拟桌面的热键
		/// </remarks>
		private Keys leftMoveHotkey = Keys.None;

		/// <remarks>
		/// 右移窗口到后一个虚拟桌面的热键
		/// </remarks>
		private Keys rightMoveHotkey = Keys.None;

		/// <remarks>
		/// 固定/取消固定窗口到所有虚拟桌面的热键
		/// </remarks>
		private Keys pinWindowHotkey = Keys.None;

		/// <remarks>
		/// 左移窗口的修饰键
		/// </remarks>
		private Keys leftMoveModKeys = Keys.None;

		/// <remarks>
		/// 右移窗口的修饰键
		/// </remarks>
		private Keys rightMoveModKeys = Keys.None;

		/// <remarks>
		/// 固定窗口的修饰键
		/// </remarks>
		private Keys pinWindowModKeys = Keys.None;

		/// <summary>
		/// 系统托盘图标
		/// </summary>
		private NotifyIcon trayIcon;

		/// <summary>
		/// 系统托盘上下文菜单
		/// </summary>
		private ContextMenuStrip trayMenu;

		/// <summary>
		/// 配置文件中的热键节名称
		/// </summary>
		private const string SectionHotkeys = "Hotkeys";

		/// <summary>
		/// 配置文件中的设置节名称
		/// </summary>
		private const string SectionSettings = "Settings";

		/// <summary>
		/// 设置键名：移动窗口时同时切换虚拟桌面
		/// </summary>
		private const string KeySwitchDesktopOnMove = "SwitchDesktopOnMove";

		/// <summary>
		/// 移动窗口时是否同时切换虚拟桌面
		/// </summary>
		private bool switchDesktopOnMove = false;

		IUnifyVirtualDesktop UVirtualDesktop;
		/// <summary>
		/// 未设置热键时的显示文本
		/// </summary>
		private const string DisplayNotSet = "未设置";

		/// <summary>
		/// 主窗口构造函数
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		// 初始化
		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
				UVirtualDesktop = UnifyInterfaceManager.GetInterfaceByOs();
				if (UVirtualDesktop == null)
					throw new Exception("获取Windows虚拟桌面操作接口失败！可能是不受支持的系统版本！");

				AttachControlEvents(); // 绑定控件事件
				SetupTrayIcon();      // 设置系统托盘
				LoadSettings();       // 加载配置

			}
			catch (Exception x)
			{
				MessageBox.Show(x.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
				ExitApplication();
			}
		}

		#region 事件绑定与打开热键捕获窗口

		/// <summary>
		/// 绑定界面上按钮到打开热键捕获对话框的逻辑
		/// </summary>
		private void AttachControlEvents()
		{
			// 使用封装方法减少重复代码
			AddOpenHotkeyClickHandler(btnLeftMove, txtLeftMove, HotkeyAction.LeftMove);
			AddOpenHotkeyClickHandler(btnRightMove, txtRightMove, HotkeyAction.RightMove);
			AddOpenHotkeyClickHandler(btnPinWindow, txtPinWindow, HotkeyAction.PinWindow);
		}

		/// <summary>
		/// 添加热键捕获对话框的点击事件处理程序
		/// </summary>
		/// <param name="button">按钮控件</param>
		/// <param name="textBox">显示热键的文本框</param>
		/// <param name="hotkeyAction">热键操作类型</param>
		private void AddOpenHotkeyClickHandler(Button button, TextBox textBox, HotkeyAction hotkeyAction)
		{
			button.Click += (s, e) => OpenHotkeyCaptureByAction(textBox, hotkeyAction);
		}

		/// <summary>
		/// 通过操作类型打开热键捕获对话框
		/// 使用枚举以减少错误
		/// </summary>
		/// <param name="textBox">显示热键的文本框</param>
		/// <param name="hotkeyAction">热键操作类型</param>
		private void OpenHotkeyCaptureByAction(TextBox textBox, HotkeyAction hotkeyAction)
		{
			// 根据操作类型获取当前热键和修饰键
			GetHotkeyPairByAction(hotkeyAction, out Keys currentHotkey, out Keys currentModKeys);

			// 创建热键捕获对话框并显示
			HotkeyCaptureForm captureForm = new HotkeyCaptureForm(currentHotkey, currentModKeys);
			if (captureForm.ShowDialog() == DialogResult.OK)
			{
				// 将捕获结果保存回对应的字段
				SetHotkeyPairByAction(hotkeyAction, captureForm.CapturedKeys, captureForm.CapturedMods);

				// 更新文本框显示
				textBox.Text = captureForm.CapturedKeys != Keys.None ?
												HotkeyManager.FormatHotkey(captureForm.CapturedKeys, captureForm.CapturedMods) : DisplayNotSet;

				// 注册更新后的热键
				RegisterHotkeys();
			}
		}

		#endregion

		#region 热键类型映射辅助

		/// <summary>
		/// 热键操作类型枚举
		/// 使用枚举替代字符串标识，统一管理热键类型
		/// </summary>
		private enum HotkeyAction { LeftMove, RightMove, PinWindow }

		/// <summary>
		/// 根据操作类型获取对应的热键对
		/// </summary>
		/// <param name="action">操作类型</param>
		/// <param name="hotkey">输出热键</param>
		/// <param name="modKeys">输出修饰键</param>
		private void GetHotkeyPairByAction(HotkeyAction action, out Keys hotkey, out Keys modKeys)
		{
			switch (action)
			{
				case HotkeyAction.LeftMove:
					hotkey = leftMoveHotkey; modKeys = leftMoveModKeys; break;
				case HotkeyAction.RightMove:
					hotkey = rightMoveHotkey; modKeys = rightMoveModKeys; break;
				case HotkeyAction.PinWindow:
					hotkey = pinWindowHotkey; modKeys = pinWindowModKeys; break;
				default:
					hotkey = Keys.None; modKeys = Keys.None; break;
			}
		}

		/// <summary>
		/// 根据操作类型设置对应的热键对
		/// </summary>
		/// <param name="action">操作类型</param>
		/// <param name="hotkey">热键</param>
		/// <param name="modKeys">修饰键</param>
		private void SetHotkeyPairByAction(HotkeyAction action, Keys hotkey, Keys modKeys)
		{
			switch (action)
			{
				case HotkeyAction.LeftMove:
					leftMoveHotkey = hotkey; leftMoveModKeys = modKeys; break;
				case HotkeyAction.RightMove:
					rightMoveHotkey = hotkey; rightMoveModKeys = modKeys; break;
				case HotkeyAction.PinWindow:
					pinWindowHotkey = hotkey; pinWindowModKeys = modKeys; break;
			}
		}

		/// <summary>
		/// 将枚举转为用于注册/保存的字符串标识
		/// 使用枚举名称作为标识，确保唯一性和一致性
		/// </summary>
		/// <param name="action">操作类型</param>
		/// <returns>字符串标识</returns>
		private static string GetActionId(HotkeyAction action) => action.ToString();

		#endregion

		#region 窗体快捷键预览与托盘相关

		/// <summary>
		/// 处理窗体预览按键事件，用于响应ESC键隐藏主窗体
		/// </summary>
		private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Hide();
				e.IsInputKey = true;
			}
		}

		/// <summary>
		/// 重写ProcessCmdKey方法以处理ESC键隐藏窗口功能
		/// </summary>
		/// <param name="msg">按键消息</param>
		/// <param name="keyData">按键数据</param>
		/// <returns>是否处理了按键消息</returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				// 按ESC键时隐藏窗口（不关闭，仅更改可见性）
				this.Hide();
				return true; // 表示已处理该按键消息
			}

			// 对于其他按键，调用基类实现
			return base.ProcessCmdKey(ref msg, keyData);
		}

		// 设置托盘
		private void SetupTrayIcon()
		{
			trayMenu = new ContextMenuStrip();
			trayMenu.Items.Add(CreateMenuItem("显示主窗口", (s, e) => ShowMainWindow()));
			trayMenu.Items.Add(CreateMenuItem("退出程序", (s, e) => ExitApplication()));

			trayIcon = new NotifyIcon();
			trayIcon.Icon = SystemIcons.Application; // 使用默认图标
			trayIcon.ContextMenuStrip = trayMenu;
			trayIcon.Visible = true;
			trayIcon.DoubleClick += (s, e) => ShowMainWindow();
			trayIcon.Text = "虚拟桌面助手";
		}

		/// <summary>
		/// 显示托盘通知
		/// </summary>
		/// <param name="title">通知标题</param>
		/// <param name="message">通知内容</param>
		/// <param name="icon">通知图标类型</param>
		/// <param name="timeout">显示时长（毫秒），默认5000毫秒</param>
		private void ShowTrayNotification(string title, string message, ToolTipIcon icon = ToolTipIcon.Info, int timeout = 5000)
		{
			try
			{
				if (trayIcon != null && !string.IsNullOrEmpty(message))
				{
					// 确保标题和消息不为null
					title = title ?? "";
					message = message ?? "";

					// 限制消息长度，避免显示问题
					if (message.Length > 256)
					{
						message = message.Substring(0, 253) + "...";
					}

					// 显示托盘通知
					trayIcon.ShowBalloonTip(timeout, title, message, icon);
				}
			}
			catch (Exception ex)
			{
				// 捕获所有异常，避免影响主程序
				Console.WriteLine($"显示托盘通知失败: {ex.Message}");
			}
		}

		private ToolStripMenuItem CreateMenuItem(string text, EventHandler onClick)
		{
			var item = new ToolStripMenuItem(text);
			item.Click += onClick;
			return item;
		}

		private void ShowMainWindow()
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;
		}

		private void ExitApplication()
		{
			this.Visible = false;
			if (trayIcon != null)
			{
				trayIcon.Visible = false;
			}
			Application.Exit();
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			// 应用退出前注销所有热键
			UnregisterAllHotkeys();
		}

		#endregion

		#region 保存/加载配置

		private void BtnSave_Click(object sender, EventArgs e)
		{
					SaveSettings();
			MessageBox.Show("设置已成功保存！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// 从INI文件加载热键设置
		/// </summary>
		private void LoadSettings()
		{
			// 加载左移窗口热键
			LoadHotkey(HotkeyAction.LeftMove, ref leftMoveHotkey, ref leftMoveModKeys);
			// 加载右移窗口热键
			LoadHotkey(HotkeyAction.RightMove, ref rightMoveHotkey, ref rightMoveModKeys);
			// 加载固定窗口热键
			LoadHotkey(HotkeyAction.PinWindow, ref pinWindowHotkey, ref pinWindowModKeys);

			// 加载设置选项
			LoadSettingsFromConfig();

			UpdateHotkeyUI();
			RegisterHotkeys();
		}

		/// <summary>
		/// 从配置文件加载设置选项
		/// </summary>
		private void LoadSettingsFromConfig()
		{
			try
			{
				string value = IniFileHelper.ReadValue(SectionSettings, KeySwitchDesktopOnMove, "true");
				if (bool.TryParse(value, out bool result))
				{
					switchDesktopOnMove = result;
					chkSwitchDesktopOnMove.Checked = result;
				}
				else
				{
					// 如果解析失败，使用默认值
					switchDesktopOnMove = false;
					chkSwitchDesktopOnMove.Checked = false;
				}
			}
			catch (Exception ex)
			{
				// 加载设置失败时使用默认值
				switchDesktopOnMove = false;
				chkSwitchDesktopOnMove.Checked = false;
				ShowTrayNotification("警告", "加载设置失败，使用默认值: " + ex.Message, ToolTipIcon.Warning);
			}
		}

		/// <summary>
		/// 从INI文件加载单个热键设置
		/// </summary>
		/// <param name="action">热键操作类型</param>
		/// <param name="hotkey">输出热键</param>
		/// <param name="mods">输出修饰键</param>
		private void LoadHotkey(HotkeyAction action, ref Keys hotkey, ref Keys mods)
		{
			string value = IniFileHelper.ReadValue(SectionHotkeys, GetActionId(action), "");
			HotkeyManager.ParseHotkeyString(value, out hotkey, out mods);
		}

		/// <summary>
		/// 将热键设置保存到INI文件
		/// </summary>
		private void SaveSettings()
		{
			// 保存左移窗口热键
			SaveHotkey(HotkeyAction.LeftMove, leftMoveHotkey, leftMoveModKeys);
			// 保存右移窗口热键
			SaveHotkey(HotkeyAction.RightMove, rightMoveHotkey, rightMoveModKeys);
			// 保存固定窗口热键
			SaveHotkey(HotkeyAction.PinWindow, pinWindowHotkey, pinWindowModKeys);

			// 保存设置选项
			SaveSettingsToConfig();
		}

		/// <summary>
		/// 将设置选项保存到配置文件
		/// </summary>
		private void SaveSettingsToConfig()
		{
			try
			{
				// 更新设置值
				switchDesktopOnMove = chkSwitchDesktopOnMove.Checked;
				IniFileHelper.WriteValue(SectionSettings, KeySwitchDesktopOnMove, switchDesktopOnMove.ToString());
			}
			catch (Exception ex)
			{
				ShowTrayNotification("错误", "保存设置失败: " + ex.Message, ToolTipIcon.Error);
			}
		}

		/// <summary>
		/// 将单个热键设置保存到INI文件
		/// </summary>
		/// <param name="action">热键操作类型</param>
		/// <param name="hotkey">热键</param>
		/// <param name="mods">修饰键</param>
		private void SaveHotkey(HotkeyAction action, Keys hotkey, Keys mods)
		{
			IniFileHelper.WriteValue(SectionHotkeys, GetActionId(action), HotkeyManager.FormatHotkeyForStorage(hotkey, mods));
		}

		#endregion

		#region 热键注册与回调

		/// <summary>
		/// 注册所有热键
		/// </summary>
		private void RegisterHotkeys()
		{
			try
			{
				UnregisterAllHotkeys();

				var failures = new System.Collections.Generic.List<string>();

				// 注册左移窗口热键
				if (!TryRegisterHotkey(GetActionId(HotkeyAction.LeftMove), leftMoveHotkey, leftMoveModKeys, OnLeftMoveHotkeyPressed, out string fail1))
				{
					failures.Add(fail1);
				}

				// 注册右移窗口热键
				if (!TryRegisterHotkey(GetActionId(HotkeyAction.RightMove), rightMoveHotkey, rightMoveModKeys, OnRightMoveHotkeyPressed, out string fail2))
				{
					failures.Add(fail2);
				}

				// 注册固定窗口热键
				if (!TryRegisterHotkey(GetActionId(HotkeyAction.PinWindow), pinWindowHotkey, pinWindowModKeys, OnPinWindowHotkeyPressed, out string fail3))
				{
					failures.Add(fail3);
				}

				// 如果存在注册失败，使用托盘气泡合并提示用户；若托盘未初始化则回落到 MessageBox
				if (failures.Count > 0)
				{
					string combined = string.Join("; ", failures);
					// 使用通用托盘通知方法
					ShowTrayNotification("热键注册失败", combined, ToolTipIcon.Warning);

					// 如果托盘通知失败，回落到MessageBox
					if (trayIcon == null)
					{
						MessageBox.Show(combined, "热键注册失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"注册热键失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private bool TryRegisterHotkey(string id, Keys hotkey, Keys mods, EventHandler<NHotkey.HotkeyEventArgs> handler, out string failureMessage)
		{
			failureMessage = string.Empty;
			if (hotkey != Keys.None)
			{
				try
				{
					Keys combined = mods | hotkey;
					NHotkey.WindowsForms.HotkeyManager.Current.AddOrReplace(id, combined, handler);
					return true;
				}
				catch (Exception ex)
				{
					// 构造失败描述，包含热键显示和异常信息的简要说明
					string hotkeyText = HotkeyManager.FormatHotkey(hotkey, mods);
					failureMessage = string.IsNullOrEmpty(hotkeyText) ?
									$"{id}: 注册失败 ({ex.Message})" :
									$"{id} ({hotkeyText}): 注册失败 ({ex.Message})";
					return false;
				}
			}
			// 如果没有设置热键，视为成功（无须注册）
			return true;
		}

		/// <summary>
		/// 注销所有热键
		/// </summary>
		private void UnregisterAllHotkeys()
		{
			try
			{
				string[] keys = new[] { GetActionId(HotkeyAction.LeftMove), GetActionId(HotkeyAction.RightMove), GetActionId(HotkeyAction.PinWindow) };
				foreach (var k in keys)
				{
					NHotkey.WindowsForms.HotkeyManager.Current.Remove(k);
				}
			}
			catch
			{
				// 忽略注销过程中的错误
			}
		}

		void MoveWindow(bool direction)
		{
			// 判断是否为桌面窗口
			if (Window_WinApiHelper.IsCurrentWindowDesktop())
			{
				System.Media.SystemSounds.Beep.Play();
				return;
			}

			IntPtr h = IntPtr.Zero;
			if (chkSwitchDesktopOnMove.Checked)
				h = Window_WinApiHelper.UGetForegroundWindow();

			if (UVirtualDesktop?.MoveWindow(direction, chkSwitchDesktopOnMove.Checked) != true)
				System.Media.SystemSounds.Beep.Play();
			else
			{
				if (h != IntPtr.Zero)
					Window_WinApiHelper.USetForegroundWindow(h);
			}
		}

		private void OnLeftMoveHotkeyPressed(object sender, NHotkey.HotkeyEventArgs e)
		{
			MoveWindow(true);
		}

		private void OnRightMoveHotkeyPressed(object sender, NHotkey.HotkeyEventArgs e)
		{
			MoveWindow(false);
		}

		private void OnPinWindowHotkeyPressed(object sender, NHotkey.HotkeyEventArgs e)
		{
			// 是否为桌面窗口
			if (Window_WinApiHelper.IsCurrentWindowDesktop())
			{
				System.Media.SystemSounds.Beep.Play();
				return;
			}

			IntPtr h = Window_WinApiHelper.UGetForegroundWindow();
			if (h != IntPtr.Zero)
			{
				if (UVirtualDesktop != null)
					if (!UVirtualDesktop.IsPinWindow(h))
					{
						System.Media.SystemSounds.Asterisk.Play();
						UVirtualDesktop.PinWindow(h);
					}
					else
					{
						System.Media.SystemSounds.Exclamation.Play();
						UVirtualDesktop.UnpinWindow(h);
					}
			}
		}

		#endregion

		/// <summary>
		/// 更新热键UI显示，同步热键设置到界面上的文本框
		/// </summary>
		private void UpdateHotkeyUI()
		{
			txtLeftMove.Text = FormatOrDefault(leftMoveHotkey, leftMoveModKeys);
			txtRightMove.Text = FormatOrDefault(rightMoveHotkey, rightMoveModKeys);
			txtPinWindow.Text = FormatOrDefault(pinWindowHotkey, pinWindowModKeys);
		}

		private string FormatOrDefault(Keys key, Keys mods)
		{
			return key != Keys.None ? HotkeyManager.FormatHotkey(key, mods) : DisplayNotSet;
		}

	}
}