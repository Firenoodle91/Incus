using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 스페어파트 관리
    /// </summary>
    [Table("TN_MEA1600T")]
    public class TN_MEA1600 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1600()
        {
            TN_MEA1601List = new List<TN_MEA1601>();
        }

        /// <summary>
        /// 설비스페어코드
        /// </summary>
        [Key, Column("SPARE_CODE"), Required(ErrorMessage = "SpareCode")] public string SpareCode { get; set; }
        /// <summary>
        /// 설비스페어명
        /// </summary>
        [Column("SPARE_NAME"), Required(ErrorMessage = "SpareName")] public string SpareName { get; set; }

        /// <summary>
        /// 20220331 오세완 차장 
        /// 스페어파트 영문명
        /// </summary>
        [Column("SPARE_NAME_ENG")]
        public string SpareNameENG { get; set; }

        /// <summary>
        /// 20220331 오세완 차장
        /// 스페어파트 중문명
        /// </summary>
        [Column("SPARE_NAME_CHN")]
        public string SpareNameCHN { get; set; }

        /// <summary>
        /// 설비 금형 구분
        /// </summary>
        [Column("MACHINEMOLDCHECK")] public string MachineMoldCheck { get; set; }   
        /// <summary>
        /// 주거래처
        /// </summary>
        [Column("MAIN_CUSTOMER_CODE")] public string MainCustomerCode { get; set; } 
        /// <summary>
        /// 규격1
        /// </summary>
        [Column("SPEC_1")] public string Spec1 { get; set; }  
        /// <summary>
        /// 규격2
        /// </summary>
        [Column("SPEC_2")] public string Spec2 { get; set; }  
        /// <summary>
        /// 규격3
        /// </summary>
        [Column("SPEC_3")] public string Spec3 { get; set; }  
        /// <summary>
        /// 규격4
        /// </summary>
        [Column("SPEC_4")] public string Spec4 { get; set; }  
        /// <summary>
        /// 단위
        /// </summary>
        [Column("UNIT")] public string Unit { get; set; }  
        /// <summary>
        /// 안전재고량
        /// </summary>
        [Column("SAFE_QTY")] public decimal? SafeQty { get; set; }  
        /// <summary>
        /// 단가
        /// </summary>
        [Column("COST")] public decimal? Cost { get; set; }  
        /// <summary>
        /// 제품사진명
        /// </summary>
        [Column("PROD_FILE_NAME")] public string ProdFileName { get; set; }
        /// <summary>
        /// 제품사진명
        /// </summary>
        [Column("PROD_FILE_IMG")] public byte[] ProdFileImage { get; set; }
        /// <summary>
        /// 제품사진URL
        /// </summary>
        [Column("PROD_FILE_URL")] public string ProdFileUrl { get; set; } 
        /// <summary>
        /// 사용여부
        /// </summary>
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }  
        /// <summary>
        /// 메모
        /// </summary>
        [Column("MEMO")] public string Memo { get; set; }  //메모
        /// <summary>
        /// 임시
        /// </summary>
        [Column("TEMP")] public string Temp { get; set; }  //임시
        /// <summary>
        /// 임시1
        /// </summary>
        [Column("TEMP1")] public string Temp1 { get; set; }  //임시1
        /// <summary>
        /// 파일폴더KEY명
        /// </summary>
        [Column("TEMP2")] public string FileFolderKeyName { get; set; }  //파일폴더KEY명
        /// <summary>
        /// 기본보관위치
        /// </summary>
        [Column("STOCK_POSITION")] public string StockPosition { get; set; }


        [NotMapped] public object localImage { get; set; }

        [NotMapped]
        public string CombineSpec
        {
            get
            {
                var spec1 = Spec1 == null ? " " : Spec1;
                var spec2 = Spec2 == null ? " " : Spec2;
                var spec3 = Spec3 == null ? " " : Spec3;
                var spec4 = Spec4 == null ? " " : Spec4;
                return spec1 + spec2 + spec3 + spec4;
            }
        }

        public virtual ICollection<TN_MEA1601> TN_MEA1601List { get; set; }




        /// <summary>
        /// 스페어파트 재고수량
        /// </summary>
        [NotMapped]
        public decimal SpareStockQty
        {
            get
            {
                if (TN_MEA1601List.Count == 0)
                    return 0;
                else
                {
                    var inQty = TN_MEA1601List.Where(p => p.Division == "01").Sum(c => c.Qty); //입고수량
                    var outQty = TN_MEA1601List.Where(p => p.Division == "02").Sum(c => c.Qty); //출고수량
                    var adjustQty = TN_MEA1601List.Where(p => p.Division == "03").Sum(c => c.Qty); //조정수량
                    return inQty - outQty + adjustQty;
                }
            }
        }
    }
}