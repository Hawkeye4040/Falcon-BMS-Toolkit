using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;

using Falcon.Radio.Engine;
using Falcon.Radio.Utilities;

namespace Falcon.Radio
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Fields

        public static Profile ActiveProfile;

        public static int WM_OPEN_EXISTING_INSTANCE;

        internal static ProcessMonitor AssociatedProcessMonitor;

        private static Dictionary<string, string> AssociatedProfiles = new Dictionary<string, string>();

        public static InputEngine InputEngine;

        #endregion

        #region Constructors

        public App()
        {
            Current.DispatcherUnhandledException += App_DispatcherUnhandledException;
            Current.SessionEnding += App_SessionEnding;
            InputEngine = new InputEngine();
        }

        private void App_SessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            StopListening(this, EventArgs.Empty);
            // TODO: Add resume listening behavior on session resume if was listening on session ending.
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

        public static void StartListening(object selectedMenuItem, EventArgs e)
        {
            //if (InputEngine.IsListening || !InputEngine.LoadListen() )return;

            //InputEngine = new InputEngine();
            InputEngine.LoadListen();

            // TODO: Update UI to indicate application is listening and report listening as necessary elsewhere.
        }

        public static void StopListening(object selectedMenuItem, EventArgs e)
        {
            if (!InputEngine.IsListening) return;

            InputEngine.StopListen();
            //InputEngine = new InputEngine();

            // TODO: Update UI to indicate application is no longer listening and report as necessary elsewhere.
        }

        #endregion

        public static class Settings
        {
            public static string PushToTalkMode;

            public static string PushToTalkKey;

            public static CultureInfo RecognizerInfo;

            public static string VoiceInfo;

            public static bool MinimizeToTrayOnClose = true; // TODO: Move settings over to proper settings file.
        }
    }
}