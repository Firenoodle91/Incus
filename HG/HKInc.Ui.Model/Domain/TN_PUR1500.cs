using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 자재출고마스터
    /// </summary>
    [Table("TN_PUR1500T")]
    public class TN_PUR1500 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1500()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
            PUR1501List = new List<TN_PUR1501>();
        }
        [Key,Column("OUT_NO",Order =0)] public string OutNo { get; set; }
        [Column("OUT_DATE")] public DateTime OutDate { get; set; }
        [Column("PRODUCT_ITEM_CODE"), Required(ErrorMessage = "생산품목코드는 필수입니다.")] public string ProductItemcode { get; set; }
        [Column("OUT_ID")] public string OutId { get; set; }     
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        [ForeignKey("ProductItemcode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }

        public virtual ICollection<TN_PUR1501> PUR1501List { get; set; }
    }
}