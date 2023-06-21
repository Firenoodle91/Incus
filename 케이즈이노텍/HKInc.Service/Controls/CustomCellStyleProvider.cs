using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Controls
{
    public class CustomCellStyleProvider : ICalendarCellStyleProvider
    {
        IService<Holiday> ModelService = (IService<Holiday>)ServiceFactory.GetDomainService("Holiday");
        List<Holiday> holidayList;
        DateEditEx dateEditEx;
        RepositoryItemDateEdit repositoryItemDateEdit;

        public CustomCellStyleProvider(DateEditEx dateEditEx)
        {
            holidayList = ModelService.GetList(p => p.HolidayFlag == "Y").ToList();
            this.dateEditEx = dateEditEx;
        }

        public CustomCellStyleProvider(RepositoryItemDateEdit repositoryItemDateEdit)
        {
            holidayList = ModelService.GetList(p => p.HolidayFlag == "Y").ToList();
            this.repositoryItemDateEdit = repositoryItemDateEdit;
        }

        public void UpdateAppearance(CalendarCellStyle cell)
        {
            if (IsHoliday(cell.Date))
            {
                cell.Appearance.ForeColor = Color.Red;
                cell.Appearance.Font = new Font(cell.Appearance.Font, FontStyle.Bold);
            }
        }

        private bool IsHoliday(DateTime dt)
        {
            if (dateEditEx != null)
            {
                if ((dateEditEx.Properties.VistaCalendarInitialViewStyle == VistaCalendarInitialViewStyle.YearsGroupView) ||
                    (dateEditEx.Properties.VistaCalendarInitialViewStyle == VistaCalendarInitialViewStyle.YearView))
                    return false;
                else
                {
                    if (holidayList.Any(p => p.Date.Date.Year == dt.Date.Year && p.Date.Month == dt.Date.Month && p.Date.Day == dt.Date.Day))
                        return true;
                    else
                        return false;
                }
            }
            else if (repositoryItemDateEdit != null)
            {
                if (holidayList.Any(p => p.Date.Date.Year == dt.Date.Year && p.Date.Month == dt.Date.Month && p.Date.Day == dt.Date.Day))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
