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
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using DevExpress.XtraBars;
using HKInc.Service.Helper;
using HKInc.Service.Service;
using DevExpress.Utils;
using System.Data.SqlClient;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.STD
{
    /// <summary>
    /// 20211018 오세완 차장
    /// bom type
    /// </summary>
    public partial class XFSTD1320 : HKInc.Service.Base.ListMasterTreeFormTemplate
    {
        #region 전역변수
        IService<TN_STD1320> ModelService = (IService<TN_STD1320>)ProductionFactory.GetDomainService("TN_STD1320");

        /// <summary>
        /// 20211102 오세완 차장 
        /// 품목코드 변경시 품목구분 중 대분류 값 출력
        /// </summary>
        List<TN_STD1000> Top_Arr = new List<TN_STD1000>();

        /// <summary>
        /// 20211102 오세완 차장 
        /// 품목코드 변경시 품목구분 중 중분류 값 출력
        /// </summary>
        List<TN_STD1000> Mid_Arr = new List<TN_STD1000>();

        /// <summary>
        /// 20211102 오세완 차장 
        /// 품목코드 변경시 품목구분 중 소분류 값 출력
        /// </summary>
        List<TN_STD1000> Bot_Arr = new List<TN_STD1000>();

        /// <summary>
        /// 20211102 오세완 차장 
        /// 품목코드 변경시 단위값 출력
        /// </summary>
        List<TN_STD1000> Unit_Arr = new List<TN_STD1000>();

        /// <summary>
        /// 20211102 오세완 차장 
        /// 프로시저로 제어해서 삭제할 객체를 따로 저장
        /// </summary>
        List<TEMP_XFSTD1320_DETAIL> delArr = new List<TEMP_XFSTD1320_DETAIL>();
        #endregion

        public XFSTD1320()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            TreeListExControl = treeListEx1;

            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            TreeListExControl.TreeList.CellValueChanged += TreeList_CellValueChanged;
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_STD1320 masterObj = MasterGridBindingSource.Current as TN_STD1320;
            if (masterObj == null)
                return;

            if(e.Column.FieldName == "TypeName")
            {
                if (masterObj.NewRowFlag.GetNullToEmpty() != "Y")
                    masterObj.EditRowFlag = "Y";
            }
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1320;
            if (masterObj == null)
            {
                TreeListExControl.Clear();
                return;
            }

            // 20211101 오세완 차장 foregin key로 등록한 item_code를 선택해야 해서 insert자체가 힘들기 때문에 procedure로 변경
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                SqlParameter sp_Typecode = new SqlParameter("@TYPE_CODE", masterObj.TypeCode);
                
                var vResult = context.Database.SqlQuery<TEMP_XFSTD1320_DETAIL>("USP_GET_XFSTD1320_DETAIL @TYPE_CODE", sp_Typecode).ToList();
                if (vResult != null)
                {
                    //TreeListBindingSource.DataSource = vResult.OrderBy(p => p.SEQ).ToList();
                    TreeListBindingSource.DataSource = vResult; // 20211103 오세완 차장 조합에 따라서 순번이 트리구조와 안맞게 된다. 
                    delArr.Clear();
                }
            }

            //TreeListBindingSource.DataSource = masterObj.TN_STD1321List.OrderBy(o => o.Seq).ToList();
            TreeListExControl.DataSource = TreeListBindingSource;
            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.OptionsView.AutoWidth = false;
            TreeListExControl.BestFitColumns();
        }

        private void TreeList_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            #region entity version
            /* TN_STD1321 treeObj = TreeListBindingSource.Current as TN_STD1321;
            if (treeObj == null)
                return;

            if (e.Column.FieldName == "ItemCode" || e.Column.FieldName == "UseQty" || e.Column.FieldName == "ProcessCode" || e.Column.FieldName == "DisplayOrder" || 
                e.Column.FieldName == "UseQtyEx" || e.Column.FieldName == "ItemCodeEx" || e.Column.FieldName == "UseFlag" || e.Column.FieldName == "Memo" || 
                e.Column.FieldName == "MgFlag")
            {
                List<TN_STD1321> treeList = TreeListBindingSource.List as List<TN_STD1321>;

                TN_STD1100 std1000_Obj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == treeObj.ItemCode).FirstOrDefault();

                // 타입구성 내 품번 중복
                List<TN_STD1321> chk_Dup_List = treeList.Where(p => p.ItemCode == treeObj.ItemCode).ToList();
                if (chk_Dup_List.Count > 0)
                {
                    MessageBoxHandler.Show("품번이 중복됩니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                    TreeListBindingSource.RemoveCurrent();
                    TreeListExControl.TreeList.BestFitColumns();
                    return;
                }

                
                if(treeObj.Level == 0)
                {
                    chk_Dup_List = treeList.Where(p => p.Level == treeObj.Level).ToList();

                    // 최상위 레벨 중복 확인
                    if (chk_Dup_List.Count > 1)
                    {
                        MessageBoxHandler.Show("최상위 구성은 단일로 존재해야 합니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                        TreeListBindingSource.RemoveCurrent();
                        TreeListExControl.TreeList.BestFitColumns();
                        return;
                    }
                    else
                    {
                        // 최상위 레벨 원자재여부 확인
                        bool bFindMat = false;
                        if(treeObj.TN_STD1100 == null)
                        {
                            if (std1000_Obj.TopCategory == MasterCodeSTR.TopCategory_MAT)
                                bFindMat = true;
                        }
                        else
                        {
                            if (treeObj.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_MAT)
                                bFindMat = true;
                        }

                        if(bFindMat)
                        {
                            MessageBoxHandler.Show("원자재는 최상위 구성이 안됩니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                            TreeListBindingSource.RemoveCurrent();
                            TreeListExControl.TreeList.BestFitColumns();
                            return;
                        }
                    }
                }

                // 상위 제품과 하위제품군 확인
                if(treeObj.Level == 1)
                {
                    chk_Dup_List = treeList.Where(p => p.Level == 0).ToList();
                    if(chk_Dup_List.Count > 0)
                    {
                        TN_STD1321 topObj = chk_Dup_List.FirstOrDefault();
                        if(topObj.TN_STD1100 != null)
                        {
                            if(topObj.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_BAN || topObj.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)
                            {
                                bool bFindCondition_Ban_Wan = false;
                                if (treeObj.TN_STD1100 == null)
                                {
                                    if (std1000_Obj.TopCategory == MasterCodeSTR.TopCategory_WAN)
                                        bFindCondition_Ban_Wan = true;
                                }
                                else
                                {
                                    if (treeObj.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_WAN)
                                        bFindCondition_Ban_Wan = true;
                                }

                                if(bFindCondition_Ban_Wan)
                                {
                                    MessageBoxHandler.Show("반제품 하위에 완제품 구성이 안됩니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                                    TreeListBindingSource.RemoveCurrent();
                                    TreeListExControl.TreeList.BestFitColumns();
                                    return;
                                }
                            }
                            else if(topObj.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_WAN)
                            {
                                bool bFindCondition_Wan_Wan = false;
                                if (treeObj.TN_STD1100 == null)
                                {
                                    if (std1000_Obj.TopCategory == MasterCodeSTR.TopCategory_WAN)
                                        bFindCondition_Wan_Wan = true;
                                }
                                else
                                {
                                    if (treeObj.TN_STD1100.TopCategory == MasterCodeSTR.TopCategory_WAN)
                                        bFindCondition_Wan_Wan = true;
                                }

                                if (bFindCondition_Wan_Wan)
                                {
                                    MessageBoxHandler.Show("완제품 하위에 완제품 구성이 안됩니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                                    TreeListBindingSource.RemoveCurrent();
                                    TreeListExControl.TreeList.BestFitColumns();
                                    return;
                                }
                            }
                        }
                    }
                }
                

                if (e.Column.FieldName == "UseQty" && treeObj.UseQty.GetIntNullToZero() < 0)
                {
                    MessageBox.Show("소요량에 음수는 불가합니다.");
                    treeObj.UseQty = 0;
                }

                if (std1000_Obj != null)
                {
                    treeObj.TN_STD1100 = std1000_Obj;
                }

                TreeListExControl.TreeList.BestFitColumns();
            } */
            #endregion

            TEMP_XFSTD1320_DETAIL treeObj = TreeListBindingSource.Current as TEMP_XFSTD1320_DETAIL;
            if (treeObj == null)
                return;

            if(e.Column.FieldName == "ItemCode")
            {
                List<TEMP_XFSTD1320_DETAIL> treeList = TreeListBindingSource.List as List<TEMP_XFSTD1320_DETAIL>;
                TN_STD1100 std1000_Obj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == treeObj.ItemCode).FirstOrDefault();

                // 타입구성 내 품번 중복
                List<TEMP_XFSTD1320_DETAIL> chk_Dup_List = treeList.Where(p => p.ItemCode == treeObj.ItemCode).ToList();
                if (chk_Dup_List.Count > 1) // 20211101 오세완 차장 이미 입력한것 까지 1개로 쳐서 중복이 되면 2개 이상이 된다. 
                {
                    MessageBoxHandler.Show("품번이 중복됩니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                    TreeListBindingSource.RemoveCurrent();
                    TreeListExControl.TreeList.BestFitColumns();
                    return;
                }

                if (treeObj.LEVEL == 0)
                {
                    chk_Dup_List = treeList.Where(p => p.LEVEL == treeObj.LEVEL).ToList();

                    // 최상위 레벨 중복 확인
                    if (chk_Dup_List.Count > 1)
                    {
                        MessageBoxHandler.Show("최상위 구성은 단일로 존재해야 합니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                        TreeListBindingSource.RemoveCurrent();
                        TreeListExControl.TreeList.BestFitColumns();
                        return;
                    }
                    else
                    {
                        // 최상위 레벨 원자재여부 확인
                        bool bFindMat = false;
                        if (std1000_Obj != null)
                        {
                            if (std1000_Obj.TopCategory == MasterCodeSTR.TopCategory_MAT)
                                bFindMat = true;
                        }

                        if (bFindMat)
                        {
                            MessageBoxHandler.Show("원자재는 최상위 구성이 안됩니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                            TreeListBindingSource.RemoveCurrent();
                            TreeListExControl.TreeList.BestFitColumns();
                            return;
                        }
                    }
                }
                else if(treeObj.LEVEL >= 1)
                {
                    chk_Dup_List = treeList.Where(p => p.LEVEL == 0).ToList();
                    if (chk_Dup_List.Count > 0)
                    {
                        TEMP_XFSTD1320_DETAIL topObj = chk_Dup_List.FirstOrDefault();
                        if (topObj.TOP_CATEGORY == MasterCodeSTR.TopCategory_BAN || topObj.TOP_CATEGORY == MasterCodeSTR.TopCategory_BAN_Outsourcing)
                        {
                            bool bFindCondition_Ban_Wan = false;
                            
                            if (std1000_Obj.TopCategory == MasterCodeSTR.TopCategory_WAN)
                                bFindCondition_Ban_Wan = true;

                            if (bFindCondition_Ban_Wan)
                            {
                                MessageBoxHandler.Show("반제품 하위에 완제품 구성이 안됩니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                                TreeListBindingSource.RemoveCurrent();
                                TreeListExControl.TreeList.BestFitColumns();
                                return;
                            }
                        }
                        else if (topObj.TOP_CATEGORY == MasterCodeSTR.TopCategory_WAN)
                        {
                            bool bFindCondition_Wan_Wan = false;
                            
                            if (std1000_Obj.TopCategory == MasterCodeSTR.TopCategory_WAN)
                                bFindCondition_Wan_Wan = true;

                            if (bFindCondition_Wan_Wan)
                            {
                                MessageBoxHandler.Show("완제품 하위에 완제품 구성이 안됩니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                                TreeListBindingSource.RemoveCurrent();
                                TreeListExControl.TreeList.BestFitColumns();
                                return;
                            }
                        }
                    }
                }

                treeObj.TOP_CATEGORY = std1000_Obj.TopCategory;
                string sValue = "";
                sValue = Top_Arr.Where(p => p.CodeVal == treeObj.TOP_CATEGORY).Select(s => s.CodeName).FirstOrDefault();
                treeObj.TOP_CATEGORY_NAME = sValue.GetNullToEmpty();

                treeObj.MIDDLE_CATEGORY = std1000_Obj.MiddleCategory;
                sValue = Mid_Arr.Where(p => p.CodeVal == treeObj.MIDDLE_CATEGORY).Select(s => s.CodeName).FirstOrDefault();
                treeObj.MIDDLE_CATEGORY_NAME = sValue.GetNullToEmpty();

                treeObj.BOTTOM_CATEGORY = std1000_Obj.BottomCategory;
                sValue = Bot_Arr.Where(p => p.CodeVal == treeObj.BOTTOM_CATEGORY).Select(s => s.CodeName).FirstOrDefault();
                treeObj.BOTTOM_CATEGORY_NAME = sValue.GetNullToEmpty();

                treeObj.UNIT = std1000_Obj.Unit;
                sValue = Unit_Arr.Where(p => p.CodeVal == treeObj.UNIT).Select(s => s.CodeName).FirstOrDefault();
                treeObj.UNIT_NAME = sValue.GetNullToEmpty();

                treeObj.ITEM_NAME = std1000_Obj.ItemName;
                treeObj.ITEM_NAME1 = std1000_Obj.ItemName1;
                treeObj.SPEC_1 = std1000_Obj.Spec1;
                treeObj.SPEC_2 = std1000_Obj.Spec2;
                treeObj.SPEC_3 = std1000_Obj.Spec3;
                treeObj.SPEC_4 = std1000_Obj.Spec4;
                treeObj.WEIGHT = std1000_Obj.Weight.GetDecimalNullToZero();

                
            }
            else if(e.Column.Name == "USE_QTY")
            {
                if(treeObj.USE_QTY.GetIntNullToZero() < 0)
                {
                    MessageBoxHandler.Show("소요량에 음수는 불가합니다.");
                    treeObj.USE_QTY = 0;
                }
            }
            else if(e.Column.Name == "DISPLAY_ORDER")
            {
                if(treeObj.DISPLAY_ORDER.GetIntNullToZero() < 0)
                {
                    MessageBoxHandler.Show("출력 순서에 음수는 불가합니다.");
                    treeObj.DISPLAY_ORDER = 0;
                }
            }

            if (treeObj.Type.GetNullToEmpty() != "Insert")
                treeObj.Type = "Update";

            TreeListExControl.BestFitColumns();
        }

        protected override void InitGrid()
        {
            #region master
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            MasterGridExControl.MainGrid.AddColumn("TypeCode");
            MasterGridExControl.MainGrid.AddColumn("TypeName");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "TypeName");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1320>(MasterGridExControl);

            // 20211102 기존의 ListMasterTreeFormTemplate가 std1400에 특화되어 있어서 추가된 로직
            IsMasterGridDoubleClick = false;
            IsMasterGridAddPopup = false;
            #endregion

            #region detail
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.TreeList.StateImageList = IconImageList;

            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("HighLevelAdd") + "[F3]"); // "최상위추가[F3]"
            IsGridButtonFileChooseEnabled = true;
            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("LowLevelAdd") + "[F10]");// "하위추가[F10]"
            TreeListExControl.SetToolbarShotKeyChange(GridToolbarButton.FileChoose, new BarShortcut(Keys.F10));

            #region entity version
            /* TreeListExControl.AddColumn("TypeName", LabelConvert.GetLabelText("TypeName"), false);
            TreeListExControl.AddColumn("Seq", LabelConvert.GetLabelText("Seq"));
            TreeListExControl.AddColumn("Level", LabelConvert.GetLabelText("Level"), false);
            TreeListExControl.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            TreeListExControl.AddColumn("TN_STD1100.ItemName1", LabelConvert.GetLabelText("ItemName1"));

            TreeListExControl.AddColumn("TN_STD1100.ItemName", LabelConvert.GetLabelText("ItemName"));
            TreeListExControl.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            TreeListExControl.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            TreeListExControl.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            TreeListExControl.AddColumn("TN_STD1100.Spec1", LabelConvert.GetLabelText("Spec1"));

            TreeListExControl.AddColumn("TN_STD1100.Spec2", LabelConvert.GetLabelText("Spec2"));
            TreeListExControl.AddColumn("TN_STD1100.Spec3", LabelConvert.GetLabelText("Spec3"));
            TreeListExControl.AddColumn("TN_STD1100.Spec4", LabelConvert.GetLabelText("Spec4"));
            TreeListExControl.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            TreeListExControl.AddColumn("TN_STD1100.Weight", LabelConvert.GetLabelText("Weight"));

            TreeListExControl.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            TreeListExControl.AddColumn("UseQty", LabelConvert.GetLabelText("UseQty"));
            TreeListExControl.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            TreeListExControl.AddColumn("DisplayOrder", LabelConvert.GetLabelText("DisplayOrder"));
            TreeListExControl.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));

            TreeListExControl.AddColumn("MgFlag", LabelConvert.GetLabelText("MgFlag")); 

            TreeListExControl.KeyFieldName = "TypeName";
            TreeListExControl.ParentFieldName = "Seq";
            TreeListExControl.SetTreeListEditable(UserRight.HasEdit, "ItemCode", "UseQty", "DisplayOrder", "Memo", "ProcessCode", "UseFlag", "MgFlag"); */
            #endregion

            TreeListExControl.AddColumn("TYPE_CODE", false);
            TreeListExControl.AddColumn("SEQ", LabelConvert.GetLabelText("Seq"));
            TreeListExControl.AddColumn("LEVEL", false);
            TreeListExControl.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            TreeListExControl.AddColumn("PARENT_ITEM_CODE", false);

            TreeListExControl.AddColumn("ITEM_NAME1", LabelConvert.GetLabelText("ItemName1"));
            TreeListExControl.AddColumn("ITEM_NAME", LabelConvert.GetLabelText("ItemName"));
            TreeListExControl.AddColumn("TOP_CATEGORY_NAME", LabelConvert.GetLabelText("TopCategory"));
            TreeListExControl.AddColumn("MIDDLE_CATEGORY_NAME", LabelConvert.GetLabelText("MiddleCategory"));
            TreeListExControl.AddColumn("BOTTOM_CATEGORY_NAME", LabelConvert.GetLabelText("BottomCategory"));

            TreeListExControl.AddColumn("SPEC_1", LabelConvert.GetLabelText("Spec1"));
            TreeListExControl.AddColumn("SPEC_2", LabelConvert.GetLabelText("Spec2"));
            TreeListExControl.AddColumn("SPEC_3", LabelConvert.GetLabelText("Spec3"));
            TreeListExControl.AddColumn("SPEC_4", LabelConvert.GetLabelText("Spec4"));
            TreeListExControl.AddColumn("UNIT_NAME", LabelConvert.GetLabelText("Unit"));

            TreeListExControl.AddColumn("WEIGHT", LabelConvert.GetLabelText("Weight"));
            TreeListExControl.AddColumn("PROCESS_CODE", LabelConvert.GetLabelText("ProcessName"));
            TreeListExControl.AddColumn("USE_QTY", LabelConvert.GetLabelText("UseQty"));
            TreeListExControl.AddColumn("MEMO", LabelConvert.GetLabelText("Memo"));
            TreeListExControl.AddColumn("DISPLAY_ORDER", LabelConvert.GetLabelText("DisplayOrder"));

            TreeListExControl.AddColumn("USE_FLAG", LabelConvert.GetLabelText("UseFlag"));
            TreeListExControl.AddColumn("MG_FLAG", LabelConvert.GetLabelText("MgFlag"));

            TreeListExControl.TreeList.OptionsView.AutoWidth = false;

            TreeListExControl.KeyFieldName = "ItemCode";
            TreeListExControl.ParentFieldName = "PARENT_ITEM_CODE";
            TreeListExControl.SetTreeListEditable(UserRight.HasEdit, "ItemCode", "USE_QTY", "DISPLAY_ORDER", "MEMO", "PROCESS_CODE", "USE_FLAG", "MG_FLAG"); 

            TreeListExControl.TreeList.Columns[0].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[1].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[2].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[3].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[4].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[5].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[6].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            #endregion

            InitLabelConvert();
        }

        private void InitLabelConvert()
        {
            gcTypeList.Text = LabelConvert.GetLabelText("TypeList");
            gcBomList.Text = LabelConvert.GetLabelText("BomList");
        }

        protected override void InitRepository()
        {
            #region entity version
            /* TreeListExControl.SetRepositoryItemSearchLookUpEdit_Bom("ItemCode", ModelService.GetChildList<VI_STD1100_BOMTREE>(p => (p.TopCategory == MasterCodeSTR.TopCategory_WAN || 
                                                                                                                                    p.TopCategory == MasterCodeSTR.TopCategory_BAN || 
                                                                                                                                    p.TopCategory == MasterCodeSTR.TopCategory_MAT || 
                                                                                                                                    p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList(), 
                                                                    "ItemCode", DataConvert.GetCultureDataFieldName("ItemName")); 
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            TreeListExControl.SetRepositoryItemSpinEdit("UseQty", true, DefaultBoolean.Default, "n3", true);
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemCheckEdit("UseFlag", "N");
            TreeListExControl.SetRepositoryItemCheckEdit("MgFlag", "N");
            TreeListExControl.SetRepositoryItemSpinEdit("DisplayOrder");

            TreeListExControl.TreeList.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(TreeListExControl, "Memo", UserRight.HasEdit); */
            #endregion

            TreeListExControl.SetRepositoryItemSearchLookUpEdit_Bom("ItemCode", ModelService.GetChildList<VI_STD1100_BOMTREE>(p => (p.TopCategory == MasterCodeSTR.TopCategory_WAN ||
                                                                                                                                    p.TopCategory == MasterCodeSTR.TopCategory_BAN ||
                                                                                                                                    p.TopCategory == MasterCodeSTR.TopCategory_MAT ||
                                                                                                                                    p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList(),
                                                                    "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            TreeListExControl.SetRepositoryItemSpinEdit("USE_QTY", true, DefaultBoolean.Default, "n3", true);
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("PROCESS_CODE", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemCheckEdit("USE_FLAG", "N");
            TreeListExControl.SetRepositoryItemCheckEdit("MG_FLAG", "N");

            TreeListExControl.SetRepositoryItemSpinEdit("DISPLAY_ORDER");
            TreeListExControl.TreeList.Columns["MEMO"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(TreeListExControl, "MEMO", UserRight.HasEdit);

            // 20211101 오세완 차장 grid입력시 사용처리
            Top_Arr = DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1);
            Mid_Arr = DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2);
            Bot_Arr = DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3);
            Unit_Arr = DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1);

            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("TypeCode");

            MasterGridExControl.MainGrid.Clear();
            TreeListExControl.Clear();

            ModelService.ReLoad();

            InitRepository();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => true).OrderBy(p => p.TypeCode).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void DataSave()
        {
            MasterGridBindingSource.EndEdit();
            MasterGridExControl.MainGrid.PostEditor();

            TreeListBindingSource.EndEdit();
            TreeListExControl.TreeList.PostEditor();

            //bool bResult = CheckBom();
            //if(bResult)
            //{
            //    ModelService.Save();
            //    DataLoad();
            //}

            SaveProcess();
            ModelService.Save();
            DataLoad();
        }

        private void SaveProcess()
        {
            var masterObj = MasterGridBindingSource.Current as TN_STD1320;
            if (masterObj == null)
                return;

            List<TEMP_XFSTD1320_DETAIL> tempList = TreeListBindingSource.List as List<TEMP_XFSTD1320_DETAIL>;
            if(tempList != null)
                if(tempList.Count > 0)
                {
                    foreach(TEMP_XFSTD1320_DETAIL each in tempList)
                    {
                        if(each.Type.GetNullToEmpty() == "Insert")
                        {
                            TN_STD1321 insertlObj = new TN_STD1321()
                            {
                                TypeCode = each.TYPE_CODE,
                                UseQty = each.USE_QTY.GetDecimalNullToZero(),
                                Level = each.LEVEL,
                                UseFlag = each.USE_FLAG,
                                DisplayOrder = each.DISPLAY_ORDER,
                                MgFlag = each.MG_FLAG,
                                Seq = each.SEQ,
                                ItemCode = each.ItemCode,
                                Memo = each.MEMO,
                                ProcessCode = each.PROCESS_CODE,
                                ParentItemCode = each.PARENT_ITEM_CODE.GetNullToEmpty()
                            };

                            masterObj.TN_STD1321List.Add(insertlObj);

                            if(masterObj.NewRowFlag.GetNullToEmpty() == "Y")
                                ModelService.Insert(masterObj);
                            else
                                ModelService.Update(masterObj);
                        }
                        else if(each.Type.GetNullToEmpty() == "Update")
                        {
                            TN_STD1321 updateObj = ModelService.GetChildList<TN_STD1321>(p => p.TypeCode == each.TYPE_CODE &&
                                                                                              p.Seq == each.SEQ).FirstOrDefault();
                            if(updateObj != null)
                            {
                                updateObj.ItemCode = each.ItemCode;
                                updateObj.UseQty = each.USE_QTY.GetDecimalNullToZero();
                                updateObj.Level = each.LEVEL;
                                updateObj.UseFlag = each.USE_FLAG;
                                updateObj.DisplayOrder = each.DISPLAY_ORDER;
                                updateObj.MgFlag = each.MG_FLAG;
                                updateObj.ProcessCode = each.PROCESS_CODE;
                                updateObj.Memo = each.MEMO;
                                updateObj.ParentItemCode = each.PARENT_ITEM_CODE;
                            }

                            ModelService.UpdateChild<TN_STD1321>(updateObj);
                        }

                        each.Type = "";
                    }
                }

            if (delArr.Count > 0)
            {
                foreach (TEMP_XFSTD1320_DETAIL del_each in delArr)
                {
                    TN_STD1321 delObj = ModelService.GetChildList<TN_STD1321>(p => p.TypeCode == del_each.TYPE_CODE &&
                                                                                   p.Seq == del_each.SEQ).FirstOrDefault();
                    if (delObj != null)
                        ModelService.RemoveChild<TN_STD1321>(delObj);
                }

                delArr.Clear();
            }
        }

        protected override void MasterGridAddRow()
        {
            TN_STD1320 newobj = new TN_STD1320()
            {
                TypeCode = DbRequestHandler.GetSeqStandard("BTYPE"),
                NewRowFlag = "Y",
            };

            MasterGridBindingSource.Add(newobj);
            ModelService.Insert(newobj);
            MasterGridExControl.BestFitColumns();
        }

        protected override void MasterGridDeleteRow()
        {
            TN_STD1320 masterObj = MasterGridBindingSource.Current as TN_STD1320;
            if (masterObj == null)
                return;

            if (masterObj.TN_STD1321List.Count > 0)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_10));
                return;
            }

            MasterGridBindingSource.RemoveCurrent();
            ModelService.Delete(masterObj);
            MasterGridExControl.BestFitColumns();
        }

        /// <summary>
        /// 20211029 오세완 차장 최상위 추가
        /// </summary>
        protected override void TreeAddRow()
        {
            if (!IsFirstLoaded)
                ActRefresh();

            TN_STD1320 masterObj = MasterGridBindingSource.Current as TN_STD1320;
            if (masterObj == null)
                return;

            #region entity version
            /* TN_STD1321 detailObj = new TN_STD1321()
            {
                TypeCode = masterObj.TypeCode,
                UseQty = 1,
                Level = 0,
                UseFlag = "Y",
                DisplayOrder = 0,
                MgFlag = "N" 
            };

            if (masterObj.TN_STD1321List == null)
                detailObj.Seq = 1;
            else if(masterObj.TN_STD1321List.Count == 0)
                detailObj.Seq = 1;
            else
                detailObj.Seq = masterObj.TN_STD1321List.Count + 1;

            TreeListBindingSource.Add(detailObj);
            masterObj.TN_STD1321List.Add(detailObj); */
            #endregion

            TEMP_XFSTD1320_DETAIL detailObj = new TEMP_XFSTD1320_DETAIL()
            {
                TYPE_CODE = masterObj.TypeCode,
                USE_QTY = 0,
                LEVEL = 0,
                USE_FLAG = "Y",
                DISPLAY_ORDER = 0,
                MG_FLAG = "N",
                Type = "Insert"
            };

            int iCount = 0;
            if (TreeListBindingSource.List == null)
                iCount = 1;
            else
                iCount = TreeListBindingSource.List.Count + 1;

            detailObj.SEQ = iCount;
            TreeListBindingSource.Add(detailObj);

            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
            TreeListExControl.TreeList.MoveLast();
        }

        protected override void FileChooseClicked()
        {
            if (!IsFirstLoaded)
                ActRefresh();

            TN_STD1320 masterObj = MasterGridBindingSource.Current as TN_STD1320;
            if (masterObj == null)
                return;

            #region entity version
            /* TN_STD1321 detailObj = TreeListBindingSource.Current as TN_STD1321;
            if (detailObj == null)
                return;

            TN_STD1321 new_detailObj = new TN_STD1321()
            {
                TypeCode = detailObj.TypeCode,
                UseQty = 1,
                Level = detailObj.Level + 1,
                UseFlag = "Y",
                DisplayOrder = 0,
                MgFlag = "N",
                TN_STD1100 = new TN_STD1100()
            };

            //TreeListBindingSource.Add(new_detailObj);
            masterObj.TN_STD1321List.Add(new_detailObj);
            TreeListBindingSource.Add(new_detailObj); */
            #endregion

            TEMP_XFSTD1320_DETAIL detailObj = TreeListBindingSource.Current as TEMP_XFSTD1320_DETAIL;
            if (detailObj == null)
                return;

            TEMP_XFSTD1320_DETAIL new_detailObj = new TEMP_XFSTD1320_DETAIL()
            {
                TYPE_CODE = detailObj.TYPE_CODE,
                USE_QTY = 0,
                LEVEL = detailObj.LEVEL.GetIntNullToZero() + 1,
                USE_FLAG = "Y",
                DISPLAY_ORDER = 0,
                MG_FLAG = "N",
                Type = "Insert"
            };

            int iCount = 0;
            if (TreeListBindingSource.List == null)
                iCount = 1;
            else
                iCount = TreeListBindingSource.List.Count + 1;

            new_detailObj.SEQ = iCount;

            // 부모코드 할당
            List<TEMP_XFSTD1320_DETAIL> current_Arr = TreeListBindingSource.List as List<TEMP_XFSTD1320_DETAIL>;
            List<TEMP_XFSTD1320_DETAIL> level_0_Arr = current_Arr.Where(p => p.LEVEL < new_detailObj.LEVEL &&
                                                                             p.SEQ < new_detailObj.SEQ && 
                                                                             p.TOP_CATEGORY != MasterCodeSTR.TopCategory_MAT).ToList();
            if(level_0_Arr != null)
                if(level_0_Arr.Count > 0)
                {
                    if(new_detailObj.LEVEL == 1)
                    {
                        TEMP_XFSTD1320_DETAIL topObj = level_0_Arr.OrderBy(o => o.SEQ).FirstOrDefault();
                        if (topObj != null)
                            new_detailObj.PARENT_ITEM_CODE = topObj.ItemCode;
                    }
                    else if(new_detailObj.LEVEL > 1)
                    {
                        List<TEMP_XFSTD1320_DETAIL> level_1_Arr = level_0_Arr.Where(p => p.LEVEL > 0).ToList();

                        bool bNeedToBan = false;
                        if (level_1_Arr != null)
                            if (level_1_Arr.Count > 0)
                                bNeedToBan = true;

                        if(bNeedToBan)
                        {
                            TEMP_XFSTD1320_DETAIL banObj = level_1_Arr.Where(p => p.TOP_CATEGORY == MasterCodeSTR.TopCategory_BAN ||
                                                                                  p.TOP_CATEGORY == MasterCodeSTR.TopCategory_BAN_Outsourcing).FirstOrDefault();
                            if (banObj != null)
                                new_detailObj.PARENT_ITEM_CODE = banObj.ItemCode;
                        }
                        else
                        {
                            TEMP_XFSTD1320_DETAIL topObj1 = level_0_Arr.OrderBy(o => o.SEQ).FirstOrDefault();
                            if (topObj1 != null)
                                new_detailObj.PARENT_ITEM_CODE = topObj1.ItemCode;
                        }
                    }
                }

            TreeListBindingSource.Add(new_detailObj);

            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
            TreeListExControl.TreeList.MoveLast();
        }

        protected override void TreeDeleteRow()
        {
            if (!IsFirstLoaded)
                ActRefresh();

            TEMP_XFSTD1320_DETAIL detailObj = TreeListBindingSource.Current as TEMP_XFSTD1320_DETAIL;
            if (detailObj == null)
                return;

            List<TEMP_XFSTD1320_DETAIL> treeList = TreeListBindingSource.List as List<TEMP_XFSTD1320_DETAIL>;
            if(treeList != null)
                if(treeList.Count > 0)
                {
                    //treeList.Remove(detailObj); // 이게 실행이 되면 TreeListBindingSource에 영향을 미친다. 
                    treeList = treeList.Where(p => p.SEQ != detailObj.SEQ).ToList();
                    int iCount_Level_0 = treeList.Where(p => p.LEVEL == 0).Count();
                    int iCount_Level_1 = treeList.Where(p => p.LEVEL >= 1).Count();

                    if (iCount_Level_0 == 0 && iCount_Level_1 >= 1)
                    {
                        //MessageBoxHandler.Show("최상위는 1개는 존재해야 합니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("ChildData")), LabelConvert.GetLabelText("Warning"));
                        return;
                    }
                }

            delArr.Add(detailObj);
            TreeListBindingSource.RemoveCurrent();
            //TreeListBindingSource.Remove(detailObj); // 이걸 사용하면 오류는 안나는데 칸이 안지워짐
            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
        }

    }
}