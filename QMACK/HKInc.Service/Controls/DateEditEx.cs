using System;
using System.ComponentModel;
using System.Drawing;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Controls;

namespace HKInc.Service.Controls
{
	[ToolboxItem(true)]
	public partial class DateEditEx : DateEdit
	{
        private HKInc.Utils.Interface.Helper.IDateEditMask DateFormatSetter;

		public DateEditEx()
		{
			InitializeComponent();

            //this.EnabledChanged += DateEditEx_EnabledChanged;
			this.Properties.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
            DateFormatSetter = Factory.HelperFactory.GetDateEditMask(this);
        }

        //private void DateEditEx_EnabledChanged(object sender, EventArgs e)
        //{
        //    SetButtonVisible(this.Enabled);
        //}

        public void Clear()
		{
			this.EditValue = null;
		}

		public void Init()
		{
			this.EditValue = DateTime.Now;
		}

		public override object EditValue
		{
			get { return base.EditValue; }
			set
			{
				if (value == DBNull.Value) value = null;
				base.EditValue = value;
			}
		}
        
        public void SetFormat(HKInc.Utils.Enum.DateFormat dateFormat, bool DeleteButtonFlag = false)
        {
            DateFormatSetter.SetEditMask(this, dateFormat);
            if (DeleteButtonFlag)
            {


                this.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
                DevExpress.XtraEditors.Controls.EditorButton _delButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete);
                _delButton.Click += _delButton_Click;
                this.Properties.Buttons.Insert(1, _delButton);
            }

        }

        private void _delButton_Click(object sender, EventArgs e)
        {
            this.EditValue = null;
        }

        public void SetEnable(bool bEnable = false)
		{
            SetButtonVisible(bEnable);
        }

        private void SetButtonVisible(bool enabled)
        {
            Properties.AllowFocused = enabled;
            Properties.ReadOnly = !enabled;

            if (Properties.Buttons.Count > 0)
            {
                foreach (EditorButton btn in Properties.Buttons)
                {
                    btn.Visible = enabled;
                }
            }
        }
        public void SetDeleteButton()
        {
            this.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            DevExpress.XtraEditors.Controls.EditorButton _delButton = new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete);
            _delButton.Click += _delButton_Click;
            this.Properties.Buttons.Insert(1, _delButton);
        }
    }
}
