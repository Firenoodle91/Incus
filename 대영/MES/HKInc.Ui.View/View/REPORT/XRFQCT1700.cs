using System;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Linq;
using HKInc.Utils;
using HKInc.Utils.Class;
using System.Collections.Generic;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 교육훅련계획 출력물
    /// </summary>
    public partial class XRFQCT1700 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");
        public XRFQCT1700()
        {
            InitializeComponent();
        }

        public XRFQCT1700(TN_QCT1700 obj) : this()
        {
            TN_STD1000 tN_STD1000 = ModelService.GetList(x => x.UseYN == "Y" && x.CodeMain == "A002" && x.CodeVal == obj.EduFlag).FirstOrDefault();

            this.xrCell_Typ.Text = tN_STD1000.GetNullToEmpty();
            //this.xrCE
        }

        public XRFQCT1700(List<TN_QCT1700> list) : this()
        {
            List<PRINT_TN_QCT1700> printList = new List<PRINT_TN_QCT1700>();
            foreach (var s in list)
            {
                TN_STD1000 std1000 = ModelService.GetList(x => x.UseYN == "Y" && x.CodeMain == "A002" && x.CodeVal == s.EduFlag).FirstOrDefault();
                Ui.Model.Domain.User eduObjUser = ModelService.GetChildList<Ui.Model.Domain.User>(x => x.Active == "Y" && x.LoginId == s.EduObj).FirstOrDefault();
                Ui.Model.Domain.User eduIdUser = ModelService.GetChildList<Ui.Model.Domain.User>(x => x.Active == "Y" && x.LoginId == s.EduId).FirstOrDefault();

                PRINT_TN_QCT1700 newObj = new PRINT_TN_QCT1700();
                newObj.EduFlagName = std1000.CodeName;
                newObj.EduContent = s.EduContent;
                newObj.EduOrgan = s.EduOrgan;
                newObj.EduObjName = s.EduObj == null ? "전사원" : eduObjUser.UserName;     //XFQCT1700 MainView_CustomColumnDisplayText 같게 처리
                newObj.EduTime = s.EduTime;
                newObj.EduPlanStart = s.EduPlanStart.ToShortDateString();
                newObj.EduPlanEnd = s.EduPlanEnd.ToShortDateString();
                newObj.EduBudget = (s.EduBudget == null || s.EduBudget == 0) ? "없음" : string.Format("{0: #,##원}", s.EduBudget);   //XFQCT1700 MainView_CustomColumnDisplayText 같게 처리
                newObj.EduStart = s.EduStart.ToShortDateString();
                newObj.EduEnd = s.EduEnd.ToShortDateString();
                newObj.EduIdName = eduIdUser == null ? "없음" : eduIdUser.UserName;
                newObj.Memo = s.Memo;

                printList.Add(newObj);
            }

            DetailReport.DataSource = printList;

            this.xrCell_Typ.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduFlagName") });
            this.xrCell_Content.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduContent") });
            this.xrCell_Organ.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduOrgan") });
            this.xrCell_eduObj.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduObjName") });
            this.xrCell_eduTime.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduTime") });
            this.xrCell_PlanStart.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduPlanStart") });
            this.xrCell_PlanEnd.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduPlanEnd") });
            this.xrCell_Pay.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduBudget") });
            this.xrCell_Start.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduStart") });
            this.xrCell_End.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduEnd") });
            this.xrCell_eduID.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduIdName") });
            this.xrCell_Memo.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "Memo") });

            //DetailReport.DataSource = list;

            //this.xrCell_Typ.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduFlag") });
            //this.xrCell_Content.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduContent") });
            //this.xrCell_Organ.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduOrgan") });
            //this.xrCell_eduObj.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduObj") });
            //this.xrCell_eduTime.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduTime") });
            //this.xrCell_PlanStart.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduPlanStart") });
            //this.xrCell_PlanEnd.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduPlanEnd") });
            //this.xrCell_Pay.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduBudget") });
            //this.xrCell_Start.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduStart") });
            //this.xrCell_End.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduEnd") });
            //this.xrCell_eduID.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "EduId") });
            //this.xrCell_Memo.ExpressionBindings.AddRange(new ExpressionBinding[] { new ExpressionBinding("BeforePrint", "Text", "Memo") });
        }

    }

    public class PRINT_TN_QCT1700
    {
        public string EduFlagName { get; set; }
        public string EduContent { get; set; }
        public string EduOrgan { get; set; }
        public string EduObjName { get; set; }
        public string EduTime { get; set; }
        public string EduPlanStart { get; set; }
        public string EduPlanEnd { get; set; }
        public string EduBudget { get; set; }
        public string EduStart { get; set; }
        public string EduEnd { get; set; }
        public string EduIdName { get; set; }
        public string Memo { get; set; }
    }
}
