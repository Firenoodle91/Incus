using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.POP_Popup
{
    /// <summary>
    /// POP실적등록창
    /// </summary>
    public partial class XPFQTYIN : XtraForm
    {
        IService<TN_MPS1401> ModelService = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");
        IService<TN_MPS1404> ModelServiceDTL = (IService<TN_MPS1404>)ProductionFactory.GetDomainService("TN_MPS1404");
        DateTime? dt;
        TP_POPJOBLIST lsobj;
        string ProcessName;
        public XPFQTYIN()
        {
            InitializeComponent();
        }
        public XPFQTYIN(TP_POPJOBLIST obj, string lotno, string processName)
        {
            InitializeComponent();
            ProcessName = processName;
            lsobj = obj;
            if (lotno == "")
            {
                TN_MPS1401 lot = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process).OrderByDescending(o => o.Seq).FirstOrDefault();
                lotno = lot.LotNo;
                dt = lot.ResultDate;
            }
         
            tx_lot.Text = lotno;
                        
            TN_MPS1401 tn = ModelService.GetList(p =>  p.WorkNo == obj.WorkNo
                                                    && p.ProcessCode == obj.Process
                                                    && p.LotNo == lotno
                                                )
                                                .OrderBy(o => o.LotNo)
                                                .FirstOrDefault();

            tx_totqty.Text = tn.ResultQty.ToString(); 
            tx_totokqty.Text = tn.OkQty.ToString();
            tx_totbad.Text = tn.FailQty.ToString();
            lupWork.SetDefault(true, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor, DevExpress.Utils.HorzAlignment.Center);
            lup_fail.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.QCFAIL), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor, DevExpress.Utils.HorzAlignment.Center);
            lupWork.EditValue = Utils.Common.GlobalVariable.LoginId;
        }

        //적용
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int qty = tx_qty.EditValue.GetIntNullToZero();
            int badqty = tx_bad.EditValue.GetIntNullToZero();

            if (lupWork.RequireCheck())
            {
                //MessageBox.Show("작업자를 선택하세요.");
                return;
            }
            if (badqty > 0 && lup_fail.EditValue.GetNullToEmpty() == "")
            {
                MessageBox.Show("불량 수량이 존재합니다. 불량유형을 선택하세요.", "경고");
                return;
            }

            TN_MPS1401 tn = ModelService.GetList(p =>  p.WorkNo == lsobj.WorkNo
                                                    && p.ProcessCode == lsobj.Process 
                                                    && p.LotNo == tx_lot.EditValue.ToString()
                                                )
                                                .OrderBy(o => o.LotNo)
                                                .FirstOrDefault();
            tn.ResultQty = tn.ResultQty.GetIntNullToZero() + qty;
            tn.OkQty = tn.OkQty.GetIntNullToZero() + qty - badqty;
            tn.FailQty = tn.FailQty.GetIntNullToZero() + badqty;
            tn.EndDate = DateTime.Now;
            if (dt == null)
            {
                tn.ResultDate = DateTime.Today;
            }
            else
            {
                tn.ResultDate = dt;
            }
            tn.WorkId = lupWork.EditValue.ToString();
            ModelService.Update(tn);

            if (badqty > 0)
            {
                TN_MPS1404 nobj = new TN_MPS1404()
                {
                    WorkDate = tn.WorkDate,
                    ResultDate = tn.ResultDate,
                    WorkNo = tn.WorkNo,
                    Seq = DbRequesHandler.GetRowCount("exec SP_MPS1404_CNT '" + tn.WorkDate + "','" + tn.WorkNo + "','" + tn.ProcessCode + "'") == 0 ? 1 : DbRequesHandler.GetRowCount("exec SP_MPS1404_CNT '" + tn.WorkDate + "','" + tn.WorkNo + "','" + tn.ProcessCode + "'") + 1,
                    ProcessCode=tn.ProcessCode,
                    LotNo=tn.LotNo,
                    FailQty = badqty,
                    FaleType = lup_fail.EditValue.GetNullToEmpty(),
                    ItemCode=lsobj.ItemCode
                    
                };
                ModelServiceDTL.Insert(nobj);
                ModelServiceDTL.Save();
            }
            ModelService.Save();

            #region 자재,칼,금형 투입량 

            if (lsobj.PSeq == 1)
            {
                MaterialUsingQty(lsobj, qty, 1);
            }
            else if (lsobj.Process == MasterCodeSTR.ProcessPacking
                || lsobj.Process == MasterCodeSTR.ProcessCutToPacking
                || lsobj.Process == MasterCodeSTR.ProcessMakeToCutToPacking)
            {
                MaterialUsingQty(lsobj, qty, 2);

                #region 포장 로직 변경 19.09.23

                PackLabelPrint();

                #endregion
            }

            #endregion


            DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 자재 투입소요량 로직
        /// </summary>
        /// <param name="lsobj">작지</param>
        /// <param name="Qty">생산수량</param>
        /// <param name="Division">구분 1: 자재,금형  2: 칼</param>
        private void MaterialUsingQty(TP_POPJOBLIST lsobj, int Qty, int Division)
        {
            //투입정보불러오기.
            var TN_LOT_MST_Obj = ModelService.GetChildList<TN_LOT_MST>(p => p.WorkNo == lsobj.WorkNo && p.LotNo == tx_lot.EditValue.ToString()).First();

            if (Division == 1)
            {
                var SrcLotList = new List<ItemCodeToLotNoModel>();
                SrcLotList.Add(new ItemCodeToLotNoModel() { ItemCode = TN_LOT_MST_Obj.SrcCode, LotNo = TN_LOT_MST_Obj.SrcLot });
                SrcLotList.Add(new ItemCodeToLotNoModel() { ItemCode = TN_LOT_MST_Obj.SrcCode1, LotNo = TN_LOT_MST_Obj.SrcLot1 });
                SrcLotList.Add(new ItemCodeToLotNoModel() { ItemCode = TN_LOT_MST_Obj.SrcCode2, LotNo = TN_LOT_MST_Obj.SrcLot2 });
                SrcLotList.Add(new ItemCodeToLotNoModel() { ItemCode = TN_LOT_MST_Obj.SrcCode3, LotNo = TN_LOT_MST_Obj.SrcLot3 });
                SrcLotList.Add(new ItemCodeToLotNoModel() { ItemCode = TN_LOT_MST_Obj.SrcCode4, LotNo = TN_LOT_MST_Obj.SrcLot4 });
                SrcLotList.Add(new ItemCodeToLotNoModel() { ItemCode = TN_LOT_MST_Obj.SrcCode5, LotNo = TN_LOT_MST_Obj.SrcLot5 });
                SrcLotList.Add(new ItemCodeToLotNoModel() { ItemCode = TN_LOT_MST_Obj.SrcCode6, LotNo = TN_LOT_MST_Obj.SrcLot6 });
                SrcLotList.Add(new ItemCodeToLotNoModel() { ItemCode = TN_LOT_MST_Obj.SrcCode7, LotNo = TN_LOT_MST_Obj.SrcLot7 });
                SrcLotList = SrcLotList.Where(p => string.IsNullOrEmpty(p.LotNo) == false).OrderBy(p => p.LotNo).ToList();

                //var ItemCodeGroupArray = SrcLotList.Select(p => p.ItemCode).Distinct().ToArray();
                //var ItemList = ModelService.GetChildList<TN_STD1100>(p => ItemCodeGroupArray.Contains(p.ItemCode)).ToList();
                var SrcQty = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == lsobj.ItemCode).First().SrcQty.GetDecimalNullToZero();
                foreach (var v in SrcLotList)
                {
                    //원소재소요량(기준정보)
                    //var SrcQty = ItemList.Where(p => p.ItemCode == v.ItemCode).First().SrcQty.GetDecimalNullToZero();
                    //소요량 로직 
                    var SpendQty = SrcQty * Qty;
                    var CheckObj = ModelService.GetChildList<TN_MPS1405>(p => p.WorkNo == lsobj.WorkNo && p.LotNo == tx_lot.EditValue.ToString() && p.InLotNo == v.LotNo).FirstOrDefault();
                    if(CheckObj != null)
                    {
                        CheckObj.InQty += SpendQty;
                        ModelService.UpdateChild(CheckObj);
                    }
                    else
                    {
                        ModelService.InsertChild(new TN_MPS1405()
                        {
                            InLotNo = v.LotNo,
                            WorkNo = lsobj.WorkNo,
                            LotNo = tx_lot.EditValue.ToString(),
                            InQty = SpendQty
                        });
                    }
                }

                //금형 샷수 UPDATE
                
                var ItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == lsobj.ItemCode 
                                                                            && string.IsNullOrEmpty(p.MoldCode) == false).FirstOrDefault();
                if (ItemObj != null)
                {
                    var MoldList = ModelService.GetChildList<TN_MOLD001>(p => p.MoldCode == ItemObj.MoldCode).ToList();
                    foreach (var v in MoldList)
                    {
                        if (v.XCase.GetIntNullToZero() <= 0) continue;
                        var ShotCnt = Qty / v.XCase;
                        v.RealShotcnt = v.RealShotcnt.GetDecimalNullToZero() + ShotCnt;
                        v.SumShotcnt = v.SumShotcnt.GetDecimalNullToZero() + ShotCnt;
                        ModelService.UpdateChild(v);

                        var CheckObj = ModelService.GetChildList<TN_MPS1407>(p => p.WorkNo == lsobj.WorkNo && p.LotNo == tx_lot.EditValue.ToString() && p.MoldCode == v.MoldCode).FirstOrDefault();
                        if (CheckObj != null)
                        {
                            CheckObj.ShotCnt += ShotCnt.GetDecimalNullToZero();
                            ModelService.UpdateChild(CheckObj);
                        }
                        else
                        {
                            ModelService.InsertChild(new TN_MPS1407()
                            {
                                MoldCode = v.MoldCode,
                                WorkNo = lsobj.WorkNo,
                                LotNo = tx_lot.EditValue.ToString(),
                                ShotCnt = ShotCnt.GetDecimalNullToZero()
                            });
                        }
                    }
                }
                ModelService.Save();
            }
            else if(Division == 2)
            {
                var KnifeCodeList = new List<string>();
                KnifeCodeList.Add(TN_LOT_MST_Obj.KnifeCode1);
                KnifeCodeList.Add(TN_LOT_MST_Obj.KnifeCode2);
                KnifeCodeList.Add(TN_LOT_MST_Obj.KnifeCode3);
                KnifeCodeList.Add(TN_LOT_MST_Obj.KnifeCode4);
                KnifeCodeList.Add(TN_LOT_MST_Obj.KnifeCode5);
                KnifeCodeList.Add(TN_LOT_MST_Obj.KnifeCode6);
                KnifeCodeList.Add(TN_LOT_MST_Obj.KnifeCode7);
                KnifeCodeList = KnifeCodeList.Where(p => string.IsNullOrEmpty(p) == false).OrderBy(p => p).ToList();

                //칼 샷수 UPDATE
                var KnifeList = ModelService.GetChildList<TN_KNIFE001>(p => KnifeCodeList.Contains(p.KnifeCode)).ToList();
                foreach (var v in KnifeList)
                {
                    if (v.XCase.GetIntNullToZero() <= 0) continue;
                    var ShotCnt = Qty / v.XCase;
                    v.RealShotcnt += ShotCnt;
                    v.SumShotcnt += ShotCnt;
                    ModelService.UpdateChild(v);

                    var CheckObj = ModelService.GetChildList<TN_MPS1406>(p => p.WorkNo == lsobj.WorkNo && p.LotNo == tx_lot.EditValue.ToString() && p.KnifeCode == v.KnifeCode).FirstOrDefault();
                    if (CheckObj != null)
                    {
                        CheckObj.ShotCnt += ShotCnt.GetDecimalNullToZero();
                        ModelService.UpdateChild(CheckObj);
                    }
                    else
                    {
                        ModelService.InsertChild(new TN_MPS1406()
                        {
                            KnifeCode = v.KnifeCode,
                            WorkNo = lsobj.WorkNo,
                            LotNo = tx_lot.EditValue.ToString(),
                            ShotCnt = ShotCnt.GetDecimalNullToZero()
                        });
                    }
                }
                ModelService.Save();
            }
        }

        
        private bool PackLabelPrint()
        {
            // 포장 로직 변경 19.09.23
            if (lsobj == null) return false;

            IService<TN_MPS1401> Service = (IService<TN_MPS1401>)ProductionFactory.GetDomainService("TN_MPS1401");
            var tn1401 = Service.GetList(p => p.WorkNo == lsobj.WorkNo && p.ProcessCode == lsobj.Process).OrderBy(o => o.Seq).LastOrDefault();
            if (tn1401 == null)
            {
                MessageBoxHandler.Show("출력할 실적이 없습니다.", "경고");
                return false;
            }

            if (tn1401.LotNo.GetNullToEmpty() == "")
            {
                MessageBoxHandler.Show("출력할 LOT NO가 없습니다.", "경고");
                return false;
            }
            else if (tn1401.ResultQty.GetIntNullToZero() == 0)
            {
                MessageBoxHandler.Show("출력할 생산 수량이 없습니다.", "경고");
                return false;
            }
            else
            {
                var ItemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == lsobj.ItemCode).First();

                //TN_MPS1401 tn = ModelService.GetList(p => p.WorkNo == obj.WorkNo && p.ProcessCode == obj.Process && p.LotNo == tn1401.LotNo).FirstOrDefault();

                int qty = tx_qty.EditValue.GetIntNullToZero();

                POP_Popup.XPFPACKLABEL form = new POP_Popup.XPFPACKLABEL(qty, tn1401.LotNo, ItemObj.ItemNm1, ItemObj.ItemNm, tn1401, ItemObj.StdPackQty.GetIntNullToZero());
                var value = form.ShowDialog();
                if (value == DialogResult.OK) return true;
                else return false;
            }
        }

        //취소
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tx_qty_Click(object sender, EventArgs e)
        {
            TextEditKeyPad(sender);
        }

        private void tx_bad_Click(object sender, EventArgs e)
        {
            TextEditKeyPad(sender);
        }

        private void TextEditKeyPad(object sender)
        {
            var textEdit = sender as TextEdit;
            if (textEdit == null) return;
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            var keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            textEdit.Text = keypad.returnval.IsNullOrEmpty() ? "0" : keypad.returnval;
            textEdit.BeginInvoke(new MethodInvoker(delegate {
                //txtPassword.SelectionLength = txtPassword.Text.Length;
                textEdit.SelectionStart = textEdit.Text.Length;
            }));
        }

        class ItemCodeToLotNoModel
        {
            public string ItemCode { get; set; }
            public string LotNo { get; set; }
        }
    }
}
