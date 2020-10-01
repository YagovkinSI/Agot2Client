using GameService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Agot2Client
{

    public interface IPosition
    {
        WCFGamePoint Position { get; set; }
        void OnPropertyChanged(string propName);
    }
}
