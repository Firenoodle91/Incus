using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Interface.Service;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;

namespace HKInc.Ui.View.SYS
{
    public partial class MasterCodeList : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<CodeMaster> MasterCodeService = (IService<CodeMaster>)ServiceFactory.GetDomainService("CodeMaster");
        string radioValue;

        private readonly List<object> FontNames = FontFamily.Families.Where(p => p.Name != "").Select(f => (object)f.Name).ToList();
        private readonly string[] Font_NameArry = new string[] { "FontName", "FontName_POP", "FontSize", "FontSize_POP_LookUp", "FontSize_POP_Grid", "FontSize_POP_Button" };
        private readonly string[] Font_SizeArry = new string[] { "FontSize", "FontSize_POP_LookUp", "FontSize_POP_Grid", "FontSize_POP_Button" };


        public MasterCodeList()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            OutPutRadioGroup = radioGroup;
            RadioGroupType = RadioGroupType.ActiveAll;
        }
        protected override void InitControls()
        {
            base.InitControls();
            MasterGridExControl.MainGrid.MainView.ShowingEditor += MasterMainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CustomRowCellEdit += MainView_CustomRowCellEdit;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
        }

        private void MasterMainView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridView gv = sender as GridView;
            var obj = MasterGridBindingSource.Current as CodeMaster;
            if (obj.GroupCodeMaster == null)
            {
                if (obj.Property1 == "Font")
                {
                    if (gv.FocusedColumn.FieldName == "Property1")
                    {
                        string Msg = "Font의 고유 코드이므로 변경이 불가합니다.";
                        MessageBoxHandler.Show(Msg, HelperFactory.GetLabelConvert().GetLabelText("Confirm"));
                        e.Cancel = true;
                    }
                }
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            var obj = DetailGridBindingSource.Current as CodeMaster;
            if (Font_SizeArry.Contains(obj.CodeName))
            {
                var FontSize = e.Value.GetDoubleNullToZero();
                if (FontSize < 8)
                {
                    obj.Property1 = "8";
                    DetailGridExControl.MainGrid.MainView.RefreshData();
                }
            }
        }

        private void MainView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridView gv = sender as GridView;
            var obj = DetailGridBindingSource.Current as CodeMaster;
            if (obj.GroupCodeMaster != null)
            {
                if (obj.GroupCodeMaster.Property1 == "Font")
                {
                    if (gv.FocusedColumn.FieldName == "Property2")
                    {
                        string Msg = "Font의 고유 코드이므로 변경이 불가합니다.";
                        MessageBoxHandler.Show(Msg, HelperFactory.GetLabelConvert().GetLabelText("Confirm"));
                        e.Cancel = true;
                    }
                }
            }

        }
        private void MainView_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "Property1") return;

            GridView gv = sender as GridView;
            string fieldValue = gv.GetRowCellValue(e.RowHandle, gv.Columns["CodeName"]).GetNullToEmpty();
            if (Font_NameArry.Contains(fieldValue))
            {
                if (fieldValue != string.Empty)
                {
                    switch (fieldValue)
                    {
                        case "FontName":
                        case "FontName_POP":
                            e.RepositoryItem = DetailGridExControl.MainGrid.SetRepositoryItemComboBox(e.Column.FieldName, FontNames);
                            break;
                        case "FontSize":
                        case "FontSize_POP_LookUp":
                        case "FontSize_POP_Grid":
                        case "FontSize_POP_Button":
                            e.RepositoryItem = DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit(e.Column.FieldName, DevExpress.Utils.DefaultBoolean.Default, "n0", true);
                            break;
                    }
                }
            }
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();            
        }
       
        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("CodeId");
            MasterGridExControl.MainGrid.AddColumn("CodeGroup", false);
            MasterGridExControl.MainGrid.AddColumn("CodeName");
            MasterGridExControl.MainGrid.AddColumn("CodeName2");
            MasterGridExControl.MainGrid.AddColumn("CodeName3");            
            MasterGridExControl.MainGrid.AddColumn("DisplayOrder", false);
            MasterGridExControl.MainGrid.AddColumn("Property1");
            MasterGridExControl.MainGrid.AddColumn("Property2");
            MasterGridExControl.MainGrid.AddColumn("Property3");
            MasterGridExControl.MainGrid.AddColumn("Property4");
            MasterGridExControl.MainGrid.AddColumn("Property5");
            MasterGridExControl.MainGrid.AddColumn("Property6");
            MasterGridExControl.MainGrid.AddColumn("Property7");
            MasterGridExControl.MainGrid.AddColumn("Property8");
            MasterGridExControl.MainGrid.AddColumn("Property9");
            MasterGridExControl.MainGrid.AddColumn("Property10");
            MasterGridExControl.MainGrid.AddColumn("GroupDescription");
            MasterGridExControl.MainGrid.AddColumn("Active");
            MasterGridExControl.MainGrid.AddColumn("DeActiveDate", false);
            MasterGridExControl.MainGrid.AddColumn("UpdateId");
            MasterGridExControl.MainGrid.AddColumn("UpdateTime");
            MasterGridExControl.MainGrid.AddColumn("UpdateClass", false);
            MasterGridExControl.MainGrid.AddColumn("CreateId");
            MasterGridExControl.MainGrid.AddColumn("CreateTime");
            MasterGridExControl.MainGrid.AddColumn("CreateClass", false);

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetEditable("CodeName", "CodeName2", "CodeName3", "GroupDescription", "Property1", "Property2", "Property3", "Property4", "Property5", "Property6", "Property7", "Property8", "Property9", "Property10", "Active");
            

            MasterGridExControl.BestFitColumns();

            //DEtail Grid
            DetailGridExControl.MainGrid.AddColumn("CodeId");
            DetailGridExControl.MainGrid.AddColumn("CodeGroup", false);
            DetailGridExControl.MainGrid.AddColumn("CodeName");
            DetailGridExControl.MainGrid.AddColumn("CodeName2");
            DetailGridExControl.MainGrid.AddColumn("CodeName3");
            DetailGridExControl.MainGrid.AddColumn("GroupDescription", false);
            DetailGridExControl.MainGrid.AddColumn("DisplayOrder");
            DetailGridExControl.MainGrid.AddColumn("Property1");
            DetailGridExControl.MainGrid.AddColumn("Property2");
            DetailGridExControl.MainGrid.AddColumn("Property3");
            DetailGridExControl.MainGrid.AddColumn("Property4");
            DetailGridExControl.MainGrid.AddColumn("Property5");
            DetailGridExControl.MainGrid.AddColumn("Property6");
            DetailGridExControl.MainGrid.AddColumn("Property7");
            DetailGridExControl.MainGrid.AddColumn("Property8");
            DetailGridExControl.MainGrid.AddColumn("Property9");
            DetailGridExControl.MainGrid.AddColumn("Property10");
            DetailGridExControl.MainGrid.AddColumn("Active");
            DetailGridExControl.MainGrid.AddColumn("DeActiveDate", false);
            DetailGridExControl.MainGrid.AddColumn("UpdateId");
            DetailGridExControl.MainGrid.AddColumn("UpdateTime");
            DetailGridExControl.MainGrid.AddColumn("UpdateClass", false);
            DetailGridExControl.MainGrid.AddColumn("CreateId");
            DetailGridExControl.MainGrid.AddColumn("CreateTime");
            DetailGridExControl.MainGrid.AddColumn("CreateClass", false);

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetEditable("CodeName", "CodeName2", "CodeName3", "DisplayOrder", "Property1", "Property2", "Property3", "Property4", "Property5", "Property6", "Property7", "Property8", "Property9", "Property10", "Active");

            
            DetailGridExControl.BestFitColumns();

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            MasterGridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            MasterGridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Active", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("UpdateTime");
            DetailGridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("CreateTime");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("CodeId");
            MasterGridExControl.MainGrid.Clear();
            
            MasterCodeService.ReLoad();

            int codeId = textCodeId.EditValue.GetIntNullToZero();
            radioValue = radioGroup.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = MasterCodeService.GetList(p => (p.CodeGroup == 0 || p.CodeGroup == null) &&
                                                                               ((codeId == 0 ? true : p.CodeId == codeId) &&
                                                                               (p.CodeName.Contains(textCodeName.Text) || p.CodeName2.Contains(textCodeName.Text) || p.CodeName3.Contains(textCodeName.Text)) &&
                                                                               (string.IsNullOrEmpty(radioValue) ? true : p.Active == radioValue)))
                                                                  .OrderBy(p=>p.CodeId)
                                                                  .ToList();
            
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();

            LoadDetail();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();            
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();
            
            MasterCodeService.Save(true);
           
            DataLoad();
        }

        #region 추상함수 구현
        protected override void AddRowClicked()
        {
            CodeMaster obj = (CodeMaster)MasterGridBindingSource.AddNew();
            obj.Active = "Y";
            obj.CodeMasterDetailList = new List<CodeMaster>();

            MasterCodeService.Insert(obj);
        }

        protected override void DeleteRow()
        {
            CodeMaster obj = MasterGridBindingSource.Current as CodeMaster;

            if (obj != null)
            {                
                if (obj.CodeMasterDetailList.Count > 0)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage(10), HelperFactory.GetLabelConvert().GetLabelText("Confirm"));
                    return;
                }                
                MasterCodeService.Delete(obj);
                MasterGridBindingSource.RemoveCurrent();
            }
        }

        protected override void DetailAddRowClicked()
        {
            CodeMaster obj = (CodeMaster)DetailGridBindingSource.AddNew();

            obj.Active = "Y";
            obj.CreateId = HKInc.Utils.Common.GlobalVariable.LoginId;
            obj.CreateTime = System.DateTime.Now;
            obj.CreateClass = HKInc.Utils.Common.GlobalVariable.CurrentInstance;
            obj.UpdateId = HKInc.Utils.Common.GlobalVariable.LoginId;
            obj.UpdateTime = System.DateTime.Now;
            obj.UpdateClass = HKInc.Utils.Common.GlobalVariable.CurrentInstance;

            CodeMaster masterObj = MasterGridBindingSource.Current as CodeMaster;
            if(masterObj != null)            
                masterObj.CodeMasterDetailList.Add(obj);            
        }

        protected override void DeleteDetailRow()
        {
            CodeMaster obj = DetailGridBindingSource.Current as CodeMaster;

            if (obj != null)
            {
                DialogResult result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage(29), "기준정보"), HelperFactory.GetLabelConvert().GetLabelText("Confirm"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.No)
                {
                    DetailGridExControl.MainGrid.MainView.SetFocusedRowCellValue("Active", "N");
                    return;
                }

                CodeMaster masterObj = MasterGridBindingSource.Current as CodeMaster;
                if (masterObj != null)
                    masterObj.CodeMasterDetailList.Remove(obj);

                if (obj.CodeId.GetNullToZero() != 0) MasterCodeService.Delete(obj);
                DetailGridBindingSource.RemoveCurrent();
            }
        }

        protected override void MasterFocusedRowChanged()
        {
            LoadDetail();
        }
        #endregion
        private void LoadDetail()
        {
            CodeMaster obj = MasterGridBindingSource.Current as CodeMaster;
            if (obj != null)
            {
                DetailGridBindingSource.DataSource = obj.CodeMasterDetailList
                                                        .Where(p=> (string.IsNullOrEmpty(radioValue) ? true : p.Active == radioValue))
                                                        .OrderBy(p=>p.CodeId).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;

                #region Font RepositoryItem Change
                if (obj.Property1 != "Font")
                {
                    var text = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
                    text.MaxLength = 0;
                    text.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
                    DetailGridExControl.MainGrid.Columns["Property1"].ColumnEdit = text;
                }
                #endregion
            }
            DetailGridExControl.BestFitColumns();
        }
        private void GridBindingSource_CurrentItemChanged(object sender, System.EventArgs e)
        {
            IsFormControlChanged = true;
        }
    }
}