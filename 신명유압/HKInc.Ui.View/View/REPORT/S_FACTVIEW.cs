using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using System.Collections.Specialized;
using System.IO;
using HKInc.Service.Helper;
using HKInc.Service.Controls;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 툴 기준정보관리
    /// </summary>
    public partial class S_FACTVIEW : Service.Base.ListFormTemplate
    {
        IService<TN_SFC1000> ModelService = (IService<TN_SFC1000>)ProductionFactory.GetDomainService("TN_SFC1000");
        BindingSource DataBinding;
        bool panel1EnterCheck = false;  // 판넬 진입여부 확인
        int CheckSeq;
        int P_X;
        int P_Y;
        int Seq_Check;

        public S_FACTVIEW()
        {
            InitializeComponent();
            this.panel1.AllowDrop = true;
            DataBinding = new BindingSource();
            DataBinding.DataSource = new List<TN_SFC1000>();
            //Timer1.Start();
            //Timer1.Interval = 10;
            Timer1.Start();
            Timer1.Interval = 10;

            DataLoad();
        }


        //protected override void DataSave()
        //{
        //    List<TN_SRC1000> TotalData = DataBinding.DataSource as List<TN_SRC1000>;
        //    TotalData.Where(p => p.NewRowFlag == "Y" || p.EditRowFlag == "Y").ToList();

            

        //    var Data = ModelService.GetList(p => p.NewRowFlag == "Y").ToList();

        //    foreach (var v in Data)
        //    {
        //        v.NewRowFlag = "N";
        //        ModelService.Update(v);
        //    }

        //    ModelService.Save();
            //List<TN_SFC1000> a = ModelService.GetList(p => true).ToList();
            //List<TN_SFC1000> b = DataBinding.DataSource as List<TN_SFC1000>;
            //List<TN_SFC1000> b = DataBinding.List as List<TN_SFC1000>;
            //DataBinding.DataSource = ModelService.GetList(p => true).ToList();
            //DataBinding.DataSource = ModelService.GetList(p => p.ItemCode == factview1.ItemCode).ToList();
            //var a = DataBinding.DataSource;

            //List<TN_SFC1000> DataList = DataBinding.DataSource as List<TN_SFC1000>;

            //DataBinding.DataSource = ModelService.GetList(p => p.NewRowFlag == "Y").ToList();
            //List<TN_SFC1000> NewObj = DataList.Where(p => p.NewRowFlag == "Y").ToList();
            //List<TN_SFC1000> EditObj = DataList.Where(p => p.EditRowFlag == "Y").ToList();
            //List<TN_SFC1000> NewObj = ModelService.GetList(p => p.NewRowFlag == "Y").ToList();
            //List<TN_SFC1000> UpdObj = ModelService.GetList(p => p.EditRowFlag == "Y").ToList();
            //List<TN_SFC1000> List = DataBinding.DataSource

            //ControlCollection CONTROL = panel1.Controls

            //int A = FACTVIEW.

            //for (int i = 0; i < NewObj.Count; i++)
            //{
            //    int Seq = NewObj.Where(p => p.ItemCode == factview1.ItemCode).Max(m => m.Seq).GetIntNullToZero();

            //    NewObj[i].ItemCode = factview1.ItemCode;
            //    NewObj[i].Seq = Seq == 0 ? 0 : Seq++;
            //    NewObj[i].Seq = ModelService.GetList(p => p.ItemCode == NewObj[i].ItemCode).Max(m => m.Seq).GetIntNullToZero() == 0 ? 0 : 1;
            //    NewObj[i].MachineCode = factview1.MachineCode;
            //    NewObj[i].x = factview1.Location.X;
            //    NewObj[i].y = factview1.Location.Y;
            //}

            //foreach (TN_SFC1000 v in NewObj)
            //{
                
            //}


            //foreach (var item in collection)
            //{

            //}

        //}

        /// <summary>
        /// 끌어서 놓기 작업이 완료될 때 발생
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drag_Drop");

            var GetType = e.Data.GetFormats()[0];
            string[] TypeSplit = GetType.Split('.');
            string type = TypeSplit[3];

            if (type == "FACTVIEW_MAIN")
            {
                FACTVIEW_MAIN control = e.Data.GetData(e.Data.GetFormats()[0]) as FACTVIEW_MAIN;

                string c = control.CompanyName;


                Point b = control.FindForm().PointToClient(control.Location);

                Point a = control.FindForm().PointToClient(
                            control.Parent.PointToScreen(control.Location));

                //var c = control.GetChildAtPoint();

                //control.Location = new Point(a.X, a.Y);
                control.Location = new Point(e.X - 262, e.Y - 115);

                //var c = control.Parent;

                //Panel d = c as Panel;
                //int X2 = c.Location.X;
                //int Y2 = c.Location.Y;

                //control.Location = new Point(X2, Y2);

                int X = control.Location.X;
                int Y = control.Location.Y;

                control.BringToFront();

                if (control != null)
                {
                    if (panel1EnterCheck)
                    {
                        TN_SFC1000 SFC1000 = ModelService.GetList(p => p.DisplayName == "테스트 화면명").OrderByDescending(o => o.Seq).FirstOrDefault();

                        TN_SFC1000 Newobj = new TN_SFC1000();
                        Newobj.DisplayName = "테스트 화면명";
                        Newobj.Seq = SFC1000 == null ? 1 : SFC1000.Seq + 1;
                        //Newobj.x = X2;
                        //Newobj.y = Y2;
                        Newobj.x = X;
                        Newobj.y = Y;
                        Newobj.NewRowFlag = "Y";

                        ModelService.Insert(Newobj);
                        CheckSeq = 0;
                        panel1EnterCheck = false;
                        control.lblSeq.Text = Newobj.Seq.ToString();
                    }
                    else
                    {
                        int seq = Convert.ToInt32(control.lblSeq.Text);

                        TN_SFC1000 MoveObj = ModelService.GetList(p => p.DisplayName == "테스트 화면명" && p.Seq == seq).FirstOrDefault();

                        //MoveObj.x = a.X;
                        //MoveObj.y = a.Y;

                        MoveObj.x = X;
                        MoveObj.y = Y;

                        ModelService.Update(MoveObj);

                    }
                }
            }
            else if (type == "FACTVIEW")
            {
                FACTVIEW control = e.Data.GetData(e.Data.GetFormats()[0]) as FACTVIEW;
                control.Location = new Point(e.X - 262, e.Y - 115);
            }

            ModelService.Save();

            #region a
            /*
            FACTVIEW control = e.Data.GetData(e.Data.GetFormats()[0]) as FACTVIEW;
            control.Location = new Point(e.X - 262, e.Y - 115);
            //control.Location = panel1.PointToClient(new Point(e.X, e.Y));

            int X = control.Location.X;
            int Y = control.Location.Y;
            //int Y = control.Location.Y - SystemInformation.CaptionHeight;

            control.BringToFront();

            if (control != null)
            {
                //if (panel1EnterCheck)
                //{
                //    List<TN_SFC1000> Maxseq = ModelService.GetList(p => p.DisplayName == "테스트화면명").OrderBy(o => o.Seq).ToList();
                //    TN_SFC1000 Newobj = new TN_SFC1000();

                //    //TN_SFC1000 Newobj = new TN_SFC1000();
                //    Newobj.DisplayName = "테스트 화면명";
                //    Newobj.Seq = CheckSeq = Maxseq.Count == 0 ? 1 : Maxseq.Max(o => o.Seq) + 1;
                //    Newobj.NewRowFlag = "Y";

                //    ModelService.Insert(Newobj);

                //    panel1EnterCheck = false;

                //    //TN_SFC1000 Newobj = ModelService.GetList(p => p.DisplayName == "테스트 화면명" && p.Seq == CheckSeq).FirstOrDefault();
                //}
                //else
                //{
                //    TN_SFC1000 ModelDropData = ModelService.GetList(p => p.DisplayName == "테스트 화면명" && p.Seq == CheckSeq).FirstOrDefault();

                //    ModelDropData.x = e.X;
                //    ModelDropData.y = e.Y;

                //    ModelService.Update(ModelDropData);
                //}
                //ModelService.Save();

                //if (control.NewOrEdit == "New")
                //{
                //TN_SFC1000 Data = ModelService.GetList(p => p.DisplayName == "테스트 화면명").OrderBy(o => o.Seq).FirstOrDefault();

                //TN_SFC1000 Newobj = new TN_SFC1000();

                //Newobj.DisplayName = "테스트 화면명";
                //Newobj.Seq = Data == null ? 1 : Data.Seq + 1;
                //Newobj.x = X;
                //Newobj.y = Y;
                //Newobj.NewRowFlag = "Y";

                ////panel1.Controls.Add(Newobj);

                //DataBinding.Add(Newobj);
                //ModelService.Insert(Newobj);

                //FACTVIEW newfact = new FACTVIEW();
                //newfact.NewOrEdit = "New";

                //    panel1.controls.add(newfact);
                //}

                if (panel1EnterCheck)
                {
                    TN_SFC1000 SFC1000 = ModelService.GetList(p => p.DisplayName == "테스트 화면명").OrderByDescending(o => o.Seq).FirstOrDefault();
                    //int maxseq = ModelService.GetList(p => p.DisplayName == "테스트 화면명").Max(m => m.Seq).GetIntNullToZero();

                    TN_SFC1000 newobj = new TN_SFC1000();
                    newobj.DisplayName = "테스트 화면명";
                    newobj.Seq = SFC1000 == null ? 1 : SFC1000.Seq + 1;
                    newobj.x = X;
                    newobj.y = Y;
                    newobj.NewRowFlag = "y";
                    //databinding.add(newobj);
                    ModelService.Insert(newobj);
                    CheckSeq = 0;
                    panel1EnterCheck = false;

                    control.Seq = newobj.Seq;

                    //control control = e.data.getdata(e.data.getformats()[0]) as control;
                    //list<tn_sfc1000> bindingdata = databinding.datasource as list<tn_sfc1000>;

                    //tn_sfc1000 newobj = new tn_sfc1000();
                    //newobj.displayname = "테스트 화면명";
                    //var checkdata = modelservice.getlist(p => p.displayname == newobj.displayname).tolist();
                    ////var checkdata = bindingdata.where(p => p.displayname == newobj.displayname).tolist();
                    //newobj.seq = checkdata.count == 0 ? 1 : checkdata.max(m => m.seq) + 1;
                    //newobj.newrowflag = "y";

                    ////databinding.add(newobj);
                    ////modelservice.insert(newobj);

                    //checkseq = newobj.seq;



                    //list<tn_sfc1000> bindingdata = databinding.datasource as list<tn_sfc1000>;

                    //tn_sfc1000 bindingdropdata = bindingdata.where(p => p.newrowflag == "y" && p.displayname == "테스트 화면명" && p.seq == checkseq).firstordefault();

                    //bindingdropdata.x = x;
                    //bindingdropdata.y = y;
                    //bindingdropdata.newrowflag = "n";
                    //bindingdropdata.displayname = "테스트 화면명";
                    //bindingdropdata.seq = checkseq == 0 ? 1 : checkseq + 1;
                    //bindingdropdata.machinecode = control.machinecode;
                    //bindingdropdata.itemcode = control.item;
                    //bindingdropdata.newrowflag = "y";

                    //databinding.add(bindingdropdata);
                    //modelservice.insert(bindingdropdata);
                    //checkseq = 0;
                    //panel1entercheck = false;

                    //    #region B
                    //    //TN_SFC1000 ModelInsert = new TN_SFC1000();

                    //    //TN_SFC1000 ModelDropData = ModelService.GetList(p => p.DisplayName == "테스트 화면명" && p.Seq == CheckSeq).FirstOrDefault();

                    //    //ModelDropData.x = e.X;
                    //    //ModelDropData.y = e.Y;

                    //    //ModelService.Update(ModelDropData);



                    //    //List<TN_SFC1000> XYCheck = 

                    //    //TN_SFC1000 newObj = new TN_SFC1000();

                    //    
                    //    TN_SFC1000 NewObj = new TN_SFC1000();

                    //    //NewObj.ItemCode = "ITEM-00001";
                    //    //NewObj.Seq = ModelService.GetList(p => p.ItemCode == NewObj.ItemCode).Max(m => m.Seq).GetIntNullToZero() == 0 ? 0 : 1;
                    //    NewObj.NewRowFlag = "Y";
                    //    NewObj.x = factview1.Location.X;
                    //    NewObj.y = factview1.Location.Y;

                    //    DataBinding.Add(NewObj);
                    //    ModelService.Insert(NewObj);
                    //    //ModelService.Save();
                    //    
                    //    //TN_SFC1000 NewObj = new TN_SFC1000();

                    //    //NewObj.ItemCode = factview1.ItemCode;
                    //    //NewObj.MachineCode = factview1.MachineCode;
                    //    //NewObj.x = control.Location.X;
                    //    //NewObj.y = control.Location.Y;

                    //    //ModelService.Insert(NewObj);
                    //    //ModelService.Save();
                    //    #endregion
                }
                else
                {
                    //string MachineCode = control.MachineCode;
                    int seq = control.Seq;

                    TN_SFC1000 MoveObj = ModelService.GetList(p => p.DisplayName == "테스트 화면명" && p.Seq == seq).FirstOrDefault();

                    //TN_SFC1000 MoveObj = ModelService.GetList(p => p.MachineCode == MachineCode && p.Seq == seq).FirstOrDefault();

                    MoveObj.x = X;
                    MoveObj.y = Y;

                    ModelService.Update(MoveObj);
                }
                ModelService.Save();
            }
            */
            #endregion

        }

        protected override void DataLoad()
        {
            panel1.Controls.Clear();

            ModelService.ReLoad();

            List<TN_SFC1000> SFC1000List = ModelService.GetList(p => true).ToList();

            foreach (var v in SFC1000List)
            {
                FACTVIEW_MAIN LoadMain = new FACTVIEW_MAIN();

                LoadMain.Location = new Point(v.x, v.y);
                LoadMain.lblSeq.Text = v.Seq.ToString();
                //LoadMain.Seq = v.Seq;
                LoadMain.lup_Machine.Text = v.MachineCode;
                //LoadMain.Machine = v.MachineCode;
                LoadMain.btn_Remove.Click += PanelControlRemove;
                LoadMain.btnDisplay.Click += FactviewDisplay;
                //LoadMain.btnDisplay.MouseClick += FactviewDisplay2;
                //LoadMain.MouseEnter += Check_Seq;
                LoadMain.MouseDown += CopyControl_MouseDown;
                panel1.Controls.Add(LoadMain);
            }
            #region a
            /*
            panel1.Controls.Clear();
            //panel1.Refresh();

            ModelService.ReLoad();
            List<TN_SFC1000> LoadList = ModelService.GetList(p => true).ToList();
            DataBinding.DataSource = LoadList;

            foreach (var v in LoadList)
            {
                TN_MEA1000 MachineData = ModelService.GetChildList<TN_MEA1000>(p => p.MachineMCode == v.MachineCode).FirstOrDefault();

                FACTVIEW LoadFactoryView = new FACTVIEW();
                LoadFactoryView.MouseDown += CopyControl_MouseDown;
                //LoadFactoryView.Location = panel1.PointToClient(new Point(v.x, v.y));
                LoadFactoryView.Location = new Point(v.x, v.y);
                LoadFactoryView.Machine = v.MachineCode;
                LoadFactoryView.Seq = v.Seq;
                //LoadFactoryView.MachineCode = MachineData.MachineName;
                LoadFactoryView.MachineCode = v.MachineCode;
                LoadFactoryView.Item = v.ItemCode;
                LoadFactoryView.ItemCode = v.ItemCode;

                panel1.Controls.Add(LoadFactoryView);
            }


            if (Timer1.Enabled) Timer1.Stop();
            Timer1.Start();
            SetMessage("모니터링 진행중...");
            */

            //panel1EnterCheck = false;
            #region a
            //GridRowLocator.GetCurrentRow("ItemCode");

            //GridExControl.MainGrid.Clear();

            //ModelService.ReLoad();
            //GridExControl.MainGrid.Columns.Clear();
            //var toolCodeName = tx_ToolCodeName.EditValue.GetNullToEmpty();
            //var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            //GridBindingSource.DataSource = ModelService.GetList(p =>    (string.IsNullOrEmpty(toolCodeName) ? true : (p.ItemCode.Contains(toolCodeName) || (p.ItemName.Contains(toolCodeName)) || p.ItemNameENG.Contains(toolCodeName) || p.ItemNameCHN.Contains(toolCodeName)))
            //                                                        && (radioValue == "A" ? true : p.UseFlag == radioValue)
            //                                                        && (p.TopCategory == MasterCodeSTR.TopCategory_TOOL)
            //                                                   )
            //                                                   .OrderBy(p => p.ItemName)
            //                                                   .ToList();
            //GridExControl.DataSource = GridBindingSource;
            //GridExControl.BestFitColumns();

            //GridRowLocator.SetCurrentRow();
            //DataSet ds = DbRequestHandler.GetDataQury("exec sp_workdt");
            //GridBindingSource.DataSource = ds.Tables[0];
            //GridExControl.DataSource = GridBindingSource;
            //GridExControl.BestFitColumns();
            #endregion
            #endregion
        }

        //private void Check_Seq(object sender, EventArgs e)
        //{
        //    var a = e.ToString();

        //}

        protected override void DataSave()
        {


            ModelService.Save();
            DataLoad();

            //List<TN_SFC1000> SFC1000List = DataBinding.DataSource as List<TN_SFC1000>;

            //List<TN_SFC1000> NewList = SFC1000List.Where(p => p.NewRowFlag == "Y").ToList();

            //for (int i = 0; i < NewList.Count; i++)
            //{
            //    ModelService.Insert(NewList[i]);
            //}



            ////FACTVIEW control = 
            //List<TN_SFC1000> NewList = ModelService.GetList(p => p.NewRowFlag == "Y").ToList();
            //for (int i = 0; i < NewList.Count; i++)
            //{
            //    //NewList[i].MachineCode = 
            //}

            //List<TN_SFC1000> TotalData = DataBinding.DataSource as List<TN_SFC1000>;
            //var a = TotalData.Where(p => p.NewRowFlag == "Y" || p.EditRowFlag == "Y").ToList();

            ////var Data = ModelService.GetList(p => p.NewRowFlag == "Y").ToList();

            //foreach (var v in a)
            //{
            //    //v.NewRowFlag = "N";
            //    //ModelService.Update(v);
            //}
        }

        /// <summary>
        /// 신규생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void factvieW_MAIN1_MouseDown(object sender, MouseEventArgs e)
        {
            FACTVIEW_MAIN Newobj = new FACTVIEW_MAIN();
            Newobj.MouseDown += CopyControl_MouseDown;
            //Newobj.MainControlRemoveHandler += PanelMainControlRemove;
            //Newobj.EnableButton
            Newobj.btn_Remove.Click += PanelControlRemove;
            Newobj.btnDisplay.Click += FactviewDisplay;
            //Newobj.btnDisplay.MouseClick += FactviewDisplay2;
            panel1.Controls.Add(Newobj);
            panel1EnterCheck = true;
            Newobj.DoDragDrop(Newobj, DragDropEffects.Move | DragDropEffects.Copy);

            //FACTVIEW NewFactoryView = new FACTVIEW();
            //NewFactoryView.MouseDown += CopyControl_MouseDown;
            //panel1.Controls.Add(NewFactoryView);
            //NewFactoryView.NewOrEdit = "New";
            //panel1EnterCheck = true;
            //NewFactoryView.DoDragDrop(NewFactoryView, DragDropEffects.Move | DragDropEffects.Copy);
        }

        //private void FactviewDisplay2(object sender, MouseEventArgs e)
        //{
        //    var a = e;
        //    var c = e.GetType();
                
        //}

        private void FactviewDisplay(object sender, EventArgs e)
        {
            /// 누른 설비의 정보를 알수 없음 불러와야됨
            Button BtnDisplay = sender as Button;
            string[] CheckData = BtnDisplay.Text.Split(',');

            if (CheckData != null)
            {
                int Seq = Convert.ToInt32(CheckData[0]);
                string Machine = CheckData[1];
                List<string> ItemList = new List<string>();
                int x = 0;
                int y = 100;

                TN_MEA1000 MEA1000 = ModelService.GetChildList<TN_MEA1000>(p => p.MachineMCode == Machine).FirstOrDefault();
                List<TN_MPS1202> MPS1202List = ModelService.GetChildList<TN_MPS1202>(p => p.MachineCode == Machine).ToList();    // 추후 날짜 추가

                foreach (var v in MPS1202List)
                {
                    TN_STD1100 STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                    ItemList.Add(STD1100.ItemName);
                }

                var ItemList2 = ItemList.Distinct().ToArray();
                TN_SFC1000 SFC1000 = ModelService.GetList(p => p.DisplayName == "테스트 화면명" && p.Seq == Seq).FirstOrDefault();

                if (SFC1000 != null && MEA1000 != null)
                {
                    for (int i = 0; i < ItemList2.Count(); i++)
                    {
                        FACTVIEW AddDetail = new FACTVIEW();
                        AddDetail.lblItemCode.Text = ItemList2[i];
                        AddDetail.lblMachineCode.Text = MEA1000.MachineName;
                        AddDetail.Location = new Point(SFC1000.x + x, SFC1000.y + y);
                        AddDetail.BringToFront();
                        AddDetail.MouseDown += CopyControl_MouseDown;
                        AddDetail.btnDelete.Click += PanelControlRemove;
                        panel1.Controls.Add(AddDetail);
                        x += 150;
                        //y += 15;
                    }
                }

            }
            BtnDisplay.Text = "상세내역";


        }

        public void PanelControlRemove(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Control P_Control = control.Parent;

            if (P_Control.Name == "FACTVIEW_MAIN")
            {
                FACTVIEW_MAIN MainControl = P_Control as FACTVIEW_MAIN;
                int seq = Convert.ToInt32(MainControl.lblSeq.Text);

                panel1.Controls.Remove(MainControl);

                TN_SFC1000 SFC1000 = ModelService.GetList(p => p.DisplayName == "테스트 화면명" && p.Seq == seq).FirstOrDefault();

                ModelService.Delete(SFC1000);
            }
            else if (P_Control.Name == "FACTVIEW")
            {
                FACTVIEW MainControl = P_Control as FACTVIEW;
                panel1.Controls.Remove(MainControl);
            }


            //FACTVIEW P_control = control.Parent as FACTVIEW;
            //int seq = 0;
            //if (P_control != null)
            //{
            //    panel1.Controls.Remove(P_control);
            //    seq = P_control.
            //}

            //TN_SFC1000 SFC1000 = ModelService.GetList(p => p.DisplayName == "테스트 화면명" && p.Seq == )

            //panel1.Controls.Remove(factview1);
            ////factview1.Dispose;

            //Control control = sender as Control;

            //if (panel1.Controls.Contains(factview1))
            //{
            //    //this.factview1.btnDelete_Click -= PanelMainControlRemove;
            //    panel1.Controls.Remove(factview1);
            //    factview1.Dispose();
                    
            //}

            //panel1.Controls.Remove(factview1);
            //panel1.Controls.

            //Control control = sender as Control;
            //FACTVIEW_MAIN control = e.Data.GetData(e.Data.GetFormats()[0]) as FACTVIEW_MAIN;
        }

        //private void RemoveControl(object sender, System.EventArgs e)
        //{
        //    if (panel1.Controls.Contains(factview1))
        //    {
        //        //this.factview1.btnDelete_Click -= PanelMainControlRemove;
        //        panel1.Controls.Remove(factview1);
        //        factview1.Dispose();
                
        //    }
        //}

        /// <summary>
        /// 드래그시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move | DragDropEffects.Copy;
        }

        /// <summary>
        /// 판넬 진입시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("Drag_Enter");

            //List<TN_SFC1000> CheckSeq = ModelService.GetList(p => p.DisplayName == "테스트화면명").OrderBy(o => o.Seq).ToList();

            //TN_SFC1000 Newobj = new TN_SFC1000();
            //Newobj.DisplayName = "테스트 화면명";
            //Newobj.Seq = CheckSeq.Count == 0 ? 1 : CheckSeq.Max(o => o.Seq) + 1;
            //Newobj.NewRowFlag = "Y";

            //ModelService.Insert(Newobj);

            //FACTVIEW NewView = new FACTVIEW();
            //NewView.NewOrEdit = "New";
            //NewView.MouseDown += CopyControl_MouseDown;
            //panel1.Controls.Add(NewView);
            //NewView.DoDragDrop(NewView, DragDropEffects.Move | DragDropEffects.Copy);

            //panel1EnterCheck = true;

            Control control = e.Data.GetData(e.Data.GetFormats()[0]) as Control;
            List<TN_SFC1000> BindingData = DataBinding.DataSource as List<TN_SFC1000>;

            TN_SFC1000 NewObj = new TN_SFC1000();
            NewObj.DisplayName = "테스트 화면명";
            var CheckData = ModelService.GetList(p => p.DisplayName == NewObj.DisplayName).ToList();
            //var CheckData = BindingData.Where(p => p.DisplayName == NewObj.DisplayName).ToList();
            NewObj.Seq = CheckData.Count == 0 ? 1 : CheckData.Max(m => m.Seq) + 1;
            NewObj.NewRowFlag = "Y";

            //DataBinding.Add(NewObj);
            //ModelService.Insert(NewObj);

            CheckSeq = NewObj.Seq;

            #region B
            //e.Effect = DragDropEffects.Move | DragDropEffects.Copy;
            /*
            TN_SFC1000 NewObj = new TN_SFC1000();

            NewObj.DisplayName = "테스트 화면명";
            //var Data = ModelService.GetList(p => p.DisplayName == NewObj.DisplayName);
            List<TN_SFC1000> BindingData = DataBinding.DataSource as List<TN_SFC1000>;
            var CheckData = BindingData.Where(p => p.DisplayName == NewObj.DisplayName).ToList();
            //var d = DataBinding.DataSource as List<TN_SFC1000>;
            //var c = ListData.Where(p => p.DisplayName == NewObj.DisplayName).Count();
            NewObj.Seq = CheckData.Count == 0 ? 1 : CheckData.Max(m => m.Seq) + 1;
            //NewObj.x = factview1.Location.X;
            //NewObj.y = factview1.Location.Y;
            NewObj.NewRowFlag = "Y";

            DataBinding.Add(NewObj);
            ModelService.Insert(NewObj);
            CheckSeq = NewObj.Seq;
            */


            //e.Data.DATA
            //var a = control;

            //a.

            //TN_SFC1000 CurrentData = DataBinding.Current as TN_SFC1000;


            //panel1EnterCheck = true;
            //var a = ModelService.GetList(p => true).ToList();

            //TN_SFC1000 NewObj = new TN_SFC1000();

            //NewObj.NewRowFlag = "Y";

            //DataBinding.Add(NewObj);
            //ModelService.Insert(NewObj);
            #endregion
        }

        /// <summary>
        /// 판넬 나갈시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_DragLeave(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 컨트롤 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void factview1_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseDown");

            //List<TN_SFC1000> Maxseq = ModelService.GetList(p => p.DisplayName == "테스트화면명").OrderBy(o => o.Seq).ToList();

            //TN_SFC1000 Newobj = new TN_SFC1000();
            //Newobj.DisplayName = "테스트 화면명";
            //Newobj.Seq = CheckSeq = Maxseq.Count == 0 ? 1 : Maxseq.Max(o => o.Seq) + 1;
            //Newobj.NewRowFlag = "Y";

            //ModelService.Insert(Newobj);

            //FACTVIEW NewView = new FACTVIEW();
            //NewView.MouseDown += CopyControl_MouseDown;
            //NewView.DoDragDrop(NewView, DragDropEffects.Move | DragDropEffects.Copy);
            //NewView.NewOrEdit = "New";
            //panel1.Controls.Add(NewView);

            //panel1EnterCheck = true;


            //int Maxseq = ModelService.GetList(p => p.DisplayName == "테스트 화면명").Max(m => m.Seq).GetIntNullToZero();

            //TN_SFC1000 NewObj = new TN_SFC1000();

            //NewObj.EditRowFlag = "Y";
            //NewObj.Seq = Maxseq == 0 ? 1 : Maxseq + 1;

            //ModelService.Insert(NewObj);



            FACTVIEW NewFactoryView = new FACTVIEW();
            NewFactoryView.MouseDown += CopyControl_MouseDown;
            panel1.Controls.Add(NewFactoryView);
            panel1EnterCheck = true;
            NewFactoryView.DoDragDrop(NewFactoryView, DragDropEffects.Move | DragDropEffects.Copy);
        }

        /// <summary>
        /// 추가된 Control 이동제어
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyControl_MouseDown (object sender, MouseEventArgs e)
        {
            Control control = sender as Control;
            control.DoDragDrop(control, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            Label NewLabel = new Label();
            NewLabel.MouseDown += CopyControl_MouseDown;
            panel1.Controls.Add(NewLabel);
            NewLabel.DoDragDrop(NewLabel, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            Button NewButton = new Button();
            NewButton.MouseDown += CopyControl_MouseDown;
            panel1.Controls.Add(NewButton);
            NewButton.DoDragDrop(NewButton, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TN_SFC1000 a = new TN_SFC1000();

            a.DisplayName = "테스트 화면명";
            a.Seq = 50;
            a.x = 120;
            a.y = 200;

            ModelService.Insert(a);

            var b = ModelService.GetList(p => true).ToList();

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            int X = P_X = e.Location.X;
            int Y = P_Y = e.Location.Y;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            PX.Text = P_X.ToString();
            PY.Text = P_Y.ToString();
        }






        protected override void InitGrid()
        {
            //GridExControl.SetToolbarVisible(false);
            //GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ToolCode"));
            //GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ToolName"));
            //GridExControl.MainGrid.AddColumn("ItemNameENG", LabelConvert.GetLabelText("ToolNameENG"));
            //GridExControl.MainGrid.AddColumn("ItemNameCHN", LabelConvert.GetLabelText("ToolNameCHN"));
            //GridExControl.MainGrid.AddColumn("MainCustomerCode", LabelConvert.GetLabelText("MainCustomer"));
            //GridExControl.MainGrid.AddColumn("Spec1", LabelConvert.GetLabelText("Spec1"));
            //GridExControl.MainGrid.AddColumn("Spec2", LabelConvert.GetLabelText("Spec2"));
            //GridExControl.MainGrid.AddColumn("Spec3", LabelConvert.GetLabelText("Spec3"));
            //GridExControl.MainGrid.AddColumn("Spec4", LabelConvert.GetLabelText("Spec4"));
            //GridExControl.MainGrid.AddColumn("ToolStockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //GridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"));
            //GridExControl.MainGrid.AddColumn("StockPosition", LabelConvert.GetLabelText("DefaultStockPosition"));
            //GridExControl.MainGrid.AddColumn("ProdFileName", LabelConvert.GetLabelText("ProdFileName"));
            //GridExControl.MainGrid.AddColumn("ProdFileUrl", LabelConvert.GetLabelText("ProdFileUrl"), false);
            //GridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            //GridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            //GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            //GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            //GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ItemName", "ItemNameENG", "ItemNameCHN", "MainCustomerCode", "Spec1", "Spec2", "Spec3", "Spec4", "SafeQty", "StockPosition", "ProdFileName", "UseFlag", "Memo");

            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_STD1100>(GridExControl);
        }

        protected override void InitRepository()
        {
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StockPosition", ModelService.GetChildList<TN_WMS2000>(p => true).ToList(), "PositionCode", Service.Helper.DataConvert.GetCultureDataFieldName("PositionName"),true);
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainCustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            //GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, GridExControl, MasterCodeSTR.FtpFolder_ProdImage, "ProdFileName", "ProdFileUrl", true);
            //GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit.KeyDown += ColumnEdit_KeyDown;
            ////GridExControl.MainGrid.MainView.Columns["ProdFileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit_BAK(UserRight.HasEdit, GridExControl, "ProdFileName", "ProdFileUrl", true);
            //GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", UserRight.HasEdit);
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            //GridExControl.BestFitColumns();
        }

        private void factvieW_MAIN1_MouseEnter(object sender, EventArgs e)
        {
            //lblenter.Text = "IN";
        }

        private void factvieW_MAIN1_MouseLeave(object sender, EventArgs e)
        {
            //lblenter.Text = "OUT";
        }
    }
}