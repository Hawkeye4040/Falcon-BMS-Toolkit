using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows;
using System.Xml;

using Falcon.Core;
using Falcon.Core.Utilities;

namespace Falcon.Radio.Engine
{
    public sealed class Profile
    {
        #region Fields

        public Database ProfileDatabase;

        private string profileFileName;

        private bool isDirty;

        private string associatedProcess;

        #endregion

        #region Properties

        public SpeechSynthesizer Synthesizer { get; }

        public List<Trigger> ProfileTriggers { get; set; }

        public List<ActionSequence> ActionSequences { get; set; }

        public string Name { get; set; }

        #endregion

        #region Constructors

        public Profile(string fileName)
        {
            ProfileTriggers = new List<Trigger>();

            Synthesizer = new SpeechSynthesizer();

            try
            {
                if (fileName != null) LoadProfile(fileName);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);

                Diagnostics.Log(e, $"There was an error loading the specified profile.\n {e.Message}");

                MessageBox.Show($"There was an error loading the specified profile.\n {e.Message}",
                    "Error Loading Profile", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
            }
        }

        #endregion

        #region Methods

        public bool NewProfile()
        {
            throw new NotImplementedException();
        }

        public bool LoadProfile(string fileName)
        {
            if (fileName == null) return false;

            NewProfile();

            XmlDocument profileFile = new XmlDocument();

            try
            {
                profileFile.Load(fileName);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);

                Diagnostics.Log(e);

                throw;
            }

            XmlNodeList profileElements = profileFile.DocumentElement?.ChildNodes;

            if (profileElements != null)
                foreach (XmlNode element in profileElements)
                    if (element.Name == "AssociatedProcess")
                    {
                        associatedProcess = element.InnerText;
                    }

                    else if (element.Name.Contains("ActionSequence"))
                    {
                        ActionSequence actionSequenceFromFile;

                        try
                        {
                            actionSequenceFromFile = new ActionSequence(element, ProfileDatabase, Synthesizer);

                            if (ActionSequences.All(
                                actionSequence => actionSequence.Name != actionSequenceFromFile.Name))
                                ActionSequences.Add(actionSequenceFromFile);

                            else
                                throw new ArgumentException(
                                    $"An action sequence with the name {actionSequenceFromFile.Name} already exists.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);

                            Diagnostics.Log(e);
                        }
                    }

                    else if (element.Name.Contains("Trigger"))
                    {
                        string triggerName = element.Attributes.GetNamedItem("Name").Value;

                        string triggerType = element.Attributes.GetNamedItem("Type").Value;

                        string triggerValue = element.Attributes.GetNamedItem("Value").Value;

                        string triggerComment = element.Attributes.GetNamedItem("Comment").Value;

                        Type newTriggerType = Type.GetType($"Falcon.Radio.{triggerType}");

                        object triggerInstance = Activator.CreateInstance(newTriggerType, triggerName, triggerValue);

                        Trigger triggerFromFile = (Trigger) triggerInstance;

                        triggerFromFile.Comment = triggerComment;

                        foreach (XmlElement triggerEvent in element.ChildNodes)
                        {
                            string eventType = triggerEvent.Attributes.GetNamedItem("Type").Value;
                            string eventName = triggerEvent.Attributes.GetNamedItem("Name").Value;
                            string eventValue = triggerEvent.Attributes.GetNamedItem("Value").Value;

                            string eventComment = triggerEvent.Attributes.GetNamedItem("Comment").Value;

                            if (eventType.Contains("ActionSequence"))
                            {
                                triggerFromFile.Add(ActionSequences.Find(actionSequence =>
                                    actionSequence.Name == eventName));
                            }

                            else if (eventType.Contains("Phrase"))
                            {
                                Type metaTriggerType = Type.GetType($"Falcon.Radio.{eventType}");

                                object metaTriggerInstance =
                                    Activator.CreateInstance(metaTriggerType, eventName, eventValue);

                                Trigger newMetaTrigger = (Trigger) metaTriggerInstance;

                                triggerFromFile.Add(newMetaTrigger);
                            }
                        }

                        if (ProfileTriggers.All(trigger => trigger.Name != triggerFromFile.Name))
                            ProfileTriggers.Add(triggerFromFile);
                    }

                    else if (element.Name.Contains("Database") || element.Name.Contains("DB") ||
                             element.Name.Contains("Db"))
                    {
                        ProfileDatabase?.Load(element);
                    }

            profileFileName = fileName;

            return true;
        }

        public bool SaveProfile()
        {
            if (IsEmpty()) return false;

            if (profileFileName == null)
            {
                // TODO: Update Save File Dialog behavior to modern APIs.

                //using (SaveFileDialog dialog = new SaveFileDialog())
                //{
                //    dialog.Title = "Save Profile as...";
                //    dialog.Filter = "Profiles (*.xml)|*.xml|All Files (*.*)|*.*";

                //    dialog.RestoreDirectory = true;

                //    if (dialog.ShowDialog() == DialogResult.Cancel) return false;

                //    profileFileName = dialog.FileName;
                //}
            }

            return SaveProfile(profileFileName);
        }

        public bool SaveProfile(string fileName)
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings {Indent = true};
                XmlWriter writer = XmlWriter.Create(fileName, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("FalconRadioProfile");

                if (associatedProcess != null)
                {
                    writer.WriteStartElement("AssociatedProcess");
                    writer.WriteString(associatedProcess);
                    writer.WriteEndElement();
                }

                ProfileDatabase?.Save(writer);

                foreach (ActionSequence actionSequence in ActionSequences) actionSequence.WriteXml(writer);

                foreach (Trigger trigger in ProfileTriggers)
                {
                    writer.WriteStartElement("Trigger");
                    writer.WriteAttributeString("Name", trigger.Name);
                    writer.WriteAttributeString("Value", trigger.Value);
                    writer.WriteAttributeString("Type", trigger.Type);
                    writer.WriteAttributeString("Comment", trigger.Comment);


                    foreach (ITriggerEvent triggerEvent in trigger.TriggerEvents)
                    {
                        writer.WriteStartElement("TriggerEvent");
                        writer.WriteAttributeString("Name", triggerEvent.Name);
                        writer.WriteAttributeString("Type", triggerEvent.Type);
                        writer.WriteAttributeString("Value", triggerEvent.Value);
                        writer.WriteAttributeString("Comment", trigger.Comment);
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine(e);

                MessageBox.Show($"Error saving to file {e.InnerException?.Message}");

                return false;
            }

            profileFileName = fileName;
            isDirty = false;

            return true;
        }

        public bool IsEmpty()
        {
            return ProfileTriggers == null || !ProfileTriggers.Any();
        }

        public void Edited()
        {
            isDirty = true;
        }

        public bool IsEdited()
        {
            return !IsEmpty() && isDirty;
        }

        public bool AssociatedProcessHasFocus()
        {
            if (associatedProcess == null) return true;

            IntPtr handle = Win32.GetForegroundWindow();
            _ = Win32.GetWindowThreadProcessId(handle, out uint pid);
            Process process = Process.GetProcessById((int) pid);
            string processPath = process.MainModule?.FileName;

            return string.Compare(Path.GetFullPath(processPath ?? string.Empty), Path.GetFullPath(associatedProcess),
                StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public void SetAssociatedProcess(string processName)
        {
            associatedProcess = processName;
        }

        #endregion
    }
}