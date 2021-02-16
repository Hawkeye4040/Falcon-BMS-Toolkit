using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Falcon.Core
{
    public abstract class Ordnance
    {
    }

    public sealed class OrdnanceRack : Ordnance
    {
    }

    public sealed class Bomb : Ordnance
    {
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class AGM : Ordnance
    {
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class AAM : Ordnance
    {
    }

    // TODO: Create a class for TGP's, Fuel Tanks and objects falling in the "other" category.

    public sealed class OrdnanceChangedEventArgs : EventArgs
    {
        #region Properties

        public Ordnance OldOrdnance { get; }

        public Ordnance NewOrdnance { get; }

        #endregion

        #region Constructors

        public OrdnanceChangedEventArgs(Ordnance oldOrdnance, Ordnance newOrdnance)
        {
            OldOrdnance = oldOrdnance;
            NewOrdnance = newOrdnance;
        }

        #endregion
    }

    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public sealed class OrdnancesChagnedEventArgs : EventArgs
    {
        public List<Ordnance> OldOrdnance { get; }

        public List<Ordnance> NewOrdnance { get; }

        public List<Ordnance> AddedOrdnance => NewOrdnance.Where(ordnance => !OldOrdnance.Contains(ordnance)).ToList();

        public List<Ordnance> RemovedOrdnance =>
            OldOrdnance.Where(ordnance => !NewOrdnance.Contains(ordnance)).ToList();

        public OrdnancesChagnedEventArgs(IEnumerable<Ordnance> oldOrdnance, IEnumerable<Ordnance> newOrdnance)
        {
            OldOrdnance = new List<Ordnance>();
            OldOrdnance.AddRange(oldOrdnance);
            NewOrdnance = new List<Ordnance>();
            NewOrdnance.AddRange(newOrdnance);
        }
    }

}