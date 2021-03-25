using System;
using System.Collections.Generic;
using System.Linq;

namespace Falcon.Core
{
    public sealed class Package : ICloneable, IFormattable, IEquatable<Package>
    {
        #region Properties

        public int Number { get; }

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

        public Package(int number)
        {
            Number = number;
            Task = string.Empty;
            Flights = new List<Flight>();
        }

        public Package(int number, string task)
        {
            Number = number;
            Task = task;
            Flights = new List<Flight>();
        }

        public Package(int number, string task, IEnumerable<Flight> flights)
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

        private static int GeneratePackageNumber()
        {
            throw new NotImplementedException();
        }

        public void Add(Flight flight)
        {
            Flights.Add(flight);
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

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Package other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Number == other.Number && Task == other.Task && Equals(Flights, other.Flights);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return $"PKG #:{Number}, {Task}";
        }

        #endregion
    }

    public sealed class PackageChangedEventArgs : EventArgs
    {
        #region Properties

        public Package OldPackage { get; }

        public Package NewPackage { get; }

        #endregion

        #region Constructors

        public PackageChangedEventArgs(Package oldPackage, Package newPackage)
        {
            OldPackage = oldPackage;
            NewPackage = newPackage;
        }

        #endregion
    }

    public sealed class PackagesChangedEventArgs : EventArgs
    {
        #region Properties

        public List<Package> OldPackages { get; }

        public List<Package> NewPackages { get; }

        public List<Package> AddedPackages => NewPackages.Where(package => !OldPackages.Contains(package)).ToList();

        public List<Package> RemovedPackages => OldPackages.Where(package => !NewPackages.Contains(package)).ToList();

        #endregion

        #region Constructors

        public PackagesChangedEventArgs(IEnumerable<Package> oldPackages, IEnumerable<Package> newPackages)
        {
            OldPackages = new List<Package>();
            OldPackages.AddRange(oldPackages);
            NewPackages = new List<Package>();
            NewPackages.AddRange(newPackages);
        }

        #endregion
    }

    public delegate void OnPackageChanged(object sender, PackageChangedEventArgs e);

    public delegate void OnPackagesChanged(object sender, PackagesChangedEventArgs e);
}