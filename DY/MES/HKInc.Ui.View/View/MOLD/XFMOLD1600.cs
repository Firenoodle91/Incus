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
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Service.Service;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.View.MOLD
{
    /// <summary>    
    /// 금형일상점검조회
    /// </summary>
    public partial class XFMOLD1600 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        #region 전역변수
        /// <summary>        
        /// </summary>
        IService<TN_MOLD1600> ModelService = (IService<TN_MOLD1600>)ProductionFactory.GetDomainService("TN_MOLD1600");
        /// <summary>        
        /// </summary>
        //IService<TN_MOLD1601> ModelDtlService = (IService<TN_MOLD1601>)ProductionFactory.GetDomainService("TN_MOLD1601");

        //IService<TN_MOLD1602> ModelDtlService = (IService<TN_MOLD1601>)ProductionFactory.GetDomainService("TN_MOLD1601");

        /// <summary>        
        /// </summary>
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;

        RepositoryItemTextEdit repositoryItemTextEdit;

        /// <summary>
        
        /// </summary>
        

        /// <summary>      
        /// </summary>
        //private List<EditCheckList> EditList = new List<EditCheckList>();
        #endregion
        public XFMOLD1600()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellvalueChanged;
            MasterGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;

            DetailGridExControl.MainGrid.MainView.CellValueChanging += MainView_CellValueChanging;
            DetailGridExControl.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;

            dt_EvolDate.SetTodayIsMonth();

            //DetailGridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            //DetailGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            //OX_List = DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldWeird);
        }



        protected override void InitCombo()
        {             
            repositoryItemGridLookUpEdit = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = "Mcode",
                DisplayMember = "Codename"
            };
            repositoryItemGridLookUpEdit.View.OptionsView.ShowColumnHeaders = false;
            repositoryItemGridLookUpEdit.View.OptionsBehavior.AutoPopulateColumns = false;
            repositoryItemGridLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            repositoryItemGridLookUpEdit.View.OptionsBehavior.AllowIncrementalSearch = true;
            repositoryItemGridLookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemGridLookUpEdit.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            repositoryItemGridLookUpEdit.NullText = "";
            repositoryItemGridLookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemGridLookUpEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            repositoryItemGridLookUpEdit.Appearance.BackColor = Color.White;
            List<TN_STD1000> tempList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldWeird).ToList();
            repositoryItemGridLookUpEdit.DataSource = tempList;
            //repositoryItemGridLookUpEdit.DataSource = DbRequesHandler.GetCommCode(MasterCodeSTR.Check_OX).ToList();
            DevExpress.XtraGrid.Columns.GridColumn col1 = repositoryItemGridLookUpEdit.View.Columns.AddField(repositoryItemGridLookUpEdit.DisplayMember);
            col1.VisibleIndex = 0;

            repositoryItemGridLookUpEdit.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton() { Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete });
            repositoryItemGridLookUpEdit.ButtonPressed += Lookup_ButtonPressed;

            repositoryItemTextEdit = new RepositoryItemTextEdit();
            repositoryItemTextEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
        }


        protected override void InitGrid()
        {

            //MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("RevisionDate"), LabelConvert.GetLabelText("RevisionDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("RegDate"), LabelConvert.GetLabelText("RegDate"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("RegId"), LabelConvert.GetLabelText("RegId"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Memo"), LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "RevisionDate", "RegDate", "RegId", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MOLD1600>(MasterGridExControl);

            //DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("RevisionDate"), LabelConvert.GetLabelText("RevisionDate"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Seq"), LabelConvert.GetLabelText("Seq"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("EvolItem"), LabelConvert.GetLabelText("EvolItem"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("EvolStandard"), LabelConvert.GetLabelText("EvolStandard"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DataType"), LabelConvert.GetLabelText("DataType"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMin"), LabelConvert.GetLabelText("MoldMin"), HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMax"), LabelConvert.GetLabelText("MoldMax"), HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldPoint"), LabelConvert.GetLabelText("MoldPoint"), HorzAlignment.Far, FormatType.Numeric, "n2");
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Memo"), LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "EvolItem", "EvolStandard", "MoldMin", "MoldMax", "MoldPoint", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MOLD1601>(DetailGridExControl);

            //SubDetailGridExControl.SetToolbarVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            SubDetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("RevisionDate"), LabelConvert.GetLabelText("RevisionDate"), false);
            SubDetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Seq"), LabelConvert.GetLabelText("Seq"));
            SubDetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldClass"), LabelConvert.GetLabelText("MoldClass"));
            SubDetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMin"), LabelConvert.GetLabelText("MoldMin"), HorzAlignment.Far, FormatType.Numeric, "n2");
            SubDetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMax"), LabelConvert.GetLabelText("MoldMax"), HorzAlignment.Far, FormatType.Numeric, "n2");
            SubDetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Memo"), LabelConvert.GetLabelText("GradeStdContent"));
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "MoldClass", "MoldMin", "MoldMax", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_MOLD1602>(SubDetailGridExControl);

        }

        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("RegId", ModelService.GetChildList<User>(p => p.ScmYn == "N" && p.Active == "Y"), "LoginId", DataConvert.GetCultureDataFieldName("UserName"));
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", true);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("EvolItem", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldEvolItem), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //MasterGridExControl.MainGrid.MainView.Columns["EvolStandard"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "EvolStandard", true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldDataType), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", true);

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldClass", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldClass), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            //var revDateEdit = MasterGridExControl.MainGrid.Columns["RevisionDate"].ColumnEdit as RepositoryItem;
            //revDateEdit.EditValueChanged += RevDateEdit_EditValueChanged;

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
        }

        private void MainView_CellvalueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gridview = sender as GridView;
            if (gridview == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_MOLD1600;
            if (masterObj == null) return;

            string fieldName = e.Column.FieldName;

            if (fieldName == "RevisionDate")
            {
                var masterList = MasterGridBindingSource.List as List<TN_MOLD1600>;
                TN_MOLD1600 lastObj = masterList.Where(x => x.GradeManageNo != masterObj.GradeManageNo).LastOrDefault() as TN_MOLD1600;

                if (lastObj != null)
                {
                    if (lastObj.RevisionDate >= masterObj.RevisionDate)
                    {
                        MessageBox.Show("이전 개정일보다 커야 됩니다.");
                        masterObj.RevisionDate = lastObj.RevisionDate.AddDays(1);
                        return;
                    }
                }
                else
                {
                    TN_MOLD1600 tN_MOLD1600 = ModelService.GetList(x => 1 == 1).LastOrDefault();

                    if (tN_MOLD1600 != null && tN_MOLD1600.RevisionDate >= masterObj.RevisionDate)
                    {
                        MessageBox.Show("이전 개정일보다 커야 됩니다.");
                        masterObj.RevisionDate = tN_MOLD1600.RevisionDate.AddDays(1);
                        return;
                    }
                }

            }
            

            
        }

        private void MainView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //if (e.Column.FieldName.IndexOf("Day") >= 0)
            //{
            //    if (e.Column.FieldName.IndexOf("CheckId") < 0)
            //    {                   
            //        var checkValue = e.Value;
            //        if (checkValue == null)
            //            return;
            //        else if (checkValue.ToString() == "")
            //            return;
            //        else
            //        {
            //            int iCheckSeq = view.GetRowCellValue(e.RowHandle, view.Columns["Seq"]) == null ? 0 : Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["CheckSeq"]));
            //            int iDay = e.Column.FieldName.Substring(3) == null ? 0 : Convert.ToInt32(e.Column.FieldName.Substring(3));
            //            var vEditList = EditList.Where(p => p.Seq == iCheckSeq &&
            //                                                p.Day == iDay).FirstOrDefault();
            //            if (vEditList != null)
            //                vEditList.Value = checkValue.ToString();
            //            else
            //                EditList.Add(new EditCheckList() { Seq = iCheckSeq, Day = iDay, Value = checkValue.ToString() });

            //        }
            //    }
            //}
        }

        /// <summary>        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            //DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            //if (e.Column.FieldName.IndexOf("Day") >= 0)
            //{
            //    if (e.Column.FieldName.IndexOf("CheckId") < 0)
            //    {
            //        var checkWay = view.GetRowCellValue(e.RowHandle, view.Columns["CheckWay"]);
            //        if (checkWay == null)
            //            return;
            //        else if (checkWay.ToString() == "")
            //            return;
            //        else
            //        {
            //            string sResult = checkWay.ToString();
            //            if (sResult == "01")
            //            {
            //                //e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            //                e.RepositoryItem = repositoryItemTextEdit;
            //            }
            //            else
            //                e.RepositoryItem = repositoryItemGridLookUpEdit;
            //        }
            //    }
            //}
        }

        

        private void Lookup_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (edit == null) return;

            if (e.Button.Kind == ButtonPredefines.Delete)
                edit.EditValue = null;

        }       

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("RevisionDate");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.RevisionDate >= dt_EvolDate.DateFrEdit.DateTime
                                                                          && p.RevisionDate <= dt_EvolDate.DateToEdit.DateTime))
                                                                          .OrderBy(o => o.RevisionDate)
                                                                          .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_MOLD1600 masterObj = MasterGridBindingSource.Current as TN_MOLD1600;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
                
            DetailGridBindingSource.DataSource = masterObj.TN_MOLD1601List.OrderBy(p => p.Seq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            SubDetailGridBindingSource.DataSource = masterObj.TN_MOLD1602List.OrderBy(p => p.Seq).ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void DetailFocusedRowChanged()
        {
            //TN_MOLD1600 masterObj = MasterGridBindingSource.Current as TN_MOLD1600;
            //if (masterObj == null)
            //{
            //    DetailGridExControl.MainGrid.Clear();
            //    return;
            //}

            //TN_MOLD1601 detailObj = DetailGridBindingSource.Current as TN_MOLD1601;
            //if (detailObj == null)
            //{
            //    SubDetailGridExControl.MainGrid.Clear();
            //    return;
            //}
           
        }

        protected override void AddRowClicked()
        {
            var masterList = MasterGridBindingSource.List as List<TN_MOLD1600>;
            var lastObj = masterList.LastOrDefault() as TN_MOLD1600;
            TN_MOLD1600 tN_MOLD1600 = ModelService.GetList(x => 1 == 1).LastOrDefault();

            TN_MOLD1600 NewObj = new TN_MOLD1600();
            NewObj.GradeManageNo = DbRequestHandler.GetSeqStandard("MOLD_GRADE");
            NewObj.RevisionDate = DateTime.Today;
            NewObj.RegDate = DateTime.Today;
            NewObj.RegId = GlobalVariable.LoginId;
            NewObj.NewRowFlag = "Y";

            //그리드의 마지막값이 있을경우 마지막값 비교
            //그리드 마지막값이 없을경우 DB의 마지막값 비교
            if (lastObj != null)
            {
                if(lastObj.RevisionDate >= DateTime.Today)
                    NewObj.RevisionDate = lastObj.RevisionDate.AddDays(1);
            }
            else if (tN_MOLD1600 != null)
            {
                if(tN_MOLD1600.RevisionDate >= DateTime.Today)
                    NewObj.RevisionDate = tN_MOLD1600.RevisionDate.AddDays(1);
            }

            ModelService.Insert(NewObj);
            MasterGridBindingSource.Add(NewObj);
            MasterGridExControl.BestFitColumns();

        }

        protected override void DeleteRow()
        {
            TN_MOLD1600 delobj = MasterGridBindingSource.Current as TN_MOLD1600;
            if (delobj != null)
            {
                if (delobj.TN_MOLD1601List.Any(p => p.GradeManageNo == delobj.GradeManageNo))
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_140));
                }
                else if (delobj.TN_MOLD1602List.Any(p => p.GradeManageNo == delobj.GradeManageNo))
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_141));
                }
                else
                {
                    MasterGridBindingSource.Remove(delobj);
                    ModelService.Delete(delobj);
                }
               
            }

            MasterGridExControl.BestFitColumns();
        }


        protected override void DetailAddRowClicked()
        {

            TN_MOLD1600 MasterObj = MasterGridBindingSource.Current as TN_MOLD1600;
            if (MasterObj == null) return;

            List<TN_MOLD1601> DetailList = DetailGridBindingSource.DataSource as List<TN_MOLD1601>;
            if (DetailList == null) return;

            TN_MOLD1601 NewObj = new TN_MOLD1601();

            NewObj.GradeManageNo = MasterObj.GradeManageNo;
            NewObj.RevisionDate = MasterObj.RevisionDate;
            NewObj.Seq = MasterObj.TN_MOLD1601List.Count == 0 ? 1 : MasterObj.TN_MOLD1601List.Max(o => o.Seq) + 1;
            NewObj.DataType = "01";
            NewObj.NewRowFlag = "Y";

            DetailGridBindingSource.Add(NewObj);
            MasterObj.TN_MOLD1601List.Add(NewObj);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            TN_MOLD1600 MasterObj = MasterGridBindingSource.Current as TN_MOLD1600;
            if (MasterObj == null) return;

            TN_MOLD1601 DetailObj = DetailGridBindingSource.Current as TN_MOLD1601;
            if (DetailObj == null) return;

            //if (DetailObj.NewRowFlag == "N")
            
            MasterObj.TN_MOLD1601List.Remove(DetailObj);
            DetailGridBindingSource.RemoveCurrent();
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        // SUB 추가
        protected override void SubDetailAddRowClicked()
        {
            List<TN_MOLD1602> SubList = SubDetailGridBindingSource.DataSource as List<TN_MOLD1602>;
            TN_MOLD1600 MasterObj = MasterGridBindingSource.Current as TN_MOLD1600;

            TN_MOLD1602 NewObj = new TN_MOLD1602();
            NewObj.GradeManageNo = MasterObj.GradeManageNo;
            NewObj.RevisionDate = MasterObj.RevisionDate;
            NewObj.Seq = MasterObj.TN_MOLD1602List.Count == 0 ? 1 : MasterObj.TN_MOLD1602List.Max(o => o.Seq) + 1;
            NewObj.NewRowFlag = "Y";

            SubDetailGridBindingSource.Add(NewObj);
            MasterObj.TN_MOLD1602List.Add(NewObj);
            SubDetailGridExControl.MainGrid.BestFitColumns();

        }

        // SUB 삭제
        protected override void DeleteSubDetailRow()
        {
            TN_MOLD1600 MasterObj = MasterGridBindingSource.Current as TN_MOLD1600;
            if (MasterObj == null) return;

            TN_MOLD1602 SubDetailObj = SubDetailGridBindingSource.Current as TN_MOLD1602;
            if (SubDetailObj == null) return;

            MasterObj.TN_MOLD1602List.Remove(SubDetailObj);
            SubDetailGridBindingSource.RemoveCurrent();
            SubDetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            SubDetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
            
        }


        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            string fieldName = view.FocusedColumn.FieldName;

            var masterObj = MasterGridBindingSource.Current as TN_MOLD1600;
            if (masterObj == null) return;

            if (fieldName == "RevisionDate")
            {
                if (masterObj.NewRowFlag == "N")
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }                
            }
        }

        private void RevDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                var masterObj = MasterGridBindingSource.Current as TN_MOLD1600;
                if (masterObj == null) return;

                if (masterObj.NewRowFlag != "Y")
                    return;

                var masterList = MasterGridBindingSource.List as List<TN_MOLD1600>;
                var lastObj = masterList.LastOrDefault() as TN_MOLD1600;

                DateTime? oldRevDate = MasterGridExControl.MainGrid.MainView.ActiveEditor.OldEditValue as DateTime?;

                if (lastObj != null)
                {
                    if (lastObj.RevisionDate >= masterObj.RevisionDate)
                    {
                        MessageBox.Show("GRID 작거나 같다");
                        masterObj.RevisionDate = oldRevDate.GetValueOrDefault();
                        return;
                    }
                }
                else
                {
                    TN_MOLD1600 tN_MOLD1600 = ModelService.GetList(x => 1 == 1).LastOrDefault();

                    if (tN_MOLD1600 != null && tN_MOLD1600.RevisionDate >= masterObj.RevisionDate)
                    {
                        MessageBox.Show("DB 작거나 같다");
                        masterObj.RevisionDate = oldRevDate.GetValueOrDefault();
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

    }
}