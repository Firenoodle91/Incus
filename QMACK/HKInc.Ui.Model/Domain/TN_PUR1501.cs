using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1501T")]
    public class TN_PUR1501 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1501()
        {
            _Check = "N";
        }

        [Key,Column("OUT_SEQ",Order =1)] public int OutSeq { get; set; }
        [ForeignKey("TN_PUR1500"),Key,Column("OUT_NO",Order =0)] public string OutNo { get; set; }
        [Column("ITEMCODE")] public string ItemCode { get; set; }
        [Column("OUT_QTY")] public Nullable<decimal> OutQty { get; set; }
        [Column("MAKE_DATE")] public Nullable<DateTime> MakeDate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>
        /// 20220425 오세완 차장
        /// 자동출고가 이뤄지면 출고가 이뤄진 공정코드 저정 
        /// </summary>
        [Column("TEMP")] public string Temp { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 출고lotno 완전체를 여기에 저장, out_no + out_seq의 형태
        /// </summary>
        [Column("TEMP1")] public string Temp1 { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 입고번호
        /// </summary>
        [Column("TEMP2")] public string Temp2 { get; set; }
        [Column("WH_CODE")] public string WhCode { get; set; }
        [Column("WH_POSITION")] public string WhPosition { get; set; }

        public virtual TN_PUR1500 TN_PUR1500 { get; set; }

        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}