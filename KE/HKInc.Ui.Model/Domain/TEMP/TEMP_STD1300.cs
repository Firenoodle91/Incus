using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_STD1300
    {

        public TEMP_STD1300()
        {
            UpdFlag = "N";
            RowIndex = 1;
        }
        /// <summary>BOM코드</summary>        
        public string BomCode { get; set; }
        /// <summary>품번</summary>           
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        /// <summary>상위BOM코드</summary>    
        public string ParentBomCode { get; set; }
        /// <summary>BOM레벨</summary>        
        public int Level { get; set; }
        /// <summary>소요량</summary>         
        public decimal UseQty { get; set; }
        /// <summary>표시순서</summary>       
        public int? DisplayOrder { get; set; }
        /// <summary>공정코드</summary>       
        public string ProcessCode { get; set; }
        ///// <summary>소요량예외</summary>       
        //public string UseQtyEx { get; set; }
        ///// <summary>작업기준</summary>       
        //public string WorkStandard { get; set; }
        ///// <summary>예외품번</summary>       
        //public string ItemCodeEx { get; set; }
        /// <summary>사용여부</summary>       
        public string UseFlag { get; set; }
        /// <summary>메모</summary>           
        public string Memo { get; set; }
        /// <summary>수동관리여부</summary>           
        public string MgFlag { get; set; }
        /// <summary>
        /// 대분류
        /// </summary>
        public string TopCategory { get; set; }
        /// <summary>
        /// 중분류
        /// </summary>
        public string MiddleCategory { get; set; }
        /// <summary>
        /// 소분류
        /// </summary>
        public string BottomCategory { get; set; }
        ///// <summary>
        ///// 순도
        ///// </summary>
        //public string Purity { get; set; }
        /// <summary>
        /// 규격1
        /// </summary>
        public string Spec1 { get; set; }
        /// <summary>
        /// 규격2
        /// </summary>
        public string Spec2 { get; set; }
        /// <summary>
        /// 규격3
        /// </summary>
        public string Spec3 { get; set; }
        /// <summary>
        /// 규격4
        /// </summary>
        public string Spec4 { get; set; }
        /// <summary>
        /// 단위
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 중량
        /// </summary>        
        public decimal? Weight { get; set; }

        public string CustomItemCode { get; set; }
        public string UpdFlag { get; set; }

        public int RowIndex { get; set; }

        /// <summary>
        /// 20220311 오세완 차장
        /// TN_STD!100T temp1 column
        /// </summary>
        public string Temp1 { get; set; }

        /// <summary>
        /// 20220311 오세완 차장
        /// TN_STD!100T temp2 column
        /// </summary>
        public string Temp2 { get; set; }

        /// <summary>
        /// 20220314 오세완 차장 
        /// TN_STD!100T colorname column
        /// </summary>
        public string ColorName { get; set; }
    }
}
