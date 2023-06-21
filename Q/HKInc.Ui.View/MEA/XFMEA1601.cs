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
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using DevExpress.Utils;
using HKInc.Utils.Enum;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.MEA
{
    /// <summary>
    /// 2021-11-04 김진우 주임 추가
    /// 설비 스페어파트 입출고관리
    /// </summary>
    public partial class XFMEA1601 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1600> ModelService = (IService<TN_MEA1600>)ProductionFactory.GetDomainService("TN_MEA1600");

        public XFMEA1601()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
        }

        protected override void InitCombo()
        {
            lup_Spare.SetDefault(true, "SpareCode", "SpareName", ModelService.GetList(p => true).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            // 20220331 오세완 차장 화면설계서대로 출력하는 걸로 변경
            //MasterGridExControl.MainGrid.AddColumn("MachineMoldCheck", "스페어파트 구분", false);
            MasterGridExControl.MainGrid.AddColumn("MachineMoldCheck", "스페어파트 구분");
            MasterGridExControl.MainGrid.AddColumn("SpareCode", "설비스페어코드");
            MasterGridExControl.MainGrid.AddColumn("SpareName", "설비스페어명");
            // 20220331 오세완 차장 화면설계서 대로 출력
            MasterGridExControl.MainGrid.AddColumn("SpareNameENG", "설비스페어명(영문)");
            MasterGridExControl.MainGrid.AddColumn("SpareNameCHN", "설비스페어명(중문)");

            MasterGridExControl.MainGrid.AddColumn("MainCustomerCode", "주거래처");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");
            // 20220331 오세완 차장 화면설계서에 없어서 생략 처리 
            //MasterGridExControl.MainGrid.AddColumn("Unit", "단위");

            MasterGridExControl.MainGrid.AddColumn("Cost", "단가");
            MasterGridExControl.MainGrid.AddColumn("SpareStockQty", "재고량");
            MasterGridExControl.MainGrid.AddColumn("SafeQty", "안전재고");
            MasterGridExControl.MainGrid.AddColumn("StockPosition", "기본보관위치");
            MasterGridExControl.MainGrid.AddColumn("ProdFileName", "제품사진명");

            MasterGridExControl.MainGrid.AddColumn("ProdFileUrl", "제품사진URL", false);
            // 20220331 오세완 차장 없어도 되서 생략
            //MasterGridExControl.MainGrid.AddColumn("ProdFileImage", "제품사진이미지", false);
            //MasterGridExControl.MainGrid.AddColumn("UploadFilePath", "업로드파일경로", false);
            //MasterGridExControl.MainGrid.AddColumn("DeleteFilePath", "삭제파일경로", false);
            //MasterGridExControl.MainGrid.AddColumn("UseFlag", "사용여부");
            //MasterGridExControl.MainGrid.AddColumn("Memo", "메모");

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번", false);
            DetailGridExControl.MainGrid.AddColumn("InOutDate", "날짜");
            DetailGridExControl.MainGrid.AddColumn("Division", "구분");
            DetailGridExControl.MainGrid.AddColumn("Qty", "수량");
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.AddColumn("InOutId", "작성자");
            DetailGridExControl.MainGrid.AddColumn("CreateTime", "작성날짜", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm");    // 2021-12-24 김진우주임  날짜 형식 변경
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InOutDate", "Division", "Qty", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1601>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("MachineMoldCheck", DbRequestHandler.GetCommCode(MasterCodeSTR.MACHINESPAREPART), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_SpareImage, "ProdFileName", "ProdFileUrl", false);
            // 20220331 오세완 차장 없어도 되서 생략
            //MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            //MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", DbRequestHandler.GetCommCode(MasterCodeSTR.INOUTDIVISION), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("InOutDate");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("Qty");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InOutId", ModelService.GetChildList<User>(p => true).ToList(), "LoginId", "UserName", true);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("SpareCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            var spareCode = lup_Spare.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(spareCode) ? true : p.SpareCode == spareCode)
                                                                        && (p.UseFlag == "Y")
                                                                     ).OrderBy(p => p.SpareCode)
                                                                     .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_MEA1600 masterObj = MasterGridBindingSource.Current as TN_MEA1600;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_MEA1601List.OrderBy(p => p.CreateTime).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            TN_MEA1600 masterObj = MasterGridBindingSource.Current as TN_MEA1600;
            if (masterObj == null) return;

            if (!masterObj.TN_MEA1601List.Any(p => p.NewRowFlag == "Y"))
            {
                TN_MEA1601 newObj = new TN_MEA1601()
                {
                    Seq = masterObj.TN_MEA1601List.Count == 0 ? 1 : masterObj.TN_MEA1601List.Max(p => p.Seq) + 1,
                    InOutDate = DateTime.Today,
                    InOutId = GlobalVariable.LoginId,
                    Qty = 0
                };
                masterObj.TN_MEA1601List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_MEA1600 masterObj = MasterGridBindingSource.Current as TN_MEA1600;
            if (masterObj == null) return;

            TN_MEA1601 detailObj = DetailGridBindingSource.Current as TN_MEA1601;
            if (detailObj == null) return;

            masterObj.TN_MEA1601List.Remove(detailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        /// <summary>
        /// 2021-11-30 김진우 주임 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (e.RowHandle >= 0)
            {
                int CheckSafeQty = Convert.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["SafeQty"]));
                int SpareStockQty = Convert.ToInt32(View.GetRowCellValue(e.RowHandle, View.Columns["SpareStockQty"]));

                if (CheckSafeQty.GetNullToEmpty() != "")
                {
                    if (SpareStockQty < CheckSafeQty /* || SpareStockQty != 0*/)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }
    }
}