using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;

using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Enum;

namespace HKInc.Service.Helper
{
    class DateEditMask : IDateEditMask
    {
        Dictionary<DateFormat, string> DicFormatString = new Dictionary<DateFormat, string>()
        {
            {DateFormat.Year, "yyyy"},
            {DateFormat.Month, HKInc.Utils.Common.GlobalVariable.YearMonthFormatString},
            {DateFormat.Day, HKInc.Utils.Common.GlobalVariable.DateFormatString},
            {DateFormat.DateAndTime, HKInc.Utils.Common.GlobalVariable.DateTimeFormatString},
            {DateFormat.DateAndTimeSecond, "yyyy/MM/dd HH:mm:ss" } // 20220509 오세완 차장 고도화로 인하여 추가 처리 
        };

        private string FormatString;
        private DateEdit Editor;
        private DateFormat DateFormat;

        public DateEditMask()
        {
            SetMaskFormat(DateFormat.Day);
        }

        public DateEditMask(DateEdit editor, DateFormat dateFormat = DateFormat.Day)
        {
            SetEditMask(editor, dateFormat);
        }        

        public void SetEditMask(DateEdit editor, DateFormat format = DateFormat.Day)
        {
            SetMaskFormat(format);
            SetEditor(editor);
            SetEditMask();
        }

        private void SetEditor(DateEdit editor)
        {
            this.Editor = editor;
        }

        public void SetMaskFormat(DateFormat dateFormat)
        {            
            this.DateFormat = dateFormat;
            this.FormatString = DicFormatString[this.DateFormat];            
        }
        
        private void SetEditMask()
        {
            this.Editor.Properties.EditMask = this.FormatString;
            this.Editor.Properties.Mask.MaskType = MaskType.DateTime;
            this.Editor.Properties.EditFormat.FormatString = this.FormatString;
            this.Editor.Properties.EditFormat.FormatType = FormatType.DateTime;
            this.Editor.Properties.DisplayFormat.FormatString = this.FormatString;
            this.Editor.Properties.DisplayFormat.FormatType = FormatType.DateTime;
            this.Editor.Properties.Mask.UseMaskAsDisplayFormat = true;

            if (this.DateFormat == DateFormat.Year)
            {
                this.Editor.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.YearsGroupView;
                this.Editor.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearsGroupView;
            }
            else if (this.DateFormat == DateFormat.Month)
            {
                this.Editor.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.YearView;
                this.Editor.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView | VistaCalendarViewStyle.YearsGroupView;
            }
            else
            {
                this.Editor.Properties.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;
                this.Editor.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.Default;
            }
        }        

        public void Dispose()
        {
            if (Editor != null)
            {
                Editor.Dispose();
                Editor = null;
            }
        }
    }
}
