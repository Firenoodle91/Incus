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
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using System.Collections.Specialized;
using HKInc.Service.Helper;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Enum;

namespace HKInc.Ui.View.MEA
{
    /// <summary>
    /// 2021-11-04 김진우 주임 추가
    /// 설비스페어파트관리
    /// </summary>
    public partial class XFMEA1600 : Service.Base.ListFormTemplate
    {
        IService<TN_MEA1600> ModelService = (IService<TN_MEA1600>)ProductionFactory.GetDomainService("TN_MEA1600");

        public XFMEA1600()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            GridExControl.MainGrid.MainView.CellValueChanged += GirdView_CellValueChanged;

            //GridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            //GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;

            rbo_UseFlag.SetLabelText("사용", "미사용", "전체");
        }

        /// </summary>
        protected override void InitCombo()
        {
            lup_SpareCodeName.SetDefault(true, "SpareCode", "SpareName", ModelService.GetList(p => true).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            // 20220331 오세완 차장 화면설계서대로 출력하는 걸로 변경
            //GridExControl.MainGrid.AddColumn("MachineMoldCheck", "스페어파트 구분", false);           
            GridExControl.MainGrid.AddColumn("MachineMoldCheck", "스페어파트 구분");
            GridExControl.MainGrid.AddColumn("SpareCode", "설비스페어코드");
            GridExControl.MainGrid.AddColumn("SpareName", "설비스페어명");
            // 20220331 오세완 차장 화면설계서 대로 출력
            GridExControl.MainGrid.AddColumn("SpareNameENG", "설비스페어명(영문)");
            GridExControl.MainGrid.AddColumn("SpareNameCHN", "설비스페어명(중문)");

            GridExControl.MainGrid.AddColumn("MainCustomerCode", "주거래처");
            GridExControl.MainGrid.AddColumn("Spec1", "규격1");
            GridExControl.MainGrid.AddColumn("Spec2", "규격2");
            GridExControl.MainGrid.AddColumn("Spec3", "규격3");
            GridExControl.MainGrid.AddColumn("Spec4", "규격4");
            
            // 20220331 오세완 차장 화면설계서에 없어서 생략 처리 
            //GridExControl.MainGrid.AddColumn("Unit", "단위");
            GridExControl.MainGrid.AddColumn("Cost", "단가");
            GridExControl.MainGrid.AddColumn("SpareStockQty", "재고량");
            GridExControl.MainGrid.AddColumn("SafeQty", "안전재고");
            GridExControl.MainGrid.AddColumn("StockPosition", "기본보관위치");
            GridExControl.MainGrid.AddColumn("ProdFileName", "제품사진명");

            GridExControl.MainGrid.AddColumn("ProdFileUrl", "제품사진URL", false);
            // 20220331 오세완 차장 없어도 되서 생략
            //GridExControl.MainGrid.AddColumn("ProdFileImage", "제품사진이미지", false);
            GridExControl.MainGrid.AddColumn("UploadFilePath", "업로드파일경로", false);
            GridExControl.MainGrid.AddColumn("DeleteFilePath", "삭제파일경로", false);
            GridExControl.MainGrid.AddColumn("UseFlag", "사용여부");
            GridExControl.MainGrid.AddColumn("Memo", "메모");

            // 20220331 오세완 차장 추가되거나 생략된 컬럼이 있어서 수정
            //GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "MachineMoldCheck", "SpareName", "MainCustomerCode", "Spec1", "Spec2", "Spec3", "Spec4", "Unit", "Cost", "SafeQty", "StockPosition", "ProdFileName", "UseFlag", "Memo");
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "MachineMoldCheck", "SpareName", "SpareNameENG", "SpareNameCHN", "MainCustomerCode", 
                "Spec1", "Spec2", "Spec3", "Spec4", "Cost", "SafeQty", "StockPosition", "ProdFileName", "UseFlag", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1600>(GridExControl);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineMoldCheck", DbRequestHandler.GetCommCode(MasterCodeSTR.MACHINESPAREPART), "Mcode", "Codename", true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StockPosition", ModelService.GetChildList<TN_WMS2000>(p => true).ToList(), "WhPositionCode", "WhPositionName", true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode","CustomerName");
            GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, GridExControl, MasterCodeSTR.FtpFolder_SpareImage, "ProdFileName", "ProdFileUrl", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("SpareCode");
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            var spareCodeName = lup_SpareCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(spareCodeName) ? true : (p.SpareCode.Contains(spareCodeName) || (p.SpareName.Contains(spareCodeName))))
                                                                  && (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                     ).OrderBy(p => p.SpareCode)
                                                                     .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void AddRowClicked()
        {
            var newObj = new TN_MEA1600()
            {
                SpareCode = DbRequestHandler.GetRequestNumber("SPARE"),
                MachineMoldCheck = "01",
                NewRowFlag = "Y",
                UseFlag = "Y",                
            };

            GridBindingSource.Add(newObj);
            ModelService.Insert(newObj);
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_MEA1600;

            if (obj != null)
            {
                var result = MessageBoxHandler.Show("스페어파트정보의 삭제는 시스템 전체에 영향을 미치므로 삭제가 불가합니다. 사용여부 해제를 하시겠습니까?", LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    obj.UseFlag = "N";
                    GridExControl.BestFitColumns();
                }
            }
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();
            
            if (GridBindingSource != null && GridBindingSource.DataSource != null)
            {
                List<TN_MEA1600> masterList = GridBindingSource.List as List<TN_MEA1600>;
                foreach (var v in masterList)
                {
                    if (v.NewRowFlag == "Y")
                    {
                        if (v.ProdFileUrl != null && (v.ProdFileUrl.Contains("\\")))      // 수정시 구분
                        {
                            string[] filename = v.ProdFileUrl.ToString().Split('\\');
                            if (filename.Length != 1)
                            {
                                var realFileName = v.SpareCode + "_" + filename[filename.Length - 1];
                                var ftpFileUrl = MasterCodeSTR.FtpFolder_SpareImage + "/" + realFileName;

                                FileHandler.UploadFTP(v.ProdFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_SpareImage + "/");

                                v.ProdFileName = realFileName;
                                v.ProdFileUrl = ftpFileUrl;
                            }
                        }
                        ModelService.Insert(v);
                    }
                    else if (v.EditRowFlag == "Y")
                    {
                        if (v.ProdFileUrl != null && (v.ProdFileUrl.Contains("\\")))      // 수정시 구분
                        {
                            string[] filename = v.ProdFileUrl.ToString().Split('\\');
                            if (filename.Length != 1)
                            {
                                var realFileName = v.SpareCode + "_" + filename[filename.Length - 1];
                                var ftpFileUrl = MasterCodeSTR.FtpFolder_SpareImage + "/" + realFileName;

                                FileHandler.UploadFTP(v.ProdFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_SpareImage + "/");

                                v.ProdFileName = realFileName;
                                v.ProdFileUrl = ftpFileUrl;
                            }
                        }
                        ModelService.Update(v);
                    }
                }
            }
            ModelService.Save();
            DataLoad();
        }

        /// <summary>
        /// 수정시 EditRowFlag 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GirdView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_MEA1600 Obj = GridBindingSource.Current as TN_MEA1600;
            if (e.Column.Name == "ProdFileName")
            {
                Obj.ProdFileUrl = Obj.ProdFileName;             // 2022-05-27 김진우 추가
                Obj.EditRowFlag = "Y";
            }
        }

