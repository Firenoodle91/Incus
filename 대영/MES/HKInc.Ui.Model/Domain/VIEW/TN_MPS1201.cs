using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>작업실적관리 마스터</summary>	
    [Table("TN_MPS1201T")]
    public class TN_MPS1201 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1201()
        {
            TN_MPS1202List = new List<TN_MPS1202>();
            TN_MPS1203List = new List<TN_MPS1203>();
            TN_MPS1300List = new List<TN_MPS1300>();
            TN_TOOL1002List = new List<TN_TOOL1002>();
            TN_TOOL1003List = new List<TN_TOOL1003>();            
        }
        /// <summary>작업지시번호</summary>             
        [ForeignKey("TN_MPS1200"), Key, Column("WORK_NO", Order = 0), Required(ErrorMessage = "WorkNo")] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>                 
        [ForeignKey("TN_MPS1200"), Key, Column("PROCESS_CODE", Order = 1), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>                 
        [ForeignKey("TN_MPS1200"), Key, Column("PROCESS_SEQ", Order = 2), Required(ErrorMessage = "ProcessSeq")] public int ProcessSeq { get; set; }
        /// <summary>생산 LOT_NO</summary>              
        [Key, Column("PRODUCT_LOT_NO", Order = 3), Required(ErrorMessage = "ProductLotNo")] public string ProductLotNo { get; set; }
        /// <summary>품번(도번)</summary>               
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>               
        [Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>마지막 이동표번호</summary>               
        [Column("ITEM_MOVE_NO")] public string ItemMoveNo { get; set; }
        /// <summary>설비코드</summary>                 
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>실적종료일</summary>               
        [Column("RESULT_DATE")] public DateTime? ResultDate { get; set; }
        /// <summary>실적시작시간</summary>             
        [Column("RESULT_START_DATE")] public DateTime? ResultStartDate { get; set; }
        /// <summary>실적종료시간</summary>             
        [Column("RESULT_END_DATE")] public DateTime? ResultEndDate { get; set; }
        /// <summary>총생산수량</summary>               
        [Column("RESULT_SUM_QTY")] public decimal? ResultSumQty { get; set; }
        /// <summary>총양품수량</summary>               
        [Column("OK_SUM_QTY")] public decimal? OkSumQty { get; set; }
        /// <summary>총불량수량</summary>               
        [Column("BAD_SUM_QTY")] public decimal? BadSumQty { get; set; }

        /// <summary>
        /// 20210614 오세완 차장
        /// 리워크 양품 수량 총계
        /// </summary>
        [Column("REWORK_OK_SUM_QTY")]
        public decimal? ReworkOkSumQty { get; set; }

        /// <summary>
        /// 20210614 오세완 차장
        /// 리워크 불량 수량 총계
        /// </summary>
        [Column("REWORK_BAD_SUM_QTY")]
        public decimal? ReworkBadSumQty { get; set; }

        /// <summary>
        /// 메모
        /// </summary>
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>
        /// 20210612 오세완 차장 작업시작시 매칭된 금형코드
        /// </summary>                     
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                    
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                    
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary>
        /// 20210709 오세완 차장
        /// plc로부터 실적 받아온 수량
        /// </summary>
        [Column("PLC_COUNT")]
        public int? PlcCount { get; set; }

        /// <summary>
        /// 20210709 오세완 차장
        /// 사용자가 수동으로 입력한 실적수량
        /// </summary>
        [Column("WORKER_INPUT_RESULT_QTY")]
        public decimal? WorkerInputResultQty { get; set; }

        /// <summary>
        /// 20210709 오세완 차장
        /// 사용자가 수동으로 입력한 양품 수량
        /// </summary>
        [Column("WORKER_INPUT_OK_QTY")]
        public decimal? WorkerInputOkQty { get; set; }
        
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_MPS1200 TN_MPS1200 { get; set; }
        public virtual ICollection<TN_MPS1202> TN_MPS1202List { get; set; }
        public virtual ICollection<TN_MPS1203> TN_MPS1203List { get; set; }
        public virtual ICollection<TN_MPS1300> TN_MPS1300List { get; set; }
        public virtual ICollection<TN_TOOL1002> TN_TOOL1002List { get; set; }
        public virtual ICollection<TN_TOOL1003> TN_TOOL1003List { get; set; }
    }
}