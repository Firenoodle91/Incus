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
using HKInc.Service.Controls;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain.VIEW;

namespace HKInc.Ui.View.View.QCT
{
    public partial class XFQC1200JI : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_QCT1200JI> ModelService = (IService<TN_QCT1200JI>)ProductionFactory.GetDomainService("TN_QCT1200JI");
        string qctype = "Q03";//초중종물검사

        public TN_QCT1200JI()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dp_procdate.DateFrEdit.DateTime = DateTime.Today.AddDays(-10);
            dp_procdate.DateToEdit.DateTime = DateTime.Today;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["ChaeckFlag"]);
                if (category == "NG")
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                    e.Appearance.ForeColor = Color.White;
                    //e.Appearance.BackColor2 = Color.SeaShell;

                }
            }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            TN_QCT1200JI obj = MasterGridBindingSource.Current as TN_QCT1200JI;
            if (e.Column.Name.ToString() == "Reading1")
            {
                GridView gv = sender as GridView;
                
                int Ncnt = 0;
                int Ocnt = 0;
                for (int i = 0; i < gv.RowCount; i++)
                {

                    switch (gv.GetRowCellValue(i, gv.Columns["ChaeckFlag"]).GetNullToEmpty())
                    {
                        case "OK": Ocnt++; break;
                        case "NG": Ncnt++; break;
                       
                    }


                }
                if (Ncnt > 0)
                {
                    obj.CheckResult = "NG";
                }
                else if (Ocnt > 0)
                { obj.CheckResult = "OK"; }
                MasterGridExControl.MainGrid.BestFitColumns();




            }
        }

        protected override void InitGrid()
        {

            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("No", "검사번호번호");
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("LotNo", "LOTNO");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");            
            MasterGridExControl.MainGrid.AddColumn("CheckDate", "검사일");
            MasterGridExControl.MainGrid.AddColumn("FmeNo", "검사구분");
            MasterGridExControl.MainGrid.AddColumn("FmeDivision", "검사시기");
            MasterGridExControl.MainGrid.AddColumn("CheckResult", "검사결과");
            MasterGridExControl.MainGrid.AddColumn("CheckId", "검사자");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckId", "Memo", "FmeDivision");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, "추가");
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.DeleteRow, "삭제");
            MasterGridExControl.MainGrid.AddColumn("No", "검사번호번호",false, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("Seq",false);
            DetailGridExControl.MainGrid.AddColumn("FmeDivision", "검사구분");
            DetailGridExControl.MainGrid.AddColumn("CheckName", "검사항목");            
            DetailGridExControl.MainGrid.AddColumn("CheckProv", "검사방법");
            DetailGridExControl.MainGrid.AddColumn("CheckStand", "기준",true,HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("UpQuad", "상한공차", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("DownQuad", "하한공차", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("Reading1", "측정치", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("ChaeckFlag", "판정", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("PoorType", "불량유형", false, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Reading1","Memo");

        }
        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").ToList(), "ItemCode", "fullName");
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("FmeDivision", DbRequesHandler.GetCommCode(MasterCodeSTR.QCSTEP), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("FmeNo", DbRequesHandler.GetCommCode(MasterCodeSTR.QCKIND), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("CheckResult", DbRequesHandler.GetCommCode(MasterCodeSTR.QCOKNG), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("CheckId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("FmeDivision", DbRequesHandler.GetCommCode(MasterCodeSTR.QCSTEP), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("CheckProv", DbRequesHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ChaeckFlag", DbRequesHandler.GetCommCode(MasterCodeSTR.QCOKNG), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("PoorType", DbRequesHandler.GetCommCode(MasterCodeSTR.QCFAIL), "Mcode", "Codename");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl.MainGrid.MainView, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            //  DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("UseYn", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");


        }
        protected override void InitCombo()
        {
            lup_item.SetDefault(true, "ItemCode", "fullName", ModelService.GetChildList<TN_STD1100>(p =>p.UseYn == "Y"&&p.TopCategory!="P03").ToList());

        }


        protected override void DataLoad()
        {
            ModelService.ReLoad();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();



            string item = lup_item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p =>(p.CheckDate>=dp_procdate.DateFrEdit.DateTime&&p.CheckDate<=dp_procdate.DateToEdit.DateTime)&&(string.IsNullOrEmpty(item)?true:p.ItemCode==item)&&p.FmeNo==qctype).ToList();


            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }
        protected override void MasterFocusedRowChanged()
        {
            TN_QCT1200JI obj = MasterGridBindingSource.Current as TN_QCT1200JI;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = obj.QCT1201JIList.OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void AddRowClicked()
        {

            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTMPS1401LIST, param, AddList);
            form.ShowPopup(true);

            //TN_QCT1200 obj = new TN_QCT1200();
            //MasterGridBindingSource.Add(obj);
            //ModelService.Insert(obj);
        }
        private void AddList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            VI_MPS1401LIST partList = (VI_MPS1401LIST)e.Map.GetValue(PopupParameter.ReturnObject);

            TN_QCT1200 obj = new TN_QCT1200() {
                No = DbRequesHandler.GetRequestNumber(qctype),
                FmeNo = qctype,
                WorkNo = partList.WorkNo,
                WorkDate = partList.WorkDate,
                CheckDate = DateTime.Today,
               // FmeDivision = DbRequesHandler.GetCellValue("exec SP_QCSTEP_STEP '" + qctype + "','" + partList.WorkDate + "','" + partList.WorkNo + "','" + partList.ProcessCode + "','" + DateTime.Today + "'", 0),
                ItemCode= partList.ItemCode,
                LotNo=partList.LotNo,
                ProcessCode=partList.ProcessCode,

            };
            MasterGridBindingSource.Add(obj);
            ModelService.Insert(obj);
        }
        protected override void DeleteRow()
        {
            TN_QCT1200 obj = MasterGridBindingSource.Current as TN_QCT1200;
            if (obj == null) return;
            if (obj.QCT1201List.Count > 0) {

                MessageBox.Show("품질데이터가 있어서 삭제할수 없습니다.");

            }
            else
            {
                MasterGridBindingSource.Remove(obj);
                ModelService.Delete(obj);
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_QCT1200 obj = MasterGridBindingSource.Current as TN_QCT1200;

            List<TN_QCT1000> qc = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == obj.ItemCode && p.ProcessGu == qctype && p.ProcessCode == obj.ProcessCode && p.UseYn == "Y").ToList();

            for (int i = 0; i < qc.Count; i++)
            {
                TN_QCT1201 newobj = new TN_QCT1201()
                {
                    No = obj.No,
                    FmeNo=obj.FmeNo,
                    FmeDivision=obj.FmeDivision,
                    ItemCode = obj.ItemCode,
                    Seq = obj.QCT1201List.Count == 0 ? 1 : obj.QCT1201List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                    CheckProv=qc[i].CheckProv,
                    CheckName=qc[i].CheckName,
                    CheckStand=qc[i].CheckStand,
                    UpQuad=qc[i].UpQuad,
                    DownQuad=qc[i].DownQuad
                };
            DetailGridBindingSource.Add(newobj);
            obj.QCT1201List.Add(newobj);
        }
            DetailGridExControl.MainGrid.BestFitColumns();

        }
        protected override void DeleteDetailRow()
        {
            TN_QCT1200 obj = MasterGridBindingSource.Current as TN_QCT1200;
           
            if (obj == null) return;
            TN_QCT1201 delobj = DetailGridBindingSource.Current as TN_QCT1201;
            obj.QCT1201List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);

            DetailGridExControl.MainGrid.BestFitColumns();


        }
        protected override void DataSave()
        {
            ModelService.Save();
            DataLoad();
        }
    }
}
