using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using Falcon.Core.Collections;
using Falcon.Core.Events;

using Microsoft.Win32;

namespace Falcon.Planner.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow
    {
        #region Fields

        /// <summary>
        ///     The backing field for the <see cref="ActiveSelection" /> property.
        /// </summary>
        private object _activeSelection;

        #endregion

        #region Properties

        public object ActiveSelection
        {
            get => _activeSelection;
            set
            {
                ValueChangedEventArgs<object> e = new ValueChangedEventArgs<object>(_activeSelection, value);

                _activeSelection = value;

                if (e.OldValue != e.NewValue)
                    ActiveSelectionChanged?.Invoke(this, e);
            }
        }

        public ObservableList<object> Selection { get; set; }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            Selection = new ObservableList<object>();

            Selection.CollectionChanged += Selection_CollectionChanged;
            ActiveSelectionChanged += MainWindow_ActiveSelectionChanged;
        }

        #endregion

        #region Events

        public event OnValueChanged<object> ActiveSelectionChanged;

        #endregion

        #region Methods

        private void MainWindow_ActiveSelectionChanged(object sender, ValueChangedEventArgs<object> e)
        {
            
        }

        private void Selection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        // File Menu Command Event Handlers

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

            if (result == true) fileName = dialog.FileName;
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

            if (result == true) fileName = dialog.FileName;
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

            if (result == true) fileName = dialog.FileName;
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

            if (result == true) fileName = dialog.FileName;
        }

        private void SettingsCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsWindow window = new SettingsWindow();
            window.ShowDialog();
        }

        private void ExitCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO: Add logic to check for unsaved changes before calling exit.

            Application.Current.Shutdown(0);
        }

        // View Menu Command Event Handlers

        private void KneeboardEditorCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<KneeboardEditorWindow>().Any())
            {
                KneeboardEditorWindow window = Application.Current.Windows.OfType<KneeboardEditorWindow>().FirstOrDefault();

                window?.Activate();
            }

            else
            {
                KneeboardEditorWindow window = new KneeboardEditorWindow();

                window.Show();
            }
        }

        private void OpenDataCartridgeEditorCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (Application.Current.Windows.OfType<DataCartridgeEditorWindow>().Any())
            {
                DataCartridgeEditorWindow window = Application.Current.Windows.OfType<DataCartridgeEditorWindow>()
                    .FirstOrDefault();

                window?.Activate();
            }
            else
            {
                DataCartridgeEditorWindow window = new DataCartridgeEditorWindow();

                window.Show();
            }
        }

        // ATO Command Event Handlers

        private void ViewPackageCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ATOPackageViewWindow window = new ATOPackageViewWindow();
            window.Show();
        }

        private void ViewFlightCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ATOFlightViewWindow window = new ATOFlightViewWindow();

            window.Show();
        }

        #endregion
    }
}