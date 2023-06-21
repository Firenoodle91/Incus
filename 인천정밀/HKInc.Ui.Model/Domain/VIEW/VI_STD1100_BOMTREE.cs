using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 20210201 오세완 차장
    /// bom관리에서 품목코드 선택 searckedit control 출력용
    /// </summary>
    [Table("VI_STD1100_BOMTREE")]
    public class VI_STD1100_BOMTREE
    {
        public VI_STD1100_BOMTREE()
        {

        }

        [Key, Column("ITEM_CODE", Order = 0)]
        public string ItemCode { get; set; }

        [Column("ITEM_NAME")]
        public string ItemName { get; set; }

        [Column("TOP_CATEGORY")]
        public string TopCategory { get; set; }

        [Column("MIDDLE_CATEGORY")]
        public string MiddleCategory { get; set; }

        [Column("BOTTOM_CATEGORY")]
        public string BottomCategory { get; set; }

        [Column("TOP_CATEGORY_NAME")]
        public string TopCategoryName { get; set; }

        [Column("MIDDLE_CATEGORY_NAME")]
        public string MiddleCategoryName { get; set; }

        [Column("BOTTOM_CATEGORY_NAME")]
        public string BottomCategoryName { get; set; }
    }
}
