using System.Diagnostics.CodeAnalysis;

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
}