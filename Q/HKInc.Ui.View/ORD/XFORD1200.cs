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
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.ORD
{
    /// <summary>
    /// 제품출고관리
    /// </summary>
    public partial class XFORD1200 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1200> ModelService = (IService<TN_ORD1200>)ProductionFactory.GetDomainService("TN_ORD1200");

        public XFORD1200()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dateOrderDate.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dateOrderDate.DateToEdit.DateTime = DateTime.Today;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("OutNo", "출고번호");
            MasterGridExControl.MainGrid.AddColumn("DelivSeq", "납품계획번호");
            MasterGridExControl.MainGrid.AddColumn("OrderNo", "수주번호");
            MasterGridExControl.MainGrid.AddColumn("OrderSeq", "순번");
            MasterGridExControl.MainGrid.AddColumn("CustCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번"); // 20220219 오세완 차장 품번 출력 추가
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Lctype", "기종");
            MasterGridExControl.MainGrid.AddColumn("OrderQty", "출고지시수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutDate", "출고일");
            MasterGridExControl.MainGrid.AddColumn("OutId", "출고자");
            MasterGridExControl.MainGrid.AddColumn("OutState", "상태");
            MasterGridExControl.MainGrid.AddColumn("Memo", "메모");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutId", "OutState", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("OutNo", false);
            DetailGridExControl.MainGrid.AddColumn("Seq", "순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            DetailGridExControl.MainGrid.AddColumn("TN_ORD1200.TN_STD1100.ItemNm", "품목");
            DetailGridExControl.MainGrid.AddColumn("TN_ORD1200.TN_STD1100.ItemNm1", "품명");
            DetailGridExControl.MainGrid.AddColumn("LotNo", "LOTNO");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutDate", "출고일");            
            DetailGridExControl.MainGrid.AddColumn("Memo", "메모");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "LotNo", "OutQty","OutDate", "Memo");
            DetailGridExControl.MainGrid.MainView.Columns["OutQty"].RealColumnEdit.EditValueChanged += RealColumnEdit_EditValueChanged;

            // 2022-04-11 김진우 추가        바코드 리딩기능 추가
            var barTextEditBarCode = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCode.Id = 6;
            barTextEditBarCode.Enabled = UserRight.HasEdit;
            barTextEditBarCode.Name = "barTextEditBarCode";
            barTextEditBarCode.EditWidth = 150;
            barTextEditBarCode.Edit.KeyDown += Edit_KeyDown;
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode);

            // 2022-04-11 김진우 추가        바코드 리딩기능 추가
            var barTextEditBarCodeStaticItem = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCodeStaticItem.Id = 7;
            barTextEditBarCodeStaticItem.Name = "barTextEditBarCodeStaticItem";
            barTextEditBarCodeStaticItem.Edit.NullText = LabelConvert.GetLabelText("포장바코드") + " : ";
            barTextEditBarCodeStaticItem.EditWidth = barTextEditBarCodeStaticItem.Edit.NullText.Length * 10;
            //barTextEditBarCodeStaticItem.EditWidth = 120;
            barTextEditBarCodeStaticItem.Enabled = false;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barTextEditBarCodeStaticItem.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Alignment = BarItemLinkAlignment.Left;
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem);
        }

        private void RealColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            string lot = DetailGridExControl.MainGrid.MainView.GetFocusedRowCellValue("LotNo").ToString();
            int outqty = DetailGridExControl.MainGrid.MainView.ActiveEditor.EditValue.GetIntNullToZero();

            VI_PRODQTYMSTLOT sqty = ModelService.GetChildList<VI_PRODQTYMSTLOT>(p => p.LotNo == lot).OrderBy(o => o.LotNo).FirstOrDefault();
            if (outqty > sqty.Stockqty)
            {
                MessageBox.Show("재고량보다 많은 양은 출고등록할수 없습니다.");
                DetailGridExControl.MainGrid.MainView.SetFocusedRowCellValue("OutQty", DetailGridExControl.MainGrid.MainView.ActiveEditor.OldEditValue);
                return;
            }

            //masterreview();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            // 20220219 오세완 차장 품목코드 / 품번 / 품명을 다출력해서 변환이 필요 없음
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx2, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.TopCategory != "P03" && p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm");
        }

        protected override void InitCombo()
        {
            lupCustcode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            //DataSet ds = DbRequestHandler.GetDataQury("exec SP_OUTFLAG_UPDATE");            // 확인하기
            string customerCode = lupCustcode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dateOrderDate.DateFrEdit.DateTime
                                                                         && p.OutDate <= dateOrderDate.DateToEdit.DateTime)
                                                                         && (string.IsNullOrEmpty(customerCode) ? true : p.CustCode == customerCode))
                                                                        .OrderBy(p => p.OutDate)
                                                                        .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            //DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_ORD1201>(p => p.OutNo == obj.OutNo).ToList();
            DetailGridBindingSource.DataSource = obj.ORD1201List.OrderBy(o => o.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }
        
        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();

            masterreview();
            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTORD1200, param, AddOrderList);
            form.ShowPopup(true);
        }

        protected override void DeleteRow()
        {
            TN_ORD1200 tn = MasterGridBindingSource.Current as TN_ORD1200;
            if (tn.ORD1201List.Count > 0)
            {
                MessageBox.Show("출고내역이 있어서 삭제 불가합니다.");
            }
            else
            {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(tn);
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            param.SetValue(PopupParameter.Value_1,obj.ItemCode);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTORD1201, param, AddDtlList);
            form.ShowPopup(true);
        }

        private void AddDtlList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            List<VI_PRODQTYMSTLOT> partList = (List<VI_PRODQTYMSTLOT>)e.Map.GetValue(PopupParameter.ReturnObject);
          TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
         //   obj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();
            if (obj == null) return;
            foreach (var returnedPart in partList)
            {
                TN_ORD1201 newobj = new TN_ORD1201()
                {
                    OutNo = obj.OutNo,
                    Seq = obj.ORD1201List.Count == 0 ? 1 : obj.ORD1201List.OrderBy(o => o.Seq).LastOrDefault().Seq + 1,
                    ItemCode = obj.ItemCode,
                    LotNo = returnedPart.LotNo,
                    OutDate = DateTime.Today,
                    OutQty= returnedPart.Stockqty
                    ,TN_ORD1200=obj
                };
                
                DetailGridBindingSource.Add(newobj);
                obj.ORD1201List.Add(newobj);
            }
            //masterreview();
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextEdit TextEdit = sender as TextEdit;
                if (TextEdit == null) return;

                string BarCodeData = TextEdit.EditValue.ToString();
                TN_ORD1200 MasterObj = MasterGridBindingSource.Current as TN_ORD1200;
                if (MasterObj == null) return;

                VI_PRODQTYMSTLOT CallData = ModelService.GetChildList<VI_PRODQTYMSTLOT>(p => p.LotNo == BarCodeData).FirstOrDefault();
                if (CallData == null)
                {
                    MessageBoxHandler.Show("등록되지 않은 Lot No 입니다.");
                    return;
                }

                TN_ORD1201 DetailObj = new TN_ORD1201();

                DetailObj.OutNo = MasterObj.OutNo;
                DetailObj.Seq = MasterObj.ORD1201List.Count == 0 ? 1 : MasterObj.ORD1201List.Max(o => o.Seq) + 1;
                DetailObj.ItemCode = CallData.ItemCode;
                DetailObj.LotNo = CallData.LotNo;
                DetailObj.OutDate = DateTime.Today;
                DetailObj.OutQty = CallData.Stockqty;

                DetailGridBindingSource.Add(DetailObj);
                MasterObj.ORD1201List.Add(DetailObj);

                //ModelService.InsertChild<TN_ORD1201>(DetailObj);
                //masterreview();

                DetailGridExControl.BestFitColumns();
            }
        }

        private void AddOrderList(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            List<TN_ORD1100> partList = (List<TN_ORD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnedPart in partList)
            {
                TN_ORD1200 newobj = new TN_ORD1200();
                newobj.OutNo = DbRequestHandler.GetRequestNumber("QM");
                newobj.DelivSeq = returnedPart.DelivSeq;
                newobj.OrderNo = returnedPart.OrderNo;
                newobj.OrderSeq = Convert.ToInt32(returnedPart.Seq);
                newobj.CustCode = returnedPart.Temp;
                newobj.ItemCode = returnedPart.ItemCode;
                newobj.OutDate = DateTime.Today;
                newobj.OrderQty = returnedPart.DelivQty;
                newobj.OutId = returnedPart.DelivId;
                newobj.Memo = returnedPart.Memo;

                // 20220219 오세완 차장 품명 출력을 추가하여 로직 추가 
                TN_STD1100 std1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == returnedPart.ItemCode &&
                                                                                p.UseYn == "Y").FirstOrDefault();
                if (std1100 != null)
                    newobj.TN_STD1100 = std1100;
              
                MasterGridBindingSource.Add(newobj);
                ModelService.Insert(newobj);
            }

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            TN_ORD1201 delobj = DetailGridBindingSource.Current as TN_ORD1201;
            if (obj == null || delobj == null) return;

            obj.ORD1201List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            //masterreview();
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 출하라벨출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_ORD1201>;
                if (PrintList.Count == 0) return;

                var FirstReport = new REPORT.ROUTLABLE();
                foreach (var v in PrintList.OrderByDescending(p => p.CreateTime).ToList())
                {
                    PRT_OUTLABLE prt = new PRT_OUTLABLE()
                    {
                        ItemCode = v.ItemCode,
                        ItemNm = DbRequestHandler.GetCellValue("SELECT ITEM_NM FROM TN_STD1100T WHERE ITEM_CODE='" + v.ItemCode + "'", 0),
                        PrtDate = v.OutDate.ToString().Substring(0, 10),
                        Qty = v.OutQty,
                        LotNo = v.OutNo,
                        CustLotNo = obj.Memo
                    };
                    var report = new REPORT.ROUTLABLE(prt);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);
                }
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();//.Print();
                //DataLoad();
                DetailGridExControl.MainGrid.MainView.RefreshData();

                if (DialogResult.Yes == MessageBox.Show("거래명세서를 출력하시겠습니까?", "", MessageBoxButtons.YesNo))
                {
                    TN_ORD2000 tn = new TN_ORD2000()
                    {
                        OutprtNo = DbRequestHandler.GetRequestNumber("PRTOUT"),
                        OutprtDate = DateTime.Today,
                        CustCode = obj.CustCode
                    };

                    foreach (var v in PrintList.OrderByDescending(p => p.CreateTime).ToList())
                    {
                        TN_ORD2001 newobj = new TN_ORD2001()
                        {
                            OutprtNo = tn.OutprtNo,
                            Seqprt = tn.ORD2001List.Count == 0 ? 1 : tn.ORD2001List.OrderBy(o => o.Seqprt).LastOrDefault().Seqprt + 1,
                            ItemCode = v.ItemCode,
                            OutQty = v.OutQty,
                            // 2022-07-05 김진우 판매계획관리의 단가액 추가
                            OutPrice = ModelService.GetChildList<TN_ORD1002>(p => p.OrderNo == obj.OrderNo && p.ItemCode == obj.ItemCode).FirstOrDefault() != null ? ModelService.GetChildList<TN_ORD1002>(p => p.OrderNo == obj.OrderNo && p.ItemCode == obj.ItemCode).FirstOrDefault().Cost : 0
                        };
                        tn.ORD2001List.Add(newobj);
                    }
                    ModelService.InsertChild<TN_ORD2000>(tn);
                    ModelService.Save();
                    REPORT.XRORD2001 report1 = new REPORT.XRORD2001(tn.CustCode, tn.OutprtNo);
               //     report1.DataSource = ModelService.GetChildList<TN_ORD2001>(p => p.OutprtNo == tn.OutprtNo);
                    report1.CreateDocument();


                    report1.ShowPrintStatusDialog = false;
                    report1.ShowPreview();
                }
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        private void masterreview()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            if (obj.ORD1201List.Count() > 0)
            {
                //obj.OutDate = obj.ORD1201List.OrderByDescending(p => p.OutDate).FirstOrDefault().OutDate;
                obj.OutQty = obj.ORD1201List.Sum(s => s.OutQty).GetDecimalNullToZero();
            }
            else
            {
                //obj.OutDate = null;         // 이것때문에 디테일 삭제시 조회가 안됌
                obj.OutQty = 0;
            }
            MasterGridExControl.MainGrid.BestFitColumns();
        }

    }
}
