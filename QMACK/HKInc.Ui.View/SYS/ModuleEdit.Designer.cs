namespace HKInc.Ui.View.SYS
{ 
    partial class ModuleEdit
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
            this.components = new System.ComponentModel.Container();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textAssembly = new DevExpress.XtraEditors.TextEdit();
            this.moduleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textUpdateId = new DevExpress.XtraEditors.TextEdit();
            this.textCreateId = new DevExpress.XtraEditors.TextEdit();
            this.dateUpdateTime = new HKInc.Service.Controls.DateEditEx();
            this.dateCreteTime = new HKInc.Service.Controls.DateEditEx();
            this.memoDescription = new DevExpress.XtraEditors.MemoEdit();
            this.textModuleName3 = new DevExpress.XtraEditors.TextEdit();
            this.textModuleName2 = new DevExpress.XtraEditors.TextEdit();
            this.textModuleName = new DevExpress.XtraEditors.TextEdit();
            this.textModuleId = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcItemModuleId = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemModuleName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemModuleName2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemModuleName3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemDescription = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemCreateTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemUpdateTime = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemCreateId = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemUpdateId = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.simpleSeparator2 = new DevExpress.XtraLayout.SimpleSeparator();
            this.simpleSeparator3 = new DevExpress.XtraLayout.SimpleSeparator();
            this.lcItemAssembly = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textAssembly.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moduleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textUpdateId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCreateId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateUpdateTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateUpdateTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCreteTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCreteTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textModuleName3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textModuleName2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textModuleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textModuleId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemModuleId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemModuleName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemModuleName2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemModuleName3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCreateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemUpdateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCreateId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemUpdateId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemAssembly)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textAssembly);
            this.layoutControl1.Controls.Add(this.textUpdateId);
            this.layoutControl1.Controls.Add(this.textCreateId);
            this.layoutControl1.Controls.Add(this.dateUpdateTime);
            this.layoutControl1.Controls.Add(this.dateCreteTime);
            this.layoutControl1.Controls.Add(this.memoDescription);
            this.layoutControl1.Controls.Add(this.textModuleName3);
            this.layoutControl1.Controls.Add(this.textModuleName2);
            this.layoutControl1.Controls.Add(this.textModuleName);
            this.layoutControl1.Controls.Add(this.textModuleId);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 31);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(835, 137, 450, 400);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(521, 330);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textAssembly
            // 
            this.textAssembly.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.moduleBindingSource, "Assembly", true));
            this.textAssembly.Location = new System.Drawing.Point(126, 112);
            this.textAssembly.Name = "textAssembly";
            this.textAssembly.Size = new System.Drawing.Size(383, 20);
            this.textAssembly.StyleController = this.layoutControl1;
            this.textAssembly.TabIndex = 13;
            // 
            // moduleBindingSource
            // 
            this.moduleBindingSource.DataSource = typeof(HKInc.Ui.Model.Domain.Module);
            // 
            // textUpdateId
            // 
            this.textUpdateId.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.moduleBindingSource, "UpdateId", true));
            this.textUpdateId.Enabled = false;
            this.textUpdateId.Location = new System.Drawing.Point(376, 298);
            this.textUpdateId.Name = "textUpdateId";
            this.textUpdateId.Size = new System.Drawing.Size(133, 20);
            this.textUpdateId.StyleController = this.layoutControl1;
            this.textUpdateId.TabIndex = 12;
            // 
            // textCreateId
            // 
            this.textCreateId.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.moduleBindingSource, "UpdateId", true));
            this.textCreateId.Enabled = false;
            this.textCreateId.Location = new System.Drawing.Point(376, 274);
            this.textCreateId.Name = "textCreateId";
            this.textCreateId.Size = new System.Drawing.Size(133, 20);
            this.textCreateId.StyleController = this.layoutControl1;
            this.textCreateId.TabIndex = 11;
            // 
            // dateUpdateTime
            // 
            this.dateUpdateTime.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.moduleBindingSource, "CreateTime", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "g"));
            this.dateUpdateTime.EditValue = null;
            this.dateUpdateTime.Enabled = false;
            this.dateUpdateTime.Location = new System.Drawing.Point(126, 298);
            this.dateUpdateTime.Name = "dateUpdateTime";
            this.dateUpdateTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateUpdateTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateUpdateTime.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateUpdateTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateUpdateTime.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateUpdateTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateUpdateTime.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dateUpdateTime.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateUpdateTime.Size = new System.Drawing.Size(132, 20);
            this.dateUpdateTime.StyleController = this.layoutControl1;
            this.dateUpdateTime.TabIndex = 10;
            // 
            // dateCreteTime
            // 
            this.dateCreteTime.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.moduleBindingSource, "UpdateTime", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "g"));
            this.dateCreteTime.EditValue = null;
            this.dateCreteTime.Enabled = false;
            this.dateCreteTime.Location = new System.Drawing.Point(126, 274);
            this.dateCreteTime.Name = "dateCreteTime";
            this.dateCreteTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCreteTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateCreteTime.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dateCreteTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateCreteTime.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dateCreteTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateCreteTime.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.dateCreteTime.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateCreteTime.Size = new System.Drawing.Size(132, 20);
            this.dateCreteTime.StyleController = this.layoutControl1;
            this.dateCreteTime.TabIndex = 9;
            // 
            // memoDescription
            // 
            this.memoDescription.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.moduleBindingSource, "Description", true));
            this.memoDescription.Location = new System.Drawing.Point(126, 136);
            this.memoDescription.Name = "memoDescription";
            this.memoDescription.Size = new System.Drawing.Size(383, 132);
            this.memoDescription.StyleController = this.layoutControl1;
            this.memoDescription.TabIndex = 8;
            // 
            // textModuleName3
            // 
            this.textModuleName3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.moduleBindingSource, "ModuleName3", true));
            this.textModuleName3.Location = new System.Drawing.Point(126, 86);
            this.textModuleName3.Name = "textModuleName3";
            this.textModuleName3.Size = new System.Drawing.Size(383, 20);
            this.textModuleName3.StyleController = this.layoutControl1;
            this.textModuleName3.TabIndex = 7;
            // 
            // textModuleName2
            // 
            this.textModuleName2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.moduleBindingSource, "ModuleName2", true));
            this.textModuleName2.Location = new System.Drawing.Point(126, 62);
            this.textModuleName2.Name = "textModuleName2";
            this.textModuleName2.Size = new System.Drawing.Size(383, 20);
            this.textModuleName2.StyleController = this.layoutControl1;
            this.textModuleName2.TabIndex = 6;
            // 
            // textModuleName
            // 
            this.textModuleName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.moduleBindingSource, "ModuleName", true));
            this.textModuleName.Location = new System.Drawing.Point(126, 38);
            this.textModuleName.Name = "textModuleName";
            this.textModuleName.Size = new System.Drawing.Size(383, 20);
            this.textModuleName.StyleController = this.layoutControl1;
            this.textModuleName.TabIndex = 5;
            // 
            // textModuleId
            // 
            this.textModuleId.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.moduleBindingSource, "ModuleId", true));
            this.textModuleId.Location = new System.Drawing.Point(126, 12);
            this.textModuleId.Name = "textModuleId";
            this.textModuleId.Size = new System.Drawing.Size(62, 20);
            this.textModuleId.StyleController = this.layoutControl1;
            this.textModuleId.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcItemModuleId,
            this.lcItemModuleName,
            this.lcItemModuleName2,
            this.lcItemModuleName3,
            this.lcItemDescription,
            this.lcItemCreateTime,
            this.lcItemUpdateTime,
            this.lcItemCreateId,
            this.lcItemUpdateId,
            this.emptySpaceItem1,
            this.simpleSeparator1,
            this.simpleSeparator2,
            this.simpleSeparator3,
            this.lcItemAssembly});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(521, 330);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcItemModuleId
            // 
            this.lcItemModuleId.Control = this.textModuleId;
            this.lcItemModuleId.Enabled = false;
            this.lcItemModuleId.Location = new System.Drawing.Point(0, 0);
            this.lcItemModuleId.Name = "lcItemModuleId";
            this.lcItemModuleId.Size = new System.Drawing.Size(180, 24);
            this.lcItemModuleId.TextSize = new System.Drawing.Size(111, 14);
            // 
            // lcItemModuleName
            // 
            this.lcItemModuleName.Control = this.textModuleName;
            this.lcItemModuleName.Location = new System.Drawing.Point(0, 26);
            this.lcItemModuleName.Name = "lcItemModuleName";
            this.lcItemModuleName.Size = new System.Drawing.Size(501, 24);
            this.lcItemModuleName.TextSize = new System.Drawing.Size(111, 14);
            // 
            // lcItemModuleName2
            // 
            this.lcItemModuleName2.Control = this.textModuleName2;
            this.lcItemModuleName2.Location = new System.Drawing.Point(0, 50);
            this.lcItemModuleName2.Name = "lcItemModuleName2";
            this.lcItemModuleName2.Size = new System.Drawing.Size(501, 24);
            this.lcItemModuleName2.TextSize = new System.Drawing.Size(111, 14);
            // 
            // lcItemModuleName3
            // 
            this.lcItemModuleName3.Control = this.textModuleName3;
            this.lcItemModuleName3.Location = new System.Drawing.Point(0, 74);
            this.lcItemModuleName3.Name = "lcItemModuleName3";
            this.lcItemModuleName3.Size = new System.Drawing.Size(501, 24);
            this.lcItemModuleName3.TextSize = new System.Drawing.Size(111, 14);
            // 
            // lcItemDescription
            // 
            this.lcItemDescription.Control = this.memoDescription;
            this.lcItemDescription.Location = new System.Drawing.Point(0, 124);
            this.lcItemDescription.Name = "lcItemDescription";
            this.lcItemDescription.Size = new System.Drawing.Size(501, 136);
            this.lcItemDescription.TextSize = new System.Drawing.Size(111, 14);
            // 
            // lcItemCreateTime
            // 
            this.lcItemCreateTime.Control = this.dateCreteTime;
            this.lcItemCreateTime.Location = new System.Drawing.Point(0, 262);
            this.lcItemCreateTime.Name = "lcItemCreateTime";
            this.lcItemCreateTime.Size = new System.Drawing.Size(250, 24);
            this.lcItemCreateTime.TextSize = new System.Drawing.Size(111, 14);
            // 
            // lcItemUpdateTime
            // 
            this.lcItemUpdateTime.Control = this.dateUpdateTime;
            this.lcItemUpdateTime.Location = new System.Drawing.Point(0, 286);
            this.lcItemUpdateTime.Name = "lcItemUpdateTime";
            this.lcItemUpdateTime.Size = new System.Drawing.Size(250, 24);
            this.lcItemUpdateTime.TextSize = new System.Drawing.Size(111, 14);
            // 
            // lcItemCreateId
            // 
            this.lcItemCreateId.Control = this.textCreateId;
            this.lcItemCreateId.Location = new System.Drawing.Point(250, 262);
            this.lcItemCreateId.Name = "lcItemCreateId";
            this.lcItemCreateId.Size = new System.Drawing.Size(251, 24);
            this.lcItemCreateId.TextSize = new System.Drawing.Size(111, 14);
            // 
            // lcItemUpdateId
            // 
            this.lcItemUpdateId.Control = this.textUpdateId;
            this.lcItemUpdateId.Location = new System.Drawing.Point(250, 286);
            this.lcItemUpdateId.Name = "lcItemUpdateId";
            this.lcItemUpdateId.Size = new System.Drawing.Size(251, 24);
            this.lcItemUpdateId.TextSize = new System.Drawing.Size(111, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(180, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(321, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 24);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(501, 2);
            // 
            // simpleSeparator2
            // 
            this.simpleSeparator2.AllowHotTrack = false;
            this.simpleSeparator2.Location = new System.Drawing.Point(0, 98);
            this.simpleSeparator2.Name = "simpleSeparator2";
            this.simpleSeparator2.Size = new System.Drawing.Size(501, 2);
            // 
            // simpleSeparator3
            // 
            this.simpleSeparator3.AllowHotTrack = false;
            this.simpleSeparator3.Location = new System.Drawing.Point(0, 260);
            this.simpleSeparator3.Name = "simpleSeparator3";
            this.simpleSeparator3.Size = new System.Drawing.Size(501, 2);
            // 
            // lcItemAssembly
            // 
            this.lcItemAssembly.Control = this.textAssembly;
            this.lcItemAssembly.Location = new System.Drawing.Point(0, 100);
            this.lcItemAssembly.Name = "lcItemAssembly";
            this.lcItemAssembly.Size = new System.Drawing.Size(501, 24);
            this.lcItemAssembly.TextSize = new System.Drawing.Size(111, 14);
            // 
            // ModuleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 386);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ModuleEdit";
            this.Text = "ModuleEdit";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textAssembly.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moduleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textUpdateId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCreateId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateUpdateTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateUpdateTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCreteTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateCreteTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textModuleName3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textModuleName2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textModuleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textModuleId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemModuleId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemModuleName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemModuleName2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemModuleName3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCreateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemUpdateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemCreateId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemUpdateId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemAssembly)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit textUpdateId;
        private DevExpress.XtraEditors.TextEdit textCreateId;
        private Service.Controls.DateEditEx dateUpdateTime;
        private Service.Controls.DateEditEx dateCreteTime;
        private DevExpress.XtraEditors.MemoEdit memoDescription;
        private DevExpress.XtraEditors.TextEdit textModuleName3;
        private DevExpress.XtraEditors.TextEdit textModuleName2;
        private DevExpress.XtraEditors.TextEdit textModuleName;
        private DevExpress.XtraEditors.TextEdit textModuleId;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lcItemModuleId;
        private DevExpress.XtraLayout.LayoutControlItem lcItemModuleName;
        private DevExpress.XtraLayout.LayoutControlItem lcItemModuleName2;
        private DevExpress.XtraLayout.LayoutControlItem lcItemModuleName3;
        private DevExpress.XtraLayout.LayoutControlItem lcItemDescription;
        private DevExpress.XtraLayout.LayoutControlItem lcItemCreateTime;
        private DevExpress.XtraLayout.LayoutControlItem lcItemUpdateTime;
        private DevExpress.XtraLayout.LayoutControlItem lcItemCreateId;
        private DevExpress.XtraLayout.LayoutControlItem lcItemUpdateId;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator2;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator3;
        private DevExpress.XtraEditors.TextEdit textAssembly;
        private System.Windows.Forms.BindingSource moduleBindingSource;
        private DevExpress.XtraLayout.LayoutControlItem lcItemAssembly;
    }
}