using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Service.Helper;
using HKInc.Utils.Class;

namespace HKInc.Service.Controls
{
    public partial class FACTVIEW : UserControl
    {
        IService<TN_SFC1000> ModelService = (IService<TN_SFC1000>)ProductionFactory.GetDomainService("TN_SFC1000");
        public event EventHandler ControlRemoveHandler;
        //bool MoveFlag = false;
        //Point MouseDownPoint;

        //public string ItemCode { get { return txt_ItemCode.Text; } set { txt_ItemCode.Text = value.ToString(); } }
        //public string MachineCode { get { return txt_MachineCode.Text; } set { txt_MachineCode.Text = value.ToString(); } }
        //public string Machine { get { return lup_Machine.Text; } set { lup_Machine.Text = value.ToString(); } }
        //public string Item { get { return lup_Item.Text; } set { lup_Item.Text = value.ToString(); } }
        //public string Seq { get { return lblSeq.Text; } set { lblSeq.Text = value.ToString(); } }
        //public string NewOrEdit { get { return lblNewOrEdit.Text; } set { lblNewOrEdit.Text = value.ToString(); } }
        //public string MachineCode { get { return lblMachineCode.Text; } set { lblMachineCode.Text = value.ToString(); } }
        //public string ItemCode { get { return lblItemCode.Text; }  set { lblItemCode.Text = value.ToString(); } }

        Timer TIMER1 = new Timer();

        public FACTVIEW()
        {
            InitializeComponent();
            //lup_Machine.SetDefault(true, "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y"));
            //lup_Machine.NullText = "";
            //lup_Machine.EditValueChanged += LupMachine_Change;

            //lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());
            //lup_Item.NullText = "";
            //lup_Item.EditValueChanged += LupItem_Change;

            TIMER1.Interval = 1000;
            TIMER1.Tick += Timer1_Tick;
            TIMER1.Start();

        }

        //public void ControlRemove()
        //{
        //    if (ControlRemoveHandler != null)
        //        ControlRemoveHandler(this, EventArgs.Empty);
        //}

        private void Timer1_Tick(object sender, EventArgs e)
        {
            TIMER1.Stop();
            string Itemcode = lblItemCode.Text;
            string MachineCode = lblMachineCode.Text;
            TN_STD1100 STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemName == Itemcode).FirstOrDefault();
            TN_MEA1000 MEA1000 = ModelService.GetChildList<TN_MEA1000>(p => p.MachineName == MachineCode).FirstOrDefault();

            if (Itemcode != "" && MachineCode != "") 
            {
                List<TN_MPS1202> Data = ModelService.GetChildList<TN_MPS1202>(p => p.ItemCode == STD1100.ItemCode && p.MachineCode == MEA1000.MachineMCode).ToList();
                int total = 0;
                int ok = 0;
                int bad = 0;

                foreach (var v in Data)
                {
                    total += Convert.ToInt32(v.ResultQty);
                    ok += Convert.ToInt32(v.OkQty);
                    bad += Convert.ToInt32(v.BadQty);
                }

                lblTotal.Text = "생산수량 : " + total.ToString();
                lblOk.Text = "정상수량 : " + ok.ToString();
                lblBad.Text = "불량수량 : " + bad.ToString();
            }
            TIMER1.Start();

            //TIMER1.Stop();
            //string a = lup_Item.Text;
            //string item2 = lup_Item.EditValue.GetNullToEmpty();
            //string Machine2 = lup_Machine.EditValue.GetNullToEmpty();
            //string item = lblItemCode.Text;
            //string machine = lblMachineCode.Text;
            ////string Machine = lup_Machine.EditValue.GetNullToEmpty();
            //DateTime Day = new DateTime(2021, 04, 15, 00, 00, 00);
            ////List<TN_MPS1202> Data2 = ModelService.GetChildList<TN_MPS1202>(p => p.ItemCode == item && p.MachineCode == machine).ToList();
            ////List<TN_MPS1202> Data = ModelService.GetChildList<TN_MPS1202>(p => p.ItemCode == item && p.MachineCode == machine && p.ResultInsDate == Day).ToList();
            ////List<TN_MPS1202> Data = ModelService.GetChildList<TN_MPS1202>(p => p.ItemCode == item && p.MachineCode == Machine && p.ResultInsDate == Today).ToList();

            //if (Machine.GetNullToEmpty() != null && Item.GetNullToEmpty() != null)
            //{
            //    int total = 0;
            //    int ok = 0;
            //    int bad = 0;

            //    foreach (var v in Data2)
            //    {
            //        total += Convert.ToInt32(v.ResultQty);
            //        ok += Convert.ToInt32(v.OkQty);
            //        bad += Convert.ToInt32(v.BadQty);
            //    }

            //    lblTotal.Text = "생산량 : " + total.ToString();
            //    lblOk.Text = "양품 : " + ok.ToString();
            //    lblBad.Text = "불량품 : " + bad.ToString();
            //}
            //TIMER1.Start();
        }

        private void LupMachine_Change(object sender, EventArgs e)
        {
            //string Machine = lup_Machine.EditValue.GetNullToEmpty();
            //int seq = Convert.ToInt32(lblSeq.Text.GetNullToZero());
            ////ModelService.ReLoad();

            //TN_SFC1000 SFC1000 = ModelService.GetList(p => p.DisplayName == "테스트 화면명" && p.Seq == seq).FirstOrDefault();

            //var LupMachine = sender as object;
            //if (LupMachine != null && SFC1000 != null)
            //{
            //    lblMachineCode.Text = Machine;

            //    SFC1000.MachineCode = Machine;
            //    ModelService.Update(SFC1000);
            //    ModelService.Save();
            //}
        }

        private void LupItem_Change(object sender, EventArgs e)
        {
            //string item = lup_Item.EditValue.GetNullToEmpty();
            //int seq = Convert.ToInt32(lblSeq.Text.GetNullToZero());

            //TN_SFC1000 SFC1000 = ModelService.GetList(p => p.DisplayName == "테스트 화면명" && p.Seq == seq).FirstOrDefault();

            //var LupItem = sender as object;
            //if (LupItem != null && SFC1000 != null)
            //{
            //    lblItemCode.Text = item;

            //    SFC1000.ItemCode = item;
            //    ModelService.Update(SFC1000);
            //    ModelService.Save();
            //}
        }

        private void FACTVIEW_MouseDown(object sender, MouseEventArgs e)
        {
            Control control = sender as Control;
            control.DoDragDrop(control, DragDropEffects.Move | DragDropEffects.Copy);
        }

        public void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
