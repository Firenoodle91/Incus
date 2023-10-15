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

namespace HKInc.Ui.View.MEA
{
    public partial class XFMEA1400 : HKInc.Service.Base.ListFormTemplate
    {
        /// <summary>
        /// 금형관리라고 추측
        /// </summary>
        IService<TN_MOLD001> ModelService = (IService<TN_MOLD001>)ProductionFactory.GetDomainService("TN_MOLD001");
        public XFMEA1400()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            checkEdit1.EditValue = "Y";
        }

        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            try
            {

                if (e.Clicks == 1)
                {
                    if (e.Column.Name.ToString() == "Imgurl")
                    {

                        String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns["Imgurl"]).ToString();


                        //      byte[] img = FileHandler.FtpImageToByte(filename);
                        byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + filename);
                      
                     


                        string[] lfileName = filename.Split('/');
                        if (lfileName.Length > 1)
                        {
                            File.WriteAllBytes(lfileName[lfileName.Length - 1], img);
                            HKInc.Service.Handler.FileHandler.StartProcess(lfileName[lfileName.Length - 1]);
                        }
                        else
                        {
                            File.WriteAllBytes(filename, img);
                            HKInc.Service.Handler.FileHandler.StartProcess(filename);
                        }
                    }

                }
            }
            catch (Exception ex){ }
        }
        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("MoldMcode", "관리번호");
            GridExControl.MainGrid.AddColumn("MoldCode", "금형코드");
            GridExControl.MainGrid.AddColumn("MoldName", "금형명");
            GridExControl.MainGrid.AddColumn("ItemCode", "제품코드");
            GridExControl.MainGrid.AddColumn("MoldMakecust", "제작처");
            GridExControl.MainGrid.AddColumn("InputDt", "이관일");
            GridExControl.MainGrid.AddColumn("MastMc", "메인설비");
            GridExControl.MainGrid.AddColumn("XCase", "캐비티");
            GridExControl.MainGrid.AddColumn("StPosition1", "위치1");
            GridExControl.MainGrid.AddColumn("StPosition2", "위치2");
            GridExControl.MainGrid.AddColumn("StPosition3", "위치3");
            GridExControl.MainGrid.AddColumn("StdShotcnt", "기준샷수", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Memo", "비고");
            GridExControl.MainGrid.AddColumn("CheckCycle", "점검주기");
            GridExControl.MainGrid.AddColumn("NextCheckDate", "다음점검일");
            GridExControl.MainGrid.AddColumn("MoldClass", "등급");
            GridExControl.MainGrid.AddColumn("RealShotcnt", "샷수", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("BaseShotcnt", "기본샷수", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("SumShotcnt", "누적샷수", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("CheckPoint", "점검기준", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Imgurl", "이미지");
            GridExControl.MainGrid.AddColumn("UseYN", "사용여부");
            //GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Imgurl");

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {

            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYN", "N");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("InputDt");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("NextCheckDate");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("MastMc", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("MoldMakecust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CheckCycle",  DbRequestHandler.GetCommCode(MasterCodeSTR.CHECKCYCLE).ToList(), "Mcode", "Codename");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.MainView.Columns["Imgurl"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(gridEx1, "Imgurl");
          
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("MoldMcode", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string useyn = checkEdit1.EditValue.ToString();
            if (useyn == "Y")
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.MoldCode.Contains(tx_MCnm.Text) || (p.MoldMcode == tx_MCnm.Text)))
                                                            .OrderBy(p => p.MoldMcode)
                                                          .ToList();
            }
            else
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.MoldCode.Contains(tx_MCnm.Text) || (p.MoldMcode == tx_MCnm.Text)) &&
                                                                          (p.UseYN == "Y"))
                                                          .OrderBy(p => p.MoldMcode)
                                                        .ToList();
            }
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }


        protected override void DeleteRow()
        {
            TN_MOLD001 obj = GridBindingSource.Current as TN_MOLD001;

            if (obj != null)
            {

                GridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYN", "N");
                obj.UseYN = "N";



                ModelService.Update(obj);


            }
        }
        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFMEA1400, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }
    }
}