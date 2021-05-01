using System;
using System.Windows;
using System.Windows.Input;

namespace Falcon.Roster.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow
    {
        #region Properties

        public object ActiveSelection { get; }

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            ActiveSelection = null;
        }

        #endregion

        #region Methods

        private void SaveCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveAsCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ManageConnectionsCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ConnectionManagerWindow window = new ConnectionManagerWindow();
            window.ShowDialog();
        }

        private void ManageAccountCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            AccountManagementWindow window = new AccountManagementWindow();
            window.ShowDialog();
        }

        private void SettingsCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void ExitCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO: check for unsaved changes and save files before exiting.

            Application.Current.Shutdown(0);
        }

        #endregion
    }
}