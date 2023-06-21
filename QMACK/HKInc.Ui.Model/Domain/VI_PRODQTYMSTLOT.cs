using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PROD_QTY_MST_LOT")]
    public class VI_PRODQTYMSTLOT
    {
      
        [Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Key,Column("LOT_NO",Order =1)] public string LotNo { get; set; }

        /// <summary>
        /// 20220219 오세완 차장 품목 출력 추가
        /// </summary>
        [Column("ITEM_NM")]
        public string ItemNm { get; set; }

        /// <summary>
        /// 20220219 오세완 차장 품번 출력 추가
        /// </summary>
        [Column("ITEM_NM1")]
        public string ItemNm1 { get; set; }

        [Column("InQty")] public Nullable<int> Inqty { get; set; }
        [Column("OutQty")] public Nullable<int> Outqty { get; set; }
        [Column("StockQty")] public Nullable<int> Stockqty { get; set; }
    }
}