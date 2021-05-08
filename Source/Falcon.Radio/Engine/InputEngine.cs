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

        private SpeechSynthesizer synthesizer;

        private SpeechRecognitionEngine speechRecognitionEngine;

        private bool pushToTalkActive;

        private bool pushToTalkKeyIsDown;

        private KeyboardHook.KeyDownEventHandler pushToTalkKeyDownHook;

        private KeyboardHook.KeyUpEventHandler pushToTalkKeyUpHook;

        #endregion

        #region Constructors

        public InputEngine()
        {
            pushToTalkActive = false;
            pushToTalkKeyIsDown = false;
            pushToTalkKeyDownHook = KeyboardHook_KeyDown;
            pushToTalkKeyUpHook = KeyboardHook_KeyUp;
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
                MessageBox.Show("You need to add at least one Trigger");

                return false;
            }

            synthesizer = App.ActiveProfile.Synthesizer;
            synthesizer.SelectVoice(App.Settings.VoiceInfo);
            speechRecognitionEngine = new SpeechRecognitionEngine(App.Settings.RecognizerInfo);

            GrammarBuilder grammarPhrases = new GrammarBuilder {Culture = App.Settings.RecognizerInfo};

            // Grammar must match speech recognition language localization

            List<string> glossary = new List<string>();

            // Add trigger phrases to glossary of voice recognition engine.
            if (App.ActiveProfile.ProfileTriggers != null)
                glossary.AddRange(from trigger in App.ActiveProfile.ProfileTriggers
                    let phrase = (Phrase) trigger
                    select trigger.Value);

            grammarPhrases.Append(new Choices(glossary.ToArray()));
            speechRecognitionEngine.LoadGrammar(new Grammar(grammarPhrases));

            // event function hook
            speechRecognitionEngine.SpeechRecognized += PhraseRecognized;
            speechRecognitionEngine.SpeechRecognitionRejected += Recognizer_SpeechRecognitionRejected;

            try
            {
                speechRecognitionEngine.SetInputToDefaultAudioDevice();
            }

            catch (InvalidOperationException exception)
            {
                //  For the time being, we're only catching failures to address an input device (typically a
                //  microphone). // TODO: Show error message indicating a microphone was not detected.


                return false;
            }

            catch (Exception)
            {
                // TODO: Show unknown error message here.
                return false;
            }

            speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

            // Install Push to talk key hooks.
            KeyboardHook.KeyDown += pushToTalkKeyDownHook;
            KeyboardHook.KeyUp += pushToTalkKeyUpHook;
            KeyboardHook.InstallHook();

            if (App.Settings.PushToTalkMode != "Hold" && App.Settings.PushToTalkMode != "Toggle" &&
                App.Settings.PushToTalkMode != "Single") pushToTalkActive = true;

            //  We have successfully establish an instance of a SAPI engine with well-formed grammar.

            IsListening = true;

            return true;
        }

        public void StopListen()
        {
            speechRecognitionEngine?.RecognizeAsyncCancel();
            speechRecognitionEngine?.UnloadAllGrammars();
            speechRecognitionEngine?.Dispose();

            KeyboardHook.UninstallHook();
            KeyboardHook.KeyDown -= pushToTalkKeyDownHook;
            KeyboardHook.KeyUp -= pushToTalkKeyUpHook;

            IsListening = false;
        }

        private void PhraseRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (pushToTalkActive)
            {
                string recognizedValue = e.Result.Text;

                if (!App.ActiveProfile.AssociatedProcessHasFocus())
                    return;

                App.ActiveProfile.ProfileTriggers.Find(trigger => trigger.Value == recognizedValue).Run();

                // Update the log after the action is run, since UpdateStatus is a blocking method.
                UpdateStatusLog(recognizedValue);

                if (App.Settings.PushToTalkMode == "Single")
                {
                    pushToTalkActive = false;
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
            if (pushToTalkKeyIsDown) return;

            // Only check if pushToTalkKeyIsDown is false
            // aka : pptKey is not down

            if (((Key) vkCode).ToString() == App.Settings.PushToTalkKey)
            {
                //if (pushToTalkKeyIsDown == false)
                if (App.Settings.PushToTalkMode == "Hold")
                {
                    pushToTalkActive = true;
                    UpdateStatusLog("Start Listening");
                }

                else if (App.Settings.PushToTalkMode == "Toggle")
                {
                    if (pushToTalkActive == false)
                    {
                        pushToTalkActive = true;
                        UpdateStatusLog("Start Listening");
                    }
                    else
                    {
                        pushToTalkActive = false;
                        UpdateStatusLog("Stop Listening");
                    }
                }
                else if (App.Settings.PushToTalkMode == "Single")
                {
                    if (pushToTalkActive == false)
                        UpdateStatusLog("Start Listening");

                    pushToTalkActive = true;
                }

                else
                {
                    Diagnostics.Log(
                        "Error reading keyboard hook(s)! This was most likely caused by manually editing the key bindings file improperly.");
                }

                pushToTalkKeyIsDown = true;
            }
        }

        private void KeyboardHook_KeyUp(int vkCode)
        {
            if (pushToTalkKeyIsDown == false) return;

            if (((Key) vkCode).ToString() == App.Settings.PushToTalkKey)
            {
                if (App.Settings.PushToTalkMode == "Hold")
                {
                    pushToTalkKeyIsDown = false;
                    pushToTalkActive = false;

                    UpdateStatusLog("Stop Listening");
                }

                else if (App.Settings.PushToTalkMode == "Toggle" || App.Settings.PushToTalkMode == "Single")
                {
                    pushToTalkKeyIsDown = false;
                }

                else
                {
                    Diagnostics.Log(
                        "Error reading keyboard hook(s)! This was most likely caused by manually editing the key bindings file improperly.");
                }
            }
        }

        private void UpdateStatusLog(string logMessage)
        {
            // TODO: Move status logging to another file.
        }

        #endregion
    }
}