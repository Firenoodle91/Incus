using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// POP 이동표재출력
    /// </summary>
    public class TEMP_PRINT_ITEM_MOVE_NO
    {
        /// <summary>이동표번호</summary>
        public string ItemMoveNo { get; set; }
        /// <summary>생산 LOT NO</summary>
        public string ProductLotNo { get; set; }
        /// <summary>박스내수량</summary>
        public decimal? OkQty { get; set; }
    }
}
