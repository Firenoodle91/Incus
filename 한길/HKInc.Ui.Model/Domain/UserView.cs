using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKInc.Ui.Model.Domain
{
    [Table("VI_USER")]
    public class UserView
    {
        public decimal UserId { get; set; }

        [Key]
        public string LoginId { get; set; }

        public string UserName { get; set; }

        public string EmployeeNo { get; set; }
        public Nullable<int> LoginDb { get; set; }
        public Nullable<int> CodeDb { get; set; }
        public Nullable<int> ProductionDb { get; set; }
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
        public string SequenceNumber { get; set; }

        [Column("Property7")]
        public string IsPopUser { get; set; }

        public string Property8 { get; set; }
        public string Property9 { get; set; }
        public string Property10 { get; set; }
        public string Active { get; set; }

        [ForeignKey("DepartmentCode")]
        public virtual TN_STD1200 Department { get; set; }

    }
}
