using System;
using System.Windows.Forms;

namespace VirtualDesktopHelper
{
	/// <summary>
	/// 热键捕获对话框
	/// </summary>
	public partial class HotkeyCaptureForm : Form
	{
		/// <summary>
		/// 当前捕获的主键（如A、B、1、2等）
		/// </summary>
		private Keys capturedKeys = Keys.None;

		/// <summary>
		/// 当前捕获的修饰键（Ctrl、Alt、Shift、Win等）
		/// </summary>
		private Keys capturedModifiers = Keys.None;

		/// <summary>
		/// 文本框占位符文本常量
		/// </summary>
		private const string PlaceholderText = "请输入热键";

		/// <summary>
		/// 热键捕获对话框构造函数
		/// </summary>
		/// <param name="keys">初始主键</param>
		/// <param name="mods">初始修饰键</param>
		public HotkeyCaptureForm(Keys keys, Keys mods)
		{
			InitializeComponent();

			// 使用传入的初始值初始化显示
			capturedKeys = keys;
			capturedModifiers = mods;

			UpdateDisplay();

			// 添加Load事件处理器以在窗体加载时设置焦点
			this.Load += HotkeyCaptureForm_Load;
		}

		private void HotkeyCaptureForm_Load(object sender, EventArgs e)
		{
			// 最初选择文本框以接收焦点
			this.hotkeyTextBox.Select();
		}

		/// <summary>
		/// 获取最终捕获的主键
		/// </summary>
		public Keys CapturedKeys => capturedKeys;

		/// <summary>
		/// 获取最终捕获的修饰键
		/// </summary>
		public Keys CapturedMods => capturedModifiers;

		#region 键盘事件处理（捕获热键的核心逻辑）

		/// <summary>
		/// 处理键盘按下事件，用于捕获热键
		/// </summary>
		/// <param name="sender">事件源</param>
		/// <param name="e">键盘事件参数</param>
		private void HotkeyCaptureForm_KeyDown(object sender, KeyEventArgs e)
		{
			// 忽略 Tab 与 Shift+Tab 以允许控件间导航
			if (e.KeyCode == Keys.Tab)
				return;

			HandleKeyCapture(e);
		}

		/// <summary>
		/// 从 KeyEventArgs 中解析并更新当前捕获的主键/修饰键
		/// 若只按下修饰键（包括Win键），则忽略；若有普通键则设置主键和修饰键
		/// </summary>
		/// <param name="e">键事件参数</param>
		private void HandleKeyCapture(KeyEventArgs e)
		{
			Keys tempModKeys = CalculateModifiers(e);

			Keys tempKey = e.KeyCode;
			if (!IsPureModifierKey(tempKey))
			{
				capturedKeys = tempKey;
				capturedModifiers = tempModKeys;
			}
			else
			{
				// 只按下修饰键时，忽略操作
				// 不更新任何捕获的键值
				return;
			}

			UpdateDisplay();

			e.Handled = true;
			e.SuppressKeyPress = true;
		}

		#endregion

		#region 按钮操作

		/// <summary>
		/// Win修饰键按钮点击事件
		/// 点击时将Win修饰键添加到当前修饰键集合
		/// </summary>
		private void BtnWin_Click(object sender, EventArgs e)
		{
			// 点击按钮时将 Win 修饰添加到当前修饰集合
			capturedModifiers |= (Keys)NHotkey.WindowsForms.ModKeys.Windows;
			UpdateDisplay();
		}

		/// <summary>
		/// 清除按钮点击事件
		/// 清除当前捕获的热键，恢复占位符显示
		/// </summary>
		private void BtnClear_Click(object sender, EventArgs e)
		{
			// 清除当前捕获的热键（恢复占位符）
			ResetCaptured();
		}

		/// <summary>
		/// 确认按钮点击事件
		/// 关闭对话框并返回OK结果，调用方通过CapturedKeys/CapturedMods读取捕获的热键
		/// </summary>
		private void BtnOK_Click(object sender, EventArgs e)
		{
			// 确认：关闭对话框并返回 OK，调用方通过 CapturedKeys/CapturedMods 读取
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		/// <summary>
		/// 取消按钮点击事件
		/// 不修改捕获结果，直接关闭对话框
		/// </summary>
		private void BtnCancel_Click(object sender, EventArgs e)
		{
			// 取消：不修改捕获结果，直接关闭
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		#endregion

		#region 辅助方法

		/// <summary>
		/// 获取显示文本或占位符
		/// 如果已捕获热键，则返回格式化的热键文本；否则返回占位符
		/// </summary>
		private string GetDisplayTextOrPlaceholder()
		{
			if (capturedKeys != Keys.None || capturedModifiers != Keys.None)
			{
				return HotkeyManager.FormatHotkey(capturedKeys, capturedModifiers);
			}
			return PlaceholderText;
		}

		/// <summary>
		/// 计算来自 KeyEventArgs 的修饰键集合
		/// </summary>
		/// <param name="e">键盘事件参数</param>
		/// <returns>计算出的修饰键集合</returns>
		private Keys CalculateModifiers(KeyEventArgs e)
		{
			Keys tempModKeys = Keys.None;
			if (e.Control) tempModKeys |= Keys.Control;
			if (e.Alt) tempModKeys |= Keys.Alt;
			if (e.Shift) tempModKeys |= Keys.Shift;

			// 检查 Win 键（NHotkey 的 Windows 标志）
			if ((Control.ModifierKeys & (Keys)NHotkey.WindowsForms.ModKeys.Windows) == (Keys)NHotkey.WindowsForms.ModKeys.Windows)
			{
				tempModKeys |= (Keys)NHotkey.WindowsForms.ModKeys.Windows; // 使用 NHotkey 的 Windows 标志来表示 Win 修饰
			}

			return tempModKeys;
		}

		/// <summary>
		/// 判断是否为纯修饰键（Control/Alt/Shift/Win）
		/// </summary>
		/// <param name="key">要检查的键</param>
		/// <returns>是否为纯修饰键</returns>
		private bool IsPureModifierKey(Keys key)
		{
			return key == Keys.ControlKey || key == Keys.Menu || key == Keys.ShiftKey ||
										key == Keys.LWin || key == Keys.RWin || key == (Keys)NHotkey.WindowsForms.ModKeys.Windows;
		}

		/// <summary>
		/// 更新文本框显示为当前捕获的热键组合（若存在）
		/// </summary>
		private void UpdateDisplay()
		{
			hotkeyTextBox.Text = GetDisplayTextOrPlaceholder();
		}

		/// <summary>
		/// 重置捕获状态并更新 UI
		/// 将捕获的主键和修饰键重置为None，并更新显示
		/// </summary>
		private void ResetCaptured()
		{
			capturedKeys = Keys.None;
			capturedModifiers = Keys.None;
			UpdateDisplay();
		}

		#endregion
	}
}