using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Falcon.Planner.Windows
{
    /// <summary>
    ///     Interaction logic for KneeboardEditorWindow.xaml
    /// </summary>
    public partial class KneeboardEditorWindow
    {
        // TODO: Fix memory leak resources not being freed from RAM when this window is closed.
        #region Constructors

        public KneeboardEditorWindow()
        {
            Initialize();
        }

        #endregion

        #region Methods

        private async void Initialize()
        {
            await Dispatcher.InvokeAsync(InitializeComponent);
        }

        private void CloseWindowCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO: Add logic to check for unsaved changes before closing.
            Close();
        }

        #endregion
    }
}