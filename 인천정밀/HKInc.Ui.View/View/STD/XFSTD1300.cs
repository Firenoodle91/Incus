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
    public partial class XFSTD1300 : HKInc.Service.Base.TreeListFormTemplate
    {
        IService<TN_STD1300> ModelService = (IService<TN_STD1300>)ProductionFactory.GetDomainService("TN_STD1300");
        List<TEMP_STD1300> MainDateList = new List<TEMP_STD1300>();

        public XFSTD1300()
        {
            InitializeComponent();
            TreeListExControl = treeListEx1;
            TreeListExControl.TreeList.CellValueChanged += TreeList_CellValueChanged;
            //TreeListExControl.TreeList.NodeCellStyle += TreeList_NodeCellStyle;
            TreeListExControl.TreeList.ShowingEditor += TreeList_ShowingEditor;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));


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

            TreeListExControl.AddColumn("BomCode", LabelConvert.GetLabelText("BomCode"), false);
            TreeListExControl.AddColumn("ParentBomCode", LabelConvert.GetLabelText("ParentBomCode"), false);
            TreeListExControl.AddColumn("Level", LabelConvert.GetLabelText("Level"), false);
            TreeListExControl.AddColumn("CustomItemCode", LabelConvert.GetLabelText("ItemCode"));
            TreeListExControl.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemName"));
            /*TreeListExControl.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            TreeListExControl.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));*/
            TreeListExControl.AddColumn("TopCategory", LabelConvert.GetLabelText("TopCategory"));
            TreeListExControl.AddColumn("MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            TreeListExControl.AddColumn("BottomCategory", LabelConvert.GetLabelText("BottomCategory"));
            //TreeListExControl.AddColumn("Purity", LabelConvert.GetLabelText("Purity"));
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
            TreeListExControl.AddColumn("MgFlag", LabelConvert.GetLabelText("MgFlag"), false); // 2022-01-19 오세완 차장 수동관리 하는 제품이 하나도 없어서 아예 출력을 생략


            //TreeListExControl.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessCode"));

            //TreeListExControl.AddColumn("UseQtyEx", LabelConvert.GetLabelText("UseQtyEx"));
            //TreeListExControl.AddColumn("WorkStandard", LabelConvert.GetLabelText("WorkStandard"), false);

            //TreeListExControl.AddColumn("ItemCodeEx", LabelConvert.GetLabelText("ItemCodeEx"));


            //TreeListExControl.AddColumn("CustomItemCombineSpec", LabelConvert.GetLabelText("Spec"));
            //TreeListExControl.AddColumn("CustomItemUnit", LabelConvert.GetLabelText("Unit"));

            TreeListExControl.KeyFieldName = "BomCode";
            TreeListExControl.ParentFieldName = "ParentBomCode";
            //TreeListExControl.SetTreeListEditable(UserRight.HasEdit, "ItemCode", "UseQty", "DisplayOrder", "Memo", "ProcessCode", "UseQtyEx", "WorkStandard", "ItemCodeEx", "UseFlag");
            //TreeListExControl.SetTreeListEditable(UserRight.HasEdit, "ItemCode", "UseQty", "DisplayOrder", "Memo", "ProcessCode", "UseFlag", "MgFlag");
            TreeListExControl.SetTreeListEditable(UserRight.HasEdit, "ItemCode", "UseQty", "DisplayOrder", "Memo", "ProcessCode", "UseFlag"); // 2022-01-19 오세완 차장 수동관리 하는 제품이 하나도 없어서 아예 출력을 생략

            TreeListExControl.TreeList.OptionsView.AutoWidth = false;

            TreeListExControl.TreeList.Columns[0].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[1].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[2].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[3].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[4].Fixed =  DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            TreeListExControl.TreeList.Columns[5].Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;

            //TreeListExControl.TreeList.BestFitColumns();
        }

        protected override void InitRepository()
        {
            //TreeListExControl.SetRepositoryItemSearchLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_SA)).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"));
            //TreeListExControl.SetRepositoryItemSearchLookUpEdit_Bom("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_SA)).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), "TopCategory", "MiddleCategory", "BottomCategory");
            //TreeListExControl.SetRepositoryItemSearchLookUpEdit_Bom("ItemCode", ModelService.GetChildList<VI_STD1100_BOMTREE>(p => (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName")); 
            TreeListExControl.SetRepositoryItemSearchLookUpEdit_Bom("ItemCode", ModelService.GetChildList<VI_STD1100_BOMTREE>(x => 1 == 1).ToList(), "ItemCode", DataConvert.GetCultureDataFieldName("ItemName")); // 20220119 오세완 차장 케이즈이노텍 스타일 반영
            /*TreeListExControl.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));            */
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            // 20210520 오세완 차장 단위가 나오지 않아서 생략처리 풀어줌
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            TreeListExControl.SetRepositoryItemSpinEdit("UseQty", true, DefaultBoolean.Default, "n3", true);
            TreeListExControl.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeListExControl.SetRepositoryItemCheckEdit("UseFlag", "N");
            TreeListExControl.SetRepositoryItemCheckEdit("MgFlag", "N");
            //TreeListExControl.SetRepositoryItemCheckEdit("UseQtyEx", "N");
            //TreeListExControl.SetRepositoryItemCheckEdit("ItemCodeEx", "N");
            TreeListExControl.SetRepositoryItemSpinEdit("DisplayOrder");

            // 20210119 오세완 차장 트리컨트롤에서 메모팝업을 사용하는 경우 아래 예제처럼 사용바람
            TreeListExControl.TreeList.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(TreeListExControl, "Memo", UserRight.HasEdit);
            //TreeListExControl.TreeList.Columns["WorkStandard"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(TreeListExControl, "WorkStandard", UserRight.HasEdit);
            //TreeListExControl.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(TreeListExControl, "Memo", UserRight.HasEdit);
            //TreeListExControl.SetRepositoryItemSearchLookUpEdit("CustomItemUnit", DbRequestHandler.GetCommCode(MasterCodeSTR.Unit, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

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

            //var Item = lup_Item.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            /*TreeListBindingSource.DataSource = ModelService.GetList(p => (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                      && (string.IsNullOrEmpty(Item) ? true : p.ItemCode == Item)
                                                                   )
                                                                 .ToList();*/

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                var ItemCode = new SqlParameter("@ITEM_CODE", lup_Item.EditValue.GetNullToEmpty());



                var result = context.Database.SqlQuery<TEMP_STD1300>("USP_GET_STD1300_LIST @ITEM_CODE", ItemCode).ToList();

                if (result == null) return;

                MainDateList.AddRange(result);

                TreeListBindingSource.DataSource = result.Where(p => (radioValue == "A" ? true : p.UseFlag == radioValue)).ToList();
                //TreeListBindingSource.DataSource = MainDateList.Where(p => (radioValue == "A" ? true : p.UseFlag == radioValue)).ToList();
            }

            //.Where(p => (radioValue == "A" ? true : p.UseFlag == radioValue)



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
                    Decimal? disp;
                    string parentbomcode = null;

                    var chk = ModelService.GetList(p => p.BomCode == v.BomCode).ToList();

                    if (chk.Count > 0)
                    {
                        var newObj1 = ModelService.GetList(p => p.BomCode == v.BomCode).First();

                        if (newObj1 != null)
                        {
                            newObj1.ItemCode = v.ItemCode;
                            newObj1.ItemName = v.ItemName;
                            newObj1.TopCategory = v.TopCategory;
                            newObj1.MiddleCategory = v.MiddleCategory;
                            newObj1.BottomCategory = v.BottomCategory;
                            //newObj1.Purity = v.Purity;
                            newObj1.Spec1 = v.Spec1;
                            newObj1.Spec2 = v.Spec2;
                            newObj1.Spec3 = v.Spec3;
                            newObj1.Spec4 = v.Spec4;
                            newObj1.Unit = v.Unit;
                            newObj1.Weight = v.Weight.GetNullToZero();
                            //newObj1.ParentBomCode = v.ParentBomCode;
                            newObj1.Level = v.Level;
                            newObj1.UseQty = v.UseQty;
                            newObj1.ProcessCode = v.ProcessCode;
                            newObj1.DisplayOrder = v.DisplayOrder;
                            //newObj1.UseQtyEx = v.UseQtyEx;
                            //newObj1.WorkStandard = v.WorkStandard;
                            //newObj1.ItemCodeEx = v.ItemCodeEx;
                            newObj1.UseFlag = v.UseFlag;
                            newObj1.Memo = v.Memo;
                            if (v.MgFlag == "")
                            {
                                v.MgFlag = "N";
                            }
                            newObj1.MgFlag = v.MgFlag;

                            ModelService.Update(newObj1);
                            //ModelService.Save();

                        }
                    }
                    else
                    {
                        //var newObj2 = new TN_STD1300
                        //{
                        //    BomCode = v.BomCode,
                        //    ItemCode = v.ItemCode,
                        //    ItemName = v.ItemName,
                        //    TopCategory = v.TopCategory,
                        //    MiddleCategory = v.MiddleCategory,
                        //    BottomCategory = v.BottomCategory,
                        //    Purity = v.Purity,
                        //    Spec1 = v.Spec1,
                        //    Spec2 = v.Spec2,
                        //    Spec3 = v.Spec3,
                        //    Spec4 = v.Spec4,
                        //    Unit = v.Unit,
                        //    Weight = v.Weight.GetNullToZero(),
                        //    ParentBomCode = v.ParentBomCode,
                        //    Level = v.Level,
                        //    UseQty = v.UseQty,
                        //    ProcessCode = v.ProcessCode,
                        //    DisplayOrder = v.DisplayOrder,
                        //    UseQtyEx = v.UseQtyEx,
                        //    WorkStandard = v.WorkStandard,
                        //    ItemCodeEx = v.ItemCodeEx,
                        //    UseFlag = v.UseFlag,
                        //    Memo = v.Memo,
                        //};


                        string sql = "exec SP_STD1300_INSERT  @BomCode='" + v.BomCode +
                                                           "',@ItemCode='" + v.ItemCode;

                        // 20220119 오세완 차장 품목명에 '가 있는 경우 mssql에서 인식을 못해서 '를 더 추가처리
                        string sTemp_itemname = "";
                        if(v.ItemName.Contains("'"))
                        {
                            sTemp_itemname = v.ItemName.Replace("'", "''");
                        }
                        else
                        {
                            sTemp_itemname = v.ItemName;
                        }

                        sql += "',@ItemName='" + sTemp_itemname + "'";

                        if (v.ParentBomCode == null)
                        {
                            sql = sql + ",@ParentBomCode =Null";

                        }
                        else
                        {
                            sql = sql + ",@ParentBomCode ='" + v.ParentBomCode + "'";
                        }





                        sql = sql + ",@Level=" + v.Level +
                                                           ",@TopCategory ='" + v.TopCategory +
                                                           "',@MiddleCategory  = '" + v.MiddleCategory +
                                                           "',@BottomCategory = '" + v.BottomCategory +
                                                           //"',@Purity = '" + v.Purity +
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
                                                           //"',@UseQtyEx = '" + v.UseQtyEx +
                                                           //"',@WorkStandard = '" + v.WorkStandard +
                                                           "',@Memo = '" + v.Memo +
                                                           "',@MgFlag = '" + v.MgFlag + "'";
                        //"',@ItemCodeEx = '" + v.ItemCodeEx + "'";


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
            if (!IsFirstLoaded) ActRefresh();



            var newObj = new TEMP_STD1300()
            {
                BomCode = DbRequestHandler.GetSeqStandard("BOM"),
                UseQty = 1,
                Level = 0,
                UseFlag = "Y",
                DisplayOrder = null,
                RowIndex = 0,
                UpdFlag = "Y",
                //NewRowFlag = "Y"
                MgFlag = "N" // 20210606 오세완 차장 중요한 값이라서 추가 처리
            };

            //var newObj1 = new TN_STD1300()
            //{
            //    BomCode = newObj.BomCode,
            //    UseQty = 1,
            //    Level = 0,
            //    UseFlag = "Y",
            //    NewRowFlag = "Y"

            //};


            TreeListBindingSource.Add(newObj);
            //TreeListExControl.TreeList.OptionsView.NewItemRowPosition = DevExpress.XtraTreeList.TreeListNewItemRowPosition.Top;
            //TreeListExControl.TreeList.focus
            //ModelService.Insert(newObj1);
            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.BestFitColumns();
            TreeListExControl.TreeList.OptionsView.AutoWidth = true;

            //TreeNode node = TreeListExControl.TreeList.FindNode(newObj.BomCode, true);

            //TreeListExControl.TreeList.FocusedNode = TreeListExControl.TreeList.Nodes[node];

            TreeListExControl.TreeList.MoveLast();

        }

        /// <summary>
        /// 하위레벨 등록
        /// </summary>
        protected override void FileChooseClicked()
        {
            if (!IsFirstLoaded) ActRefresh();
            //var obj = TreeListBindingSource.Current as TN_STD1300;
            var obj = TreeListBindingSource.Current as TEMP_STD1300;
            if (obj == null) return;

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

            //var newObj1 = new TN_STD1300()
            //{
            //    BomCode = newObj.BomCode,
            //    ParentBomCode = obj.BomCode,
            //    UseQty = 0,
            //    Level = obj.Level + 1,
            //    UseFlag = "Y"
            //};

            TreeListBindingSource.Add(newObj);
            //ModelService.Insert(newObj);
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
            //var obj = TreeListBindingSource.Current as TN_STD1300;
            var obj = TreeListBindingSource.Current as TEMP_STD1300;
            if (obj == null) return;

            //var objList = TreeListBindingSource.List as List<TN_STD1300>;
            var objList = TreeListBindingSource.List as List<TEMP_STD1300>;
            if (objList.Any(p => p.ParentBomCode == obj.BomCode))
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("ChildData")), LabelConvert.GetLabelText("Warning"));
                return;
            }

            //var delobj = new TN_STD1300()
            //{
            //    BomCode = obj.BomCode                                
            //};

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
            //var obj = TreeListBindingSource.Current as TN_STD1300;
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

                    //if (bomrealchk.TopCategory == "A00" && (bomrealchk.MiddleCategory != "A002" && bomrealchk.MiddleCategory != "A003" && bomrealchk.MiddleCategory != "A009") && itemObj.TopCategory == "A01")
                    if (bomrealchk.TopCategory == "A00" && itemObj.TopCategory == "A00")
                    {

                        MessageBoxHandler.Show("완제품 하위에 완제품구성은 불가합니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                        TreeListBindingSource.RemoveCurrent();
                        TreeListExControl.TreeList.BestFitColumns();
                        return;
                    }

                    // 20220119 오세완 차장 케이즈이노텍 스타일 반영
                    //if (bomrealchk.TopCategory == "A01" && itemObj.TopCategory == "A01")
                    //{

                    //    MessageBoxHandler.Show("반제품 하위에 반제품은 구성이 불가합니다. 확인하여 주십시오.", LabelConvert.GetLabelText("Warning"));
                    //    TreeListBindingSource.RemoveCurrent();
                    //    TreeListExControl.TreeList.BestFitColumns();
                    //    return;
                    //}

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


                //obj.TN_STD1100 = itemObj;
                if (itemObj == null)
                {
                    //obj.TN_STD1100 = null;
                }
                else
                {

                    obj.ItemName = itemObj.ItemName;
                    obj.TopCategory = itemObj.TopCategory;
                    obj.MiddleCategory = itemObj.MiddleCategory;
                    obj.BottomCategory = itemObj.BottomCategory;
                    //obj.Purity = itemObj.Purity;
                    obj.Spec1 = itemObj.Spec1;
                    obj.Spec2 = itemObj.Spec2;
                    obj.Spec3 = itemObj.Spec3;
                    obj.Spec4 = itemObj.Spec4;
                    obj.Unit = itemObj.Unit;
                    obj.Weight = itemObj.Weight.GetNullToZero();
                    obj.UpdFlag = "Y";
                    /*UseQty = v.UseQty,
                         ProcessCode = v.ProcessCode,
                         DisplayOrder = v.DisplayOrder,
                         UseQtyEx = v.UseQtyEx,
                         WorkStandard = v.WorkStandard,
                         ItemCodeEx = v.ItemCodeEx,
                         UseFlag = v.UseFlag,*/

                    //obj.TN_STD1100 = itemObj;

                }

                //var newObj1 = ModelService.GetList(p => p.BomCode == obj.BomCode).First();




                //ModelService.Update(newObj1);
                TreeListExControl.TreeList.BestFitColumns();
                //TreeListExControl.TreeList.OptionsView.AutoWidth = true;
            }
        }

        private void TreeList_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            //if (e.Column.FieldName == "UseQty")
            //{
            //    var level = e.Node.GetValue("Level").GetIntNullToZero();

            //    if (level == 0)
            //    {
            //        e.Appearance.BackColor = Color.Empty;
            //    }
            //    else
            //    {
            //        var parentBomId = e.Node.GetValue("ParentBomCode").GetNullToEmpty();
            //        var BomCode = e.Node.GetValue("BomCode").GetNullToEmpty();
            //        var list = TreeListBindingSource.List as List<TN_STD1300>;
            //        if (list.Any(p => p.ParentBomCode == parentBomId && p.BomCode != BomCode && p.UseQty > 0))
            //        {
            //            e.Appearance.BackColor = Color.Empty;
            //        }
            //        else
            //            e.Appearance.BackColor = GlobalVariable.GridEditableColumnColor;
            //    }
            //}
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
    }
}