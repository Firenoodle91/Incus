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
			this.Properties.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
            DateFormatSetter = Factory.HelperFactory.GetDateEditMask(this);

            Properties.CellStyleProvider = new CustomCellStyleProvider(this);

            Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

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
            else
            {
                this.Properties.ShowClear = false;
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

        public void SetFontSize(Font font)
        {
            Properties.Appearance.Font = font;
            Properties.AppearanceDisabled.Font = font;
            Properties.AppearanceFocused.Font = font;
            Properties.AppearanceReadOnly.Font = font;

            Properties.AppearanceDropDownHeader.Font = font;
            Properties.AppearanceDropDown.Font = font;

            Properties.AppearanceCalendar.DayCell.Font = font;
            Properties.AppearanceCalendar.DayCellDisabled.Font = font;
            Properties.AppearanceCalendar.DayCellHoliday.Font = font;
            Properties.AppearanceCalendar.DayCellInactive.Font = font;
            Properties.AppearanceCalendar.DayCellPressed.Font = font;
            Properties.AppearanceCalendar.DayCellSelected.Font = font;
            Properties.AppearanceCalendar.DayCellSpecial.Font = font;
            Properties.AppearanceCalendar.DayCellSpecialDisabled.Font = font;
            Properties.AppearanceCalendar.DayCellSpecialHighlighted.Font = font;
            Properties.AppearanceCalendar.DayCellSpecialInactive.Font = font;
            Properties.AppearanceCalendar.DayCellSpecialPressed.Font = font;
            Properties.AppearanceCalendar.DayCellSpecialSelected.Font = font;
            Properties.AppearanceCalendar.DayCellToday.Font = font;
            Properties.AppearanceCalendar.DayCellHighlighted.Font = font;

            Properties.AppearanceCalendar.WeekDay.Font = font;
            Properties.AppearanceCalendar.WeekNumber.Font = font;

            Properties.AppearanceCalendar.HeaderHighlighted.Font = font;
            Properties.AppearanceCalendar.Header.Font = font;
            Properties.AppearanceCalendar.HeaderPressed.Font = font;

            Properties.AppearanceCalendar.Button.Font = font;
            Properties.AppearanceCalendar.ButtonHighlighted.Font = font;
            Properties.AppearanceCalendar.ButtonPressed.Font = font;

            Properties.AppearanceCalendar.CalendarHeader.Font = font;

        }
        
    }
}
