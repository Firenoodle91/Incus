using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;

using HKInc.Ui.Model.Domain;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;

namespace HKInc.Ui.View.SELECT_Popup
{
    /// <summary>
    /// 검사규격관리 복사하기 팝업
    /// </summary>
    public partial class XSFQCSTDCOPY : HKInc.Service.Base.POP_PopupCallbackFormTemplate
    {
        IService<TN_QCT1000> ModelService = (IService<TN_QCT1000>)ProductionFactory.GetDomainService("TN_QCT1000");

        //BindingSource bindingSource = new BindingSource();        // 2022-02-23 김진우 미사용 주석
        private bool IsmultiSelect = true;
        //private string DepartmentCode = string.Empty;             // 2022-02-23 김진우 미사용 주석
        //private string qctype = string.Empty;                     // 2022-02-23 김진우 미사용 주석
        public XSFQCSTDCOPY()
        {
            InitializeComponent();
        }

        public XSFQCSTDCOPY(PopupDataParam parameter, PopupCallback callback) :this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Text);
            lupitemcode.EditValueChanged += Lupitemcode_EditValueChanged;
            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            //if (parameter.ContainsKey(PopupParameter.Value_1))                                        // 2022-02-23 김진우 미사용 주석
            //    qctype = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            //if (parameter.ContainsKey(PopupParameter.Constraint))                                     // 2022-02-23 김진우 미사용 주석
            //    DepartmentCode = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
        }

        private void Lupitemcode_EditValueChanged(object sender, EventArgs e)
        {
            DataLoad();
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();
            //this.Text = "복사하기";                   // 2022-02-23 김진우 주석
            gridEx1.MainGrid.MainView.RowClick += MainView_RowClick;
            lupitemcode.SetDefault(false, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o=>o.ItemNm1).ToList());
        }

        protected override void InitGrid()
        {
            gridEx1.MainGrid.Init();
            gridEx1.MainGrid.MultiSelect = IsmultiSelect;
            gridEx1.MainGrid.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridEx1.SetToolbarVisible(false);
  //          gridEx1.MainGrid.AddColumn("_Check", "선택");       // 2022-02-23 김진우 추가
            gridEx1.MainGrid.AddColumn("ItemCode");
            gridEx1.MainGrid.AddColumn("Temp2", "검사순서");    // 2022-04-20 김진우 수정
            gridEx1.MainGrid.AddColumn("ProcessCode");
            gridEx1.MainGrid.AddColumn("CheckName", "검사항목");    // 2022-04-20 김진우 수정
            gridEx1.MainGrid.AddColumn("ProcessGu");
            gridEx1.MainGrid.AddColumn("CheckProv", "검사방법");    // 2022-04-20 김진우 수정
            gridEx1.MainGrid.AddColumn("CheckStand");
            gridEx1.MainGrid.AddColumn("UpQuad");
            gridEx1.MainGrid.AddColumn("DownQuad");
    //        gridEx1.MainGrid.SetEditable("_Check");
          
            //gridEx1.MainGrid.BestFitColumns();
        }

        protected override void InitRepository()
        {
   //         gridEx1.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");             // 2022-02-23 김진우 추가
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessGu", DbRequestHandler.GetCommCode(MasterCodeSTR.QCKIND), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckProv", DbRequestHandler.GetCommCode(MasterCodeSTR.QCTYPE), "Mcode", "Codename");
            gridEx1.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckName", DbRequestHandler.GetCommCode(MasterCodeSTR.QCPOINT), "Mcode", "Codename");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            string itemName = lupitemcode.EditValue.GetNullToEmpty();
            ModelBindingSource.DataSource = ModelService.GetList(p=> p.ItemCode == itemName).OrderBy(p => p.Seq).ToList();

            gridEx1.DataSource = ModelBindingSource;
            gridEx1.BestFitColumns();
        }

        /// <summary>
        /// 2022-02-23 김진우 수정
        /// 선택된 물품에 대한 정보를 확인버튼 누를시에 전송되도록 수정
        /// </summary>
        protected override void Confirm()
        {
            /*       PopupDataParam Param = new PopupDataParam();

                   if (IsmultiSelect)
                   {
                       List<TN_QCT1000> AddList = new List<TN_QCT1000>();
                       List<TN_QCT1000> QCT_POPUP_List = ModelBindingSource.List as List<TN_QCT1000>;

                       foreach (TN_QCT1000 QCT in QCT_POPUP_List)
                       {
                           if (QCT._Check == "Y")
                               AddList.Add(QCT);
                       }
                       Param.SetValue(PopupParameter.ReturnObject, AddList);
                       ReturnPopupArgument = new PopupArgument(Param);
                   }

                   Close();*/
            //BaseForm_FormClosing(false);

            //Close();
            #region 기존소스

            PopupDataParam param = new PopupDataParam();

            string Constraint = this.PopupParam.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            if (IsmultiSelect)
            {
                List<TN_QCT1000> itemMasterList = new List<TN_QCT1000>();

                foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
                {
                    TN_QCT1000 tn = new TN_QCT1000();
                    tn.ProcessCode = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessCode").GetNullToEmpty();
                    tn.ProcessGu = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessGu").GetNullToEmpty();
                    tn.CheckName = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckName").GetNullToEmpty();
                    tn.CheckProv = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckProv").GetNullToEmpty();
                    tn.CheckStand = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckStand").GetNullToEmpty();
                    tn.UpQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "UpQuad").GetDoubleNullToZero();
                    tn.DownQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "DownQuad").GetDoubleNullToZero();
                    tn.Temp2 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty();
                    itemMasterList.Add(tn);
                }
                param.SetValue(PopupParameter.ReturnObject, itemMasterList);
            }

            ReturnPopupArgument = new PopupArgument(param);
            base.Close();

            #endregion
        }

        /// <summary>
        /// 2022-02-23 김진우 주임 수정
        /// 더블클릭시에만 팝업에서 데이터 전송할수 있도록 수정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2) 
            {
                List<TN_QCT1000> QCTList = new List<TN_QCT1000>();
                TN_QCT1000 QCT = ModelBindingSource.Current as TN_QCT1000;

                QCTList.Add(QCT);

                param.SetValue(PopupParameter.ReturnObject, QCTList);
                ReturnPopupArgument = new PopupArgument(param);

                Close();
            }
            #region 기존소스

            //PopupDataParam param = new PopupDataParam();

            //if (e.Clicks == 2)
            //{
            //    List<TP_QC1000> itemMasterList = new List<TP_QC1000>();

            //    foreach (var rowHandle in gridEx1.MainGrid.MainView.GetSelectedRows())
            //    {
            //        TP_QC1000 tn = new TP_QC1000();
            //        tn.ProcessCode = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessCode").GetNullToEmpty();
            //        tn.ProcessGu = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "ProcessGu").GetNullToEmpty();
            //        tn.CheckName = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckName").GetNullToEmpty();
            //        tn.CheckProv = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckProv").GetNullToEmpty();
            //        tn.CheckStand = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "CheckStand").GetNullToEmpty();
            //        tn.UpQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "UpQuad").GetDoubleNullToZero();
            //        tn.DownQuad = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "DownQuad").GetDoubleNullToZero();
            //        tn.Temp2 = gridEx1.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty();
            //        itemMasterList.Add(tn);
            //    }
            //    param.SetValue(PopupParameter.ReturnObject, itemMasterList);
            //    ReturnPopupArgument = new PopupArgument(param);

            //    base.ActClose();
            //}
            //else
            //{
            //    List<TP_QC1000> itemMasterList1 = new List<TP_QC1000>();
            //    TP_QC1000 tn = new TP_QC1000();
            //    tn.ProcessCode = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "ProcessCode").GetNullToEmpty();
            //    tn.ProcessGu = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "ProcessGu").GetNullToEmpty();
            //    tn.CheckName = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "CheckName").GetNullToEmpty();
            //    tn.CheckProv = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "CheckProv").GetNullToEmpty();
            //    tn.CheckStand = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "CheckStand").GetNullToEmpty();
            //    tn.UpQuad = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "UpQuad").GetDoubleNullToZero();
            //    tn.DownQuad = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "DownQuad").GetDoubleNullToZero();
            //    tn.Temp2 = gridEx1.MainGrid.MainView.GetRowCellValue(e.RowHandle, "Temp2").GetNullToEmpty();
            //    itemMasterList1.Add(tn);
            //    param.SetValue(PopupParameter.ReturnObject, itemMasterList1);
            //}

            //ReturnPopupArgument = new PopupArgument(param);

            //base.ActClose();

            #endregion
        }

        /// <summary>
        /// 종료시 메세지떠서 생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void BaseForm_FormClosing(object sender, FormClosingEventArgs e) { }
    }
}

