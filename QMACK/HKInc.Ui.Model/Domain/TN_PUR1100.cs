using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1100T")]
    public class TN_PUR1100 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1100()
        {
            PUR1200List = new List<TN_PUR1200>();
        }

        [Key,Column("REQ_NO",Order =0)] public string ReqNo { get; set; }
        [Column("REQ_DATE")] public DateTime ReqDate { get; set; }
        [Column("REQ_ID")] public string ReqId { get; set; }
        [Column("DUE_DATE")] public DateTime DueDate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [ForeignKey("TN_STD1400"), Column("TEMP",Order =1),Required(ErrorMessage = "거래처")] public string CustomerCode { get; set; }     // 2022-03-28 김진우 자재발주관리에서 거래서 필수값으로 변경
        [Column("TEMP1")] public string Temp1 { get; set; }

        /// <summary>
        /// 20220217 오세완 차장 
        /// 임시2인데 발주확정으로 사용
        /// </summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1400 TN_STD1400 { get; set; }
        public virtual ICollection<TN_PUR1200> PUR1200List { get; set; }
    }
}