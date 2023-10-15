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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;
using System.IO;
using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;


namespace HKInc.Ui.View.View.MOLD
{
    //금형입출고관리

    public partial class XFMOLD1300 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_MOLD1100> ModelService = (IService<TN_MOLD1100>)ProductionFactory.GetDomainService("TN_MOLD1100");

        public XFMOLD1300()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
        }

       

        protected override void InitCombo()
        {
            lup_MoldCode.SetDefault(true, "MoldMCode", DataConvert.GetCultureDataFieldName("MoldName"), ModelService.GetChildList<TN_MOLD1100>(p => p.UseYN == "Y").ToList());
        }


        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMCode"), LabelConvert.GetLabelText("MoldMCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCode"), LabelConvert.GetLabelText("MoldCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldName"), LabelConvert.GetLabelText("MoldName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMakerCust"), LabelConvert.GetLabelText("MoldMakerCust"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldWhCode"), LabelConvert.GetLabelText("MoldMakerCust"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldWhPosition"), LabelConvert.GetLabelText("MoldWhPosition"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Memo"), LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldClass"), LabelConvert.GetLabelText("MoldClass"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldFileName"), LabelConvert.GetLabelText("MoldFileName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldFileUrl"), LabelConvert.GetLabelText("MoldFileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCheckFileName"), LabelConvert.GetLabelText("MoldCheckFileName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCheckFileUrl"), LabelConvert.GetLabelText("MoldCheckFileUrl"), false);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MOLD1100>(MasterGridExControl);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMcode"), LabelConvert.GetLabelText("MoldMcode"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCode"), LabelConvert.GetLabelText("MoldCode"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Seq"), LabelConvert.GetLabelText("Seq"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Division"), LabelConvert.GetLabelText("Division"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("InOutDate"), LabelConvert.GetLabelText("InOutDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldOutWhCode"), LabelConvert.GetLabelText("OutWhCode"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldOutWhPosition"), LabelConvert.GetLabelText("MoldOutWhPosition"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldInWhCode"), LabelConvert.GetLabelText("InWhCode"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldInWhPosition"), LabelConvert.GetLabelText("MoldInWhPosition"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("InOutId"), LabelConvert.GetLabelText("InOutId"));
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MOLD1300>(DetailGridExControl);

            //DetailGridExControl.MainGrid.AddColumn("StOutpostion2", "출고위치1");
            //DetailGridExControl.MainGrid.AddColumn("StOutpostion3", "출고위치1");
            //DetailGridExControl.MainGrid.AddColumn("StInpostion2", "입고위치2");
            //DetailGridExControl.MainGrid.AddColumn("StInpostion3", "입고위치3");

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "InOutId", "Division", "InOutDate", "MoldOutWhCode", "MoldOutWhPosition", "MoldInWhCode", "MoldInWhPosition", "InOutId");

        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldMakerCust", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldMakercust), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldWhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.WhDivision == MasterCodeSTR.WhCodeDivision_MOLD) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", DataConvert.GetCultureDataFieldName("PositionName"), true);
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", false);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldClass", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldClass), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.MainView.Columns["MoldFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MoldImage, "MoldFileName", "MoldFileUrl", true);
            MasterGridExControl.MainGrid.MainView.Columns["MoldCheckFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MoldCheckImage, "MoldCheckFileName", "MoldCheckFileUrl", true);


            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldClass", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InOutDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Division", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InOutDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldOutWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.WhDivision == MasterCodeSTR.WhCodeDivision_MOLD && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldOutWhPosition", ModelService.GetChildList<TN_WMS2000>(p => 1 == 1).ToList(), "PositionCode", DataConvert.GetCultureDataFieldName("PositionName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldInWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.WhDivision == MasterCodeSTR.WhCodeDivision_MOLD && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldInWhPosition", ModelService.GetChildList<TN_WMS2000>(p => 1 == 1).ToList(), "PositionCode", DataConvert.GetCultureDataFieldName("PositionName"), true);
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StOutpostion1", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 1), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StOutpostion2", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 2), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StOutpostion3", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 3), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StInpostion1", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 1), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StInpostion2", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 2), "Mcode", "Codename");
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StInpostion3", DbRequestHandler.GetCommCode(MasterCodeSTR.MOLDPOSTION, 3), "Mcode", "Codename");

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InOutId", ModelService.GetChildList<User>(p => p.ScmYn == "N" && p.Active == "Y"), "LoginId", DataConvert.GetCultureDataFieldName("UserName"));

            var OutWhPositionEdit = DetailGridExControl.MainGrid.Columns["MoldOutWhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            OutWhPositionEdit.Popup += OutWhPositionEdit_Popup;

            var InWhPositionEdit = DetailGridExControl.MainGrid.Columns["MoldInWhPosition"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            InWhPositionEdit.Popup += InWhPositionEdit_Popup;

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }


        protected override void DataLoad()
        {

            
            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("MoldMCode");

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            MasterGridExControl.DataSource = null;
            DetailGridExControl.DataSource = null;
            var moldCode = lup_MoldCode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(moldCode) ? true : p.MoldMCode == moldCode)
                                                             && (p.UseYN == "Y"))
                                                            .OrderBy(p => p.MoldMCode)
                                                            .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }


        protected override void MasterFocusedRowChanged()
        {

            TN_MOLD1100 masterobj = MasterGridBindingSource.Current as TN_MOLD1100;
            DetailGridBindingSource.DataSource = ModelService.GetChildList<TN_MOLD1300>(p => p.MoldMCode == masterobj.MoldMCode).OrderBy(o => o.InOutDate).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

        }
        protected override void DetailAddRowClicked()
        {
            //    base.DetailAddRowClicked();
            if (!UserRight.HasEdit) return;
            TN_MOLD1100 MasterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (MasterObj != null)
            {
                TN_MOLD1300 newobj = new TN_MOLD1300();
                newobj.MoldMCode = MasterObj.MoldMCode;
                newobj.MoldCode = MasterObj.MoldCode;
                newobj.MoldName = MasterObj.MoldName;
                newobj.Seq = MasterObj.TN_MOLD1300List.Count == 0 ? 1 : MasterObj.TN_MOLD1300List.Max(o => o.Seq) + 1;
                newobj.InOutDate = DateTime.Today;
                //string sql = "SELECT isnull(max(seq),0)+1 FROM [TN_MOLD003T] where MOLD_MCODE='" + obj1.MoldMcode + "'";
                //obj.Seq = Convert.ToInt32(DbRequestHandler.GetCellValue(sql, 0));
                //obj.MoldOutWhPosition = obj1.MoldWhPosition;
                //obj.MoldWhCode = obj1.MoldWhCode;
                //obj.StOutpostion2 = obj1.StPostion2;
                //obj.StOutpostion3 = obj1.StPostion3;

                DetailGridBindingSource.Add(newobj);
                MasterObj.TN_MOLD1300List.Add(newobj);
                //ModelService.InsertChild<TN_MOLD1300>(newobj);
                DetailGridBindingSource.MoveLast();

            }

            DetailGridExControl.MainGrid.BestFitColumns();
        }


        protected override void DeleteDetailRow()
        {

            TN_MOLD1100 MasterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            TN_MOLD1300 delobj = DetailGridBindingSource.Current as TN_MOLD1300;

            if (MasterObj != null)
            {
                MasterObj.TN_MOLD1300List.Remove(delobj);
                //ModelService.RemoveChild<TN_MOLD003>(obj);
                DetailGridBindingSource.RemoveCurrent();
                MasterGridExControl.BestFitColumns();
            }
        }

        protected override void DataSave()
        {
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_MOLD1300;
            if (detailObj == null) return;

            if (detailObj.Division == "01")//입고
            {

                detailObj.MoldOutWhCode = "";
                detailObj.MoldOutWhPosition = "";

            }
            else if (detailObj.Division == "02")
            {

                detailObj.MoldInWhCode = "";
                detailObj.MoldInWhPosition = "";

            }

        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            string fieldName = view.FocusedColumn.FieldName;

            var masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_MOLD1300;
            if (detailObj == null) return;

            if (detailObj.Division == "01")//입고
            {
               
                if (fieldName == "MoldOutWhCode" || fieldName == "MoldOutWhPosition")
                {

                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }

            }
            else if (detailObj.Division == "02")
            {
                
                if (fieldName == "MoldInWhCode" || fieldName == "MoldInWhPosition")
                {
                    
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
            //else
            //{
            //    //detailObj.MoldInWhCode = "";
            //    //detailObj.MoldInWhPosition = "";
            //    //detailObj.MoldoutWhCode = "";
            //    //detailObj.MoldOutWhPosition = "";
            //}


        }

        private void OutWhPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_MOLD1300;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.MoldOutWhCode + "'";
        }

        private void InWhPositionEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_MOLD1300;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.MoldInWhCode + "'";
        }
    }
}