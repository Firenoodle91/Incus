using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.View.PopupFactory
{
    enum ProductionPopupView
    {

        //System
        Module,
        User,
        Screen,
        UserGroupSelectList,
        Menu,
        UserGroup,
        UserSelectList,
        CodeSql,
        LicenseKeyEdit,
        MenuOpenLog,
        UserDepartmentSelect,
        //system
        PFSTD1100,
        PFSTD1200,
        PFSTD1400,//거래처추가팝업
        PFSTD1500,
        PFStd2000,//설비목록
        PFStd4M001,//4M관리대장

        PFMEA1000,
        PFMEA1200,
        PFMEA1400,
        PFQCT2200,
        PFORD1200, //박스당 개수 출력팝업


        PFORD1000,
        PFQCT1700,

        SELECTSTD1100,
        SELECTPUR1100,
        SELECTPUR1200,
        SELECTPUR1700,
        SELECTPUR1800M,
        SELECTPUR1800D,
        SELECTPUR2100, //외주가공품발주 마스터 SELECT 팝업
        SELECTPUR2101, //외주가공품발주 디테일 SELECT 팝업
        SELECTMPS1401LIST,
        SELECTORD1200,
        SELECTORD1201,
        SELECTINSPLIST,
        SELECTQCCOPY,
        SELECTOUTLIST,
        SELECT_MACHINE_CHECK_COPY,
    }
}
