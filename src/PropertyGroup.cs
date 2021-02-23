using System;
using System.Collections.Generic;
using Inventor;

namespace iPropertyGroups
{
    [Serializable]
    public class PropertyGroup
    {
        public string Name { get; set; } = "";
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public PropertyGroup()
        {
        }

        public PropertyGroup(string name, Dictionary<string, string> props)
        {
            Name = name;
            Properties = props;
        }

        public void Add(string key, string value)
        {
            Properties.Add(key, value);
        }

        public void Remove(string key)
        {
            if (Properties.ContainsKey(key))
                Properties.Remove(key);
        }

        public string this[string key]
        {
            get
            {
                // If this key is in the dictionary, return its value.
                if (Properties.ContainsKey(key))
                {
                    return Properties[key];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                // If this key is in the dictionary, change its value.
                if (Properties.ContainsKey(key))
                {
                    // The key was found; change its value.
                    Properties[key] = (string)value;
                }
                else
                {
                    // This key is not in the dictionary; add this key/value pair.
                    Properties.Add(key, (string)value);
                }
            }
        }

        public long Count()
        {
            return Properties.Count;
        }

        public void ApplyToAndOverwrite(Document document)
        {
            foreach (string key in Properties.Keys)
            {
                WriteIPropertyToDocument(document, key, Properties[key]);
            }
        }

        public void ApplyTo(Document document)
        {
            foreach (string key in Properties.Keys)
            {
                if (!PropertyEditor.PropertyExists(document, key))
                    WriteIPropertyToDocument(document, key, Properties[key]);
            }
        }

        private void WriteIPropertyToDocument(Document document, string key, string value)
        {
            try
            {
                PropertyEditor.SetPropertyValue(document, key, value);
            }
            catch
            {
            }
        }
    }
}
