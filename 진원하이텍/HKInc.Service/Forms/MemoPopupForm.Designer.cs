﻿namespace HKInc.Service.Forms
{
    partial class MemoPopupForm
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
            this.memoEdit = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoEdit
            // 
            this.memoEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEdit.Location = new System.Drawing.Point(0, 31);
            this.memoEdit.Name = "memoEdit";
            this.memoEdit.Size = new System.Drawing.Size(474, 311);
            this.memoEdit.TabIndex = 4;
            // 
            // MemoPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 367);
            this.Controls.Add(this.memoEdit);
            this.Name = "MemoPopupForm";
            this.Text = "MemoPopupForm";
            this.Controls.SetChildIndex(this.memoEdit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoEdit;
    }
}