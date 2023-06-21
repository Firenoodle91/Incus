using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_UPH1000T")]
    public class TN_UPH1000 : BaseDomain.MES_BaseDomain
    {
        public TN_UPH1000()
        {
    
        }
        [ForeignKey("TN_MEA1000"), Key, Column("MACHINE_MCODE",Order =0)] public string MachineMCode { get; set; }
        [ForeignKey("TN_STD1100"),Key,Column("ITEM_CODE",Order =1), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }

        /// <summary>
        /// 20220105 오세완 차장 
        /// 동일한 제품에 금형이 다른 것이 있다고 하여 추가가 되면서 key조건으로 변경
        /// </summary>
        [ForeignKey("TN_MOLD1100"), Key, Column("MOLD_MCODE", Order = 2)]
        public string MoldMCode { get; set; }

        [Column("UPH")] public Nullable<int> Uph { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_MOLD1100 TN_MOLD1100 { get; set; }
    }
}