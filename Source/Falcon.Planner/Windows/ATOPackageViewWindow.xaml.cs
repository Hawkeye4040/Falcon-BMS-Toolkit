using System.Diagnostics.CodeAnalysis;

using Falcon.Core;

namespace Falcon.Planner.Windows
{
    /// <summary>
    ///     Interaction logic for ATOPackageViewWindow.xaml
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed partial class ATOPackageViewWindow
    {
        #region Properties

        public Package Package { get; private set; }

        #endregion

        #region Constructors

        internal ATOPackageViewWindow()
        {
            InitializeComponent();

            Title = "PKG# N/A";
        }

        public ATOPackageViewWindow(Package package)
        {
            InitializeComponent();

            Package = package;

            Title = $"PKG# {Package.Number}";
        }

        #endregion
    }
}