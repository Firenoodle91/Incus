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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.QCT_POPUP
{
    /// <summary>
    /// 20210924 오세완 차장
    /// 출하검사 상세목록 추가 
    /// </summary>
    public partial class XPFQCT1900_DETAIL : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_ORD1201> ModelService = (IService<TN_ORD1201>)ProductionFactory.GetDomainService("TN_ORD1201");
        private bool IsmultiSelect = true;

        /// <summary>
        /// 20210927 오세완 차장 기추가 생산lotno 중복추가 방지 하기 위함
        /// </summary>
        //private List<TN_QCT1901> returnArr = new List<TN_QCT1901>();
        private List<string> rtnArr_ProductLotNo = new List<string>();

        private int gi_LastSelectedTargetRow = -1;
        #endregion

        public XPFQCT1900_DETAIL()
        {
            InitializeComponent();
        }

        public XPFQCT1900_DETAIL(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ShipDetail");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);

            // 20210924 오세완 차장 출고번호
            textEdit2.EditValue = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            //returnArr = (List<TN_QCT1901>)parameter.GetValue(PopupParameter.Value_2);
            rtnArr_ProductLotNo = (List<string>)parameter.GetValue(PopupParameter.Value_2);

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
            GridExControl.MainGrid.MainView.CellValueChanging += MainView_CellValueChanging;
        }

        /// <summary>
        /// 20211013 오세완 차장 1개의 row만 선택하는 로직 구현
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null)
                return;

            if (e.Column.FieldName == "_Check" && e.Value.ToString() == "Y")
                for(int i=0; i<view.RowCount; i++)
                {
                    view.SetRowCellValue(i, "_Check", "N");
                }
        }

        protected override void InitBindingSource() { }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            
        }
        
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            GridExControl.MainGrid.CheckBoxMultiSelect(true, "ProductLotNo", IsmultiSelect);
            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo")); // 20210129 오세완 차장 outno -> ProductLotNo 수정처리
            GridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            GridExControl.MainGrid.AddColumn("Memo");

            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string sValue = textEdit2.EditValue.ToString();
            List<TN_ORD1201> tempList = ModelService.GetList(p => p.OutNo == sValue );
            if(tempList != null)
            {
                if(tempList.Count > 0)
                {
                    List<TN_ORD1201> tempList2 = new List<TN_ORD1201>();
                    foreach(TN_ORD1201 each in tempList)
                    {
                        if (each.Temp.GetNullToEmpty() == "Y")
                        {
                            //if(returnArr == null)
                            //    tempList2.Add(each);
                            //else if (returnArr.Count == 0)
                            //    tempList2.Add(each);
                            //else
                            //{
                            //    bool bDuplicate = false;
                            //    foreach(TN_QCT1901 returnEach in returnArr)
                            //    {
                            //        if (each.ProductLotNo == returnEach.ProductLotNo)
                            //            bDuplicate = true;
                            //    }

                            //    if(!bDuplicate)
                            //        tempList2.Add(each);
                            //}

                            if (rtnArr_ProductLotNo == null)
                                tempList2.Add(each);
                            else if (rtnArr_ProductLotNo.Count == 0)
                                tempList2.Add(each);
                            else
                            {
                                bool bFind = rtnArr_ProductLotNo.Exists(p=>p == each.ProductLotNo);
                                if (!bFind)
                                    tempList2.Add(each);
                            }

                        }
                    }

                    List<TN_ORD1201> searchList = new List<TN_ORD1201>();
                    if (tempList2.Count > 0)
                    {
                        // 20211029 오세완 차장 생산lotno 조회 로직이 빠져서 추가
                        string sLotNo = textEdit1.EditValue.GetNullToEmpty();
                        
                        if(sLotNo != "")
                            searchList = tempList2.Where(p=>p.ProductLotNo == sLotNo).OrderBy(o => o.OutSeq).ToList();
                        else
                            searchList = tempList2.OrderBy(p => p.OutSeq).ToList();
                        
                    }

                    //ModelBindingSource.DataSource = tempList2.OrderBy(p => p.OutSeq).ToList();
                    ModelBindingSource.DataSource = searchList;

                }
            }

            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        protected override void Confirm()
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            
            if (IsmultiSelect)
            {
                var returnList = new List<TN_ORD1201>();
                List<TN_ORD1201> tempList = ModelBindingSource.List as List<TN_ORD1201>;
                foreach(TN_ORD1201 each in tempList)
                {
                    if (each._Check.GetNullToEmpty() == "Y")
                        returnList.Add(each);
                }

                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                var obj = (TN_ORD1201)ModelBindingSource.Current;
                param.SetValue(PopupParameter.ReturnObject, obj);
            }

            ReturnPopupArgument = new PopupArgument(param);

            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var obj = (TN_ORD1201)ModelBindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<TN_ORD1201>();
                    List<TN_ORD1201> tempList = ModelBindingSource.List as List<TN_ORD1201>;
                    foreach (TN_ORD1201 each in tempList)
                    {
                        if (each._Check.GetNullToEmpty() == "Y")
                            returnList.Add(each);
                    }

                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, obj);
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }
        }
    }
}