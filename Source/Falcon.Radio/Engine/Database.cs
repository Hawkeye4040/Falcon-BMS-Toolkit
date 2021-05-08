using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Xml;

using Falcon.Core;

namespace Falcon.Radio.Engine
{
    public sealed class Database
    {
        #region Fields

        private string dbFileName;

        #endregion

        #region Properties

        public Dictionary<string, Data> DatabaseInternal { get; }

        #endregion

        #region Constructors

        public Database()
        {
            DatabaseInternal = new Dictionary<string, Data>();
        }

        public Database(string fileName)
        {
            dbFileName = fileName;
        }

        #endregion

        #region Methods

        public void Load(XmlNode dbElements)
        {
            try
            {
                foreach (XmlNode data in dbElements.ChildNodes)
                {
                    string dataElementName = null;
                    string dataElementType = null;
                    string dataElementComment = string.Empty;
                    string dataElementValue = null;

                    if (data != null)
                    {
                        dataElementName = data.Attributes.GetNamedItem("Name").Value;
                        dataElementType = data.Attributes.GetNamedItem("Type").Value;
                        dataElementComment = data.Attributes.GetNamedItem("Comment").Value;
                        dataElementValue = data.Attributes.GetNamedItem("Value").Value;
                    }

                    Type newDataElementType = Type.GetType($"Falcon.Radio.{dataElementType}");

                    if (newDataElementType == null) continue;

                    MethodInfo methodInfo = newDataElementType.GetMethod("ToObject");

                    if (methodInfo == null) continue;

                    object typeInstance = methodInfo.Invoke(null, new object [] {dataElementValue});

                    object entryInstance = Activator.CreateInstance(newDataElementType, dataElementName, typeInstance,
                        dataElementComment);

                    if (entryInstance == null) continue;

                    Diagnostics.Log(!Insert((Data) entryInstance)
                        ? $"Error loading instance of database entry: {entryInstance}"
                        : "Radio database loaded successfully.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                if (e.InnerException != null)
                {
                    Diagnostics.Log(e,
                        $"Warning! Failed to load database from file: \n {e.Message} \n Error Code: {e.InnerException.HResult}");

                    MessageBox.Show(
                        $"Warning! Failed to load database from file:\n {e.Message} \n Error Code: {e.InnerException.HResult}",
                        "Warning", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                else
                {
                    Diagnostics.Log(e,
                        $"Warning! Failed to load database from file: \n {e.Message} \n Error Code: Unknown");

                    MessageBox.Show(
                        $"Warning! Failed to load database from file:\n {e.Message}",
                        "Warning", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
        }

        public void Save(XmlWriter writer)
        {
            writer.WriteStartElement("Database");

            foreach ((string key, Data value) in DatabaseInternal)
            {
                writer.WriteStartElement("Data");
                writer.WriteAttributeString("Name", key);
                writer.WriteAttributeString("Type", value.Type);
                writer.WriteAttributeString("Value", value.Value);
                writer.WriteAttributeString("Comment", value.Comment);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
            writer.Flush();
        }

        public bool Insert(Data data)
        {
            if (DatabaseInternal.ContainsKey(data.Name)) return false;

            DatabaseInternal.Add(data.Name, data);

            return true;
        }

        public bool Remove(Data data)
        {
            if (!DatabaseInternal.ContainsKey(data.Name)) return false;

            DatabaseInternal.Remove(data.Name);

            return true;
        }

        public bool Update(Data data)
        {
            if (!DatabaseInternal.ContainsKey(data.Name)) return false;

            DatabaseInternal[data.Name] = data;

            return true;
        }

        public bool IsDataNameTaken(string name)
        {
            return DatabaseInternal.ContainsKey(name);
        }

        #endregion
    }
}