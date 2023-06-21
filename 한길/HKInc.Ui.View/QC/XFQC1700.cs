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
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// 클레임접수화면
    /// </summary>
    public partial class XFQC1700 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1700> ModelService = (IService<TN_QCT1700>)ProductionFactory.GetDomainService("TN_QCT1700");
        public XFQC1700()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dp_dt.DateToEdit.DateTime = DateTime.Today;
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("ResultDate", "접수일");
            GridExControl.MainGrid.AddColumn("ClaimNo","접수번호");
            GridExControl.MainGrid.AddColumn("Seq",false);
          
            GridExControl.MainGrid.AddColumn("CustCode","고객사");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");

            GridExControl.MainGrid.AddColumn("PLotno","생산 LOT NO");
            GridExControl.MainGrid.AddColumn("OutLotno","출고 LOT NO");
            GridExControl.MainGrid.AddColumn("CalimQty","수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("CalimType","유형");
            GridExControl.MainGrid.AddColumn("ClaimId","접수자"); // 10
            GridExControl.MainGrid.AddColumn("ClaimMemo","내용");
            GridExControl.MainGrid.AddColumn("ClaimFile", "파일");
            GridExControl.MainGrid.AddColumn("ClaimFiledata", "첨부이미지", false);
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            GridExControl.MainGrid.SetRepositoryItemPictureEdit("ClaimFiledata");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CalimType", DbRequesHandler.GetCommCode(MasterCodeSTR.QCFAIL), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ClaimId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.MainView.Columns["ClaimMemo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("ClaimNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            var CustomerName = tx_custnm.EditValue.GetNullToEmpty();
            var ItemCodeName = textItemCodeName.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(CustomerName) ? true : p.TN_STD1400.CustomerName.Contains(CustomerName))
                                                                 && (string.IsNullOrEmpty(ItemCodeName) ? true : (p.TN_STD1100.ItemNm1.Contains(ItemCodeName) || p.TN_STD1100.ItemNm.Contains(ItemCodeName)))
                                                                 && (p.ResultDate >= dp_dt.DateFrEdit.DateTime
                                                                 && p.ResultDate <= dp_dt.DateToEdit.DateTime)
                                                                )
                                                                .OrderBy(p => p.ResultDate)
                                                                .ToList();
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion
            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        protected override void DeleteRow()
        {
            TN_QCT1700 obj = GridBindingSource.Current as TN_QCT1700;
            GridBindingSource.RemoveCurrent();
            ModelService.Delete(obj);
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFQCT1700, param, PopupRefreshCallback);

            form.ShowPopup(true);
        }

        protected override void GridRowDoubleClicked()
        {
            TN_QCT1700 obj = GridBindingSource.Current as TN_QCT1700;
            if (obj == null) return;
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.KeyValue, obj);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.Update);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFQCT1700, param, PopupRefreshCallback);

            form.ShowPopup(true);

        }
        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name.ToString() != "ClaimFile") return;
            TN_QCT1700 obj = GridBindingSource.Current as TN_QCT1700;
            Form temp = new Form();
            temp.BackgroundImage = FileHandler.FtpImageLoad(GlobalVariable.HTTP_SERVER + obj.ClaimFile);
            temp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            temp.ShowDialog();
        }
    }
}