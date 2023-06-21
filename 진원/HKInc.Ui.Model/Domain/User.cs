using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("User")]
    public class User : BaseDomain.BaseDomain3
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal UserId { get; set; }

        [Required(ErrorMessage = "Login Id is required.")]
        public string LoginId { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        public string EmployeeNo { get; set; }
        //public Nullable<int> LoginDb { get; set; }
        //public Nullable<int> CodeDb { get; set; }
        //public Nullable<int> ProductionDb { get; set; }
        public string ADUser { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> HireDate { get; set; }
        public Nullable<DateTime> DischargeDate { get; set; }

        [Column("Property1")]
        public string Rank { get; set; }

        [Column("Property2")]
        public string GroupCode { get; set; }

        [Column("Property3")]
        public string DepartmentCode { get; set; }

        [Column("Property4")]
        public string Email { get; set; }

        [Column("Property5")]
        public string CellPhone { get; set; }

        [Column("Property6")]
        public string PurMaster { get; set; }

        [Column("Property7")]
        public string IsPopUser { get; set; }
        [Column("Property8")]
        public string Property8 { get; set; }
        [Column("Property9")]
        public string Property9 { get; set; }
        [Column("Property10")]
        public string Property10 { get; set; }
        public string Active { get; set; }

        //[Column("SignFile")]
        //public string SignFile { get; set; }
        //[Column("signImg")]
        //public byte[] SignImg { get; set; }

        [ForeignKey("DepartmentCode")]
        public virtual UserDepartment Department { get; set; }

        public virtual ICollection<UserUserGroup> UserUserGroupList { get; set; }


    }
}
