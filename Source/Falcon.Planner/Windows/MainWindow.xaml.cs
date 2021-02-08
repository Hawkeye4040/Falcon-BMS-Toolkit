using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Falcon.Planner.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

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