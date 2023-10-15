using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 생산POP BOM리스트 - USP_GET_BOMLIST_MATERIAL_INFO
    /// </summary>
    public class TEMP_XFPOP_BOMLIST
    {
        /// <summary>
        /// BOM 코드
        /// </summary>
        public string BomCode { get; set; }
        /// <summary>
        /// 품목코드
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// TOP 카테고리 
        /// </summary>
        public string TopCategory { get; set; }
        /// <summary>
        /// 사용량
        /// </summary>
        public decimal UseQty { get; set; }
        /// <summary>
        /// 공정코드
        /// </summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 수동관리여부
        /// </summary>
        public string MgFlag { get; set; }
        /// <summary>
        /// 사용여부
        /// </summary>
        public string UseFlag { get; set; }
        /// <summary>
        /// BOM 타입(생산/투입)
        /// </summary>
        public string BomTyp { get; set; }
    }
}
