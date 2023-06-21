using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain.TEMP;
using System.Collections.Generic;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Linq;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Common;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 2021-06-10 김진우 주임
    /// 용접지그, 검사구 리포트
    /// </summary>
    public partial class XRCHFI_WEL2002 : DevExpress.XtraReports.UI.XtraReport
    {
        public XRCHFI_WEL2002()
        {
            InitializeComponent();
            xrLabel11.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
        }

        /// <summary>
        /// 검사구 리포트
        /// </summary>
        /// <param name="masterObj"></param>
        /// <param name="DetailList"></param>
        public XRCHFI_WEL2002(TN_CHF2000 masterObj, List<TN_CHF2003> DetailList) : this()
        {
            try
            {
                if (masterObj == null || DetailList == null)
                    return;

                lblTitle.Text = "검사구 이력서";
                TN_CHF2000 TN_WEL2000T = masterObj;

                if (masterObj != null && masterObj.FileUrl != null)
                    xrPictureBox1.ImageUrl = GlobalVariable.HTTP_SERVER + masterObj.FileUrl;
                if (masterObj != null && masterObj.FileUrl1 != null)
                    xrPictureBox2.ImageUrl = GlobalVariable.HTTP_SERVER + masterObj.FileUrl1;
                if (masterObj != null && masterObj.FileUrl2 != null)
                    xrPictureBox3.ImageUrl = GlobalVariable.HTTP_SERVER + masterObj.FileUrl2;
                if (masterObj != null && masterObj.FileUrl3 != null)
                    xrPictureBox4.ImageUrl = GlobalVariable.HTTP_SERVER + masterObj.FileUrl3;

                List<DATA> List = new List<DATA>();
                foreach (var v in DetailList)
                {
                    DATA InputData = new DATA(v.PointNo, v.Invalue, v.Judgement);
                    List.Add(InputData);
                }
                bindingSource1.DataSource = List;

            }
            catch (Exception ex)
            {
                MessageBoxHandler.Show(ex.ToString());
            }
        }

        public class DATA
        {
            public string PointNo { get; set; }
            public string InValue { get; set; }
            public string Judgement { get; set; }

            public DATA(string pointno, string invalue, string judgement)
            {
                this.PointNo = pointno;                
                this.InValue = invalue;
                this.Judgement = judgement;
            }

        }

    }
}
