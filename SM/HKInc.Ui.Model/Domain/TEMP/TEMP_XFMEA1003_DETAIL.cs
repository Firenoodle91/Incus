using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>설비점검이력관리 디테일</summary>	
    public class TEMP_XFMEA1003_DETAIL 
    {
        public TEMP_XFMEA1003_DETAIL()
        {

        }

        /// <summary>조회년월</summary>            
        public DateTime MonthDate { get; set; }
        /// <summary>조회년월</summary>            
        public int CheckSeq { get; set; }
        /// <summary>점검위치</summary>            
        public string CheckPosition { get; set; }
        /// <summary>점검항목</summary>            
        public string CheckList { get; set; }
        /// <summary>점검방법</summary>            
        public string CheckWay { get; set; }
        /// <summary>육안검사여부</summary>            
        public string Temp { get; set; }
        /// <summary>점검주기</summary>            
        public string CheckCycle { get; set; }
        /// <summary>점검기준일</summary>          
        public string CheckStandardDate { get; set; }
        /// <summary>관리기준</summary>            
        public string ManagementStandard { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string Day4 { get; set; }
        public string Day5 { get; set; }
        public string Day6 { get; set; }
        public string Day7 { get; set; }
        public string Day8 { get; set; }
        public string Day9 { get; set; }
        public string Day10 { get; set; }
        public string Day11{ get; set; }
        public string Day12{ get; set; }
        public string Day13{ get; set; }
        public string Day14{ get; set; }
        public string Day15{ get; set; }
        public string Day16{ get; set; }
        public string Day17{ get; set; }
        public string Day18{ get; set; }
        public string Day19{ get; set; }
        public string Day20{ get; set; }
		public string Day21{ get; set; }
        public string Day22{ get; set; }
        public string Day23{ get; set; }
        public string Day24{ get; set; }
        public string Day25{ get; set; }
        public string Day26{ get; set; }
        public string Day27{ get; set; }
        public string Day28{ get; set; }
        public string Day29{ get; set; }
        public string Day30{ get; set; }
        public string Day31{ get; set; }


        public string Day1_LastCheckId { get; set; }
        public string Day2_LastCheckId { get; set; }
        public string Day3_LastCheckId { get; set; }
        public string Day4_LastCheckId { get; set; }
        public string Day5_LastCheckId { get; set; }
        public string Day6_LastCheckId { get; set; }
        public string Day7_LastCheckId { get; set; }
        public string Day8_LastCheckId { get; set; }
        public string Day9_LastCheckId { get; set; }
        public string Day10_LastCheckId { get; set; }
        public string Day11_LastCheckId { get; set; }
        public string Day12_LastCheckId { get; set; }
        public string Day13_LastCheckId { get; set; }
        public string Day14_LastCheckId { get; set; }
        public string Day15_LastCheckId { get; set; }
        public string Day16_LastCheckId { get; set; }
        public string Day17_LastCheckId { get; set; }
        public string Day18_LastCheckId { get; set; }
        public string Day19_LastCheckId { get; set; }
        public string Day20_LastCheckId { get; set; }
        public string Day21_LastCheckId { get; set; }
        public string Day22_LastCheckId { get; set; }
        public string Day23_LastCheckId { get; set; }
        public string Day24_LastCheckId { get; set; }
        public string Day25_LastCheckId { get; set; }
        public string Day26_LastCheckId { get; set; }
        public string Day27_LastCheckId { get; set; }
        public string Day28_LastCheckId { get; set; }
        public string Day29_LastCheckId { get; set; }
        public string Day30_LastCheckId { get; set; }
        public string Day31_LastCheckId { get; set; }
    }
}