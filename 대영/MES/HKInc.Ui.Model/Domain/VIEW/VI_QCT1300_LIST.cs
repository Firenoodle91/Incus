using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>부적합관리</summary>	
    [Table("VI_QCT1300_LIST")]
    public class VI_QCT1300_LIST 
    {
        public VI_QCT1300_LIST()
        {
        }

        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>부적합번호</summary>            
        [Column("PNO")] public string PNO { get; set; }
        /// <summary>실적/클레임구분</summary>              
        [Column("P_TYPE")] public string P_TYPE { get; set; }
        /// <summary>실적/클레임 키</summary>              
        [Column("P_KEY")] public string P_KEY { get; set; }                              
        /// <summary>품번(도번)</summary>          
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>          
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        /// <summary>작업지시번호</summary>           
        [Column("WORK_NO")] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>               
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>               
        [Column("PROCESS_SEQ")] public int? ProcessSeq { get; set; }
        /// <summary>생산 LOT_NO</summary>            
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>불량수량</summary>            
        [Column("BAD_QTY")] public decimal? BadQty { get; set; }
        /// <summary>불량유형</summary>            
        [Column("BAD_TYPE")] public string BadType { get; set; }
        /// <summary>불량유형명</summary>            
        [Column("BAD_NAME")] public string BadName { get; set; }
        /// <summary>불량유형명(영문)</summary>            
        [Column("BAD_NAME_ENG")] public string BadNameENG { get; set; }
        /// <summary>불량유형명(중문)</summary>            
        [Column("BAD_NAME_CHN")] public string BadNameCHN { get; set; }
        /// <summary>작업자</summary>              
        [Column("WORK_ID")] public string WorkId { get; set; }
        /// <summary>발생일</summary>            
        [Column("OCCUR_DATE")] public DateTime OccurDate { get; set; }
        /// <summary>설비코드</summary>              
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>가공시간</summary>              
        [Column("PROCESS_MINUTE")] public int? ProcessMinute { get; set; }
        /// <summary>리워크공정 여부</summary>              
        [Column("REWORK_FLAG")] public string ReworkFlag { get; set; }
        /// <summary>이동표 번호</summary>
        [Column("ITEM_MOVE_NO")] public string ItemMoveNo { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}