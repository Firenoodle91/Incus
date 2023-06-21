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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.MPS
{
    /// <summary>
    /// 자재재공재고
    /// </summary>
    public partial class XFMPS1800 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {

        IService<VI_SRC_STOCK_SUM> ModelService = (IService<VI_SRC_STOCK_SUM>)ProductionFactory.GetDomainService("VI_SRC_STOCK_SUM");
        public XFMPS1800()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
         

        }

        protected override void InitCombo()
        {
            lupitemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y"&&(p.TopCategory=="P03"||p.TopCategory == "P02")).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("SrcCode", "품목코드",false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("InQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Useqty", "사용수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("Stockqty", "재고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            

            DetailGridExControl.SetToolbarButtonVisible(false);
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "재고조정[Alt+R]");

            DetailGridExControl.MainGrid.AddColumn("ResultDate", "입출고일");
            DetailGridExControl.MainGrid.AddColumn("SrcLotNo", "출고 LOT NO");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("UseQty", "사용수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고");

        }

        protected override void InitRepository()
        {           
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcNm", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y"&&(p.TopCategory=="P03"||p.TopCategory == "P02")).OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");          
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            //string itemcode = lupitemcode.EditValue.GetNullToEmpty();
            string Item = textItemCodeName.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => p.TN_STD1100.ItemNm.Contains(Item) || p.TN_STD1100.ItemNm1.Contains(Item)
                                                                    )
                                                                    .OrderBy(p => p.TN_STD1100.ItemNm1)
                                                                    .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            VI_SRC_STOCK_SUM obj = MasterGridBindingSource.Current as VI_SRC_STOCK_SUM;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_SRC_USE>(p => p.SrcCode == obj.SrcCode).OrderBy(o => o.ResultDate).ThenBy(o => o.SrcLotNo).ThenBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            VI_SRC_STOCK_SUM obj = MasterGridBindingSource.Current as VI_SRC_STOCK_SUM;
            if (obj == null) return;
            MPS_Popup.XPFMPS1700 fm = new MPS_Popup.XPFMPS1700(obj);
            fm.ShowDialog();
            DataLoad();
        }

        protected override void DetailFileChooseClicked()
        {
            VI_SRC_USE obj = DetailGridBindingSource.Current as VI_SRC_USE;
            if (obj == null) return;
            if (obj.Memo.Contains("재고조정"))
            {
                MPS_Popup.XPFMPS1700 fm = new MPS_Popup.XPFMPS1700(false, obj);
                fm.ShowDialog();
            }
            else
            {
                MPS_Popup.XPFMPS1700 fm = new MPS_Popup.XPFMPS1700(true, obj);
                fm.ShowDialog();
            }
            DataLoad();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            VI_SRC_USE obj = DetailGridBindingSource.Current as VI_SRC_USE;
            if (obj.Memo.GetNullToEmpty() == "") return;
            if (e.Clicks >= 2 && obj.Memo.Contains("재고조정"))
            {
                MPS_Popup.XPFMPS1700 fm = new MPS_Popup.XPFMPS1700(false, obj);
                fm.ShowDialog();
                DataLoad();
            }
        }

    }
}
