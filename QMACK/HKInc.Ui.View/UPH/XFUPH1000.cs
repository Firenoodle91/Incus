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

namespace HKInc.Ui.View.UPH
{
    public partial class XFUPH1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");
        public XFUPH1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitControls()
        {
            base.InitControls();
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MachineCode","설비코드");
           // MasterGridExControl.MainGrid.AddColumn("Temp1", "설비그룹");
            MasterGridExControl.MainGrid.AddColumn("MachineName","설비명");
            MasterGridExControl.MainGrid.AddColumn("ModelNo","모델NO");          
            MasterGridExControl.MainGrid.AddColumn("Maker","제조사");
            MasterGridExControl.MainGrid.AddColumn("Memo","비고");
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.MainGrid.Init();
            IsDetailGridButtonExportEnabled = true;
            DetailGridExControl.MainGrid.AddColumn("MachineCode", "설비코드", false);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품번");            
            //DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            DetailGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            DetailGridExControl.MainGrid.AddColumn("UPH", "UPH(시간당 생산량)", HorzAlignment.Far, true);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemCode", "UPH", "ProcessCode");
            DetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
       //     MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Temp1", DbRequesHandler.GetCommCode(MasterCodeSTR.MCgroup), "Mcode", "Codename");        
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Maker", MasterCode.GetMasterCode((int)MasterCodeEnum.Maker).ToList());
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p =>(p.TopCategory== MasterCodeSTR.Topcategory_Final_Product||p.TopCategory==MasterCodeSTR.Topcategory_Half_Product||p.TopCategory==MasterCodeSTR.Tpocategory_Outsorcing_Product)&& p.UseYn == "Y").ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("UPH", DefaultBoolean.True, "n0", true);
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineCode");
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            MasterGridBindingSource.DataSource = ModelService.GetList(p =>  (p.MachineName.Contains(tx_MCnm.Text) || (p.MachineCode == tx_MCnm.Text)) &&
                                                                            (p.UseYn == "Y"))
                                                            .OrderBy(p => p.MachineName)
                                                            .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void MasterFocusedRowChanged()
        {
            var Mobj = MasterGridBindingSource.Current as TN_MEA1000;
            if (Mobj == null) { DetailGridExControl.MainGrid.Clear(); return; }

            DetailGridBindingSource.DataSource = Mobj.TN_UPH1000List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DetailAddRowClicked()
        {
            var Mobj = MasterGridBindingSource.Current as TN_MEA1000;
            if (Mobj == null) return;

            var NewDobj = new TN_UPH1000()
            {
                MachineCode = Mobj.MachineCode,
                UPH = 0
            };
            Mobj.TN_UPH1000List.Add(NewDobj);
            DetailGridBindingSource.Add(NewDobj);
            DetailGridBindingSource.MoveLast();
        }

        protected override void DeleteDetailRow()
        {
            var Mobj = MasterGridBindingSource.Current as TN_MEA1000;
            var Dobj = DetailGridBindingSource.Current as TN_UPH1000;
            if (Mobj == null || Dobj == null) return;

            Mobj.TN_UPH1000List.Remove(Dobj);
            DetailGridBindingSource.RemoveCurrent();
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as GridView;
            var Dobj = DetailGridBindingSource.Current as TN_UPH1000;
            if (e.Column.FieldName == "ItemCode" && Dobj != null)
            {
                string ItemCode = e.Value.GetNullToEmpty();
                var ItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == ItemCode).FirstOrDefault();
                if (ItemObj != null) 
                {
                    Dobj.TN_STD1100 = ItemObj;
                    DetailGridExControl.BestFitColumns();
                }
            }
        }

    }
}