using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 이동표관리 마스터
    /// </summary>
    public class TEMP_ITEM_MOVE_NO_MASTER
    {
        /// <summary>이동표번호</summary>
        public string ItemMoveNo { get; set; }
        /// <summary>작업지시번호</summary>
        public string WorkNo { get; set; }
        /// <summary>생산 LOT NO</summary>
        public string ProductLotNo { get; set; }
        /// <summary>품번(도번)</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>품명(영문)</summary>
        public string ItemNameENG { get; set; }
        /// <summary>품명(중문)</summary>
        public string ItemNameCHN { get; set; }
        /// <summary>거래처코드</summary>
        public string CustomerCode { get; set; }
        /// <summary>수주번호</summary>
        public string OrderNo { get; set; }
        /// <summary>메모</summary>
        public string Memo { get; set; }
        /// <summary>박스내수량</summary>
        public decimal? BoxInQty { get; set; }
        /// <summary>출력일</summary>
        public DateTime PrintDate { get; set; }
    }
}
