using GameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agot2Client
{
    static public class ExtRandomDesk
    {
        static public WCFRandomDesk GetRandomDesk(Guid randomDeskId)
        {
            return MainWindow.ClientInfo.WorldData.WCFStaticData.RandomDesk.SingleOrDefault(p => p.Id == randomDeskId);
        }
    }
}
