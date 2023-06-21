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
using HKInc.Service.Handler;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.MPS
{
    public partial class XFMPS1300 : HKInc.Service.Base.ListMasterDetailDetailFormTemplate
    {
        IService<TN_MPS1300> ModelService = (IService<TN_MPS1300>)ProductionFactory.GetDomainService("TN_MPS1300");
        protected BindingSource temp = new BindingSource();

        public XFMPS1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            dp_plan.EditValue = DateTime.Today;
            dp_workstart.EditValue = DateTime.Today;

            //DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailMainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += DetailMainView_ShowingEditor;
        }

        private void DetailMainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            var detailObj = DetailGridBindingSource.Current as TN_MPS1400;
            if (detailObj == null) return;

            string fieldName = view.FocusedColumn.FieldName.GetNullToEmpty();

            if (fieldName != "Memo" && fieldName != "PlanQty")
            {
                if (view.GetFocusedRowCellValue("JobStates").GetIntNullToZero() != (int)MasterCodeEnum.POP_Status_Wait)
                    e.Cancel = true;
            }

            if (fieldName == "PlanQty")
            {
                if (view.GetFocusedRowCellValue("JobStates").GetIntNullToZero() == (int)MasterCodeEnum.POP_Status_End)
                    e.Cancel = true;
            }
        }

        protected override void InitCombo()
        {
          //  luptem.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.tem));
            //  lupItemcode.SetDefault(true, "ItemCode", "ItemNm1", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory!="P03" && p.UseYn=="Y").OrderBy(o=>o.ItemNm1).ToList());
        }
        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("PlanNo", "계획번호");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
           // MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Temp5", "팀");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
           // MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Lctype", "기종");
            MasterGridExControl.MainGrid.AddColumn("PlanQty", "계획수량");
            MasterGridExControl.MainGrid.AddColumn("PlanStartdt", "계획시작일");
            MasterGridExControl.MainGrid.AddColumn("PlanEnddt", "계획종료일");
            MasterGridExControl.MainGrid.AddColumn("Memo", "비고");
            MasterGridExControl.MainGrid.AddColumn("WorkorderYn", "작업지시여부");
            MasterGridExControl.MainGrid.AddColumn("OrderNo", false);
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "PlanStartdt", "PlanEnddt");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("WorkDate", "작업지시일");
            DetailGridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            DetailGridExControl.MainGrid.AddColumn("PlanNo", false);
            DetailGridExControl.MainGrid.AddColumn("JobStates", "상태");
            DetailGridExControl.MainGrid.AddColumn("PSeq", "공정순서");
            DetailGridExControl.MainGrid.AddColumn("Process", "공정");
            DetailGridExControl.MainGrid.AddColumn("MachineCode", "설비");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", false);
            DetailGridExControl.MainGrid.AddColumn("PlanQty", "지시수량");
            DetailGridExControl.MainGrid.AddColumn("OutProc", "외주여부");
            DetailGridExControl.MainGrid.AddColumn("DelivDate", false);
            DetailGridExControl.MainGrid.AddColumn("OrderNo", false);
            DetailGridExControl.MainGrid.AddColumn("WorkId", "작업자");
            DetailGridExControl.MainGrid.AddColumn("EMType", LabelConvert.GetLabelText("EMType"));
            DetailGridExControl.MainGrid.AddColumn("Memo", "비고");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "WorkDate", "PSeq", "OutProc","MachineCode", "PlanQty", "WorkId", "Memo", "EMType");
            SubDetailGridExControl.SetToolbarVisible(false);

            var barWorkOrderPrint = new DevExpress.XtraBars.BarButtonItem();
            barWorkOrderPrint.Id = 4;
            //barWorkOrderPrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/boreport2");
            barWorkOrderPrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/boreport2");
            barWorkOrderPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barWorkOrderPrint.Name = "barPoDocumentPrint";
            barWorkOrderPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barWorkOrderPrint.ShortcutKeyDisplayString = "Alt+P";
            barWorkOrderPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barWorkOrderPrint.Caption = LabelConvert.GetLabelText("WorkOrderPrint") + "[Alt+P]";
            barWorkOrderPrint.ItemClick += BarWorkOrderPrint_ItemClick;
            barWorkOrderPrint.Enabled = UserRight.HasEdit;
            barWorkOrderPrint.Alignment = BarItemLinkAlignment.Right;
            DetailGridExControl.BarTools.AddItem(barWorkOrderPrint);
        }
        protected override void InitRepository()
        {
         //   MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanStartdt");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("PlanEnddt");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequesHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
      //      MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Temp5", DbRequesHandler.GetCommCode(MasterCodeSTR.tem), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Process", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WorkId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkDate");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("EMType", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutProc", "N");// DbRequesHandler.GetCommCode(MasterCodeSTR.YN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory == "P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("JobStates", MasterCode.GetMasterCode((int)MasterCodeEnum.PopStatus).ToList());
        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("PlanNo");

            ModelService.ReLoad();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            string dt = dp_plan.EditValue.ToString().Replace("-","").Substring(0,6);
            string item = tx_item.EditValue.GetNullToEmpty();
           // string tem = luptem.EditValue.GetNullToEmpty();
             List<TN_MPS1300> mps1300= ModelService.GetList(p=>string.IsNullOrEmpty(item)?true:( p.ItemCode.Contains(item)||p.TN_STD1100.ItemNm.Contains(item)||p.TN_STD1100.ItemNm1.Contains(item))).ToList();
            MasterGridBindingSource.DataSource = mps1300.Where(p => (p.PlanYYMM == dt) ).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();

            DataSet ds = DbRequesHandler.GetDataQury("exec  SP_MPS1400_LIST '" + dt + "'");
            SubDetailGridBindingSource.DataSource = ds.Tables[0];
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();

            GridRowLocator.SetCurrentRow();
        }
        protected override void MasterFocusedRowChanged()
        {
            TN_MPS1300 obj = MasterGridBindingSource.Current as TN_MPS1300;
            if (obj == null) return;
            var result = obj.MPS1400List.ToList();
            DetailGridBindingSource.DataSource = result.OrderBy(o => o.WorkNo).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DetailAddRowClicked()
        {
            TN_MPS1300 obj = MasterGridBindingSource.Current as TN_MPS1300;
            if (obj == null) return;
            List<TN_MPS1000> process = ModelService.GetChildList<TN_MPS1000>(p => p.ItemCode == obj.ItemCode && p.UseYn == "Y").OrderBy(o => o.ProcessSeq).ToList();
            DateTime dt =Convert.ToDateTime(dp_workstart.EditValue);
            string workno = DbRequesHandler.GetRequestNumber("WNO");
            for (int i = 0; i < process.Count; i++)
            {   if (i != 0)
                {
                    dt = dt.AddDays(Convert.ToInt32(process[i].STD));
                }
                TN_MPS1400 newobj = new TN_MPS1400()
                {
                    WorkDate=dt,
                    WorkNo= workno,
                    PlanNo =obj.PlanNo,
                    //PSeq=Convert.ToInt32(process[i].ProcessSeq),
                    PSeq = i + 1, // 20220111 오세완 차장 공정순번을 가운데가 이빨이 빠져도 순차적으로 설정하게 변경
                    Process = process[i].ProcessCode,
                    ItemCode=obj.ItemCode,
                    PlanQty=obj.PlanQty,
                    DelivDate=obj.DelivDate,
                    OrderNo=obj.OrderNo,
                    DelivSeq=obj.DelivSeq,
                    OutProc=process[i].OutProc,
                    JobStates=((int)MasterCodeEnum.POP_Status_Wait).ToString()

                    

                };
                DetailGridBindingSource.Add(newobj);
                obj.WorkorderYn = "Y";
                obj.MPS1400List.Add(newobj);
                
            }
          
            DetailGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteDetailRow()
        {
            TN_MPS1300 Tobj = MasterGridBindingSource.Current as TN_MPS1300;
            TN_MPS1400 obj = DetailGridBindingSource.Current as TN_MPS1400;

            if (obj == null) return;
            
            if (DialogResult.Yes == MessageBox.Show("일괄삭제인가요?", "알림", MessageBoxButtons.YesNo))
            {
                List<TN_MPS1400> mps1400List = ModelService.GetChildList<TN_MPS1400>(x => x.WorkNo == obj.WorkNo).ToList();

                //대기 제외한 작업상태값 개수체크
                if (mps1400List.Where(x => x.JobStates != ((int)MasterCodeEnum.POP_Status_Wait).ToString() ).Count() > 0)
                {
                    MessageBox.Show("생산이 시작된 작업은 삭제할수 없습니다.");
                    if (Tobj.MPS1400List.Count >= 1) { Tobj.WorkorderYn = "Y"; }
                    else
                    {
                        Tobj.WorkorderYn = "N";
                    }
                }
                else
                {
                    foreach (var s in Tobj.MPS1400List.Where(x => x.WorkNo == obj.WorkNo).ToList())
                    {
                        DetailGridBindingSource.Remove(s);
                        Tobj.MPS1400List.Remove(s);
                        try
                        {
                            ModelService.RemoveChild<TN_MPS1400>(s);
                        }
                        catch (Exception e) { }
                    }
                    if (Tobj.MPS1400List.Count >= 1) { Tobj.WorkorderYn = "Y"; }
                    else
                    {
                        Tobj.WorkorderYn = "N";
                    }

                    //ModelService.Update(Tobj);

                    MasterGridExControl.MainGrid.BestFitColumns();
                    DetailGridExControl.MainGrid.BestFitColumns();
                }
            }
            else
            {       
                List<TN_MPS1401> mps1401 = ModelService.GetChildList<TN_MPS1401>(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process).ToList();
                if (mps1401.Count >= 1)
                {
                    MessageBox.Show("생산이 시작된 공정 작업은 삭제할수 없습니다.");
                    if (Tobj.MPS1400List.Count >= 1) { Tobj.WorkorderYn = "Y"; }
                    else
                    {
                        Tobj.WorkorderYn = "N";
                    }
                }
                else
                {
                    Tobj.MPS1400List.Remove(obj);
                    DetailGridBindingSource.Remove(obj);
                  
                    ModelService.Update(Tobj);
                    try
                    {
                        ModelService.RemoveChild<TN_MPS1400>(obj);
                    }
                    catch (Exception e) { }
                }

                if (Tobj.MPS1400List.Count >= 1) { Tobj.WorkorderYn = "Y"; }
                else
                {
                    Tobj.WorkorderYn = "N";
                }

                ModelService.Update(Tobj);
              
                MasterGridExControl.MainGrid.BestFitColumns();
                DetailGridExControl.MainGrid.BestFitColumns();
            }
        }

        private void BarWorkOrderPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DetailGridBindingSource == null)
                return;

            if (!UserRight.HasEdit)
                return;

            var detailObj = DetailGridBindingSource.Current as TN_MPS1400;
            if (detailObj == null)
                return;

            if (detailObj.TN_MPS1300 == null)
            {
                MessageBoxHandler.Show("작업지시 저장후 출력하십시오.");
                return;
            }

            try
            {
                var detailList = DetailGridBindingSource.List as List<TN_MPS1400>;
                WaitHandler.ShowWait();

                var report = new REPORT.XRMPS1200(detailList.Where(p => p.WorkNo == detailObj.WorkNo).OrderBy(p => p.PSeq).First());
                report.CreateDocument();

                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                
                report.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex.Message);
            }
            finally
            {
                WaitHandler.CloseWait();
            }
        }


        protected override void DataSave()
        {
            ModelService.Save();
            DataLoad();   
        }
    }
}
