using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("VI_DEPARTMENT")]
    public class UserDepartment
    {
        [Key, Column("DEPARTMENT_CODE"), Required(ErrorMessage = "Department Code is required.")]
        public string DepartmentCode { get; set; }

        [Column("DEPARTMENT_NAME")]
        public string DepartmentName { get; set; }

        [Column("DEPARTMENT_MANAGEMENT_ID")]
        public Nullable<decimal> DepartmentManager { get; set; }

        [Column("PARENT_DEPARTMENT_CODE")]
        public string ParentDepartmentCode { get; set; }

        [Column("LEVEL")]
        public string Level { get; set; }

        [Column("SEQUENCE_NUMBER")]
        public Nullable<int> SeqNumber { get; set; }

        [Column("USE_PLAG")]
        public string UseFlag { get; set; }
    }
}

