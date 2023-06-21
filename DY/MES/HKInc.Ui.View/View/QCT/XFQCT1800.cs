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
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Handler.EventHandler;

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// 교육계획 자료 관리
    /// </summary>
    public partial class XFQCT1800 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1800> ModelService = (IService<TN_QCT1800>)ProductionFactory.GetDomainService("TN_QCT1800");

        public XFQCT1800()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            dt_EduDate.SetTodayIsMonth();

            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

        }

       
        protected override void InitCombo()
        {
            
        }

        protected override void InitGrid()
        {
            
            GridExControl.MainGrid.AddColumn("EduDate", LabelConvert.GetLabelText("EduDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("EduDocName", LabelConvert.GetLabelText("EduDocName"));            
            GridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("AttachFile"));
            GridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            GridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            GridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "EduDate", "EduDocName", "Memo");

        }

        protected override void InitRepository()
        {

            GridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, GridExControl, MasterCodeSTR.FtpFolder_Inspection_IN_File, "FileName", "FileUrl");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("Seq", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);

            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            //데이터리로드
            InitRepository();
            InitCombo();

            var EduDocName = tx_EduDocName.EditValue.GetNullToEmpty();
            

            GridBindingSource.DataSource = ModelService.GetList(p => (p.EduDate >= dt_EduDate.DateFrEdit.DateTime && p.EduDate <= dt_EduDate.DateToEdit.DateTime)
                                                                  && (string.IsNullOrEmpty(EduDocName) ? true : (p.EduDocName.Contains(EduDocName)))
                                                               )
                                                               .OrderBy(p => p.EduDate)
                                                               .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }

        protected override void AddRowClicked()
        {
            TN_QCT1800 newobj = new TN_QCT1800();
            {
                newobj.EduDate = DateTime.Today;
                newobj.EditRowFlag = "Y";
            };

            GridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            GridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var delobj = GridBindingSource.Current as TN_QCT1800;

            if (delobj != null)
            {
                GridBindingSource.Remove(delobj);
                ModelService.Delete(delobj);
                GridExControl.BestFitColumns();
            }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var obj = GridBindingSource.Current as TN_QCT1800;
            obj.EditRowFlag = "Y";
        }


        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();



            #region 파일 CHECK
            if (GridBindingSource  != null && GridBindingSource.DataSource != null)
            {
                var List = GridBindingSource.List as List<TN_QCT1800>;
                var editList = List.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count > 0)
                {
                    foreach (var v in editList.Where(c => c.FileUrl != null && c.FileUrl.Contains("\\")).ToList())
                    {
                        string[] filename = v.FileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = v.EduDocName + "_" + filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_Inspection_IN_File + "/" + realFileName;

                            FileHandler.UploadFTP(v.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_Inspection_IN_File + "/");

                            v.FileName = realFileName;
                            v.FileUrl = ftpFileUrl;
                        }
                    }
                  
                }
            }
            
            #endregion

            ModelService.Save();
            DataLoad();
        }

       

        
    }
}