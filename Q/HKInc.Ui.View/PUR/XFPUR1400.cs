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
using DevExpress.XtraReports.UI;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.PUR
{
    /// <summary>
    /// 자재출고관리
    /// </summary>
    public partial class XFPUR1400 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_PUR1500> ModelService = (IService<TN_PUR1500>)ProductionFactory.GetDomainService("TN_PUR1500");
        public XFPUR1400()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
         
            dp_date.DateFrEdit.DateTime = DateTime.Today.AddDays(-30);
            dp_date.DateToEdit.DateTime = DateTime.Today.AddDays(+30);

            MasterGridExControl.MainGrid.MainView.CellValueChanged += Master_CellValueChanged;          // 2022-03-18 김진우 추가
            DetailGridExControl.MainGrid.MainView.CellValueChanged += Detail_CellValueChanged;
        }

        protected override void InitCombo()
        {
            lupUser.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("OutNo", "출고번호");
            MasterGridExControl.MainGrid.AddColumn("OutDate", "출고일");
            MasterGridExControl.MainGrid.AddColumn("ItemCode", "품목코드");              // 2022-03-17 김진우 수정
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "생산제품");            // 2022-03-17 김진우 추가
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");          // 2022-03-17 김진우 추가
            MasterGridExControl.MainGrid.AddColumn("OutId", "출고자");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutDate", "ItemCode", "OutId", "Memo");

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn("_Check", "선택");
            DetailGridExControl.MainGrid.AddColumn("OutNo", false);
            DetailGridExControl.MainGrid.AddColumn("OutSeq", "순번");
            DetailGridExControl.MainGrid.AddColumn("Temp1", "출고Lot");
            DetailGridExControl.MainGrid.AddColumn("ItemCode", "품목코드"); // 20220217 오세완 차장 공통사항으로 수정 요창한 것 수정
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목"); // 20220217 오세완 차장 공통사항으로 수정 요창한 것 수정
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품명"); // 20220217 오세완 차장 공통사항으로 수정 요창한 것 수정
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", "대분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", "중분류");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            DetailGridExControl.MainGrid.AddColumn("Temp2", "입고Lot");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", "단위");
            //DetailGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "#,###.##");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "출고수량", HorzAlignment.Far, FormatType.Numeric, "#,##0.##"); // 20220429 오세완 차장 0.nn단위로 출고할 일은 없겠지만 그게 출력이 되지 않아서 수정 
            DetailGridExControl.MainGrid.AddColumn("WhCode", "창고");
            DetailGridExControl.MainGrid.AddColumn("WhPosition", "위치코드");
            DetailGridExControl.MainGrid.AddColumn("Memo");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "Temp2", "OutQty", "Memo", "WhCode", "WhPosition");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<UserView>(p=>p.Active=="Y").ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y" && p.TopCategory != "P03").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemCode");                 // 2022-02-17 김진우 수정
            //MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate", Utils.Enum.DateFormat.DateAndTime, 130); // 20220502 오세완 차장 이차장 요구로 시간까지 출력으로 변경 
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 1), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.itemtype, 2), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            // 20220217 오세완 차장 품목코드, 품명, 품번을 전부 출력ㅎ게 수정하기 때문에 필요가 없어 보임
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p=>p.UseYn=="Y"&&(p.TopCategory=="P03"|| p.TopCategory == "P02" || p.TopCategory == "P07" || p.TopCategory == "P06")).OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");         // 2022-02-17 김진우 수정
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "WhPositionCode", "WhPositionName");          // 2021-11-04 김진우 주임 수정

            // 2022-03-18 김진우 추가
            RepositoryItemSearchLookUpEdit WhPositionEdit = DetailGridExControl.MainGrid.Columns["WhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            WhPositionEdit.Popup += WhPositionEdit_Popup;

        }
               
        protected override void DataLoad()
        {
            ModelService.ReLoad();

            #region grid focus 불러오기
            GridRowLocator.GetCurrentRow("OutNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1)); // 20220217 오세완 차장 그리드포커스 기능 추가 
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            string luser = lupUser.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dp_date.DateFrEdit.DateTime.Date && p.OutDate <= dp_date.DateToEdit.DateTime.Date)
                                                                        && (string.IsNullOrEmpty(luser) ? true : p.OutId == luser)).OrderBy(o => o.OutDate).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.MainGrid.BestFitColumns();

            #region grid focus 설정
            GridRowLocator.SetCurrentRow();
            #endregion
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;
            //DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_PUR1501>()
            DetailGridBindingSource.DataSource = obj.PUR1501List.ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            TN_PUR1500 newobj = new TN_PUR1500()
            {
                OutNo = DbRequestHandler.GetRequestNumber("OUT"),
                OutDate = DateTime.Today
            };

            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
        }

        protected override void DetailAddRowClicked()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;

            int seq = (obj.PUR1501List.Count == 0) ? 1 : obj.PUR1501List.OrderBy(o => o.OutSeq).LastOrDefault().OutSeq + 1;
            TN_PUR1501 newobj = new TN_PUR1501()
            {
                OutNo = obj.OutNo,
                OutSeq = seq,
                Temp1 = obj.OutNo+ seq.ToString()
            };

            DetailGridBindingSource.Add(newobj);
            obj.PUR1501List.Add(newobj);
        }

        protected override void DeleteRow()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;
            if (obj.PUR1501List.Count >= 1)
            {
                MessageBox.Show("상세내역이 있어 삭제할수 없습니다");
            }
            else
            {
                    MasterGridBindingSource.RemoveCurrent();
                    ModelService.Delete(obj);
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_PUR1500 obj = MasterGridBindingSource.Current as TN_PUR1500;
            if (obj == null) return;
         
            TN_PUR1501 dtlobj = DetailGridBindingSource.Current as TN_PUR1501;
            DetailGridBindingSource.RemoveCurrent();
            obj.PUR1501List.Remove(dtlobj);
        }

        protected override void DataSave()
        {
            foreach (var rowHandle in gridEx2.MainGrid.MainView.GetSelectedRows())
            {
                string _inlot = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty());
                string _outqty = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "OutQty").GetNullToEmpty();

                if (_inlot == null || _inlot == "")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("출고등록" + Convert.ToInt32(rowHandle + 1) + "행의 입고LOT는 필수입력 사항입니다.");
                    return;
                }

                if (_outqty == null || _outqty == ""||_outqty=="0")
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("출고등록 " + Convert.ToInt32(rowHandle + 1) + "행의 출고수량은 필수입력 사항입니다.");
                    return;
                }
            }

            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var rowHandle in gridEx2.MainGrid.MainView.GetSelectedRows())
                {
                    string _inlot = Convert.ToString(gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "Temp2").GetNullToEmpty());
                    string _outqty = gridEx2.MainGrid.MainView.GetRowCellValue(rowHandle, "OutQty").GetNullToEmpty();

                    if (_inlot == null || _inlot == "")
                    {
                        HKInc.Service.Handler.MessageBoxHandler.Show("출고등록" + Convert.ToInt32(rowHandle + 1) + "행의 입고LOt는 필수입력 사항입니다.");
                        return;
                    }

                    if (_outqty == null || _outqty == "" || _outqty == "0")
                    {
                        HKInc.Service.Handler.MessageBoxHandler.Show("출고등록 " + Convert.ToInt32(rowHandle + 1) + "행의 출고수량은 필수입력 사항입니다.");
                        return;
                    }
                }

                MasterGridExControl.MainGrid.PostEditor();
                MasterGridBindingSource.EndEdit();
                DetailGridExControl.MainGrid.PostEditor();
                DetailGridBindingSource.EndEdit();
                ModelService.Save();

                WaitHandler.ShowWait();

                if (DetailGridBindingSource == null) return;
                var PrintList = DetailGridBindingSource.List as List<TN_PUR1501>;
                if (PrintList.Where(p => p._Check == "Y").ToList().Count == 0) return;
                ModelService.Save();
                var FirstReport = new REPORT.RINPUTLABLE();
                foreach (var v in PrintList.Where(p => p._Check == "Y").OrderByDescending(p => p.CreateTime).ToList())
                {
                    var report = new REPORT.RINPUTLABLE(v);
                    report.CreateDocument();
                    FirstReport.Pages.AddRange(report.Pages);
                    v._Check = "N";
                }
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();
                DetailGridExControl.MainGrid.MainView.RefreshData();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally { WaitHandler.CloseWait(); }
        }

        /// <summary>
        /// 2022-03-17 김진우 마스터 그리드 제품 선택시 정보 표시기능 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Master_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "ItemCode")
            {
                TN_PUR1500 MasterObj = MasterGridBindingSource.Current as TN_PUR1500;
                TN_STD1100 STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == MasterObj.ItemCode).FirstOrDefault();

                MasterObj.TN_STD1100 = STD1100;
                MasterGridExControl.BestFitColumns();
            }
        }

        private void Detail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_PUR1501 DetailObj = DetailGridBindingSource.Current as TN_PUR1501;

            if (e.Column.Name == "Temp2")       // 입고 LOT
            {
                string outf = DbRequestHandler.GetCellValue("exec SP_STOPINOUT '" + DetailObj.Temp2 + "'", 0);
                if (outf == "Y")
                {
                    MessageBox.Show("이전 LOT가 있습니다. 선입선출 확인하세요");
                }
                VI_PURSTOCK_LOT stock = ModelService.GetChildList<VI_PURSTOCK_LOT>(p => p.Temp2 == DetailObj.Temp2).OrderBy(o => o.ItemCode).FirstOrDefault();
                if (stock == null)
                {
                    MessageBox.Show("정보가 없습니다.");
                    DetailObj.ItemCode = null;
                    DetailObj.Temp2 = null;
                }
                else
                {
                    // 2022-03-17 김진우 디테일그리드 LOT NO 입력시 정보 표시기능 추가
                    TN_STD1100 STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == stock.ItemCode).FirstOrDefault();
                    if (STD1100 != null)
                    {
                        DetailObj.TN_STD1100 = STD1100;
                        DetailObj.ItemCode = stock.ItemCode;
                    }

                    // 20220217 오세완 차장 품목코드 / 품목 / 품명 출력 방법이 변경되어 추가 하려 했으나 있어도 저장전에는 출력이 안되서 그냥 생략
                    //TN_STD1100 std1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == stock.ItemCode &&
                    //                                                                p.UseYn == "Y").FirstOrDefault();
                    //if (std1100 != null)
                    //    obji.TN_STD1100 = std1100;
                }
            }

            if (e.Column.Name == "OutQty")
            {
                if (DetailObj.Temp2 == null)
                {
                    DetailObj.OutQty = 0;
                    MessageBox.Show("입고 LOT는 필수입력값입니다.");
                }
                else
                {
                    VI_PURSTOCK_LOT stock = ModelService.GetChildList<VI_PURSTOCK_LOT>(p => p.ItemCode == DetailObj.ItemCode && p.Temp2 == DetailObj.Temp2).OrderBy(o => o.ItemCode).FirstOrDefault();
                    if (stock == null)
                    {
                        MessageBox.Show("해당 품고에 대한 입고LOT " + DetailObj.Temp2 + "정보가 없습니다.");
                        DetailObj.Temp2 = null;
                        DetailObj.OutQty = null;
                    }
                    else
                    {
                        if (stock.StockQty < DetailObj.OutQty)
                        {
                            MessageBox.Show("재고량이 부족하여 출고할수 없습니다.");
                            DetailObj.OutQty = stock.StockQty <= 0 ? 0 : stock.StockQty;
                        }
                        else
                        {
                            DetailObj.WhCode = stock.WhCode;
                            DetailObj.WhPosition = stock.WhPosition;
                        }
                    }
                }
            }

            if (e.Column.Name == "WhCode")
                DetailObj.WhPosition = "";

            DetailGridExControl.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 2022-03-18 김진우 추가
        /// 창고선택시 위치코드 자동으로 되게끔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WhPositionEdit_Popup(object sender, EventArgs e)
        {
            TN_PUR1501 DetailObj = DetailGridBindingSource.Current as TN_PUR1501;
            SearchLookUpEdit LU = sender as SearchLookUpEdit;
            if (DetailObj == null || LU == null) return;

            if (DetailObj.WhCode == null)
                LU.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = ''";
            else
                LU.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + DetailObj.WhCode + "'";
        }
    }
}
