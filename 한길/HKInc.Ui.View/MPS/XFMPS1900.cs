using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using System.Data.SqlClient;
using HKInc.Utils.Enum;
using HKInc.Ui.View.POP_Popup;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 이동표관리화면
    /// </summary>
    public partial class XFMPS1900 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        private ToolTipController defController = ToolTipController.DefaultController;

        public XFMPS1900()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            //textWorkNo.ToolTipController = defController;
        }

        protected override void InitCombo()
        {
            defController.AutoPopDelay = 10000000;
            defController.KeepWhileHovered = true;
            defController.CloseOnClick = DefaultBoolean.False;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "이동표출력[F10]", IconImageList.GetIconImage("print/printer"));
            MasterGridExControl.MainGrid.AddColumn("MoveNo", "이동표번호");
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("LotNo", "생산 LOT NO");
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("CustomerName", "수주처");
            MasterGridExControl.MainGrid.AddColumn("OrderNo", "수주번호");
            MasterGridExControl.MainGrid.AddColumn("Memo", "비고");

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("ProcessName", "공정");
            DetailGridExControl.MainGrid.AddColumn("OkQty", "누적양품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("WorkId", "작업자");
            DetailGridExControl.MainGrid.AddColumn("WorkDate", "작업일", HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            string WorkNo = textWorkNo.EditValue.GetNullToEmpty();

            if (WorkNo.IsNullOrEmpty())
            {                
                defController.Appearance.BackColor = Color.AntiqueWhite;
                defController.ShowBeak = true;
                defController.ShowHint("필수 항목입니다.", textWorkNo, ToolTipLocation.RightTop);
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var WorkNoParam = new SqlParameter("@WorkNo", WorkNo);

                var result = context.Database.SqlQuery<TP_MPS1900_Master>("SP_ITEM_MOVE_GET_MASTER @WorkNo", WorkNoParam).ToList();

                MasterGridBindingSource.DataSource = result.ToList();
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var WorkNoParam = new SqlParameter("@WorkNo", WorkNo);

                var result = context.Database.SqlQuery<TP_MPS1900_Detail>("SP_ITEM_MOVE_GET_DETAIL @WorkNo", WorkNoParam).ToList();

                DetailGridBindingSource.DataSource = result.ToList();
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            DetailGridExControl.DataSource = DetailGridBindingSource;

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void FileChooseClicked()
        {
            var obj = MasterGridBindingSource.Current as TP_MPS1900_Master;
            if (obj == null || obj.MoveNo.IsNullOrEmpty()) return;
            XRITEMMOVE prt = new XRITEMMOVE("", "", obj.MoveNo); 
            ReportPrintTool printTool = new ReportPrintTool(prt);
            //printTool.ShowPreview();

            printTool.PrintingSystem.ShowMarginsWarning = false;
            //printTool.rep.ShowPrintStatusDialog = false;
            printTool.ShowPreview();
            //printTool.Print();
        }
    }
}