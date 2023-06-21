using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>납품원가기준관리</summary>	

    [Table("TN_STD1105T")]
    public class TN_STD1105 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1105()
        {
        }
         
        [Key, Column("COGS", Order = 0)] public string COGS { get; set; }//매출원가 cost of goods sold

        [Column("ITEM_CODE")] public string ItemCode { get; set; }//품목코드

        [Column("START_DATE")] public DateTime StartDate { get; set; }//시작 날짜

        [Column("END_DATE")] public DateTime EndDate { get; set; }//종료 날짜

        [Column("SRC_COST")] public decimal SrcCost { get; set; }//자재단가

        [Column("BarfeederCNC_FLAG")] public string BarfeederCNCflag { get; set; }//BarfeederCNC 공정 진행 여부

        [Column("BarfeederCNC_CYCLE_TIME")] public decimal BarfeederCNCcycleTime { get; set; }//BarfeederCNC 공정 제품 생산 시간

        [Column("CNC_FLAG")] public string CNCflag { get; set; }//CNC 공정 진행 여부

        [Column("CNC1_CYCLE_TIME")] public decimal CNC1cycleTime { get; set; }//CNC1 공정 제품 생산 시간

        [Column("CNC2_CYCLE_TIME")] public decimal CNC2cycleTime { get; set; }//CNC2 공정 제품 생산 시간

        [Column("CNC3_CYCLE_TIME")] public decimal CNC3cycleTime { get; set; }//CNC3 공정 제품 생산 시간

        [Column("MCT_FLAG")] public string MCTflag { get; set; }//MCT 공정 진행 여부

        [Column("MCT_CYCLE_TIME")] public decimal MCTcycleTime { get; set; }//MCT 공정 제품 생산 시간

        [Column("Tapping_FLAG")] public string Tappingflag { get; set; }//Tapping 공정 진행 여부

        [Column("Tapping_CYCLE_TIME")] public decimal TappingcycleTime { get; set; }//Tapping 공정 제품 생산 시간

        [Column("MEMO")] public string Memo { get; set; }  //메모



    }
}