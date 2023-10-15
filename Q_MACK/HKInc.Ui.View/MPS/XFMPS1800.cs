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
    /// 일일생산실적
    /// </summary>
    public partial class XFMPS1800 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_RESULT_QTY> ModelService = (IService<VI_RESULT_QTY>)ProductionFactory.GetDomainService("VI_RESULT_QTY");
        
        public XFMPS1800()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;

            // 2022-06-22 김진우 추가        기존 날짜구간조회방식에서 날짜 지정방식으로 변경      기존 조회방식은 Visible Never로 변경
            dt_Date.SetFormat(DateFormat.Day);
            dt_Date.DateTime = DateTime.Today;
            dt_Date.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            //dpdt.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            //dpdt.DateToEdit.DateTime = DateTime.Today;
        }
     
        protected override void InitCombo()
        {
            lupmachine.SetDefault(true, "MachineCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList());
            // 20220219 오세완 차장 품목 / 품명이 동일하게 나오는 오류가 있어서 생략             2022-02-23 김진우 ItemCodeColumnSetting 수정하여 원복
            lupitemcode.SetDefault(true, "ItemCode", "ItemNm", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == MasterCodeSTR.Topcategory_Final_Product || p.TopCategory == MasterCodeSTR.Topcategory_Half_Product)).ToList());
            lupProc.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process, "", "", ""));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");                      
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", "공정");
            MasterGridExControl.MainGrid.AddColumn("ResultDate", "작업일");
            MasterGridExControl.MainGrid.AddColumn("WorkId", "작업자");
            MasterGridExControl.MainGrid.AddColumn("MachineCode", "설비");
            MasterGridExControl.MainGrid.AddColumn("OrderQty", "지시수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Resultqty", "생산량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Okqty", "양품수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            MasterGridExControl.MainGrid.AddColumn("Failqty", "불량수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");

            MasterGridExControl.MainGrid.ShowFooter = true;
            //MasterGridExControl.MainGrid.SummaryItemAddNew(6);
            MasterGridExControl.MainGrid.SummaryItemAddNew(7);
            MasterGridExControl.MainGrid.SummaryItemAddNew(8);
            MasterGridExControl.MainGrid.SummaryItemAddNew(9);
            MasterGridExControl.MainGrid.SummaryItemAddNew(10);
        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("SrcNm", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y"&&(p.TopCategory=="P03"||p.TopCategory == "P02")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process, "", "", ""), "Mcode", "Codename");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => true).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => true).ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();
            MasterGridExControl.MainGrid.Clear();

            string itemcode = lupitemcode.EditValue.GetNullToEmpty();
            string machine = lupmachine.EditValue.GetNullToEmpty();
            string proc = lupProc.EditValue.GetNullToEmpty();

            // 2022-06-22 김진우 추가            ResultDate가 시간값을 가지고 있어서 개별로 조건넣음
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.ResultDate.Year == dt_Date.DateTime.Year)
                                                                        && (p.ResultDate.Month == dt_Date.DateTime.Month)
                                                                        && (p.ResultDate.Day == dt_Date.DateTime.Day)
                                                                        && (string.IsNullOrEmpty(itemcode) ? true : p.ItemCode == itemcode)
                                                                        && (string.IsNullOrEmpty(proc) ? true : p.ProcessCode == proc)
                                                                        && (string.IsNullOrEmpty(machine) ? true : p.MachineCode == machine)
                                                                        ).OrderBy(o => o.WorkNo).OrderBy(o => o.Pseq).ToList();

            //MasterGridBindingSource.DataSource = ModelService.GetList(p 
            //    =>p.ResultDate >= dpdt.DateFrEdit.DateTime
            //    &&p.ResultDate <= dpdt.DateToEdit.DateTime
            //    &&(string.IsNullOrEmpty(itemcode) ? true:p.ItemCode==itemcode )
            //    &&(string.IsNullOrEmpty(proc) ? true:p.ProcessCode==proc)
            //    &&(string.IsNullOrEmpty(machine) ? true:p.MachineCode==machine)
            //).OrderBy(o=>o.WorkNo).OrderBy(s=>s.Pseq).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();
        }
      
    }
}
