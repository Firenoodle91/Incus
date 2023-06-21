using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.Domain.VIEW;

namespace HKInc.Ui.Model.Domain
{
    [Table("User")]
    public class User : BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal UserId { get; set; } //일련번호

        [Required(ErrorMessage = "LoginId")]
        public string LoginId { get; set; } //로그인ID

        [Required(ErrorMessage = "Password")]
        public string Password { get; set; } //패스워드

        [Required(ErrorMessage = "UserName")]
        public string UserName { get; set; } //사용자명

        public string EmployeeNo { get; set; } //사번

        public string Description { get; set; } //비고

        public DateTime? HireDate { get; set; } //입사일자

        public DateTime? DischargeDate { get; set; } //퇴사일자

        [Column("Property1")]
        public string RankCode { get; set; } //직급코드

        [Column("Property2")]
        public string ProductTeamCode { get; set; } //제조팀코드

        [Column("Property3")]
        public string DepartmentCode { get; set; } //부서코드

        [Column("Property4")]
        public string Email { get; set; } //이메일

        [Column("Property5")]
        public string CellPhone { get; set; } //전화번호

        [Column("Property6")]
        public string DisplayOrder { get; set; } //표시순서

        [Column("Property7")]
        public string Property7 { get; set; }

        [Column("Property8")]
        public string Property8 { get; set; }

        [Column("Property9")]
        public string Property9 { get; set; }

        [Column("Property10")]
        public string Property10 { get; set; }

        public string Active { get; set; } //사용여부

        public string FielName { get; set; } //파일명

        public string FileUrl { get; set; } //파일URL

        [Column("Main_YN")]
        public string MainYn { get; set; }//자사타사여부
        [Column("CUSTOMER_CODE")]
        public string CustomerCode { get; set; }//거래처코드

        public virtual ICollection<UserUserGroup> UserUserGroupList { get; set; }

        [ForeignKey("DepartmentCode")]
        public virtual TN_STD1200 TN_STD1200 { get; set; }
    }
}
