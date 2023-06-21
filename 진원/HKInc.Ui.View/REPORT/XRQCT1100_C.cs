using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using System.Linq;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Utils.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 디와이솔루텍 포함 기본업체
    /// </summary>
    public partial class XRQCT1100_C : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_QCT1200> ModelService = (IService<TN_QCT1200>)ProductionFactory.GetDomainService("TN_QCT1200");

        List<TN_STD1000> qctQ006;   //검사항목
        List<TN_STD1000> qctQ002;   //검사방법

        public XRQCT1100_C()
        {
            InitializeComponent();
        }

        public XRQCT1100_C(TN_QCT1200 masterObj, string customerCode) : this()
        {
            qctQ002 = ModelService.GetChildList<TN_STD1000>(x => x.Codemain == MasterCodeSTR.QCTYPE && x.Useyn == "Y");
            qctQ006 = ModelService.GetChildList<TN_STD1000>(x => x.Codemain == MasterCodeSTR.QCPOINT && x.Useyn == "Y");

            var tN_STD1400 = ModelService.GetChildList<TN_STD1400>(x => x.CustomerCode == customerCode).FirstOrDefault();
            if (tN_STD1400 != null)
            {
                cell_Customer.Text = tN_STD1400.CustomerName;
            }
            else
            {
                cell_Customer.Text = "";
            }
            
            MainData(masterObj);
            SubData(masterObj);
        }

        private void MainData(TN_QCT1200 obj)
        {
            cell_ItemNM1.Text = obj.TN_STD1100.ItemNm.GetNullToEmpty();
            cell_ItemNM2.Text = obj.TN_STD1100.ItemNm1.GetNullToEmpty();
            //cell_Company.Text = GlobalVariable.COMPANY_NAME.GetNullToEmpty();
            cell_LotNO.Text = obj.LotNo;
            //cell_InspDate.Text = obj.CheckDate?.ToString("yyyy-MM-dd").GetNullToEmpty();
        }

        private void SubData(TN_QCT1200 obj)
        {
            XRTableCell[][] cellArr =
            {
                new XRTableCell[]{ cell_CheckNM0, cell_Stand0, cell_Prov0, cell_InspQty0, cell_Read0_1, cell_Read0_2, cell_Read0_3, cell_Read0_4, cell_Read0_5 }
                ,new XRTableCell[]{ cell_CheckNM1, cell_Stand1, cell_Prov1, cell_InspQty1, cell_Read1_1, cell_Read1_2, cell_Read1_3, cell_Read1_4, cell_Read1_5 }
                , new XRTableCell[]{ cell_CheckNM2, cell_Stand2, cell_Prov2, cell_InspQty2, cell_Read2_1, cell_Read2_2, cell_Read2_3, cell_Read2_4, cell_Read2_5 }
                , new XRTableCell[]{ cell_CheckNM3, cell_Stand3, cell_Prov3, cell_InspQty3, xrTableCell135, xrTableCell136, xrTableCell137, xrTableCell138, xrTableCell139 }
                , new XRTableCell[]{ cell_CheckNM4, cell_Stand4, cell_Prov4, cell_InspQty4, xrTableCell150, xrTableCell151, xrTableCell152, xrTableCell153, xrTableCell154 }
                , new XRTableCell[]{ cell_CheckNM5, cell_Stand5, cell_Prov5, cell_InspQty5, xrTableCell165, xrTableCell166, xrTableCell167, xrTableCell168, xrTableCell169 }
                , new XRTableCell[]{ cell_CheckNM6, cell_Stand6, cell_Prov6, cell_InspQty6, xrTableCell180, xrTableCell181, xrTableCell182, xrTableCell183, xrTableCell184 }
                , new XRTableCell[]{ cell_CheckNM7, cell_Stand7, cell_Prov7, cell_InspQty7, xrTableCell195, xrTableCell196, xrTableCell197, xrTableCell198, xrTableCell199 }
                , new XRTableCell[]{ cell_CheckNM8, cell_Stand8, cell_Prov8, cell_InspQty8, xrTableCell210, xrTableCell211, xrTableCell212, xrTableCell213, xrTableCell214 }
                , new XRTableCell[]{ cell_CheckNM9, cell_Stand9, cell_Prov9, cell_InspQty9, xrTableCell225, xrTableCell226, xrTableCell227, xrTableCell228, xrTableCell229 }
                , new XRTableCell[]{ cell_CheckNM10, cell_Stand10, cell_Prov10, cell_InspQty10, xrTableCell240, xrTableCell241, xrTableCell242, xrTableCell243, xrTableCell244 }
                , new XRTableCell[]{ cell_CheckNM11, cell_Stand11, cell_Prov11, cell_InspQty11, xrTableCell255, xrTableCell256, xrTableCell257, xrTableCell258, xrTableCell259 }
                , new XRTableCell[]{ cell_CheckNM12, cell_Stand12, cell_Prov12, cell_InspQty12, xrTableCell270, xrTableCell271, xrTableCell272, xrTableCell273, xrTableCell274 }
                , new XRTableCell[]{ cell_CheckNM13, cell_Stand13, cell_Prov13, cell_InspQty13, xrTableCell285, xrTableCell286, xrTableCell287, xrTableCell288, xrTableCell289 }
                , new XRTableCell[]{ cell_CheckNM14, cell_Stand14, cell_Prov14, cell_InspQty14, xrTableCell300, xrTableCell301, xrTableCell302, xrTableCell303, xrTableCell304 }
                , new XRTableCell[]{ cell_CheckNM15, cell_Stand15, cell_Prov15, cell_InspQty15, xrTableCell315, xrTableCell316, xrTableCell317, xrTableCell318, xrTableCell319 }
                , new XRTableCell[]{ cell_CheckNM16, cell_Stand16, cell_Prov16, cell_InspQty16, xrTableCell330, xrTableCell331, xrTableCell332, xrTableCell333, xrTableCell334 }
                , new XRTableCell[]{ cell_CheckNM17, cell_Stand17, cell_Prov17, cell_InspQty17, xrTableCell56, xrTableCell61, xrTableCell63, xrTableCell69, xrTableCell86 }
                , new XRTableCell[]{ cell_CheckNM18, cell_Stand18, cell_Prov18, cell_InspQty18, xrTableCell103, xrTableCell104, xrTableCell105, xrTableCell106, xrTableCell107 }
                , new XRTableCell[]{ cell_CheckNM19, cell_Stand19, cell_Prov19, cell_InspQty19, xrTableCell124, xrTableCell131, xrTableCell132, xrTableCell133, xrTableCell134 }
            };

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                SqlParameter param1 = new SqlParameter("@ItemCode", obj.ItemCode);
                SqlParameter param2 = new SqlParameter("@InspNo", obj.No);

                List<XRQCT1100_DATA_B> result = context.Database.SqlQuery<XRQCT1100_DATA_B>("USP_GET_XRQCT1100_LISTDATA_B @ItemCode, @InspNo", param1, param2).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    if (cellArr.Length >= i)
                    {
                        cellArr[i][0].Text = result[i].CheckNameNM.GetNullToEmpty();
                        cellArr[i][1].Text = result[i].CheckStand.GetNullToEmpty();
                        cellArr[i][2].Text = result[i].CheckProvNM.GetNullToEmpty();
                        cellArr[i][3].Text = "";
                        cellArr[i][4].Text = result[i].Reading1.GetNullToEmpty();
                        cellArr[i][5].Text = result[i].Reading2.GetNullToEmpty();
                        cellArr[i][6].Text = result[i].Reading3.GetNullToEmpty();
                        cellArr[i][7].Text = result[i].Reading4.GetNullToEmpty();
                        cellArr[i][8].Text = result[i].Reading5.GetNullToEmpty();
                    }
                }            
            }
        }

    }
}
