using System;
using System.Collections;
using System.Collections.Generic;
using Inventor;

namespace iPropertyGroups
{
    [Serializable]
    public class PropertyGroup : IDictionary<string, string>
    {
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public ICollection<string> Keys => Properties.Keys;

        public ICollection<string> Values => Properties.Values;

        int ICollection<KeyValuePair<string, string>>.Count => (int)this.Count();

        public bool IsReadOnly => false;

        public PropertyGroup()
        {
        }

        public PropertyGroup(Dictionary<string, string> props)
        {
            Properties = props;
        }

        public void Add(string key, string value)
        {
            Properties.Add(key, value);
        }

        public void Add(KeyValuePair<string, string> item)
        {
            this.Add(item.Key, item.Value);
        }

        public bool Remove(string key)
        {
            return Properties.Remove(key);
        }

        public string this[string key]
        {
            get
            {
                // If this key is in the dictionary, return its value.
                return (Properties.ContainsKey(key)) ? Properties[key] : null;
            }

            set
            {
                // If this key is in the dictionary, change its value.
                Properties[key] = (string)value;
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

        public bool ContainsKey(string key)
        {
            return Properties.ContainsKey(key);
        }

        bool IDictionary<string, string>.Remove(string key)
        {
            return Remove(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            return Properties.TryGetValue(key, out value);
        }

        public void Clear()
        {
            Properties.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return Properties.ContainsKey(item.Key) && Properties.ContainsValue(item.Value);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return Properties.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<string, string>>)Properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
