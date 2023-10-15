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
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using DevExpress.Utils;
using DevExpress.XtraBars;
using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraEditors.Repository;

namespace HKInc.Ui.View.View.STD
{
    public partial class XFSTD1400 : HKInc.Service.Base.ListMasterTreeFormTemplate
    {
        IService<TN_STD1400> ModelService = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");
        IService<User> UserService = (IService<User>)ServiceFactory.GetDomainService("User");
        public XFSTD1400()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            TreeListExControl = treeListEx1;

            MasterGridExControl.MainGrid.MainView.FocusedRowChanged += MasterGrid_FocusedRowChangedEvent;

            TreeListExControl.TreeList.NodeCellStyle += TreeList_NodeCellStyleEvent;
            TreeListExControl.TreeList.ShowingEditor += TreeList_ShowingEditorEvent;
            TreeListExControl.TreeList.CellValueChanged += TreeList_CellValueChangedEvet;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }

        protected override void InitGrid()
        {
            TreeListExControl.SetTreeListOption(false);
            TreeListExControl.TreeList.StateImageList = IconImageList;

            IsGridButtonFileChooseEnabled = true;
            IsMasterGridDoubleClick = true;
            IsMasterGridAddPopup = true;

            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("HighLevelAdd") + "[Alt + Q]");
            TreeListExControl.SetToolbarShotKeyChange(GridToolbarButton.AddRow, new BarShortcut(Keys.Alt | Keys.Q));
            TreeListExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("LowLevelAdd") + "[F10]");// "하위추가[F10]"
            TreeListExControl.SetToolbarShotKeyChange(GridToolbarButton.FileChoose, new BarShortcut(Keys.F10));

            MasterGridExControl.MainGrid.AddColumn("CustomerCode");
            MasterGridExControl.MainGrid.AddColumn("CustomerName");
            MasterGridExControl.MainGrid.AddColumn("CustomerNameENG");
            MasterGridExControl.MainGrid.AddColumn("CustomerNameCHN");
            MasterGridExControl.MainGrid.AddColumn("MyCompanyFlag");
            MasterGridExControl.MainGrid.AddColumn("NationalCode", LabelConvert.GetLabelText("National"));
            MasterGridExControl.MainGrid.AddColumn("CustomerType");
            MasterGridExControl.MainGrid.AddColumn("RegistrationNo");
            MasterGridExControl.MainGrid.AddColumn("CorporationNo");
            MasterGridExControl.MainGrid.AddColumn("CustomerCategoryCode");
            MasterGridExControl.MainGrid.AddColumn("CustomerCategoryType");
            MasterGridExControl.MainGrid.AddColumn("Email");
            MasterGridExControl.MainGrid.AddColumn("RepresentativeName");
            MasterGridExControl.MainGrid.AddColumn("ZipCode");
            MasterGridExControl.MainGrid.AddColumn("Address");
            MasterGridExControl.MainGrid.AddColumn("PhoneNumber");
            MasterGridExControl.MainGrid.AddColumn("FaxNumber");
            MasterGridExControl.MainGrid.AddColumn("TradingStartDate", LabelConvert.GetLabelText("TradingStartDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("TradingEndDate", LabelConvert.GetLabelText("TradingEndDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("DeadLine");
            MasterGridExControl.MainGrid.AddColumn("BusinessManagementId", LabelConvert.GetLabelText("ManagerName"));
            MasterGridExControl.MainGrid.AddColumn("ManagerName",false);
            MasterGridExControl.MainGrid.AddColumn("ManagerPhoneNumber");
            MasterGridExControl.MainGrid.AddColumn("CustomerBankCode");
            MasterGridExControl.MainGrid.AddColumn("AccountNumber");
            MasterGridExControl.MainGrid.AddColumn("Homepage");
            MasterGridExControl.MainGrid.AddColumn("Memo");
            MasterGridExControl.MainGrid.AddColumn("ScmYn", LabelConvert.GetLabelText("ScmYn"));
            MasterGridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            MasterGridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            MasterGridExControl.MainGrid.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            TreeListExControl.AddColumn("CustDeptCD", "SEQ", false);
            TreeListExControl.AddColumn("ParentCustDeptCD", "ParSeq", false);
            TreeListExControl.AddColumn("CustomrCD", LabelConvert.GetLabelText("CustomerCode"), false);
            TreeListExControl.AddColumn("DeptCD", LabelConvert.GetLabelText("Department"));
            TreeListExControl.AddColumn("Manager", LabelConvert.GetLabelText("ManagerName"));
            TreeListExControl.AddColumn("TelNo", LabelConvert.GetLabelText("TelNo"));
            TreeListExControl.AddColumn("Email", LabelConvert.GetLabelText("Email"));
            TreeListExControl.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            TreeListExControl.AddColumn("CreateTime", LabelConvert.GetLabelText("CreateTime"), false);
            TreeListExControl.AddColumn("CreateId", LabelConvert.GetLabelText("CreateId"), false);
            TreeListExControl.AddColumn("UpdateTime", LabelConvert.GetLabelText("UpdateTime"), false);
            TreeListExControl.AddColumn("UpdateId", LabelConvert.GetLabelText("UpdateId"), false);

            TreeListExControl.KeyFieldName = "CustDeptCD";
            TreeListExControl.ParentFieldName = "ParentCustDeptCD";
            TreeListExControl.SetTreeListEditable(UserRight.HasEdit, "DeptCD", "Manager", "TelNo", "Email", "Memo");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("MyCompanyFlag", "N");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("ScmYn", "N");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CustomerType), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("NationalCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.NationalCode), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BusinessManagementId", ModelService.GetChildList<Model.Domain.User>(p => true).ToList(), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DeadLine", DbRequestHandler.GetCommTopCode(MasterCodeSTR.DeadLine), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CreateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("UpdateId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");

            MasterGridExControl.BestFitColumns();
            //TreeListExControl.SetRepositoryItemSearchLookUpEdit("Manager", UserService.GetList(p => p.ScmYn == "Y" && p.Active == "Y" && p.MainYn == "01").ToList(), "LoginId", DataConvert.GetCultureDataFieldName("UserName"));
       //     TreeListExControl.SetRepositoryItemSearchLookUpEdit("Manager", UserService.GetList(p => p.Active == "Y").ToList(), "LoginId", DataConvert.GetCultureDataFieldName("UserName"));
            TreeListExControl.TreeList.Columns["CreateId"].ColumnEdit = RepositoryFactory.GetRepositoryItemLookUpEdit(ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            TreeListExControl.TreeList.Columns["UpdateId"].ColumnEdit = RepositoryFactory.GetRepositoryItemLookUpEdit(ModelService.GetChildList<User>(p => true), "LoginId", "UserName");

            //TreeListExControl.SetRepositoryItemSearchLookUpEdit("DeptCD", ModelService.GetChildList<TN_STD1200>(x => x.UseFlag == "Y").ToList(), "DepartmentCode", DataConvert.GetCultureDataFieldName("DepartmentName"));
            //TreeListExControl.SetRepositoryItemSearchLookUpEdit("Manager", ModelService.GetChildList<Ui.Model.Domain.User>(x => x.Active == "Y").ToList(), "LoginId", "UserName");

            //var UserEdit = TreeListExControl.TreeList.Columns["Manager"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            //UserEdit.Popup += UserEdit_Popup;
        }

        protected override void DataLoad()
        {


            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("CustomerCode", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            TreeListExControl.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624


            var CustomerCodeName = tx_CustomerCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.CustomerName.Contains(CustomerCodeName) || p.CustomerCode.Contains(CustomerCodeName))
                                                                      && (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                    )
                                                                    .OrderBy(p => p.CustomerName)
                                                                    .ToList();

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            //GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(MasterGridExControl);
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            MasterGridBindingSource.EndEdit();

            TreeListExControl.TreeList.PostEditor();
            TreeListBindingSource.EndEdit();

            ModelService.Save();
            //DataLoad();
        }
        
        protected override void MasterGridDeleteRow()
        {
            TN_STD1400 obj = MasterGridBindingSource.Current as TN_STD1400;

            if (obj != null)
            {
                DialogResult result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("CustomerInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    obj.UseFlag = "N";
                    MasterGridExControl.BestFitColumns();
                }
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1400, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }

        /// <summary>/// 최상위 등록/// </summary>
        protected override void TreeAddRow()
        {
            var obj = MasterGridBindingSource.Current as TN_STD1400;

            TN_STD1401 subObj = new TN_STD1401();
            subObj.CustDeptCD = DbRequestHandler.GetSeqStandard("CUSTDEPT");
            subObj.CustomrCD = obj.CustomerCode;
            subObj.UseYN = true;
            subObj.NewRowFlag = "Y";

            TreeListBindingSource.Add(subObj);
            TreeListExControl.DataSource = TreeListBindingSource;

            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.BestFitColumns();

            ModelService.InsertChild<TN_STD1401>(subObj);
        }

        /// <summary>/// 하위 등록/// </summary>
        protected override void FileChooseClicked()
        {
            var obj = TreeListBindingSource.Current as TN_STD1401;
            if (obj == null || obj.CustomrCD == null) return;

            if (obj.ParentCustDeptCD != null || obj.DeptCD == null)
            {
                //MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_56));
                return;
            }

            TN_STD1401 newObj = new TN_STD1401();
            newObj.CustDeptCD = DbRequestHandler.GetSeqStandard("CUSTDEPT");
            newObj.CustomrCD = obj.CustomrCD;
            newObj.DeptCD = obj.DeptCD;
            newObj.ParentCustDeptCD = obj.CustDeptCD;
            newObj.UseYN = true;

            TreeListBindingSource.Add(newObj);
            ModelService.InsertChild<TN_STD1401>(newObj);

            TreeListExControl.ExpandAll();
            TreeListExControl.TreeList.BestFitColumns();

        }

        protected override void TreeDeleteRow()
        {
            TN_STD1401 obj = TreeListBindingSource.Current as TN_STD1401;
            if (obj == null) return;

            List<TN_STD1401> list = TreeListBindingSource.List as List<TN_STD1401>;

            if (list.Any(x => x.ParentCustDeptCD == obj.CustDeptCD))
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("ChildData")), LabelConvert.GetLabelText("Warning"));
                return;
            }

            obj.UseYN = false;

            TreeListBindingSource.RemoveCurrent();
            ModelService.UpdateChild(obj);
        }

        #region Event
        private void MasterGrid_FocusedRowChangedEvent(object sender, EventArgs e)
        {
            TreeListExControl.Clear();

            var obj = MasterGridBindingSource.Current as TN_STD1400;

            var list = obj.TN_STD1401List.Where(x => x.UseYN == true && x.DeptCD != null).ToList();
            TreeListBindingSource.DataSource = list;
            TreeListExControl.DataSource = TreeListBindingSource;
            TreeListExControl.BestFitColumns();
        }

        private void TreeList_NodeCellStyleEvent(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "DeptCD")
            {
                if (e.Node.GetValue("ParentCustDeptCD").GetNullToNull() == null)
                {
                    e.Appearance.BackColor = Color.Empty;
                }
            }
        }

        private void TreeList_ShowingEditorEvent(object sender, CancelEventArgs e)
        {
            TN_STD1401 obj = TreeListBindingSource.Current as TN_STD1401;
            if (obj == null) return;

            var fieldName = TreeListExControl.TreeList.FocusedColumn.FieldName;
            if (fieldName == "DeptCD")
            {
                if (obj.ParentCustDeptCD != null)
                    e.Cancel = true;
            }

            //if (fieldName == "Manager")
            //{
            //    if (obj.ParentCustDeptCD == null)
            //        e.Cancel = true;
            //}
        }

        private void TreeList_CellValueChangedEvet(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "DeptCD")
            {
                TN_STD1401 obj = TreeListBindingSource.Current as TN_STD1401;
                if (obj == null || obj.ParentCustDeptCD != null) return;

                if (obj.ParentCustDeptCD == null)
                {
                    List<TN_STD1401> list = TreeListBindingSource.List as List<TN_STD1401>;
                    var editlist = list.Where(x => x.ParentCustDeptCD == obj.CustDeptCD && x.UseYN == true).ToList();

                    if (editlist.Count > 0)
                    {
                        foreach (TN_STD1401 s in editlist)
                        {
                            TN_STD1401 edit = list.Where(x => x.CustDeptCD == s.CustDeptCD && x.ParentCustDeptCD == obj.CustDeptCD).FirstOrDefault();

                            edit.DeptCD = obj.DeptCD;
                        }
                    }

                    TreeListBindingSource.DataSource = list;
                }
            }

            //if (e.Column.FieldName == "Manager")
            //{
            //    TN_STD1401 obj = TreeListBindingSource.Current as TN_STD1401;

            //    var userinfo = UserService.GetList(p => p.LoginId == obj.Manager).FirstOrDefault();

            //    obj.TelNo = userinfo.CellPhone;
            //    obj.Email = userinfo.Email;
            //}
        }

        private void UserEdit_Popup(object sender, EventArgs e)
        {
            var MasterObj = MasterGridBindingSource.Current as TN_STD1400;
            var detailObj = TreeListBindingSource.Current as TN_STD1401;
            //var userlist = UserService.GetList(p => p.ScmYn == "Y" && p.Active == "Y" && p.MainYn == "01").ToList();
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[CustomerCode] = '" + MasterObj.CustomerCode + "'";
            //lookup.Properties.View.ActiveFilter.NonColumnFilter = "[CustomerCode] = '" + userlist.Select(p => p.CustomerCode) + "'";
        }
        #endregion

        #region Function

        #endregion
    }
}