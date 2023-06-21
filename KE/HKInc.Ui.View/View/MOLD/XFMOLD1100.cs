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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using HKInc.Service.Helper;
using HKInc.Service.Handler.EventHandler;
using HKInc.Ui.Model.Domain.VIEW;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.MOLD
{
    public partial class XFMOLD1100 : HKInc.Service.Base.ListFormTemplate        
    {
        IService<TN_MOLD1100> ModelService = (IService<TN_MOLD1100>)ProductionFactory.GetDomainService("TN_MOLD1100");
        public XFMOLD1100()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            checkEdit1.EditValue = "Y";
        }


        protected override void InitGrid()
        {
           
            IsGridButtonFileChooseEnabled = true;
            GridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "바코드출력[F10]", GlobalVariable.IconImageCollection.GetIconImage("print/printer"));
            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "MoldCode", true);
            GridExControl.MainGrid.AddColumn("_Check", "출력선택");

            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMCode"), LabelConvert.GetLabelText("MoldMCode"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCode"), LabelConvert.GetLabelText("MoldCode"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldName"), LabelConvert.GetLabelText("MoldName"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemCode"), LabelConvert.GetLabelText("ItemCode"), false);
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldDayInspFlag"), LabelConvert.GetLabelText("MoldDayInspFlag"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMakerCust"), LabelConvert.GetLabelText("MoldMakerCust"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("TransferDate"), LabelConvert.GetLabelText("TransferDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Cavity"), LabelConvert.GetLabelText("Cavity"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MainMachineCode"), LabelConvert.GetLabelText("MainMachineCode"));
            GridExControl.MainGrid.AddColumn("MoldWhCode", LabelConvert.GetLabelText("MoldWhCode"));
            GridExControl.MainGrid.AddColumn("MoldWhPosition", LabelConvert.GetLabelText("MoldWhPosition"));
            //GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StPostion1"), LabelConvert.GetLabelText("StPostion1"));
            //GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StPostion2"), LabelConvert.GetLabelText("StPostion2"));
            //GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StPostion3"), LabelConvert.GetLabelText("StPostion3"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StdShotcnt"), LabelConvert.GetLabelText("StdShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Memo"), LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckCycle"), LabelConvert.GetLabelText("CheckCycle"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("NextCheckDate"), LabelConvert.GetLabelText("NextMoldCheckDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldClass"), LabelConvert.GetLabelText("MoldClass"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("RealShotcnt"), LabelConvert.GetLabelText("RealShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("BaseShotcnt"), LabelConvert.GetLabelText("BaseShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("SumShotcnt"), LabelConvert.GetLabelText("SumShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckShotcnt"), LabelConvert.GetLabelText("CheckShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldFileName"), LabelConvert.GetLabelText("MoldFileName"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldFileUrl"), LabelConvert.GetLabelText("MoldFileUrl"),false);
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCheckFileName"), LabelConvert.GetLabelText("MoldCheckFileName"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCheckFileUrl"), LabelConvert.GetLabelText("MoldCheckFileUrl"), false);
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("UploadFilePath"), LabelConvert.GetLabelText("UploadFilePath"), false);
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DeleteFilePath"), LabelConvert.GetLabelText("DeleteFilePath"), false);
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DisuseDate"), LabelConvert.GetLabelText("DisuseDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("UseYN"), LabelConvert.GetLabelText("UseFlag"), HorzAlignment.Center, true);


            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("MoldDayInspFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemCodeLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p =>  p.UseFlag == "Y").ToList()); // 2021-07-15 김진우 주임 SetRepositoryItemSearchLookUpEdit 에서 SetRepositoryItemCodeLookUpEdit 로 변경


            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldMakerCust", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldMakercust), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", DataConvert.GetCultureDataFieldName("PositionName"), true);

            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StPostion1", DbRequestHandler.GetCommCode(MasterCodeSTR.MoldPosition, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StPostion2", DbRequestHandler.GetCommCode(MasterCodeSTR.MoldPosition, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StPostion3", DbRequestHandler.GetCommCode(MasterCodeSTR.MoldPosition, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", false);
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckCycle), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldClass", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldClass), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.MainView.Columns["MoldFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_MoldImage, "MoldFileName", "MoldFileUrl", true);
            GridExControl.MainGrid.MainView.Columns["MoldCheckFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, GridExControl, MasterCodeSTR.FtpFolder_MoldCheckImage, "MoldCheckFileName", "MoldCheckFileUrl", true);



        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("MoldMCode", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            string useyn = checkEdit1.EditValue.ToString();

            var MoldM = tx_MoldM.Text;

            if (useyn == "Y")
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(MoldM) ? true : (p.MoldMCode.Contains(MoldM) || (p.MoldCode.Contains(MoldM) || p.MoldName.Contains(MoldM)))))
                                                            .OrderBy(p => p.MoldMCode)
                                                          .ToList();
            }
            else
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(MoldM) ? true : (p.MoldMCode.Contains(MoldM) || (p.MoldCode.Contains(MoldM) || p.MoldName.Contains(MoldM))))
                                                                          && (p.UseYN == "Y"))
                                                          .OrderBy(p => p.MoldMCode)
                                                        .ToList();
            }
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFMOLD1100, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }


        protected override void DeleteRow()
        {
            TN_MOLD1100 obj = GridBindingSource.Current as TN_MOLD1100;

            if (obj != null)
            {
                obj.UseYN = "N";
                GridExControl.MainGrid.PostEditor();

                ModelService.Update(obj);


            }
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 1)
            {
                TN_MOLD1100 obj = GridBindingSource.Current as TN_MOLD1100;

                //if (e.Column.Name.ToString() == "ProdFileName")
                //{
                //    string fileName = obj.FileName.GetNullToEmpty();
                //    FileHandler.SaveFile(new FileHolder
                //    {
                //        FileName = fileName,
                //        FileData = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + MasterCodeSTR.FtpFolder_MoldImage + "/" + fileName)
                //    });
                //}
                //return;
            }

        }




        /// <summary>        
        /// </summary>
        protected override void FileChooseClicked()
        {
            if (GridBindingSource == null) return;
            var allList = GridBindingSource.List as List<TN_MOLD1100>;
            var checkList = allList.Where(p => p._Check == "Y").OrderBy(p => p.MoldCode).ToList();
            var obj = GridBindingSource.Current as TN_MOLD1100;
            if (obj == null) return;

            if (checkList.Count > 0)
            {
                try
                {
                    WaitHandler.ShowWait();

                    //var FirstReport = new REPORT.RETCLABEL_PIT();
                    //FirstReport.LABEL = REPORT.RETCLABEL_PIT.eLabelType.Mold;

                    // 20210825 오세완 차장 10 * 6 크기로 변경
                    var FirstReport = new REPORT.RETCLABEL_PIT_V2();
                    FirstReport.LABEL = REPORT.RETCLABEL_PIT_V2.eLabelType.Mold;
                    foreach (var v in checkList)
                    {
                        //var report = new REPORT.RETCLABEL_PIT(v);
                        var report = new REPORT.RETCLABEL_PIT_V2(v);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);
                    }
                    FirstReport.PrintingSystem.ShowMarginsWarning = false;
                    FirstReport.ShowPrintStatusDialog = false;
                    FirstReport.ShowPreview();
                }
                catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
                finally { WaitHandler.CloseWait(); }
            }
            else
            {
                //var FirstReport = new REPORT.RETCLABEL_PIT(obj);
                //FirstReport.LABEL = REPORT.RETCLABEL_PIT.eLabelType.Mold;

                // 20210825 오세완 차장 10 * 6 크기로 변경
                var FirstReport = new REPORT.RETCLABEL_PIT_V2(obj);
                FirstReport.LABEL = REPORT.RETCLABEL_PIT_V2.eLabelType.Mold;
                FirstReport.CreateDocument();
                FirstReport.PrintingSystem.ShowMarginsWarning = false;
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();
            }
        }
    }
}