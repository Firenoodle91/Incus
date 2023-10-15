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
using HKInc.Utils.Class;
using HKInc.Utils.Common;

namespace HKInc.Main
{
    public partial class DatabaseIP_Setting : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        public DatabaseIP_Setting()
        {
            InitializeComponent();
        }

        public DatabaseIP_Setting(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
        }

        protected override void InitToolbarButton()
        {
            base.InitToolbarButton();

            SetToolbarVisible(false);
        }

        protected override void InitControls()
        {
            base.InitControls();
            SetMessage(string.Format("현재 Setting IP : {0}", GlobalVariable.DatabaseIP));
            simpleButton1.Click += SimpleButton1_Click;
            simpleButton2.Click += SimpleButton2_Click;
        }

        protected override void InitCombo()
        {
            MasterCode.Reset();
            lupDatabase.SetDefault(false, "Property1", "Property1", MasterCode.GetMasterCode((int)Service.Factory.MasterCodeEnum.DatabaseIP).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lupDatabase.AddColumnDisplay("CodeName", 1, true);            
            lupDatabase.ColumnCpationChange("CodeName", "IP명");
            lupDatabase.ColumnCpationChange("Property1", "IP정보");
            lupDatabase.Columns["Property1"].VisibleIndex = 2;
            lupDatabase.ShowColumnHeaders(true);
            lupDatabase.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            lupDatabase.EditValue = GlobalVariable.DatabaseIP;
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            var DatabaseIP = lupDatabase.EditValue.GetNullToEmpty();
            if (IsServerConnected(DatabaseIP))
            {
                this.DialogResult = DialogResult.OK;
                GlobalVariable.DatabaseIP = DatabaseIP;
                ServerInfo.Server = DatabaseIP;
                ServerInfo.ConnectStringListChange();
                HKInc.Service.Handler.MessageBoxHandler.Show("적용 완료");
                Close();
            }
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        public bool IsServerConnected(string DatabaseIP = null)
        {
            if (string.IsNullOrEmpty(DatabaseIP))
            {
                
                List<string> ConnectionStringList = new List<string>();
                var ConnectionString1 = string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", ServerInfo.Server, ServerInfo.Database, ServerInfo.UserId, ServerInfo.Password);
                var ConnectionString2 = string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", ServerInfo.Server, ServerInfo.ProductionDatabase, ServerInfo.UserId, ServerInfo.Password);
                var ConnectionString3 = string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", ServerInfo.Server, ServerInfo.FileStreamDatabase, ServerInfo.UserId, ServerInfo.Password);
                
                ConnectionStringList.Add(ConnectionString1);
                ConnectionStringList.Add(ConnectionString2);
                ConnectionStringList.Add(ConnectionString3);
               

                foreach (var ConnectionString in ConnectionStringList)
                {
                    using (var l_oConnection = new System.Data.SqlClient.SqlConnection(ConnectionString))
                    {
                        try
                        {
                            WaitHandler.ShowWait();
                            l_oConnection.Open();
                        }
                        catch (System.Data.SqlClient.SqlException e)
                        {
                            HKInc.Service.Handler.MessageBoxHandler.ErrorShow(e.Message);
                            return false;
                        }
                        finally
                        {
                            WaitHandler.CloseWait();
                        }
                    }
                }

                return true;
            }
            else
            {
                List<string> ConnectionStringList = new List<string>();
                var ConnectionString1 = string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", DatabaseIP, ServerInfo.Database, ServerInfo.UserId, ServerInfo.Password);
                var ConnectionString2 = string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", DatabaseIP, ServerInfo.ProductionDatabase, ServerInfo.UserId, ServerInfo.Password);
                var ConnectionString3 = string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", DatabaseIP, ServerInfo.FileStreamDatabase, ServerInfo.UserId, ServerInfo.Password);
            
                ConnectionStringList.Add(ConnectionString1);
                ConnectionStringList.Add(ConnectionString2);
                ConnectionStringList.Add(ConnectionString3);
            

                foreach (var ConnectionString in ConnectionStringList)
                {
                    using (var l_oConnection = new System.Data.SqlClient.SqlConnection(ConnectionString))
                    {
                        try
                        {
                            WaitHandler.ShowWait();
                            l_oConnection.Open();
                        }
                        catch (System.Data.SqlClient.SqlException e)
                        {
                            HKInc.Service.Handler.MessageBoxHandler.ErrorShow(e.Message);
                            return false;
                        }
                        finally
                        {
                            WaitHandler.CloseWait();
                        }
                    }
                }

                return true;
            }
        }
    }
}