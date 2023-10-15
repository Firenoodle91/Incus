using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>금형일상점검조회</summary>	
    public class TEMP_MOLD_CHECK_LIST
    {

        public TEMP_MOLD_CHECK_LIST()
        {

        }

        /// <summary>      
        /// 금형관리번호
        /// </summary>
        public string  MoldMCode { get; set; }

        /// <summary>      
        /// 점검순번
        /// </summary>
        public int Seq { get; set; }
        /// <summary>      
        /// 점검위치
        /// </summary>
        public string CheckPosition { get; set; }
        /// 점검항목
        /// </summary>            
        public string CheckList { get; set; }
        /// <summary>      
        /// 점검방법
        /// </summary>
        public string CheckWay { get; set; }      
        /// <summary>        
        /// 점검주기
        /// <summary>            
        public string CheckCycle { get; set; }
        /// <summary>        
        /// 점검기준일
        /// <summary>            
        public string CheckStandardDate { get; set; }
        /// <summary>        
        /// 관리기준
        /// <summary>            
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
        public string Day11 { get; set; }
        public string Day12 { get; set; }
        public string Day13 { get; set; }
        public string Day14 { get; set; }
        public string Day15 { get; set; }
        public string Day16 { get; set; }
        public string Day17 { get; set; }
        public string Day18 { get; set; }
        public string Day19 { get; set; }
        public string Day20 { get; set; }
        public string Day21 { get; set; }
        public string Day22 { get; set; }
        public string Day23 { get; set; }
        public string Day24 { get; set; }
        public string Day25 { get; set; }
        public string Day26 { get; set; }
        public string Day27 { get; set; }
        public string Day28 { get; set; }
        public string Day29 { get; set; }
        public string Day30 { get; set; }
        public string Day31 { get; set; }


        public string Day1_CheckId { get; set; }
        public string Day2_CheckId { get; set; }
        public string Day3_CheckId { get; set; }
        public string Day4_CheckId { get; set; }
        public string Day5_CheckId { get; set; }
        public string Day6_CheckId { get; set; }
        public string Day7_CheckId { get; set; }
        public string Day8_CheckId { get; set; }
        public string Day9_CheckId { get; set; }
        public string Day10_CheckId { get; set; }
        public string Day11_CheckId { get; set; }
        public string Day12_CheckId { get; set; }
        public string Day13_CheckId { get; set; }
        public string Day14_CheckId { get; set; }
        public string Day15_CheckId { get; set; }
        public string Day16_CheckId { get; set; }
        public string Day17_CheckId { get; set; }
        public string Day18_CheckId { get; set; }
        public string Day19_CheckId { get; set; }
        public string Day20_CheckId { get; set; }
        public string Day21_CheckId { get; set; }
        public string Day22_CheckId { get; set; }
        public string Day23_CheckId { get; set; }
        public string Day24_CheckId { get; set; }
        public string Day25_CheckId { get; set; }
        public string Day26_CheckId { get; set; }
        public string Day27_CheckId { get; set; }
        public string Day28_CheckId { get; set; }
        public string Day29_CheckId { get; set; }
        public string Day30_CheckId { get; set; }
        public string Day31_CheckId { get; set; }
    }
}
