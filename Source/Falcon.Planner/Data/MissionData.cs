using System.Collections.Generic;

using Falcon.Core;

namespace Falcon.Planner.Data
{
    public sealed class MissionData
    {
        #region Fields

        private static readonly List<string> FileFormats = new List<string>
        {
            ".tac",
            ".cam"
        };

        #endregion

        #region Properties

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Package> Packages { get; }

        public string FileName { get; set; }

        #endregion

        #region Constructors

        public MissionData()
        {
            Packages = new List<Package>();
        }

        public MissionData(IEnumerable<Package> packages)
        {
            Packages = new List<Package>();
            Packages.AddRange(packages);
        }

        #endregion
    }
}