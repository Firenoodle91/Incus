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

namespace HKInc.Ui.View.STD
{
    public partial class XFSTD1000 : HKInc.Service.Base.ListMasterDetailDetailDetailFormTemplate
    {
        IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

        protected HKInc.Service.Controls.GridEx GridExControlExcel;

        public XFSTD1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            TreeDetailGridExControl = gridEx4;
        }

        protected override void InitControls()
        {
            base.InitControls();
            chk_UseYN.Checked = false;

            SubDetailGridExControl.MainGrid.MainView.FocusedRowChanged += SubMainView_FocusedRowChanged;

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += Drtail_CellValueChanged;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += Sub_CellValueChanged;
            TreeDetailGridExControl.MainGrid.MainView.CellValueChanged += Tre_CellValueChanged;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            MasterGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeTypeName"));
            MasterGridExControl.MainGrid.AddColumn("Codemid", LabelConvert.GetLabelText("TopCategory"), false);
            MasterGridExControl.MainGrid.AddColumn("Codesub", LabelConvert.GetLabelText("BottomCategory"), false);
            MasterGridExControl.MainGrid.AddColumn("Codeval", LabelConvert.GetLabelText("BottomCategory"), false);
            MasterGridExControl.MainGrid.AddColumn("Mcode", false);
            MasterGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codemain", "Codename", "Useyn");
            MasterGridExControl.MainGrid.SetHeaderColor(Color.Red, "Codemain");

            DetailGridExControl.MainGrid.Init();
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            DetailGridExControl.MainGrid.AddColumn("Codemid", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("Codesub",  false);
            DetailGridExControl.MainGrid.AddColumn("Codeval",  false);
            DetailGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeName"));
            DetailGridExControl.MainGrid.AddColumn("Displayorder", LabelConvert.GetLabelText("DisplayOrder"));
            DetailGridExControl.MainGrid.AddColumn("Mcode", false);
            DetailGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codemid", "Codename", "Displayorder", "Useyn");
            DetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "Codemid");

            SubDetailGridExControl.MainGrid.Init();
            SubDetailGridExControl.SetToolbarButtonVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            SubDetailGridExControl.MainGrid.AddColumn("Codemid", false);
            SubDetailGridExControl.MainGrid.AddColumn("Codesub", LabelConvert.GetLabelText("MiddleCategory"));
            SubDetailGridExControl.MainGrid.AddColumn("Codeval",  false);
            SubDetailGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeName2"));
            SubDetailGridExControl.MainGrid.AddColumn("Displayorder", LabelConvert.GetLabelText("DisplayOrder"));
            SubDetailGridExControl.MainGrid.AddColumn("Mcode", false);
            SubDetailGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codesub", "Codename", "Displayorder", "Useyn");
            SubDetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "Codesub");

