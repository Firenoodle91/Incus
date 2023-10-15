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
    public partial class XFSTD1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
         IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");
        protected BindingSource SubDetailGridBindingSource = new BindingSource();
        protected HKInc.Service.Controls.GridEx SubDetailGridExControl;
        protected BindingSource TreSubDetailGridBindingSource = new BindingSource();
        protected HKInc.Service.Controls.GridEx TreSubDetailGridExControl;
        protected HKInc.Service.Controls.GridEx GridExControlExcel;
        private HKInc.Utils.Interface.Helper.IGridRowLocator SubDetailGridRowLocator;
        private HKInc.Utils.Interface.Helper.IGridRowLocator TreSubDetailGridRowLocator;
        public XFSTD1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
         
            SubDetailGridExControl.ActAddRowClicked += GridEx3_ActAddRowClicked;
            SubDetailGridExControl.ActDeleteRowClicked += SubDetailGridExControl_ActDeleteRowClicked;
            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += DetailView_FocusedRowChanged;
            TreSubDetailGridExControl = gridEx4;
            TreSubDetailGridExControl.ActAddRowClicked += TreSubDetailGridExControl_ActAddRowClicked;
            TreSubDetailGridExControl.ActDeleteRowClicked += TreSubDetailGridExControl_ActDeleteRowClicked;
            SubDetailGridExControl.MainGrid.MainView.FocusedRowChanged += SubMainView_FocusedRowChanged;


            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += Drtail_CellValueChanged;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += Sub_CellValueChanged;
            TreSubDetailGridExControl.MainGrid.MainView.CellValueChanged += Tre_CellValueChanged;

            SubDetailGridRowLocator = HelperFactory.GetGridRowLocator(SubDetailGridExControl.MainGrid.MainView);
            TreSubDetailGridRowLocator = HelperFactory.GetGridRowLocator(TreSubDetailGridExControl.MainGrid.MainView);
         
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

        private void TreSubDetailGridExControl_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            TN_STD1000 obj = TreSubDetailGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;

           
            if (obj.Codename == null)
            {
                ModelService.Delete(obj);
                TreSubDetailGridBindingSource.RemoveCurrent();
            }
        }

        private void SubDetailGridExControl_ActDeleteRowClicked(object sender, ItemClickEventArgs e)
        {
            TN_STD1000 obj = SubDetailGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;
            if (obj.Codename == null)
            {
                ModelService.Delete(obj);
                SubDetailGridBindingSource.RemoveCurrent();
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
        private void Tre_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {

            if (e.Column.FieldName.Contains("Codeval"))
            {
                for (int i = 0; i < e.RowHandle; i++)
                {
                    if (TreSubDetailGridExControl.MainGrid.MainView.GetRowCellValue(i, "Codeval").ToString().ToUpper() == e.Value.ToString().ToUpper())
                    {
                        MessageBox.Show("이미등록된 코드입니다..");
                    }
                }

            }


            if (!e.Column.FieldName.Contains("Codename")) return;

            //값이 변경되었을 때
            //최소 최대값 있는지 판단해서 없으면 문자
            TN_STD1000 detail = TreSubDetailGridBindingSource.Current as TN_STD1000;
            if (detail == null) return;
            detail.Mcode = detail.Codemid + detail.Codesub + detail.Codeval;
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

        private void Drtail_CellValueChanged(object sender, CellValueChangedEventArgs e)
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
        protected override void InitBindingSource()
        {
            base.InitBindingSource();
        }
        protected override void InitControls()
        {

            base.InitControls();
            chk_UseYN.Checked = false;
        }
        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.Init();



            MasterGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
         

            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
           // DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            TreSubDetailGridExControl.SetToolbarButtonVisible(false);
            TreSubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //TreSubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            MasterGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeTypeName"));
            MasterGridExControl.MainGrid.AddColumn("Codemid", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("Codesub", LabelConvert.GetLabelText("BottomCategory"));
            MasterGridExControl.MainGrid.AddColumn("Codeval", LabelConvert.GetLabelText("BottomCategory"));
            MasterGridExControl.MainGrid.AddColumn("Mcode", false);
            MasterGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));


            DetailGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            DetailGridExControl.MainGrid.AddColumn("Codemid", LabelConvert.GetLabelText("TopCategory"));
            DetailGridExControl.MainGrid.AddColumn("Codesub",  false);
            DetailGridExControl.MainGrid.AddColumn("Codeval",  false);
            DetailGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeName"));
            DetailGridExControl.MainGrid.AddColumn("Displayorder", LabelConvert.GetLabelText("DisplayOrder"));
            DetailGridExControl.MainGrid.AddColumn("Mcode", false);
            DetailGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));

            SubDetailGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            SubDetailGridExControl.MainGrid.AddColumn("Codemid", false);
            SubDetailGridExControl.MainGrid.AddColumn("Codesub", LabelConvert.GetLabelText("MiddleCategory"));
            SubDetailGridExControl.MainGrid.AddColumn("Codeval",  false);
            SubDetailGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeName2"));
            SubDetailGridExControl.MainGrid.AddColumn("Displayorder", LabelConvert.GetLabelText("DisplayOrder"));
            SubDetailGridExControl.MainGrid.AddColumn("Mcode", false);
            SubDetailGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));

            TreSubDetailGridExControl.MainGrid.AddColumn("Codemain", LabelConvert.GetLabelText("CodeType"));
            TreSubDetailGridExControl.MainGrid.AddColumn("Codemid",  false);
            TreSubDetailGridExControl.MainGrid.AddColumn("Codesub",  false);
            TreSubDetailGridExControl.MainGrid.AddColumn("Codeval", LabelConvert.GetLabelText("BottomCategory"));
            TreSubDetailGridExControl.MainGrid.AddColumn("Codename", LabelConvert.GetLabelText("CodeName3"));
            TreSubDetailGridExControl.MainGrid.AddColumn("Displayorder", LabelConvert.GetLabelText("DisplayOrder"));
            TreSubDetailGridExControl.MainGrid.AddColumn("Mcode", false);
            TreSubDetailGridExControl.MainGrid.AddColumn("Useyn", LabelConvert.GetLabelText("UseYn"));

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codemain", "Codename", "Useyn");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codemid", "Codename", "Displayorder", "Useyn");
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codesub", "Codename", "Displayorder", "Useyn");
            TreSubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Codeval", "Codename", "Displayorder", "Useyn");

        

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
            TreSubDetailGridExControl.BestFitColumns();
        }
        protected override void InitRepository()
        {
            base.InitRepository();
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
            DetailGridExControl.MainGrid.SetRepositoryItemTextEdit("Codemid", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("Codesub", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            TreSubDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("Codeval", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            MasterGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Useyn", objs);
            DetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Useyn", objs);
            SubDetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Useyn", objs);
            TreSubDetailGridExControl.MainGrid.SetRepositoryItemLookUpEdit("Useyn", objs);
        }
        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));
            DetailGridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_2));
            SubDetailGridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_3));
            TreSubDetailGridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_4));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            PopupDataParam.SetValue(PopupParameter.GridRowId_2, null);
            PopupDataParam.SetValue(PopupParameter.GridRowId_3, null);
            PopupDataParam.SetValue(PopupParameter.GridRowId_4, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            TreSubDetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            string commcode = tx_CommCode.EditValue.GetNullToEmpty();
            if (chk_UseYN.Checked == false)
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => ((string.IsNullOrEmpty(commcode) ? true : p.Codemain == commcode) || (string.IsNullOrEmpty(commcode) ? true : p.Codename.StartsWith(commcode))) && (p.Useyn == "Y") && (p.Codemid == "00") && (p.Codesub == "00") && (p.Codeval == "00")).OrderBy(p => p.Codemain).ToList();
            }
            else
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => ((string.IsNullOrEmpty(commcode) ? true : p.Codemain == commcode) || (string.IsNullOrEmpty(commcode) ? true : p.Codename.StartsWith(commcode))) && (p.Codemid == "00") && (p.Codesub == "00") && (p.Codeval == "00")).OrderBy(p => p.Codemain).ToList();
            }
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            #region Grid Focus를 위한 코드
            GridRowLocator.SetCurrentRow();
            #endregion
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }
        protected override void MasterFocusedRowChanged()
        {


            //DetailGridExControl.DataSource = null;
            //SubDetailGridExControl.DataSource = null;
            //TreSubDetailGridExControl.DataSource = null;

            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            TreSubDetailGridExControl.MainGrid.Clear();

            TN_STD1000 obj = MasterGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;
            if (chk_UseYN.Checked == false)
            {
                //DetailGridBindingSource.DataSource = ModelServiceSub.GetList(p => (p.Useyn == "Y") && (p.Codemid != "00") && (p.Codesub == "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.Displayorder).ToList();
                DetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Useyn == "Y") && (p.Codemid != "00") && (p.Codesub == "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            else
            {
                //DetailGridBindingSource.DataSource = ModelServiceSub.GetList(p =>  (p.Codemid != "00")&&(p.Codesub == "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.Displayorder).ToList();
                DetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemid != "00") && (p.Codesub == "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
        }
        private void DetailView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            SubDetailGridExControl.MainGrid.Clear();
            TreSubDetailGridExControl.MainGrid.Clear();

            //TN_STD1000 subobj = SubDetailGridBindingSource.Current as TN_STD1000;
            //if (subobj != null)
            //{
            //    if (subobj.Codesub.ToString() == "")
            //    {
            //        //   MasterGridBindingSource.MovePrevious();
            //        return;
            //    }
            //}

            //SubDetailGridExControl.DataSource = null;
            //TreSubDetailGridExControl.DataSource = null;
            TN_STD1000 obj = DetailGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;
            if (chk_UseYN.Checked == false)
            {
                //SubDetailGridBindingSource.DataSource = ModelServiceSubDtl.GetList(p => (p.Useyn == "Y") && (p.Codeval == "00") && (p.Codemid == obj.Codemid) && (p.Codesub != "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.Displayorder).ToList();
                SubDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Useyn == "Y") && (p.Codeval == "00") && (p.Codemid == obj.Codemid) && (p.Codesub != "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            else
            {
                //SubDetailGridBindingSource.DataSource = ModelServiceSubDtl.GetList(p => (p.Codemid == obj.Codemid) && (p.Codesub != "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.Displayorder).ToList();
                SubDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemid == obj.Codemid) && (p.Codesub != "00") && (p.Codeval == "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
            SubDetailGridRowLocator.SetCurrentRow();
        }
        private void SubMainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            TreSubDetailGridExControl.MainGrid.Clear();

            //TN_STD1000 tresubobj = TreSubDetailGridBindingSource.Current as TN_STD1000;
            //if (tresubobj != null)
            //{
            //    if (tresubobj.Codeval.ToString() == "")
            //    {
            //        //   MasterGridBindingSource.MovePrevious();
            //        return;
            //    }
            //}

            //TreSubDetailGridExControl.DataSource = null;
            TN_STD1000 obj = SubDetailGridBindingSource.Current as TN_STD1000;
            if (obj == null) return;
            if (chk_UseYN.Checked == false)
            {
                //TreSubDetailGridBindingSource.DataSource = ModelServiceSubTre.GetList(p => (p.Useyn == "Y") && (p.Codeval != "00") && (p.Codemid == obj.Codemid) && (p.Codesub == obj.Codesub) && (p.Codemain == obj.Codemain)).OrderBy(p => p.Displayorder).ToList();
                TreSubDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Useyn == "Y") && (p.Codeval != "00") && (p.Codemid == obj.Codemid) && (p.Codesub == obj.Codesub) && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            else
            {
                //TreSubDetailGridBindingSource.DataSource = ModelServiceSubTre.GetList(p => (p.Codemid == obj.Codemid) && (p.Codesub == obj.Codesub) && (p.Codeval != "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.Displayorder).ToList();
                TreSubDetailGridBindingSource.DataSource = ModelService.GetList(p => (p.Codemid == obj.Codemid) && (p.Codesub == obj.Codesub) && (p.Codeval != "00") && (p.Codemain == obj.Codemain)).OrderBy(p => p.displayorder).ToList();
            }
            TreSubDetailGridExControl.DataSource = TreSubDetailGridBindingSource;
            TreSubDetailGridExControl.MainGrid.BestFitColumns();
            TreSubDetailGridRowLocator.SetCurrentRow();
        }
        protected override void AddRowClicked()
        {

            //{
            //if (!UserRight.HasEdit) return;
            //        TrunkInspMst obj1 = MasterTrunkeyBindingSource.Current as TrunkInspMst;

            TN_STD1000 obj = new TN_STD1000();
            obj.Codemid = "00";
            obj.Codesub = "00";
            obj.Codeval = "00";
            obj.Useyn = "Y";

            ModelService.Insert(obj);

            MasterGridBindingSource.Add(obj);
            //}
            MasterGridBindingSource.MoveLast();

            //}
            //catch { }

            #region Grid Focus를 위한 코드
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, obj);
            #endregion
        }
        protected override void DetailAddRowClicked()
        {
            // if (!UserRight.HasEdit) return;

            TN_STD1000 obj1 = MasterGridBindingSource.Current as TN_STD1000;

            if (obj1 != null)
            {
                List<TN_STD1000> dobj = DetailGridBindingSource.DataSource as List<TN_STD1000>;
                string b = "";
                int i=0;
                if (dobj.Count != 0)
                {
                    string pcode = dobj.Where(p => p.Codemain == obj1.Codemain).OrderByDescending(o => Convert.ToInt32(o.Codemid.Substring(1, o.Codemid.Length-1))).FirstOrDefault().Codemid.ToString();
                    //ModelService.GetList(p => (p.Codemain == obj1.Codemain)).OrderByDescending(o => o.Codemid).FirstOrDefault().Codemid.ToString();
                    string a = pcode.Substring(1, pcode.Length - 1);
                    i = Convert.ToInt32(a) + 1;
                    b = pcode.Substring(0, 1) + new string('0',(a.Length - Convert.ToString(i).Length)<=0?0: a.Length - Convert.ToString(i).Length) + Convert.ToString(i);
                }
                else { b = "A00"; }
                // MessageBox.Show(pcode.Substring(0,1)+b);
                //int cnt = ModelService.GetList(p => (p.Codemain == obj1.Codemain)).ToList().Count() + 1;
                TN_STD1000 obj = (TN_STD1000)DetailGridBindingSource.AddNew();
                obj.Codemain = obj1.Codemain;
                obj.Codemid =  b;// cnt.ToString();
                obj.Codesub = "00";
                obj.Codeval = "00";
                obj.Useyn = "Y";
                obj.Displayorder = i.ToString();

                //ModelServiceSub.Insert(obj);
                ModelService.Insert(obj);
                DetailGridBindingSource.EndEdit();

                #region Grid Focus를 위한 코드
                PopupDataParam.SetValue(PopupParameter.GridRowId_2, obj);
                #endregion

                //DetailGridBindingSource.Add(obj);
                ////}
                //DetailGridBindingSource.MoveLast();
            }
        }
        private void GridEx3_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            //   if (!UserRight.HasEdit) return;
            TN_STD1000 obj1 = DetailGridBindingSource.Current as TN_STD1000;

            if (obj1 != null)
            {

                List<TN_STD1000> dobj = SubDetailGridBindingSource.DataSource as List<TN_STD1000>;
                string b = "";
                int i = 0;
                if (dobj.Count != 0)
                {
                    string pcode = dobj.Where(p => p.Codemain == obj1.Codemain && p.Codemid == obj1.Codemid).OrderByDescending(o => Convert.ToInt32(o.Codesub.Substring(1, o.Codesub.Length - 1))).FirstOrDefault().Codesub.ToString();
                    //ModelService.GetList(p => (p.Codemain == obj1.Codemain)).OrderByDescending(o => o.Codemid).FirstOrDefault().Codemid.ToString();
                    string a = pcode.Substring(1, pcode.Length - 1);
                     i = Convert.ToInt32(a) + 1;
                    b = pcode.Substring(0, 1) + new string('0', a.Length - Convert.ToString(i).Length) + Convert.ToString(i);
                }
                else { b = "A00"; }
                //  int cnt = ModelService.GetList(p => (p.Codemain == obj1.Codemain && p.Codemid == obj1.Codemid)).ToList().Count() + 1;
                TN_STD1000 obj = new TN_STD1000();
                obj.Codemain = obj1.Codemain;
                obj.Codemid = obj1.Codemid;
                obj.Codesub = b;// cnt.ToString();
                obj.Codeval = "00";
                obj.Useyn = "Y";
                obj.Displayorder = i.ToString();

                ModelService.Insert(obj);
                //ModelServiceSubDtl.Insert(obj);
                SubDetailGridBindingSource.Add(obj);
                //}
                SubDetailGridBindingSource.MoveLast();


                #region Grid Focus를 위한 코드
                PopupDataParam.SetValue(PopupParameter.GridRowId_3, obj);
                #endregion
            }
        }
        private void TreSubDetailGridExControl_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            // if (!UserRight.HasEdit) return;
            TN_STD1000 obj1 = SubDetailGridBindingSource.Current as TN_STD1000;

            if (obj1 != null)
            {
                List<TN_STD1000> dobj = TreSubDetailGridBindingSource.DataSource as List<TN_STD1000>;
                string b = "";
                int i = 0;
                if (dobj.Count != 0)
                {
                    string pcode = dobj.Where(p => p.Codemain == obj1.Codemain && p.Codemid == obj1.Codemid && p.Codesub == obj1.Codesub).OrderByDescending(o => Convert.ToInt32(o.Codeval.Substring(1, o.Codeval.Length - 1))).FirstOrDefault().Codeval.ToString();
                    //ModelService.GetList(p => (p.Codemain == obj1.Codemain)).OrderByDescending(o => o.Codemid).FirstOrDefault().Codemid.ToString();
                    string a = pcode.Substring(1, pcode.Length - 1);
                    i = Convert.ToInt32(a) + 1;
                    b = pcode.Substring(0, 1) + new string('0', a.Length - Convert.ToString(i).Length) + Convert.ToString(i);
                }
                else { b = "A00"; }
              //  int cnt = ModelService.GetList(p => (p.Codemain == obj1.Codemain && p.Codemid == obj1.Codemid && p.Codesub == obj1.Codesub)).ToList().Count() + 1;
                TN_STD1000 obj = new TN_STD1000();
                obj.Codemain = obj1.Codemain;
                obj.Codemid = obj1.Codemid;
                obj.Codesub = obj1.Codesub;
                obj.Codeval = b;// cnt.ToString();
                obj.Useyn = "Y";
                obj.Displayorder = i.ToString();

                ModelService.Insert(obj);
                // ModelServiceSubTre.Insert(obj);
                TreSubDetailGridBindingSource.Add(obj);
                //}
                TreSubDetailGridBindingSource.MoveLast();

                #region Grid Focus를 위한 코드
                PopupDataParam.SetValue(PopupParameter.GridRowId_4, obj);
                #endregion
            }
        }
        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();
            DetailGridBindingSource.EndEdit();
            SubDetailGridExControl.MainGrid.PostEditor();
            SubDetailGridBindingSource.EndEdit();
            TreSubDetailGridExControl.MainGrid.PostEditor();
            TreSubDetailGridBindingSource.EndEdit();

            ModelService.Save();

            #region Grid Focus를 위한 코드
            var MObj = PopupDataParam.GetValue(PopupParameter.GridRowId_1) as TN_STD1000;
            var DObj = PopupDataParam.GetValue(PopupParameter.GridRowId_2) as TN_STD1000;
            var SObj = PopupDataParam.GetValue(PopupParameter.GridRowId_3) as TN_STD1000;
            var TObj = PopupDataParam.GetValue(PopupParameter.GridRowId_4) as TN_STD1000;
            #endregion

            DataLoad();
        }

       
    }
}