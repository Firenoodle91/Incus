using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

using HKInc.Utils.Enum;
using HKInc.Utils.Class;

namespace HKInc.Service.Handler
{
    public class RadioGroupHandler
    {
        private static Dictionary<HKInc.Utils.Enum.RadioGroupType, RadioGroupItemList> radioItemList = new Dictionary<RadioGroupType, RadioGroupItemList>
        {
            {RadioGroupType.ActiveAll, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("Active"),
                                                                                         new Helper.LabelConvert().GetLabelText("All") },
                                                                ItemValue = new string[]{ "Y", string.Empty },
                                                                DefaultIndex = 0 } },
            {RadioGroupType.HireAll, new RadioGroupItemList(){ ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("Hired"),
                                                                                        new Helper.LabelConvert().GetLabelText("All") },
                                                                ItemValue = new string[]{ "Y", string.Empty },
                                                                DefaultIndex = 0 } },
            {RadioGroupType.ConfirmAll, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("Confirm"),
                                                                                          new Helper.LabelConvert().GetLabelText("All") },
                                                                ItemValue = new string[]{ "Y", string.Empty },
                                                                DefaultIndex = 1 } },
            {RadioGroupType.NoYesAll, new RadioGroupItemList(){ ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("No"),
                                                                                        new Helper.LabelConvert().GetLabelText("Yes"),
                                                                                        new Helper.LabelConvert().GetLabelText("All")},
                                                                ItemValue = new string[]{ "N", "Y", string.Empty },
                                                                DefaultIndex = 2 } },
            {RadioGroupType.InspectionAll, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("Inspection"),
                                                                                             new Helper.LabelConvert().GetLabelText("NoInspection"),
                                                                                             new Helper.LabelConvert().GetLabelText("All")},
                                                                    ItemValue = new string[]{ "Y", "N", string.Empty },
                                                                    DefaultIndex = 0 } },
            {RadioGroupType.ShipmentAll, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("Shipment"),
                                                                                           new Helper.LabelConvert().GetLabelText("ExpShipment")},
                                                                  ItemValue = new string[]{ "Y", string.Empty },
                                                                  DefaultIndex = 0 } },
            {RadioGroupType.OkNotGood, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("OK"),
                                                                                         new Helper.LabelConvert().GetLabelText("NotGood")},
                                                                ItemValue = new string[]{ "Y", "N" },
                                                                DefaultIndex = 0 } },
            {RadioGroupType.TryAsAll, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("Try"),
                                                                                        new Helper.LabelConvert().GetLabelText("AS"),
                                                                                        new Helper.LabelConvert().GetLabelText("All") },
                                                                ItemValue = new string[]{ "T", "S", string.Empty },
                                                                DefaultIndex = 2 } },
            {RadioGroupType.AsAll, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("AS"),
                                                                                     new Helper.LabelConvert().GetLabelText("All") },
                                                                ItemValue = new string[]{ "S", string.Empty },
                                                                DefaultIndex = 0 } },
            {RadioGroupType.ConfirmNot, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("Confirm2"),
                                                                                          new Helper.LabelConvert().GetLabelText("NotConfirm") },
                                                                ItemValue = new string[]{ "Y", "N" },
                                                                DefaultIndex = 0 } },
             {RadioGroupType.OkNg, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("합격"),
                                                                                         new Helper.LabelConvert().GetLabelText("불합격") },
                                                                ItemValue = new string[]{"Y", "N"},
                                                                DefaultIndex = 0 } },
             {RadioGroupType.MachineAsItemCode, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("설비"),
                                                                                                  new Helper.LabelConvert().GetLabelText("품목") },
                                                                ItemValue = new string[]{"1", "2"},
                                                                DefaultIndex = 0 } },
             {RadioGroupType.NcrTabCheck, new RadioGroupItemList(){ItemName = new string[]{ "외부용","내부용"},
                                                                ItemValue = new string[]{"1", "2"},
                                                                DefaultIndex = 0 } },
             {RadioGroupType.NcrPreventYN, new RadioGroupItemList(){ItemName = new string[]{ "무","유"},
                                                                ItemValue = new string[]{"N", "Y"},
                                                                DefaultIndex = 0 } },
             {RadioGroupType.NcrCheckYN, new RadioGroupItemList(){ItemName = new string[]{ "완료","미완료"},
                                                                ItemValue = new string[]{"Y", "N"},
                                                                DefaultIndex = 0 } },
             {RadioGroupType.CheckResultYND, new RadioGroupItemList(){ItemName = new string[]{ "대기","합격","불합격"},
                                                                ItemValue = new string[]{"D","Y", "N"},
                                                                DefaultIndex = 0 } },
             {RadioGroupType.LoginMode, new RadioGroupItemList(){ItemName = new string[]{ "MES","POP","현황판","Russian" },
                                                                ItemValue = new string[]{"M","P","P1","P2"},
                                                                DefaultIndex = 0 } },
             {RadioGroupType.SafeAll, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("Safe"),
                                                                                     new Helper.LabelConvert().GetLabelText("All") },
                                                                ItemValue = new string[]{ "S", string.Empty },
                                                                DefaultIndex = 0 } },
             {RadioGroupType.DateMachine, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("Date"),
                                                                                     new Helper.LabelConvert().GetLabelText("Machine") },
                                                                ItemValue = new string[]{ "D", "M" },
                                                                DefaultIndex = 0 } },
             {RadioGroupType.EditMode, new RadioGroupItemList(){ItemName = new string[]{ new Helper.LabelConvert().GetLabelText("View"),
                                                                                         new Helper.LabelConvert().GetLabelText("Edit") },
                                                                ItemValue = new string[]{ "Y", string.Empty },
                                                                DefaultIndex = 0 } },
             {RadioGroupType.NotPrintAsAll, new RadioGroupItemList(){ItemName = new string[]{ "미출력", "모두" },
                                                                ItemValue = new string[]{ "N", string.Empty },
                                                                DefaultIndex = 0 } }

        };

        public static void SetRadioGroup(RadioGroup radioGroup, HKInc.Utils.Enum.RadioGroupType radioGroupType)
        {
            SetRadioGroup(radioGroup, radioItemList[radioGroupType].ItemName, radioItemList[radioGroupType].ItemValue, radioItemList[radioGroupType].DefaultIndex);
        }

        public static void SetRadioGroup(RadioGroup radioGroup, int GroupCode, string ValueMember = "CodeId", string DisplayMember = "CodeName", int DefaultIndex = 0)
        {
            HKInc.Utils.Interface.Helper.IMasterCode masterCode = HKInc.Service.Factory.HelperFactory.GetMasterCode();
            var List = masterCode.GetMasterCode(GroupCode).Where(p=>p.Active == "Y").OrderBy(p => p.DisplayOrder).ToList();
            if(List.Count > 0)
            {                
                var ItemName = GetMasterCodeArray(DisplayMember, GroupCode);
                var ItemValue = GetMasterCodeArray(ValueMember, GroupCode);
                SetRadioGroup(radioGroup, ItemName, ItemValue, DefaultIndex);
            }            
        }

        private static string[] GetMasterCodeArray(string ColumnName, int GroupCode)
        {
            if (string.IsNullOrEmpty(ColumnName)) return new string[] { };

            HKInc.Utils.Interface.Helper.IMasterCode masterCode = HKInc.Service.Factory.HelperFactory.GetMasterCode();
            var MasterCodeList = masterCode.GetMasterCode(GroupCode).OrderBy(p => p.DisplayOrder).ToList();
            if (MasterCodeList.Count == 0) return new string[] { };

            switch (ColumnName)
            {
                case "CodeId":
                    return MasterCodeList.Select(p => p.CodeId.GetNullToEmpty()).ToArray();
                case "CodeGroup":
                    return MasterCodeList.Select(p => p.CodeGroup.GetNullToEmpty()).ToArray();
                case "GroupDescription":
                    return MasterCodeList.Select(p => p.GroupDescription.GetNullToEmpty()).ToArray();
                case "CodeName":
                    return MasterCodeList.Select(p => p.CodeName.GetNullToEmpty()).ToArray();
                case "CodeName2":
                    return MasterCodeList.Select(p => p.CodeName2.GetNullToEmpty()).ToArray();
                case "CodeName3":
                    return MasterCodeList.Select(p => p.CodeName3.GetNullToEmpty()).ToArray();
                case "DisplayOrder":
                    return MasterCodeList.Select(p => p.DisplayOrder.GetNullToEmpty()).ToArray();
                case "Property1":
                    return MasterCodeList.Select(p => p.Property1.GetNullToEmpty()).ToArray();
                case "Property2":
                    return MasterCodeList.Select(p => p.Property2.GetNullToEmpty()).ToArray();
                case "Property3":
                    return MasterCodeList.Select(p => p.Property3.GetNullToEmpty()).ToArray();
                case "Property4":
                    return MasterCodeList.Select(p => p.Property4.GetNullToEmpty()).ToArray();
                case "Property5":
                    return MasterCodeList.Select(p => p.Property5.GetNullToEmpty()).ToArray();
                case "Property6":
                    return MasterCodeList.Select(p => p.Property6.GetNullToEmpty()).ToArray();
                case "Property7":
                    return MasterCodeList.Select(p => p.Property7.GetNullToEmpty()).ToArray();
                case "Property8":
                    return MasterCodeList.Select(p => p.Property8.GetNullToEmpty()).ToArray();
                case "Property9":
                    return MasterCodeList.Select(p => p.Property9.GetNullToEmpty()).ToArray();
                case "Property10":
                    return MasterCodeList.Select(p => p.Property10.GetNullToEmpty()).ToArray();
                case "Active":
                    return MasterCodeList.Select(p => p.Active.GetNullToEmpty()).ToArray();
                case "DeActiveDate":
                    return MasterCodeList.Select(p => p.DeActiveDate.GetNullToEmpty()).ToArray();
                default:
                    return new string[] { };
            }
        }

        public static void SetRepositoryRadioGroup(RepositoryItemRadioGroup radioGroup, HKInc.Utils.Enum.RadioGroupType radioGroupType)
        {
            SetRadioGroup(radioGroup, radioItemList[radioGroupType].ItemName, radioItemList[radioGroupType].ItemValue, radioItemList[radioGroupType].DefaultIndex);
        }

        private static void SetRadioGroup(RepositoryItemRadioGroup radioGroup, string[] itemName, object[] itemValue, int defaultIndex = 0)
        {
            radioGroup.BeginUpdate();
            radioGroup.Columns = itemName.Count();
            for (int i = 0; i < itemValue.Length; i++)
            {
                radioGroup.Items.Add(new RadioGroupItem(itemValue[i], itemName[i]));
            }
            radioGroup.Columns = 2;
            radioGroup.EndUpdate();                    
        }

        private static void SetRadioGroup(RadioGroup radioGroup, string[] itemName, object[] itemValue, int defaultIndex = 0)
        {
            radioGroup.Properties.BeginUpdate();
            radioGroup.Properties.Columns = itemName.Count();
            for (int i = 0; i < itemValue.Length; i++)
            {
                radioGroup.Properties.Items.Add(new RadioGroupItem(itemValue[i], itemName[i]));
            }

            radioGroup.Properties.EndUpdate();
            radioGroup.SelectedIndex = defaultIndex;            
        }

        class RadioGroupItemList
        {
            public string[] ItemName { get; set; }
            public string[] ItemValue { get; set; }
            public int DefaultIndex { get; set; }
        }
    }
}
