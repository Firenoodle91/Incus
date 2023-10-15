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
    /// <summary>
    /// 20220323 오세완 차장
    /// 고도화로 인하여 변경된 섬비관리
    /// </summary>
    public partial class XFMEA1000_V2 : HKInc.Service.Base.ListFormTemplate
    {
        #region 전역변수
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        #endregion

        public XFMEA1000_V2()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
        }

        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            try
            {
                if (e.Column.Name.ToString() == "FileName")
                {
                    string sFilename = gv.GetRowCellValue(e.RowHandle, gv.Columns["FileUrl"]).ToString();
                    if(sFilename.GetNullToEmpty() != "")
                    {
                        byte[] bArr = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + sFilename);
                        if (bArr != null)
                            if (bArr.Count() > 0)
                            {
                                POP_Popup.XPFPOPIMG_V2 form = new POP_Popup.XPFPOPIMG_V2("설비사진", bArr);
                                form.ShowDialog();
                            }
                    }
                }
                else if (e.Column.Name.ToString() == "CheckPointFileName")
                {
                    string sFilename_Check = gv.GetRowCellValue(e.RowHandle, gv.Columns["CheckPointFileNameUrl"]).ToString();
                    if(sFilename_Check.GetNullToEmpty() != "")
                    {
                        byte[] bArr_Check = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + sFilename_Check);
                        if (bArr_Check != null)
                            if (bArr_Check.Count() > 0)
                            {
                                POP_Popup.XPFPOPIMG_V2 form = new POP_Popup.XPFPOPIMG_V2("점검포인트사진", bArr_Check);
                                form.ShowDialog();
                            }
                    }
                }
                else if (e.Column.Name.ToString() == "MainternanceFileName")
                {
                    string sFilename_Check = gv.GetRowCellValue(e.RowHandle, gv.Columns["MainternanceFileNameUrl"]).ToString();
                    if(sFilename_Check.GetNullToEmpty() != "")
                    {
                        byte[] bArr_Check = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + sFilename_Check);
                        if (bArr_Check != null)
                            if (bArr_Check.Count() > 0)
                            {
                                POP_Popup.XPFPOPIMG_V2 form = new POP_Popup.XPFPOPIMG_V2("예방보전점검사진", bArr_Check);
                                form.ShowDialog();
                            }
                    }
                }
            }
            catch { }
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("MachineCode","설비고유코드");
            GridExControl.MainGrid.AddColumn("MachineCode2", "설비코드");
            GridExControl.MainGrid.AddColumn("MachineGroupCode", "설비그룹");
            GridExControl.MainGrid.AddColumn("MachineName","설비명");
            GridExControl.MainGrid.AddColumn("ModelNo","모델");          
            GridExControl.MainGrid.AddColumn("Maker","제작사");
            GridExControl.MainGrid.AddColumn("InstallDate","설치일");
            GridExControl.MainGrid.AddColumn("SerialNo","S/N");
            GridExControl.MainGrid.AddColumn("CheckTurn","예방보전주기");
            GridExControl.MainGrid.AddColumn("NextCheck", "예방보전예정일");
            GridExControl.MainGrid.AddColumn("MonitorLocation", "모니터링위치");      // 모니터링 위치 추가           2022-07-15 김진우
            GridExControl.MainGrid.AddColumn("Class", "등급");
            GridExControl.MainGrid.AddColumn("ClassDate", "등급평가일", HorzAlignment.Near, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("Grade", "점수", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FileName","설비사진");
            GridExControl.MainGrid.AddColumn("FileUrl" ,false);
            GridExControl.MainGrid.AddColumn("CheckPointFileName", "점검포인트사진");
            GridExControl.MainGrid.AddColumn("CheckPointFileNameUrl", false);
            GridExControl.MainGrid.AddColumn("MainternanceFileName", "예방보전점검사진");
            GridExControl.MainGrid.AddColumn("MainternanceFileNameUrl", false);
            GridExControl.MainGrid.AddColumn("DailyCheckFlag", "일상점검유무"); 
            GridExControl.MainGrid.AddColumn("UseYn", "사용여부"); 
            GridExControl.MainGrid.AddColumn("Memo","비고");
           
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("DailyCheckFlag", "N");           // 2021-11-17 김진우 주임 추가
            GridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");          
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.MCMAKER), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CheckTurn", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("MonitorLocation", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineMonotoringLocation), "Mcode", "Codename");      // 모니터링 위치 추가       2022-07-15 김진우
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("MachineGroupCode", DbRequestHandler.GetCommCode(MasterCodeSTR.MachineGroup), "Mcode", "Codename");
            GridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, gridEx1,
                MasterCodeSTR.FtpFolder_MachineImage, "FileName", "FileUrl");
            GridExControl.MainGrid.MainView.Columns["CheckPointFileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, gridEx1,
                MasterCodeSTR.FtpFolder_MachineCheckPoint, "CheckPointFileName", "CheckPointFileNameUrl");
            GridExControl.MainGrid.MainView.Columns["MainternanceFileName"].ColumnEdit = new HKInc.Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, gridEx1,
                MasterCodeSTR.FtpFolder_MachineMaintenance, "MainternanceFileName", "MainternanceFileNameUrl");
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            if (chk_UseYN.Checked == true)
                GridBindingSource.DataSource = ModelService.GetList(p => (p.MachineName.Contains(tx_MCnm.Text) || (p.MachineCode == tx_MCnm.Text)))
                                                                        .OrderBy(p => p.MachineName)
                                                                        .ToList();
            else
                GridBindingSource.DataSource = ModelService.GetList(p => (p.MachineName.Contains(tx_MCnm.Text) || (p.MachineCode == tx_MCnm.Text)) &&
                                                                         (p.UseYn == "Y"))
                                                                        .OrderBy(p => p.MachineName)
                                                                        .ToList();

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
            TN_MEA1000 obj = GridBindingSource.Current as TN_MEA1000;
            if (obj != null)
            {
                GridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYn", "N");
                obj.UseYn = "N";
                ModelService.Update(obj);
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFMEA1000_V2, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }
    }
}