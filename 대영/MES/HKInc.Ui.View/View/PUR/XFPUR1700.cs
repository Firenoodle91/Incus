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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using System.IO;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 자재반품관리
    /// </summary>
    public partial class XFPUR1700 : Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1304> ModelService = (IService<TN_PUR1304>)ProductionFactory.GetDomainService("TN_PUR1304");

        public XFPUR1700()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.AllowDrop = true;

            MasterGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailMainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.DragOver += new System.Windows.Forms.DragEventHandler(grid_DragOver);
            DetailGridExControl.MainGrid.DragDrop += new System.Windows.Forms.DragEventHandler(grid_DragDrop);

            dt_DisposalDate.SetTodayIsMonth();      // 2021-06-29 김진우 주임 추가     날짜 초기화
        }

        private void grid_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point p = Control.MousePosition;
            Point mp = (this.DetailGridExControl.MainGrid.PointToClient(p));
            GridHitInfo hitInfo = this.DetailGridExControl.MainGrid.MainView.CalcHitInfo(mp);
            if (hitInfo.Column.FieldName == "InCustomerLotNo")
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            //if (e.Data.GetDataPresent(typeof(Drag_n_Drop_2Forms.User_DataSet.UserRow)))
            //{
            //    e.Effect = DragDropEffects.Move;

            //    // *********** Added Code ******** (start) ************************************
            //    //GridHitInfo srcHitInfo = e.Data.GetData(typeof(GridHitInfo)) as GridHitInfo;
            //    Drag_n_Drop_2Forms.User_DataSet.UserRow dr = e.Data.GetData(typeof(Drag_n_Drop_2Forms.User_DataSet.UserRow)) as Drag_n_Drop_2Forms.User_DataSet.UserRow;
            //    if (dr == null)
            //    {
            //        lblID_Value.Text = "Unable to obtain the row number.";
            //        return;
            //    }
            //    else
            //    {
            //        lblID_Value.Text = "Row Number: " + dr["id"].ToString();
            //    }

            //    //int sourceRow = srcHitInfo.RowHandle;
            //    //lblID_Value.Text = "It came from row number " + sourceRow.ToString();
            //    // *********** Added Code ******** (end) **************************************
            //}
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //}
        }

        private void grid_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            var data = e.Data.GetData(typeof(string));
            if (data.IsNullOrEmpty()) return;

            Point p = Control.MousePosition;

            Point mp = (this.DetailGridExControl.MainGrid.PointToClient(p));
            GridHitInfo hitInfo = this.DetailGridExControl.MainGrid.MainView.CalcHitInfo(mp);
            int targetRow = hitInfo.RowHandle;
            DetailGridExControl.MainGrid.MainView.SetRowCellValue(targetRow, "InCustomerLotNo", data);

            dt_DisposalDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dt_DisposalDate.DateToEdit.DateTime = DateTime.Today;


            //DataRow row = e.Data.GetData(typeof(Drag_n_Drop_2Forms.User_DataSet.UserRow)) as DataRow;
            //if (row != null && table != null && row.Table != table)
            //{

            //    // *********** Added Code ******** (start) ************************************
            //    try
            //    {

            //        GridView view = grid.MainView as GridView;
            //        Point p = Control.MousePosition;

            //        Point mp = (this.gridControl2.PointToClient(p));
            //        GridHitInfo hitInfo = this.gridView2.CalcHitInfo(mp);
            //        int targetRow = hitInfo.RowHandle;
            //        object o = this.gridView2.GetRowCellValue(targetRow, "Id");
            //        string number = "";
            //        if (o != null)
            //        {
            //            number = this.gridView2.GetRowCellValue(targetRow, "Id").ToString();
            //        }

            //        //lblID_Value.Text = "It came from row number " + row["id"] + ".";
            //        lblID_Value.Text = lblID_Value.Text + " Then it went to number " + number;
            //        table.ImportRow(row);
            //        row.Delete();
            //    }
            //    catch
            //    {
            //        lblID_Value.Text = "Unable to obtain the Id or User Name.";
            //    }
            //    // *********** Added Code ******** (end) **************************************

            //}
        }

        protected override void InitCombo()
        {          
            lup_InCustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == null)).ToList());
            lup_DisposalId.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.ScmYn == "N" && p.Active == "Y" && p.MainYn == "02").ToList());
        }

        protected override void InitGrid()
        {
            IsMasterGridButtonFileChooseEnabled = UserRight.HasEdit;            
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, false);
            MasterGridExControl.MainGrid.AddColumn("ReturnNo", LabelConvert.GetLabelText("ReturnNo"));
            MasterGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"));
            MasterGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"));
            MasterGridExControl.MainGrid.AddColumn("InCustomerDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("InCustomerId", LabelConvert.GetLabelText("InCustomerId"));
            MasterGridExControl.MainGrid.AddColumn("InDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("InCustomerCode", LabelConvert.GetLabelText("InCustomer"));
            MasterGridExControl.MainGrid.AddColumn("ReturnDate", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("ReturnId", LabelConvert.GetLabelText("ReturnId"));            
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ReturnDate", "ReturnId", "Memo");

            DetailGridExControl.MainGrid.AddColumn("ReturnNo", LabelConvert.GetLabelText("ReturnNo"), false);
            DetailGridExControl.MainGrid.AddColumn("InNo", LabelConvert.GetLabelText("InNo"), false);
            DetailGridExControl.MainGrid.AddColumn("InSeq", LabelConvert.GetLabelText("InSeq"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            DetailGridExControl.MainGrid.AddColumn("PoNo", LabelConvert.GetLabelText("PoNo"), false);
            DetailGridExControl.MainGrid.AddColumn("PoSeq", LabelConvert.GetLabelText("PoSeq"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.StockInspFlag", LabelConvert.GetLabelText("StockInspFlag"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InInspectionState", LabelConvert.GetLabelText("InInspectionState"), HorzAlignment.Center, true);            
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InCost", LabelConvert.GetLabelText("InCost"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddUnboundColumn("InAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([InQty],0) * ISNULL([InCost],0)", FormatType.Numeric, "#,###,###,###.##");            
            DetailGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            DetailGridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("InspectOkQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("NgQty", LabelConvert.GetLabelText("InspectNgQty"), HorzAlignment.Far, FormatType.Numeric, "n0");            
            DetailGridExControl.MainGrid.AddColumn("ReturnPossiQty", LabelConvert.GetLabelText("ReturnPossiQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ReturnQty", LabelConvert.GetLabelText("ReturnQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InCustomerLotNo", LabelConvert.GetLabelText("InCustomerLotNo"),false);
            DetailGridExControl.MainGrid.AddColumn("ReturnWhCode", LabelConvert.GetLabelText("ReturnWhCode"),false);
            DetailGridExControl.MainGrid.AddColumn("ReturnWhPosition", LabelConvert.GetLabelText("ReturnWhPosition"), false);
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("AttachFile"));
            DetailGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            DetailGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            DetailGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ReturnQty");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1304>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_PUR1305>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ReturnDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnId", ModelService.GetChildList<User>(p => p.Active == "Y" && p.MainYn == "02"), "LoginId", "UserName");            
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));        // 2021-06-29 김진우 주임 추가     업체명이 코드명 그대로 나와서 추가

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));            
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("ReturnQty", DefaultBoolean.Default, "n2");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnWhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_MAT || p.Temp == MasterCodeSTR.WhCodeDivision_BU || p.Temp == null) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReturnWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_Inspection_IN_File, "FileName", "FileUrl");

        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InputNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            string inCustomerCode = lup_InCustomerCode.EditValue.GetNullToEmpty();
            string inId = lup_DisposalId.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.ReturnDate >= dt_DisposalDate.DateFrEdit.DateTime
                                                                         && p.ReturnDate <= dt_DisposalDate.DateToEdit.DateTime)
                                                                         && (string.IsNullOrEmpty(inCustomerCode) ? true : p.InCustomerCode == inCustomerCode)
                                                                         && (string.IsNullOrEmpty(inId) ? true : p.InId == inId)
                                                                         //&& p.ReturnYn == "Y"
                                                                      )
                                                                      .OrderBy(o => o.InDate)
                                                                      .ThenBy(o => o.InNo)
                                                                      .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1304;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            DetailGridBindingSource.DataSource = masterObj.TN_PUR1305List.OrderBy(p => p.InSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }
        

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            //IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR1100, param, MasterAddRowPopupCallback);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR1200_RETURN, param, MasterAddRowPopupCallback);

            form.ShowPopup(true);
        }

        private void MasterAddRowPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<VI_PUR1304>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnObj in returnList)
            {
                var newObj = (TN_PUR1304)MasterGridBindingSource.AddNew();
                newObj.ReturnNo = DbRequestHandler.GetSeqDay("RIN");
                newObj.InNo = returnObj.InNo;
                newObj.PoNo = returnObj.PoNo;
                newObj.InDate = returnObj.InDate;
                newObj.InId = returnObj.InId;
                newObj.InCustomerCode = returnObj.InCustomerCode;
                newObj.InCustomerDate = returnObj.InCustomerDate;
                newObj.InCustomerId = returnObj.InCustomerId;
                newObj.ReturnDate = DateTime.Today;
                newObj.ReturnId = GlobalVariable.LoginId;
                ModelService.Insert(newObj);
            }

            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                MasterGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1304;
            if (masterObj == null) return;

            if (masterObj.TN_PUR1305List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("ReturnMasterInfo"), LabelConvert.GetLabelText("ReturnMasterInfo"), LabelConvert.GetLabelText("ReturnMasterInfo")));
                return;
            }
            ModelService.Delete(masterObj);
            MasterGridBindingSource.RemoveCurrent();
        }
       

        protected override void DetailAddRowClicked()
        {
            var obj = MasterGridBindingSource.Current as TN_PUR1304;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, obj.PoNo);
            param.SetValue(PopupParameter.Value_1, obj.InNo);            
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PUR1201_RETURN, param, DetailAddRowPopupCallback);
            form.ShowPopup(true);
        }

        private void DetailAddRowPopupCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_PUR1304;
            if (masterObj == null) return;

            //var itemCodeCheck = string.Empty;
            //if (masterObj.TN_PUR1201List.Count > 0)
            //    itemCodeCheck = masterObj.TN_PUR1201List.First().ItemCode;

            var returnList = (List<VI_PUR1305>)e.Map.GetValue(PopupParameter.ReturnObject);
            //foreach (var v in returnList)
            //{
            //    if (!itemCodeCheck.IsNullOrEmpty())
            //    {
            //        if (itemCodeCheck != v.ItemCode)
            //        {
            //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_112));
            //            return;
            //        }
            //    }

            //    itemCodeCheck = v.ItemCode;
            //}
            
            foreach (var v in returnList)
            {
                //var poDetailObj = ModelService.GetChildList<TN_PUR1305>(p => p.PoNo == v.PoNo && p.PoSeq == v.PoSeq).FirstOrDefault();
                var newObj = new TN_PUR1305();
                newObj.ReturnNo = masterObj.ReturnNo;
                newObj.ReturnSeq = masterObj.TN_PUR1305List.Count == 0 ? 1 : masterObj.TN_PUR1305List.Max(p => p.ReturnSeq) + 1;
                newObj.InInspectionState = v.InInspectionState;
                newObj.InNo = v.InNo;
                newObj.InSeq = v.InSeq;
                newObj.PoNo = v.PoNo;
                newObj.PoSeq = v.PoSeq;
                newObj.ItemCode = v.ItemCode;
                newObj.InQty = v.InQty;
                newObj.InCost = v.InCost;
                newObj.InLotNo = v.InLotNo;
                newObj.InCustomerLotNo = v.InCustomerLotNo;
                newObj.ReturnWhCode = v.InWhCode;
                newObj.ReturnWhPosition = v.InWhPosition;
                newObj.ReturnPossiQty = v.StockQty;
                newObj.OkQty = v.OkQty;
                newObj.NgQty = v.NgQty;
                //newObj.Temp = newObj.InLotNo;
                newObj.NewRowFlag = "Y";
                
                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == newObj.TN_STD1100.StockPosition).FirstOrDefault();
                if (WMS2000 != null)
                {
                    newObj.ReturnWhCode = WMS2000.WhCode;
                    newObj.ReturnWhPosition = WMS2000.PositionCode;
                }
                newObj.TN_PUR1304 = masterObj;
                masterObj.TN_PUR1305List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
            }

            if (returnList.Count > 0)
            {
                SetIsFormControlChanged(true);
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_PUR1304;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_PUR1305;
            if (detailObj == null) return;

            int cnt = ModelService.GetChildList<SCM_VI_PUR1305>(p => p.ReturnNo == detailObj.ReturnNo && p.ReturnSeq == detailObj.ReturnSeq && p.PoNo == detailObj.PoNo
             && p.PoSeq == detailObj.PoSeq).Count();
            if (cnt == 0)
            {

                DetailGridBindingSource.RemoveCurrent();
                masterObj.TN_PUR1305List.Remove(detailObj);
            }
            else{
                MessageBox.Show("협력업체에서 확인한 반품은 삭제할수 없습니다.");
                return;
            }
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            if (DetailGridBindingSource != null && DetailGridBindingSource.DataSource != null)
            {
                var detailList = DetailGridBindingSource.List as List<TN_PUR1305>;
                var editList = detailList.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count > 0)
                {
                    foreach (var v in editList.Where(c => c.FileUrl != null && c.FileUrl.Contains("\\")).ToList())
                    {
                        string[] filename = v.FileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = v.ReturnNo + "_" + v.ReturnSeq + "_" + filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_Inspection_IN_File + "/" + realFileName;

                            FileHandler.UploadFTP(v.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_Inspection_IN_File + "/");

                            v.FileName = realFileName;
                            v.FileUrl = ftpFileUrl;
                        }
                    }
                    //foreach (var d in masterList.Where(p => !p.DeleteFilePath.IsNullOrEmpty()))
                    //{
                    //    try
                    //    {
                    //        FileHandler.PathDeleteFTP(GlobalVariable.FTP_SERVER, d.DeleteFilePath);
                    //    }
                    //    catch { }
                    //}

                    //foreach (var d in masterList.Where(p => !p.UploadFilePath.IsNullOrEmpty()))
                    //{
                    //    try
                    //    {
                    //        FileHandler.UploadFTP(d.UploadFilePath, string.Format("{0}{1}/{2}/", GlobalVariable.FTP_SERVER, MasterCodeSTR.FtpFolder_Inspection_IN_File, d.InspNo));
                    //        d.FileUrl = string.Format("{0}/{1}/{2}", MasterCodeSTR.FtpFolder_Inspection_IN_File, d.InspNo, d.FileName);
                    //    }
                    //    catch { }
                    //}
                }
            }

            ModelService.Save();
            DataLoad();
        }

   

       
      
     
        
      

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            //var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            //if (masterObj == null) return;

            //if (masterObj.TN_PUR1201List.Any(p => p.InConfirmFlag == "Y"))
            //{
            //    e.Cancel = true;
            //}
            //else
            //{
            //    bool check = false;
            //    for (int i = 0; i < masterObj.TN_PUR1201List.Count; i++)
            //    {
            //        var inInspectionState = DetailGridExControl.MainGrid.MainView.GetFocusedRowCellDisplayText("InInspectionState").GetNullToEmpty();
            //        if (inInspectionState != "대기")
            //        {
            //            check = true;
            //        }
            //    }

            //    e.Cancel = check;
            //}
        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            //var view = sender as GridView;
            //string fieldName = view.FocusedColumn.FieldName;

            //var masterObj = MasterGridBindingSource.Current as TN_PUR1200;
            //if (masterObj == null) return;
            //var detailObj = DetailGridBindingSource.Current as TN_PUR1201;
            //if (detailObj == null) return;

            ////if (fieldName != "_Check")
            ////{
            ////    if (detailObj.InConfirmFlag == "Y")
            ////    {
            ////        e.Cancel = true;
            ////    }
            ////    else
            ////    {
            ////        if (view.GetFocusedRowCellDisplayText("InInspectionState").GetNullToEmpty() != "대기")
            ////        {
            ////            e.Cancel = true;
            ////        }
            ////    }
            ////}
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.Column.FieldName == "InInspectionState")
            //{
            //    var InLotNo = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "Temp").GetNullToEmpty();
            //    var TN_QCT1100 = ModelService.GetChildList<TN_QCT1100>(p => p.CheckDivision == MasterCodeSTR.InspectionDivision_IN && p.InLotNo == InLotNo).FirstOrDefault();
            //    if (TN_QCT1100 != null)
            //    {
            //        e.DisplayText = TN_QCT1100.CheckResult;
            //    }
            //    else
            //    {
            //        e.DisplayText = "대기";
            //    }
            //}
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_PUR1305;
            if (detailObj == null) return;

            if (e.Column.FieldName == "ReturnQty")
            {
                if(detailObj.ReturnPossiQty < detailObj.ReturnQty)
                {
                    MessageBox.Show("반품가능수량을 초과할 수 없습니다.");
                    detailObj.ReturnQty = 0;
                    return;
                }
                
            }

                //if (e.Column.FieldName == "InWhCode")
                //{
                //    detailObj.InWhPosition = null;
                //}
                //else if (e.Column.FieldName == "InCustomerLotNo")
                //{
                //    if (!detailObj.Temp.IsNullOrEmpty())
                //    {
                //        var detailList = DetailGridBindingSource.List as List<TN_PUR1201>;
                //        var temp = DetailGridExControl.MainGrid.MainView.GetRowCellValue(e.RowHandle, "Temp").GetNullToEmpty();
                //        var sameWorkNoList = detailList.Where(p => p.Temp == temp).ToList();
                //        if (sameWorkNoList.Count > 1)
                //        {
                //            foreach (var v in sameWorkNoList)
                //                v.InCustomerLotNo = e.Value.GetNullToNull();
                //            DetailGridExControl.BestFitColumns();
                //        }
                //    }
                //}
                //else if (e.Column.FieldName == "InCost")
                //{
                //    var detailList = DetailGridBindingSource.List as List<TN_PUR1201>;
                //    var sameWorkNoList = detailList.Where(p => p.Temp == detailObj.Temp).ToList();
                //    if (sameWorkNoList.Count > 1)
                //    {
                //        foreach (var v in sameWorkNoList)
                //            v.InCost = e.Value.GetDecimalNullToNull();
                //        DetailGridExControl.BestFitColumns();
                //    }
                //}
        }

        private void WhPositionEdit_Popup(object sender, EventArgs e)
        {
            //var detailObj = DetailGridBindingSource.Current as TN_PUR1305;
            //var lookup = sender as SearchLookUpEdit;
            //if (lookup == null) return;
            //if (detailObj == null) return;

            //lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.InWhCode + "'";
        }
    }
}