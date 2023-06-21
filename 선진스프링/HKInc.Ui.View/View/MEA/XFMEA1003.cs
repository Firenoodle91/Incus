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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;

namespace HKInc.Ui.View.View.MEA
{
    /// <summary>
    /// 설비점검이력관리
    /// </summary>
    public partial class XFMEA1003 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        RepositoryItemSpinEdit repositorySpinEdit = new RepositoryItemSpinEdit();
        //RepositoryItemCheckEdit repositoryCheckEdit = new RepositoryItemCheckEdit() { ValueChecked = "Y", ValueUnchecked = "N" };
        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;

        IService<VI_MEA1003_MASTER_LIST> ModelService = (IService<VI_MEA1003_MASTER_LIST>)ProductionFactory.GetDomainService("VI_MEA1003_MASTER_LIST");

        private List<EditCheckList> EditList = new List<EditCheckList>();

        private bool technicianFlag = false;

        public XFMEA1003()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanging += MainView_CellValueChanging;
            DetailGridExControl.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            //DetailGridExControl.MainGrid.MainView.CustomRowCellEdit += MainView_CustomRowCellEdit;
        }

        private void MainView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var view = sender as GridView;
            if (e.Column.FieldName.Contains("Day"))
            {
                var checkSeq = view.GetFocusedRowCellValue("CheckSeq").GetIntNullToZero();
                var checkDay = e.Column.FieldName.Substring(3).GetIntNullToZero();
                var value = e.Value.GetNullToEmpty();
                var obj = EditList.Where(p => p.CheckSeq == checkSeq && p.Day == checkDay).FirstOrDefault();
                if (obj != null)
                    obj.Value = value;
                else
                    EditList.Add(new EditCheckList() { CheckSeq = checkSeq, Day = checkDay, Value = value });
            }
        }

        protected override void InitCombo()
        {
            lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
            dt_YearMonth.SetFormat(DateFormat.Month);
            dt_YearMonth.DateTime = DateTime.Today;

            //repositorySpinEdit.Buttons[0].Visible = false;

            //repositoryCheckEdit.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            //repositoryCheckEdit.NullText = "N";

            repositoryItemGridLookUpEdit = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = "CodeVal"
                ,DisplayMember = DataConvert.GetCultureDataFieldName("CodeName")
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
            repositoryItemGridLookUpEdit.DataSource = DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckOX).ToList();
            DevExpress.XtraGrid.Columns.GridColumn col1 = repositoryItemGridLookUpEdit.View.Columns.AddField(repositoryItemGridLookUpEdit.DisplayMember);
            col1.VisibleIndex = 0;

            repositoryItemGridLookUpEdit.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton() { Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete });
            repositoryItemGridLookUpEdit.ButtonPressed += Lookup_ButtonPressed;

            repositoryItemSearchLookUpEdit = new RepositoryItemSearchLookUpEdit()
            {
                ValueMember = "LoginId",
                DisplayMember = "UserName"
            };
            repositoryItemSearchLookUpEdit.View.OptionsView.ShowColumnHeaders = false;
            repositoryItemSearchLookUpEdit.View.OptionsBehavior.AutoPopulateColumns = false;
            repositoryItemSearchLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            repositoryItemSearchLookUpEdit.View.OptionsBehavior.AllowIncrementalSearch = true;
            repositoryItemSearchLookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemSearchLookUpEdit.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            repositoryItemSearchLookUpEdit.NullText = "";
            repositoryItemSearchLookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemSearchLookUpEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            repositoryItemSearchLookUpEdit.Appearance.BackColor = Color.White;
            repositoryItemSearchLookUpEdit.DataSource = ModelService.GetChildList<User>(p => true).ToList();

            repositoryItemTextEdit = new RepositoryItemTextEdit();
            repositoryItemTextEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Default;

            var technicianGroupObj = ModelService.GetChildList<UserUserGroup>(p => p.UserGroupId == MasterCodeSTR.UserGroup_Technician
                                                                                && p.UserId == GlobalVariable.UserId).FirstOrDefault();
            if (technicianGroupObj != null)
                technicianFlag = true;

            repositorySpinEdit.Buttons[0].Visible = false;
        }

        private void Lookup_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (edit == null) return;

            if (e.Button.Kind == ButtonPredefines.Delete)
                edit.EditValue = null;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("MachineCode", LabelConvert.GetLabelText("MachineMCode"), false);
            MasterGridExControl.MainGrid.AddColumn("TN_MEA1000.MachineCode", LabelConvert.GetLabelText("MachineCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_MEA1000." + DataConvert.GetCultureDataFieldName("MachineName"), LabelConvert.GetLabelText("MachineName"));

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.AddColumn("CheckSeq", LabelConvert.GetLabelText("CheckSeq"), false);
            DetailGridExControl.MainGrid.AddColumn("CheckPosition", LabelConvert.GetLabelText("CheckPosition"));
            DetailGridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("CheckList"));
            DetailGridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("CheckWay"));
            DetailGridExControl.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("EyeCheckFlag"), false);
            DetailGridExControl.MainGrid.AddColumn("CheckCycle", LabelConvert.GetLabelText("CheckCycle"));
            DetailGridExControl.MainGrid.AddColumn("CheckStandardDate", LabelConvert.GetLabelText("CheckStandardDate"));
            DetailGridExControl.MainGrid.AddColumn("ManagementStandard", LabelConvert.GetLabelText("ManagementStandard"));

            DetailGridExControl.MainGrid.Columns["CheckPosition"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            DetailGridExControl.MainGrid.Columns["CheckList"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            DetailGridExControl.MainGrid.Columns["CheckWay"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            DetailGridExControl.MainGrid.Columns["CheckCycle"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            DetailGridExControl.MainGrid.Columns["CheckStandardDate"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            DetailGridExControl.MainGrid.Columns["ManagementStandard"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            for (int i = 1; i <= 31; i++)
            {
                var valueColumnName = "Day" + i.ToString();
                var checkIdColumnName = "Day" + i.ToString() + "_LastCheckId";
                DetailGridExControl.MainGrid.AddColumn(valueColumnName, i.ToString() + LabelConvert.GetLabelText("Day"), HorzAlignment.Center, true);
                DetailGridExControl.MainGrid.AddColumn(checkIdColumnName, i.ToString() + LabelConvert.GetLabelText("Day") + " " + LabelConvert.GetLabelText("LastCheckId"), HorzAlignment.Center, true);
                DetailGridExControl.MainGrid.Columns["Day" + i.ToString()].MinWidth = 50;
                DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Day" + i.ToString());
            }
        }

        protected override void InitRepository()
        {
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckPosition), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckList), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckWay), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckCycle), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckStandardDate", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MachineCheckStandardDate), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.MainView.Columns["ManagementStandard"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "ManagementStandard");
            
            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("MachineMCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            EditList.Clear();

            ModelService.ReLoad();

            var machineCode = lup_Machine.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(machineCode) ? true : p.MachineCode == machineCode)
                                                                        && (p.TN_MEA1000.UseFlag == "Y")
                                                                        && (p.TN_MEA1000.DailyCheckFlag == "Y")
                                                               )
                                                               .OrderBy(p => p.TN_MEA1000.MachineName)
                                                               .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            EditList.Clear();

            var masterObj = MasterGridBindingSource.Current as VI_MEA1003_MASTER_LIST;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var MonthDate = new SqlParameter("@MonthDate", dt_YearMonth.DateTime);
                var MachineCode = new SqlParameter("@MachineCode", masterObj.MachineCode);
                var result = context.Database.SqlQuery<TEMP_XFMEA1003_DETAIL>("USP_GET_XFMEA1003_DETAIL @MonthDate, @MachineCode", MonthDate, MachineCode).ToList();
                DetailGridBindingSource.DataSource = result.ToList();
            }

            var lastDay = DateTime.DaysInMonth(dt_YearMonth.DateTime.Year, dt_YearMonth.DateTime.Month);
            for (int i = 1; i <= 31; i++)
            {
                var valueColumnName = "Day" + i.ToString();
                var checkIdColumnName = "Day" + i.ToString() + "_LastCheckId";
                if (i <= lastDay)
                {
                    DetailGridExControl.MainGrid.Columns[valueColumnName].Visible = true;
                    DetailGridExControl.MainGrid.Columns[checkIdColumnName].Visible = true;
                    DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, valueColumnName);
                    //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, valueColumnName, checkIdColumnName);
                }
                else
                {
                    DetailGridExControl.MainGrid.Columns[valueColumnName].Visible = false;
                    DetailGridExControl.MainGrid.Columns[checkIdColumnName].Visible = false;
                    //DetailGridExControl.MainGrid.SetEditable(false, valueColumnName);
                }
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            #region INSERT, UPDATE

            var masterObj = MasterGridBindingSource.Current as VI_MEA1003_MASTER_LIST;
            if (masterObj != null)
            {
                if (EditList.Count > 0)
                {
                    var list = DetailGridBindingSource.List as List<TEMP_XFMEA1003_DETAIL>;
                    var monthDate = list.First().MonthDate;


                    var dataList = ModelService.GetChildList<TN_MEA1003>(p => p.CheckDate.Year == monthDate.Year
                                                                            && p.CheckDate.Month == monthDate.Month
                                                                            && p.MachineCode == masterObj.MachineCode).ToList();

                    foreach (var v in EditList)
                    {
                        var checkObj = dataList.Where(p => p.CheckDate.Day == v.Day && p.CheckSeq == v.CheckSeq).FirstOrDefault();
                        if (checkObj != null)
                        {
                            checkObj.CheckId = GlobalVariable.LoginId;
                            checkObj.CheckValue = v.Value;
                            checkObj.UpdateTime = DateTime.Now;
                            ModelService.UpdateChild(checkObj);
                        }
                        else
                        {
                            if (!v.Value.IsNullOrEmpty())
                            {
                                var newObj = new TN_MEA1003();
                                newObj.MachineCode = masterObj.MachineCode;
                                newObj.CheckSeq = v.CheckSeq;
                                newObj.CheckDate = new DateTime(monthDate.Year, monthDate.Month, v.Day);
                                newObj.CheckId = GlobalVariable.LoginId;
                                newObj.CheckValue = v.Value;
                                ModelService.InsertChild(newObj);
                            }
                        }
                    }
                }
            }

            //var masterObj = MasterGridBindingSource.Current as VI_MEA1003_MASTER_LIST;
            //if (masterObj != null)
            //{
            //    var list = DetailGridBindingSource.List as List<TEMP_XFMEA1003_DETAIL>;
            //    if (list != null && list.Count != 0)
            //    {
            //        var monthDate = list.First().MonthDate;
            //        var lastDay = DateTime.DaysInMonth(monthDate.Year, monthDate.Month);
            //        var dataList = ModelService.GetChildList<TN_MEA1003>(p => p.CheckDate.Year == monthDate.Year
            //                                                                && p.CheckDate.Month == monthDate.Month
            //                                                                && p.MachineCode == masterObj.MachineCode).ToList();
            //        foreach (var v in list)
            //        {
            //            for (int i = 1; i <= 31; i++)
            //            {
            //                if (i <= lastDay)
            //                {
            //                    var checkValue = GetCheckValue(i, v);
            //                    if (!checkValue.IsNullOrEmpty())
            //                    {
            //                        var checkObj = dataList.Where(p => p.CheckDate.Day == i && p.CheckSeq == v.CheckSeq).FirstOrDefault();
            //                        if (checkObj != null)
            //                        {
            //                            checkObj.CheckId = GlobalVariable.LoginId;
            //                            checkObj.CheckValue = checkValue;
            //                            ModelService.UpdateChild(checkObj);
            //                        }
            //                        else
            //                        {
            //                            var newObj = new TN_MEA1003();
            //                            newObj.MachineCode = masterObj.MachineCode;
            //                            newObj.CheckSeq = v.CheckSeq;
            //                            newObj.CheckDate = new DateTime(monthDate.Year, monthDate.Month, i);
            //                            newObj.CheckId = GlobalVariable.LoginId;
            //                            newObj.CheckValue = checkValue;
            //                            ModelService.InsertChild(newObj);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion

            ModelService.Save();
            DataLoad();
        }

        /// <summary>
        /// 점검 컨트롤 변경을 위함
        /// </summary>
        //private void MainView_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        //{
        //    var view = sender as GridView;
        //    if (e.Column.FieldName.Contains("Day") && e.RowHandle >= 0)
        //    {
        //        var checkWay = view.GetRowCellValue(e.RowHandle, view.Columns["CheckWay"]).GetNullToEmpty();
        //        if (checkWay == MasterCodeSTR.MachineCheckWay_CheckFlag)
        //        {
        //            //치수 입력
        //            e.RepositoryItem = repositorySpinEdit;
        //        }
        //        else if (checkWay == MasterCodeSTR.MachineCheckWay_Memo)
        //        {
        //            //메모 입력
        //            e.Column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Default;
        //            e.RepositoryItem = null;
        //        }
        //        else
        //        {
        //            //O,X 입력
        //            e.RepositoryItem = repositoryItemGridLookUpEdit;
        //            e.Column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        //        }
        //    }
        //}

        private void MainView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Day") && !e.Column.FieldName.Contains("LastCheckId"))
                {
                    var checkPosition = view.GetRowCellValue(e.RowHandle, view.Columns["CheckPosition"]).GetNullToEmpty();
                    var checkWay = view.GetRowCellValue(e.RowHandle, view.Columns["CheckWay"]).GetNullToEmpty();

                    if (checkPosition == MasterCodeSTR.MachineCheckPosition_ConfirmFlag)
                    {
                        e.RepositoryItem = repositoryItemGridLookUpEdit;
                        //e.RepositoryItem = repositoryItemGridLookUpEdit;
                    }
                    else
                    {
                        if (checkWay == MasterCodeSTR.MachineCheckWay_Memo)
                        {
                            e.RepositoryItem = repositoryItemTextEdit;
                            //e.RepositoryItem = repositoryItemGridLookUpEdit;
                        }
                        else
                        {
                            var eyeCheckFlag = view.GetRowCellValue(e.RowHandle, view.Columns["Temp"]).GetNullToEmpty();
                            if (eyeCheckFlag == "N")
                            {
                                //치수 입력
                                e.RepositoryItem = repositorySpinEdit;
                            }
                            else
                            {
                                //O,X 입력
                                //e.Column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                                e.RepositoryItem = repositoryItemGridLookUpEdit;
                            }
                        }
                    }
                }
                else if (e.Column.FieldName.Contains("LastCheckId"))
                {
                    e.RepositoryItem = repositoryItemSearchLookUpEdit;
                }
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            var obj = DetailGridBindingSource.Current as TEMP_XFMEA1003_DETAIL;
            if (obj.CheckPosition == MasterCodeSTR.MachineCheckPosition_ConfirmFlag)
            {
                if(!technicianFlag)
                    e.Cancel = true;
            }
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;

            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Day") && !e.Column.FieldName.Contains("LastCheckId"))
                {
                    var checkPosition = view.GetRowCellValue(e.RowHandle, view.Columns["CheckPosition"]).GetNullToEmpty();
                    if (checkPosition == MasterCodeSTR.MachineCheckPosition_ConfirmFlag)
                    {
                        if (!technicianFlag)
                            e.Appearance.BackColor = Color.Empty;
                        else
                            e.Appearance.BackColor = GlobalVariable.GridEditableColumnColor;
                    }
                }
            }
        }

        private string GetCheckValue(int Day, TEMP_XFMEA1003_DETAIL obj)
        {
            string returnValue = string.Empty;
            switch(Day)
            {
                case 1:
                    returnValue = obj.Day1;
                    break;
                case 2:
                    returnValue = obj.Day2;
                    break;
                case 3:
                    returnValue = obj.Day3;
                    break;
                case 4:
                    returnValue = obj.Day4;
                    break;
                case 5:
                    returnValue = obj.Day5;
                    break;
                case 6:
                    returnValue = obj.Day6;
                    break;
                case 7:
                    returnValue = obj.Day7;
                    break;
                case 8:
                    returnValue = obj.Day8;
                    break;
                case 9:
                    returnValue = obj.Day9;
                    break;
                case 10:
                    returnValue = obj.Day10;
                    break;
                case 11:
                    returnValue = obj.Day11;
                    break;
                case 12:
                    returnValue = obj.Day12;
                    break;
                case 13:
                    returnValue = obj.Day13;
                    break;
                case 14:
                    returnValue = obj.Day14;
                    break;
                case 15:
                    returnValue = obj.Day15;
                    break;
                case 16:
                    returnValue = obj.Day16;
                    break;
                case 17:
                    returnValue = obj.Day17;
                    break;
                case 18:
                    returnValue = obj.Day18;
                    break;
                case 19:
                    returnValue = obj.Day19;
                    break;
                case 20:
                    returnValue = obj.Day20;
                    break;
                case 21:
                    returnValue = obj.Day21;
                    break;
                case 22:
                    returnValue = obj.Day22;
                    break;
                case 23:
                    returnValue = obj.Day23;
                    break;
                case 24:
                    returnValue = obj.Day24;
                    break;
                case 25:
                    returnValue = obj.Day25;
                    break;
                case 26:
                    returnValue = obj.Day26;
                    break;
                case 27:
                    returnValue = obj.Day27;
                    break;
                case 28:
                    returnValue = obj.Day28;
                    break;
                case 29:
                    returnValue = obj.Day29;
                    break;
                case 30:
                    returnValue = obj.Day30;
                    break;
                case 31:
                    returnValue = obj.Day31;
                    break;
                default:
                    break;
            }

            return returnValue;
        }

        private class EditCheckList
        {
            public int CheckSeq { get; set; }
            public int Day { get; set; }
            public string Value { get; set; }
        }
    }
}