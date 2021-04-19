using System.Diagnostics.CodeAnalysis;

using Falcon.Core;

namespace Falcon.Planner.Windows
{
    /// <summary>
    ///     Interaction logic for ATOFlightViewWindow.xaml
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed partial class ATOFlightViewWindow
    {
        #region Properties

        public Flight Flight { get; private set; }

        #endregion

        #region Constructors

        public ATOFlightViewWindow()
        {
            InitializeComponent();
        }

        public ATOFlightViewWindow(Flight flight)
        {
            InitializeComponent();

            Flight = flight;
        }

        #endregion
    }
}