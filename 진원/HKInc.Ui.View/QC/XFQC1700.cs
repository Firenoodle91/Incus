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
using System.IO;

namespace HKInc.Ui.View.QC
{
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

     

        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.Name.ToString() != "ClaimFiledata") return;
            TN_QCT1700 obj = GridBindingSource.Current as TN_QCT1700;
            //Form temp = new Form();
            ////  temp.Size = new Size(500, 500);
            //temp.BackgroundImage= FileHandler.FtpImageLoad(GlobalVariable.HTTP_SERVER+ obj.ClaimFile);
            //temp.BackgroundImageLayout= System.Windows.Forms.ImageLayout.Stretch;
            //temp.ShowDialog();

            GridView gv = (GridView)sender;

            String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns["ClaimFile"]).GetNullToNull();

            if(filename != null)
            {
                File.WriteAllBytes(filename, (byte[])gv.GetRowCellValue(e.RowHandle, gv.Columns["ClaimFiledata"]));
                HKInc.Service.Handler.FileHandler.StartProcess(filename);
            }
        }

        protected override void InitCombo()
        {
            lup_Customer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(x => x.UseFlag == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("ResultDate", "접수일");
            GridExControl.MainGrid.AddColumn("ClaimNo","관리번호");
            GridExControl.MainGrid.AddColumn("Seq",false);
          
            GridExControl.MainGrid.AddColumn("CustCode","고객사");
            GridExControl.MainGrid.AddColumn("ItemCode","품목코드");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품목명");
            GridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            GridExControl.MainGrid.AddColumn("PLotno","제품LOT");
            GridExControl.MainGrid.AddColumn("OutLotno","출고LOT");
            GridExControl.MainGrid.AddColumn("CalimQty","수량");
            GridExControl.MainGrid.AddColumn("CalimType","유형");
            GridExControl.MainGrid.AddColumn("ClaimId","접수자"); // 10
            GridExControl.MainGrid.AddColumn("ClaimMemo","내용");
            GridExControl.MainGrid.AddColumn("ClaimFile", false);
            GridExControl.MainGrid.AddColumn("ClaimFiledata", "첨부이미지");
           // GridExControl.MainGrid.AddColumn("image");

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            GridExControl.MainGrid.SetRepositoryItemPictureEdit("ClaimFiledata");
            //GridExControl.MainGrid.SetRepositoryItemPictureEdit("image");


            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CustCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
           // GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseYn == "Y").OrderBy(o => o.ItemNm).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CalimType", DbRequesHandler.GetCommCode(MasterCodeSTR.QCFAIL), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ClaimId", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), "LoginId", "UserName");

            GridExControl.MainGrid.MainView.Columns["ClaimMemo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(gridEx1, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string customCode = lup_Customer.EditValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList();
            GridBindingSource.DataSource = ModelService.GetList(p => (p.ResultDate >= dp_dt.DateFrEdit.DateTime && p.ResultDate <= dp_dt.DateToEdit.DateTime)
                                                                //&& customCode.IsNullOrEmpty() == true ? true : p.CustCode == customCode
                                                                    && string.IsNullOrEmpty(customCode) ? true : p.CustCode == customCode
                                                                )
                                                        .OrderBy(p => p.ResultDate)
                                                        .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
           // image();
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
        private void image()
        {
            GridView View = GridExControl.MainGrid.MainView as GridView;
            for (int i = 0; i < View.RowCount; i++)
            {
                string category = View.GetRowCellDisplayText(i, View.Columns["ClaimFile"]);
                View.SetRowCellValue(i,View.Columns["ClaimFiledata"], FileHandler.FtpImageToByte(category));
            }
            
        }
    protected override void GridRowDoubleClicked()
        {
           
                TN_QCT1700 obj = GridBindingSource.Current as TN_QCT1700;
            if (obj == null) return;
           
                //PopupDataParam param = new PopupDataParam();
                //param.SetValue(PopupParameter.Service, ModelService);
                //param.SetValue(PopupParameter.KeyValue, obj);
                //param.SetValue(PopupParameter.UserRight, UserRight);
                //param.SetValue(PopupParameter.EditMode, PopupEditMode.Update);

                //IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFOrd1000, param, PopupRefreshCallback);

                //form.ShowPopup(true);
                //if (!UserRight.HasEdit) return;

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Service, ModelService);
                param.SetValue(PopupParameter.KeyValue, obj);
                param.SetValue(PopupParameter.UserRight, UserRight);
                param.SetValue(PopupParameter.EditMode, PopupEditMode.Update);

                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFQCT1700, param, PopupRefreshCallback);

                form.ShowPopup(true);
            
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
        //protected override IPopupForm GetPopupForm(PopupDataParam param)
        //{
        //    return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFQCT1700, param, PopupRefreshCallback);
        //}

        //protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        //{
        //    param.SetValue(PopupParameter.Service, ModelService);
        //    return param;
        //}
    }
}