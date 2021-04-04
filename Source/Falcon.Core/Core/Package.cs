using System;
using System.Collections.Generic;
using System.Globalization;
using Falcon.Core.Events;

namespace Falcon.Core
{
    public sealed class Package : ICloneable, IFormattable, IEquatable<Package>
    {
        #region Properties

        public uint Number { get; }

        public string Task { get; }

        public List<Flight> Flights { get; }

        #endregion

        #region Constructors

        public Package()
        {
            Number = GeneratePackageNumber();
            Task = string.Empty;
            Flights = new List<Flight>();
        }

        public Package(uint number)
        {
            Number = number;
            Task = string.Empty;
            Flights = new List<Flight>();
        }

        public Package(uint number, string task)
        {
            Number = number;
            Task = task;
            Flights = new List<Flight>();
        }

        public Package(uint number, string task, IEnumerable<Flight> flights)
        {
            Number = number;
            Task = task;
            Flights = new List<Flight>();
            Flights.AddRange(flights);
        }

        #endregion

        #region Methods

        public static List<Pilot> GetAllPilots(Package package)
        {
            List<Pilot> pilots = new List<Pilot>();

            foreach (Flight packageFlight in package.Flights) pilots.AddRange(packageFlight.Pilots);

            return pilots;
        }

        private static uint GeneratePackageNumber()
        {
            throw new NotImplementedException();
        }

        public void Add(Flight flight)
        {
            Flights.Add(flight);
        }

        public void Add(IEnumerable<Flight> flights)
        {
            Flights.AddRange(flights);
        }

        public bool Remove(Flight flight)
        {
            return Flights.Remove(flight);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Package other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, Task, Flights);
        }

        public Package Clone()
        {
            return new Package(Number, Task, Flights);
        }

        object ICloneable.Clone() => Clone();

        public bool Equals(Package other)
        {
            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            return Number == other.Number && Task == other.Task && Equals(Flights, other.Flights);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, $"PKG #:{Number}, {Task}");
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, $"PKG #:{Number}, {Task}");
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return format == null ? ToString(formatProvider) : string.Format(formatProvider, format, $"PKG #:{Number}, {Task}");
        }

        #endregion
    }

    public delegate void OnPackageChanged(object sender, ValueChangedEventArgs<Package> e);

    public delegate void OnPackagesChanged(object sender, ValuesChangedEventArgs<Package> e);
}