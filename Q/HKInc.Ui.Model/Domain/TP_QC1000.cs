using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    public class TP_QC1000
    {
        public TP_QC1000()
        { }

        /// <summary> 공정번호 </summary>
        public string ProcessCode { get; set; }
        /// <summary> 검사항목 </summary>
        public string CheckName { get; set; }
        /// <summary> 검사종류 </summary>
        public string ProcessGu { get; set; }
        /// <summary> 검사방법 </summary>
        public string CheckProv { get; set; }
        /// <summary> 규격 </summary>
        public string CheckStand { get; set; }
        /// <summary> 상한 </summary>
        public Nullable<double> UpQuad { get; set; }
        /// <summary> 하한 </summary>
        public Nullable<double> DownQuad { get; set; }
        /// <summary> 계측기종류 </summary>                  // 2022-02-23 김진우 추가
        public string Temp1 { get; set; }
        /// <summary> 검사방법, 검사순서 </summary>
        public string Temp2 { get; set; }

    }
}

