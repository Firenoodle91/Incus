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
using DevExpress.XtraGrid.Columns;



namespace HKInc.Ui.View.View.MOLD
{
    /// <summary>    
    /// 금형일상점검조회
    /// </summary>
    public partial class XFMOLD1500 : Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        /// <summary>        
        /// </summary>
        IService<TN_MOLD1100> ModelService = (IService<TN_MOLD1100>)ProductionFactory.GetDomainService("TN_MOLD1100");
        /// <summary>        
        /// </summary>
        IService<TN_MOLD1500> ModelDtlService = (IService<TN_MOLD1500>)ProductionFactory.GetDomainService("TN_MOLD1500");

        /// <summary>        
        /// </summary>
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;

        RepositoryItemTextEdit repositoryItemTextEdit;

        /// <summary>
        
        /// </summary>
        List<TN_STD1000> OX_List = new List<TN_STD1000>();

        /// <summary>      
        /// </summary>
        private List<EditCheckList> EditList = new List<EditCheckList>();
        #endregion
        public XFMOLD1500()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            DetailGridExControl.MainGrid.MainView.CellValueChanging += MainView_CellValueChanging;
            DetailGridExControl.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            //DetailGridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            //DetailGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;

            OX_List = DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldWeird);

            dt_YearMonth.SetFormat(DateFormat.Month);
            dt_YearMonth.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {   
            lup_MoldCode.SetDefault(true, "MoldMCode", DataConvert.GetCultureDataFieldName("MoldName"), ModelService.GetChildList<TN_MOLD1100>(p => p.UseYN == "Y").ToList());
            
            repositoryItemGridLookUpEdit = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = "CodeVal"
               ,
                DisplayMember = DataConvert.GetCultureDataFieldName("CodeName")
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
            repositoryItemGridLookUpEdit.DataSource = DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckOX).ToList();
            GridColumn col1 = repositoryItemGridLookUpEdit.View.Columns.AddField(repositoryItemGridLookUpEdit.DisplayMember);
            col1.VisibleIndex = 0;            

            repositoryItemTextEdit = new RepositoryItemTextEdit();
        }


        protected override void InitGrid()
        {

            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMCode"), LabelConvert.GetLabelText("MoldMCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCode"), LabelConvert.GetLabelText("MoldCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldName"), LabelConvert.GetLabelText("MoldName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemCode"), LabelConvert.GetLabelText("ItemCode"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMakerCust"), LabelConvert.GetLabelText("MoldMakerCust"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("TransferDate"), LabelConvert.GetLabelText("TransferDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");            
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MainMachineCode"), LabelConvert.GetLabelText("MainMachineCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Cavity"), LabelConvert.GetLabelText("Cavity"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldWhCode"), LabelConvert.GetLabelText("MoldWhCode"),false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldWhPosition"), LabelConvert.GetLabelText("MoldWhPosition"), false);            
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StdShotcnt"), LabelConvert.GetLabelText("StdShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Memo"), LabelConvert.GetLabelText("Memo"),false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckCycle"), LabelConvert.GetLabelText("CheckCycle"),false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("NextCheckDate"), LabelConvert.GetLabelText("NextMoldCheckDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd", false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldClass"), LabelConvert.GetLabelText("MoldClass"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("RealShotcnt"), LabelConvert.GetLabelText("RealShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("BaseShotcnt"), LabelConvert.GetLabelText("BaseShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckShotcnt"), LabelConvert.GetLabelText("CheckShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldFileName"), LabelConvert.GetLabelText("MoldFileName"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldFileUrl"), LabelConvert.GetLabelText("MoldFileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCheckFileName"), LabelConvert.GetLabelText("MoldCheckFileName"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCheckFileUrl"), LabelConvert.GetLabelText("MoldCheckFileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("UploadFilePath"), LabelConvert.GetLabelText("UploadFilePath"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DeleteFilePath"), LabelConvert.GetLabelText("DeleteFilePath"), false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DisuseDate"), LabelConvert.GetLabelText("DisuseDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd", false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("UseYN"), LabelConvert.GetLabelText("UseFlag"), HorzAlignment.Center, true);

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Seq"), LabelConvert.GetLabelText("Seq"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckPosition"), LabelConvert.GetLabelText("CheckPosition"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckList"), LabelConvert.GetLabelText("CheckList"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckWay"), LabelConvert.GetLabelText("CheckWay"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckCycle"), LabelConvert.GetLabelText("CheckCycle"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckStandardDate"), LabelConvert.GetLabelText("CheckStandardDate"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ManagementStandard"), LabelConvert.GetLabelText("ManagementStandard"));



            for (int i = 1; i <= 31; i++)
            {
                var valueColumnName = "Day" + i.ToString();
                var checkIdColumnName = "Day" + i.ToString() + "_CheckId";
                DetailGridExControl.MainGrid.AddColumn(valueColumnName, i.ToString() + "일차", HorzAlignment.Center, true);
                DetailGridExControl.MainGrid.AddColumn(checkIdColumnName, i.ToString() + "일차 점검자", HorzAlignment.Center, true);
                DetailGridExControl.MainGrid.Columns["Day" + i.ToString()].MinWidth = 50;
                DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Day" + i.ToString());
            }
        }

        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldMakerCust", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldMakercust), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckCycle), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", false);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldClass", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldClass), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.MainView.Columns["MoldFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MoldImage, "MoldFileName", "MoldFileUrl", true);
            MasterGridExControl.MainGrid.MainView.Columns["MoldCheckFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, MasterGridExControl, MasterCodeSTR.FtpFolder_MoldCheckImage, "MoldCheckFileName", "MoldCheckFileUrl", true);
            MasterGridExControl.MainGrid.SetRepositoryItemCodeLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());    // 2021-07-15 김진우 주임 SetRepositoryItemSearchLookUpEdit 에서 SetRepositoryItemCodeLookUpEdit 로 변경


            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckPosition), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckList), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckCycle), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("CheckStandardDate");
            
            

            List<TN_STD1000> ox_List = DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldWeird);
            //List<User> user_List = ModelService.GetChildList<User>(p => p.Active.Equals("Y"));

            for (int i = 1; i < 32; i++)
            {
                string sColumnValue_User = "Day" + i.ToString() + "_CheckId";
                DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit(sColumnValue_User, ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");            
            }

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        

        private void MainView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Column.FieldName.IndexOf("Day") >= 0)
            {
                if (e.Column.FieldName.IndexOf("CheckId") < 0)
                {                   
                    var checkValue = e.Value;
                    if (checkValue == null)
                        return;
                    else if (checkValue.ToString() == "")
                        return;
                    else
                    {
                        int iCheckSeq = view.GetRowCellValue(e.RowHandle, view.Columns["Seq"]) == null ? 0 : Convert.ToInt32(view.GetRowCellValue(e.RowHandle, view.Columns["Seq"]));
                        int iDay = e.Column.FieldName.Substring(3) == null ? 0 : Convert.ToInt32(e.Column.FieldName.Substring(3));
                        var vEditList = EditList.Where(p => p.Seq == iCheckSeq &&
                                                            p.Day == iDay).FirstOrDefault();
                        if (vEditList != null)
                            vEditList.Value = checkValue.ToString();
                        else
                            EditList.Add(new EditCheckList() { Seq = iCheckSeq, Day = iDay, Value = checkValue.ToString() });

                    }
                }
            }
        }

        /// <summary>        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            if (e.Column.FieldName.IndexOf("Day") >= 0)
            {
                if (e.Column.FieldName.IndexOf("CheckId") < 0)
                {
                    var checkWay = view.GetRowCellValue(e.RowHandle, view.Columns["CheckWay"]);
                    if (checkWay == null)
                        return;
                    else if (checkWay.ToString() == "")
                        return;
                    else
                    {
                        string sResult = checkWay.ToString();
                        if (sResult == "01")
                        {
                            //e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                            e.RepositoryItem = repositoryItemTextEdit;
                        }
                        else
                            e.RepositoryItem = repositoryItemGridLookUpEdit;
                    }
                }
            }
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
            GridRowLocator.GetCurrentRow("MoldMcode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            string MoldMcode = lup_MoldCode.EditValue == null ? "" : lup_MoldCode.EditValue.ToString();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(MoldMcode) ? true : p.MoldMCode.Equals(MoldMcode)) &&
                                                                           p.UseYN == "Y").OrderBy(o => o.MoldMCode) .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_MOLD1100 masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (masterObj == null)
                return;

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var MonthDate = new SqlParameter("@SEARCH_DATA", dt_YearMonth.DateTime.ToString("yyyy-MM-01"));
                var MoldMcode = new SqlParameter("@MOLD_MCODE", masterObj.MoldMCode);
                var result = context.Database.SqlQuery<TEMP_MOLD_CHECK_LIST>("SP_GET_MOLDCHECK_LIST @SEARCH_DATA, @MOLD_MCODE", MonthDate, MoldMcode).ToList();
                DetailGridBindingSource.DataSource = result.ToList();
            }

            var lastDay = DateTime.DaysInMonth(dt_YearMonth.DateTime.Year, dt_YearMonth.DateTime.Month);
            for (int i = 1; i <= 31; i++)
            {
                var valueColumnName = "Day" + i.ToString();
                var checkIdColumnName = "Day" + i.ToString() + "_CheckId";
                if (i <= lastDay)
                {
                    DetailGridExControl.MainGrid.Columns[valueColumnName].Visible = true;
                    // 20210630 오세완 차장 출력 순번이 꼬일 수 있어서 visiableindex 추가 처리
                    DetailGridExControl.MainGrid.Columns[valueColumnName].VisibleIndex = (i * 2) + 7;
                    DetailGridExControl.MainGrid.Columns[checkIdColumnName].Visible = true;
                    DetailGridExControl.MainGrid.Columns[checkIdColumnName].VisibleIndex = DetailGridExControl.MainGrid.Columns[valueColumnName].VisibleIndex + 1;
                    //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit(checkIdColumnName, ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
                    DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, valueColumnName);
                }
                else
                {
                    DetailGridExControl.MainGrid.Columns[valueColumnName].Visible = false;
                    DetailGridExControl.MainGrid.Columns[checkIdColumnName].Visible = false;
                }
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            TN_MOLD1100 masterObj = MasterGridBindingSource.Current as TN_MOLD1100;

            if (masterObj == null)
                return;

            if (EditList.Count > 0)
            {
                List<TN_MOLD1500> tempList = ModelDtlService.GetList(p => p.CheckDate.Year == dt_YearMonth.DateTime.Year &&
                                                                          p.CheckDate.Month == dt_YearMonth.DateTime.Month &&
                                                                          p.MoldMCode == masterObj.MoldMCode).ToList();
                foreach (EditCheckList ecl in EditList)
                {
                    TN_MOLD1500 detailobj = tempList.Where(p => p.CheckDate.Day == ecl.Day &&
                                                                 p.Seq == ecl.Seq).FirstOrDefault();
                    if (detailobj != null)
                    {
                        detailobj.CheckId = GlobalVariable.LoginId;
                        detailobj.CheckValue = ecl.Value;
                        detailobj.UpdateTime = DateTime.Now;
                        ModelDtlService.Update(detailobj);
                    }
                    else
                    {
                        bool bInsert = false;
                        if (ecl.Value == null)
                            bInsert = false;
                        else if (ecl.Value.ToString() == "")
                            bInsert = false;
                        else
                            bInsert = true;

                        if (bInsert)
                        {
                            TN_MOLD1500 newobj = new TN_MOLD1500()
                            {
                                MoldMCode = masterObj.MoldMCode,
                                Seq = ecl.Seq,
                                CheckDate = new DateTime(dt_YearMonth.DateTime.Year, dt_YearMonth.DateTime.Month, ecl.Day),
                                CheckId = GlobalVariable.LoginId,
                                CheckValue = ecl.Value
                            };
                            ModelDtlService.Insert(newobj);
                        }
                    }
                }

                ModelDtlService.Save();
                DataLoad();
            }
        }

        private class EditCheckList
        {
            public int Seq { get; set; }
            public int Day { get; set; }
            public string Value { get; set; }
        }
    }
}