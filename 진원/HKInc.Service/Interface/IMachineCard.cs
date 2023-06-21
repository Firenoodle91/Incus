using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Interface
{
    public interface IMachineCard
    {
        void SetPicture(byte[] picture);
        void SetWorkerPicture(byte[] picture);
    //    void SetCardData(HKInc.Ui.Model.Domain.MachineMonitoring cardData);
        void SetNavigationFrame(INaviFrame naviFrame);
        string MachineCode { get; }
    }
}
