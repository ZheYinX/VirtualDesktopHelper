﻿﻿﻿﻿﻿﻿﻿﻿using System;
using System.Windows.Forms;

namespace VirtualDesktopHelper
{
    partial class HotkeyCaptureForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // 热键捕捉表单控件声明
        private TextBox hotkeyTextBox;
        private Button btnOK;
        private Button btnCancel;
        private Button btnClear;
        private Button btnWin;
        private TableLayoutPanel layout;
        private FlowLayoutPanel buttonsPanel;

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
			this.layout = new System.Windows.Forms.TableLayoutPanel();
			this.hotkeyTextBox = new System.Windows.Forms.TextBox();
			this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnWin = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.layout.SuspendLayout();
			this.buttonsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// layout
			// 
			this.layout.ColumnCount = 1;
			this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.layout.Controls.Add(this.hotkeyTextBox, 0, 0);
			this.layout.Controls.Add(this.buttonsPanel, 0, 1);
			this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layout.Location = new System.Drawing.Point(0, 0);
			this.layout.Name = "layout";
			this.layout.Padding = new System.Windows.Forms.Padding(10);
			this.layout.RowCount = 2;
			this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.layout.Size = new System.Drawing.Size(334, 111);
			this.layout.TabIndex = 0;
			// 
			// hotkeyTextBox
			// 
			this.hotkeyTextBox.AccessibleDescription = "请输入热键组合";
			this.hotkeyTextBox.AccessibleName = "热键捕捉区域";
			this.hotkeyTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hotkeyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.hotkeyTextBox.Location = new System.Drawing.Point(13, 13);
			this.hotkeyTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
			this.hotkeyTextBox.Name = "hotkeyTextBox";
			this.hotkeyTextBox.ReadOnly = true;
			this.hotkeyTextBox.Size = new System.Drawing.Size(308, 26);
			this.hotkeyTextBox.TabIndex = 0;
			this.hotkeyTextBox.Text = "请输入热键";
			this.hotkeyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotkeyCaptureForm_KeyDown);
			// 
			// buttonsPanel
			// 
			this.buttonsPanel.Controls.Add(this.btnOK);
			this.buttonsPanel.Controls.Add(this.btnCancel);
			this.buttonsPanel.Controls.Add(this.btnWin);
			this.buttonsPanel.Controls.Add(this.btnClear);
			this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.buttonsPanel.Location = new System.Drawing.Point(13, 53);
			this.buttonsPanel.Name = "buttonsPanel";
			this.buttonsPanel.Size = new System.Drawing.Size(308, 45);
			this.buttonsPanel.TabIndex = 1;
			// 
			// btnOK
			// 
			this.btnOK.AccessibleDescription = "确认当前热键设置并关闭热键捕捉窗口";
			this.btnOK.AccessibleName = "确定";
			this.btnOK.Location = new System.Drawing.Point(230, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 25);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "确定";
			this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleDescription = "取消当前热键设置操作并关闭热键捕捉窗口";
			this.btnCancel.AccessibleName = "取消";
			this.btnCancel.Location = new System.Drawing.Point(149, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 25);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "取消";
			this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// btnWin
			// 
			this.btnWin.AccessibleDescription = "添加Windows键到当前热键组合中";
			this.btnWin.AccessibleName = "Win键";
			this.btnWin.Location = new System.Drawing.Point(68, 3);
			this.btnWin.Name = "btnWin";
			this.btnWin.Size = new System.Drawing.Size(75, 25);
			this.btnWin.TabIndex = 2;
			this.btnWin.Text = "Win键";
			this.btnWin.Click += new System.EventHandler(this.BtnWin_Click);
			// 
			// btnClear
			// 
			this.btnClear.AccessibleDescription = "清除当前已设置的热键组合";
			this.btnClear.AccessibleName = "清除";
			this.btnClear.Location = new System.Drawing.Point(230, 34);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 25);
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = "清除";
			this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
			// 
			// HotkeyCaptureForm
			// 
			this.AcceptButton = this.btnOK;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(334, 111);
			this.Controls.Add(this.layout);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HotkeyCaptureForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "捕获热键";
			this.layout.ResumeLayout(false);
			this.layout.PerformLayout();
			this.buttonsPanel.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
    }
}