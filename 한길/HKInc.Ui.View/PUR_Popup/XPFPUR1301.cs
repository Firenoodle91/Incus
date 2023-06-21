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
using HKInc.Service.Base;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Utils.Common;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.PUR_Popup
{
    public partial class XPFPUR1301 : PopupCallbackFormTemplate
    {
        IService<TN_PUR1301> ModelService = (IService<TN_PUR1301>)ProductionFactory.GetDomainService("TN_PUR1301");
        TN_PUR1301 detailObj;

        public XPFPUR1301()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            GridExControl.ActAddRowClicked += GridExControl_ActAddRowClicked;
            GridExControl.ActDeleteRowClicked += GridExControl_ActDeleteRowClicked;

            ModelBindingSource.CurrentItemChanged -= ModelBindingSource_CurrentItemChanged;
        }

        public XPFPUR1301(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            detailObj = (TN_PUR1301)PopupParam.GetValue(PopupParameter.KeyValue);
        }
        
        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitControls()
        {
            base.InitControls();

            textItemCode.ReadOnly = true;
            textItemName.ReadOnly = true;
            textPoNo.ReadOnly = true;
            textPoQty.ReadOnly = true;
            textPoCost.ReadOnly = true;
            textPoAmt.ReadOnly = true;

            textItemCode.EditValue = detailObj.ItemCode;
            textItemName.EditValue = detailObj.TN_STD1100.ItemNm;
            textPoNo.EditValue = detailObj.ReqNo;
            textPoQty.EditValue = detailObj.ReqQty.GetIntNullToZero().ToString("n0");
            textPoCost.EditValue = detailObj.Cost.GetIntNullToZero().ToString("n0");
            textPoAmt.EditValue = detailObj.ReqAmt.GetIntNullToZero().ToString("n0");
            textInQty.EditValue = detailObj.InputQty;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            //GridExControl.MainGrid.AddColumn("_Check", "선택");
            GridExControl.MainGrid.AddColumn("InputNo", false);
            GridExControl.MainGrid.AddColumn("InputSeq", "입고순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0", false);
            GridExControl.MainGrid.AddColumn("ItemCode", "품번");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("Temp2", "입고 LOT NO");
            //GridExControl.MainGrid.AddColumn("ReqNo", "발주번호",false);
            //GridExControl.MainGrid.AddColumn("ReqSeq", "발주순번", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("ReqQty", "발주수량");
            //GridExControl.MainGrid.AddColumn("Cost", "발주단가");
            //GridExControl.MainGrid.AddColumn("ReqAmt", "발주금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("InputQty", "입고수량");
            GridExControl.MainGrid.AddColumn("InCost", "입고단가");
            GridExControl.MainGrid.AddColumn("InputAmt", "입고금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Lqty", "롤 수");
            //GridExControl.MainGrid.AddColumn("InYn", false);
            GridExControl.MainGrid.AddColumn("WhCode", "입고창고");
            GridExControl.MainGrid.AddColumn("WhPosition", "입고위치");
            GridExControl.MainGrid.AddColumn("Memo");
            //GridExControl.MainGrid.AddColumn("inqcf");

            GridExControl.MainGrid.SetEditable("InCost", "Memo", "WhCode", "Lqty", "WhPosition");
            //GridExControl.MainGrid.SetHeaderColor(Color.Red, "InputQty");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("InputQty", DefaultBoolean.Default, "n0", true);
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("InCost", DefaultBoolean.Default, "n0", true);
            GridExControl.MainGrid.SetRepositoryItemSpinEdit("Lqty", DefaultBoolean.Default, "n0", true);
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, true, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);            
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            ModelBindingSource.DataSource = ModelService.GetList(p => p.Temp2 == detailObj.Temp2).ToList();

            GridExControl.DataSource = ModelBindingSource;

            var list = ModelBindingSource.List as List<TN_PUR1301>;
            if (list.Count == 0)
            {
                //저장을 안하고 분할처리 버튼 클릭 시
                ModelBindingSource.Add(detailObj);
            }

            GridExControl.BestFitColumns();

            SetIsFormControlChanged(false);
        }

        private void GridExControl_ActAddRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ModelBindingSource.Add(new TN_PUR1301()
            {
                ItemCode = detailObj.ItemCode
                ,TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == detailObj.ItemCode).First()
            });
        }

        private void GridExControl_ActDeleteRowClicked(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TN_PUR1301 dtlobj = ModelBindingSource.Current as TN_PUR1301;
            if (!dtlobj.Temp2.IsNullOrEmpty())
            {
                MessageBoxHandler.Show("초기 분할을 시키기 위한 데이터이기 때문에 삭제할 수 없습니다.");
                return;
            }
            ModelBindingSource.RemoveCurrent();
        }

        protected override void ActConfirm()
        {
            var list = ModelBindingSource.List as List<TN_PUR1301>;
            if (list == null) return;

            var updateObj = list.Where(p => !p.Temp2.IsNullOrEmpty()).First();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Value_1, updateObj);
            param.SetValue(PopupParameter.ReturnObject, list);
            ReturnPopupArgument = new PopupArgument(param);

            ModelService.Dispose();

            SetIsFormControlChanged(false);
            ActClose();
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = GridExControl.MainGrid.MainView as GridView;
            TN_PUR1301 dtlobj = ModelBindingSource.Current as TN_PUR1301;
            if (e.Column.FieldName == "WhCode")
            {
                GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y" && p.WhCode == dtlobj.WhCode).ToList(), "PosionCode", "PosionName");
            }
            else if (e.Column.FieldName == "WhPosition")
            {
                //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
            }
            else if (e.Column.FieldName == "Lqty")
            {
                var dtlList = ModelBindingSource.List as List<TN_PUR1301>;
                int inQty = textInQty.EditValue.GetIntNullToZero();
                int sumLqty = dtlList.Sum(p => p.Lqty).GetIntNullToZero();
                if (sumLqty != 0)
                {
                    int oneQty = inQty / sumLqty;
                    foreach (var v in dtlList)
                    {
                        v.InputQty = oneQty * v.Lqty;
                    }
                }
                else
                {
                    dtlobj.InputQty = inQty;
                }
                GridExControl.MainGrid.MainView.RefreshData();
            }
        }
    }
}