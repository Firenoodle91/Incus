using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 공정별 기본 작업표준서에서 사용
    /// </summary>
    public class TEMP_STD1500_DETAIL
    {        
        /// <summary>공정코드</summary>
        public string ProcessCode { get; set; }

        /// <summary> 공정명 </summary>
        public string ProcessName { get; set; }

        /// <summary> 순번 </summary>
        public int Seq { get; set; }

        /// <summary> 작업표준서 파일명 </summary>
        public string DesignFileName { get; set; }

        /// <summary> 작업표준서 파일경로 </summary>
        public string DesignFileUrl { get; set; }

        public DateTime? CreateTime { get; set; }

        public string CreateId { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateId { get; set; }

        /// <summary> 그리드 번호  </summary>
        public decimal RowId { get; set; }
        /// <summary>
        /// 그리드 행 상태
        /// N : 신규 U : 수정 D : 삭제
        /// </summary>
        public string RowFlag { get; set; }
    }
}

