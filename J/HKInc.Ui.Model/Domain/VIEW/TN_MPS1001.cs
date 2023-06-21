using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 표준공정타입관리 마스터
    /// </summary>
    [Table("TN_MPS1001T")]
    public class TN_MPS1001 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1001()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;

            TN_MPS1002List = new List<TN_MPS1002>();
        }
        [Key, Column("TYPE_CODE", Order = 0), Required(ErrorMessage = "TypeCode")] public string TypeCode { get; set; }
        [Column("TYPE_NAME"), Required(ErrorMessage = "TypeName")] public string TypeName { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual List<TN_MPS1002> TN_MPS1002List { get; set; }

    }
}
