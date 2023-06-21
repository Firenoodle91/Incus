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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.Model.Domain.TEMP;


namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>    
    /// Part Identification Table 
    /// </summary>
    public partial class RETCLABEL_PIT : DevExpress.XtraReports.UI.XtraReport
    {

        IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

        #region 전역변수
        /// <summary>       
        /// </summary>
        public enum eLabelType
        {
            
            Mold = 0,

            
            Storage
        };

        private eLabelType LABEL_SHAPE = eLabelType.Mold;

        
        public eLabelType LABEL
        {
            get
            {
                return LABEL_SHAPE;
            }

            set
            {
                LABEL_SHAPE = value;
            }
        }
        #endregion 

        public RETCLABEL_PIT()
        {
            InitializeComponent();
        }

        public RETCLABEL_PIT(PRT_OUTLABLE obj) : this()
        {
            Tc_itemcode.Text = obj.ItemCode; // 품번
            Tc_itemnm.Text = obj.ItemName; // 품명
            Tc_indate.Text = "   " + obj.PrtDate; // 납품일자
            bar_inlot.Text = obj.LotNo; // 바코드

        }
       
        /// </summary>
        /// <param name="obj">TN_WMS2000 객체 </param>
        public RETCLABEL_PIT(TN_WMS2000 obj) : this()
        {
            Tc_itemcode.Text = obj.WhCode;

            if (obj.TN_WMS1000 != null)
            {
                if (obj.TN_WMS1000.WhName != null)
                {
                    Tc_itemnm.Text = obj.TN_WMS1000.WhName;
                }
                else
                {
                    Tc_itemnm.Text = "";
                }
            }
            else
            {
                Tc_itemnm.Text = "";
            }

            Tc_indate.Text = obj.PositionA;            
            byte[] bArr_Value = System.Text.Encoding.UTF8.GetBytes(obj.PositionCode);
            bar_inlot.BinaryData = bArr_Value;
        }
        
        
        public RETCLABEL_PIT(TN_MOLD1100 obj) : this()
        {
            if (LABEL_SHAPE.Equals(eLabelType.Mold))
            {
                xrLabel1.Text = "금 형";
                xrLabel2.Text = "금형명";
                xrLabel8.Text = "금형코드";
            }



            Tc_itemcode.Text = obj.MoldCode;
            Tc_itemnm.Text = obj.MoldName;
            string sLocation = string.Empty;

            if (obj.StPostion1 != null)
            {
                var StPostion1 = ModelService.GetList(p => p.CodeMain == MasterCodeSTR.MoldPosition && p.CodeTop == obj.StPostion1).FirstOrDefault();


                sLocation = StPostion1.CodeName;
            }

            if (sLocation.Equals(string.Empty))
            {
                if (obj.StPostion2 != null)
                {

                    var StPostion2 = ModelService.GetList(p => p.CodeMain == MasterCodeSTR.MoldPosition && p.CodeTop == obj.StPostion2).FirstOrDefault();

                    sLocation = StPostion2.CodeName;
                }
            }
            else
            {
                if (obj.StPostion2 != null)
                {
                    var StPostion2 = ModelService.GetList(p => p.CodeMain == MasterCodeSTR.MoldPosition && p.CodeTop == obj.StPostion2).FirstOrDefault();

                    sLocation += "-" + StPostion2.CodeName;
                }
            }

            if (sLocation.Equals(string.Empty))
            {
                if (obj.StPostion3 != null)
                {
                    var StPostion3 = ModelService.GetList(p => p.CodeMain == MasterCodeSTR.MoldPosition && p.CodeTop == obj.StPostion3).FirstOrDefault();

                    sLocation = StPostion3.CodeName;
                }
            }
            else
            {
                if (obj.StPostion3 != null)
                {
                    var StPostion3 = ModelService.GetList(p => p.CodeMain == MasterCodeSTR.MoldPosition && p.CodeTop == obj.StPostion3).FirstOrDefault();

                    sLocation += "-" + StPostion3.CodeName;
                }
            }

            Tc_indate.Text = sLocation;            
            byte[] bArr_Qr = System.Text.Encoding.UTF8.GetBytes(obj.MoldMCode);
            bar_inlot.BinaryData = bArr_Qr;
        }
    }
}
