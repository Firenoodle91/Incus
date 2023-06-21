using DevExpress.XtraCharts;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Helper;
using System.Data.SqlClient;

namespace HKInc.Service.Helper
{
    public class ChartHelper
    {
        private static readonly int CountForEachPage = 10;

        public static void SetModelValue(object model, string propertyNameName, double value, string valueString)
        {
            PropertyInfo dayPropertyInfo;
            PropertyInfo dayValuePropertyInfo;

            dayPropertyInfo = model.GetType().GetProperty(propertyNameName);
            dayPropertyInfo.SetValue(model, valueString);

            dayValuePropertyInfo = model.GetType().GetProperty($"{propertyNameName}Value");
            dayValuePropertyInfo.SetValue(model, value);
        }

        public static double GetModelValue(object model, string propertyNameName)
        {
            PropertyInfo dayPropertyInfo;

            dayPropertyInfo = model.GetType().GetProperty(propertyNameName);
            return dayPropertyInfo.GetValue(model).GetDoubleNullToZero();
        }

    

      



   

     

        public static Cursor GetCursor(Bitmap bmp, int width = 80, int height = 80)
        {
            return HKInc.Service.Helper.CustomCursor.GetCursor(bmp, width, height);
        }

        public static void SetVisualRange(XYDiagram Diagram, BindingList<DiagChartObject> sourceList)
        {
            double badValue = Diagram.AxisY.ConstantLines[0].AxisValue.GetDoubleNullToZero();
            //double goodValue = Diagram.AxisY.ConstantLines[2].AxisValue.GetDoubleNullToZero();
            double maxValue = sourceList.Max(p => p.DiagValue).GetDoubleNullToZero();
            double minValue = sourceList.Min(p => p.DiagValue).GetDoubleNullToZero();

            double setMaxValue = badValue;

            if (badValue == 0)
                setMaxValue = maxValue;
            else
                setMaxValue = badValue > maxValue ? badValue : maxValue;

            double setMinValue = -badValue;

            if (badValue == 0)
                setMinValue = minValue;
            else
                setMinValue = -badValue < minValue ? -badValue : minValue;

            ChartHelper.SetVisualRange(Diagram, setMaxValue, setMinValue);
        }

   
        private static void SetVisualRange(XYDiagram Diagram, double MaxValue, double MinValue)
        {
            Diagram.AxisY.VisualRange.Auto = false;
            Diagram.AxisY.WholeRange.Auto = false;

            Diagram.AxisY.WholeRange.MaxValue = MaxValue + 0.1;
            Diagram.AxisY.VisualRange.MaxValue = MaxValue + 0.1;
            Diagram.AxisY.WholeRange.MinValue = MinValue - 0.1;
            Diagram.AxisY.VisualRange.MinValue = MinValue - 0.1;
        }

   




        //public class MachineStateDailyTodayView
        //{
        //    public Nullable<DateTime> WorkDate;
        //    public string MachineCode;
        //    public int OnMinute;
        //    //public Nullable<DateTime> OnDate;
        //    //public Nullable<DateTime> OnDateTime;
        //    //public Nullable<DateTime> OffDate;
        //    //public Nullable<DateTime> OffDateTime;
        //    //public Nullable<decimal> StateId;
        //    //public int StateGroup;
        //    public Nullable<decimal> StateCode;
        //    //public string StateName;
        //    //public int StateOrder;
        //    //public string ProductCode;
        //    public string Process;
        //    public string SicGroup;
        //}
    }
}
