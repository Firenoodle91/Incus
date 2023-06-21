using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using DevExpress.XtraGrid.Views.Base;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.View.STD
{
    public partial class XFSTD1000 : HKInc.Service.Base.ListMasterDetailSubDetailTreeDetailFormTemplate
    {
         IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

        public XFSTD1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            TreeDetailGridExControl = gridEx4;

            //DetailGridExControl.MainGrid.MainView.FocusedRowChanged += DetailView_FocusedRowChanged;
            SubDetailGridExControl.MainGrid.MainView.FocusedRowChanged += SubMainView_FocusedRowChanged;

            MasterGridExControl.MainGrid.MainView.CellValueChanged += Master_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CellValueChanged += Detail_CellValueChanged;
            SubDetailGridExControl.MainGrid.MainView.CellValueChanged += Sub_CellValueChanged;
            TreeDetailGridExControl.MainGrid.MainView.CellValueChanged += Tree_CellValueChanged;
            
            SubDetailGridRowLocator = HelperFactory.GetGridRowLocator(SubDetailGridExControl.MainGrid.MainView);
            TreeDetailGridRowLocator = HelperFactory.GetGridRowLocator(TreeDetailGridExControl.MainGrid.MainView);
        }

        protected override void InitCombo()
        {
            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            SubDetailGridExControl.SetToolbarButtonVisible(false);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            SubDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            TreeDetailGridExControl.SetToolbarButtonVisible(false);
            TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);

            MasterGridExControl.MainGrid.AddColumn("CodeMain", LabelConvert.GetLabelText("CodeMain"));
            MasterGridExControl.MainGrid.AddColumn("CodeName", LabelConvert.GetLabelText("CodeName"));
            MasterGridExControl.MainGrid.AddColumn("CodeNameENG", LabelConvert.GetLabelText("CodeNameENG"));
            MasterGridExControl.MainGrid.AddColumn("CodeNameCHN", LabelConvert.GetLabelText("CodeNameCHN"));
            MasterGridExControl.MainGrid.AddColumn("CodeVal", LabelConvert.GetLabelText("CodeVal"), false);
            MasterGridExControl.MainGrid.AddColumn("UseYN", LabelConvert.GetLabelText("UseFlag"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.AddColumn("CodeMain", LabelConvert.GetLabelText("CodeMain"));
            DetailGridExControl.MainGrid.AddColumn("CodeTop", LabelConvert.GetLabelText("CodeTop"));
            DetailGridExControl.MainGrid.AddColumn("CodeMid", LabelConvert.GetLabelText("CodeMid"),  false);
            DetailGridExControl.MainGrid.AddColumn("CodeBottom", LabelConvert.GetLabelText("CodeBottom"),  false);
            DetailGridExControl.MainGrid.AddColumn("CodeVal", LabelConvert.GetLabelText("CodeVal"), false);
            DetailGridExControl.MainGrid.AddColumn("CodeName", LabelConvert.GetLabelText("CodeName"));
            DetailGridExControl.MainGrid.AddColumn("CodeNameENG", LabelConvert.GetLabelText("CodeNameENG"));
            DetailGridExControl.MainGrid.AddColumn("CodeNameCHN", LabelConvert.GetLabelText("CodeNameCHN"));
            DetailGridExControl.MainGrid.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"));            
            DetailGridExControl.MainGrid.AddColumn("UseYN", LabelConvert.GetLabelText("UseFlag"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("SelfInspFlag"), false);
            DetailGridExControl.MainGrid.AddColumn("Temp1", LabelConvert.GetLabelText("FMEInspFlag"), false);
            DetailGridExControl.MainGrid.AddColumn("Temp2", LabelConvert.GetLabelText("ProcInspFlag"), false);

            SubDetailGridExControl.MainGrid.AddColumn("CodeMain", LabelConvert.GetLabelText("CodeMain"));
            SubDetailGridExControl.MainGrid.AddColumn("CodeTop", LabelConvert.GetLabelText("CodeTop"), false);
            SubDetailGridExControl.MainGrid.AddColumn("CodeMid", LabelConvert.GetLabelText("CodeMid"));
            SubDetailGridExControl.MainGrid.AddColumn("CodeBottom", LabelConvert.GetLabelText("CodeBottom"), false);
            SubDetailGridExControl.MainGrid.AddColumn("CodeVal", LabelConvert.GetLabelText("CodeVal"), false);
            SubDetailGridExControl.MainGrid.AddColumn("CodeName", LabelConvert.GetLabelText("CodeName"));
            SubDetailGridExControl.MainGrid.AddColumn("CodeNameENG", LabelConvert.GetLabelText("CodeNameENG"));
            SubDetailGridExControl.MainGrid.AddColumn("CodeNameCHN", LabelConvert.GetLabelText("CodeNameCHN"));
            SubDetailGridExControl.MainGrid.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"));           
            SubDetailGridExControl.MainGrid.AddColumn("UseYN", LabelConvert.GetLabelText("UseFlag"));
            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            TreeDetailGridExControl.MainGrid.AddColumn("CodeMain", LabelConvert.GetLabelText("CodeMain"));
            TreeDetailGridExControl.MainGrid.AddColumn("CodeTop", LabelConvert.GetLabelText("CodeTop"), false);
            TreeDetailGridExControl.MainGrid.AddColumn("CodeMid", LabelConvert.GetLabelText("CodeMid"), false);
            TreeDetailGridExControl.MainGrid.AddColumn("CodeBottom", LabelConvert.GetLabelText("CodeBottom"));
            TreeDetailGridExControl.MainGrid.AddColumn("CodeVal", LabelConvert.GetLabelText("CodeVal"), false);
            TreeDetailGridExControl.MainGrid.AddColumn("CodeName", LabelConvert.GetLabelText("CodeName"));
            TreeDetailGridExControl.MainGrid.AddColumn("CodeNameENG", LabelConvert.GetLabelText("CodeNameENG"));
            TreeDetailGridExControl.MainGrid.AddColumn("CodeNameCHN", LabelConvert.GetLabelText("CodeNameCHN"));
            TreeDetailGridExControl.MainGrid.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"));            
            TreeDetailGridExControl.MainGrid.AddColumn("UseYN", LabelConvert.GetLabelText("UseFlag"));
            TreeDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CodeMain", "CodeName", "CodeNameENG", "CodeNameCHN", "UseYN", "Memo");
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CodeTop", "CodeName", "CodeNameENG", "CodeNameCHN", "DisplayOrder", "UseYN", "Memo", "Temp", "Temp1", "Temp2");
            SubDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CodeMid", "CodeName", "CodeNameENG", "CodeNameCHN", "DisplayOrder", "UseYN", "Memo");
            TreeDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CodeBottom", "CodeName", "CodeNameENG", "CodeNameCHN", "DisplayOrder", "UseYN", "Memo");

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1000>(MasterGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1000>(DetailGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1000>(SubDetailGridExControl);
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1000>(TreeDetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemTextEdit("CodeMain", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYN", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);

            DetailGridExControl.MainGrid.SetRepositoryItemTextEdit("CodeTop", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYN", "N");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp1", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp2", "N");

            SubDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("CodeMid", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            SubDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYN", "N");
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);

            TreeDetailGridExControl.MainGrid.SetRepositoryItemTextEdit("CodeBottom", 4, DevExpress.Utils.DefaultBoolean.Default, DevExpress.XtraEditors.Mask.MaskType.None, null, true);
            TreeDetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYN", "N");
            TreeDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
            TreeDetailGridExControl.BestFitColumns();
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

            DetailGridExControl.MainGrid.Columns["Temp"].Visible = false;
            DetailGridExControl.MainGrid.Columns["Temp1"].Visible = false;
            DetailGridExControl.MainGrid.Columns["Temp2"].Visible = false;

            ModelService.ReLoad();

            string commcode = tx_CommCode.EditValue.GetNullToEmpty();
            string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(commcode) ? true : p.CodeName.Contains(commcode))
                                                                        && (p.CodeTop == "00") 
                                                                        && (p.CodeMid == "00") 
                                                                        && (p.CodeBottom == "00")
                                                                        && (radioValue == "A" ? true : p.UseYN == radioValue)
                                                                        )
                                                                        .OrderBy(p => p.CodeMain)
                                                                        .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            TreeDetailGridExControl.MainGrid.Clear();

            var masterObj = MasterGridBindingSource.Current as TN_STD1000;
            if (masterObj == null)
            {
                return;
            }

            if (masterObj.CodeMain == MasterCodeSTR.Process)
            {
                DetailGridExControl.MainGrid.Columns["Temp"].Visible = true;
                DetailGridExControl.MainGrid.Columns["Temp1"].Visible = true;
                DetailGridExControl.MainGrid.Columns["Temp2"].Visible = true;

                DetailGridExControl.MainGrid.Columns["Temp1"].VisibleIndex = DetailGridExControl.MainGrid.Columns["Temp"].VisibleIndex + 1;
                DetailGridExControl.MainGrid.Columns["Temp2"].VisibleIndex = DetailGridExControl.MainGrid.Columns["Temp1"].VisibleIndex + 1;
            }
            else
            {
                DetailGridExControl.MainGrid.Columns["Temp"].Visible = false;
                DetailGridExControl.MainGrid.Columns["Temp1"].Visible = false;
                DetailGridExControl.MainGrid.Columns["Temp2"].Visible = false;
            }

            string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            DetailGridBindingSource.DataSource = ModelService.GetList(p => (radioValue == "A" ? true : p.UseYN == radioValue)
                                                                        && (p.CodeTop != "00") 
                                                                        && (p.CodeMid == "00") 
                                                                        && (p.CodeBottom == "00") 
                                                                        && (p.CodeMain == masterObj.CodeMain)
                                                                        )
                                                                        .OrderBy(p => p.DisplayOrder)
                                                                        .ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.MainGrid.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();
        }

        protected override void DetailFocusedRowChanged()
        {
            SubDetailGridExControl.MainGrid.Clear();
            TreeDetailGridExControl.MainGrid.Clear();

            var detailObj = DetailGridBindingSource.Current as TN_STD1000;
            if (detailObj == null)
            {
                return;
            }

            string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            SubDetailGridBindingSource.DataSource = ModelService.GetList(p => (radioValue == "A" ? true : p.UseYN == radioValue)
                                                                            && (p.CodeBottom == "00")
                                                                            && (p.CodeTop == detailObj.CodeTop)
                                                                            && (p.CodeMid != "00")
                                                                            && (p.CodeMain == detailObj.CodeMain)
                                                                        )
                                                                        .OrderBy(p => p.DisplayOrder)
                                                                        .ToList();
            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.MainGrid.BestFitColumns();
            SubDetailGridRowLocator.SetCurrentRow();
        }

        private void SubMainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            TreeDetailGridExControl.MainGrid.Clear();
            var subObj = SubDetailGridBindingSource.Current as TN_STD1000;
            if (subObj == null)
            {
                return;
            }

            string radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            TreeDetailGridBindingSource.DataSource = ModelService.GetList(p => (radioValue == "A" ? true : p.UseYN == radioValue)
                                                                            && (p.CodeBottom != "00") 
                                                                            && (p.CodeTop == subObj.CodeTop) 
                                                                            && (p.CodeMid == subObj.CodeMid) 
                                                                            && (p.CodeMain == subObj.CodeMain)
                                                                         )
                                                                         .OrderBy(p => p.DisplayOrder)
                                                                         .ToList();
            TreeDetailGridExControl.DataSource = TreeDetailGridBindingSource;
            TreeDetailGridExControl.MainGrid.BestFitColumns();
            TreeDetailGridRowLocator.SetCurrentRow();
        }

        protected override void AddRowClicked()
        {
            TN_STD1000 obj = new TN_STD1000();
            obj.CodeTop = "00";
            obj.CodeMid = "00";
            obj.CodeBottom = "00";
            obj.UseYN = "Y";

            ModelService.Insert(obj);
            MasterGridBindingSource.Add(obj);
            MasterGridBindingSource.MoveLast();

            #region Grid Focus를 위한 코드
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, obj);
            #endregion
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1000;
            if (masterObj == null || masterObj.CodeMain.IsNullOrEmpty()) return;

            var detailList = DetailGridBindingSource.List as List<TN_STD1000>;

            TN_STD1000 obj = new TN_STD1000();
            obj.CodeMain = masterObj.CodeMain;
            obj.CodeTop =  null;
            obj.CodeMid = "00";
            obj.CodeBottom = "00";
            obj.UseYN = "Y";
            obj.DisplayOrder = detailList.Count + 1;

            ModelService.Insert(obj);
            DetailGridBindingSource.Add(obj);
            DetailGridBindingSource.MoveLast();

            #region Grid Focus를 위한 코드
            PopupDataParam.SetValue(PopupParameter.GridRowId_2, obj);
            #endregion
        }

        protected override void SubDetailAddRowClicked()
        {
            var detailObj = DetailGridBindingSource.Current as TN_STD1000;
            if (detailObj == null || detailObj.CodeTop.IsNullOrEmpty()) return;        

            var subList = SubDetailGridBindingSource.List as List<TN_STD1000>;

            TN_STD1000 obj = new TN_STD1000();
            obj.CodeMain = detailObj.CodeMain;
            obj.CodeTop = detailObj.CodeTop;
            obj.CodeMid = null;
            obj.CodeBottom = "00";
            obj.UseYN = "Y";
            obj.DisplayOrder = subList.Count + 1;

            ModelService.Insert(obj);
            SubDetailGridBindingSource.Add(obj);
            SubDetailGridBindingSource.MoveLast();

            #region Grid Focus를 위한 코드
            PopupDataParam.SetValue(PopupParameter.GridRowId_3, obj);
            #endregion
        }

        protected override void TreeDetailAddRowClicked()
        {
            var subObj = SubDetailGridBindingSource.Current as TN_STD1000;
            if (subObj == null || subObj.CodeMid.IsNullOrEmpty()) return;

            var treeList = TreeDetailGridBindingSource.List as List<TN_STD1000>;

            TN_STD1000 obj = new TN_STD1000();
            obj.CodeMain = subObj.CodeMain;
            obj.CodeTop = subObj.CodeTop;
            obj.CodeMid = subObj.CodeMid;
            obj.CodeBottom = null;
            obj.UseYN = "Y";
            obj.DisplayOrder = treeList.Count + 1;

            ModelService.Insert(obj);
            TreeDetailGridBindingSource.Add(obj);
            TreeDetailGridBindingSource.MoveLast();

            #region Grid Focus를 위한 코드
            PopupDataParam.SetValue(PopupParameter.GridRowId_4, obj);
            #endregion
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1000;
            if (masterObj == null) return;

            var detailList = DetailGridBindingSource.List as List<TN_STD1000>;
            if (detailList == null) return;

            if (detailList.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48)
                                        , LabelConvert.GetLabelText("CodeList")
                                        , LabelConvert.GetLabelText("TopCategory")
                                        , LabelConvert.GetLabelText("TopCategory")));
                return;
            }

            var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("Code"))
                                               , LabelConvert.GetLabelText("Warning")
                                               , MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                masterObj.UseYN = "N";
                MasterGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteDetailRow()
        {
            var detailObj = DetailGridBindingSource.Current as TN_STD1000;
            if (detailObj == null) return;

            var subList = SubDetailGridBindingSource.List as List<TN_STD1000>;
            if (subList == null) return;

            if (subList.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48)
                                        , LabelConvert.GetLabelText("TopCategory")
                                        , LabelConvert.GetLabelText("MiddleCategory")
                                        , LabelConvert.GetLabelText("MiddleCategory")));
                return;
            }


            var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("Code"))
                                                , LabelConvert.GetLabelText("Warning")
                                                , MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                detailObj.UseYN = "N";
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteSubDetailRow()
        {
            var subObj = SubDetailGridBindingSource.Current as TN_STD1000;
            if (subObj == null) return;

            var treeList = TreeDetailGridBindingSource.List as List<TN_STD1000>;
            if (treeList == null) return;

            if (treeList.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48)
                                        , LabelConvert.GetLabelText("MiddleCategory")
                                        , LabelConvert.GetLabelText("BottomCategory")
                                        , LabelConvert.GetLabelText("BottomCategory")));
                return;
            }


            var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("Code"))
                                                , LabelConvert.GetLabelText("Warning")
                                                , MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                subObj.UseYN = "N";
                DetailGridExControl.BestFitColumns();
            }
        }

        protected override void DeleteTreeDetailRow()
        {
            var treeObj = TreeDetailGridBindingSource.Current as TN_STD1000;
            if (treeObj == null) return;

            var result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("Code"))
                                                , LabelConvert.GetLabelText("Warning")
                                                , MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                treeObj.UseYN = "N";
                DetailGridExControl.BestFitColumns();
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

            TreeDetailGridExControl.MainGrid.PostEditor();
            TreeDetailGridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }

        private void Master_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (!e.Column.FieldName.Contains("CodeMain")) return;
            for (int i = 0; i < e.RowHandle; i++)
            {
                if (MasterGridExControl.MainGrid.MainView.GetRowCellValue(i, "CodeMain").ToString().ToUpper() == e.Value.ToString().ToUpper())
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), LabelConvert.GetLabelText("Code")));
                    return;
                }
            }
        }

        private void Detail_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Contains("CodeTop"))
            {
                for (int i = 0; i < e.RowHandle; i++)
                {
                    if (DetailGridExControl.MainGrid.MainView.GetRowCellValue(i, "CodeTop").ToString().ToUpper() == e.Value.ToString().ToUpper())
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), LabelConvert.GetLabelText("Code")));
                        return;
                    }
                }
                //if (!e.Column.FieldName.Contains("CodeName")) return;

                //값이 변경되었을 때
                //최소 최대값 있는지 판단해서 없으면 문자
                TN_STD1000 detail = DetailGridBindingSource.Current as TN_STD1000;
                if (detail == null) return;
                detail.CodeVal = detail.CodeTop;
            }
        }

        private void Sub_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Contains("CodeMid"))
            {
                for (int i = 0; i < e.RowHandle; i++)
                {
                    if (SubDetailGridExControl.MainGrid.MainView.GetRowCellValue(i, "CodeMid").ToString().ToUpper() == e.Value.ToString().ToUpper())
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), LabelConvert.GetLabelText("Code")));
                        return;
                    }
                }

                //if (!e.Column.FieldName.Contains("CodeName")) return;

                //값이 변경되었을 때
                //최소 최대값 있는지 판단해서 없으면 문자
                TN_STD1000 detail = SubDetailGridBindingSource.Current as TN_STD1000;
                if (detail == null) return;
                detail.CodeVal = detail.CodeTop + detail.CodeMid;
            }
        }

        private void Tree_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.Contains("CodeBottom"))
            {
                for (int i = 0; i < e.RowHandle; i++)
                {
                    if (TreeDetailGridExControl.MainGrid.MainView.GetRowCellValue(i, "CodeBottom").ToString().ToUpper() == e.Value.ToString().ToUpper())
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_41), LabelConvert.GetLabelText("Code")));
                        return;
                    }
                }

                //if (!e.Column.FieldName.Contains("CodeName")) return;

                //값이 변경되었을 때
                //최소 최대값 있는지 판단해서 없으면 문자
                TN_STD1000 detail = TreeDetailGridBindingSource.Current as TN_STD1000;
                if (detail == null) return;
                detail.CodeVal = detail.CodeTop + detail.CodeMid + detail.CodeBottom;
            }
        }
    }
}