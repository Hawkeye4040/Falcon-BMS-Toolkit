using System;
using System.Diagnostics;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows;

using Falcon.Core;

using NAudio.Wave;

namespace Falcon.Radio.Engine
{
    public abstract class Action
    {
        #region Properties

        public string Type { get; }

        public string Value { get; set; }

        #endregion

        #region Constructors

        protected Action()
        {
            Type = GetType().Name;
        }

        protected Action(string value)
        {
            Type = GetType().Name;
            Value = value;
        }

        #endregion

        #region Methods

        public abstract void Run();

        #endregion
    }

    public sealed class KeyDown : Action
    {
        #region Methods

        public override void Run()
        {
            const int WM_KEYDOWN = 0x0100;
            
            // Keyboard.KeyDown((Key) Enum.Parse(typeof(Key),
            //     Value));
            // TODO: Update Input API to more current one than Win forms.
            
            IntPtr parameter = new IntPtr(0xFFFFFFF);
            int message = 0x74;

           Process [] bmsProcess = Process.GetProcessesByName("Falcon BMS");

           foreach (Process process in bmsProcess)
           {
               Win32.PostMessage(process.MainWindowHandle, WM_KEYDOWN, parameter, parameter);
           }
        }

        // TODO: Implement 137 virtual input addresses corresponding to the needed hard-coded BMS Voice Commands.

        #endregion
    }

    public sealed class KeyUp : Action
    {
        #region Methods

        public override void Run()
        {
            const int WM_KEYUP = 0x0101;

            IntPtr parameter = new IntPtr(0xFFFFFF);
            int message = 0x74;

            Process [] bmsProcesses = Process.GetProcessesByName("Falcon BMS");

            foreach (Process process in bmsProcesses)
            {
                Win32.PostMessage(process.MainWindowHandle, WM_KEYUP, parameter, parameter);
            }
        }

        #endregion
    }

    public sealed class MouseDown : Action
    {
        #region Methods

        public override void Run()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public sealed class MouseUp : Action
    {
        #region Methods

        public override void Run()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public sealed class Speak : Action
    {
        private SpeechSynthesizer _profileSynthesis;

        public Speak(SpeechSynthesizer profileSynthesis, string value)
            : base(value)
        {
            _profileSynthesis = profileSynthesis;
        }

        #region Methods

        public override void Run()
        {
            try
            {
                _profileSynthesis.SpeakAsync(Value);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);

                // TODO: Log error.
            }
        }

        #endregion
    }

    public sealed class Wait : Action
    {
        public Wait(string value) : base(value)
        {

        }

        #region Methods

        public override void Run()
        {
            int sleepDuration = Int32.Parse(Value);
            Thread.Sleep(sleepDuration);
        }

        #endregion
    }

    public sealed class PlaySound : Action
    {
        private IWavePlayer wavePlayer;
        private WaveOut waveOut;
        private AudioFileReader audioFileReader;
        private int playbackDeviceId;

        public const int DefaultDeviceId = -1;

        public PlaySound(string fileName, int deviceId)
        {
            playbackDeviceId = deviceId;

            try
            {
                waveOut = new WaveOut {DeviceNumber = playbackDeviceId};
                wavePlayer = waveOut;
                audioFileReader = new AudioFileReader(Value);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }
        }

        #region Methods

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            wavePlayer?.Stop();
        }

        public int GetDeviceId()
        {
            return playbackDeviceId;
        }

        #endregion
    }

    public sealed class StopSound : Action
    {
        public StopSound()
        {
            Value = "Stop All Playback";
        }

        public StopSound(string value) : base(value)
        {

        }
        #region Methods

        public override void Run()
        {
            foreach (PlaySound test in App.ActiveProfile.ActionSequences.SelectMany(actionSequence => actionSequence.Actions).OfType<PlaySound>())
            {
                test.Stop();
            }
        }

        #endregion
    }

    public sealed class DataIncrement : Action
    {
        private Data _data;

        public DataIncrement(Data data, string value) : base(value)
        {
            _data = data;
        }

        #region Methods

        public override void Run()
        {
            
        }

        #endregion
    }

    public sealed class DataDecrement : Action
    {
        private Data _data;

        public DataDecrement(Data data, string value) : base(value)
        {
            _data = data;
        }

        #region Methods

        public override void Run()
        {
            
        }

        #endregion
    }

    public sealed class DataSet : Action
    {
        private Data _data;

        public DataSet(Data data, string value) : base(value)
        {
            _data = data;
        }

        #region Methods

        public override void Run()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public sealed class DataSpeak : Action
    {
        private SpeechSynthesizer profileSynthesizer;

        private Data data;

        public DataSpeak(SpeechSynthesizer profileSynthesizer, Data data)
        {
            this.profileSynthesizer = profileSynthesizer;
            this.data = data;
        }

        #region Methods

        public override void Run()
        {
            try
            {
                profileSynthesizer.SpeakAsync(data.Value);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);

                // TODO: Log Speech Synthesis Error.
            }
        }

        #endregion
    }


    public sealed class ProcessExecution : Action
    {
        Process process = new Process();

        public ProcessExecution(string processName) : base(processName)
        {

        }

        #region Methods

        public override void Run()
        {
            Process.Start(Value);
        }

        #endregion
    }

    public sealed class ClipboardCopy : Action
    {
        #region Methods

        public override void Run()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public sealed class ClipboardPaste : Action
    {
        #region Methods

        public override void Run()
        {
            Clipboard.SetText(Value, TextDataFormat.Text);
            //SendKeys.Send("^V");
        }

        #endregion
    }

    // TODO: Implement Logic Actions.
}