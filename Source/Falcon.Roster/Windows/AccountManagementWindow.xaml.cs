using System.Windows.Input;

namespace Falcon.Roster.Windows
{
    /// <summary>
    ///     Interaction logic for AccountManagementWindow.xaml
    /// </summary>
    public partial class AccountManagementWindow
    {
        #region Constructors

        public AccountManagementWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void CloseWindowCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}