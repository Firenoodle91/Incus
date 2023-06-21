using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HKInc.Service.Interface
{
    public interface IBallBarCard
    {
        void SetPicture(byte[] picture);
       // void SetCardData(HKInc.Ui.Model.Domain.BallBarState cardData);
      //  void SetCardData(BallBarState cardData, BallBarCardData cardData2);
        void SetNavigationFrame(INaviFrame naviFrame);
        string MachineCode { get; }
    }
}
