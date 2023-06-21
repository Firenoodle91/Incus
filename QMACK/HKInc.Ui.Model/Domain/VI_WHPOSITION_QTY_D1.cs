﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("VI_WHPOSITION_QTY_D1")]
    public class VI_WHPOSITION_QTY_D1
    {
        [Key,Column("WH_CODE",Order =0)] public string WhCode { get; set; }
        [Key,Column("ITEM_CODE",Order =1)] public string ItemCode { get; set; }
        [Key, Column("WH_POSITION", Order = 2)] public string WhPosition { get; set; }
        [Column("QTY")] public Nullable<decimal> Qty { get; set; }
        
    }
}