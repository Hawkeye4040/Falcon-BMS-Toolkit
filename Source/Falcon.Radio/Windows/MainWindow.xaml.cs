using System;
using System.Linq;
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

        private bool _isMasterPowerEnabled;

        private bool _isVoiceControlEnabled;

        #endregion

        #region Properties

        private bool IsListeningEnabled => _isMasterPowerEnabled && _isVoiceControlEnabled;

        #endregion

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            MMDeviceEnumerator captureDevices = new MMDeviceEnumerator();

            foreach (ComboBoxItem item in captureDevices.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).Select(device => new ComboBoxItem {Content = device.FriendlyName, Tag = device.ID}))
            {
                AudioDeviceSelector.Items.Add(item);
            }

            AudioDeviceSelector.SelectedIndex = 0;

            if (IsListeningEnabled)
                App.StartListening(this, EventArgs.Empty);
        }

        #endregion

        #region Methods

        private void MasterPowerButton_Checked(object sender, RoutedEventArgs e)
        {
            _isMasterPowerEnabled = true;
            RadioPowerToggleButton.RenderTransform = new RotateTransform(180);

            RadioIndicatorLight.Source =
                new BitmapImage(new Uri("../Resources/Cockpit Indicator Light.png", UriKind.Relative));

            //  BUG: Fix Indicator light issue.

            if (IsListeningEnabled) App.StartListening(this, EventArgs.Empty);
        }

        private void MasterPowerButton_UnChecked(object sender, RoutedEventArgs e)
        {
            _isMasterPowerEnabled = true;
            _isVoiceControlEnabled = false;
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
    }
}