        #region 주석
        /// <summary>
        /// 셀 변경 시 Edit 체크를 위함.
        /// </summary>
        //private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    var masterObj = GridBindingSource.Current as TN_MEA1001;
        //    if (masterObj == null) return;

        // 2021-05-27 김진우 주임        새로 생성된 행에 대해서 채번
        //if (masterObj.NewRowFlag == "Y" && masterObj.SpareCode == null)
        //{
        //if (masterObj.MachineMoldCheck == "01")
        //{
        //    masterObj.SpareCode = DbRequestHandler.GetRequestNumber("SPARE");
        //    masterObj.TopCategory = MasterCodeSTR.TopCategory_SPARE;
        //}
        //else if (masterObj.MachineMoldCheck == "02")
        //{
        //    masterObj.SpareCode = DbRequestHandler.GetRequestNumber("MSPARE");
        //    masterObj.TopCategory = MasterCodeSTR.TopCategory_Mold;
        //}
        //}

        //masterObj.EditRowFlag = "Y";
        //}

        //private void MainView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    TN_MEA1001 Obj = GridBindingSource.Current as TN_MEA1001;
        //    GridView gv = sender as GridView;

        //    if (e.Column.Name.ToString() == "MachineMoldCheck")
        //    {
        //        if (Obj.MachineMoldCheck == null)
        //            gv.Columns[0].OptionsColumn.AllowEdit = true;
        //        else
        //            gv.Columns[0].OptionsColumn.AllowEdit = false;
        //    }
        ////}

        //private void ColumnEdit_KeyDown(object sender, KeyEventArgs e)
        //{
        //    var masterObj = GridBindingSource.Current as TN_MEA1600;
        //    if (masterObj == null) return;

        //    if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
        //    {
        //        StringCollection list = Clipboard.GetFileDropList();
        //        if (list != null && list.Count > 0 && ExtensionHelper.picExtensions.Contains(Path.GetExtension(list[0]).ToLower()))
        //        {
        //            using (FileStream fs = new FileStream(list[0], FileMode.OpenOrCreate, FileAccess.Read))
        //            {
        //                byte[] fileData = new byte[fs.Length];
        //                fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
        //                fs.Close();

        //                masterObj.localImage = fileData;
        //                masterObj.ProdFileName = list[0];
        //                masterObj.ProdFileUrl = list[0];
        //            }
        //        }
        //        else
        //        {
        //            var GetImage = Clipboard.GetImage();
        //            if (GetImage != null)
        //            {
        //                masterObj.ProdFileName = "Clipboard_Image";
        //                masterObj.ProdFileUrl = "Clipboard_Image";
        //                masterObj.localImage = GetImage;
        //            }
        //        }
        //        GridExControl.BestFitColumns();
        //        masterObj.EditRowFlag = "Y";
        //    }
        //}

        /// <summary>
        /// 2021-06-07 김진우 주임
        /// 구분에서 선택시 수정불가하도록 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        //{
        //    TN_MEA1600 Obj = GridBindingSource.Current as TN_MEA1600;
        //    GridView gv = sender as GridView;


        //    if (Obj.MachineMoldCheck == null)
        //        gv.Columns[0].OptionsColumn.AllowEdit = true;
        //    else
        //        gv.Columns[0].OptionsColumn.AllowEdit = false;
        //}

        /// <summary>
        /// 2021-06-07 김진우 주임
        /// 구분에서 선택시 수정불가하도록 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void MainView_RowCellClick(object sender, RowCellClickEventArgs e)
        //{
        //    TN_MEA1600 Obj = GridBindingSource.Current as TN_MEA1600;
        //    GridView gv = sender as GridView;

        //    if (e.Column.Name.ToString() == "MachineMoldCheck")
        //    {
        //        if (Obj.MachineMoldCheck == null)
        //            gv.Columns[0].OptionsColumn.AllowEdit = true;
        //        else
        //            gv.Columns[0].OptionsColumn.AllowEdit = false;
        //    }
        //}
        #endregion

    }
}