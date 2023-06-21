using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;

using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 메뉴접속로그
    /// </summary>
    public partial class MenuOpenLog : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<LoginLog> ModelService;
        BindingSource bindingSource = new BindingSource();
        
        public MenuOpenLog()
        {
            InitializeComponent();
        }

        public MenuOpenLog(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;

            ModelService = (IService<LoginLog>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarButtonVisible(false);                        
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }
        
        protected override void InitGrid()
        {
            gridEx1.Init();
            
            gridEx1.AddColumn("MenuLogId", false);
            gridEx1.AddColumn("LoginLogId", false);
            gridEx1.AddColumn("MenuId");
            gridEx1.AddColumn("Menu.MenuName", LabelConvert.GetLabelText("MenuName"));
            gridEx1.AddColumn("Menu.Screen.ScreenName", LabelConvert.GetLabelText("ScreenName"));
            gridEx1.AddColumn("Menu.Screen.NameSpace", LabelConvert.GetLabelText("NameSpace"));
            gridEx1.AddColumn("Menu.Screen.ClassName", LabelConvert.GetLabelText("ClassName"));
            gridEx1.AddColumn("OpenTime", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
            gridEx1.AddColumn("CloseTime", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");

            gridEx1.BestFitColumns();
            gridEx1.GridLayoutRestore();
        }
        
        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            bindingSource.DataSource = (List<MenuLog>)PopupParam.GetValue(PopupParameter.DataParam);
            gridEx1.DataSource = bindingSource;

            gridEx1.BestFitColumns();
        }        
    }
}
