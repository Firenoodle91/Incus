using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Service;
using System.Data.SqlClient;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 기간별 거래처벌 품목에 대한 생산실적
    /// </summary>
    public partial class XFMPS2000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1600> ModelService = (IService<TN_MPS1600>)ProductionFactory.GetDomainService("TN_MPS1600");

        public XFMPS2000()
        {
            InitializeComponent();

            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            datePeriodEditEx1.SetTodayIsMonth();
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("CustomerCode", "거래처코드", false);
            GridExControl.MainGrid.AddColumn("CustomerName", "거래처명");
            GridExControl.MainGrid.AddColumn("ItemNm1", "품번", false);
            GridExControl.MainGrid.AddColumn("ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("SumOkQty", "양품수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("SumFailQty", "불량수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
        }

        protected override void InitRepository()
        {
        }

        protected override void DataLoad()
        {
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                string Item = textItemCodeName.EditValue.GetNullToEmpty();
                string Customer = textCustomerCodeName.EditValue.GetNullToEmpty();

                var FrDate = new SqlParameter("@FrDate", datePeriodEditEx1.DateFrEdit.DateTime);
                var ToDate = new SqlParameter("@ToDate", datePeriodEditEx1.DateToEdit.DateTime);

                var result = context.Database
                      .SqlQuery<DataModel>("USP_GET_MPS2000 @FrDate, @ToDate", FrDate, ToDate)
                      .Where(p => (p.ItemNm1.Contains(Item) || p.ItemNm.Contains(Item))
                               && (p.CustomerCode.Contains(Customer) || p.CustomerName.Contains(Customer))
                            )
                      .ToList();

                GridBindingSource.DataSource = result;
            }
            GridExControl.DataSource = GridBindingSource;
            GridExControl.MainGrid.BestFitColumns();
        }

        private class DataModel
        {
            public string CustomerCode { get; set; }
            public string CustomerName { get; set; }
            public string ItemNm1 { get; set; }
            public string ItemNm { get; set; }
            public int? SumOkQty { get; set; }
            public int? SumFailQty { get; set; }
        }
    }
}
