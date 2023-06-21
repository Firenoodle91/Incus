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
    /// 부서관리
    /// </summary>
    [Table("TN_STD1200T")]
    public class TN_STD1200 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1200()
        {
        }

        [Key, Column("DEPARTMENT_CODE"), Required(ErrorMessage = "DepartmentCode")]
        public string DepartmentCode { get; set; } //부서코드

        [ForeignKey("ParentDepartment"), Column("PARENT_DEPARTMENT_CODE")]
        public string ParentDepartmentCode { get; set; } //상위부서코드

        [Column("DEPARTMENT_NAME"), Required(ErrorMessage = "DepartmentName")]
        public string DepartmentName { get; set; } //부서명

        [Column("DEPARTMENT_MANAGEMENT_ID")]
        public string DepartmentManager { get; set; } //부서관리자

        [Column("DISPLAY_ORDER")]
        public int? DisplayOrder { get; set; } //표시순서

        [Column("USE_FLAG")]
        public string UseFlag { get; set; } //사용여부

        public virtual TN_STD1200 ParentDepartment { get; set; }
    }
}
