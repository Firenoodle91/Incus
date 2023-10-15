using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.Linq;
using System.Collections.Generic;

using HKInc.Ui.Model.Domain;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.View.REPORT
{
    public partial class XRORD1000_DEV : DevExpress.XtraReports.UI.XtraReport
    {
        public XRORD1000_DEV()
        {
            InitializeComponent();
        }

        public XRORD1000_DEV(TN_ORD1000 masterObj) : this()
        {
            bindingSource3.DataSource = masterObj.TN_ORD1001List as List<TN_ORD1001>;
        }

        #region 사용안함
        private void TEST(TN_ORD1000 masterObj)
        {
            //xrTable2
            //for (int i = 0; i < 19; i++)
            //{
            //    xrTable2.InsertRowBelow(xrTable2.Rows[2]);
            //}

            List<TN_ORD1001> list = masterObj.TN_ORD1001List as List<TN_ORD1001>;

            xrTable2.BeginInit();

            if (list.Count > 18)
            {

            }
            else
            {
                for (int i = 0; i < 19; i++)
                {
                    if (i < masterObj.TN_ORD1001List.Count)
                    {
                        SetXrTableRow(xrTable2, list, masterObj, i);
                    }
                    else
                    {
                        SetXrTableRow(xrTable2, null, null, i);
                    }
                }
            }
            xrTable2.EndInit();
        }

        private void SetXrTableRow(DevExpress.XtraReports.UI.XRTable tb, List<TN_ORD1001> list, TN_ORD1000 masterObj, int index)
        {
            
            XRTableRow row = new XRTableRow();
            row.HeightF = 30F;
            SetXtrColumns(row, tb.Rows[1]);
            tb.Rows.Add(row);

            if (masterObj != null)
            {
                row.Cells[0].Text = (index + 1).ToString();
                row.Cells[1].Text = masterObj.OrderDate.ToShortDateString();
                row.Cells[2].Text = masterObj.OrderCustomerCode.ToString();
                row.Cells[3].Text = list[index].ItemCode.ToString();
                row.Cells[4].Text = list[index].TN_STD1100.ItemName.ToString();
                row.Cells[5].Text = list[index].ConfirmReqDate == null ? string.Empty : list[index].ConfirmReqDate.Value.ToShortDateString();
                row.Cells[6].Text = list[index].DrawSubmitDate == null ? string.Empty : list[index].DrawSubmitDate.Value.ToShortDateString();
                row.Cells[7].Text = list[index].ConfirmDate == null ? string.Empty : list[index].ConfirmDate.Value.ToShortDateString();
                row.Cells[8].Text = list[index].RfqDate == null ? string.Empty : list[index].RfqDate.Value.ToShortDateString();
                row.Cells[9].Text = list[index].QuoteSubmitDate == null ? string.Empty : list[index].QuoteSubmitDate.Value.ToShortDateString();
                row.Cells[10].Text = string.Empty;
                row.Cells[11].Text = string.Empty;
                row.Cells[12].Text = list[index].ResrchConfirmDate == null ? string.Empty : list[index].ResrchConfirmDate.Value.ToShortDateString();
                row.Cells[13].Text = list[index].SalesConfirmDate == null ? string.Empty : list[index].SalesConfirmDate.Value.ToShortDateString();
                row.Cells[14].Text = list[index].ProdConfirmDate == null ? string.Empty : list[index].ProdConfirmDate.Value.ToShortDateString();
                row.Cells[15].Text = list[index].PurConfirmDate == null ? string.Empty : list[index].PurConfirmDate.Value.ToShortDateString();
                row.Cells[16].Text = string.Empty;
                row.Cells[17].Text = string.Empty;
                row.Cells[18].Text = list[index].Memo.GetNullToEmpty();
            }
            else
            {
                row.Cells[0].Text = (index + 1).ToString();
            }
        }

        private void SetXtrColumns(XRTableRow row, XRTableRow realRow)
        {
            for (int i = 0; i < 19; i++)
            {
                XRTableCell xrCell = new XRTableCell();
                xrCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrCell.Borders = DevExpress.XtraPrinting.BorderSide.All;
                xrCell.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                xrCell.BorderColor = Color.Black;
                xrCell.WidthF = realRow.Cells[i].WidthF;
                xrCell.Font = new Font("Arial", 6);
                row.Cells.Add(xrCell);
            }
        }
        #endregion
    }
}
