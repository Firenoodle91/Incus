﻿using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using System.Data;
using System.Linq;

namespace HKInc.Ui.View.SYS
{
    /// <summary>
    /// 20220422 오세완 차장
    /// TP보고용 TP보고서
    /// </summary>
    public partial class TPReport : HKInc.Service.Base.ListFormTemplate
    {
        IService<FieldLabel> LabelTextService = (IService<FieldLabel>)ServiceFactory.GetDomainService("FieldLabel");

        public TPReport()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();          
        }
       
        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("공급기업");
            GridExControl.MainGrid.AddColumn("도입기업");
            GridExControl.MainGrid.AddColumn("솔루션구분");
            GridExControl.MainGrid.AddColumn("사용자");
            GridExControl.MainGrid.AddColumn("접속IP");
            GridExControl.MainGrid.AddColumn("기능명", false);
            GridExControl.MainGrid.AddColumn("접속일시");
            GridExControl.MainGrid.AddColumn("종료일시");
            GridExControl.MainGrid.AddColumn("데이터크기(Mb)");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("접속일시");
            GridExControl.MainGrid.SetRepositoryItemFullDateTimeEdit("종료일시");
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            DataSet ds = DbRequestHandler.GetDataQury("exec SP_TP_REPORT '큐맥'");
            if(ds != null)
                if(ds.Tables.Count > 0)
                {
                    GridBindingSource.DataSource = ds.Tables[0];
                }
            
            GridExControl.DataSource = GridBindingSource;
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);

            GridExControl.BestFitColumns();
        }
    }
}