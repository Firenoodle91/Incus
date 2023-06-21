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
            {"TN_STD1000", () => new ProductionService<TN_STD1000>()},
            {"TN_STD1100", () => new ProductionService<TN_STD1100>()},
            {"TN_STD1101", () => new ProductionService<TN_STD1101>()},
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

            {"TN_MPS1000", () => new ProductionService<TN_MPS1000>()},
            {"TN_MPS1001", () => new ProductionService<TN_MPS1001>()},
            {"TN_MPS1300", () => new ProductionService<TN_MPS1300>()},
            {"TN_MPS1400", () => new ProductionService<TN_MPS1400>()},
            {"TN_MPS1401", () => new ProductionService<TN_MPS1401>()},
            {"TN_MPS1402", () => new ProductionService<TN_MPS1402>()},
            {"TN_MPS1404", () => new ProductionService<TN_MPS1404>()},
            {"TN_MPS1600", () => new ProductionService<TN_MPS1600>()},
            
            {"TN_MPS1700", () => new ProductionService<TN_MPS1700>()},
            {"VI_MPSPLAN_LIST", () => new ProductionService<VI_MPSPLAN_LIST>()},
            

            {"TN_PUR1100", () => new ProductionService<TN_PUR1100>()},
            {"TN_PUR1200", () => new ProductionService<TN_PUR1200>()},
            {"TN_PUR1300", () => new ProductionService<TN_PUR1300>()},
            {"TN_PUR1301", () => new ProductionService<TN_PUR1301>()},
            {"TN_PUR1500", () => new ProductionService<TN_PUR1500>()},
            {"TN_PUR1501", () => new ProductionService<TN_PUR1501>()},
            {"TN_PUR1502", () => new ProductionService<TN_PUR1502>()},            
            {"TN_PUR1600", () => new ProductionService<TN_PUR1600>()},
            {"TN_PUR1700", () => new ProductionService<TN_PUR1700>()},
            {"TN_PUR1800", () => new ProductionService<TN_PUR1800>()},
            {"TN_PUR1801", () => new ProductionService<TN_PUR1801>()},
            {"TN_PUR2000", () => new ProductionService<TN_PUR2000>()},
            {"TN_PUR2001", () => new ProductionService<TN_PUR2001>()},

            {"VI_PURSTOCK", () => new ProductionService<VI_PURSTOCK>()},
            {"VI_PURINOUT", () => new ProductionService<VI_PURINOUT>()},
            {"VI_PURSTOCK_LOT", () => new ProductionService<VI_PURSTOCK_LOT>()},
            {"VI_POLISTPRT", () => new ProductionService<VI_POLISTPRT>()},
            {"VI_PUR1600_LIST", () => new ProductionService<VI_PUR1600_LIST>()},
            {"VI_PUR1900V1", () => new ProductionService<VI_PUR1900V1>()},
            {"VI_PUR1900V2", () => new ProductionService<VI_PUR1900V2>()},
            {"VI_SRC_STOCK_SUM", () => new ProductionService<VI_SRC_STOCK_SUM>()},
            {"VI_SRC_USE", () => new ProductionService<VI_SRC_USE>()},


            {"TN_MEA1000", () => new ProductionService<TN_MEA1000>()},
            {"TN_MEA1100", () => new ProductionService<TN_MEA1100>()},
            {"TN_MEA1200", () => new ProductionService<TN_MEA1200>()},
            {"TN_MEA1300", () => new ProductionService<TN_MEA1300>()},
            {"TN_MOLD001", () => new ProductionService<TN_MOLD001>()},
            {"TN_MOLD002", () => new ProductionService<TN_MOLD002>()},

            {"TN_KNIFE001", () => new ProductionService<TN_KNIFE001>()},
            {"TN_KNIFE002", () => new ProductionService<TN_KNIFE002>()},
            {"TN_KNIFE003", () => new ProductionService<TN_KNIFE003>()},

            {"TN_INDOO001", () => new ProductionService<TN_INDOO001>()},
            {"TN_INDOO002", () => new ProductionService<TN_INDOO002>()},
            {"TN_QCT1000", () => new ProductionService<TN_QCT1000>()},
            {"TN_QCT1001", () => new ProductionService<TN_QCT1001>()},
            {"TN_QCT1200", () => new ProductionService<TN_QCT1200>()},
            {"TN_QCT2200", () => new ProductionService<TN_QCT2200>()},
            {"TN_QCT1201", () => new ProductionService<TN_QCT1201>()},
            {"TN_QCT1500", () => new ProductionService<TN_QCT1500>()},
            {"TN_QCT1700", () => new ProductionService<TN_QCT1700>()},
            {"VI_QCT1500_LIST", () => new ProductionService<VI_QCT1500_LIST>()},
            {"VI_INSPLIST", () => new ProductionService<VI_INSPLIST>()},
            {"VI_LOTTRACKING", () => new ProductionService<VI_LOTTRACKING>()},

            {"VI_MPS1401LIST", () => new ProductionService<VI_MPS1401LIST>()},


            {"TN_BAN1000", () => new ProductionService<TN_BAN1000>()},
            {"TN_BAN1001", () => new ProductionService<TN_BAN1001>()},
            {"TN_BAN1200", () => new ProductionService<TN_BAN1200>()},
            {"TN_BAN1201", () => new ProductionService<TN_BAN1201>()},

            {"VI_PRODQTYMSTLOT", () => new ProductionService<VI_PRODQTYMSTLOT>()},
            {"VI_PRODQTY_MST", () => new ProductionService<VI_PRODQTY_MST>()},
            {"VI_PRODQTY_DTL", () => new ProductionService<VI_PRODQTY_DTL>()},
            {"VI_PRODQTY_MON", () => new ProductionService<VI_PRODQTY_MON>()},
            {"VI_PRODQTY_DAY", () => new ProductionService<VI_PRODQTY_DAY>()},

            {"VI_BAN_QTYM", () => new ProductionService<VI_BAN_QTYM>()},
            {"VI_BAN_QTYSUM", () => new ProductionService<VI_BAN_QTYSUM>()},

            {"VI_BANTOCK_MST_Lot", () => new ProductionService<VI_BANTOCK_MST_Lot>()},
           

            {"TN_WMS1000", () => new ProductionService<TN_WMS1000>()},
            {"TN_WMS2000", () => new ProductionService<TN_WMS2000>()},

            {"VI_MPS1700_MASTER", () => new ProductionService<VI_MPS1700_MASTER>()},

            {"TN_LOT_MST", () => new ProductionService<TN_LOT_MST>()},
            {"TN_MPS1405", () => new ProductionService<TN_MPS1405>()},
            {"TN_MPS1406", () => new ProductionService<TN_MPS1406>()},
            {"TN_MPS1407", () => new ProductionService<TN_MPS1407>()},

            {"VI_ADD_QC2000_LIST", () => new ProductionService<VI_ADD_QC2000_LIST>()},
            {"VI_MEA1000_NOT_FILE_LIST", () => new ProductionService<VI_MEA1000_NOT_FILE_LIST>()},
            {"VI_ORD1200_DETAIL_ADD", () => new ProductionService<VI_ORD1200_DETAIL_ADD>()},
            
        };
        public static object GetDomainService(string domainName)
        {
            return DomainList[domainName]();
        }
    }
}


