using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220425 오세완 차장
    /// 신규 로트 추적 디테일
    /// </summary>
    [Table("TN_LOT_DTL")]
    public class TN_LOT_DTL : BaseDomain.MES_BaseDomain
    {
        public TN_LOT_DTL()
        {

        }

        /// <summary>
        /// 20220425 오세완 차장
        /// 작업일
        /// </summary>
        //[ForeignKey("TN_LOT_MST_V2"), Key, Column("WORKING_DATE", Order = 0)]
        [Key, Column("WORKING_DATE", Order = 0)]
        public DateTime WorkingDate { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 생산lotno
        /// </summary>
        //[ForeignKey("TN_LOT_MST_V2"), Key, Column("PRODUCT_LOT_NO", Order = 1)]
        [Key, Column("PRODUCT_LOT_NO", Order = 1)]
        public string ProductLotNo { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 작업지시번호
        /// </summary>
        //[ForeignKey("TN_LOT_MST_V2"), Key, Column("WORK_NO", Order = 2)]
        [Key, Column("WORK_NO", Order = 2)]
        public string WorkNo { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 투입순번
        /// </summary>
        [Key, Column("SEQ", Order = 3)]
        public decimal Seq { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 원자재 품목코드
        /// </summary>
        [Column("SRC_CODE")]
        public string SrcCode { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 생산한 설비코드
        /// </summary>
        [Column("MACHINE_CODE")]
        public string MachineCode { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 생산한 공정코드
        /// </summary>
        [Column("PROCESS_CODE")]
        public string ProcessCode { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 원자재 출고 lotno
        /// </summary>
        [Column("SRC_IN_LOT_NO")]
        public string SrcInLotNo { get; set; }


        public virtual TN_LOT_MST_V2 TN_LOT_MST { get; set; }   
    }
}
