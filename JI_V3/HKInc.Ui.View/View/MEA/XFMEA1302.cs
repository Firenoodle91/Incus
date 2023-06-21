using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Specialized;


namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 설비/금형 등급평가서 관리
    /// </summary>
    public partial class XFMEA1302 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MEA1305> ModelService = (IService<TN_MEA1305>)ProductionFactory.GetDomainService("TN_MEA1305");
        IService<VI_MACHINE_MOLD_LIST> SubModelService = (IService<VI_MACHINE_MOLD_LIST>)ProductionFactory.GetDomainService("VI_MACHINE_MOLD_LIST");

        public XFMEA1302()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dt_Year.DateTime = DateTime.Today;

            GridExControl.MainGrid.MainView.CellValueChanged += ListFormTemplate_EditValueChanged;

        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("ManageNo", LabelConvert.GetLabelText("ManageNo"),false);
            GridExControl.MainGrid.AddColumn("ManageType", LabelConvert.GetLabelText("Division"));
            GridExControl.MainGrid.AddColumn("SaveYear", LabelConvert.GetLabelText("Year"));
            //GridExControl.MainGrid.AddColumn("ManageCode", LabelConvert.GetLabelText("MachineMoldName"));
            //GridExControl.MainGrid.AddColumn("Name", LabelConvert.GetLabelText("MachineMoldName"));
            GridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("FileName"));
            GridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ManageType", "ManageNo", "SaveYear", "FileName", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MEA1305>(GridExControl);
        }

        protected override void InitCombo()
        {
            lup_Division.SetDefault(true, "CodeVal", "CodeName", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMoldCheck, 1));
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ManageType", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMoldCheck, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ManageCode", SubModelService.GetList(p => true), "ManageCode", Service.Helper.DataConvert.GetCultureDataFieldName("Name"));
            GridExControl.MainGrid.SetRepositoryItemDateEdit("SaveYear", DateFormat.Year);
            GridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, GridExControl, MasterCodeSTR.FtpFolder_MachineSpare_Mold_Doc_Image, "FileName", "FileUrl");
            
            GridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);

            //var CodeEdit = GridExControl.MainGrid.Columns["ManageCode"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            //CodeEdit.Popup += CodeEditEdit_Popup;
        }

        private void CodeEditEdit_Popup(object sender, EventArgs e)
        {
            try
            {
                var detailObj = GridBindingSource.Current as TN_MEA1305;
                var lookup = sender as SearchLookUpEdit;
                if (lookup == null) return;
                if (detailObj == null) return;

                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[ManageType] = '" + detailObj.ManageType + "'";
            }
            catch (Exception ex)
            {
                MessageBoxHandler.Show(ex.ToString());
            }
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            InitRepository();
            InitCombo();

            string Year = dt_Year.DateTime.Year.ToString();
            string division = lup_Division.EditValue.GetNullToEmpty();


            GridBindingSource.DataSource = ModelService.GetList(p => (p.SaveYear.ToString().Substring(0,4) == Year)
                                                                && (string.IsNullOrEmpty(division) == true ? true : p.ManageType == division))
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(GridExControl);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            List<TN_MEA1305> ObjList = GridBindingSource.DataSource as List<TN_MEA1305>;
            if (ObjList == null) return;

            foreach (var v in ObjList)
            {
                if (v.FileUrl != null && (v.FileUrl.Contains("\\") || v.FileUrl == "Clipboard_Image"))
                {
                    string[] filename = v.FileUrl.ToString().Split('\\');
                    if (filename.Length != 1)
                    {
                        var realFileName = v.ManageNo + "_" + filename[filename.Length - 1];
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineSpare_Mold_Doc_Image + "/" + realFileName;

                        //UploadFile1
                        FileHandler.UploadFTP(v.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineSpare_Mold_Doc_Image + "/");

                        v.FileName = realFileName;
                        v.FileUrl = ftpFileUrl;
                    }
                    else if (v.FileUrl == "Clipboard_Image")
                    {
                        var realFileName = v.ManageNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                        var ftpFileUrl = MasterCodeSTR.FtpFolder_MachineSpare_Mold_Doc_Image + "/" + realFileName;
                        var localImage = v.localImage as Image;
                        //FileHandler.UploadFTP(localImage, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineSpare_Mold_Doc_Image + "/");
                        FileHandler.UploadFile1(v.FileUrl, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MachineSpare_Mold_Doc_Image + "/");

                        v.FileName = realFileName;
                        v.FileUrl = ftpFileUrl;
                    }
                }
            }

            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            TN_MEA1305 NewObj = new TN_MEA1305();

            NewObj.ManageNo = DbRequestHandler.GetSeqMonth("GDOC");
            NewObj.SaveYear = DateTime.Today;

            ModelService.Insert(NewObj);
            GridBindingSource.Add(NewObj);
        }

        protected override void DeleteRow()
        {
            var obj = GridBindingSource.Current as TN_MEA1305;
            if (obj == null) return;

            if (obj.NewRowFlag != "Y")
                ModelService.Delete(obj);
            GridBindingSource.RemoveCurrent();
        }

        private void ListFormTemplate_EditValueChanged(object sender, CellValueChangedEventArgs e)
        {

            TN_MEA1305 Obj = GridBindingSource.Current as TN_MEA1305;

            Obj.EditRowFlag = "Y";
        }

        private void ColumnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            var masterObj = GridBindingSource.Current as TN_MEA1305;
            if (masterObj == null) return;

            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                StringCollection list = Clipboard.GetFileDropList();
                if (list != null && list.Count > 0 && ExtensionHelper.picExtensions.Contains(Path.GetExtension(list[0]).ToLower()))
                {
                    using (FileStream fs = new FileStream(list[0], FileMode.OpenOrCreate, FileAccess.Read))
                    {
                        byte[] fileData = new byte[fs.Length];
                        fs.Read(fileData, 0, System.Convert.ToInt32(fs.Length));
                        fs.Close();

                        masterObj.localImage = fileData;
                        masterObj.FileName = list[0];
                        masterObj.FileUrl = list[0];
                    }
                }
                else
                {
                    var GetImage = Clipboard.GetImage();
                    if (GetImage != null)
                    {
                        masterObj.FileName = "Clipboard_Image";
                        masterObj.FileUrl = "Clipboard_Image";
                        masterObj.localImage = GetImage;
                    }
                }
                GridExControl.BestFitColumns();
                masterObj.EditRowFlag = "Y";
            }
        }


    }
}