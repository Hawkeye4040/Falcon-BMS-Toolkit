using System;
using System.Collections.Generic;
using System.Speech.Synthesis;
using System.Threading;
using System.Xml;

namespace Falcon.Radio.Engine
{
    public sealed class ActionSequence : ITriggerEvent
    {
        #region Properties

        public List<Action> Actions { get; }

        public string Name { get; }

        public string Type { get; }

        public string Comment { get; }

        public string Value { get; }

        #endregion

        #region Constructors

        public ActionSequence()
        {
            Actions = new List<Action>();
            Name = "New Sequence";
            Type = GetType().Name;
            Value = null;
            Comment = string.Empty;
        }

        public ActionSequence(string name)
        {
            Actions = new List<Action>();
            Name = name;
            Type = GetType().Name;
            Value = null;
            Comment = string.Empty;
        }

        public ActionSequence(XmlNode element, Database profileDb, SpeechSynthesizer synthesizer)
        {
            Actions = new List<Action>();
            Name = string.Empty;
            Type = GetType().Name;
            Value = null;
            Comment = string.Empty;

            if (element.Attributes?["Name"] != null)
            {
                Name = element.Attributes["Name"].Value;
            }

            else
            {
                Console.WriteLine($@"[{DateTime.Now}] An erroneous action sequence was detected and ignored.");

                throw new ArgumentNullException(nameof(element), @"Action Sequence missing required attribute: Name.");
            }

            if (element.Attributes["Comment"] != null) Comment = element.Attributes["Comment"].Value;

            foreach (XmlNode action in element.ChildNodes)
                if (action.Attributes != null)
                {
                    string actionType = action.Attributes.GetNamedItem("Type").Value;
                    string actionValue = action.Attributes.GetNamedItem("Value").Value;

                    Type newActionType = System.Type.GetType($"Falcon.Radio.{actionType}");

                    object actionInstance;

                    switch (actionType)
                    {
                        case "Speak":
                        {
                            actionInstance = Activator.CreateInstance(newActionType, synthesizer, actionValue);

                            break;
                        }

                        case "PlaySound":
                        {
                            int deviceId = 0;

                            if (int.Parse(action.Attributes.GetNamedItem("DeviceId").Value) == deviceId)
                                actionInstance = Activator.CreateInstance(newActionType, actionValue, deviceId);

                            else
                                actionInstance =
                                    Activator.CreateInstance(newActionType, actionValue, deviceId);

                            break;
                        }

                        case "DataSpeak":
                        {
                            if (profileDb.DatabaseInternal.ContainsKey(actionValue))
                                actionInstance = Activator.CreateInstance(newActionType, synthesizer,
                                    profileDb.DatabaseInternal[actionValue]);

                            else
                                actionInstance = null;

                            break;
                        }

                        default:
                        {
                            actionInstance = Activator.CreateInstance(newActionType, actionValue);

                            break;
                        }
                    }
                }
        }

        #endregion

        #region Methods

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("ActionSequence");
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Type", Type);
            writer.WriteAttributeString("Comment", Comment);

            foreach (Action action in Actions)
            {
                writer.WriteStartElement("Action");

                switch (action.Type)
                {
                    case "PlaySound":
                    {
                        writer.WriteAttributeString("Type", action.Type);
                        writer.WriteAttributeString("Value", action.Value);

                        PlaySound sound = (PlaySound) action;

                        writer.WriteAttributeString("DeviceId", sound.GetDeviceId().ToString());

                        break;
                    }

                    default:
                    {
                        writer.WriteAttributeString("Type", action.Type);
                        writer.WriteAttributeString("Value", action.Value);

                        break;
                    }
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        public void Run()
        {
            foreach (Action action in Actions)
            {
                action.Run();
                Thread.Sleep(20);
            }
        }

        #endregion
    }
}