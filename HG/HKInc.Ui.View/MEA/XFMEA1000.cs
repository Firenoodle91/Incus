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

namespace HKInc.Ui.View.MEA
{
    /// <summary>
    /// 설비등록관리화면
    /// </summary>
    public partial class XFMEA1000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        public XFMEA1000()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("MachineCode","설비코드");
            GridExControl.MainGrid.AddColumn("MachineName","설비명");
            GridExControl.MainGrid.AddColumn("ModelNo","모델");          
            GridExControl.MainGrid.AddColumn("Maker","제조사");
            GridExControl.MainGrid.AddColumn("InstallDate","설치일자");
            GridExControl.MainGrid.AddColumn("SerialNo","S/N");
            GridExControl.MainGrid.AddColumn("CheckTurn","점검주기");
            GridExControl.MainGrid.AddColumn("NextCheck", "점검예정일");
            GridExControl.MainGrid.AddColumn("DayQty", "일일생산량", HorzAlignment.Far, FormatType.Numeric, "n0");            
            //GridExControl.MainGrid.AddColumn("ContinueCheck", "연속설비");
            GridExControl.MainGrid.AddColumn("FileName","사진");
            GridExControl.MainGrid.AddColumn("FileData" ,false);
            GridExControl.MainGrid.AddColumn("UseYn","사용여부"); // 10
            GridExControl.MainGrid.AddColumn("Memo","비고");
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("ContinueCheck", "N");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("InstallDate");            
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", DbRequesHandler.GetCommCode(MasterCodeSTR.MCMAKER), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckTurn", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("MachineCode", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            if (chk_UseYN.Checked == true)
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.MachineName.Contains(tx_MCnm.Text) || (p.MachineCode == tx_MCnm.Text)))
                                                            .OrderBy(p => p.MachineName)
                                                            .ToList();
            }
            else
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.MachineName.Contains(tx_MCnm.Text) || (p.MachineCode == tx_MCnm.Text)) &&
                                                                          (p.UseYn == "Y"))
                                                            .OrderBy(p => p.MachineName)
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
            TN_MEA1000 obj = GridBindingSource.Current as TN_MEA1000;
            //if (obj != null)
            //{
            //    GridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYn", "N");
            //    obj.UseYn = "N";
            //    ModelService.Update(obj);
            //}
            if (obj != null)
            {
                DialogResult result = Service.Handler.MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage(29), "설비정보"), HelperFactory.GetLabelConvert().GetLabelText("Confirm"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.No)
                {
                    GridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYn", "N");
                    //obj.UseYn = "N";
                    //ModelService.Update(obj);
                    return;
                }

                ModelService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFMEA1000, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }

        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            try
            {
                if (e.Column.Name.ToString() == "FileName")
                {

                    String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns["FileName"]).ToString();
                    File.WriteAllBytes(filename, (byte[])gv.GetRowCellValue(e.RowHandle, gv.Columns["FileData"]));
                    HKInc.Service.Handler.FileHandler.StartProcess(filename);
                }
            }
            catch { }
        }
    }
}