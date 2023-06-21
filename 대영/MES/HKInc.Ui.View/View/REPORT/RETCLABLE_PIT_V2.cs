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
    /// 20210825 오세완 차장
    /// 10 * 6 크기 금형 바코드 
    /// </summary>
    public partial class RETCLABEL_PIT_V2 : DevExpress.XtraReports.UI.XtraReport
    {
        #region 전역변수

        IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");
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

        public RETCLABEL_PIT_V2()
        {
            InitializeComponent();
        }

        public RETCLABEL_PIT_V2(TN_MOLD1100 obj) : this()
        {
            if (LABEL_SHAPE.Equals(eLabelType.Mold))
            {
                xrLabel1.Text = "금 형";
                xrLabel2.Text = "금형명";
                xrLabel8.Text = "금형코드";

                // 20211026 오세완 차장 신부장님 요청으로 코드명 출력 추가
                xl_Moldmcode.Visible = true;
                xl_Moldmcode.Text = "";
            }
            else
                xl_Moldmcode.Visible = false;

            Tc_itemcode.Text = obj.MoldCode;
            Tc_itemnm.Text = obj.MoldName;
            string sLocation = string.Empty;

            #region 기존 위치 출력 로직
            //if (obj.StPostion1 != null)
            //{
            //    var StPostion1 = ModelService.GetList(p => p.CodeMain == MasterCodeSTR.MoldPosition && p.CodeTop == obj.StPostion1).FirstOrDefault();
            //    if(StPostion1 != null)
            //        sLocation = StPostion1.CodeName;
            //}

            //if (sLocation.Equals(string.Empty))
            //{
            //    if (obj.StPostion2 != null)
            //    {
            //        var StPostion2 = ModelService.GetList(p => p.CodeMain == MasterCodeSTR.MoldPosition && p.CodeTop == obj.StPostion2).FirstOrDefault();
            //        if(StPostion2 != null)
            //            sLocation = StPostion2.CodeName;
            //    }
            //}
            //else
            //{
            //    if (obj.StPostion2 != null)
            //    {
            //        var StPostion2 = ModelService.GetList(p => p.CodeMain == MasterCodeSTR.MoldPosition && p.CodeTop == obj.StPostion2).FirstOrDefault();
            //        if(StPostion2 != null)
            //            sLocation += "-" + StPostion2.CodeName;
            //    }
            //}

            //if (sLocation.Equals(string.Empty))
            //{
            //    if (obj.StPostion3 != null)
            //    {
            //        var StPostion3 = ModelService.GetList(p => p.CodeMain == MasterCodeSTR.MoldPosition && p.CodeTop == obj.StPostion3).FirstOrDefault();
            //        if(StPostion3!= null)
            //            sLocation = StPostion3.CodeName;
            //    }
            //}
            //else
            //{
            //    if (obj.StPostion3 != null)
            //    {
            //        var StPostion3 = ModelService.GetList(p => p.CodeMain == MasterCodeSTR.MoldPosition && p.CodeTop == obj.StPostion3).FirstOrDefault();
            //        if(StPostion3 != null)
            //            sLocation += "-" + StPostion3.CodeName;
            //    }
            //}
            #endregion

            // 20211026 오세완 차장 금형창고위치로 변경
            sLocation = obj.MoldWhPosition.GetNullToEmpty();

            Tc_indate.Text = sLocation;            
            byte[] bArr_Qr = System.Text.Encoding.UTF8.GetBytes(obj.MoldMCode);
            bar_inlot.BinaryData = bArr_Qr;
            xl_Moldmcode.Text = obj.MoldMCode.GetNullToEmpty(); // 20211026 오세완 차장 신부장님 요청으로 코드명 출력 추가
        }
    }
}
