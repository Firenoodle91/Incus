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
    /// 공통코드
    /// </summary>
    [Table("TN_STD1000T")]
    public class TN_STD1000 : BaseDomain.BaseDomain
    {
        public TN_STD1000()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Key, Column("CODE_MAIN", Order = 0), Required(ErrorMessage = "코드분류는 필수입니다.")]
        public string Codemain { get; set; }
        [Key, Column("CODE_MID", Order = 1), Required(ErrorMessage = "대분류는 필수입니다.")]
        public string Codemid { get; set; }
        [Key, Column("CODE_SUB", Order = 2), Required(ErrorMessage = "중분류는 필수입니다.")]
        public string Codesub { get; set; }
        [Key, Column("CODE_VAL", Order = 3), Required(ErrorMessage = "소분류는 필수입니다.")]
        public string Codeval { get; set; }
        [Column("CODE_NAME")]
        public string Codename { get; set; }
        [Column("CODE_NAME1")]
        public string Codename1 { get; set; }
        [Column("CODE_NAME2")]
        public string Codename2 { get; set; }
        [Column("PROPERTY1")]
        public string Property1 { get; set; }
        [Column("PROPERTY2")]
        public string Property2 { get; set; }
        [Column("USE_YN")]
        public string Useyn { get; set; }
        [Column("DISPLAYORDER")]
        public string Displayorder { get; set; }
        [Column("MCODE")]
        public string Mcode { get; set; }

        [NotMapped]
        public Nullable<int> displayorder
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Displayorder);
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}