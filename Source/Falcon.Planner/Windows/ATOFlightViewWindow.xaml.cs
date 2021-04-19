using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

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

        internal ATOFlightViewWindow()
        {
            InitializeComponent();

            Title = "Flight - Unassigned";
            FlightCallsignTextBlock.Text = "Unassigned";
        }

        public ATOFlightViewWindow(Flight flight)
        {
            InitializeComponent();

            Flight = flight;

            Title = $"Flight - {Flight.Callsign}";
            FlightCallsignTextBlock.Text = Flight.Callsign;
        }

        #endregion

        #region Methods

        private void ViewPackageCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO: Add logic to check for existing package windows with this particular package in it to activate before opening a new window.

            ATOPackageViewWindow window = new ATOPackageViewWindow();

            window.Show();
        }

        private void ViewDTCCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ViewFlightPlanCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ViewLoadoutCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ViewSquadronCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FlightLeadSlotButton_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FlightWingSlotButton_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ElementLeadSlotButton_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ElementWingSlotButton_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}