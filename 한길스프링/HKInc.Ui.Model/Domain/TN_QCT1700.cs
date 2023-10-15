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
        [Column("RESULT_DATE"), Required(ErrorMessage = "접수일은 필수입니다")] public DateTime ResultDate { get; set; }
        [Key, Column("CLAIM_NO", Order = 1)] public string ClaimNo { get; set; }
        [Column("SEQ")] public int Seq { get; set; }
        [Column("ITEM_CODE"), Required(ErrorMessage = "품목코드는 필수입니다")] public string ItemCode { get; set; }
        [Column("CUST_CODE"), Required(ErrorMessage = "고객사는 필수입니다")] public string CustCode { get; set; }
        [Column("P_LOT_NO"), Required(ErrorMessage = "생산 LOT NO는 필수입니다")] public string PLotno { get; set; }
        [Column("OUT_LOT_NO"), Required(ErrorMessage = "출고 LOT NO는 필수입니다")] public string OutLotno { get; set; }
        [Column("CALIM_QTY"), Required(ErrorMessage = "수량은 필수입니다")] public string CalimQty { get; set; }
        [Column("CALIM_TYPE"), Required(ErrorMessage = "유형은 필수입니다")] public string CalimType { get; set; }
        [Column("CLAIM_ID")] public string ClaimId { get; set; }
        [Column("CLAIM_MEMO")] public string ClaimMemo { get; set; }
        [Column("CLAIM_FILE")] public string ClaimFile { get; set; }
        [Column("CLAIM_FILEDATA")] public byte[] ClaimFiledata { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [NotMapped]
        public byte[] image
        {
            get
            {
                return BaseDomain.GsValue.UriToByte(ClaimFile);

            }
        }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }

        [ForeignKey("CustCode")]
        public virtual TN_STD1400 TN_STD1400 { get; set; }
        
    }
}