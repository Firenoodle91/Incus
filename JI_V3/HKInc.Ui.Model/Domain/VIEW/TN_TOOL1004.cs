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
    [Table("TN_TOOL1004T")]
    public class TN_TOOL1004 : BaseDomain.MES_BaseDomain
    {
        public TN_TOOL1004()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;

            TN_TOOL1005List = new List<TN_TOOL1005>();
        }
        [Key, Column("TYPE_CODE", Order = 0), Required(ErrorMessage = "TypeCode")] public string TypeCode { get; set; }
        [Column("TYPE_NAME"), Required(ErrorMessage = "TypeName")] public string TypeName { get; set; }
        [Column("MEMO")] public string Memo { get; set; }


        public virtual List<TN_TOOL1005> TN_TOOL1005List { get; set; }

    }
}
