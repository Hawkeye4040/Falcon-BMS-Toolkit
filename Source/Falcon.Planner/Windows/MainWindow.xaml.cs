using System.Linq;
using System.Windows;
using System.Windows.Input;

using Microsoft.Win32;

namespace Falcon.Planner.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void NewMissionPlanCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            MissionPlanTemplatesWindow window = new MissionPlanTemplatesWindow();

            window.ShowDialog();
        }

        private void OpenMissionPlanCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string fileName = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                DefaultExt = ".fmp",
                FileName = "Mission Plan",
                Filter = "Mission Plan File (.fmp)|*.fmp"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                fileName = dialog.FileName;
            }
        }

        private void OpenDTCCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string fileName = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                DefaultExt = ".dtc",
                FileName = "Data Cartridge",
                Filter = "Data Cartridge Files (.dtc)|*.dtc"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                fileName = dialog.FileName;
            }
        }

        private void OpenCampaignCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string fileName = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                DefaultExt = ".cam",
                FileName = "",
                Filter = "Campaign Files (.cam)|*.cam"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                fileName = dialog.FileName;
            }
        }

        private void OpenTacticalEngagementCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string fileName = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                DefaultExt = ".tac",
                FileName = "",
                Filter = "Tactical Engagement Files (.tac)|*.tac"
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                fileName = dialog.FileName;
            }
        }

        private void KneeboardCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<KneeboardEditorWindow>().Any())
            {
                Window window = Application.Current.Windows.OfType<KneeboardEditorWindow>().FirstOrDefault();

                window?.Activate();
            }

            else
            {
                KneeboardEditorWindow window = new KneeboardEditorWindow();

                window.Show();
            }
        }

        private void ExitCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO: Add logic to check for unsaved changes before calling exit.

            Application.Current.Shutdown(0);
        }

        #endregion
    }
}