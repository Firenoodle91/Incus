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
using HKInc.Ui.Model.Domain.VIEW;

namespace HKInc.Ui.View.View.QCT
{
    public partial class XFQC1000JI : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        public XFQC1000JI()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
           
            //MasterGridExControl.MainGrid.AddColumn("TopCategory", "대분류");
            //MasterGridExControl.MainGrid.AddColumn("MiddleCategory", "중분류");
            //MasterGridExControl.MainGrid.AddColumn("BottomCategory", "소분류");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("fullName", "품명");
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
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");
            DetailGridExControl.MainGrid.AddColumn("Temp2", "검사순서");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정명");
            DetailGridExControl.MainGrid.AddColumn("CheckName", "검사항목");
            DetailGridExControl.MainGrid.AddColumn("ProcessGu", "검사구분");
            DetailGridExControl.MainGrid.AddColumn("CheckProv", "검사방법");
            DetailGridExControl.MainGrid.AddColumn("Temp1", "계측기종류");
            DetailGridExControl.MainGrid.AddColumn("CheckStand", "기준",true,HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("UpQuad", "상한공차", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("DownQuad", "하한공차", true, HorzAlignment.Far);
            DetailGridExControl.MainGrid.AddColumn("SpcYn", "특별항목", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("UseYn", "사용여부", true, HorzAlignment.Center);
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ProcessCode", "ProcessGu", "CheckName", "CheckProv", "CheckStand", "UpQuad", "DownQuad","UseYn","Memo", "SpcYn", "Temp2", "Temp1");

        }
        protected override void InitRepository()
        {

            //MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("TopCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("MiddleCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, 3), "Mcode", "Codename");


            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("UseYn", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("SpcYn", DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ProcessGu", DbRequesHandler.GetCommCode(MasterCodeSTR.QCKIND), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("CheckProv", DbRequesHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("Temp1", DbRequesHandler.GetCommCode(MasterCodeSTR.VCTYPE), "Mcode", "Codename");
            //  DetailGridExControl.MainGrid.SetRepositoryItemMemoEdit("Memo");
            DetailGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" ).ToList(), "ItemCode", "fullName");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }
        protected override void InitCombo()
        {
            lup_itemtype.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.itemtype, "", "", ""));
          
        }

        private void lup_itemtype_EditValueChanged(object sender, EventArgs e)
        {
            string itemtype = lup_itemtype.EditValue.GetNullToEmpty();
         lup_item.SetDefault(true, "ItemCode", "fullName", ModelService.GetList(p => (string.IsNullOrEmpty(itemtype) ? true : p.TopCategory == itemtype) && p.UseYn == "Y").ToList());
        }

        protected override void DataLoad()
        {
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
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;

            TN_QCT1000 qc = new TN_QCT1000()
            {
                ItemCode = obj.ItemCode,
                  Seq = obj.QCT1000List.Count == 0 ? 1 : obj.QCT1000List.OrderByDescending(o=>o.Seq).FirstOrDefault().Seq + 1,
               // Seq = obj.QCT1000List.Count == 0 ? 1 : DbRequesHandler.GetCellValue("SELECT max(seq) + 1  FROM[HKInc_Data].[dbo].[TN_QCT1000T] where ITEM_CODE = '"+obj.ItemCode+"'", 0).GetIntNullToZero(),
                UseYn = "Y"
            };
            DetailGridBindingSource.Add(qc);
            obj.QCT1000List.Add(qc);
            DetailGridExControl.MainGrid.BestFitColumns();

        }
        protected override void DeleteDetailRow()
        {
            TN_STD1100 mobj = MasterGridBindingSource.Current as TN_STD1100;
            TN_QCT1000 obj = DetailGridBindingSource.Current as TN_QCT1000;
            if (obj == null) return;
            if (obj.CheckName.GetNullToEmpty() == "")
            {
                DetailGridBindingSource.RemoveCurrent();
                mobj.QCT1000List.Remove(obj);

            }
            else
            {
                obj.UseYn = "N";
            }
            DetailGridExControl.MainGrid.BestFitColumns();


        }
        protected override void DetailFileChooseClicked()
        {
            TN_STD1100 obj = MasterGridBindingSource.Current as TN_STD1100;
            if (obj== null) return;
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
            

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTQCT1000, param, AddPur1301);
            form.ShowPopup(true);
        }
        private void AddPur1301(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;


            List<TN_QCT1000JI> partList = (List<TN_QCT1000JI>)e.Map.GetValue(PopupParameter.ReturnObject);
            TN_STD1100 oldobj = MasterGridBindingSource.Current as TN_STD1100;
            foreach (var returnedPart in partList)
            {
                TN_QCT1000 dtl = oldobj.QCT1000List.Where(p => p.ProcessCode == returnedPart.ProcessCode && p.CheckName == returnedPart.CheckName &&
                    p.ProcessGu == returnedPart.ProcessGu && p.CheckProv == returnedPart.CheckProv).FirstOrDefault();
                if (dtl == null)
                {
                    TN_QCT1000 obj = (TN_QCT1000)DetailGridBindingSource.AddNew();
                    obj.ItemCode = oldobj.ItemCode;
                    obj.Seq = oldobj.QCT1000List.Count == 0 ? 1 : oldobj.QCT1000List.OrderByDescending(o => o.Seq).FirstOrDefault().Seq + 1;// oldobj.QCT1000List.Count + 1;//returnedPart.Seq;
                    obj.ProcessCode = returnedPart.ProcessCode;
                    obj.CheckName = returnedPart.CheckName;
                    obj.ProcessGu = returnedPart.ProcessGu;
                    obj.CheckProv = returnedPart.CheckProv;
                    obj.CheckStand = returnedPart.CheckStand;
                    obj.UpQuad = returnedPart.UpQuad;
                    obj.DownQuad = returnedPart.DownQuad;
                    obj.Memo = returnedPart.Memo;
                    obj.UseYn = returnedPart.UseYn;
                    obj.Temp1 = returnedPart.Temp1;
                    obj.Temp2 = returnedPart.Temp2;
                    obj.SpcYn = returnedPart.SpcYn;


                    oldobj.QCT1000List.Add(obj);
                }
            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DataSave()
        {
            ModelService.Save();
            DataLoad();
        }
    }
}
