using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    namespace HKInc.Ui.Model.Domain
    {
        [Table("VI_QCT1500_LIST")]
        public class VI_QCT1500_LIST
        {
            [Key,Column("P_TYPE",Order =0)] public string PType { get; set; }
            [Column("WORK_DATE")] public Nullable<DateTime> WorkDate { get; set; }
            [Key,Column("RESULT_DATE",Order =3)] public Nullable<DateTime> ResultDate { get; set; }
            [Key,Column("WORK_NO",Order =2)] public string WorkNo { get; set; }
            [Key,Column("SEQ",Order =1)] public Nullable<int> Seq { get; set; }
            [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
            [Column("ITEM_CODE")] public string ItemCode { get; set; }
            [Column("LOT_NO")] public string LotNo { get; set; }
            [Column("FAIL_QTY")] public Nullable<int> FailQty { get; set; }
            [Column("FALE_TYPE")] public string FaleType { get; set; }
            [Column("OUT_LOT_NO")] public string OutLotno { get; set; }
            [Column("CLAIM_ID")] public string ClaimId { get; set; }
            [Column("CLAIM_MEMO")] public string ClaimMemo { get; set; }
            [Column("CLAIM_FILE")] public string ClaimFile { get; set; }
            [Column("CLAIM_FILEDATA")] public byte[] ClaimFiledata { get; set; }
            [Column("P_NO")] public string PNo { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
    }


