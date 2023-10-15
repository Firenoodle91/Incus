using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;

namespace HKInc.Service.Forms
{
    public partial class ProgressWaitForm : DevExpress.XtraWaitForm.WaitForm {
        public ProgressWaitForm() {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        #region Overrides

        public override void SetCaption(string caption) {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description) {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg) {        
            base.ProcessCommand(cmd, arg);
            WaitFormCommand command = (WaitFormCommand)cmd;
            if (command == WaitFormCommand.SetProgress) {
                int pos = (int)arg;
                progressBarControl1.Position = pos;
            }
            else if(command == WaitFormCommand.SetProgressMaximum)
            {
                int Maximum = (int)arg;
                progressBarControl1.Properties.Maximum = Maximum;
            }
        }
        
        #endregion

        public enum WaitFormCommand {
            SetProgress,
            SetProgressMaximum,
            Command2,
            Command3
        }

    }
}