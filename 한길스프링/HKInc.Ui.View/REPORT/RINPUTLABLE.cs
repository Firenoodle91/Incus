using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using System.Linq;
using HKInc.Utils.Class;
using System.Collections.Generic;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 현품표라벨 화면
    /// </summary>
    public partial class RINPUTLABLE : DevExpress.XtraReports.UI.XtraReport
    {
        public RINPUTLABLE()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 20220405 오세완 차장
        /// 이차장 요청으로 기타입고 라벨 출력 기능 추가 
        /// </summary>
        /// <param name="obj"></param>
        public RINPUTLABLE(TN_ORD1701 obj) : this()
        {
            if(obj != null)
            {
                Tc_itemcode.Text = obj.ItemCode.GetNullToEmpty();
                Tc_qty.Text = obj.InQty.GetIntNullToZero().ToString("n0");
                TcWms1000.Text = string.Empty ;
                Tc_Whposion.Text = string.Empty ;
                bar_inlot.Text = obj.LotNo.GetNullToEmpty();
                try
                {
                    Tc_indate.Text = obj.InDate.ToString().Substring(0, 10);
                }
                catch
                {
                    Tc_indate.Text = "";
                }

                try
                {
                    IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
                    TN_STD1100 std1100 = ModelService.GetList(p => p.ItemCode == obj.ItemCode &&
                                                                   p.UseYn == "Y").FirstOrDefault();
                    if(std1100 != null)
                        Tc_itemnm.Text = std1100.ItemNm;
                    
                }
                catch
                {
                    Tc_itemnm.Text = "";
                }
            }
            
        }

        /// <summary>
        /// 자재입고
        /// </summary>
        /// <param name="obj"></param>
        public RINPUTLABLE(TN_PUR1301 obj):this()
        {
            Tc_itemcode.Text = obj.ItemCode;
            Tc_itemnm.Text = obj.TN_STD1100.ItemNm;
            //Tc_itemnm.Text = string.Format("{0}/{1}",obj.TN_STD1100.ItemNm1, obj.TN_STD1100.ItemNm);
            Tc_qty.Text = obj.InputQty.GetIntNullToZero().ToString("n0");
            try
            {
                Tc_indate.Text = obj.CreateTime.ToString().Substring(0, 10);
            }
            catch { Tc_indate.Text = ""; }
            try
            {
                IService<TN_MOLD001> ModelService = (IService<TN_MOLD001>)ProductionFactory.GetDomainService("TN_MOLD001");
                var WMS1000 = ModelService.GetChildList<TN_WMS1000>(p => p.WhCode == obj.WhCode).FirstOrDefault();
                var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PosionCode == obj.WhPosition).FirstOrDefault();
                TcWms1000.Text = WMS1000 == null ? string.Empty : WMS1000.WhName;
                Tc_Whposion.Text = WMS2000 == null ? string.Empty : WMS2000.PosionName;
                //Tc_Whposion.Text = obj.WhPosition.ToString();
            }
            catch { Tc_Whposion.Text = ""; }
            bar_inlot.Text = obj.Temp2;            
        }

        /// <summary>
        /// 자재재입고
        /// </summary>
        /// <param name="obj"></param>
        public RINPUTLABLE(ReturnInLabelTemp obj, VI_PURINOUT VI_PURINOUT) : this()
        {
            Tc_itemcode.Text = VI_PURINOUT.ItemCode;
            Tc_itemnm.Text = VI_PURINOUT.TN_STD1100.ItemNm;
            Tc_qty.Text = obj.Qty.GetIntNullToZero().ToString("n0");
            try
            {
                Tc_indate.Text = obj.Date.ToString().Substring(0, 10);
            }
            catch { Tc_indate.Text = ""; }
            try
            {
                IService<TN_MOLD001> ModelService = (IService<TN_MOLD001>)ProductionFactory.GetDomainService("TN_MOLD001");
                var WMS1000 = ModelService.GetChildList<TN_WMS1000>(p => p.WhCode == obj.WhCode).FirstOrDefault();
                var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PosionCode == obj.WhPosition).FirstOrDefault();
                TcWms1000.Text = WMS1000 == null ? string.Empty : WMS1000.WhName;
                Tc_Whposion.Text = WMS2000 == null ? string.Empty : WMS2000.PosionName;
                //Tc_Whposion.Text = obj.WhPosition.ToString();
            }
            catch { Tc_Whposion.Text = ""; }
            bar_inlot.Text = VI_PURINOUT.InLotNo;
        }

        /// <summary>
        /// 자재출고
        /// </summary>
        /// <param name="obj"></param>
        public RINPUTLABLE(TN_PUR1501 obj) : this()
        {
            xrLabel1.Multiline = true;
            xrLabel2.Multiline = true;
            xrLabel1.Text = string.Format("{0}{1}{2}", "품번", Environment.NewLine, "(생산/자재)");
            xrLabel2.Text = string.Format("{0}{1}{2}", "품명", Environment.NewLine, "(생산/자재)");
            xrLabel2.Font = new Font(xrLabel2.Font.FontFamily, 8f);
            xrLabel3.Text = "출고량";
            xrLabel4.Text = "출고일";
            xrLabel5.Text = "출고위치";
            Tc_itemcode.Text = string.Format("{0} / {1}", obj.TN_PUR1500.TN_STD1100.ItemNm1, obj.TN_STD1100.ItemNm1);
            Tc_itemnm.Text = string.Format("{0} / {1}", obj.TN_PUR1500.TN_STD1100.ItemNm, obj.TN_STD1100.ItemNm);
            Tc_qty.Text = obj.OutQty.GetIntNullToZero().ToString("n0");
            try
            {
                Tc_indate.Text = obj.CreateTime.ToString().Substring(0, 10);
            }
            catch { Tc_indate.Text = ""; }
            try
            {
                Tc_Whposion.Text = obj.WhPosition.ToString();
            }
            catch { Tc_Whposion.Text = ""; }
            bar_inlot.Text = obj.Temp1;

            WhRow.Visible = false;
        }

        /// <summary>
        /// 반제품입고
        /// </summary>
        /// <param name="obj"></param>
        public RINPUTLABLE(TN_BAN1001 obj) : this()
        {
            Tc_itemcode.Text = obj.ItemCode;
            Tc_itemnm.Text = obj.TN_STD1100.ItemNm;
            //Tc_itemnm.Text = string.Format("{0}/{1}", obj.TN_STD1100.ItemNm1, obj.TN_STD1100.ItemNm);
            Tc_qty.Text = obj.InputQty.GetIntNullToZero().ToString("n0");
            try
            {
                Tc_indate.Text = obj.CreateTime.ToString().Substring(0, 10);
            }
            catch { Tc_indate.Text = ""; }
            try
            {
                Tc_Whposion.Text = obj.WhPosition.ToString();
            }
            catch { Tc_Whposion.Text = ""; }
            bar_inlot.Text = obj.Temp2;

        }

        /// <summary>
        /// 반제품출고
        /// </summary>
        /// <param name="obj"></param>
        public RINPUTLABLE(TN_BAN1201 obj) : this()
        {
            xrLabel3.Text = "출고량";
            xrLabel4.Text = "출고일";
            xrLabel5.Text = "출고위치";
            Tc_itemcode.Text = obj.ItemCode;
            Tc_itemnm.Text = obj.TN_STD1100.ItemNm;
            //Tc_itemnm.Text = string.Format("{0}/{1}", obj.TN_STD1100.ItemNm1, obj.TN_STD1100.ItemNm);
            Tc_qty.Text = obj.OutQty.GetIntNullToZero().ToString("n0");
            try
            {
                Tc_indate.Text = obj.CreateTime.ToString().Substring(0, 10);
            }
            catch { Tc_indate.Text = ""; }
            try
            {
                Tc_Whposion.Text = obj.WhPosition.ToString();
            }
            catch { Tc_Whposion.Text = ""; }
            bar_inlot.Text = obj.Temp2;

        }
    }
}
