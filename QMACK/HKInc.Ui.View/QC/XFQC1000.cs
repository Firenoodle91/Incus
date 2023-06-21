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
namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// 검사기준관리
    /// </summary>
    public partial class XFQC1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        public XFQC1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            MasterGridExControl.MainGrid.AddColumn("BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("ItemNm", "품명");            
            MasterGridExControl.MainGrid.AddColumn("ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("Spec1", "규격1");
            MasterGridExControl.MainGrid.AddColumn("Spec2", "규격2");
            MasterGridExControl.MainGrid.AddColumn("Spec3", "규격3");
            MasterGridExControl.MainGrid.AddColumn("Spec4", "규격4");            
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "복사하기");
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.MainGrid.AddColumn("Seq", false);
            DetailGridExControl.MainGrid.AddColumn("Temp2", "검사순서");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정명");
            DetailGridExControl.MainGrid.AddColumn("CheckName", "검사항목");
            DetailGridExControl.MainGrid.AddColumn("ProcessGu", "검사구분");
            DetailGridExControl.MainGrid.AddColumn("CheckProv", "검사방법");
            DetailGridExControl.MainGrid.AddColumn("Temp1", "계측기종류");
            DetailGridExControl.MainGrid.AddColumn("CheckStand", "기준",true,HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("UpQuad", "상한공차", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("DownQuad", "하한공차", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("UseYn", "사용여부", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ProcessGu", "CheckName", "CheckProv", "CheckStand", "UpQuad", "DownQuad","UseYn","Memo", "Temp2", "Temp1");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UseYn", DbRequestHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessGu", DbRequestHandler.GetCommCode(MasterCodeSTR.QCKIND), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckProv", DbRequestHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequestHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp1", DbRequestHandler.GetCommCode(MasterCodeSTR.VCTYPE), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" ).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void InitCombo()
        {
            lup_itemtype.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
            // 20220219 오세완 차장 대분류를 선택 안하면 어차피 품목이 아예 출력이 안되서 선택을 못하게 처리 
            //lup_item.Enabled = false;
            string itemtype = lup_itemtype.EditValue.GetNullToEmpty();
            lup_item.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetList(p => (string.IsNullOrEmpty(itemtype) ? true : p.TopCategory == itemtype) && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
            lup_item.Enabled = true;
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            ModelService.ReLoad();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            string cta = lup_itemtype.EditValue.GetNullToEmpty();
            string item = lup_item.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.ItemNm.Contains(item) || (p.ItemCode == item)) && (string.IsNullOrEmpty(cta) ? true : p.TopCategory == cta))
                                                         .OrderBy(p => p.ItemNm).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
            GridRowLocator.SetCurrentRow();
        }
        
        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = obj.QCT1000List.OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            TN_STD1100 MasterObj = MasterGridBindingSource.Current as TN_STD1100;
            if (MasterObj == null) return;

            // 2022-02-21 김진우 품목, 품명 표시되도록 수정
            TN_QCT1000 DetailObj = new TN_QCT1000();

            DetailObj.ItemCode = MasterObj.ItemCode;
            DetailObj.Seq = MasterObj.QCT1000List.Count == 0 ? 1 : MasterObj.QCT1000List.Max(p => p.Seq) + 1;   // 2022-04-20 김진우 수정
            //DetailObj.Seq = MasterObj.QCT1000List.Count == 0 ? 1 : MasterObj.QCT1000List.Count + 1;
            DetailObj.UseYn = "Y";
            DetailObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == MasterObj.ItemCode).FirstOrDefault();

            DetailGridBindingSource.Add(DetailObj);
            MasterObj.QCT1000List.Add(DetailObj);
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
           
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            //param.SetValue(PopupParameter.Constraint, "Final");           // 2022-02-23 김진우 미사용 주석
            param.SetValue(PopupParameter.IsMultiSelect, true);             // 2022-02-23 김진우 추가

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XSFQCSTDCOPY, param, AddPur1301);
            form.ShowPopup(true);
        }

        private void AddPur1301(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<TN_QCT1000> QCTList = (List<TN_QCT1000>)e.Map.GetValue(PopupParameter.ReturnObject);       // 2022-02-23 김진우 추가
            //List<TP_QC1000> partList = (List<TP_QC1000>)e.Map.GetValue(PopupParameter.ReturnObject);
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            foreach (var returnedPart in QCTList)
            {
                // 2022-02-23 김진우 수정
                TN_QCT1000 qc = new TN_QCT1000();

                qc.ItemCode = obj.ItemCode;
                qc.Seq = obj.QCT1000List.Count == 0 ? 1 : obj.QCT1000List.Count + 1;
                qc.UseYn = "Y";
                qc.ProcessCode = returnedPart.ProcessCode;
                qc.ProcessGu = returnedPart.ProcessGu;
                qc.CheckName = returnedPart.CheckName;
                qc.CheckProv = returnedPart.CheckProv;
                qc.CheckStand = returnedPart.CheckStand;
                qc.UpQuad = returnedPart.UpQuad;
                qc.DownQuad = returnedPart.DownQuad;
                qc.Temp1 = returnedPart.Temp1;           // 2022-02-23 김진우 계측기 종류 추가
                qc.Temp2 = returnedPart.Temp2;
                qc.TN_STD1100 = obj;                    // 2022-02-23 김진우 품목, 품번 추가

                DetailGridBindingSource.Add(qc);
                obj.QCT1000List.Add(qc);
            }
            if (QCTList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();      // 2022-07-06 김진우 추가
            MasterGridBindingSource.EndEdit();              // 2022-07-06 김진우 추가

            ModelService.Save();
            DataLoad();
        }

        #region 미사용 주석
        //2022-07-06 김진우
        //private void lup_itemtype_EditValueChanged(object sender, EventArgs e)
        //{
        //    string itemtype = lup_itemtype.EditValue.GetNullToEmpty();
        //    if (itemtype == "")
        //    {
        //        //lup_item.EditValue = "";
        //        //lup_item.DataSource = null;
        //        //lup_item.Enabled = false;
        //        lup_item.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetList(p => (string.IsNullOrEmpty(itemtype) ? true : p.TopCategory == itemtype) && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
        //        lup_item.Enabled = true;
        //    }
        //    else
        //    {
        //        // 20220219 오세완 차장 품목 / 품명이 동일하게 나오는 오류가 있어서 생략             2022-02-23 김진우 ItemCodeColumnSetting 수정하여 원복
        //        lup_item.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetList(p => (string.IsNullOrEmpty(itemtype) ? true : p.TopCategory == itemtype) && p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList());
        //        lup_item.Enabled = true;
        //    }
        //}
        #endregion

    }
}
