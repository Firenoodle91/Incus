using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 금형등급관리평가 Master
    /// </summary>
    public class TEMP_XFMOLD1700_MASTER
    {
        /// <summary>
        /// 금형관리코드
        /// </summary>
        public string MoldMCode { get; set; }
        /// <summary>
        /// 금형코드
        /// </summary>
        public string MoldCode { get; set; }
        /// <summary>
        /// 금형명
        /// </summary>
        public string MoldName { get; set; }
        /// <summary>
        /// 제품코드
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 제작처
        /// </summary>
        public string MoldMakerCust { get; set; }
        /// <summary>
        /// 이관일
        /// </summary>
        public DateTime? TransferDate { get; set; }
        /// <summary>
        /// 메인설비
        /// </summary>
        public string MainMachineCode { get; set; }
        /// <summary>
        /// 점검주기
        /// </summary>
        public string CheckCycle { get; set; }
        /// <summary>
        /// 다음 점검일
        /// </summary>
        public DateTime? NextCheckDate { get; set; }
        /// <summary>
        /// 마지막 평가일
        /// </summary>
        public DateTime? LastCheckDate { get; set; }


    }
}
