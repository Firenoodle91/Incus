using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain.VIEW;

namespace HKInc.Service.Factory
{ 

    public class ProductionFactory
    {
        private static Dictionary<string, Func<object>> DomainList = new Dictionary<string, Func<object>>()
        {

            #region STD
            {"TN_STD1000", () => new ProductionService<TN_STD1000>()},
            {"TN_STD1100", () => new ProductionService<TN_STD1100>()},
            {"VI_STD1100", () => new ProductionService<VI_STD1100>()},
            {"TN_STD1101", () => new ProductionService<TN_STD1101>()},
            {"TN_STD1102", () => new ProductionService<TN_STD1102>()},
            {"TN_STD1103", () => new ProductionService<TN_STD1103>()},
            {"TN_STD1104", () => new ProductionService<TN_STD1104>()},
            {"TN_STD1200", () => new ProductionService<TN_STD1200>()},
            {"TN_STD1300", () => new ProductionService<TN_STD1300>()},
            {"TN_STD1320", () => new ProductionService<TN_STD1320>()}, // 20211018 오세완 차장 bom type master 
            {"TN_STD1321", () => new ProductionService<TN_STD1321>()}, // 20211018 오세완 차장 bom type detail 
            {"TN_STD1400", () => new ProductionService<TN_STD1400>()},
            {"TN_STD1401", () => new ProductionService<TN_STD1401>()},
            {"TN_STD4M001", () => new ProductionService<TN_STD4M001>()},
            #endregion

            #region ORD
            {"TN_ORD1000", () => new ProductionService<TN_ORD1000>()},
            {"TN_ORD1001", () => new ProductionService<TN_ORD1001>()},
            {"TN_ORD1100", () => new ProductionService<TN_ORD1100>()},
            {"TN_ORD1101", () => new ProductionService<TN_ORD1101>()},
            {"TN_ORD1102", () => new ProductionService<TN_ORD1102>()},
            {"TN_ORD1200", () => new ProductionService<TN_ORD1200>()},
            {"TN_ORD1201", () => new ProductionService<TN_ORD1201>()},
            {"TN_ORD1300", () => new ProductionService<TN_ORD1300>()},
            {"TN_ORD1301", () => new ProductionService<TN_ORD1301>()},
            {"TN_ORD1400", () => new ProductionService<TN_ORD1400>()},
            {"TN_ORD1401", () => new ProductionService<TN_ORD1401>()},
            {"TN_ORD1500", () => new ProductionService<TN_ORD1500>()},
            
            {"TN_PROD_DEAD_MST", () => new ProductionService<TN_PROD_DEAD_MST>()},
            {"TN_PROD_DEAD_DTL", () => new ProductionService<TN_PROD_DEAD_DTL>()},         
            {"TN_PROD_DEAD_HISTORY", () => new ProductionService<TN_PROD_DEAD_HISTORY>()},
            {"TN_BAN_DEAD_MST", () => new ProductionService<TN_BAN_DEAD_MST>()},
            {"TN_BAN_DEAD_DTL", () => new ProductionService<TN_BAN_DEAD_DTL>()},
            {"TN_BAN_DEAD_HISTORY", () => new ProductionService<TN_BAN_DEAD_HISTORY>()},
            {"TN_MAT_DEAD_MST", () => new ProductionService<TN_MAT_DEAD_MST>()},
            {"TN_MAT_DEAD_DTL", () => new ProductionService<TN_MAT_DEAD_DTL>()},
            {"TN_MAT_DEAD_HISTORY", () => new ProductionService<TN_MAT_DEAD_HISTORY>()},
            #endregion

            #region PUR
            {"TN_PUR1100", () => new ProductionService<TN_PUR1100>()},
            {"TN_PUR1101", () => new ProductionService<TN_PUR1101>()},
            {"TN_PUR1200", () => new ProductionService<TN_PUR1200>()},
            {"TN_PUR1201", () => new ProductionService<TN_PUR1201>()},
            {"TN_PUR1300", () => new ProductionService<TN_PUR1300>()},
            {"TN_PUR1301", () => new ProductionService<TN_PUR1301>()},
            {"TN_PUR1302", () => new ProductionService<TN_PUR1302>()},
            {"TN_PUR1303", () => new ProductionService<TN_PUR1303>()},           
            {"TN_PUR1400", () => new ProductionService<TN_PUR1400>()},
            {"TN_PUR1401", () => new ProductionService<TN_PUR1401>()},
            {"TN_PUR1500", () => new ProductionService<TN_PUR1500>()},
            {"TN_PUR1501", () => new ProductionService<TN_PUR1501>()},
            {"TN_PUR1600", () => new ProductionService<TN_PUR1600>()},
            {"TN_PUR1700", () => new ProductionService<TN_PUR1700>()},
            {"TN_PUR1200_SCM", () => new ProductionService<TN_PUR1200_SCM>()},
            {"TN_PUR1201_SCM", () => new ProductionService<TN_PUR1201_SCM>()},
            {"TN_PUR1304", () => new ProductionService<TN_PUR1304>()},
            {"TN_PUR1305", () => new ProductionService<TN_PUR1305>()},
            {"TN_PUR2000", () => new ProductionService<TN_PUR2000>()},


            #endregion

            #region MPS
            {"TN_MPS1000", () => new ProductionService<TN_MPS1000>()},
            {"TN_MPS1001", () => new ProductionService<TN_MPS1001>()},
            {"TN_MPS1002", () => new ProductionService<TN_MPS1002>()},
            {"TN_MPS1100", () => new ProductionService<TN_MPS1100>()},
            {"TN_MPS1200", () => new ProductionService<TN_MPS1200>()},
            {"TN_MPS1201", () => new ProductionService<TN_MPS1201>()},
            {"TN_MPS1202", () => new ProductionService<TN_MPS1202>()},
            {"TN_MPS1203", () => new ProductionService<TN_MPS1203>()},
            {"TN_MPS1204", () => new ProductionService<TN_MPS1204>()},
            {"TN_MPS1205", () => new ProductionService<TN_MPS1205>()},
            {"TN_MPS1206", () => new ProductionService<TN_MPS1206>()},
            {"TN_MPS1207", () => new ProductionService<TN_MPS1207>()},
            {"TN_MPS1300", () => new ProductionService<TN_MPS1300>()},
            {"TN_MPS1400", () => new ProductionService<TN_MPS1400>()},  // 20210610 오세완 차장 설비 비가동 정보 조회 및 관리 테이블 추가
            {"TN_SRC1000", () => new ProductionService<TN_SRC1000>()},
            {"TN_SRC1001", () => new ProductionService<TN_SRC1001>()},
            {"TN_LOT_MST", () => new ProductionService<TN_LOT_MST>()},
            {"TN_LOT_DTL", () => new ProductionService<TN_LOT_DTL>()},            
            {"TN_ITEM_MOVE", () => new ProductionService<TN_ITEM_MOVE>()},
            
            #endregion

            #region BAN
            {"TN_BAN1000", () => new ProductionService<TN_BAN1000>()},
            {"TN_BAN1001", () => new ProductionService<TN_BAN1001>()},
            {"TN_BAN1100", () => new ProductionService<TN_BAN1100>()},
            {"TN_BAN1101", () => new ProductionService<TN_BAN1101>()},
            {"TN_BAN1102", () => new ProductionService<TN_BAN1102>()},
            #endregion

            #region QCT
            {"TN_QCT1000", () => new ProductionService<TN_QCT1000>()},
            {"TN_QCT1001", () => new ProductionService<TN_QCT1001>()},
            {"TN_QCT1100", () => new ProductionService<TN_QCT1100>()},
            {"TN_QCT1101", () => new ProductionService<TN_QCT1101>()},
            {"TN_QCT1400", () => new ProductionService<TN_QCT1400>()},
            {"TN_QCT1300", () => new ProductionService<TN_QCT1300>()},
            {"TN_QCT1500", () => new ProductionService<TN_QCT1500>()},
            {"TN_QCT1501", () => new ProductionService<TN_QCT1501>()},
            {"TN_QCT1600", () => new ProductionService<TN_QCT1600>()},
            {"TN_QCT1700", () => new ProductionService<TN_QCT1700>()},
            {"TN_QCT1800", () => new ProductionService<TN_QCT1800>()},
            {"TN_QCT1900", () => new ProductionService<TN_QCT1900>()}, // 20210917 오세완 차장 출하검사 마스터 추가
            {"TN_QCT1901", () => new ProductionService<TN_QCT1901>()}, // 20210917 오세완 차장 출하검사 마스터 추가
        //    {"TN_QCT1101_SCM", () => new ProductionService<TN_QCT1101_SCM>()},
            
            #endregion

            #region MEA
            {"TN_MEA1000", () => new ProductionService<TN_MEA1000>()},
            {"TN_MEA1001", () => new ProductionService<TN_MEA1001>()},
            {"TN_MEA1002", () => new ProductionService<TN_MEA1002>()},
            {"TN_MEA1003", () => new ProductionService<TN_MEA1003>()},
            {"TN_MEA1004", () => new ProductionService<TN_MEA1004>()},
            {"TN_MEA1100", () => new ProductionService<TN_MEA1100>()},
            {"TN_MEA1101", () => new ProductionService<TN_MEA1101>()},
            {"TN_MEA1201", () => new ProductionService<TN_MEA1201>()},
            {"TN_UPH1000", () => new ProductionService<TN_UPH1000>()},
            {"TN_MEA1400", () => new ProductionService<TN_MEA1400>()},
            {"TN_MEA1600", () => new ProductionService<TN_MEA1600>()}, // 20210715 오세완 차장 추가, plc 통신 상태 현황 테이블
            {"TN_MEA1300", () => new ProductionService<TN_MEA1300>()},  // 2021-05-31 김진우 주임 추가
            {"TN_MEA1301", () => new ProductionService<TN_MEA1301>()},  // 2021-05-31 김진우 주임 추가
            {"TN_MEA1302", () => new ProductionService<TN_MEA1302>()},  // 2021-05-31 김진우 주임 추가
            {"TN_MEA1305", () => new ProductionService<TN_MEA1305>()},  // 2021-06-14 김진우 주임 추가
            {"TN_MEQ1300", () => new ProductionService<TN_MEQ1300>()},  // 2021-06-03 김진우 주임 추가
            {"TN_MEQ1301", () => new ProductionService<TN_MEQ1301>()},  // 2021-06-03 김진우 주임 추가
            {"TN_CHF2000", () => new ProductionService<TN_CHF2000>()},
            {"TN_CHF2001", () => new ProductionService<TN_CHF2001>()},
            {"TN_CHF2002", () => new ProductionService<TN_CHF2002>()},
            {"TN_CHF2003", () => new ProductionService<TN_CHF2003>()},
            {"TN_WEL2000", () => new ProductionService<TN_WEL2000>()},  // 2021-06-08 김진우 주임 추가
            {"TN_WEL2001", () => new ProductionService<TN_WEL2001>()},  // 2021-06-08 김진우 주임 추가
            {"TN_WEL2002", () => new ProductionService<TN_WEL2002>()},  // 2021-06-08 김진우 주임 추가
            {"TN_WEL2003", () => new ProductionService<TN_WEL2003>()},  // 2021-06-08 김진우 주임 추가
            #endregion

            #region WMS
            {"TN_WMS1000", () => new ProductionService<TN_WMS1000>()},
            {"TN_WMS2000", () => new ProductionService<TN_WMS2000>()},
            #endregion

            #region TRY
            {"TN_TRY1000", () => new ProductionService<TN_TRY1000>()},
            #endregion

            #region TOOL
            {"TN_TOOL1001", () => new ProductionService<TN_TOOL1001>()},
            {"TN_TOOL1002", () => new ProductionService<TN_TOOL1002>()},
            {"TN_TOOL1003", () => new ProductionService<TN_TOOL1003>()},
            #endregion

            #region DEV
            {"TN_DEV1000", () => new ProductionService<TN_DEV1000>()},
            #endregion

            #region VIEW
            {"VI_INSP_IN_OBJECT", () => new ProductionService<VI_INSP_IN_OBJECT>()},
            {"VI_INSP_IN_OBJECT_V2", () => new ProductionService<VI_INSP_IN_OBJECT_V2>()},
            {"VI_INSP_IN_OBJECT_SCM", () => new ProductionService<VI_INSP_IN_OBJECT_SCM>()},
            {"VI_INSP_FME_OBJECT", () => new ProductionService<VI_INSP_FME_OBJECT>()},
            {"VI_INSP_FREQNTLY_OBJECT", () => new ProductionService<VI_INSP_FREQNTLY_OBJECT>()},
            {"VI_INSP_PROCESS_OBJECT", () => new ProductionService<VI_INSP_PROCESS_OBJECT>()},
            {"VI_INSP_SETTING_OBJECT", () => new ProductionService<VI_INSP_SETTING_OBJECT>()},
            {"VI_INSP_SHIPMENT_OBJECT", () => new ProductionService<VI_INSP_SHIPMENT_OBJECT>()},
            {"VI_PUR_STOCK_IN_LOT_NO", () => new ProductionService<VI_PUR_STOCK_IN_LOT_NO>()},
            {"VI_PUR_STOCK_ITEM", () => new ProductionService<VI_PUR_STOCK_ITEM>()},
            {"VI_WH_STOCK_QTY", () => new ProductionService<VI_WH_STOCK_QTY>()},
            {"VI_WH_POSITON_QTY", () => new ProductionService<VI_WH_POSITON_QTY>()},
            {"VI_RETURN_OBJECT", () => new ProductionService<VI_RETURN_OBJECT>()},
            {"VI_LOT_TRACKING", () => new ProductionService<VI_LOT_TRACKING>()},
            {"VI_LOT_TRACKING_V2", () => new ProductionService<VI_LOT_TRACKING_V2>()},
            {"VI_QCT1300_LIST", () => new ProductionService<VI_QCT1300_LIST>()},
            {"VI_PROD_STOCK_ITEM", () => new ProductionService<VI_PROD_STOCK_ITEM>()},
            {"VI_PROD_STOCK_PRODUCT_LOT_NO", () => new ProductionService<VI_PROD_STOCK_PRODUCT_LOT_NO>()},
            {"VI_SRC_STOCK_SUM", () => new ProductionService<VI_SRC_STOCK_SUM>()},
            {"VI_SRC_USE", () => new ProductionService<VI_SRC_USE>()},
            {"VI_SRC_STOCK", () => new ProductionService<VI_SRC_STOCK>()},
            {"VI_OUT_PROC_STOCK_MASTER", () => new ProductionService<VI_OUT_PROC_STOCK_MASTER>()},
            {"VI_MEA1003_MASTER_LIST", () => new ProductionService<VI_MEA1003_MASTER_LIST>()},
            {"VI_BAN_STOCK_IN_LOT_NO", () => new ProductionService<VI_BAN_STOCK_IN_LOT_NO>()},
            {"VI_BAN_STOCK_PRODUCT_LOT_NO", () => new ProductionService<VI_BAN_STOCK_PRODUCT_LOT_NO>()},
            {"VI_BAN_STOCK_ITEM", () => new ProductionService<VI_BAN_STOCK_ITEM>()},
            {"VI_BAN_STOCK_ITEM_V2", () => new ProductionService<VI_BAN_STOCK_ITEM_V2>()}, // 20210616 오세완 차장 대영스타일 반제품 재고 뷰 추가
            {"VI_XFORD1102_MASTER_VIEW", () => new ProductionService<VI_XFORD1102_MASTER_VIEW>()},
            {"VI_MPS1800_LIST", () => new ProductionService<VI_MPS1800_LIST>()},
            {"VI_XRREP6000_LIST", () => new ProductionService<VI_XRREP6000_LIST>()},
            {"VI_TOOL1002_LIST", () => new ProductionService<VI_TOOL1002_LIST>()},
            {"VI_BUSINESS_MANAGEMENT_USER", () => new ProductionService<VI_BUSINESS_MANAGEMENT_USER>()},
            {"VI_DISPOSAL_OBJECT", () => new ProductionService<VI_DISPOSAL_OBJECT>()},
            {"VI_MCSRCLOT_STOCK", () => new ProductionService<VI_MCSRCLOT_STOCK>()},
            {"VI_PROD_STOCK_PRODUCT_MM", () => new ProductionService<VI_PROD_STOCK_PRODUCT_MM>()},
            {"VI_POP_WORK_START_BOM_CHECK", () => new ProductionService<VI_POP_WORK_START_BOM_CHECK>()},
            {"VI_PUR_STOCK", () => new ProductionService<VI_PUR_STOCK>()},
            {"VI_STD1100_BOMTREE", () => new ProductionService<VI_STD1100_BOMTREE>() }, // 20210201 오세완 차장 bom관리 품목코드 콤보박스 컨트롤 조회용
            {"VI_PUR_SCM", () => new ProductionService<VI_PUR_SCM>()},
            {"VI_PUR1101_SCM", () => new ProductionService<VI_PUR1101_SCM>()},
            {"VI_PUR1304", () => new ProductionService<VI_PUR1304>()},
            {"VI_PUR1305", () => new ProductionService<VI_PUR1305>()},
            {"VI_MACHINE_MOLD_LIST", () => new ProductionService<VI_MACHINE_MOLD_LIST>()}, // 2021-06-14 김진우 주임 추가
            {"VI_BAN_STOCK_ITEM_MOVE_NO", () => new ProductionService<VI_BAN_STOCK_ITEM_MOVE_NO>() },
             
            #endregion
            #region SCM
            {"SCM_VI_PUR1100_SCM", () => new ProductionService<SCM_VI_PUR1100_SCM>()},
            {"SCM_VI_PUR1101_SCM", () => new ProductionService<SCM_VI_PUR1101_SCM>()},
            {"SCM_VI_PUR1200_SCM", () => new ProductionService<SCM_VI_PUR1200_SCM>()},
            {"SCM_VI_PUR1201_SCM", () => new ProductionService<SCM_VI_PUR1201_SCM>()},
            {"SCM_VI_QCT1100_SCM", () => new ProductionService<SCM_VI_QCT1100_SCM>()},
            {"SCM_VI_QCT1101_SCM", () => new ProductionService<SCM_VI_QCT1101_SCM>()},
            {"SCM_VI_PUR1305", () => new ProductionService<SCM_VI_PUR1305>()},   
            #endregion
            
            #region MOLD
            {"TN_MOLD1100", () => new ProductionService<TN_MOLD1100>()},
            {"TN_MOLD1101", () => new ProductionService<TN_MOLD1101>()},
            {"TN_MOLD1300", () => new ProductionService<TN_MOLD1300>()},
            {"TN_MOLD1400", () => new ProductionService<TN_MOLD1400>()},
            {"TN_MOLD1500", () => new ProductionService<TN_MOLD1500>()}, // 20210604 오세완 차장 금형일상점검 테이블 추가
            {"TN_MOLD1600", () => new ProductionService<TN_MOLD1600>()}, 
            {"TN_MOLD1601", () => new ProductionService<TN_MOLD1601>()}, 
            {"TN_MOLD1602", () => new ProductionService<TN_MOLD1602>()},
            {"TN_MOLD1700", () => new ProductionService<TN_MOLD1700>()},
            {"TN_MOLD1701", () => new ProductionService<TN_MOLD1701>()},
            #endregion
    };

        public static object GetDomainService(string domainName)
        {
            return DomainList[domainName]();
        }
    }
}


