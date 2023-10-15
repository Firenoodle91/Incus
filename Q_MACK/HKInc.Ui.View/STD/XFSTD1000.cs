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

namespace HKInc.Ui.View.STD
{
    public partial class XFSTD1000 : HKInc.Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

        public XFSTD1000()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            TreeDetailGridExControl = gridEx4;

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += Detail_CellValueChanged;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += Sub_CellValueChanged;
            TreeDetailGridExControl.MainGrid.MainView.CellValueChanged += Tre_CellValueChanged;

            chk_UseYN.Checked = false;
        }          

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(false);
            TreeDetailGridExControl.SetToolbarButtonVisible(false);

            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);

            MasterGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            MasterGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeTypeName"));
            MasterGridExControl.MainGrid.AddColumn("Codemid", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("Codesub", LabelConvert.GetLabelText("MiddleCategory"));
            MasterGridExControl.MainGrid.AddColumn("Codeval", LabelConvert.GetLabelText("BottomCategory"));
            MasterGridExControl.MainGrid.AddColumn("Mcode", false);
            MasterGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));


            DetailGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            DetailGridExControl.MainGrid.AddColumn("Codemid", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("Codesub", false);
            DetailGridExControl.MainGrid.AddColumn("Codeval", false);
            DetailGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeName"));
            DetailGridExControl.MainGrid.AddColumn("Displayorder", LabelConvert.GetLabelText("DisplayOrder"));
            DetailGridExControl.MainGrid.AddColumn("Mcode", false);
            DetailGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));

            SubDetailGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            SubDetailGridExControl.MainGrid.AddColumn("Codemid", false);
            SubDetailGridExControl.MainGrid.AddColumn("Codesub", LabelConvert.GetLabelText("MiddleCategory"));
            SubDetailGridExControl.MainGrid.AddColumn("Codeval", false);
            SubDetailGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeName2"));
            SubDetailGridExControl.MainGrid.AddColumn("Displayorder", LabelConvert.GetLabelText("DisplayOrder"));
            SubDetailGridExControl.MainGrid.AddColumn("Mcode", false);
            SubDetailGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));

            TreeDetailGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            TreeDetailGridExControl.MainGrid.AddColumn("Codemid", false);
            TreeDetailGridExControl.MainGrid.AddColumn("Codesub", false);
            TreeDetailGridExControl.MainGrid.AddColumn("Codeval", LabelConvert.GetLabelText("BottomCategory"));
            TreeDetailGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeName3"));
            TreeDetailGridExControl.MainGrid.AddColumn("Displayorder", LabelConvert.GetLabelText("DisplayOrder"));
            TreeDetailGridExControl.MainGrid.AddColumn("Mcode", false);
            TreeDetailGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codemain", "Codename", "Useyn");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codemid", "Codename", "Displayorder", "Useyn");
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codesub", "Codename", "Displayorder", "Useyn");
            TreeDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codeval", "Codename", "Displayorder", "Useyn");

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
            TreeDetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemTextEdit("Codemain", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            DetailGridExControl.MainGrid.SetRepositoryItemTextEdit("Codemid", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("Codesub", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            TreeDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("Codeval", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);

            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Useyn", DbRequestHandler.GetCommCode(MasterCodeSTR.CheckYN), "Mcode", "Codename");
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Useyn", DbRequestHandler.GetCommCode(MasterCodeSTR.CheckYN), "Mcode", "Codename");
            SubDetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Useyn", DbRequestHandler.GetCommCode(MasterCodeSTR.CheckYN), "Mcode", "Codename");
            TreeDetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Useyn", DbRequestHandler.GetCommCode(MasterCodeSTR.CheckYN), "Mcode", "Codename");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("Codemain");
            DetailGridRowLocator.GetCurrentRow("Codemid");
            SubDetailGridRowLocator.GetCurrentRow("Codesub");
            TreeDetailGridRowLocator.GetCurrentRow("Codeval");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            TreeDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string commcode = tx_CommCode.EditValue.GetNullToEmpty();

            if (chk_UseYN.Checked == false)
                MasterGridBindingSource.DataSource = ModelService.GetList(p => ((string.IsNullOrEmpty(commcode) ? true : p.Codemain == commcode)
                                                                             || (string.IsNullOrEmpty(commcode) ? true : p.Codename.StartsWith(commcode)))
                                                                             && (p.Useyn == "Y")
                                                                             && (p.Codemid == "00")
                                                                             && (p.Codesub == "00")
                                                                             && (p.Codeval == "00")
                                                                                ).OrderBy(p => p.Codemain).ToList();
            else
                MasterGridBindingSource.DataSource = ModelService.GetList(p => ((string.IsNullOrEmpty(commcode) ? true : p.Codemain == commcode)
                                                                             || (string.IsNullOrEmpty(commcode) ? true : p.Codename.StartsWith(commcode)))
                                                                             && (p.Codemid == "00")
                                                                             && (p.Codesub == "00")
                                                                             && (p.Codeval == "00")
                                                                                ).OrderBy(p => p.Codemain).ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            #region Grid Focus를 위한 코드
            GridRowLocator.SetCurrentRow();
            DetailGridRowLocator.SetCurrentRow();
            SubDetailGridRowLocator.SetCurrentRow();
            TreeDetailGridRowLocator.SetCurrentRow();
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

            DataLoad();
        }

        #region 추가버튼
        protected override void AddRowClicked()
        {
            TN_STD1000 obj = new TN_STD1000();
            obj.Codemid = "00";
            obj.Codesub = "00";
            obj.Codeval = "00";
            obj.Useyn = "Y";

            ModelService.Insert(obj);
            MasterGridBindingSource.Add(obj);
        }

        protected override void DetailAddRowClicked()
        {
            TN_STD1000 MasterObj = MasterGridBindingSource.Current as TN_STD1000;

            if (MasterObj != null)
            {
                List<TN_STD1000> DetailList = DetailGridBindingSource.List as List<TN_STD1000>;

                TN_STD1000 AddObj = new TN_STD1000();
                AddObj.Codemain = MasterObj.Codemain;
                AddObj.Codemid = null;
                AddObj.Codesub = "00";
                AddObj.Codeval = "00";
                AddObj.Useyn = "Y";
                AddObj.Displayorder = DetailList.Max(p => p.Displayorder) + 1;

                ModelService.Insert(AddObj);
                DetailGridBindingSource.Add(AddObj);
                DetailGridBindingSource.MoveLast();
            }
        }

        protected override void SubDetailAddRowClicked()
        {
            TN_STD1000 DetailObj = DetailGridBindingSource.Current as TN_STD1000;
            if (DetailObj != null)
            {
                List<TN_STD1000> SubdetailList = SubDetailGridBindingSource.List as List<TN_STD1000>;

                TN_STD1000 AddObj = new TN_STD1000();
                AddObj.Codemain = DetailObj.Codemain;
                AddObj.Codemid = DetailObj.Codemid;
                AddObj.Codesub = null;
                AddObj.Codeval = "00";
                AddObj.Useyn = "Y";
                AddObj.Displayorder = SubdetailList.Max(p => p.Displayorder) + 1;

                ModelService.Insert(AddObj);
                SubDetailGridBindingSource.Add(AddObj);
                SubDetailGridBindingSource.MoveLast();
            }
        }

        protected override void TreeDetailAddRowClicked()
        {
            TN_STD1000 SubDetailObj = SubDetailGridBindingSource.Current as TN_STD1000;
            if (SubDetailObj != null)
            {
                List<TN_STD1000> TreeList = TreeDetailGridBindingSource.List as List<TN_STD1000>;

                TN_STD1000 AddObj = new TN_STD1000();

                AddObj.Codemain = SubDetailObj.Codemain;
                AddObj.Codemid = SubDetailObj.Codemid;
                AddObj.Codesub = SubDetailObj.Codesub;
                AddObj.Codeval = null;
                AddObj.Useyn = "Y";
                AddObj.Displayorder = TreeList.Max(p => p.Displayorder) + 1;

                ModelService.Insert(AddObj);
                TreeDetailGridBindingSource.Add(AddObj);
                TreeDetailGridBindingSource.MoveLast();
            }
        }
        #endregion

        #region ValueChange

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

        private void Detail_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Contains("Codemid"))
            {
                for (int i = 0; i < e.RowHandle; i++)
                {
                    if (DetailGridExControl.MainGrid.MainView.GetRowCellValue(i, "Codemid").ToString().ToUpper() == e.Value.ToString().ToUpper())
                    {
                        MessageBox.Show("이미등록된 코드입니다..");
                    }
                }

            }
            if (!e.Column.FieldName.Contains("Codename")) return;

            //값이 변경되었을 때
            //최소 최대값 있는지 판단해서 없으면 문자
            TN_STD1000 detail = DetailGridBindingSource.Current as TN_STD1000;
            if (detail == null) return;
            detail.Mcode = detail.Codemid;
        }

        private void Sub_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Contains("Codesub"))
            {
                for (int i = 0; i < e.RowHandle; i++)
                {
                    if (SubDetailGridExControl.MainGrid.MainView.GetRowCellValue(i, "Codesub").ToString().ToUpper() == e.Value.ToString().ToUpper())
                    {
                        MessageBox.Show("이미등록된 코드입니다..");
                    }
                }

            }
            if (!e.Column.FieldName.Contains("Codename")) return;

            //값이 변경되었을 때
            //최소 최대값 있는지 판단해서 없으면 문자
            TN_STD1000 detail = SubDetailGridBindingSource.Current as TN_STD1000;
            if (detail == null) return;
            detail.Mcode = detail.Codemid + detail.Codesub;
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
                    }
                }
            }

            if (!e.Column.FieldName.Contains("Codename")) return;

            //값이 변경되었을 때
            //최소 최대값 있는지 판단해서 없으면 문자
            TN_STD1000 detail = TreeDetailGridBindingSource.Current as TN_STD1000;
            if (detail == null) return;
            detail.Mcode = detail.Codemid + detail.Codesub + detail.Codeval;
        }
        #endregion

        #region FocusRowChange

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            TreeDetailGridExControl.MainGrid.Clear();

            TN_STD1000 obj = MasterGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;

            if (chk_UseYN.Checked == false)
                DetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Useyn == "Y") && (p.Codemid != "00") && (p.Codesub == "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.Displayorder).ToList();
            else
                DetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemid != "00") && (p.Codesub == "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.Displayorder).ToList();

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
        }
        protected override void DetailFocusedRowChanged()
        {
            SubDetailGridExControl.MainGrid.Clear();
            TreeDetailGridExControl.MainGrid.Clear();

            var DetailObj = DetailGridBindingSource.Current as TN_STD1000;
            if (DetailObj == null) return;

            if (chk_UseYN.Checked == false)
                SubDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Useyn == "Y") && (p.Codeval == "00") && (p.Codemid == DetailObj.Codemid) && (p.Codesub != "00") && (p.Codemain == DetailObj.Codemain)).OrderBy(p => p.Displayorder).ToList();
            else
                SubDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemid == DetailObj.Codemid) && (p.Codesub != "00") && (p.Codeval == "00") && (p.Codemain == DetailObj.Codemain)).OrderBy(p => p.Displayorder).ToList();

            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
            SubDetailGridRowLocator.SetCurrentRow();
        }

        protected override void SubDetailFocusedRowChanged()
        {
            TreeDetailGridExControl.MainGrid.Clear();
            var SubObj = SubDetailGridBindingSource.Current as TN_STD1000;
            if (SubObj == null) return;

            if (chk_UseYN.Checked == false)
                TreeDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Useyn == "Y") && (p.Codeval != "00") && (p.Codemid == SubObj.Codemid) && (p.Codesub == SubObj.Codesub) && (p.Codemain == SubObj.Codemain)).OrderBy(p => p.Displayorder).ToList();
            else
                TreeDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemid == SubObj.Codemid) && (p.Codesub == SubObj.Codesub) && (p.Codeval != "00") && (p.Codemain == SubObj.Codemain)).OrderBy(p => p.Displayorder).ToList();

            TreeDetailGridExControl.DataSource = TreeDetailGridBindingSource;
            TreeDetailGridExControl.MainGrid.BestFitColumns();
            TreeDetailGridRowLocator.SetCurrentRow();
        }
        #endregion


        #region 안쓰는 기능

        //protected override void DeleteDetailRow()
        //{
        //    TN_STD1000 obj = DetailGridBindingSource.Current as TN_STD1000;
        //    if (obj == null) return;
        //    if (obj.Codename == null)
        //    {
        //        ModelService.Delete(obj);
        //        DetailGridBindingSource.RemoveCurrent();
        //    }
        //}

        //protected override void DeleteTreeDetailRow()
        //{
        //    TN_STD1000 obj = TreeDetailGridBindingSource.Current as TN_STD1000;
        //    if (obj == null) return;

        //    if (obj.Codename == null)
        //    {
        //        ModelService.Delete(obj);
        //        TreeDetailGridBindingSource.RemoveCurrent();
        //    }
        //}

        //protected override void DeleteSubDetailRow()
        //{
        //    TN_STD1000 obj = SubDetailGridBindingSource.Current as TN_STD1000;
        //    if (obj == null) return;
        //    if (obj.Codename == null)
        //    {
        //        ModelService.Delete(obj);
        //        SubDetailGridBindingSource.RemoveCurrent();
        //    }
        //}

        //protected override void InitBindingSource()
        //{
        //    base.InitBindingSource();
        //}
        #endregion
    }
}