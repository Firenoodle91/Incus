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
using HKInc.Service.Handler;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.PUR
{
    /// <summary>
    /// 자재입고관리
    /// </summary>
    public partial class XFPUR1300 :  HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1300> ModelService = (IService<TN_PUR1300>)ProductionFactory.GetDomainService("TN_PUR1300");

        public XFPUR1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += DetailView_CellValueChanged;

            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-10);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+10);
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "발주내역참조");
            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.MainGrid.AddColumn("InputNo", "입고번호");
            MasterGridExControl.MainGrid.AddColumn("InputDate", "입고일");
            MasterGridExControl.MainGrid.AddColumn("InputId", "입고자");
            MasterGridExControl.MainGrid.AddColumn("ReqNo", "발주번호");
            MasterGridExControl.MainGrid.AddColumn("ReqDate", "발주일");
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", "거래처");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("Temp1", "입고완료");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InputDate", "InputId", "CustomerCode", "Memo", "Temp1");

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.Export, false);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "발주내역참조");
            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("InputNo", false);
            DetailGridExControl.MainGrid.AddColumn("InputSeq", "입고순번");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드"); // 20220217 오세완 차장 공통사항으로 수정 요창한 것 수정
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목"); // 20220217 오세완 차장 공통사항으로 수정 요창한 것 수정
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품명"); // 20220217 오세완 차장 공통사항으로 수정 요창한 것 수정
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            DetailGridExControl.MainGrid.AddColumn("ReqNo", "발주번호");
            DetailGridExControl.MainGrid.AddColumn("ReqSeq", "발주순번");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            DetailGridExControl.MainGrid.AddColumn("ReqQty", "발주수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Cost","발주단가", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("ReqAmt","발주금액", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("InputQty","입고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("InCost","입고단가", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("InputAmt","입고금액", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("Lqty", "라벨수", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("InYn",false);
            DetailGridExControl.MainGrid.AddColumn("Temp2", "LOTNO");
            DetailGridExControl.MainGrid.AddColumn("WhCode", "창고");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "위치코드");
            DetailGridExControl.MainGrid.AddColumn("Memo");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "ItemCode", "InputQty", "InCost", "Memo", "Temp2", "WhCode", "Lqty", "WhPosition");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InputId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("InputDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("ReqDate");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp1", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            // 20220217 오세완 차장 품목코드, 품명, 품번을 전부 출력ㅎ게 수정하기 때문에 필요가 없어 보임                 2022-03-10 김진우 직접입력으로 변경되어서 다시 수정      마지막 "ItemCode"는 "ItemNm"에서 변경
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && (p.TopCategory == "P03" || p.TopCategory == "P02" || p.TopCategory == "P07" || p.TopCategory == "P06")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("InYn", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "WhPositionCode", "WhPositionName");          // 2021-11-04 김진우 주임 수정

            // 2022-01-26 김진우 추가
            var WhPositionEdit = DetailGridExControl.MainGrid.Columns["WhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            WhPositionEdit.Popup += WhPositionEdit_Popup;
        }

        /// <summary>
        /// 2022-03-16 김진우 추가
        /// 창고 변경시 위치코드 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DetailView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView GV = sender as GridView;
            TN_PUR1301 DetailObj = DetailGridBindingSource.Current as TN_PUR1301;

            if (e.Column.Name == "WhCode")
                DetailObj.WhPosition = "" ;

            if (e.Column.Name == "ItemCode")                        // 2022-03-31 김진우   디테일에서 품목코드 추가시 제품정보 표시
            {
                string Item = GV.GetFocusedRowCellValue(GV.Columns["ItemCode"]).ToString();
                TN_STD1100 STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == Item).FirstOrDefault();

                DetailObj.TN_STD1100 = STD1100;
                DetailGridExControl.BestFitColumns();
            }
        }

        /// <summary>
        /// 2022-01-26 김진우 추가
        /// 창고 선택시 위치 변경되도록 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WhPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_PUR1301;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.WhCode + "'";
        }

        protected override void InitCombo()
        {
            lupcust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void DataLoad()
        {
            #region grid focus 불러오기
            //GridRowLocator.GetCurrentRow();
            GridRowLocator.GetCurrentRow("InputNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1)); // 20220217 오세완 차장 그리드포커스 기능 추가 
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            string cust = lupcust.EditValue.GetNullToEmpty();
            string inputNo = tx_ReqNo.Text.GetNullToEmpty();
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();      // 2022-01-20 김진우 대리 추가

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.InputDate >= dp_date.DateFrEdit.DateTime.Date && p.InputDate <= dp_date.DateToEdit.DateTime.Date)
                                                                        && (string.IsNullOrEmpty(cust) ? true : p.CustomerCode == cust) 
                                                                        && (string.IsNullOrEmpty(inputNo) ? true : p.InputNo == inputNo)
                                                                        ).OrderBy(o => o.InputDate).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();

            #region grid focus 설정
            GridRowLocator.SetCurrentRow();
            #endregion
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;
            DetailGridBindingSource.DataSource = obj.PUR1301List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            TN_PUR1300 newobj = new TN_PUR1300()
            {
                InputNo = DbRequestHandler.GetRequestNumber("IN"),
                InputDate = DateTime.Today,
                Temp1 = "N"
            };
            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
        }

        protected override void DeleteRow()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;
          
            if (obj.PUR1301List.Count>= 1)
                MessageBox.Show("상세내역이 있어 삭제할수 없습니다.");
            else
            {
                if (obj.Temp1 == "Y")
                    MessageBox.Show("입고완료 삭제할수 없습니다.");
                else
                {
                    MasterGridBindingSource.Remove(obj);            // 2022-04-01 김진우 변경
                    //MasterGridBindingSource.RemoveCurrent();
                    ModelService.Delete(obj);
                }
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;

            if (obj.Temp1 == "Y")
                MessageBox.Show("입고완료건은 변경할수 없습니다.");
            else
            {
                TN_PUR1301 newobj = new TN_PUR1301()
                {
                    InputNo=obj.InputNo,
                    InputSeq= obj.PUR1301List.Count == 0? 1: obj.PUR1301List.Count + 1,
                    ReqNo=obj.ReqNo,
                    Temp2= obj.InputNo.ToString()+ (obj.PUR1301List.Count == 0 ? 1 : obj.PUR1301List.Count + 1).ToString()
                };
                DetailGridBindingSource.Add(newobj);
                obj.PUR1301List.Add(newobj);
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            TN_PUR1301 DetailObj = DetailGridBindingSource.Current as TN_PUR1301;       // 디테일값이 없어서 추가     2022-07-22 김진우 추가
            if (obj == null || DetailObj == null) return;

            if (obj.Temp1 == "Y" || ModelService.GetChildList<TN_PUR1501>(p => true).Any(a => a.Temp2 == DetailObj.Temp2))  // 출고된 건에 대해 삭제가 되어 추가      2022-07-27 김진우
                MessageBoxHandler.Show("발주확정건이나 출고된 제품은 삭제할 수 없습니다.");
            //MessageBox.Show("발주확정건은 삭제할수 없습니다.");
            else
            {
                TN_PUR1301 dtlobj = DetailGridBindingSource.Current as TN_PUR1301;
                DetailGridBindingSource.RemoveCurrent();
                obj.PUR1301List.Remove(dtlobj);
            }
        }

        protected override void FileChooseClicked()
        {
            DataLoad();
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
           
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR1100, param, AddPur1300);
            form.ShowPopup(true);
        }

        private void AddPur1300(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            List<TN_PUR1100> partList = (List<TN_PUR1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnedPart in partList)
            {
                if (ModelService.GetList(p => p.ReqNo == returnedPart.ReqNo).Count == 0)
                {
                    TN_PUR1300 obj = (TN_PUR1300)MasterGridBindingSource.AddNew();
                    obj.InputNo = DbRequestHandler.GetRequestNumber("IN");
                    obj.InputDate = DateTime.Today;
                    obj.CustomerCode = returnedPart.CustomerCode;
                    obj.DueDate = returnedPart.DueDate;
                    obj.ReqNo = returnedPart.ReqNo;
                    obj.ReqDate = returnedPart.ReqDate;

                    ModelService.Insert(obj);
                }
            }

            if (partList.Count > 0) SetIsFormControlChanged(true);
            MasterGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DetailFileChooseClicked()
        {
            TN_PUR1300 obj = MasterGridBindingSource.Current as TN_PUR1300;
            if (obj == null) return;
            if (obj.ReqNo.GetNullToEmpty() == "") return;
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Constraint, "Final");
            param.SetValue(PopupParameter.Value_1, obj.ReqNo);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECTPUR1200, param, AddPur1301);
            form.ShowPopup(true);
        }

        private void AddPur1301(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;
            List<TN_PUR1200> partList = (List<TN_PUR1200>)e.Map.GetValue(PopupParameter.ReturnObject);
            TN_PUR1300 oldobj = MasterGridBindingSource.Current as TN_PUR1300;

            foreach (var returnedPart in partList)
            {
                if (oldobj.PUR1301List.Where(p => p.ItemCode == returnedPart.ItemCode).ToList().Count == 0)
                {
                    TN_PUR1301 obj = (TN_PUR1301)DetailGridBindingSource.AddNew();
                    obj.InputNo = oldobj.InputNo;
                    obj.InputSeq = oldobj.PUR1301List.Count == 0 ? 1 : oldobj.PUR1301List.OrderBy(o => o.InputSeq).LastOrDefault().InputSeq + 1;
                    obj.ItemCode = returnedPart.ItemCode;
                    obj.ReqNo = returnedPart.ReqNo;
                    obj.ReqSeq = returnedPart.ReqSeq;
                    obj.ReqQty = returnedPart.ReqQty;
                    obj.InputQty = returnedPart.ReqQty; // 20220217 오세완 차장 사업관리시스템 개선사항 요청대로 수정 
                    obj.Cost = returnedPart.Temp1;       
                    obj.InCost = returnedPart.Temp1;    // 2022-06-21 김진우 추가
                    obj.Memo = returnedPart.Memo;
                    obj.Temp2 = oldobj.InputNo.ToString() + obj.InputSeq.ToString();

                    // 20220217 오세완 차장 디테일 출력 방식이 변경되어 추가 
                    TN_STD1100 std1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == returnedPart.ItemCode &&
                                                                                    p.UseYn == "Y").FirstOrDefault();
                    if (std1100 != null)
                        obj.TN_STD1100 = std1100;

                    oldobj.PUR1301List.Add(obj);
                }
            }
            if (partList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            for (int rowHandle = 0; rowHandle < DetailGridExControl.MainGrid.MainView.RowCount; rowHandle++)
            {
                string inqty = Convert.ToString(DetailGridExControl.MainGrid.MainView.GetRowCellValue(rowHandle, "InputQty").GetNullToEmpty());
                string ItemCode = Convert.ToString(DetailGridExControl.MainGrid.MainView.GetRowCellValue(rowHandle, "ItemCode").GetNullToEmpty());      // 2022-01-20 김진우 대리 추가
                if (inqty == "0" || inqty == "") { HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(40)); return; }
                if (ItemCode == "0" || ItemCode == "") { MessageBoxHandler.Show("품목을 선택해주세요."); return; }                                        // 2022-01-20 김진우 대리 추가
            }

            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            ModelService.Save();
         
            DataLoad();
        }

        /// <summary>
        /// 현품표 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();

                // 입고수량 체크
                for (int rowHandle = 0; rowHandle < DetailGridExControl.MainGrid.MainView.RowCount; rowHandle++)
                {
                    string inqty = Convert.ToString(DetailGridExControl.MainGrid.MainView.GetRowCellValue(rowHandle, "InputQty").GetNullToEmpty());
                    if (inqty == "0" || inqty == "") { HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(40)); return; }
                }

                // 입고 상세목록 검사
                if (DetailGridBindingSource == null || DetailGridBindingSource.Count == 0)
                {
                    MessageBoxHandler.Show("입고상세목록이 존재하지 않습니다.");
                    return;
                }

                // 체크 여부 확인
                var PrintList = DetailGridBindingSource.List as List<TN_PUR1301>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0)
                {
                    MessageBoxHandler.Show("입고목록을 선택해 주세요.");
                    return;
                }

                // 버튼 클릭시 자동으로 저장되는것 수정
                DialogResult result = MessageBoxHandler.Show("현품표 출력시 입고 상세목록이 저장됩니다. \n출력하시겠습니까?", LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.OK)
                    ModelService.Save();
                else
                    return;

                var FirstReport = new REPORT.RINPUTLABLE();
                foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
                {
                    for (int j = 0; j < (v.Lqty == 0 ? 1 : v.Lqty); j++)
                    {
                        var report = new REPORT.RINPUTLABLE(v);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);
                    }
                    v._Check = "N";
                }
                DataLoad();
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();
            }
            catch (Exception ex)
            { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            { WaitHandler.CloseWait(); }
        }
    }
}

