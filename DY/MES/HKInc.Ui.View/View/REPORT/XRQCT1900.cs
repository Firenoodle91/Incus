using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
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

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 20211007 오세완 차장 
    /// 출하검사 산출물
    /// </summary>
    public partial class XRQCT1900 : DevExpress.XtraReports.UI.XtraReport
    {
        #region 전역변수
        IService<TN_QCT1900> ModelService = (IService<TN_QCT1900>)ProductionFactory.GetDomainService("TN_QCT1900");
        XRLabel[][] lblList1;
        #endregion

        public XRQCT1900()
        {
            InitializeComponent();
        }

        public XRQCT1900(TN_QCT1100 obj)
        {
            InitializeComponent();
            printTitle(obj);
            printQc(obj);

            PrintDetail(obj);
            PrintImage(obj.ItemCode);
        }

        #region 품번으로 구분할 경우

        /// <summary>RH</summary>
        private void R_REport(TN_QCT1100 obj)
        {
            //입고수량//생산LOT//LOT종합판정 합격 /불합격
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                SqlParameter param1 = new SqlParameter("@ItemCode", obj.ItemCode);
                SqlParameter param2 = new SqlParameter("@InspNo", obj.InspNo);

                List<XRQCT1100_DATA> result = context.Database.SqlQuery<XRQCT1100_DATA>("USP_GET_XRQCT1100_LISTDATA @ItemCode, @InspNo", param1, param2).ToList();

                XRLabel[][] rCell = 
                {
                    new XRLabel[] { cellCRHxa0, cellCRHxa1, cellCRHxa2}
                    , new XRLabel[] { cellCRHxb0, cellCRHxb1, cellCRHxb2 }
                    , new XRLabel[] { cellCRHxc0, cellCRHxc1, cellCRHxc2 }
                    , new XRLabel[] { cellCRHxd0, cellCRHxd1, cellCRHxd2 }
                    , new XRLabel[] { cellCRHxe0, cellCRHxe1, cellCRHxe2 }
                    , new XRLabel[] { cellCRHxf0, cellCRHxf1, cellCRHxf2 }
                    , new XRLabel[] { cellCRHxg0, cellCRHxg1, cellCRHxg2 }
                };

                for (int i = 0; i < result.Count; i++)
                {
                    rCell[i][0].Text = result[i].Reading1;
                    rCell[i][1].Text = result[i].Reading2;
                    rCell[i][2].Text = result[i].Reading3;
                }

                //입고수량
                cellCRHqty0.Text = result.FirstOrDefault().InQty.GetNullToEmpty();
                cellCRHqty1.Text = result.FirstOrDefault().InQty.GetNullToEmpty();
                cellCRHqty2.Text = result.FirstOrDefault().InQty.GetNullToEmpty();

                //생산LOT
                cellCRHLotno0.Text = result.FirstOrDefault().ProductLotNo.GetNullToEmpty();
                cellCRHLotno1.Text = result.FirstOrDefault().ProductLotNo.GetNullToEmpty();
                cellCRHLotno2.Text = result.FirstOrDefault().ProductLotNo.GetNullToEmpty();

                //LOT종합판정
            }
        }

        /// <summary>LH</summary>
        private void L_REport(TN_QCT1100 obj)
        {
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                SqlParameter param1 = new SqlParameter("@ItemCode", obj.ItemCode);
                SqlParameter param2 = new SqlParameter("@InspNo", obj.InspNo);

                List<XRQCT1100_DATA> result = context.Database.SqlQuery<XRQCT1100_DATA>("USP_GET_XRQCT1100_LISTDATA @ItemCode, @InspNo", param1, param2).ToList();

                XRLabel[][] lCell =
                {
                    new XRLabel[] { cellCLHxa0, cellCLHxa1, cellCLHxa2 }
                    , new XRLabel[] { cellCLHxb0, cellCLHxb1, cellCLHxb2 }
                    , new XRLabel[] { cellCLHxc0, cellCLHxc1, cellCLHxc2 }
                    , new XRLabel[] { cellCLHxd0, cellCLHxd1, cellCLHxd2 }
                    , new XRLabel[] { cellCLHxe0, cellCLHxe1, cellCLHxe2 }
                    , new XRLabel[] { cellCLHxf0, cellCLHxf1, cellCLHxf2 }
                    , new XRLabel[] { cellCLHxg0, cellCLHxg1, cellCLHxg2 }
                };

                for (int i = 0; i < result.Count; i++)
                {
                    lCell[i][0].Text = result[i].Reading1;
                    lCell[i][1].Text = result[i].Reading2;
                    lCell[i][2].Text = result[i].Reading3;
                }

                //입고수량
                cellCLHqty0.Text = result.FirstOrDefault().InQty.GetNullToEmpty();
                cellCLHqty1.Text = result.FirstOrDefault().InQty.GetNullToEmpty();
                cellCLHqty2.Text = result.FirstOrDefault().InQty.GetNullToEmpty();

                //생산LOT
                cellCLHLotno0.Text = result.FirstOrDefault().ProductLotNo.GetNullToEmpty();
                cellCLHLotno1.Text = result.FirstOrDefault().ProductLotNo.GetNullToEmpty();
                cellCLHLotno2.Text = result.FirstOrDefault().ProductLotNo.GetNullToEmpty();

                //LOT종합판정
            }
        }

        private void LR_REport(TN_QCT1100 obj)
        {
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                SqlParameter param1 = new SqlParameter("@ItemCode", obj.ItemCode);
                SqlParameter param2 = new SqlParameter("@InspNo", obj.InspNo);

                List<XRQCT1100_DATA> result = context.Database.SqlQuery<XRQCT1100_DATA>("USP_GET_XRQCT1100_LISTDATA @ItemCode, @InspNo", param1, param2).ToList();
            }
        }

        #endregion

        /// <summary>
        /// 20211007 오세완 차장
        /// 3항 최종검사 CHECK 결과(DATA) 출력
        /// </summary>
        /// <param name="obj"></param>
        private void PrintDetail(TN_QCT1100 obj)
        {
            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                SqlParameter param1 = new SqlParameter("@ItemCode", obj.ItemCode);
                SqlParameter param2 = new SqlParameter("@InspNo", obj.InspNo);

                List<XRQCT1900_DATA> result = context.Database.SqlQuery<XRQCT1900_DATA>("USP_GET_XRQCT1900_LISTDATA @ItemCode, @InspNo", param1, param2).ToList();

                #region 공급자 RH 중 관리항목 CHECK
                XRLabel[][] rhCell =
                {
                    new XRLabel[] { cellCRHxa0, cellCRHxa1, cellCRHxa2}
                    , new XRLabel[] { cellCRHxb0, cellCRHxb1, cellCRHxb2 }
                    , new XRLabel[] { cellCRHxc0, cellCRHxc1, cellCRHxc2 }
                    , new XRLabel[] { cellCRHxd0, cellCRHxd1, cellCRHxd2 }
                    , new XRLabel[] { cellCRHxe0, cellCRHxe1, cellCRHxe2 }
                    , new XRLabel[] { cellCRHxf0, cellCRHxf1, cellCRHxf2 }
                    , new XRLabel[] { cellCRHxg0, cellCRHxg1, cellCRHxg2 }
                };

                for (int i = 0; i < result.Where(x => x.InspCheckPosition == "02" || x.InspCheckPosition == "03").ToList().Count; i++)
                {
                    rhCell[i][0].Text = result[i].Reading1;
                    rhCell[i][1].Text = result[i].Reading2;
                    rhCell[i][2].Text = result[i].Reading3;
                }

                //입고수량
                cellCRHqty0.Text = result.FirstOrDefault().OkQty.GetNullToEmpty();

                //생산LOT
                cellCLHLotno0.Text = result.FirstOrDefault().ProductLotNo.GetNullToEmpty();
                #endregion

                #region LH
                XRLabel[][] lCell =
                {
                    new XRLabel[] { cellCLHxa0, cellCLHxa1, cellCLHxa2 }
                    , new XRLabel[] { cellCLHxb0, cellCLHxb1, cellCLHxb2 }
                    , new XRLabel[] { cellCLHxc0, cellCLHxc1, cellCLHxc2 }
                    , new XRLabel[] { cellCLHxd0, cellCLHxd1, cellCLHxd2 }
                    , new XRLabel[] { cellCLHxe0, cellCLHxe1, cellCLHxe2 }
                    , new XRLabel[] { cellCLHxf0, cellCLHxf1, cellCLHxf2 }
                    , new XRLabel[] { cellCLHxg0, cellCLHxg1, cellCLHxg2 }
                };

                for (int i = 0; i < result.Where(x => x.InspCheckPosition == "01" || x.InspCheckPosition == "03").ToList().Count; i++)
                {
                    lCell[i][0].Text = result[i].Reading1;
                    lCell[i][1].Text = result[i].Reading2;
                    lCell[i][2].Text = result[i].Reading3;
                }

                //LOT 종합 판정
                if(result.FirstOrDefault().CheckResult.GetNullToEmpty() == "OK")
                    cellCLHOK0.Text = "OK";
                else if (result.FirstOrDefault().CheckResult.GetNullToEmpty() == "NG")
                    cellCLHNG0.Text = "NG";


                cellCLHqty0.Text = result.FirstOrDefault().OkQty.GetNullToEmpty();
                #endregion

            }
        }

        /// <summary>
        /// 20211007 오세완 차장
        /// 최상단 협력사, 차종, 품명, 3항 출하검사 check결과 중 일자 출력
        /// </summary>
        /// <param name="obj"></param>
        private void printTitle(TN_QCT1100 obj)
        {
            TN_STD1400 cust = ModelService.GetChildList<TN_STD1400>(x => x.CustomerCode == obj.CustomerCode && x.UseFlag == "Y").FirstOrDefault();
            if(cust != null)
            {
                cellCust.Text = cust.CustomerName.GetNullToEmpty();
                cellCar.Text = obj.TN_STD1100.CarType.GetNullToEmpty();
                cellItem.Text = obj.TN_STD1100.ItemName.GetNullToEmpty();
                cell_InspDate.Text = obj.CheckDate.Value.ToShortDateString();
            }
        }

        /// <summary>
        /// 20211002 오세완 차장 
        /// 2항 출하검사 check 항목 전체
        /// </summary>
        /// <param name="obj"></param>
        private void printQc(TN_QCT1100 obj)
        {
            string rev = obj.TN_QCT1101List.Last().RevNo.GetNullToEmpty();
            var qctype = ModelService.GetChildList<TN_QCT1001>(x => x.ItemCode == obj.ItemCode && x.RevNo == rev && x.CheckDivision == obj.CheckDivision).OrderBy(o => o.DisplayOrder).ToList();

            lblList1 = new XRLabel[][]
            {
                new XRLabel[] { cellType0, cellPtype0, cellX0, cellMemo0},
                new XRLabel[] { cellType1, cellPtype1, cellX1, cellMemo1},
                new XRLabel[] { cellType2, cellPtype2, cellX2, cellMemo2},
                new XRLabel[] { cellType3, cellPtype3, cellX3, cellMemo3},
                new XRLabel[] { cellType4, cellPtype4, cellX4, cellMemo4},
                new XRLabel[] { cellType5, cellPtype5, cellX5, cellMemo5},
                new XRLabel[] { cellType6, cellPtype6, cellX6, cellMemo6 }
            };

            for (int i = 0; i < lblList1.Count(); i++)
            {
                lblList1[i][0].Text = "";
                lblList1[i][1].Text = "";
                lblList1[i][2].Text = "";
                lblList1[i][3].Text = "";
                if (qctype.Count > i)
                {
                    lblList1[i][0].Text = qctype[i].CheckList.GetNullToEmpty();
                    lblList1[i][1].Text = QCtype(qctype[i].CheckWay.GetNullToEmpty());
                    lblList1[i][2].Text = qctype[i].MaxReading.GetNullToEmpty();
                    lblList1[i][3].Text = qctype[i].Memo.GetNullToEmpty();
                }
            }
        }

        private string QCtype(string st)
        {
            string qcval = DbRequestHandler.GetCellValue("SELECT [CODE_NAME] from [TN_STD1000T] where [CODE_MAIN]='Q003'  and CODE_VAL='"+st+"'", 0);
            return qcval;
        }

        /// <summary>
        /// 20211007 오세완 차장
        /// 부품전개도 이미지 출력
        /// </summary>
        /// <param name="itemCode"></param>
        private void PrintImage(string itemCode)
        {
            TN_STD1104 tN_STD1104 = ModelService.GetChildList<TN_STD1104>(x => x.ItemCode == itemCode && 
                                                                               x.CheckDivision == MasterCodeSTR.InspectionDivision_ShipmentV2).OrderByDescending(x => x.ApplyDate).FirstOrDefault();

            if (tN_STD1104 != null && tN_STD1104.FileUrl != null)
                xrPictureBox1.ImageUrl = GlobalVariable.HTTP_SERVER + tN_STD1104.FileUrl;
        }
    }

    public class XRQCT1900_DATA
    {
        public string InspNO { get; set; }
        public string ProductLotNo { get; set; }
        public string CheckList { get; set; }
        public string InspCheckPosition { get; set; }
        public string Reading1 { get; set; }
        public string Reading2 { get; set; }
        public string Reading3 { get; set; }
        public string Judge { get; set; }
        public decimal? OkQty { get; set; }
        public string CheckResult { get; set; }
    }
}
