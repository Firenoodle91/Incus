namespace HKInc.Service.Forms
{
    partial class ErrorMsgBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lc = new DevExpress.XtraLayout.LayoutControl();
            this.btnSendServer = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.txtOccurredTime = new DevExpress.XtraEditors.TextEdit();
            this.memStackTrace = new DevExpress.XtraEditors.MemoEdit();
            this.memMessage = new DevExpress.XtraEditors.MemoEdit();
            this.lcGroupBase = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcItemOk = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemSendServer = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.tabGroup = new DevExpress.XtraLayout.TabbedControlGroup();
            this.lcGroupDetail = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcItemStackTrace = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemOccurredTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGroupMessage = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcItemMessage = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lc)).BeginInit();
            this.lc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOccurredTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memStackTrace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memMessage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemSendServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemStackTrace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemOccurredTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // lc
            // 
            this.lc.Controls.Add(this.btnSendServer);
            this.lc.Controls.Add(this.btnOk);
            this.lc.Controls.Add(this.txtOccurredTime);
            this.lc.Controls.Add(this.memStackTrace);
            this.lc.Controls.Add(this.memMessage);
            this.lc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lc.Location = new System.Drawing.Point(0, 0);
            this.lc.Name = "lc";
            this.lc.Root = this.lcGroupBase;
            this.lc.Size = new System.Drawing.Size(584, 348);
            this.lc.TabIndex = 0;
            this.lc.Text = "layoutControl1";
            // 
            // btnSendServer
            // 
            this.btnSendServer.Location = new System.Drawing.Point(423, 322);
            this.btnSendServer.Name = "btnSendServer";
            this.btnSendServer.Size = new System.Drawing.Size(81, 22);
            this.btnSendServer.StyleController = this.lc;
            this.btnSendServer.TabIndex = 0;
            this.btnSendServer.Text = "Send Server";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(508, 322);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 22);
            this.btnOk.StyleController = this.lc;
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            // 
            // txtOccurredTime
            // 
            this.txtOccurredTime.Location = new System.Drawing.Point(9, 268);
            this.txtOccurredTime.Name = "txtOccurredTime";
            this.txtOccurredTime.Properties.ReadOnly = true;
            this.txtOccurredTime.Size = new System.Drawing.Size(566, 20);
            this.txtOccurredTime.StyleController = this.lc;
            this.txtOccurredTime.TabIndex = 9;
            // 
            // memStackTrace
            // 
            this.memStackTrace.Location = new System.Drawing.Point(9, 32);
            this.memStackTrace.Name = "memStackTrace";
            this.memStackTrace.Properties.ReadOnly = true;
            this.memStackTrace.Size = new System.Drawing.Size(566, 256);
            this.memStackTrace.StyleController = this.lc;
            this.memStackTrace.TabIndex = 5;
            // 
            // memMessage
            // 
            this.memMessage.Location = new System.Drawing.Point(9, 32);
            this.memMessage.Name = "memMessage";
            this.memMessage.Properties.ReadOnly = true;
            this.memMessage.Size = new System.Drawing.Size(566, 232);
            this.memMessage.StyleController = this.lc;
            this.memMessage.TabIndex = 4;
            // 
            // lcGroupBase
            // 
            this.lcGroupBase.CustomizationFormText = "layoutControlGroup1";
            this.lcGroupBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcGroupBase.GroupBordersVisible = false;
            this.lcGroupBase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.lcItemOk,
            this.lcItemSendServer,
            this.simpleSeparator1,
            this.tabGroup});
            this.lcGroupBase.Location = new System.Drawing.Point(0, 0);
            this.lcGroupBase.Name = "lcGroupBase";
            this.lcGroupBase.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcGroupBase.Size = new System.Drawing.Size(584, 348);
            this.lcGroupBase.Text = "lcGroupBase";
            this.lcGroupBase.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 293);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(580, 23);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 318);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(419, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcItemOk
            // 
            this.lcItemOk.Control = this.btnOk;
            this.lcItemOk.CustomizationFormText = "layoutControlItem1";
            this.lcItemOk.Location = new System.Drawing.Point(504, 318);
            this.lcItemOk.MaxSize = new System.Drawing.Size(76, 26);
            this.lcItemOk.MinSize = new System.Drawing.Size(76, 26);
            this.lcItemOk.Name = "lcItemOk";
            this.lcItemOk.Size = new System.Drawing.Size(76, 26);
            this.lcItemOk.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcItemOk.Text = "lcItemOk";
            this.lcItemOk.TextSize = new System.Drawing.Size(0, 0);
            this.lcItemOk.TextToControlDistance = 0;
            this.lcItemOk.TextVisible = false;
            // 
            // lcItemSendServer
            // 
            this.lcItemSendServer.Control = this.btnSendServer;
            this.lcItemSendServer.CustomizationFormText = "layoutControlItem2";
            this.lcItemSendServer.Location = new System.Drawing.Point(419, 318);
            this.lcItemSendServer.MaxSize = new System.Drawing.Size(85, 26);
            this.lcItemSendServer.MinSize = new System.Drawing.Size(85, 26);
            this.lcItemSendServer.Name = "lcItemSendServer";
            this.lcItemSendServer.Size = new System.Drawing.Size(85, 26);
            this.lcItemSendServer.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcItemSendServer.Text = "lcItemSendServer";
            this.lcItemSendServer.TextSize = new System.Drawing.Size(0, 0);
            this.lcItemSendServer.TextToControlDistance = 0;
            this.lcItemSendServer.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.CustomizationFormText = "simpleSeparator1";
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 316);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(580, 2);
            this.simpleSeparator1.Text = "simpleSeparator1";
            // 
            // tabGroup
            // 
            this.tabGroup.CustomizationFormText = "tabGroup";
            this.tabGroup.Location = new System.Drawing.Point(0, 0);
            this.tabGroup.Name = "tabGroup";
            this.tabGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.tabGroup.SelectedTabPage = this.lcGroupMessage;
            this.tabGroup.SelectedTabPageIndex = 0;
            this.tabGroup.Size = new System.Drawing.Size(580, 293);
            this.tabGroup.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcGroupMessage,
            this.lcGroupDetail});
            this.tabGroup.Text = "tabGroup";
            // 
            // lcGroupDetail
            // 
            this.lcGroupDetail.CustomizationFormText = "Detail Error Info";
            this.lcGroupDetail.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemStackTrace});
            this.lcGroupDetail.Location = new System.Drawing.Point(0, 0);
            this.lcGroupDetail.Name = "lcGroupDetail";
            this.lcGroupDetail.Size = new System.Drawing.Size(570, 260);
            this.lcGroupDetail.Text = "Detail Error Info";
            // 
            // lcItemStackTrace
            // 
            this.lcItemStackTrace.Control = this.memStackTrace;
            this.lcItemStackTrace.CustomizationFormText = "Stack Trace";
            this.lcItemStackTrace.Location = new System.Drawing.Point(0, 0);
            this.lcItemStackTrace.MinSize = new System.Drawing.Size(98, 20);
            this.lcItemStackTrace.Name = "lcItemStackTrace";
            this.lcItemStackTrace.Size = new System.Drawing.Size(570, 260);
            this.lcItemStackTrace.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcItemStackTrace.Text = "Stack Trace";
            this.lcItemStackTrace.TextSize = new System.Drawing.Size(0, 0);
            this.lcItemStackTrace.TextToControlDistance = 0;
            this.lcItemStackTrace.TextVisible = false;
            // 
            // lcItemOccurredTime
            // 
            this.lcItemOccurredTime.Control = this.txtOccurredTime;
            this.lcItemOccurredTime.CustomizationFormText = "Occurred Time";
            this.lcItemOccurredTime.Location = new System.Drawing.Point(0, 236);
            this.lcItemOccurredTime.Name = "lcItemOccurredTime";
            this.lcItemOccurredTime.Size = new System.Drawing.Size(570, 24);
            this.lcItemOccurredTime.Text = "Occurred Time";
            this.lcItemOccurredTime.TextSize = new System.Drawing.Size(0, 0);
            this.lcItemOccurredTime.TextToControlDistance = 0;
            this.lcItemOccurredTime.TextVisible = false;
            // 
            // lcGroupMessage
            // 
            this.lcGroupMessage.CustomizationFormText = "Error Message";
            this.lcGroupMessage.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemMessage,
            this.lcItemOccurredTime});
            this.lcGroupMessage.Location = new System.Drawing.Point(0, 0);
            this.lcGroupMessage.Name = "lcGroupMessage";
            this.lcGroupMessage.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcGroupMessage.Size = new System.Drawing.Size(570, 260);
            this.lcGroupMessage.Text = "Error Message";
            // 
            // lcItemMessage
            // 
            this.lcItemMessage.Control = this.memMessage;
            this.lcItemMessage.CustomizationFormText = "Message";
            this.lcItemMessage.Location = new System.Drawing.Point(0, 0);
            this.lcItemMessage.MinSize = new System.Drawing.Size(14, 20);
            this.lcItemMessage.Name = "lcItemMessage";
            this.lcItemMessage.Size = new System.Drawing.Size(570, 236);
            this.lcItemMessage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcItemMessage.Text = "Message";
            this.lcItemMessage.TextSize = new System.Drawing.Size(0, 0);
            this.lcItemMessage.TextToControlDistance = 0;
            this.lcItemMessage.TextVisible = false;
            // 
            // ErrorMsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 348);
            this.Controls.Add(this.lc);
            this.MinimizeBox = false;
            this.Name = "ErrorMsgBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ErrorMsgBox";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.lc)).EndInit();
            this.lc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOccurredTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memStackTrace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memMessage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemSendServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemStackTrace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemOccurredTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGroupMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemMessage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lc;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupBase;
        private DevExpress.XtraEditors.MemoEdit memMessage;
        private DevExpress.XtraEditors.MemoEdit memStackTrace;
        private DevExpress.XtraLayout.LayoutControlItem lcItemStackTrace;
        private DevExpress.XtraEditors.TextEdit txtOccurredTime;
        private DevExpress.XtraLayout.LayoutControlItem lcItemOccurredTime;
        private DevExpress.XtraEditors.SimpleButton btnSendServer;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lcItemOk;
        private DevExpress.XtraLayout.LayoutControlItem lcItemSendServer;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.TabbedControlGroup tabGroup;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupMessage;
        private DevExpress.XtraLayout.LayoutControlItem lcItemMessage;
        private DevExpress.XtraLayout.LayoutControlGroup lcGroupDetail;
    }
}