using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

using HKInc.Utils.Common;
using HKInc.Utils.Enum;

namespace HKInc.Service.Factory
{
    public static class RepositoryFactory
    {
        public static RepositoryItemDateEdit GetRepositoryItemDate(DateFormat formatFlag = DateFormat.Day)
        {
            string formatString;

            RepositoryItemDateEdit date = new RepositoryItemDateEdit();
            formatString = formatFlag == DateFormat.DateAndTime ? string.Format("{0} {1}", GlobalVariable.DateFormatString, "HH:mm") : (formatFlag == DateFormat.DateAndTimeSecond ? string.Format("{0} {1}", GlobalVariable.DateFormatString, "HH:mm:ss") : (formatFlag == DateFormat.Month ? "yyyy-MM" : GlobalVariable.DateFormatString));

            date.DisplayFormat.FormatType = FormatType.DateTime;
            date.DisplayFormat.FormatString = formatString;
            date.EditFormat.FormatString = formatString;
            date.EditFormat.FormatType = FormatType.DateTime;
            date.EditMask = formatString;
            date.Mask.UseMaskAsDisplayFormat = true;
            date.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            date.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            if (formatFlag == DateFormat.DateAndTime || formatFlag == DateFormat.DateAndTimeSecond)
            {
                date.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
                date.CalendarTimeProperties.TimeEditStyle = TimeEditStyle.SpinButtons;
                date.CalendarTimeProperties.EditMask = formatString;
                date.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat = true;
            }

            if (formatFlag == DateFormat.Year)
            {
                date.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.YearsGroupView;
                date.VistaCalendarViewStyle = VistaCalendarViewStyle.YearsGroupView;
            }
            else if (formatFlag == DateFormat.Month)
            {
                date.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.YearView;
                date.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView | VistaCalendarViewStyle.YearsGroupView;
            }
            else
            {
                date.VistaCalendarInitialViewStyle = VistaCalendarInitialViewStyle.MonthView;
                date.VistaCalendarViewStyle = VistaCalendarViewStyle.Default;
            }

            return date;
        }

        public static RepositoryItemLookUpEdit GetRepositoryItemLookUpEdit(object dataSource, string valueMember = "CodeVal", string displayMember = "CodeName", string nullText = "")
        {
            RepositoryItemLookUpEdit lookup = new RepositoryItemLookUpEdit()
            {
                ValueMember = valueMember,
                DisplayMember = displayMember,
                SearchMode = SearchMode.AutoComplete
            };
            lookup.Columns.Add(new LookUpColumnInfo(displayMember));
            lookup.ShowHeader = false;
            lookup.NullText = nullText;            
            lookup.DataSource = dataSource;

            return lookup;
        }
    }
}
