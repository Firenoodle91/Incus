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
using DevExpress.XtraCharts;

namespace HKInc.Ui.View.REPORT
{
    public partial class CombineBoard : HKInc.Service.Base.BaseForm
    {
        public CombineBoard()
        {
            InitializeComponent();
        }

        private DataTable CreateChartData()
        {
            // Create an empty table. 
            DataTable table = new DataTable("Table1");

            // Add three columns to the table. 
            table.Columns.Add("Month", typeof(String));
            table.Columns.Add("Section", typeof(String));
            table.Columns.Add("Value", typeof(Int32));

            // Add data rows to the table. 
            table.Rows.Add(new object[] { "Jan", "Section1", 10 });
            table.Rows.Add(new object[] { "Jan", "Section2", 20 });
            table.Rows.Add(new object[] { "Feb", "Section1", 20 });
            table.Rows.Add(new object[] { "Feb", "Section2", 30 });
            table.Rows.Add(new object[] { "March", "Section1", 15 });
            table.Rows.Add(new object[] { "March", "Section2", 25 });

            return table;
        }

        protected override void DataLoad()
        {
            // Generate a data table and bind the chart to it. 
            chartControl1.DataSource = CreateChartData();

            // Specify data members to bind the chart's series template. 
            chartControl1.SeriesDataMember = "Month";
            chartControl1.SeriesTemplate.ArgumentDataMember = "Section";
            chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Value" });

            // Specify the template's series view. 
            chartControl1.SeriesTemplate.View = new StackedBarSeriesView();

            // Specify the template's name prefix. 
            chartControl1.SeriesNameTemplate.BeginText = "Month: ";

        }
    }
}