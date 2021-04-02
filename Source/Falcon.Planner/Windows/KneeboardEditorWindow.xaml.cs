using System.Threading.Tasks;
using System.Windows.Input;

namespace Falcon.Planner.Windows
{
    /// <summary>
    ///     Interaction logic for KneeboardEditorWindow.xaml
    /// </summary>
    public partial class KneeboardEditorWindow
    {
        #region Constructors

        public KneeboardEditorWindow()
        {
            Initialize();
        }

        private async Task InitializeDocuments()
        {
            StatusTextBlock.Text = "Initializing Documents";

            await Task.Run(() =>
            {

            });

            StatusTextBlock.Text = "Documents Initialized";
        }

        #endregion

        #region Methods

        private async void Initialize()
        {
            await Dispatcher.InvokeAsync(InitializeComponent);

            await InitializeDocuments();
        }

        private void CloseWindowCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO: Add logic to check for unsaved changes before closing.
            Close();
        }

        #endregion
    }
}