using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_STD1401T")]
    public class TN_STD1401 : BaseDomain.MES_BaseDomain
    {
        /// <summary>
        /// key코드
        /// </summary>
        [Key, Column("CUSTDEPT_CODE")] public string CustDeptCD { get; set; }
        /// <summary>
        /// 거래처코드
        /// </summary>
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE")] public string CustomrCD { get; set; }
        /// <summary>
        /// 부서코드
        /// </summary>
        [Column("DEPT_CODE")] public string DeptCD { get; set; }
        /// <summary>
        /// 상위코드
        /// </summary>
        [Column("PARENT_CUSTDEPT_CODE")] public string ParentCustDeptCD { get; set; }
        /// <summary>
        /// 담당자
        /// </summary>
        [Column("MANAGER")] public string Manager { get; set; }
        /// <summary>
        /// 메모
        /// </summary>
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>
        /// 정렬
        /// </summary>
        [Column("SORT")] public int? sort { get; set; }
        /// <summary>
        /// 사용여부
        /// </summary>
        [Column("USEYN")] public bool UseYN { get; set; }
        /// <summary>
        /// 연락처
        /// </summary>
        [Column("TEL_NO")] public string TelNo { get; set; }
        /// <summary>
        /// 이메일
        /// </summary>
        [Column("EMAIL")] public string Email { get; set; }

        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}
