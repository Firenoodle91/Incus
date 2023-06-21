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

namespace HKInc.Ui.View.MEA
{
    /// <summary>
    /// 계측기 등록 관리
    /// </summary>
    public partial class XFMEA1200 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MEA1200> ModelService = (IService<TN_MEA1200>)ProductionFactory.GetDomainService("TN_MEA1200");

        public XFMEA1200()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            //checkEdit1.EditValue = "Y";
            checkEdit1.Checked = true;          // 2022-04-14 김진우 추가
        }
         
        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("InstrNo", "계측기관리번호");
            GridExControl.MainGrid.AddColumn("InstrNm", "계측기명");
            GridExControl.MainGrid.AddColumn("Maker", "제작사");
            GridExControl.MainGrid.AddColumn("Spec", "규격");
            GridExControl.MainGrid.AddColumn("PurcDate", "도입일");
            GridExControl.MainGrid.AddColumn("SerialNo", "일련번호");
            GridExControl.MainGrid.AddColumn("CorTurn", "교정주기");
            GridExControl.MainGrid.AddColumn("CorDate", "교정일");
            GridExControl.MainGrid.AddColumn("NxcorDate", "교정예정일");
            GridExControl.MainGrid.AddColumn("FileName", "사진");
            GridExControl.MainGrid.AddColumn("FileData", false);
            GridExControl.MainGrid.AddColumn("UseYn", "사용여부"); // 10
            GridExControl.MainGrid.AddColumn("Memo", "비고");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYn", "N");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("PurcDate");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("Maker", DbRequestHandler.GetCommCode(MasterCodeSTR.TOOLMAKER), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CorTurn", MasterCode.GetMasterCode((int)MasterCodeEnum.CheckTurn).ToList());
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InstrNo");

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            // 2022-04-14 김진우 추가
            GridBindingSource.DataSource = ModelService.GetList(p => (p.InstrNo.Contains(tx_MCnm.Text) || p.InstrNm.Contains(tx_MCnm.Text))
                                                                  && (checkEdit1.Checked == true ? true : p.UseYn == "Y"))
                                                                    .OrderBy(o => o.InstrNo)
                                                                    .ToList();
            #region 이전소스
            //string useyn = checkEdit1.EditValue.ToString();
            //if (useyn == "Y")
            //{
            //    GridBindingSource.DataSource = ModelService.GetList(p => (p.InstrNo.Contains(tx_MCnm.Text) || (p.InstrNo == tx_MCnm.Text)))
            //                                                .OrderBy(p => p.InstrNo)
            //                                              .ToList();
            //}
            //else
            //{
            //    GridBindingSource.DataSource = ModelService.GetList(p => (p.InstrNo.Contains(tx_MCnm.Text) || (p.InstrNo == tx_MCnm.Text)) &&
            //                                                              (p.UseYn == "Y"))
            //                                              .OrderBy(p => p.InstrNo)
            //                                            .ToList();
            #endregion
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

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
            TN_MEA1200 obj = GridBindingSource.Current as TN_MEA1200;
            if (obj != null)
            {
                GridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYn", "N");
                obj.UseYn = "N";

                ModelService.Update(obj);
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFMEA1200, param, PopupRefreshCallback);
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
                if (e.Clicks == 1)
                {
                    if (e.Column.Name.ToString() == "FileName")
                    {
                        String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns["FileName"]).ToString();
                        File.WriteAllBytes(filename, (byte[])gv.GetRowCellValue(e.RowHandle, gv.Columns["FileData"]));
                        HKInc.Service.Handler.FileHandler.StartProcess(filename);
                    }
                }
            }
            catch { }
        }
    }
}