using System.Windows.Input;

namespace Falcon.Planner.Windows
{
    /// <summary>
    ///     Interaction logic for MissionPlanTemplatesWindow.xaml
    /// </summary>
    public sealed partial class MissionPlanTemplatesWindow
    {
        #region Constructors

        public MissionPlanTemplatesWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void CancelCommandOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmCommandOnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO: Open selected template in application before closing this window.

            Close();
        }

        #endregion
    }
}