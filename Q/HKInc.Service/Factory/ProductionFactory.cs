using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;

namespace HKInc.Service.Factory
{ 

    public class ProductionFactory
    {
        private static Dictionary<string, Func<object>> DomainList = new Dictionary<string, Func<object>>()
        {
            //{"TN_QCREPORTMAPPING_MST", () => new ProductionService<TN_QCREPORTMAPPING_MST>()},
            //{"TN_QCREPORTMAPPING_DTL", () => new ProductionService<TN_QCREPORTMAPPING_DTL>()},
            {"TN_STD1000", () => new ProductionService<TN_STD1000>()},
            {"TN_STD1100", () => new ProductionService<TN_STD1100>()},
            {"TN_STD1101", () => new ProductionService<TN_STD1101>()},
            {"TN_STD1120", () => new ProductionService<TN_STD1120>()}, // 20220407 오세완 차장 단가이력관리 거래처 목록 추가
            {"TN_STD1121", () => new ProductionService<TN_STD1121>()}, // 20220407 오세완 차장 단가이력관리 단가 목록 추가
            {"TN_OUT1101", () => new ProductionService<TN_OUT1101>()},
            {"TN_STD1200", () => new ProductionService<TN_STD1200>()},
            {"TN_STD1400", () => new ProductionService<TN_STD1400>()},
            {"TN_STD1600", () => new ProductionService<TN_STD1600>()},
            {"TN_STD1800", () => new ProductionService<TN_STD1800>()},
            {"TN_STD4M001", () => new ProductionService<TN_STD4M001>()},
            {"TN_STD1500", () => new ProductionService<TN_STD1500>()},
            {"TN_ORD1000", () => new ProductionService<TN_ORD1000>()},
            {"TN_ORD1002", () => new ProductionService<TN_ORD1002>()},
            {"TN_ORD1100", () => new ProductionService<TN_ORD1100>()},
            {"VI_ORD1100", () => new ProductionService<VI_ORD1100>()},
            {"TN_ORD1200", () => new ProductionService<TN_ORD1200>()},
            {"TN_ORD1201", () => new ProductionService<TN_ORD1201>()},
            {"TN_ORD1600", () => new ProductionService<TN_ORD1600>()},
            {"TN_ORD1601", () => new ProductionService<TN_ORD1601>()},
            {"TN_ORD1700", () => new ProductionService<TN_ORD1700>()},
            {"TN_ORD1701", () => new ProductionService<TN_ORD1701>()},
            {"TN_ORD2000", () => new ProductionService<TN_ORD2000>()},
            {"TN_ORD2001", () => new ProductionService<TN_ORD2001>()},

            {"TN_MPS1000", () => new ProductionService<TN_MPS1000>()},
            {"TN_MPS1001", () => new ProductionService<TN_MPS1001>()},
            {"TN_MPS1010", () => new ProductionService<TN_MPS1010>()}, //. 20220321 오세완 차장 표준별공정타입관리 테이블 추가 
            {"TN_MPS1011", () => new ProductionService<TN_MPS1011>()}, //. 20220321 오세완 차장 표준별공정타입관리 상세 테이블 추가 
            {"TN_MPS1300", () => new ProductionService<TN_MPS1300>()},
            {"TN_MPS1400", () => new ProductionService<TN_MPS1400>()},
            {"TN_MPS1401", () => new ProductionService<TN_MPS1401>()},
            {"TN_MPS1404", () => new ProductionService<TN_MPS1404>()},
            {"TN_MPS1405", () => new ProductionService<TN_MPS1405>()},
            {"TN_MPS1600", () => new ProductionService<TN_MPS1600>()},
            {"VI_MPSPLAN_LIST", () => new ProductionService<VI_MPSPLAN_LIST>()},
            {"VI_MPS1401V", () => new ProductionService<VI_MPS1401V>()},
            {"VI_MPS1405V", () => new ProductionService<VI_MPS1405V>()},
            {"TN_UPH1000", () => new ProductionService<TN_UPH1000>()},

            {"TN_PUR1100", () => new ProductionService<TN_PUR1100>()},
            {"TN_PUR1200", () => new ProductionService<TN_PUR1200>()},
            {"TN_PUR1300", () => new ProductionService<TN_PUR1300>()},
            {"TN_PUR1301", () => new ProductionService<TN_PUR1301>()},
            {"TN_PUR1500", () => new ProductionService<TN_PUR1500>()},
            {"TN_PUR1501", () => new ProductionService<TN_PUR1501>()},
            {"TN_PUR1600", () => new ProductionService<TN_PUR1600>()},
            {"TN_PUR1700", () => new ProductionService<TN_PUR1700>()},
            {"TN_PUR1800", () => new ProductionService<TN_PUR1800>()},
            {"TN_PUR1801", () => new ProductionService<TN_PUR1801>()},
            {"TN_PUR2000", () => new ProductionService<TN_PUR2000>()}, // 20220415 오세완 차장 자재재고조정 테이블 추가 
            {"TN_ALAM002", () => new ProductionService<TN_ALAM002>()},

            {"VI_OUTLIST", () => new ProductionService<VI_OUTLIST>()},
            {"VI_RESULT_QTY", () => new ProductionService<VI_RESULT_QTY>()},
            {"VI_PURSTOCK", () => new ProductionService<VI_PURSTOCK>()},
            {"VI_PURINOUT", () => new ProductionService<VI_PURINOUT>()},
            {"VI_PURINOUT_V2", () => new ProductionService<VI_PURINOUT_V2>()}, // 20220414 오세완 차장 자재재고관리 입출고목록 view추가 
            {"VI_PURINOUT_LOT", () => new ProductionService<VI_PURINOUT_LOT>()}, // 20220414 오세완 차장 자재재고관리(lotno) 상세목록 view추가 
            {"VI_PURSTOCK_LOT", () => new ProductionService<VI_PURSTOCK_LOT>()},
            {"VI_PO_LIST_PRT", () => new ProductionService<VI_PO_LIST_PRT>()},
            {"VI_PUR1600_LIST", () => new ProductionService<VI_PUR1600_LIST>()},
            {"VI_PUR1900V1", () => new ProductionService<VI_PUR1900V1>()},
            {"VI_PUR1900V2", () => new ProductionService<VI_PUR1900V2>()},
            {"VI_SRC_STOCK_SUM", () => new ProductionService<VI_SRC_STOCK_SUM>()},
            {"VI_SRC_USE", () => new ProductionService<VI_SRC_USE>()},
            {"VI_PUR_LIST",()=> new ProductionService<VI_PUR_LIST>() },
            {"VI_PROCESSING_QTY",()=> new ProductionService<VI_PROCESSING_QTY>() },

            {"TN_MEA1000", () => new ProductionService<TN_MEA1000>()},
            {"TN_MEA1001", () => new ProductionService<TN_MEA1001>()},  // 2022-04-19 김진우    설비이력목록 추가
            {"TN_MEA1002", () => new ProductionService<TN_MEA1002>()}, // 20220313 오세완 차장 설비일상점검관리 기준 테이블 추가 
            {"TN_MEA1003", () => new ProductionService<TN_MEA1003>()}, // 20220313 오세완 차장 설비일상점검관리 기준 테이블 추가 
            {"TN_MEA1004", () => new ProductionService<TN_MEA1004>()}, // 20220404 오세완 차장 비가동 관리 테이블 추가 
            {"TN_MEA1100", () => new ProductionService<TN_MEA1100>()},
            {"TN_MEA1200", () => new ProductionService<TN_MEA1200>()},
            {"TN_MEA1300", () => new ProductionService<TN_MEA1300>()},
            {"TN_MEA1600", () => new ProductionService<TN_MEA1600>()},
            {"TN_MEA1601", () => new ProductionService<TN_MEA1601>()},
            {"TN_MEA1700", () => new ProductionService<TN_MEA1700>()},
            {"TN_MOLD001", () => new ProductionService<TN_MOLD001>()},
            {"TN_MOLD002", () => new ProductionService<TN_MOLD002>()},

            {"TN_INDOO001", () => new ProductionService<TN_INDOO001>()},
            {"TN_INDOO002", () => new ProductionService<TN_INDOO002>()},
            {"TN_QCT1000", () => new ProductionService<TN_QCT1000>()},
            //{"TN_QCT1001", () => new ProductionService<TN_QCT1001>()},             // 2022-03-07 김진우 주석
            {"TN_QCT1200", () => new ProductionService<TN_QCT1200>()},
            {"TN_QCT2200", () => new ProductionService<TN_QCT2200>()},
            {"TN_QCT1201", () => new ProductionService<TN_QCT1201>()},
            {"TN_QCT1500", () => new ProductionService<TN_QCT1500>()},
            {"TN_QCT1600", () => new ProductionService<TN_QCT1600>()},              // 안돈 추가        2022-07-15 김진우
            {"TN_QCT1700", () => new ProductionService<TN_QCT1700>()},
            {"VI_QCT1500_LIST", () => new ProductionService<VI_QCT1500_LIST>()},
            {"VI_INSPLIST", () => new ProductionService<VI_INSPLIST>()},
            {"VI_LOTTRACKING", () => new ProductionService<VI_LOTTRACKING>()},

            {"VI_MEA1003_MASTER_LIST", () => new ProductionService<VI_MEA1003_MASTER_LIST>()}, // 20220313 오세완 차장 설비알상점검관리 조회용 추가 
            {"VI_MPS1401LIST", () => new ProductionService<VI_MPS1401LIST>()},
            {"VI_MACHINE_PROCESS_QTY" ,() => new ProductionService<VI_MACHINE_PROCESS_QTY>()},

            //{"TN_BAN1000", () => new ProductionService<TN_BAN1000>()},                   // 2022-02-23 김진우 주석처리
            //{"TN_BAN1001", () => new ProductionService<TN_BAN1001>()},                   // 2022-02-23 김진우 주석처리
            //{"TN_BAN1200", () => new ProductionService<TN_BAN1200>()},                   // 2022-02-23 김진우 주석처리
            //{"TN_BAN1201", () => new ProductionService<TN_BAN1201>()},                   // 2022-02-23 김진우 주석처리

            {"VI_PRODQTYMSTLOT", () => new ProductionService<VI_PRODQTYMSTLOT>()},
            {"VI_PRODQTY_MST", () => new ProductionService<VI_PRODQTY_MST>()},
            {"VI_PRODQTY_DTL", () => new ProductionService<VI_PRODQTY_DTL>()},
            {"VI_PRODQTY_MON", () => new ProductionService<VI_PRODQTY_MON>()},
            {"VI_PRODQTY_DAY", () => new ProductionService<VI_PRODQTY_DAY>()},

            //{"VI_BAN_QTYM", () => new ProductionService<VI_BAN_QTYM>()},          // 2022-02-23 김진우 주석처리
            //{"VI_BAN_QTYSUM", () => new ProductionService<VI_BAN_QTYSUM>()},      // 2022-02-23 김진우 주석처리

            //{"VI_BANTOCK_MST_Lot", () => new ProductionService<VI_BANTOCK_MST_Lot>()},        // 2022-02-23 김진우 주석처리

            {"TN_WMS1000", () => new ProductionService<TN_WMS1000>()},
            {"TN_WMS2000", () => new ProductionService<TN_WMS2000>()},

            {"VI_WHPOSITION_QTY_D1", () => new ProductionService<VI_WHPOSITION_QTY_D1>()},      // 2021-11-04 김진우 주임 수정
            {"VI_WHPOSITION_QTY_D2", () => new ProductionService<VI_WHPOSITION_QTY_D2>()},      // 2021-11-04 김진우 주임 수정
         
            {"CultureField", () => new ProductionService<CultureField>()},
            {"Holiday", () => new ProductionService<Holiday>() }, // 20220321 오세완 차장 생산관리 쪽에 휴일관리가 있어서 이쪽에 추가 처리 

            {"TN_LOT_MST_V2", () => new ProductionService<TN_LOT_MST_V2>() }, // 20220425 오세완 차장 고도화로 인한 로트추적마스터 테이블 신규 추가 
            {"TN_LOT_DTL", () => new ProductionService<TN_LOT_DTL>() }, // 20220425 오세완 차장 고도화로 인한 로트추적디테일 테이블 신규 추가 
        };
        public static object GetDomainService(string domainName)
        {
            return DomainList[domainName]();
        }
    }
}


