using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualDesktopHelper
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // 热键相关控件声明
        private GroupBox grpLeftMove;
        private GroupBox grpRightMove;
        private GroupBox grpPinWindow;
        private GroupBox grpSettings;
        private TextBox txtLeftMove;
        private TextBox txtRightMove;
        private TextBox txtPinWindow;
        private Button btnLeftMove;
        private Button btnRightMove;
        private Button btnPinWindow;
        private Button btnSave;
        private Button btnStartup;
        private FlowLayoutPanel bottomPanel;
        private CheckBox chkSwitchDesktopOnMove;
        private TableLayoutPanel mainPanel;
        private TableLayoutPanel leftMovePanel;
        private TableLayoutPanel rightMovePanel;
        private TableLayoutPanel pinWindowPanel;
        private TableLayoutPanel settingsPanel;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
			this.grpLeftMove = new System.Windows.Forms.GroupBox();
			this.leftMovePanel = new System.Windows.Forms.TableLayoutPanel();
			this.txtLeftMove = new System.Windows.Forms.TextBox();
			this.btnLeftMove = new System.Windows.Forms.Button();
			this.grpRightMove = new System.Windows.Forms.GroupBox();
			this.rightMovePanel = new System.Windows.Forms.TableLayoutPanel();
			this.txtRightMove = new System.Windows.Forms.TextBox();
			this.btnRightMove = new System.Windows.Forms.Button();
			this.grpPinWindow = new System.Windows.Forms.GroupBox();
			this.pinWindowPanel = new System.Windows.Forms.TableLayoutPanel();
			this.txtPinWindow = new System.Windows.Forms.TextBox();
			this.btnPinWindow = new System.Windows.Forms.Button();
			this.grpSettings = new System.Windows.Forms.GroupBox();
			this.settingsPanel = new System.Windows.Forms.TableLayoutPanel();
			this.chkSwitchDesktopOnMove = new System.Windows.Forms.CheckBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnStartup = new System.Windows.Forms.Button();
			this.bottomPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.mainPanel.SuspendLayout();
			this.grpLeftMove.SuspendLayout();
			this.leftMovePanel.SuspendLayout();
			this.grpRightMove.SuspendLayout();
			this.rightMovePanel.SuspendLayout();
			this.grpPinWindow.SuspendLayout();
			this.pinWindowPanel.SuspendLayout();
			this.grpSettings.SuspendLayout();
			this.settingsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.ColumnCount = 1;
			this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.mainPanel.Controls.Add(this.grpLeftMove, 0, 0);
			this.mainPanel.Controls.Add(this.grpRightMove, 0, 1);
			this.mainPanel.Controls.Add(this.grpPinWindow, 0, 2);
			this.mainPanel.Controls.Add(this.grpSettings, 0, 3);
			this.mainPanel.Controls.Add(this.bottomPanel, 0, 4);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.RowCount = 5;
			this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.mainPanel.Size = new System.Drawing.Size(484, 261);
			this.mainPanel.TabIndex = 0;
			// 
			 // bottomPanel
			 // 
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bottomPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
			this.bottomPanel.Location = new System.Drawing.Point(3, 223);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(478, 35);
			this.bottomPanel.TabIndex = 5;
			this.bottomPanel.Controls.Add(this.btnSave);
			this.bottomPanel.Controls.Add(this.btnStartup);
			// 
			// grpLeftMove
			// 
			this.grpLeftMove.Controls.Add(this.leftMovePanel);
			this.grpLeftMove.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpLeftMove.Location = new System.Drawing.Point(3, 3);
			this.grpLeftMove.Name = "grpLeftMove";
			this.grpLeftMove.Size = new System.Drawing.Size(478, 49);
			this.grpLeftMove.TabIndex = 0;
			this.grpLeftMove.TabStop = false;
			this.grpLeftMove.Text = "左移虚拟桌面热键";
			// 
			// leftMovePanel
			// 
			this.leftMovePanel.ColumnCount = 2;
			this.leftMovePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.leftMovePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.leftMovePanel.Controls.Add(this.txtLeftMove, 0, 0);
			this.leftMovePanel.Controls.Add(this.btnLeftMove, 1, 0);
			this.leftMovePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.leftMovePanel.Location = new System.Drawing.Point(3, 17);
			this.leftMovePanel.Name = "leftMovePanel";
			this.leftMovePanel.RowCount = 1;
			this.leftMovePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.leftMovePanel.Size = new System.Drawing.Size(472, 29);
			this.leftMovePanel.TabIndex = 0;
			// 
			// txtLeftMove
			// 
			this.txtLeftMove.AccessibleDescription = "显示用于将当前窗口移动到左侧虚拟桌面的热键组合";
			this.txtLeftMove.AccessibleName = "左移虚拟桌面热键";
			this.txtLeftMove.BackColor = System.Drawing.SystemColors.Window;
			this.txtLeftMove.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLeftMove.Location = new System.Drawing.Point(3, 3);
			this.txtLeftMove.Name = "txtLeftMove";
			this.txtLeftMove.ReadOnly = true;
			this.txtLeftMove.Size = new System.Drawing.Size(324, 21);
			this.txtLeftMove.TabIndex = 0;
			this.txtLeftMove.Text = "未设置";
			// 
			// btnLeftMove
			// 
			this.btnLeftMove.Location = new System.Drawing.Point(333, 3);
			this.btnLeftMove.Name = "btnLeftMove";
			this.btnLeftMove.Size = new System.Drawing.Size(80, 21);
			this.btnLeftMove.TabIndex = 1;
			this.btnLeftMove.Text = "设置";
			this.btnLeftMove.UseVisualStyleBackColor = true;
			// 
			// grpRightMove
			// 
			this.grpRightMove.Controls.Add(this.rightMovePanel);
			this.grpRightMove.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpRightMove.Location = new System.Drawing.Point(3, 58);
			this.grpRightMove.Name = "grpRightMove";
			this.grpRightMove.Size = new System.Drawing.Size(478, 49);
			this.grpRightMove.TabIndex = 1;
			this.grpRightMove.TabStop = false;
			this.grpRightMove.Text = "右移虚拟桌面热键";
			// 
			// rightMovePanel
			// 
			this.rightMovePanel.ColumnCount = 2;
			this.rightMovePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.rightMovePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.rightMovePanel.Controls.Add(this.txtRightMove, 0, 0);
			this.rightMovePanel.Controls.Add(this.btnRightMove, 1, 0);
			this.rightMovePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rightMovePanel.Location = new System.Drawing.Point(3, 17);
			this.rightMovePanel.Name = "rightMovePanel";
			this.rightMovePanel.RowCount = 1;
			this.rightMovePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.rightMovePanel.Size = new System.Drawing.Size(472, 29);
			this.rightMovePanel.TabIndex = 0;
			// 
			// txtRightMove
			// 
			this.txtRightMove.AccessibleDescription = "显示用于将当前窗口移动到右侧虚拟桌面的热键组合";
			this.txtRightMove.AccessibleName = "右移虚拟桌面热键";
			this.txtRightMove.BackColor = System.Drawing.SystemColors.Window;
			this.txtRightMove.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtRightMove.Location = new System.Drawing.Point(3, 3);
			this.txtRightMove.Name = "txtRightMove";
			this.txtRightMove.ReadOnly = true;
			this.txtRightMove.Size = new System.Drawing.Size(324, 21);
			this.txtRightMove.TabIndex = 0;
			this.txtRightMove.Text = "未设置";
			// 
			// btnRightMove
			// 
			this.btnRightMove.Location = new System.Drawing.Point(333, 3);
			this.btnRightMove.Name = "btnRightMove";
			this.btnRightMove.Size = new System.Drawing.Size(80, 21);
			this.btnRightMove.TabIndex = 1;
			this.btnRightMove.Text = "设置";
			this.btnRightMove.UseVisualStyleBackColor = true;
			// 
			// grpPinWindow
			// 
			this.grpPinWindow.Controls.Add(this.pinWindowPanel);
			this.grpPinWindow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpPinWindow.Location = new System.Drawing.Point(3, 113);
			this.grpPinWindow.Name = "grpPinWindow";
			this.grpPinWindow.Size = new System.Drawing.Size(478, 49);
			this.grpPinWindow.TabIndex = 2;
			this.grpPinWindow.TabStop = false;
			this.grpPinWindow.Text = "固定窗口到当前桌面热键";
			// 
			// pinWindowPanel
			// 
			this.pinWindowPanel.ColumnCount = 2;
			this.pinWindowPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.pinWindowPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.pinWindowPanel.Controls.Add(this.txtPinWindow, 0, 0);
			this.pinWindowPanel.Controls.Add(this.btnPinWindow, 1, 0);
			this.pinWindowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pinWindowPanel.Location = new System.Drawing.Point(3, 17);
			this.pinWindowPanel.Name = "pinWindowPanel";
			this.pinWindowPanel.RowCount = 1;
			this.pinWindowPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.pinWindowPanel.Size = new System.Drawing.Size(472, 29);
			this.pinWindowPanel.TabIndex = 0;
			// 
			// txtPinWindow
			// 
			this.txtPinWindow.AccessibleDescription = "显示用于固定或取消固定当前窗口在所有虚拟桌面上显示的热键组合";
			this.txtPinWindow.AccessibleName = "固定窗口热键";
			this.txtPinWindow.BackColor = System.Drawing.SystemColors.Window;
			this.txtPinWindow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtPinWindow.Location = new System.Drawing.Point(3, 3);
			this.txtPinWindow.Name = "txtPinWindow";
			this.txtPinWindow.ReadOnly = true;
			this.txtPinWindow.Size = new System.Drawing.Size(324, 21);
			this.txtPinWindow.TabIndex = 0;
			this.txtPinWindow.Text = "未设置";
			// 
			// btnPinWindow
			// 
			this.btnPinWindow.Location = new System.Drawing.Point(333, 3);
			this.btnPinWindow.Name = "btnPinWindow";
			this.btnPinWindow.Size = new System.Drawing.Size(80, 21);
			this.btnPinWindow.TabIndex = 1;
			this.btnPinWindow.Text = "设置";
			this.btnPinWindow.UseVisualStyleBackColor = true;
			// 
			// grpSettings
			// 
			this.grpSettings.Controls.Add(this.settingsPanel);
			this.grpSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpSettings.Location = new System.Drawing.Point(3, 168);
			this.grpSettings.Name = "grpSettings";
			this.grpSettings.Size = new System.Drawing.Size(478, 49);
			this.grpSettings.TabIndex = 3;
			this.grpSettings.TabStop = false;
			this.grpSettings.Text = "设置选项";
			// 
			// settingsPanel
			// 
			this.settingsPanel.ColumnCount = 1;
			this.settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.settingsPanel.Controls.Add(this.chkSwitchDesktopOnMove, 0, 0);
			this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.settingsPanel.Location = new System.Drawing.Point(3, 17);
			this.settingsPanel.Name = "settingsPanel";
			this.settingsPanel.RowCount = 1;
			this.settingsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.settingsPanel.Size = new System.Drawing.Size(472, 29);
			this.settingsPanel.TabIndex = 0;
			// 
			// chkSwitchDesktopOnMove
			// 
			this.chkSwitchDesktopOnMove.AccessibleDescription = "勾选此选项后，移动窗口时会自动切换到目标虚拟桌面";
			this.chkSwitchDesktopOnMove.AccessibleName = "切换桌面设置";
			this.chkSwitchDesktopOnMove.AutoSize = true;
			this.chkSwitchDesktopOnMove.Checked = true;
			this.chkSwitchDesktopOnMove.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSwitchDesktopOnMove.Location = new System.Drawing.Point(3, 3);
			this.chkSwitchDesktopOnMove.Name = "chkSwitchDesktopOnMove";
			this.chkSwitchDesktopOnMove.Size = new System.Drawing.Size(180, 16);
			this.chkSwitchDesktopOnMove.TabIndex = 0;
			this.chkSwitchDesktopOnMove.Text = "移动窗口时同时切换虚拟桌面";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(3, 223);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(100, 30);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "保存设置";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// btnStartup
			// 
			this.btnStartup.Location = new System.Drawing.Point(109, 223);
			this.btnStartup.Name = "btnStartup";
			this.btnStartup.Size = new System.Drawing.Size(120, 30);
			this.btnStartup.TabIndex = 6;
			this.btnStartup.Text = "启用自启动";
			this.btnStartup.UseVisualStyleBackColor = true;
			this.btnStartup.Click += new System.EventHandler(this.BtnToggleStartup_Click);
			// 
			// MainForm
			// 
			this.ClientSize = new System.Drawing.Size(484, 261);
			this.Controls.Add(this.mainPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "虚拟桌面助手";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.mainPanel.ResumeLayout(false);
			this.grpLeftMove.ResumeLayout(false);
			this.leftMovePanel.ResumeLayout(false);
			this.leftMovePanel.PerformLayout();
			this.grpRightMove.ResumeLayout(false);
			this.rightMovePanel.ResumeLayout(false);
			this.rightMovePanel.PerformLayout();
			this.grpPinWindow.ResumeLayout(false);
			this.pinWindowPanel.ResumeLayout(false);
			this.pinWindowPanel.PerformLayout();
			this.grpSettings.ResumeLayout(false);
			this.settingsPanel.ResumeLayout(false);
			this.settingsPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
	}
}