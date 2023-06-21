using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.BaseDomain
{
    public abstract class MES_BaseDomain
    {
        /// <summary>
        /// 2021-10-26 김진우 주임 수정
        /// 기존 MES_BaseDomain 을 사용하는 테이블들의 초기 생성자, 생성시간, 수정자, 수정시간의 값을 불러오지 못하여 수정
        /// 원본은 최하단 주석친 부분
        /// </summary>
        public MES_BaseDomain()
        {
            var dateTime = DateTime.Now;

            CreateId = GsValue.UserId;
            CreateTime = dateTime;
            UpdateId = GsValue.UserId;
            UpdateTime = dateTime;

            _Check = "N";
        }
        [Column("UPD_ID")] public string UpdateId { get; set; }

        [Column("UPD_DATE")] public Nullable<System.DateTime> UpdateTime { get; set; }

        [Column("INS_ID"), Required] public string CreateId { get; set; }

        [Column("INS_DATE"), Required] public Nullable<System.DateTime> CreateTime { get; set; }

        //[Column("FACT_CODE"),Required]
        //public string FactCode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ROW_ID"), Required(ErrorMessage = "RowId is required")]
        public decimal RowId { get; set; }

        /// <summary>체크여부</summary>
        [NotMapped] public string _Check { get; set; }
        /// <summary>새로운 행 추가시</summary>        2021-11-17 김진우 주임 추가
        [NotMapped] public string NewRowFlag { get; set; }
        /// <summary>수정된 행</summary>                2021-11-17 김진우 주임 추가
        [NotMapped] public string EditRowFlag { get; set; }
    }



    //public abstract class MES_BaseDomain
    //{
    //    [Column("UPD_ID")]
    //    public string UpdateId { get; set; }

    //    [Column("UPD_DATE")]
    //    public Nullable<System.DateTime> UpdateTime { get; set; }

    //    [Column("INS_ID"), Required]
    //    public string CreateId { get; set; }

    //    [Column("INS_DATE"), Required]
    //    public Nullable<System.DateTime> CreateTime { get; set; }

    //    //[Column("FACT_CODE"),Required]
    //    //public string FactCode { get; set; }

    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [Column("ROW_ID"), Required(ErrorMessage = "RowId is required")]
    //    public decimal RowId { get; set; }
    //}
}
