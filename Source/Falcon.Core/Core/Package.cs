using System;
using System.Collections.Generic;

namespace Falcon.Core
{
    public sealed class Package
    {
        #region Properties

        public int Number { get; set; }

        public string Task { get; set; }

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

            foreach (Flight packageFlight in package.Flights)
            {
                pilots.AddRange(packageFlight.Pilots);
            }

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

        #endregion
    }
}