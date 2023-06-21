using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Helper;
using System.Linq;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using System.IO;
using HKInc.Service.Service;
using HKInc.Ui.View.View;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 리워크 지시서
    /// </summary>
    public partial class XRQCT1302 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

        public XRQCT1302()
        {
            InitializeComponent();
        }

        public XRQCT1302(VI_QCT1300_LIST viObj, TN_QCT1300 obj, QCT.XRQCT1301_DATA dataObj) : this()
        {
            TN_STD1100 tN_STD1100 = ModelService.GetChildList<TN_STD1100>(x => x.UseFlag == "Y" && x.ItemCode == obj.ItemCode).FirstOrDefault();
            TN_STD1400 tN_STD1400 = ModelService.GetChildList<TN_STD1400>(x => x.UseFlag == "Y" && x.CustomerCode == obj.CustomerCode).FirstOrDefault();

            xrBarCode1.Text = viObj.ItemMoveNo;
            xrBarCode2.Text = viObj.ProductLotNo;

            //xrCell_OccurMoment.Text = dataObj.OccurMoment.GetNullToEmpty();
            //xrCell_OccurDivision.Text = dataObj.OccurDivision.GetNullToEmpty();
            xrCell_PNo.Text = obj.PNo.GetNullToEmpty();
            xrCell_OccurDate.Text = dataObj.OccurDate.GetNullToEmpty();
            xrCell_CheckDate.Text = obj.CheckDate?.ToShortDateString().GetNullToEmpty();
            xrCell_CustomName.Text = tN_STD1400.CustomerName.GetNullToEmpty();
            xrCell_LotNo.Text = obj.ProductLotNo.GetNullToEmpty();
            xrCell_CarType.Text = tN_STD1100.CarType.GetNullToEmpty();
            xrCell_ItemName.Text = tN_STD1100.ItemName.GetNullToEmpty();
            xrCell_ItemName1.Text = tN_STD1100.ItemName1.GetNullToEmpty();
            xrCell_Grade.Text = dataObj.OccurGrade.GetNullToEmpty();
            xrCell_Location.Text = obj.OccurLocation.GetNullToEmpty();
            //xrCell_OccurQty.Text = obj.OccurQty.GetNullToEmpty();
            xrCell_OccurQty.Text = dataObj.OccurQtyStr.GetNullToEmpty();
            xrCell_BadQty.Text = obj.BadQty.GetNullToEmpty();
            xrCell_BadType.Text = dataObj.BadType.GetNullToEmpty();
            xrCell_BadContent.Text = obj.BadContent.GetNullToEmpty();
            xrCell_BadSolution.Text = dataObj.SolutionContent.GetNullToEmpty();
            xrCell_ManHour.Text = obj.ManHour.GetNullToEmpty();
            xrCell_MeasureNeed.Text = dataObj.MeasureNeeds.GetNullToEmpty();
            xrCell_MeasureNeedDate.Text = obj.MeasureNeedsDate?.ToShortDateString().GetNullToEmpty();
            xrCell_ReplyDate.Text = obj.MeasureAnswerDate?.ToShortDateString().GetNullToEmpty();
            xrCell_SubtractionCost.Text = dataObj.SubtractionCost.GetNullToEmpty();
            xrCell_LossTime.Text = obj.LoseTime.GetNullToEmpty();
            xrCell_LossCost.Text = obj.LoseCost.GetNullToEmpty();

            if (obj.FileName2 != null && obj.FileUrl2 != null)
            {
                var fileData = Service.Handler.FileHandler.FtpImageToByte(GlobalVariable.HTTP_SERVER + obj.FileUrl2);
                if (fileData != null)
                {
                    using (MemoryStream stream = new MemoryStream(fileData))
                    {
                        xrPictureBox1.Image = Image.FromStream(stream);
                        //Image imgOrigin = Image.FromStream(stream);
                        //Size resize = new Size(xrPictureBox1.Width, xrPictureBox1.Height);
                        //Image imgResize = new Bitmap(imgOrigin, resize);
                        //xrPictureBox1.Image = imgResize;
                    }
                }
            }
        }
        

    }
}
