using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1500T")]
    public class TN_PUR1500 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1500()
        {
            PUR1501List = new List<TN_PUR1501>();
        }

        [Key,Column("OUT_NO",Order =0)] public string OutNo { get; set; }
        [Column("OUT_DATE")] public DateTime OutDate { get; set; }
        [Column("PRODUCT_ITEM_CODE")] public string ItemCode { get; set; }              // 2022-03-17 김진우 STD1100에서 정보 불러오기 위한 수정
        [Column("OUT_ID"), Required(ErrorMessage = "출고자")] public string OutId { get; set; }     // 2022-03-18 김진우 오류시 알수가 없어서 추가
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary>
        /// 2022-03-17 김진우 품목,품번 데이터추가를 위한 추가
        /// </summary>
        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }

        public virtual ICollection<TN_PUR1501> PUR1501List { get; set; }
    }
}