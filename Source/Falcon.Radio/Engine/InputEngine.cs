using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Input;

using Falcon.Core;

using InputManager;

namespace Falcon.Radio.Engine
{
    public sealed class InputEngine
    {
        #region Fields

        public bool IsListening;

        private SpeechSynthesizer _synthesizer;

        private SpeechRecognitionEngine _speechRecognitionEngine;

        private bool _pushToTalkActive;

        private bool _pushToTalkKeyIsDown;

        private readonly KeyboardHook.KeyDownEventHandler _pushToTalkKeyDownHook;

        private readonly KeyboardHook.KeyUpEventHandler _pushToTalkKeyUpHook;

        #endregion

        #region Constructors

        public InputEngine()
        {
            _pushToTalkActive = false;
            _pushToTalkKeyIsDown = false;
            _pushToTalkKeyDownHook = KeyboardHook_KeyDown;
            _pushToTalkKeyUpHook = KeyboardHook_KeyUp;
        }

        #endregion

        #region Methods

        public bool LoadListen()
        {
            // Don't allocate anything if we have no phrases to hook.

            if (App.ActiveProfile == null) return false;

            if (App.ActiveProfile.ProfileTriggers != null &&
                App.ActiveProfile.ProfileTriggers.Count == 0)
            {
                Diagnostics.Log("LoadListen() called without a trigger added.");
                MessageBox.Show("At least one Trigger must be added!");

                return false;
            }

            _synthesizer = App.ActiveProfile.Synthesizer;
            _synthesizer.SelectVoice(App.Settings.VoiceInfo);
            _speechRecognitionEngine = new SpeechRecognitionEngine(App.Settings.RecognizerInfo);

            GrammarBuilder grammarPhrases = new GrammarBuilder {Culture = App.Settings.RecognizerInfo};

            // Grammar must match speech recognition language localization

            List<string> glossary = new List<string>();

            // Add trigger phrases to glossary of voice recognition engine.
            if (App.ActiveProfile.ProfileTriggers != null)
                glossary.AddRange(from trigger in App.ActiveProfile.ProfileTriggers
                    let phrase = (Phrase) trigger
                    select trigger.Value);

            grammarPhrases.Append(new Choices(glossary.ToArray()));
            _speechRecognitionEngine.LoadGrammar(new Grammar(grammarPhrases));

            // event function hook
            _speechRecognitionEngine.SpeechRecognized += PhraseRecognized;
            _speechRecognitionEngine.SpeechRecognitionRejected += Recognizer_SpeechRecognitionRejected;

            try
            {
                _speechRecognitionEngine.SetInputToDefaultAudioDevice();
            }

            catch (InvalidOperationException e)
            {
                Diagnostics.Log(e, "No microphone was detected.");
                MessageBox.Show("No microphone was detected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            catch (Exception e)
            {
                Diagnostics.Log(e, "An Unknown error occured when attempting to set default input device.");

                MessageBox.Show("An unknown error has occured, contact support if the problem persists.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            _speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

            // subscribe to Push-to-Talk key hooks.
            KeyboardHook.KeyDown += _pushToTalkKeyDownHook;
            KeyboardHook.KeyUp += _pushToTalkKeyUpHook;
            KeyboardHook.InstallHook();

            if (App.Settings.PushToTalkMode != "Hold" && App.Settings.PushToTalkMode != "Toggle" &&
                App.Settings.PushToTalkMode != "Single") _pushToTalkActive = true;

            //  successfully established an instance of SAPI engine with well-formed grammar.

            IsListening = true;

            return true;
        }

        public void StopListen()
        {
            _speechRecognitionEngine?.RecognizeAsyncCancel();
            _speechRecognitionEngine?.UnloadAllGrammars();
            _speechRecognitionEngine?.Dispose();

            KeyboardHook.UninstallHook();
            KeyboardHook.KeyDown -= _pushToTalkKeyDownHook;
            KeyboardHook.KeyUp -= _pushToTalkKeyUpHook;

            IsListening = false;
        }

        private void PhraseRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (_pushToTalkActive)
            {
                string recognizedValue = e.Result.Text;

                if (!App.ActiveProfile.AssociatedProcessHasFocus())
                    return;

                App.ActiveProfile.ProfileTriggers.Find(trigger => trigger.Value == recognizedValue).Run();

                // Update the log after the action is run, since UpdateStatus is a blocking method.
                UpdateStatusLog(recognizedValue);

                if (App.Settings.PushToTalkMode == "Single")
                {
                    _pushToTalkActive = false;
                    UpdateStatusLog("Stop Listening");
                }
            }
        }

        private void Recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            // TODO: Change behavior when speech is not recognized.
            UpdateStatusLog("?");
        }

        private void KeyboardHook_KeyDown(int vkCode)
        {
            if (_pushToTalkKeyIsDown) return;

            // Only check if Push-to-Talk key is not pressed.

            if (((Key) vkCode).ToString() != App.Settings.PushToTalkKey) return;

            switch (App.Settings.PushToTalkMode)
            {
                case "Hold":
                    _pushToTalkActive = true;
                    UpdateStatusLog("Start Listening");

                    break;
                case "Toggle" when _pushToTalkActive == false:
                    _pushToTalkActive = true;
                    UpdateStatusLog("Start Listening");

                    break;
                case "Toggle":
                    _pushToTalkActive = false;
                    UpdateStatusLog("Stop Listening");

                    break;
                case "Single":
                {
                    if (_pushToTalkActive == false)
                        UpdateStatusLog("Start Listening");

                    _pushToTalkActive = true;

                    break;
                }
                default:
                    Diagnostics.Log(
                        "Error reading keyboard hook(s)! This was most likely caused by manually editing the key bindings file improperly.");

                    break;
            }

            _pushToTalkKeyIsDown = true;
        }

        private void KeyboardHook_KeyUp(int vkCode)
        {
            if (_pushToTalkKeyIsDown == false) return;

            if (((Key) vkCode).ToString() != App.Settings.PushToTalkKey) return;

            switch (App.Settings.PushToTalkMode)
            {
                case "Hold":
                    _pushToTalkKeyIsDown = false;
                    _pushToTalkActive = false;

                    UpdateStatusLog("Stop Listening");

                    break;
                case "Toggle":
                case "Single":
                    _pushToTalkKeyIsDown = false;

                    break;
                default:
                    Diagnostics.Log(
                        "Error reading keyboard hook(s)! This was most likely caused by manually editing the key bindings file improperly.");

                    break;
            }
        }

        private void UpdateStatusLog(string logMessage)
        {
            // TODO: Move status logging to another file.
        }

        #endregion
    }
}