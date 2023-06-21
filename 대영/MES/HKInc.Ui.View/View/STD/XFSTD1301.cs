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
    /// BOM관리화면
    /// </summary>
    public partial class XFSTD1301 : HKInc.Service.Base.TreeListFormTemplate
    {
        #region 전역변수
        IService<TN_STD1300> ModelService = (IService<TN_STD1300>)ProductionFactory.GetDomainService("TN_STD1300");

        /// <summary>
        /// 20211110 오세완 차장 
        /// 타입추가를 통해서 추가를 진행한 경우는 저장시 검증이 필요하여 추가
        /// </summary>
        private bool gb_UsedType = false;
        #endregion 
        public XFSTD1301()
        {
            InitializeComponent();
            TreeListExControl = treeListEx1;
            TreeListExControl.TreeList.CellValueChanged += TreeList_CellValueChanged;
            TreeListExControl.TreeList.ShowingEditor += TreeList_ShowingEditor;

            // 20211110 오세완 차장 BOM TYPE 팝업 추가
            TreeListExControl.ActExportClicked += TreeListExControl_ActExportClicked;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));


        }

        private void TreeListExControl_ActExportClicked(object sender, ItemClickEventArgs e)
        {
            DataExport();
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && p.TopCategory == MasterCodeSTR.TopCategory_WAN).ToList());

        }

        protected override void InitGrid()
        {
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.TreeList.StateImageList = IconImageList;

            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("HighLevelAdd") + "[F3]"); // "최상위추가[F3]"
            IsGridButtonFileChooseEnabled = true;
            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("LowLevelAdd") + "[F10]");// "하위추가[F10]"
            TreeListExControl.SetToolbarShotKeyChange(GridToolbarButton.FileChoose, new BarShortcut(Keys.F10));

            // 20211102 오세완 차장 bom type 추가 팝업 버튼 추가
            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.Export, LabelConvert.GetLabelText("TypeAdd"), IconImageList.GetIconImage("spreadsheet/grouprows"));
            IsGridButtonExportEnabled = true;


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
            // 20210520 오세완 차장 단위가 나오지 않아서 생략처리 풀어줌
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            TreeListExControl.SetRepositoryItemSpinEdit("UseQty", true, DefaultBoolean.Default, "n3", true);
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemCheckEdit("UseFlag", "N");
            TreeListExControl.SetRepositoryItemCheckEdit("MgFlag", "N");
            TreeListExControl.SetRepositoryItemSpinEdit("DisplayOrder");

            // 20210119 오세완 차장 트리컨트롤에서 메모팝업을 사용하는 경우 아래 예제처럼 사용바람
            TreeListExControl.TreeList.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(TreeListExControl, "Memo", UserRight.HasEdit);
            
            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
        }

        protected override void DataLoad()
        {
            TreeListExControl.Clear();
            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                var ItemCode = new SqlParameter("@ITEM_CODE", lup_Item.EditValue.GetNullToEmpty());

                //var result = context.Database.SqlQuery<TEMP_STD1300>("USP_GET_STD1300_LIST @ITEM_CODE", ItemCode).ToList();
                var result = context.Database.SqlQuery<TEMP_STD1300>("USP_GET_STD1300_LIST_V2 @ITEM_CODE", ItemCode).ToList(); // 20210910 오세완 차장 반제품에 하위 반제품을 출력하는 버전으로 교체

                if (result == null)
                    return;

                TreeListBindingSource.DataSource = result.Where(p => (radioValue == "A" ? true : p.UseFlag == radioValue)).ToList();
            }

            TreeListExControl.DataSource = TreeListBindingSource;

            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.OptionsView.AutoWidth = false;
            TreeListExControl.TreeList.BestFitColumns();
            gb_UsedType = false;
        }

        /// <summary>
        /// 20211110 오세완 차장
        /// 타입으로 불러와서 저장하는 경우는 상위코드가 존재하는 경우는 db에선 오류를 알수 있으나 여기서는 그냥 무시되기 때문에 확인이 필요
        /// </summary>
        /// <returns></returns>
        private bool Check_Duplcate_Item()
        {
            bool bFind = false;

            List<TEMP_STD1300> tempArr = TreeListBindingSource.List as List<TEMP_STD1300>;
            if (tempArr != null)
                if (tempArr.Count > 0)
                {
                    TEMP_STD1300 top = tempArr.Where(p => p.Level == 0).FirstOrDefault();
                    if (top != null)
                    {
                        List<TN_STD1300> std_Arr = ModelService.GetList(p => p.ItemCode == top.ItemCode).ToList();
                        if (std_Arr != null)
                            if (std_Arr.Count > 0)
                                bFind = true;
                    }
                }

            return bFind;
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

                if(gb_UsedType)
                {
                    bool bDup = Check_Duplcate_Item();
                    if(bDup)
                    {
                        MessageBoxHandler.Show("해당 품목인 이미 구성된 BOM입니다. 확인바랍니다.");
                        return;
                    }
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
                                v.MgFlag = "N";
                            
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

                        if (K == 0)
                        {

                        }
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
            if (!IsFirstLoaded) ActRefresh();
            var obj = TreeListBindingSource.Current as TEMP_STD1300;
            if (obj == null)
                return;

            //if (obj.Level > 0)
            //{
            //    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32));
            //    //MessageBoxHandler.Show("최상위에 대해서만 하위추가를 하실 수 있습니다.");
            //    return;
            //}

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
            if (!IsFirstLoaded) ActRefresh();
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

        private void TreeList_CellValueChanged_V2(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            TEMP_STD1300 obj = TreeListBindingSource.Current as TEMP_STD1300;
            if (obj == null)
                return;

            if (e.Column.FieldName == "ItemCode")
            {
                List<TEMP_STD1300> tree_Arr = TreeListBindingSource.List as List<TEMP_STD1300>;
                TN_STD1100 itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();
                List<TEMP_STD1300> bomchk_Arr = tree_Arr.Where(p => p.BomCode == obj.ParentBomCode).ToList();
                if (bomchk_Arr.Count > 0)
                {
                    // bom 하위구조 확인
                    TEMP_STD1300 bom_top = tree_Arr.Where(p => p.BomCode == obj.ParentBomCode).FirstOrDefault();
                    if (bom_top != null)
                    {
                        if (bom_top.TopCategory == MasterCodeSTR.TopCategory_WAN && itemObj.TopCategory == MasterCodeSTR.TopCategory_WAN)
                        {
                            MessageBoxHandler.Show("완제품 하위에 완제품구성은 불가합니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                            TreeListBindingSource.RemoveCurrent();
                            TreeListExControl.TreeList.BestFitColumns();
                            return;
                        }

                        if (bom_top.TopCategory == MasterCodeSTR.TopCategory_BAN || bom_top.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)
                        {
                            if (itemObj.TopCategory == MasterCodeSTR.TopCategory_WAN)
                            {
                                MessageBoxHandler.Show("반제품 하위에 완제품은 구성이 불가합니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                                TreeListBindingSource.RemoveCurrent();
                                TreeListExControl.TreeList.BestFitColumns();
                                return;
                            }
                        }
                    }

                    // 품번중복확인
                    bool bDup_Itemcode = false;
                    List<TEMP_STD1300> item_dup_Arr = tree_Arr.Where(p => (p.ParentBomCode == obj.ParentBomCode) && (p.ItemCode == obj.ItemCode)).ToList();
                    if (item_dup_Arr != null)
                        if (item_dup_Arr.Count > 0)
                            bDup_Itemcode = true;

                    if (bDup_Itemcode)
                    {
                        MessageBoxHandler.Show("품번이 중복됩니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                        TreeListBindingSource.RemoveCurrent();
                        TreeListExControl.TreeList.BestFitColumns();
                        return;
                    }
                }

                if(itemObj != null)
                {
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

                if (gb_UsedType)
                {
                    // 20211111 오세완 차장 bom type으로 정보를 가져온 후 구조를 변경할때 부모code가 변경되는 경우 재구성이 필요
                    Update_ParentBomCode(obj, tree_Arr);
                }
                
            }
            else if (e.Column.FieldName == "UseQty")
            {
                if(obj.UseQty.GetDecimalNullToZero() < 0)
                {
                    MessageBoxHandler.Show("소요량에 음수는 불가합니다.");
                    obj.UseQty = 0;
                }
            }

            TreeListExControl.TreeList.BestFitColumns();
        }

        /// <summary>
        /// 20211129 오세완 차장
        /// bom type으로 정보를 가져온 후 구조를 변경할때 부모code가 변경되는 경우 재구성이 필요
        /// </summary>
        /// <param name="updObj"></param>
        /// <param name="gridArr"></param>
        private void Update_ParentBomCode(TEMP_STD1300 updObj, List<TEMP_STD1300> gridArr)
        {
            if(updObj.Level == 0)
            {
                // 부모가 변경되기 때문에 하위 일방적으로 적용한다. 
                List<TEMP_STD1300> level1_Arr = gridArr.Where(p => p.Level == 1 && p.ParentBomCode != "").ToList();
                if(level1_Arr != null)
                    if(level1_Arr.Count > 0)
                    {
                        foreach (TEMP_STD1300 each in level1_Arr)
                            each.ParentBomCode = updObj.BomCode;
                    }
            }
            else if(updObj.Level == 1)
            {
                // 선택한 하위의 제품 중에서 부모가 없는 것을 찾아서 매칭해야 하는데 level을 2로 제한해야 하나? 하위까지 다 챙겨야 하나?
                // 20211129 오세완 차장 변경하는 레벨에서 하위만 책임지면 되는 구조로 일단.
                List<TEMP_STD1300> level2_Arr = gridArr.Where(p => p.Level == 2 && p.ParentBomCode != "").ToList();
                if (level2_Arr != null)
                    if (level2_Arr.Count > 0)
                    {
                        foreach(TEMP_STD1300 each in level2_Arr)
                        {
                            int iCount = 0;
                            List<TEMP_STD1300> count_Arr = gridArr.Where(p => p.ParentBomCode == each.BomCode).ToList();

                        }
                    }
            }
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
                    var bomrealchk = objList.Where(p => p.BomCode == obj.ParentBomCode).FirstOrDefault();

                    if (bomrealchk.TopCategory == "A00" && itemObj.TopCategory == "A00")
                    {
                        MessageBoxHandler.Show("완제품 하위에 완제품구성은 불가합니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                        TreeListBindingSource.RemoveCurrent();
                        TreeListExControl.TreeList.BestFitColumns();
                        return;
                    }

                    if (bomrealchk.TopCategory == "A01" && itemObj.TopCategory == "A01")
                    {
                        // 20210812 오세완 차장 대영은 반제품 밑에 특정 용접품에 한하여 반제품 구성이 가능해야 해서 제외 처리
                        //MessageBoxHandler.Show("반제품 하위에 반제품은 구성이 불가합니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                        //TreeListBindingSource.RemoveCurrent();
                        //TreeListExControl.TreeList.BestFitColumns();
                        //return;
                    }

                    if (bomrealchk.TopCategory == "A01" && itemObj.TopCategory == "A00")
                    {
                        MessageBoxHandler.Show("반제품 하위에 완제품은 구성이 불가합니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                        TreeListBindingSource.RemoveCurrent();
                        TreeListExControl.TreeList.BestFitColumns();
                        return;
                    }

                    var samechk = objList.Where(p => (p.ParentBomCode == obj.ParentBomCode) && (p.ItemCode == obj.ItemCode)).ToList();

                    if (samechk.Count() > 1)
                    {
                        DialogResult okyn = MessageBoxHandler.Show("품번이 중복됩니다. \r\n 계속 하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (okyn == DialogResult.Yes)
                        {

                        }
                        else
                        {
                            obj.ItemName = string.Empty;
                            obj.ItemName1 = string.Empty;
                            obj.TopCategory = string.Empty;
                            obj.MiddleCategory = string.Empty;
                            obj.BottomCategory = string.Empty;
                            //obj.Purity = itemObj.Purity;
                            obj.Spec1 = string.Empty;
                            obj.Spec2 = string.Empty;
                            obj.Spec3 = string.Empty;
                            obj.Spec4 = string.Empty;
                            obj.Unit = string.Empty;
                            obj.Weight = 0;
                            obj.UpdFlag = "Y";

                            TreeListExControl.TreeList.PostEditor();
                            TreeListExControl.TreeList.BestFitColumns();
                            return;
                        }
                    }
                }

                if (e.Column.FieldName == "UseQty" && obj.UseQty.GetIntNullToZero() < 0)
                {
                    MessageBox.Show("소요량에 음수는 불가합니다.");
                    obj.UseQty = 0;
                }

                if (itemObj == null)
                {
                    
                }
                else
                {
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
            //var obj = TreeListBindingSource.Current as TN_STD1300;
            //if (obj == null) return;

            //var fieldName = TreeListExControl.TreeList.FocusedColumn.FieldName;
            //if (fieldName == "UseQty")
            //{
            //    if (obj.Level == 0)
            //    {
            //        e.Cancel = true;
            //    }
            //    else
            //    {
            //        var parentBomId = obj.ParentBomCode;
            //        var list = TreeListBindingSource.List as List<TN_STD1300>;
            //        if (list.Any(p => p.ParentBomCode == parentBomId && p.BomCode != obj.BomCode && p.UseQty > 0))
            //        {
            //            e.Cancel = true;
            //        }
            //    }
            //}

            TreeListExControl.TreeList.OptionsView.AutoWidth = false;
            //TreeListExControl.TreeList.BestFitColumns();
        }

        protected override void TreeList_MouseDoubleClick(object sender, MouseEventArgs e) { }

        /// <summary>
        /// 20211129 오세완 차장 bom type 팝업 출력
        /// </summary>
        protected override void DataExport()
        {
            if (!UserRight.HasEdit)
                return;

            List<TEMP_STD1300> treeList = TreeListBindingSource.List as List<TEMP_STD1300>;
            if(treeList != null)
                if(treeList.Count > 0)
                {
                    MessageBoxHandler.Show("타입을 불러오기 위해서는 하위 항목이 없어야 합니다.");
                    return;
                }

            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1320, param, AddTypeCallBack);
            form.ShowPopup(true);
        }

        /// <summary>
        /// 20211129 오세완 차장 
        /// bom type 가져온 후 bomcode와 parentbomcode 재구성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTypeCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null)
                return;

            string sBomTypeCode = e.Map.GetValue(PopupParameter.ReturnObject).GetNullToEmpty();
            List<TN_STD1320> tempArr = ModelService.GetChildList<TN_STD1320>(p => p.TypeCode == sBomTypeCode);
            if(tempArr != null)
                if(tempArr.Count > 0)
                {
                    TN_STD1320 std1320 = tempArr.FirstOrDefault();
                    if(std1320 != null)
                    {
                        if (std1320.TN_STD1321List != null)
                        {
                            List<TEMP_STD1300> returnArr = new List<TEMP_STD1300>();
                            foreach (TN_STD1321 each in std1320.TN_STD1321List)
                            {
                                TEMP_STD1300 type_Insert = new TEMP_STD1300()
                                {
                                    BomCode = DbRequestHandler.GetSeqStandard("BOM"),
                                    UseQty = each.UseQty,
                                    Level = each.Level,
                                    UseFlag = each.UseFlag,
                                    DisplayOrder = each.DisplayOrder,
                                    MgFlag = each.MgFlag,
                                    ItemCode = each.ItemCode,
                                    ProcessCode = each.ProcessCode,
                                    Memo = each.Memo
                                };

                                if (each.TN_STD1100 != null)
                                {
                                    type_Insert.ItemName = each.TN_STD1100.ItemName;
                                    type_Insert.ItemName1 = each.TN_STD1100.ItemName1;
                                }

                                if(each.ParentItemCode.GetNullToEmpty() != "")
                                {
                                    TEMP_STD1300 parent_std = returnArr.Where(p => p.ItemCode == each.ParentItemCode).FirstOrDefault();
                                    if (parent_std != null)
                                        type_Insert.ParentBomCode = parent_std.BomCode;
                                }

                                returnArr.Add(type_Insert);
                            }

                            if (returnArr != null)
                                if (returnArr.Count > 0)
                                {
                                    if (TreeListBindingSource.DataSource == null)
                                    {
                                        TreeListBindingSource.DataSource = returnArr;
                                        TreeListExControl.DataSource = TreeListBindingSource;
                                    }
                                    else
                                    {
                                        foreach (TEMP_STD1300 each in returnArr)
                                            TreeListBindingSource.Add(each);
                                    }
                                }
                                    

                            //TreeListBindingSource.Add(returnArr);
                            TreeListExControl.ExpandAll();
                            TreeListExControl.TreeList.BestFitColumns();
                            TreeListExControl.TreeList.OptionsView.AutoWidth = true;
                            gb_UsedType = true;
                        }
                    }
                }
        }
    }
}