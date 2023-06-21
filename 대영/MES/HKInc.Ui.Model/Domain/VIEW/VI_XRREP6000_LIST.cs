using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>설비현황</summary>	
    [Table("VI_XRREP6000_LIST")]
    public class VI_XRREP6000_LIST
    {
        public VI_XRREP6000_LIST()
        {

        }

        /// <summary>설비고유코드</summary>           
        [ForeignKey("TN_MEA1000"), Key, Column("MACHINE_MCODE")] public string MachineMCode { get; set; }
        /// <summary>설비코드</summary>           
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>설비그룹코드</summary>           
        [Column("MACHINE_GROUP_CODE")] public string MachineGroupCode { get; set; }
        /// <summary>설비명</summary>           
        [Column("MACHINE_NAME")] public string MachineName { get; set; }
        /// <summary>작업상태</summary>           
        [Column("JOB_STATES")] public string JobStates { get; set; }
        /// <summary>품목코드</summary>           
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>품목</summary>           
        [Column("ITEM_NAME")] public string ItemName { get; set; }
        /// <summary>품번명</summary>           
        [Column("ITEM_NAME1")] public string ItemName1 { get; set; }
        /// <summary>거래처</summary>           
        [Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        /// <summary>작업지시번호</summary>           
        [Column("WORK_NO")] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>           
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>생산LOTNO</summary>           
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>실적시작시간</summary>           
        [Column("RESULT_START_DATE")] public DateTime? ResultStartDate  { get; set; }
        /// <summary>양품수량</summary>                 
        [Column("OK_SUM_QTY")] public decimal? OkSumQty { get; set; }
        /// <summary>불량수량</summary>                 
        [Column("BAD_SUM_QTY")] public decimal? BadSumQty { get; set; }

        /// <summary>근무시간(분)</summary>                 
        [Column("WORK_TIME")] public int WorkTime { get; set; }
        /// <summary>비가동시간(분)</summary>                 
        [Column("STOP_M")] public int StopM { get; set; }
        /// <summary>가동시간(분)</summary>                 
        [Column("UP_TIME")] public int UpTime { get; set; }

        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
    }
}