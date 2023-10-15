using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XFQCT1201_MASTER
    {
        /// <summary>출고일자</summary>
        public string OutDate { get; set; }
        /// <summary>출고번호</summary>
        public string OutNo { get; set; }
        /// <summary>생산 LOT NO</summary>
        public string ProductLotNo { get; set; }
        /// <summary>품번</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>품명 영문</summary>
        public string ItemNameENG { get; set; }
        /// <summary>품명 중문</summary>
        public string ItemNameCHN { get; set; }
        /// <summary>거래처코드</summary>
        public string CustomerCode { get; set; }
        /// <summary>거래처명</summary>
        public string CustomerName { get; set; }
        /// <summary>거래처명 영문</summary>
        public string CustomerNameENG { get; set; }
        /// <summary>거래처명 중문</summary>
        public string CustomerNameCHN { get; set; }
    }
}
 