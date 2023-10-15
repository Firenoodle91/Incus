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
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Specialized;
using System.IO;
using HKInc.Ui.Model.Domain;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 도면관리화면
    /// </summary>
    public partial class XFSTD1500 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

        public XFSTD1500()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitCombo()
        {
            lup_Process.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), ModelService.GetList(p => p.UseYN == "Y" && p.CodeMain == "P001" && p.CodeVal != null).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("CodeVal", LabelConvert.GetLabelText("ProcessCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CodeName"), LabelConvert.GetLabelText("ProcessName"));
            MasterGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            DetailGridExControl.SetToolbarButtonVisible(false);            
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessCode"), false);
            DetailGridExControl.MainGrid.AddColumn("Seq", LabelConvert.GetLabelText("Seq"), HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0", false);
            DetailGridExControl.MainGrid.AddColumn("DesignFileName", LabelConvert.GetLabelText("DesignFileName"));
            DetailGridExControl.MainGrid.AddColumn("DesignFileUrl", LabelConvert.GetLabelText("DesignFileUrl"), false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "DesignFileName");
            DetailGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"),HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss", true);
            DetailGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), true);
            DetailGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            DetailGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1500>(DetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");            
            DetailGridExControl.MainGrid.MainView.Columns["DesignFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_WorkStandardDocumentFile, "DesignFileName", "DesignFileUrl");
            DetailGridExControl.MainGrid.MainView.Columns["DesignFileName"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ProcessCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var processCode = lup_Process.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(processCode) ? true : p.CodeVal == processCode)
                                                                      && p.CodeMain == "P001"
                                                                      && p.CodeVal != null
                                                                     )
                                                                     .ToList();
                                                 
            MasterGridExControl.MainGrid.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1000;
            //var detailObj = DetailGridBindingSource.Current as TEMP_STD1500_DETAIL;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            var processCode = new SqlParameter("@ProcessCode", masterObj.CodeVal);
            var queryFlag = new SqlParameter("@QueryFlag", "S");

            var ds = DbRequestHandler.GetDataSet("USP_GET_XFSTD1500", queryFlag, processCode);
            
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                DetailGridExControl.MainGrid.Clear();
            
            List<TEMP_STD1500_DETAIL> list = new List<TEMP_STD1500_DETAIL>();
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                TEMP_STD1500_DETAIL newObj = new TEMP_STD1500_DETAIL();
                newObj.ProcessCode = row.ItemArray[0].ToString();                
                newObj.Seq = row.ItemArray[2].GetIntNullToZero();
                newObj.DesignFileName = row.ItemArray[3].GetNullToEmpty();
                newObj.RowId = row.ItemArray[9].GetIntNullToZero();
                newObj.RowFlag = row.ItemArray[10].ToString();
                newObj.CreateTime = Convert.ToDateTime(row.ItemArray[5].ToString());
                newObj.CreateId = row.ItemArray[6].ToString();
                newObj.UpdateTime = Convert.ToDateTime(row.ItemArray[7].ToString());
                newObj.UpdateId = row.ItemArray[8].ToString();
                list.Add(newObj);
            }            
            DetailGridBindingSource.DataSource = list.OrderBy(p => p.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            var masterObj = MasterGridBindingSource.Current as TN_STD1000;
            var detailList = DetailGridBindingSource.List as List<TEMP_STD1500_DETAIL>;
            var targetList = detailList.Where(p => p.ProcessCode == masterObj.CodeVal).ToList();

            #region 필수 CHECK
            try
            {
                if (targetList != null)
                {
                    foreach (var v in targetList.ToList())
                    {
                        TN_STD1500 date = ModelService.GetChildList<TN_STD1500>(p => p.Seq == v.Seq).FirstOrDefault();
                        string[] filename = v.DesignFileName.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            //var realFileName = masterObj.CodeVal + "_" + filename[filename.Length - 1];
                            var realFileName = filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/" + realFileName;

                            FileHandler.UploadFTP(v.DesignFileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/");

                            v.DesignFileName = realFileName;
                            v.DesignFileUrl = ftpFileUrl;
                        }
                        else
                        {
                            //var realFileName = masterObj.CodeVal + "_" + filename[filename.Length - 1];
                            var realFileName = filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_WorkStandardDocumentFile + "/" + realFileName;
                            v.DesignFileName = realFileName;
                            v.DesignFileUrl = ftpFileUrl;
                        }

                        if (v.RowFlag == "N")
                        {
                            date = new TN_STD1500();
                            date.ProcessCode = v.ProcessCode;
                            date.Seq = v.Seq;
                            date.DesignFileName = v.DesignFileName;
                            date.DesignFileUrl = v.DesignFileUrl;
                            date.CreateTime = System.DateTime.Now;
                            date.CreateId = GlobalVariable.LoginId;
                            ModelService.InsertChild<TN_STD1500>(date);
                        }
                        else if (v.RowFlag == "U" || v.RowFlag == "")
                        {
                            date.DesignFileName = v.DesignFileName;
                            date.DesignFileUrl = v.DesignFileUrl;
                            date.CreateTime = System.DateTime.Now;
                            date.CreateId = GlobalVariable.LoginId;
                            date.UpdateTime = System.DateTime.Now;
                            date.UpdateId = GlobalVariable.LoginId;
                            ModelService.UpdateChild<TN_STD1500>(date);
                        }
                    }
                }
                ModelService.Save();
                DataLoad();
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);                
            }
            #endregion
        }

        protected override void DetailAddRowClicked()
        {            
            var masterObj = MasterGridBindingSource.Current as TN_STD1000;
            var detailObj = DetailGridBindingSource.Current as TEMP_STD1500_DETAIL;
            var detailList = DetailGridBindingSource.List as List<TEMP_STD1500_DETAIL>;

            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            
            var newObj = new TEMP_STD1500_DETAIL();
            newObj.ProcessCode = masterObj.CodeVal;
            newObj.Seq = DetailGridBindingSource.List.Count == 0 ? 1 : detailList.Max(m => m.Seq) + 1;
            newObj.RowId = DetailGridBindingSource.List.Count == 0 ? 1 : detailList.Max(m => m.Seq) + 1;

            masterObj.NewRowFlag = "Y";
            newObj.RowFlag = "N";            
            DetailGridBindingSource.Add(newObj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {

            var masterObj = MasterGridBindingSource.Current as TN_STD1000;
            if (masterObj == null) return;

            var delobj = DetailGridBindingSource.Current as TEMP_STD1500_DETAIL;
            if (delobj == null) return;

            TN_STD1500 date = ModelService.GetChildList<TN_STD1500>(p => p.Seq == delobj.Seq).FirstOrDefault();

            if (date ==  null)
            {
                DetailGridBindingSource.Remove(delobj);
                return;
            }
            delobj.RowFlag = "D";
            ModelService.RemoveChild<TN_STD1500>(date);
            DetailGridBindingSource.Remove(delobj);

            ModelService.Save();
            DataLoad();
            DetailGridExControl.MainGrid.BestFitColumns();           
        }

        /// <summary>
        /// 디테일 셀 변경 시 마스터 Edit 체크를 위함.
        /// </summary>
        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (masterObj == null) return;

            masterObj.EditRowFlag = "Y";
        }

        private void ColumnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1000;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TEMP_STD1500_DETAIL;
            if (detailObj == null) return;

            StringCollection list = Clipboard.GetFileDropList();
            if (list != null && list.Count > 0 && ExtensionHelper.picExtensions.Contains(Path.GetExtension(list[0]).ToLower()))
            {
                using (FileStream fs = new FileStream(list[0], FileMode.OpenOrCreate, FileAccess.Read))
                {
                    byte[] fileData = new byte[fs.Length];
                    fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();

                    //detailObj.localImage = fileData;
                    detailObj.DesignFileName = list[0];
                    detailObj.DesignFileUrl = list[0];
                }
            }
            else
            {
                var GetImage = Clipboard.GetImage();
                if (GetImage != null)
                {
                    detailObj.DesignFileName = "Clipboard_Image";
                    detailObj.DesignFileUrl = "Clipboard_Image";
                    //detailObj.localImage = GetImage;
                }
            }
            DetailGridExControl.BestFitColumns();
            detailObj.RowFlag = "U";
            masterObj.EditRowFlag = "Y";            
        }
    }
}