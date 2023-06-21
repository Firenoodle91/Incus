using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20210930 오세완 차장
    /// USP_GET_XFQCT1900_DETAIL 반환 객체
    /// </summary>
    public class TEMP_XFQCT1900_DETAIL
    {
        public TEMP_XFQCT1900_DETAIL()
        {

        }

        /// <summary>
        /// 20210930 오세완 차장 출하검사번호
        /// </summary>
        public string ShipmentInspectionNo { get; set; }

        /// <summary>
        /// 20210930 오세완 차장 출하검사번호 순번
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 20210930 오세완 차장 생산lotno
        /// </summary>
        public string ProductLotNo { get; set; }

        /// <summary>
        /// 20210930 오세완 차장 출고수량
        /// </summary>
        public decimal? OutQty { get; set; }

        /// <summary>
        /// 20210930 오세완 차장 검사결과
        /// </summary>
        public string CheckResult { get; set; }

        /// <summary>
        /// 20210930 오세완 차장 검사자
        /// </summary>
        public string CheckId { get; set; }

        /// <summary>
        /// 20210930 오세완 차장 메모
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 20210930 오세완 차장 실제 출하검사를 진행하는 검사번호
        /// </summary>
        public string InspNo { get; set; }

        /// <summary>
        /// 20210930 오세완 차장 insert / update 확인
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 20211006 오세완 차장
        /// 출하검사 성적서 출력 선택용
        /// </summary>
        public string _Check { get; set; }
    }
}
