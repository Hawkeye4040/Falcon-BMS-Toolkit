using Falcon.Core.Events;

namespace Falcon.Core
{
    public abstract class MilitaryUnit : Entity
    {
        
    }

    public abstract class AirUnit : MilitaryUnit
    {

    }

    public abstract class NavalUnit : MilitaryUnit
    {

    }

    public abstract class GroundUnit : MilitaryUnit
    {

    }

    public sealed class Division : GroundUnit
    {

    }

    public sealed class Brigade : GroundUnit
    {

    }

    public sealed class Battalion : GroundUnit
    {

    }

    public sealed class TaskForce : NavalUnit
    {

    }

    public sealed class Squadron : AirUnit
    {

    }

    public delegate void OnMilitaryUnitChanged(object sender, ValueChangedEventArgs<MilitaryUnit> e);

    public delegate void OnMilitaryUnitsChanged(object sender, ValuesChangedEventArgs<MilitaryUnit> e);
}