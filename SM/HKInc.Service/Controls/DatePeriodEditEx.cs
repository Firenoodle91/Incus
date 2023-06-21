using HKInc.Utils.Class;
using System;
using System.ComponentModel;
using System.Drawing;


namespace HKInc.Service.Controls
{
    [ToolboxItem(true)]
    public partial class DatePeriodEditEx : DevExpress.XtraEditors.XtraUserControl
	{		
		public DatePeriodEditEx()
		{
			InitializeComponent();
			Init();
		}

		public DateEditEx DateFrEdit
		{
			get { return this.datDateFr; }
			set { this.datDateFr = value; }
		}

		public DateEditEx DateToEdit
		{
			get { return this.datDateTo; }
			set { this.datDateTo = value; }
		}

		public object EditFrValue
		{
			get { return this.datDateFr.EditValue; }
			set { this.datDateFr.EditValue = value; }
		}

		public object EditToValue
		{
			get { return this.datDateTo.EditValue; }
			set { this.datDateTo.EditValue = value; }
		}

		public void Init()
		{
			this.datDateFr.Init();
			this.datDateTo.Init();

            datDateTo.EditValue = (DateTime.Today).GetFullTimeToDateTime();
            datDateFr.EditValue = DateTime.Today.AddMonths(-1);

			datDateTo.EditValueChanged += datDateTo_EditValueChanged;
            datDateFr.EditValueChanged += datDateFr_EditValueChanged;
        }

        public void SetDisableBaseEditValueChanged()
        {
            datDateTo.EditValueChanged -= datDateTo_EditValueChanged;
            datDateFr.EditValueChanged -= datDateFr_EditValueChanged;
        }

        /// <summary>
        /// 오늘기준 한달로 날짜 초기화
        /// </summary>
        public void SetTodayIsMonth(int Month = 1)
        {
            datDateTo.EditValue = (DateTime.Today).GetFullTimeToDateTime();
            datDateFr.EditValue = DateTime.Today.AddMonths(-Month);
        }
        /// <summary>
        /// 오늘기준 한달로 1주일 초기화
        /// </summary>
        public void SetTodayIsWeek()
        {
            datDateTo.EditValue = (DateTime.Today).GetFullTimeToDateTime();
            datDateFr.EditValue = DateTime.Today.AddDays(-6);
        }
        /// <summary>
        /// 오늘기준 한달로 [변수]일 초기화
        /// </summary>
        public void SetTodayIsDay(int Day = 3)
        {
            datDateTo.EditValue = (DateTime.Today).GetFullTimeToDateTime();
            datDateFr.EditValue = DateTime.Today.AddDays(-Day);
        }

        void datDateFr_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(datDateFr.Text)) return;
            if (string.IsNullOrEmpty(datDateTo.Text)) return;
                        
            DateTime fromDate = (DateTime)datDateFr.EditValue;
            DateTime toDate = (DateTime)datDateTo.EditValue;

            if (fromDate > toDate)            
                datDateTo.EditValue = fromDate;         
        }

        void datDateTo_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(datDateFr.Text)) return;
            if (string.IsNullOrEmpty(datDateTo.Text)) return;

            if (!datDateFr.ToString().Equals(string.Empty))
            {                
                DateTime Fr = (DateTime)datDateFr.EditValue;
                DateTime To = (DateTime)datDateTo.EditValue;
                if (Fr > To)                
                    datDateTo.EditValue = Fr;                
            }
            else
            {
                datDateFr.EditValue = DateTime.Parse(datDateTo.SelectedText);
            }
        }
        
		public void Clear()
		{
			this.datDateFr.EditValue = null;
			this.datDateTo.EditValue = null;
		}
        
        private Size maxSize = new Size(200, 20);

		public override Size MaximumSize
		{
			get { return base.MaximumSize; }
			set { base.MaximumSize = maxSize; }
		}

		public void SetEnable(bool bEnable = false, Color? backColor = null)
		{			
			datDateFr.SetEnable(bEnable);
			datDateTo.SetEnable(bEnable);
		}        
	}
}
