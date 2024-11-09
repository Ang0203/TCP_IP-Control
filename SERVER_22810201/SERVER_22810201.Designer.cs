namespace SERVER_22810201
{
    partial class SERVER_22810201
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SERVER_22810201));
            NotifyIcon1 = new NotifyIcon(components);
            ContextMenuStrip1 = new ContextMenuStrip(components);
            ExitToolStripMenuItem = new ToolStripMenuItem();
            LblCurrentIpPort = new Label();
            TableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            LblStatus = new Label();
            TableLayoutPanel3 = new TableLayoutPanel();
            TxtIpAddress_Lbl = new Label();
            label1 = new Label();
            TxtIpAddress = new TextBox();
            TxtPort = new TextBox();
            BtnConfirm = new Button();
            ContextMenuStrip1.SuspendLayout();
            TableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            TableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // NotifyIcon1
            // 
            NotifyIcon1.Icon = (Icon)resources.GetObject("NotifyIcon1.Icon");
            NotifyIcon1.Text = "TcpIpControl";
            NotifyIcon1.Visible = true;
            // 
            // ContextMenuStrip1
            // 
            ContextMenuStrip1.Font = new Font("Helvetica Neue", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            ContextMenuStrip1.Items.AddRange(new ToolStripItem[] { ExitToolStripMenuItem });
            ContextMenuStrip1.Name = "contextMenuStrip1";
            ContextMenuStrip1.Size = new Size(109, 26);
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Font = new Font("Helvetica", 9F);
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Size = new Size(108, 22);
            ExitToolStripMenuItem.Text = "Thoát";
            ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // LblCurrentIpPort
            // 
            LblCurrentIpPort.AutoSize = true;
            LblCurrentIpPort.Dock = DockStyle.Fill;
            LblCurrentIpPort.Font = new Font("Helvetica Neue", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 163);
            LblCurrentIpPort.Location = new Point(13, 10);
            LblCurrentIpPort.Name = "LblCurrentIpPort";
            LblCurrentIpPort.Size = new Size(358, 22);
            LblCurrentIpPort.TabIndex = 1;
            LblCurrentIpPort.Text = "Địa chỉ hiện tại: (Không)";
            LblCurrentIpPort.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TableLayoutPanel1
            // 
            TableLayoutPanel1.AutoSize = true;
            TableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableLayoutPanel1.ColumnCount = 1;
            TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            TableLayoutPanel1.Controls.Add(LblCurrentIpPort, 0, 0);
            TableLayoutPanel1.Dock = DockStyle.Top;
            TableLayoutPanel1.Location = new Point(0, 0);
            TableLayoutPanel1.Name = "TableLayoutPanel1";
            TableLayoutPanel1.Padding = new Padding(10);
            TableLayoutPanel1.RowCount = 1;
            TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            TableLayoutPanel1.Size = new Size(384, 42);
            TableLayoutPanel1.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(LblStatus, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Bottom;
            tableLayoutPanel2.Location = new Point(0, 319);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(10);
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(384, 42);
            tableLayoutPanel2.TabIndex = 10;
            // 
            // LblStatus
            // 
            LblStatus.AutoSize = true;
            LblStatus.Dock = DockStyle.Fill;
            LblStatus.Font = new Font("Helvetica Neue", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 163);
            LblStatus.Location = new Point(13, 10);
            LblStatus.Name = "LblStatus";
            LblStatus.Size = new Size(358, 22);
            LblStatus.TabIndex = 1;
            LblStatus.Text = "(Trạng thái)";
            LblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TableLayoutPanel3
            // 
            TableLayoutPanel3.AutoSize = true;
            TableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TableLayoutPanel3.ColumnCount = 1;
            TableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            TableLayoutPanel3.Controls.Add(TxtIpAddress_Lbl, 0, 0);
            TableLayoutPanel3.Controls.Add(label1, 0, 2);
            TableLayoutPanel3.Controls.Add(TxtIpAddress, 0, 1);
            TableLayoutPanel3.Controls.Add(TxtPort, 0, 3);
            TableLayoutPanel3.Controls.Add(BtnConfirm, 0, 4);
            TableLayoutPanel3.Dock = DockStyle.Fill;
            TableLayoutPanel3.Location = new Point(0, 42);
            TableLayoutPanel3.Margin = new Padding(0);
            TableLayoutPanel3.Name = "TableLayoutPanel3";
            TableLayoutPanel3.Padding = new Padding(10);
            TableLayoutPanel3.RowCount = 5;
            TableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            TableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            TableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            TableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            TableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            TableLayoutPanel3.Size = new Size(384, 277);
            TableLayoutPanel3.TabIndex = 16;
            // 
            // TxtIpAddress_Lbl
            // 
            TxtIpAddress_Lbl.AutoSize = true;
            TxtIpAddress_Lbl.Font = new Font("Helvetica Neue", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            TxtIpAddress_Lbl.Location = new Point(93, 35);
            TxtIpAddress_Lbl.Margin = new Padding(83, 25, 0, 0);
            TxtIpAddress_Lbl.Name = "TxtIpAddress_Lbl";
            TxtIpAddress_Lbl.Size = new Size(69, 18);
            TxtIpAddress_Lbl.TabIndex = 14;
            TxtIpAddress_Lbl.Text = "Nhập IP:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Helvetica Neue", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label1.Location = new Point(93, 111);
            label1.Margin = new Padding(83, 5, 0, 0);
            label1.Name = "label1";
            label1.Size = new Size(84, 18);
            label1.TabIndex = 15;
            label1.Text = "Nhập Port:";
            // 
            // TxtIpAddress
            // 
            TxtIpAddress.Font = new Font("Helvetica Neue", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            TxtIpAddress.Location = new Point(94, 62);
            TxtIpAddress.Margin = new Padding(84, 0, 0, 0);
            TxtIpAddress.Name = "TxtIpAddress";
            TxtIpAddress.Size = new Size(193, 27);
            TxtIpAddress.TabIndex = 11;
            TxtIpAddress.Text = "192.168.109.128";
            // 
            // TxtPort
            // 
            TxtPort.Font = new Font("Helvetica Neue", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            TxtPort.Location = new Point(94, 140);
            TxtPort.Margin = new Padding(84, 0, 0, 0);
            TxtPort.Name = "TxtPort";
            TxtPort.Size = new Size(193, 27);
            TxtPort.TabIndex = 12;
            TxtPort.Text = "8080";
            // 
            // BtnConfirm
            // 
            BtnConfirm.AutoSize = true;
            BtnConfirm.Font = new Font("Helvetica Neue", 12F, FontStyle.Regular, GraphicsUnit.Point, 163);
            BtnConfirm.Location = new Point(135, 198);
            BtnConfirm.Margin = new Padding(125, 0, 0, 0);
            BtnConfirm.Name = "BtnConfirm";
            BtnConfirm.Size = new Size(107, 43);
            BtnConfirm.TabIndex = 13;
            BtnConfirm.Text = "Xác nhận";
            BtnConfirm.UseVisualStyleBackColor = true;
            // 
            // SERVER_22810201
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 361);
            Controls.Add(TableLayoutPanel3);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(TableLayoutPanel1);
            Name = "SERVER_22810201";
            Text = "SERVER_22810201";
            ContextMenuStrip1.ResumeLayout(false);
            TableLayoutPanel1.ResumeLayout(false);
            TableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            TableLayoutPanel3.ResumeLayout(false);
            TableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NotifyIcon NotifyIcon1;
        private ContextMenuStrip ContextMenuStrip1;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private Label LblCurrentIpPort;
        private TableLayoutPanel TableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label LblStatus;
        private TableLayoutPanel TableLayoutPanel3;
        private Button BtnConfirm;
        private TextBox TxtIpAddress;
        private Label TxtIpAddress_Lbl;
        private TextBox TxtPort;
        private Label label1;
    }
}
