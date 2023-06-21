using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("TN_STD1200T")]
    public class TN_STD1200 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1200() { }

        [Key, Column("DEPARTMENT_CODE"), Required(ErrorMessage = "Department Code is required.")] public string DepartmentCode { get; set; }
        [Column("DEPARTMENT_NAME"), Required(ErrorMessage = "DepartmentName Code is required.")] public string DepartmentName { get; set; }
        [Column("DEPARTMENT_MANAGEMENT_ID")] public Nullable<decimal> DepartmentManager { get; set; }
        [Column("PARENT_DEPARTMENT_CODE")] public string ParentDepartmentCode { get; set; }
        [Column("LEVEL")] public string Level { get; set; }
        [Column("SEQUENCE_NUMBER")] public Nullable<int> SeqNumber { get; set; }
        [Column("USE_PLAG")] public string UseFlag { get; set; }

        //Parent
        [ForeignKey("DepartmentManager")] public virtual HKInc.Ui.Model.Domain.User ManagerUser { get; set; }
        [ForeignKey("ParentDepartmentCode")] public virtual TN_STD1200 ParentDepartment { get; set; }

    }
    }
