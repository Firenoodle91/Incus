using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20210924 오세완 차장
    /// USP_GET_XFQCT1900_MASTER 프로시저 반환객체
    /// </summary>
    public class TEMP_XFQCT1900_MASTER
    {
        public TEMP_XFQCT1900_MASTER()
        {

        }

        /// <summary>
        /// 20210924 오세완 차장
        /// 출고번호
        /// </summary>
        public string OutNo { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 수주번호
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 납품계획번호
        /// </summary>
        public string DelivNo { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 거래처코드
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 거래처코드명
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 거래처코드명 영문
        /// </summary>
        public string CustomerNameENG { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 거래처코드명 중문
        /// </summary>
        public string CustomerNameCHN { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 품목코드
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 품번
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 품목명
        /// </summary>
        public string ItemName1 { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 품목명 영문
        /// </summary>
        public string ItemNameENG { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 품목명 중문
        /// </summary>
        public string ItemNameCHN { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 수주수량
        /// </summary>
        public decimal? DelivQty { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 출고수량(출하검사 설정한 건)
        /// </summary>
        public decimal? SumOutQty { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 출고일
        /// </summary>
        public DateTime OutDate { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 출고자
        /// </summary>
        public string OutId { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 출고자명
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// </summary>
        public DateTime CheckDate { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 메모
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 출하검사번호
        /// </summary>
        public string ShipmentInspectionNo { get; set; }

        /// <summary>
        /// 20210928 오세완 차장 insert / update type
        /// </summary>
        public string Type { get; set; }
    }
}
