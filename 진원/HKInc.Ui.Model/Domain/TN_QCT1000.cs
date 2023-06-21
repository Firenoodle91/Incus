using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>검사 마스터</summary>	
    [Table("TN_QCT1000T")]
    public class TN_QCT1000 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1000()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }

        /// <summary품번</summary>
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 0)] public string ItemCode { get; set; }
        /// <summary>순번</summary>
        [Key, Column("SEQ", Order = 1)] public int Seq { get; set; }
        /// <summary>공정코드</summary>
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>검사항목</summary>
        [Column("CHECK_NAME")] public string CheckName { get; set; }
        /// <summary>검사종류</summary>
        [Column("PROCESS_GU")] public string ProcessGu { get; set; }
        /// <summary>검사방법</summary>
        [Column("CHECK_PROV")] public string CheckProv { get; set; }
        /// <summary>규격</summary>
        [Column("CHECK_STAND")] public string CheckStand { get; set; }
        /// <summary>기종(LH, RH)</summary>
        [Column("LHRH_TYPE")] public string LhRhType { get; set; }
        /// <summary>상한</summary>
        [Column("UP_QUAD")] public Nullable<double> UpQuad { get; set; }
        /// <summary>하한</summary>
        [Column("DOWN_QUAD")] public Nullable<double> DownQuad { get; set; }
        /// <summary>메모</summary>
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>사용 여부</summary>
        [Column("USE_YN")] public string UseYn { get; set; }
        /// <summary>임시1</summary>
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }

        public string ChaeckFlag
        {
            get
            {
                string val1 = "";
                string val2 = "";
                string val3 = "";
                string val4 = "";
                string val5 = "";
                string val6 = "";
                string val7 = "";
                string val8 = "";

                try
                {
                    val1 = X1 == null ? "" : X1.ToString();
                    val2 = X2 == null ? "" : X2.ToString();
                    val3 = X3 == null ? "" : X3.ToString();
                    val4 = X4 == null ? "" : X4.ToString();
                    val5 = X5 == null ? "" : X5.ToString();
                    val6 = X6 == null ? "" : X6.ToString();
                    val7 = X7 == null ? "" : X7.ToString();
                    val8 = X8 == null ? "" : X8.ToString();
                }
                catch { }
                int Ncnt = 0;
                int Ocnt = 0;

                if (val1 != "")
                {
                    string Prov = CheckProv.ToString();
                    if (Prov == "QT1")//육안
                    {
                        if (val1 == "NG")
                        {
                            Ncnt++;
                        }
                        else { Ocnt++; }

                    }
                    else
                    {

                        Decimal dval = Convert.ToDecimal(val1);
                        Decimal std = Convert.ToDecimal(CheckStand);
                        Decimal stdup = Convert.ToDecimal(UpQuad);
                        Decimal stddown = Convert.ToDecimal(DownQuad);

                        if (dval >= (std - stddown) && dval <= (std + stdup))
                        { Ocnt++; }
                        else { Ncnt++; }
                    }
                }

                if (val2 != "")
                {
                    string Prov = CheckProv.ToString();
                    if (Prov == "QT1")//육안
                    {
                        if (val2 == "NG")
                        {
                            Ncnt++;
                        }
                        else { Ocnt++; }

                    }
                    else
                    {

                        Decimal dval = Convert.ToDecimal(val2);
                        Decimal std = Convert.ToDecimal(CheckStand);
                        Decimal stdup = Convert.ToDecimal(UpQuad);
                        Decimal stddown = Convert.ToDecimal(DownQuad);

                        if (dval >= (std - stddown) && dval <= (std + stdup))
                        { Ocnt++; }
                        else { Ncnt++; }
                    }
                }
                if (val3 != "")
                {
                    string Prov = CheckProv.ToString();
                    if (Prov == "QT1")//육안
                    {
                        if (val3 == "NG")
                        {
                            Ncnt++;
                        }
                        else { Ocnt++; }

                    }
                    else
                    {

                        Decimal dval = Convert.ToDecimal(val3);
                        Decimal std = Convert.ToDecimal(CheckStand);
                        Decimal stdup = Convert.ToDecimal(UpQuad);
                        Decimal stddown = Convert.ToDecimal(DownQuad);

                        if (dval >= (std - stddown) && dval <= (std + stdup))
                        { Ocnt++; }
                        else { Ncnt++; }
                    }
                }
                if (val4 != "")
                {
                    string Prov = CheckProv.ToString();
                    if (Prov == "QT1")//육안
                    {
                        if (val4 == "NG")
                        {
                            Ncnt++;
                        }
                        else { Ocnt++; }

                    }
                    else
                    {

                        Decimal dval = Convert.ToDecimal(val4);
                        Decimal std = Convert.ToDecimal(CheckStand);
                        Decimal stdup = Convert.ToDecimal(UpQuad);
                        Decimal stddown = Convert.ToDecimal(DownQuad);

                        if (dval >= (std - stddown) && dval <= (std + stdup))
                        { Ocnt++; }
                        else { Ncnt++; }
                    }
                }
                if (val5 != "")
                {
                    string Prov = CheckProv.ToString();
                    if (Prov == "QT1")//육안
                    {
                        if (val5 == "NG")
                        {
                            Ncnt++;
                        }
                        else { Ocnt++; }

                    }
                    else
                    {

                        Decimal dval = Convert.ToDecimal(val5);
                        Decimal std = Convert.ToDecimal(CheckStand);
                        Decimal stdup = Convert.ToDecimal(UpQuad);
                        Decimal stddown = Convert.ToDecimal(DownQuad);

                        if (dval >= (std - stddown) && dval <= (std + stdup))
                        { Ocnt++; }
                        else { Ncnt++; }
                    }
                }
                if (val6 != "")
                {
                    string Prov = CheckProv.ToString();
                    if (Prov == "QT1")//육안
                    {
                        if (val6 == "NG")
                        {
                            Ncnt++;
                        }
                        else { Ocnt++; }

                    }
                    else
                    {

                        Decimal dval = Convert.ToDecimal(val6);
                        Decimal std = Convert.ToDecimal(CheckStand);
                        Decimal stdup = Convert.ToDecimal(UpQuad);
                        Decimal stddown = Convert.ToDecimal(DownQuad);

                        if (dval >= (std - stddown) && dval <= (std + stdup))
                        { Ocnt++; }
                        else { Ncnt++; }
                    }
                }
                if (val7 != "")
                {
                    string Prov = CheckProv.ToString();
                    if (Prov == "QT1")//육안
                    {
                        if (val7 == "NG")
                        {
                            Ncnt++;
                        }
                        else { Ocnt++; }

                    }
                    else
                    {

                        Decimal dval = Convert.ToDecimal(val7);
                        Decimal std = Convert.ToDecimal(CheckStand);
                        Decimal stdup = Convert.ToDecimal(UpQuad);
                        Decimal stddown = Convert.ToDecimal(DownQuad);

                        if (dval >= (std - stddown) && dval <= (std + stdup))
                        { Ocnt++; }
                        else { Ncnt++; }
                    }
                }
                if (val8 != "")
                {
                    string Prov = CheckProv.ToString();
                    if (Prov == "QT1")//육안
                    {
                        if (val8 == "NG")
                        {
                            Ncnt++;
                        }
                        else { Ocnt++; }

                    }
                    else
                    {

                        Decimal dval = Convert.ToDecimal(val8);
                        Decimal std = Convert.ToDecimal(CheckStand);
                        Decimal stdup = Convert.ToDecimal(UpQuad);
                        Decimal stddown = Convert.ToDecimal(DownQuad);

                        if (dval >= (std - stddown) && dval <= (std + stdup))
                        { Ocnt++; }
                        else { Ncnt++; }
                    }
                }

                if (Ncnt == 0 && Ocnt == 0) { return ""; }
                else if (Ncnt >= 1) { return "NG"; }
                else { return "OK"; }


            }
        }

        [NotMapped]
        public string X1 { get; set; } //측정치
        [NotMapped]
        public string X2 { get; set; }
        [NotMapped]
        public string X3 { get; set; }
        [NotMapped]
        public string X4 { get; set; }
        [NotMapped]
        public string X5 { get; set; }
        [NotMapped]
        public string X6 { get; set; }
        [NotMapped]
        public string X7 { get; set; }
        [NotMapped]
        public string X8 { get; set; }

    }
}