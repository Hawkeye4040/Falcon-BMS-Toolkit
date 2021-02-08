using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Xml;

namespace Falcon.Radio.Engine
{
    public sealed class Database
    {
        private string dbFileName;

        public Dictionary<string, Data> DatabaseInternal { get; }

        public Database()
        {
            DatabaseInternal = new Dictionary<string, Data>();
        }

        public Database(string fileName)
        {
            dbFileName = fileName;
        }

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

                    object typeInstance;
                    object entryInstance;

                    MethodInfo methodInfo = newDataElementType.GetMethod("ToObject");
                    typeInstance = methodInfo.Invoke(null, new object [] {dataElementValue});
                    entryInstance = Activator.CreateInstance(newDataElementType, dataElementName, typeInstance, dataElementComment);

                    if (entryInstance != null)
                    {
                        if (!Insert((Data)entryInstance))
                        {
                            // TODO: Log a warning, variable failed to load.
                        }

                        else
                        {
                            // TODO: Log successful entry into the database.
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                if (e.InnerException != null)
                {
                    MessageBox.Show(
                        $"Warning! Failed to load database from file:\n {e.Message} \n Error Code: {e.InnerException.HResult}",
                        "Warning", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                else
                {
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

            foreach (KeyValuePair<string, Data> data in DatabaseInternal)
            {
                writer.WriteStartElement("Data");
                writer.WriteAttributeString("Name", data.Key);
                writer.WriteAttributeString("Type", data.Value.Type);
                writer.WriteAttributeString("Value", data.Value.Value);
                writer.WriteAttributeString("Comment", data.Value.Comment);
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
    }
}