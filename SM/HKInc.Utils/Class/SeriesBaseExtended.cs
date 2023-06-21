using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraCharts;

namespace HKInc.Utils.Class
{
    public static class SeriesBaseExtended
    {

        public static void ChangeViewType(this SeriesBase series, ViewType viewType)
        {
            series.ChangeView(viewType);
        }
    }
}
