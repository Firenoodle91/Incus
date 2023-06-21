using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20210604 오세완 차장
    /// 금형일상점검 조회 프로시저 겍체
    /// </summary>
    public class TEMP_MOLDCHECK_DAILY
    {
        public TEMP_MOLDCHECK_DAILY()
        {

        }

        /// <summary>
        /// 20210604 오세완 차장
        /// 금형관리코드
        /// </summary>
        public string MoldMcode { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점검순번
        /// </summary>
        public int CheckSeq { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점검부위사진 ftp경로
        /// </summary>
        public string CheckpartPicUrl { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점검위치
        /// </summary>
        public string CheckPosition { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점검항목
        /// </summary>
        public string CheckList { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점검방법
        /// </summary>
        public string CheckWay { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점검주기
        /// </summary>
        public string CheckCycle { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점김기준일
        /// </summary>
        public string CheckStandardDate { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 관리기준
        /// </summary>
        public string ManagementStandard { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 메모
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점검자
        /// </summary>
        public string CheckId { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점검일
        /// </summary>
        public DateTime? CheckDate { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점검값
        /// </summary>
        public string CheckValue { get; set; }

        /// <summary>
        /// 20210604 오세완 차장
        /// 점검파트 사진경로
        /// </summary>
        public string PicUrl { get; set; }
    }
}