            TreeDetailGridExControl.MainGrid.Init();
            TreeDetailGridExControl.SetToolbarButtonVisible(false);
            TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            TreeDetailGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            TreeDetailGridExControl.MainGrid.AddColumn("Codemid",  false);
            TreeDetailGridExControl.MainGrid.AddColumn("Codesub",  false);
            TreeDetailGridExControl.MainGrid.AddColumn("Codeval", LabelConvert.GetLabelText("BottomCategory"));
            TreeDetailGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeName3"));
            TreeDetailGridExControl.MainGrid.AddColumn("Displayorder", LabelConvert.GetLabelText("DisplayOrder"));
            TreeDetailGridExControl.MainGrid.AddColumn("Mcode", false);
            TreeDetailGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));
            TreeDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codeval", "Codename", "Displayorder", "Useyn");
            SubDetailGridExControl.MainGrid.SetHeaderColor(Color.Red, "Codeval");

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
            TreeDetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            List<Temp> objs = new List<Temp>();
            Temp obj = new Temp();
            obj.CodeId = "Y";
            obj.CodeName = "사용";
            Temp obj1 = new Temp();
            obj1.CodeId = "N";
            obj1.CodeName = "미사용";
            objs.Add(obj);
            objs.Add(obj1);

            MasterGridExControl.MainGrid.SetRepositoryItemTextEdit("Codemain", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Useyn", objs);

            DetailGridExControl.MainGrid.SetRepositoryItemTextEdit("Codemid", 3, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Useyn", objs);

            SubDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("Codesub", 3, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Useyn", objs);

            TreeDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("Codeval", 3, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            TreeDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Useyn", objs);
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            DetailGridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_2));
            SubDetailGridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_3));
            TreeDetailGridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_4));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            PopupDataParam.SetValue(PopupParameter.GridRowId_2, null);
            PopupDataParam.SetValue(PopupParameter.GridRowId_3, null);
            PopupDataParam.SetValue(PopupParameter.GridRowId_4, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            TreeDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string commcode = tx_CommCode.EditValue.GetNullToEmpty();
            if (chk_UseYN.Checked == false)
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemain.Contains(commcode) || p.Codename.Contains(commcode)) 
                                                                             && (p.Useyn == "Y") 
                                                                             && (p.Codemid == "00") 
                                                                             && (p.Codesub == "00") 
                                                                             && (p.Codeval == "00")
                                                                          )
                                                                          .OrderBy(p => p.Codemain)
                                                                          .ToList();
            }
            else
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemain.Contains(commcode) || p.Codename.Contains(commcode)) 
                                                                            && (p.Codemid == "00") 
                                                                            && (p.Codesub == "00") 
                                                                            && (p.Codeval == "00")
                                                                          )
                                                                          .OrderBy(p => p.Codemain).ToList();
            }
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            #region Grid Focus를 위한 코드
            GridRowLocator.SetCurrentRow();
            #endregion
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();
            TreeDetailGridExControl.MainGrid.PostEditor();
            TreeDetailGridBindingSource.EndEdit();

            ModelService.Save();

            #region Grid Focus를 위한 코드
            var MObj = PopupDataParam.GetValue(PopupParameter.GridRowId_1) as TN_STD1000;
            var DObj = PopupDataParam.GetValue(PopupParameter.GridRowId_2) as TN_STD1000;
            var SObj = PopupDataParam.GetValue(PopupParameter.GridRowId_3) as TN_STD1000;
            var TObj = PopupDataParam.GetValue(PopupParameter.GridRowId_4) as TN_STD1000;
            #endregion

            DataLoad();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            TreeDetailGridExControl.MainGrid.Clear();
            TN_STD1000 obj = MasterGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;
            if (chk_UseYN.Checked == false)
            {                
                DetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Useyn == "Y") && (p.Codemid != "00") && (p.Codesub == "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            else
            {             
                DetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemid != "00") && (p.Codesub == "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void DetailFocusedRowChanged()
        {
            SubDetailGridExControl.MainGrid.Clear();
            TreeDetailGridExControl.MainGrid.Clear();

            TN_STD1000 subobj = SubDetailGridBindingSource.Current as TN_STD1000;
            if (subobj != null && subobj.Codesub.ToString() == "") return;

            TN_STD1000 obj = DetailGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;
            if (chk_UseYN.Checked == false)
            {                
                SubDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Useyn == "Y") && (p.Codeval == "00") && (p.Codemid == obj.Codemid) && (p.Codesub != "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            else
            {                
                SubDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemid == obj.Codemid) && (p.Codesub != "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
            SubDetailGridRowLocator.SetCurrentRow();
        }

        protected override void AddRowClicked()
        {
            TN_STD1000 obj = new TN_STD1000();
            obj.Codemid = "00";
            obj.Codesub = "00";
            obj.Codeval = "00";
            obj.Useyn = "Y";

            ModelService.Insert(obj);
            MasterGridBindingSource.Add(obj);
            MasterGridBindingSource.MoveLast();

            #region Grid Focus를 위한 코드
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, obj);
            #endregion
        }

        protected override void DetailAddRowClicked()
        {
            TN_STD1000 obj1 = MasterGridBindingSource.Current as TN_STD1000;

            if (obj1 != null)
            {
                int cnt = ModelService.GetList(p => (p.Codemain == obj1.Codemain)).ToList().Count() + 1;
                TN_STD1000 obj = (TN_STD1000)DetailGridBindingSource.AddNew();
                obj.Codemain = obj1.Codemain;
                //obj.Codemid = cnt.ToString();
                obj.Codesub = "00";
                obj.Codeval = "00";
                obj.Useyn = "Y";

                ModelService.Insert(obj);
                DetailGridBindingSource.EndEdit();

                #region Grid Focus를 위한 코드
                PopupDataParam.SetValue(PopupParameter.GridRowId_2, obj);
                #endregion
            }

        }

        protected override void DeleteDetailRow()
        {
            TN_STD1000 obj = DetailGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;
            if (obj.Codename == null)
            {
                ModelService.Delete(obj);
                DetailGridBindingSource.RemoveCurrent();
            }


        }

        protected override void SubDetailAddRowClicked()
        {
            TN_STD1000 obj1 = DetailGridBindingSource.Current as TN_STD1000;

            if (obj1 != null)
            {
                int cnt = ModelService.GetList(p => (p.Codemain == obj1.Codemain && p.Codemid == obj1.Codemid)).ToList().Count() + 1;
                TN_STD1000 obj = new TN_STD1000();
                obj.Codemain = obj1.Codemain;
                obj.Codemid = obj1.Codemid;
                //obj.Codesub = cnt.ToString();
                obj.Codeval = "00";
                obj.Useyn = "Y";

                ModelService.Insert(obj);
                SubDetailGridBindingSource.Add(obj);
                SubDetailGridBindingSource.MoveLast();

                #region Grid Focus를 위한 코드
                PopupDataParam.SetValue(PopupParameter.GridRowId_3, obj);
                #endregion
            }
        }

        protected override void DeleteSubDetailRow()
        {
            TN_STD1000 obj = SubDetailGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;
            if (obj.Codename == null)
            {
                ModelService.Delete(obj);
                SubDetailGridBindingSource.RemoveCurrent();
            }
        }

        protected override void TreeDetailAddRowClicked()
        {
            TN_STD1000 obj1 = SubDetailGridBindingSource.Current as TN_STD1000;

            if (obj1 != null)
            {
                int cnt = ModelService.GetList(p => (p.Codemain == obj1.Codemain && p.Codemid == obj1.Codemid && p.Codesub == obj1.Codesub)).ToList().Count() + 1;
                TN_STD1000 obj = new TN_STD1000();
                obj.Codemain = obj1.Codemain;
                obj.Codemid = obj1.Codemid;
                obj.Codesub = obj1.Codesub;
                //obj.Codeval = cnt.ToString();
                obj.Useyn = "Y";
                ModelService.Insert(obj);
                TreeDetailGridBindingSource.Add(obj);
                TreeDetailGridBindingSource.MoveLast();

                #region Grid Focus를 위한 코드
                PopupDataParam.SetValue(PopupParameter.GridRowId_4, obj);
                #endregion
            }
        }

        protected override void DeleteTreeDetailRow()
        {
            TN_STD1000 obj = TreeDetailGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;


            if (obj.Codename == null)
            {
                ModelService.Delete(obj);
                TreeDetailGridBindingSource.RemoveCurrent();
            }
        }

        private void SubMainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            TreeDetailGridExControl.MainGrid.Clear();

            TN_STD1000 tresubobj = TreeDetailGridBindingSource.Current as TN_STD1000;
            if (tresubobj != null && tresubobj.Codeval.ToString() == "") return;

            TN_STD1000 obj = SubDetailGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;
            if (chk_UseYN.Checked == false)
            {                
                TreeDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Useyn == "Y") && (p.Codeval != "00") && (p.Codemid == obj.Codemid) && (p.Codesub == obj.Codesub) && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            else
            {                
                TreeDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemid == obj.Codemid) && (p.Codesub == obj.Codesub) && (p.Codeval != "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            TreeDetailGridExControl.DataSource = TreeDetailGridBindingSource;
            TreeDetailGridExControl.BestFitColumns();
            TreeDetailGridRowLocator.SetCurrentRow();
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (!e.Column.FieldName.Contains("Codemain")) return;
            for (int i = 0; i < e.RowHandle; i++)
            {
                if (MasterGridExControl.MainGrid.MainView.GetRowCellValue(i, "Codemain").ToString().ToUpper() == e.Value.ToString().ToUpper())
                {
                    MessageBox.Show("이미등록된 코드입니다..");
                }
            }
        }

        private void Drtail_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Codemid")
            {
                for (int i = 0; i < e.RowHandle; i++)
                {
                    if (DetailGridExControl.MainGrid.MainView.GetRowCellValue(i, "Codemid") != null)
                    {
                        if (DetailGridExControl.MainGrid.MainView.GetRowCellValue(i, "Codemid").ToString().ToUpper() == e.Value.ToString().ToUpper())
                        {
                            MessageBox.Show("이미등록된 코드입니다..");
                            return;
                        }
                    }
                }

                //값이 변경되었을 때
                //최소 최대값 있는지 판단해서 없으면 문자
                TN_STD1000 detail = DetailGridBindingSource.Current as TN_STD1000;
                if (detail == null) return;
                detail.Mcode = detail.Codemid;

            }
        }

        private void Sub_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Codesub")
            {
                for (int i = 0; i < e.RowHandle; i++)
                {
                    if (SubDetailGridExControl.MainGrid.MainView.GetRowCellValue(i, "Codesub").ToString().ToUpper() == e.Value.ToString().ToUpper())
                    {
                        MessageBox.Show("이미등록된 코드입니다..");
                        return;
                    }
                }

                //값이 변경되었을 때
                //최소 최대값 있는지 판단해서 없으면 문자
                TN_STD1000 detail = SubDetailGridBindingSource.Current as TN_STD1000;
                if (detail == null) return;
                detail.Mcode = detail.Codemid + detail.Codesub;
            }
        }

        private void Tre_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Contains("Codeval"))
            {
                for (int i = 0; i < e.RowHandle; i++)
                {
                    if (TreeDetailGridExControl.MainGrid.MainView.GetRowCellValue(i, "Codeval").ToString().ToUpper() == e.Value.ToString().ToUpper())
                    {
                        MessageBox.Show("이미등록된 코드입니다..");
                        return;
                    }
                }

                //값이 변경되었을 때
                //최소 최대값 있는지 판단해서 없으면 문자
                TN_STD1000 detail = TreeDetailGridBindingSource.Current as TN_STD1000;
                if (detail == null) return;
                detail.Mcode = detail.Codemid + detail.Codesub + detail.Codeval;
            }
        }
    }
}