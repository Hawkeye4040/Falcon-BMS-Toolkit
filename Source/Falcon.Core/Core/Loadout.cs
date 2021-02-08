using System;
using System.Collections.Generic;

namespace Falcon.Core
{
    public sealed class Loadout
    {
        #region Properties

        public List<Station> Stations { get; set; }

        #endregion

        #region Methods

        public static List<Ordnance> GetOrdnance(Loadout loadout)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public sealed class Station
    {
    }
}