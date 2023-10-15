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
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using System.Drawing.Imaging;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 초중종현황판
    /// </summary>
    public partial class XFFME_STATUS : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_QCT1100> ModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100"); //무의미
        List<TEMP_XFFME_STATUS> MainDateList = new List<TEMP_XFFME_STATUS>();
        List<byte[]> imageList = new List<byte[]>();

        private Timer timer1 = new Timer();

        public XFFME_STATUS()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;

            MasterGridExControl.MainGrid.MainView.CustomUnboundColumnData += MainView_CustomUnboundColumnData;
            MasterGridExControl.MainGrid.MainView.KeyDown += MainView_KeyDown;
            this.FormClosed += XFFME_STATUS_FormClosed;
        }

        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (!UserRight.HasSelect)
                    ActRefresh();
            }
        }

        protected override void InitCombo()
        {
            timer1.Interval = 10000;
            timer1.Tick += Timer1_Tick;

            var loadingBitmap = new Bitmap(Properties.Resources.Loading);
            MemoryStream ms = new MemoryStream();
            loadingBitmap.Save(ms, ImageFormat.Gif);
            imageList.Add(ms.ToArray());

            var OK_Bitmap = new Bitmap(Properties.Resources.OK);
            MemoryStream ms2 = new MemoryStream();
            OK_Bitmap.Save(ms2, ImageFormat.Png);
            imageList.Add(ms2.ToArray());

            var NG_Bitmap = new Bitmap(Properties.Resources.NG);
            MemoryStream ms3 = new MemoryStream();
            NG_Bitmap.Save(ms3, ImageFormat.Png);
            imageList.Add(ms3.ToArray());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();

            MasterGridExControl.SetToolbarVisible(false);

            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("WorkDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineName"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("ResultQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "n0");

            MasterGridExControl.MainGrid.AddUnboundColumn("FirstResultImage", LabelConvert.GetLabelText("CheckResultF"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            MasterGridExControl.MainGrid.AddUnboundColumn("MidResultImage", LabelConvert.GetLabelText("CheckResultM"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            MasterGridExControl.MainGrid.AddUnboundColumn("EndResultImage", LabelConvert.GetLabelText("CheckResultE"), DevExpress.Data.UnboundColumnType.Object, null, FormatType.None, null);
            MasterGridExControl.MainGrid.AddColumn("FirstResult", LabelConvert.GetLabelText("CheckResultF"), HorzAlignment.Center, false);
            MasterGridExControl.MainGrid.AddColumn("MidResult", LabelConvert.GetLabelText("CheckResultM"), HorzAlignment.Center, false);
            MasterGridExControl.MainGrid.AddColumn("EndResult", LabelConvert.GetLabelText("CheckResultE"), HorzAlignment.Center, false);

            MasterGridExControl.MainGrid.AddColumn("RowNum", LabelConvert.GetLabelText("RowNum"), HorzAlignment.Center, false);

            MasterGridExControl.MainGrid.Columns["FirstResultImage"].MinWidth = 100;
            MasterGridExControl.MainGrid.Columns["FirstResultImage"].MaxWidth = 100;
            MasterGridExControl.MainGrid.Columns["FirstResultImage"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            MasterGridExControl.MainGrid.Columns["MidResultImage"].MinWidth = 100;
            MasterGridExControl.MainGrid.Columns["MidResultImage"].MaxWidth = 100;
            MasterGridExControl.MainGrid.Columns["MidResultImage"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            MasterGridExControl.MainGrid.Columns["EndResultImage"].MinWidth = 100;
            MasterGridExControl.MainGrid.Columns["EndResultImage"].MaxWidth = 100;
            MasterGridExControl.MainGrid.Columns["EndResultImage"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            //MasterGridExControl.MainGrid.Columns["FirstResult"].MinWidth = 100;
            //MasterGridExControl.MainGrid.Columns["FirstResult"].MaxWidth = 100;

            //MasterGridExControl.MainGrid.Columns["MidResult"].MinWidth = 100;
            //MasterGridExControl.MainGrid.Columns["MidResult"].MaxWidth = 100;

            //MasterGridExControl.MainGrid.Columns["EndResult"].MinWidth = 100;
            //MasterGridExControl.MainGrid.Columns["EndResult"].MaxWidth = 100;

            MasterGridExControl.MainGrid.MainView.RowHeight = 50;
            MasterGridExControl.MainGrid.SetGridFont(this.MasterGridExControl.MainGrid.MainView, new Font(DefaultFont.FontFamily, 14f, FontStyle.Bold));
         
        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("ItemName", ModelService.GetChildList<TN_STD1100>(p => 1 == 1).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process));

            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("FirstResultImage");
            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("MidResultImage");
            MasterGridExControl.MainGrid.SetRepositoryItemPictureEdit("EndResultImage");

            MasterGridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
            timer1.Start();
        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            MainDateList.Clear();

            // 20210809 오세완 차장 여기는 조회조건이 없어서 이걸 실행하게 되면 출력에 문제가 발생한다. 
            //InitCombo(); //phs20210624
            //InitRepository();//phs20210624

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var result = context.Database.SqlQuery<TEMP_XFFME_STATUS>("USP_GET_FME_MONITERING_DAY").ToList();

                if (result == null) return;

                MainDateList.AddRange(result);

                MasterGridBindingSource.DataSource = MainDateList.Where(p => p.RowNum <= 10).ToList();                
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            if (timer1.Enabled) timer1.Stop();
            timer1.Start();
            SetMessage("모니터링 진행중...");
        }

        private void MainView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if ((e.Column.FieldName == "FirstResultImage" || e.Column.FieldName == "MidResultImage" || e.Column.FieldName == "EndResultImage") && e.IsGetData)
            {
                var judge = MasterGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, e.Column.FieldName.Replace("Image", "")).GetNullToEmpty();
                if (judge == "1")
                    e.Value = imageList[0];
                if (judge == "2")
                    e.Value = imageList[1];
                if (judge == "3")
                    e.Value = imageList[2];
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!this.IsActive)
            {
                timer1.Stop();
                SetMessage("화면이 변경되어 모니터링이 중지되었습니다. 다시 진행하시려면 조회[F1] 버튼을 이용해 주시기 바랍니다.");
                return;
            }

            if (MasterGridBindingSource == null || MasterGridBindingSource.DataSource == null)
            {
                timer1.Stop();
            }
            else
            {
                List<TEMP_XFFME_STATUS> DateList = MasterGridBindingSource.List as List<TEMP_XFFME_STATUS>;
                //수정ㅇㅇㅇㅇ
                if(DateList != null)
                {
                    decimal MaxRowNum = DateList.Count == 0 ? 0 : DateList.Max(p => p.RowNum).GetDecimalNullToZero();
                    if(MaxRowNum == MainDateList.Count)
                    {
                        timer1.Stop();
                        ActRefresh();
                    }
                    else
                    {
                        MasterGridBindingSource.DataSource = MainDateList.Where(p => p.RowNum > MaxRowNum && p.RowNum <= MaxRowNum + 10).ToList();

                        MasterGridExControl.DataSource = MasterGridBindingSource;
                        MasterGridExControl.BestFitColumns();
                    }                    
                }
            }
        }

        private void XFFME_STATUS_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
        }

    }
}
