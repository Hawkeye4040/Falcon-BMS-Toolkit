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
}