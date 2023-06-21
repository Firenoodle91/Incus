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

namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// 검사규격관리화면
    /// </summary>
    public partial class XFQC1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XFQC1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            lup_itemtype.EditValueChanged += lup_itemtype_EditValueChanged;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);           
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "소분류");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");
            //MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
            //MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            //MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            //MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "원재료)");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "색상)");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "재료규격");
            MasterGridExControl.MainGrid.AddColumn("Temp2", "실리콘농도");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "제품규격");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "복사하기[Alt+R]");
            DetailGridExControl.MainGrid.AddColumn("Seq", false);
            DetailGridExControl.MainGrid.AddColumn("Temp2", "검사순서", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정명");
            DetailGridExControl.MainGrid.AddColumn("ProcessGu", "검사구분");
            DetailGridExControl.MainGrid.AddColumn("CheckName", "검사항목");
            DetailGridExControl.MainGrid.AddColumn("CheckProv", "검사방법");
            DetailGridExControl.MainGrid.AddColumn("Temp1", "계측기종류");
            DetailGridExControl.MainGrid.AddColumn("CheckStand", "기준",true,HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("UpQuad", "상한공차", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("DownQuad", "하한공차", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("UseYn", "사용여부", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ProcessGu", "CheckName", "CheckProv", "CheckStand", "UpQuad", "DownQuad","UseYn","Memo", "Temp2", "Temp1");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "ProcessCode", "ProcessGu", "CheckName", "CheckProv", "CheckStand");

        }

        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");


            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UseYn", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessGu", DbRequesHandler.GetCommCode(MasterCodeSTR.QCKIND), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckProv", DbRequesHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequesHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp1", DbRequesHandler.GetCommCode(MasterCodeSTR.VCTYPE), "Mcode", "Codename");            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" ).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void InitCombo()
        {
            lup_itemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));          
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string cta = lup_itemtype.EditValue.GetNullToEmpty();
            //string item = lup_item.EditValue.GetNullToEmpty();
            string Item = textItemCodeName.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(Item) || p.ItemNm1.Contains(Item)) 
                                                                        && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta)
                                                                     )
                                                                     .OrderBy(p => p.ItemNm1)
                                                                     .ToList();


            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.QCT1000List.OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            ModelService.Save();
            DataLoad();
        }

        protected override void DetailAddRowClicked()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null) return;

            TN_QCT1000 qc = new TN_QCT1000()
            {
                ItemCode = obj.ItemCode,
                Seq = obj.QCT1000List.Count == 0 ? 1 : obj.QCT1000List.Count + 1,
                UseYn = "Y"
            };
            DetailGridBindingSource.Add(qc);
            obj.QCT1000List.Add(qc);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            TN_QCT1000 obj = DetailGridBindingSource.Current as TN_QCT1000;
            if (obj == null) return;
            obj.UseYn = "N";
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailFileChooseClicked()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null) return;

            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTQCCOPY, param, AddPur1301);
            form.ShowPopup(true);
        }

        private void AddPur1301(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<TP_QC1000> partList = (List<TP_QC1000>)e.Map.GetValue(PopupParameter.ReturnObject);
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            foreach (var returnedPart in partList)
            {
                TN_QCT1000 qc = new TN_QCT1000()
                {
                    ItemCode = obj.ItemCode,
                    Seq = obj.QCT1000List.Count == 0 ? 1 : obj.QCT1000List.Count + 1,
                    UseYn = "Y",
                    ProcessCode = returnedPart.ProcessCode,
                    ProcessGu = returnedPart.ProcessGu,
                    CheckName = returnedPart.CheckName,
                    CheckProv = returnedPart.CheckProv,
                    CheckStand = returnedPart.CheckStand,
                    UpQuad = returnedPart.UpQuad,
                    DownQuad = returnedPart.DownQuad,
                    Temp2 = returnedPart.Temp2,
                    Temp1 = returnedPart.Temp1
                };

                DetailGridBindingSource.Add(qc);
                obj.QCT1000List.Add(qc);

            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void lup_itemtype_EditValueChanged(object sender, EventArgs e)
        {
            string itemtype = lup_itemtype.EditValue.GetNullToEmpty();
            lup_item.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetList(p => (string.IsNullOrEmpty(itemtype) ? true : p.TopCategory == itemtype) && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as GridView;
            if(e.Column.FieldName == "CheckProv")
            {
                TN_QCT1000 obj = DetailGridBindingSource.Current as TN_QCT1000;
                if (view.GetFocusedRowCellDisplayText("CheckProv").GetNullToEmpty() == "육안검사")
                    obj.CheckStand = "OK/NG";
            }
        }

    }
}
