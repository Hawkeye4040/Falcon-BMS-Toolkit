using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace Falcon.Radio.Server
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App
    {
        #region Constructors

        public App()
        {
            Current.DispatcherUnhandledException += App_DispatcherUnhandledException;
            Current.SessionEnding += App_SessionEnding;
        }

        #endregion

        #region Methods

        private static void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is NotImplementedException)
            {
                MessageBox.Show("This feature has not yet been implemented.", "Feature not Implemented",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                e.Handled = true;
            }

            else
            {
                // Skip this step if debugging so the debugger can catch errors.
                if (Debugger.IsAttached) return;

                MessageBox.Show("An unknown error has occured. Contact support if this problem persists.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                e.Handled = true;
            }
        }

        private void App_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            
        }

        #endregion
    }
}