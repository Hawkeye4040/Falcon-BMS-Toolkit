using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using NAudio.CoreAudioApi;

namespace Falcon.Radio.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow
    {
        #region Fields

        private bool IsMasterPowerEnabled;

        private bool IsVoiceControlEnabled;

        #endregion

        #region Properties

        private bool IsListeningEnabled => IsMasterPowerEnabled && IsVoiceControlEnabled;

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            //App.StartListening(this, EventArgs.Empty);

            MMDeviceEnumerator captureDevices = new MMDeviceEnumerator();

            foreach (MMDevice device in captureDevices.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
            {
                ComboBoxItem item = new ComboBoxItem {Content = device.FriendlyName, Tag = device.ID};

                AudioDeviceSelector.Items.Add(item);
            }

            AudioDeviceSelector.SelectedIndex = 0;
        }

        #endregion

        #region Methods

        private void MasterPowerButton_Checked(object sender, RoutedEventArgs e)
        {
            IsMasterPowerEnabled = true;
            RadioPowerToggleButton.RenderTransform = new RotateTransform(180);

            RadioIndicatorLight.Source =
                new BitmapImage(new Uri("../Resources/Cockpit Indicator Light.png", UriKind.Relative));

            //  TODO: Fix Indicator light bug.

            if (IsListeningEnabled) App.StartListening(this, EventArgs.Empty);
        }

        private void MasterPowerButton_UnChecked(object sender, RoutedEventArgs e)
        {
            IsMasterPowerEnabled = true;
            IsVoiceControlEnabled = false;
            RadioPowerToggleButton.RenderTransform = new RotateTransform(0);
            RadioVoiceControlToggleButton.RenderTransform = new RotateTransform(0);

            RadioIndicatorLight.Source =
                new BitmapImage(new Uri("../Resources/Cockpit Indicator Light.png", UriKind.Relative));

            App.StopListening(this, EventArgs.Empty);
        }

        private void RadioVoiceControlToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            RadioVoiceControlToggleButton.RenderTransform = new RotateTransform(180);

            if (RadioPowerToggleButton.IsChecked != null && (bool) RadioPowerToggleButton.IsChecked)
                RadioIndicatorLight.Source =
                    new BitmapImage(new Uri("../Resources/Cockpit Indicator Light On.png", UriKind.Relative));

            if (IsListeningEnabled) App.StartListening(this, EventArgs.Empty);
        }

        private void RadioVoiceControlToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            RadioVoiceControlToggleButton.RenderTransform = new RotateTransform(0);

            RadioIndicatorLight.Source =
                new BitmapImage(new Uri("../Resources/Cockpit Indicator Light.png", UriKind.Relative));

            App.StopListening(this, EventArgs.Empty);
        }

        private void AudioDeviceSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = e.AddedItems[0] as ComboBoxItem;

            MMDeviceEnumerator captureDevices = new MMDeviceEnumerator();

            foreach (MMDevice device in captureDevices.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
                if (device.ID == item?.Tag as string)
                {
                    // TODO: Find reference to audio device Id and insert into loop here to change from bound tag value on combobox items.
                }
        }

        #endregion

        /* protected override void OnClosing(CancelEventArgs e)
         {
             e.Cancel = true;
 
             Hide();
 
             base.OnClosing(e);
         } */
    }
}