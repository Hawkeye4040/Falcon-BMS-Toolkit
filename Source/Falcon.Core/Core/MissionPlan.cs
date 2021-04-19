using System;
using System.Threading.Tasks;

namespace Falcon.Core
{
    public sealed class MissionPlan
    {
        #region Methods

        public static async Task<bool> Load(string filePath)
        {
            Uri uri = new Uri(filePath, UriKind.Absolute);

            bool successful = await Load(uri);

            return successful;
        }

        public static async Task<bool> Load(Uri uri)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Save(string filePath)
        {
            Uri uri = new Uri(filePath, UriKind.Absolute);

            bool successful = await Save(uri);

            return successful;
        }

        public async Task<bool> Save(Uri filePath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}