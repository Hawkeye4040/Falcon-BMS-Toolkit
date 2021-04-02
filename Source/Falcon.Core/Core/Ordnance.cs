using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Falcon.Core.Events;

namespace Falcon.Core
{
    public abstract class Ordnance
    {
    }

    public sealed class OrdnanceRack : Ordnance
    {
        private List<Ordnance> Ordnance { get; }

        public Ordnance this[int index]
        {
            get => Ordnance[index];
            set => Ordnance.Insert(index, value);
        }
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

    public delegate void OnOrdnanceChanged(object sender, ValuesChangedEventArgs<Ordnance> e);
}