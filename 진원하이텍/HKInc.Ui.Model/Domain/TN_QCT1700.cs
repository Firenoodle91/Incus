using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Drawing;
using System.IO;

namespace HKInc.Ui.Model.Domain
{
    [Table("TN_QCT1700T")]
    public class TN_QCT1700 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1700()
        {
            CreateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Column("RESULT_DATE")] public DateTime ResultDate { get; set; }
        [Key,Column("CLAIM_NO",Order =1)] public string ClaimNo { get; set; }
        [Column("SEQ")] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }        
        [Column("CUST_CODE")] public string  CustCode { get; set; }
        [Column("P_LOT_NO")] public string PLotno { get; set; }
        [Column("OUT_LOT_NO")] public string OutLotno { get; set; }
        [Column("CALIM_QTY")] public string CalimQty { get; set; }
        [Column("CALIM_TYPE")] public string CalimType { get; set; }
        [Column("CLAIM_ID")] public string ClaimId { get; set; }
        [Column("CLAIM_MEMO")] public string ClaimMemo { get; set; }
        [Column("CLAIM_FILE")] public string ClaimFile { get; set; }
        [Column("CLAIM_FILEDATA")] public byte[] ClaimFiledata { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        [NotMapped]
        public byte[] image
        {
            get
            {
                return BaseDomain.GsValue.UriToByte(ClaimFile);
              
            }
        }
        
    }
}