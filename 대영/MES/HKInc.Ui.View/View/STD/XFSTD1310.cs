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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Utils.Class;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using DevExpress.Xpf;
using DevExpress.XtraEditors.Repository;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain.TEMP;
using System.Data.SqlClient;





namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 20211013 오세완 차장
    /// BOM 생성화면
    /// </summary>
    public partial class XFSTD1310 : HKInc.Service.Base.TreeListFormTemplate
    {
        #region 전역변수
        IService<TN_STD1300> ModelService = (IService<TN_STD1300>)ProductionFactory.GetDomainService("TN_STD1300");
        List<TEMP_STD1300> MainDateList = new List<TEMP_STD1300>();
        #endregion

        public XFSTD1310()
        {
            InitializeComponent();
            TreeListExControl = treeListEx1;
            TreeListExControl.TreeList.CellValueChanged += TreeList_CellValueChanged;
            TreeListExControl.TreeList.ShowingEditor += TreeList_ShowingEditor;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && 
                                                                                                                                        (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.TreeList.StateImageList = IconImageList;

            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("HighLevelAdd") + "[F3]"); // "최상위추가[F3]"
            IsGridButtonFileChooseEnabled = true;
            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("LowLevelAdd") + "[F10]");// "하위추가[F10]"
            TreeListExControl.SetToolbarShotKeyChange(GridToolbarButton.FileChoose, new BarShortcut(Keys.F10));

            TreeListExControl.AddColumn("BomCode", LabelConvert.GetLabelText("BomCode"), false);
            TreeListExControl.AddColumn("ParentBomCode", LabelConvert.GetLabelText("ParentBomCode"), false);
            TreeListExControl.AddColumn("Level", LabelConvert.GetLabelText("Level"), false);
            TreeListExControl.AddColumn("CustomItemCode", LabelConvert.GetLabelText("ItemCode"));
            TreeListExControl.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemName"));

            TreeListExControl.AddColumn("ItemName1", LabelConvert.GetLabelText("ItemName1"));
            TreeListExControl.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            TreeListExControl.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            TreeListExControl.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            TreeListExControl.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));

            TreeListExControl.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            TreeListExControl.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            TreeListExControl.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            TreeListExControl.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
            TreeListExControl.AddColumn("Weight", LabelConvert.GetLabelText("Weight"));

            TreeListExControl.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            TreeListExControl.AddColumn("UseQty", LabelConvert.GetLabelText("UseQty"));
            TreeListExControl.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            TreeListExControl.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"));
            TreeListExControl.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));

            TreeListExControl.AddColumn("MgFlag", LabelConvert.GetLabelText("MgFlag"));

            TreeListExControl.KeyFieldName = "BomCode";
            TreeListExControl.ParentFieldName = "ParentBomCode";
            TreeListExControl.SetTreeListEditable(UserRight.HasEdit, "ItemCode", "UseQty", "DisplayOrder", "Memo", "ProcessCode", "UseFlag", "MgFlag");

            TreeListExControl.TreeList.OptionsView.AutoWidth = false;
            TreeListExControl.TreeList.Columns[0].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[1].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[2].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[3].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[4].Fixed =  DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[5].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
        }

        protected override void InitRepository()
        {
            TreeListExControl.SetRepositoryItemSearchLookUpEdit_Bom("ItemCode", ModelService.GetChildList<VI_STD1100_BOMTREE>(p => (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName")); // 20210524 오세완 차장 bom구성시에는 반제품(외주)가 필요햐여 추가 처리
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            TreeListExControl.SetRepositoryItemSpinEdit("UseQty", true, DefaultBoolean.Default, "n3", true);
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemCheckEdit("UseFlag", "N");
            TreeListExControl.SetRepositoryItemCheckEdit("MgFlag", "N");
            TreeListExControl.SetRepositoryItemSpinEdit("DisplayOrder");

            TreeListExControl.TreeList.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(TreeListExControl, "Memo", UserRight.HasEdit);

            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
        }

        protected override void DataLoad()
        {
            TreeListExControl.Clear();
            ModelService.ReLoad();
            MainDateList.Clear();

            InitCombo(); 
            InitRepository();

            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                var ItemCode = new SqlParameter("@ITEM_CODE", lup_Item.EditValue.GetNullToEmpty());
                var result = context.Database.SqlQuery<TEMP_STD1300>("USP_GET_STD1310_LIST @ITEM_CODE", ItemCode).ToList(); 

                if (result == null)
                    return;

                MainDateList.AddRange(result);

                TreeListBindingSource.DataSource = result.Where(p => (radioValue == "A" ? true : p.UseFlag == radioValue)).ToList();
            }

            TreeListExControl.DataSource = TreeListBindingSource;
            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.OptionsView.AutoWidth = false;
            TreeListExControl.TreeList.BestFitColumns();
        }

        protected override void DataSave()
        {
            TreeListBindingSource.EndEdit();
            TreeListExControl.TreeList.PostEditor();

            var list = TreeListBindingSource.List as List<TEMP_STD1300>;
            if (list.Count > 0)
            {
                if (list.Where(w => w.Level == 0).GroupBy(p => p.ItemCode).Where(c => c.Count() > 1).Count() > 0)
                {
                    MessageBoxHandler.Show("최상위품목코드는 중복이 불가합니다. 확인바랍니다.");
                    return;
                }

                foreach (var v in list.Where(p => p.UpdFlag == "Y"))
                {
                    var chk = ModelService.GetList(p => p.BomCode == v.BomCode).ToList();
                    if (chk.Count > 0)
                    {
                        var newObj1 = ModelService.GetList(p => p.BomCode == v.BomCode).First();
                        if (newObj1 != null)
                        {
                            newObj1.ItemCode = v.ItemCode;
                            newObj1.ItemName = v.ItemName;
                            newObj1.ItemName1 = v.ItemName1;
                            newObj1.TopCategory = v.TopCategory;
                            newObj1.MiddleCategory = v.MiddleCategory;
                            newObj1.BottomCategory = v.BottomCategory;
                            newObj1.Spec1 = v.Spec1;
                            newObj1.Spec2 = v.Spec2;
                            newObj1.Spec3 = v.Spec3;
                            newObj1.Spec4 = v.Spec4;
                            newObj1.Unit = v.Unit;
                            newObj1.Weight = v.Weight.GetNullToZero();
                            newObj1.Level = v.Level;
                            newObj1.UseQty = v.UseQty;
                            newObj1.ProcessCode = v.ProcessCode;
                            newObj1.DisplayOrder = v.DisplayOrder;
                            newObj1.UseFlag = v.UseFlag;
                            newObj1.Memo = v.Memo;
                            if (v.MgFlag == "")
                            {
                                v.MgFlag = "N";
                            }
                            newObj1.MgFlag = v.MgFlag;
                            ModelService.Update(newObj1);
                        }
                    }
                    else
                    {
                        string sql = "exec SP_STD1300_INSERT  @BomCode='" + v.BomCode +
                                                           "',@ItemCode='" + v.ItemCode +
                                                           "',@ItemName='" + v.ItemName +
                                                           "',@ItemName1='" + v.ItemName1 + "'";

                        if (v.ParentBomCode == null)
                        {
                            sql = sql + ",@ParentBomCode =Null";
                        }
                        else
                        {
                            sql = sql + ",@ParentBomCode ='" + v.ParentBomCode + "'";
                        }
                        
                        sql = sql + ",@Level=" + v.Level + ",@TopCategory ='" + v.TopCategory +
                                                           "',@MiddleCategory  = '" + v.MiddleCategory +
                                                           "',@BottomCategory = '" + v.BottomCategory +
                                                           "',@Spec1 = '" + v.Spec1 +
                                                           "',@Spec2 = '" + v.Spec2 +
                                                           "',@Spec3 = '" + v.Spec3 +
                                                           "',@Spec4 = '" + v.Spec4 +
                                                           "',@Unit = '" + v.Unit +
                                                           "',@Weight = " + v.Weight.GetNullToZero() +
                                                           ",@UseQty = " + v.UseQty + "";

                        if (Convert.ToInt32(v.DisplayOrder).GetNullToZero() == 0)
                        {
                            sql = sql + ",@DisplayOrder = Null";
                        }
                        else
                        {
                            sql = sql + ",@DisplayOrder = " + v.DisplayOrder + "";
                        }


                        if (v.MgFlag == "")
                        {
                            v.MgFlag = "N";
                        }

                        sql = sql + ",@ProcessCode = '" + v.ProcessCode +
                                                           "',@UseFlag = '" + v.UseFlag +
                                                           "',@Memo = '" + v.Memo +
                                                           "',@MgFlag = '" + v.MgFlag + "'";

                        int K = DbRequestHandler.SetDataQury(sql);

                    }
                }
            }

            ModelService.Save();
            DataLoad();
        }

        /// <summary>
        /// 0레벨 등록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>F
        protected override void TreeListEx_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            if (!IsFirstLoaded)
                ActRefresh();

            var newObj = new TEMP_STD1300()
            {
                BomCode = DbRequestHandler.GetSeqStandard("BOM"),
                UseQty = 1,
                Level = 0,
                UseFlag = "Y",
                DisplayOrder = null,
                RowIndex = 0,
                UpdFlag = "Y",
                MgFlag = "N" // 20210606 오세완 차장 중요한 값이라서 추가 처리
            };

            TreeListBindingSource.Add(newObj);
            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
            TreeListExControl.TreeList.MoveLast();
        }

        /// <summary>
        /// 하위레벨 등록
        /// </summary>
        protected override void FileChooseClicked()
        {
            if (!IsFirstLoaded)
                ActRefresh();

            var obj = TreeListBindingSource.Current as TEMP_STD1300;
            if (obj == null)
                return;

            if (obj.Level >= 1 && obj.TopCategory == MasterCodeSTR.TopCategory_BAN)
            {
                //MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32));
                MessageBoxHandler.Show("반제품의 하위 제품은 최상위에서 구성해야 합니다.");
                return;
            }
            else if(obj.Level >= 1 && obj.TopCategory == MasterCodeSTR.TopCategory_MAT )
            {
                MessageBoxHandler.Show("원자재의 하위 제품은 구성할 수 없습니다.");
                return;
            }

            var newObj = new TEMP_STD1300()
            {
                BomCode = DbRequestHandler.GetSeqStandard("BOM"),
                ParentBomCode = obj.BomCode,
                UseQty = 0,
                Level = obj.Level + 1,
                UseFlag = "Y",
                DisplayOrder = null,
                UpdFlag = "Y",
                MgFlag = "N",
            };

            TreeListBindingSource.Add(newObj);
            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
        }

        /// <summary>
        /// 삭제
        /// </summary>
        protected override void DeleteRow()
        {
            if (!IsFirstLoaded)
                ActRefresh();

            var obj = TreeListBindingSource.Current as TEMP_STD1300;
            if (obj == null)
                return;

            var objList = TreeListBindingSource.List as List<TEMP_STD1300>;
            if (objList.Any(p => p.ParentBomCode == obj.BomCode))
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("ChildData")), LabelConvert.GetLabelText("Warning"));
                return;
            }

            var chk = ModelService.GetList(p => p.BomCode == obj.BomCode).ToList();
            if (chk.Count > 0)
            {
                var delobj = ModelService.GetList(p => p.BomCode == obj.BomCode).FirstOrDefault();

                ModelService.Delete(delobj);
            }

            TreeListBindingSource.RemoveCurrent();
            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
        }

        private void TreeList_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            var obj = TreeListBindingSource.Current as TEMP_STD1300;
            if (obj == null) return;

            if (e.Column.FieldName == "ItemCode" || e.Column.FieldName == "UseQty" || e.Column.FieldName == "ProcessCode" || e.Column.FieldName == "DisplayOrder"
                                                 || e.Column.FieldName == "UseQtyEx" || e.Column.FieldName == "WorkStandard" || e.Column.FieldName == "ItemCodeEx"
                                                 || e.Column.FieldName == "UseFlag" || e.Column.FieldName == "Memo" || e.Column.FieldName == "MgFlag")
            {
                var objList = TreeListBindingSource.List as List<TEMP_STD1300>;

                var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();

                var bomchk = objList.Where(p => p.BomCode == obj.ParentBomCode).ToList();

                if (bomchk.Count > 0)
                {
                    bool bReturn = false;
                    string sMessage = "";
                    var bomrealchk = objList.Where(p => p.BomCode == obj.ParentBomCode).FirstOrDefault();

                    if (bomrealchk.TopCategory == MasterCodeSTR.TopCategory_WAN && itemObj.TopCategory == MasterCodeSTR.TopCategory_WAN)
                    {
                        sMessage = "완제품 하위에 완제품구성은 불가합니다. 확인하여 주십시오.";
                        bReturn = true;
                    }

                    if (bomrealchk.TopCategory == MasterCodeSTR.TopCategory_BAN && itemObj.TopCategory == MasterCodeSTR.TopCategory_WAN)
                    {
                        sMessage = "반제품 하위에 완제품은 구성이 불가합니다. 확인하여 주십시오.";
                        bReturn = true;
                    }

                    if(bReturn)
                    {
                        MessageBoxHandler.Show(sMessage, LabelConvert.GetLabelText("Warning"));
                        TreeListBindingSource.RemoveCurrent();
                        TreeListExControl.TreeList.BestFitColumns();
                        return;
                    }

                    var samechk = objList.Where(p => (p.ParentBomCode == obj.ParentBomCode) && (p.ItemCode == obj.ItemCode)).ToList();

                    if (samechk.Count() > 1)
                    {
                        sMessage = "품번이 중복됩니다.";
                        bReturn = true;
                    }
                    
                    if (bReturn)
                    {
                        MessageBoxHandler.Show(sMessage, LabelConvert.GetLabelText("Warning"));
                        TreeListBindingSource.RemoveCurrent();
                        TreeListExControl.TreeList.BestFitColumns();
                        return;
                    }
                }

                if (e.Column.FieldName == "UseQty" && obj.UseQty.GetIntNullToZero() < 0)
                {
                    MessageBox.Show("소요량에 음수는 불가합니다.");
                    obj.UseQty = 0;
                }

                if (itemObj != null)
                {
                    obj.CustomItemCode = itemObj.ItemCode;
                    obj.ItemCode = itemObj.ItemCode;
                    obj.ItemName = itemObj.ItemName;
                    obj.ItemName1 = itemObj.ItemName1;
                    obj.TopCategory = itemObj.TopCategory;
                    obj.MiddleCategory = itemObj.MiddleCategory;
                    obj.BottomCategory = itemObj.BottomCategory;
                    obj.Spec1 = itemObj.Spec1;
                    obj.Spec2 = itemObj.Spec2;
                    obj.Spec3 = itemObj.Spec3;
                    obj.Spec4 = itemObj.Spec4;
                    obj.Unit = itemObj.Unit;
                    obj.Weight = itemObj.Weight.GetNullToZero();
                    obj.UpdFlag = "Y";
                }
                
                TreeListExControl.TreeList.BestFitColumns();
            }
        }

        private void TreeList_ShowingEditor(object sender, CancelEventArgs e)
        {
            TreeListExControl.TreeList.OptionsView.AutoWidth = false;
        }

        protected override void TreeList_MouseDoubleClick(object sender, MouseEventArgs e) { }
    }
}