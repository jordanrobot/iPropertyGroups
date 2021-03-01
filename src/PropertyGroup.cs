using System;
using System.Collections;
using System.Collections.Generic;
using Inventor;

namespace iPropertyGroups
{
    /// <summary>
    /// Represents a dictionary of string PropertyGroup to be written to an Inventor.Document.
    /// </summary>
    [Serializable]
    public class PropertyGroup : IDictionary<string, string>
    {
        /// <summary>
        /// Main dictionary object that holds the string PropertyGroup values.
        /// </summary>
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets a collection containing the keys in the PropertyGroup Dictionary&lt;string, string&gt;.
        /// </summary>
        public ICollection<string> Keys => Properties.Keys;

        /// <summary>
        /// Gets a collection containing the values in PropertyGroup Dictionary&lt;string, string&gt;.
        /// </summary>
        public ICollection<string> Values => Properties.Values;

        int ICollection<KeyValuePair<string, string>>.Count => (int)this.Count();
        /// <summary>
        /// Gets the number of key/value pairs in the PropertyGroup Dictionary&lt;string, string&gt;
        /// </summary>
        int ICollection<KeyValuePair<string, string>>.Count => (int)Count();

        /// <summary>
        /// Returns a boolean indicating if the object is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Creates an empty PropertyGroup object.
        /// </summary>
        public PropertyGroup()
        {
        }

        /// <summary>
        /// Creates a PropertyGroup initialized with the provided Dictionary&lt;string, string&gt; object.
        /// </summary>
        /// <param name="props">Dictionary containing the property values.</param>
        public PropertyGroup(Dictionary<string, string> props)
        {
            Properties = props;
        }

        /// <summary>
        /// Adds the specified key and value to the collection.
        /// </summary>
        /// <param name="key">PropertyGroup name</param>
        /// <param name="value">PropertyGroup value</param>
        public void Add(string key, string value)
        {
            Properties.Add(key, value);
        }

        /// <summary>
        /// Adds the specified KeyValuePair to the collection.
        /// </summary>
        /// <param name="item">The PropertyGroup name and value</param>
        public void Add(KeyValuePair<string, string> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes the specified value with the specified key from the PropertyGroup&lt;string, string&gt; dictionary.
        /// </summary>
        /// <param name="key">PropertyGroup key</param>
        /// <returns>true if the element is successfully found and removed; otherwise, false. This method returns false if the key is not found in the PropertyGroup&lt;string, string&gt;</returns>
        public bool Remove(string key)
        {
            return Properties.Remove(key);
        }

        /// <summary>
        /// Returns a Dictionary value for the provided key.
        /// </summary>
        /// <param name="key">PropertyGroup key</param>
        /// <returns>PropertyGroup value</returns>
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

        /// <summary>
        /// Gets the number of key/value pairs in the PropertyGroup&lt;string, string&gt; object.
        /// </summary>
        /// <returns>the number of key/value pairs as long.</returns>
        public long Count()
        {
            return Properties.Count;
        }

        /// <summary>
        /// Applies the PropertyGroup iProperties to the specified Inventor Document.  This version will overwrite any existing iProperties in the document.
        /// </summary>
        /// <param name="document">Inventor Document</param>
        public void ApplyToAndOverwrite(Document document)
        {
            foreach (string key in Properties.Keys)
            {
                WriteIPropertyToDocument(document, key, Properties[key]);
            }

        }

        /// <summary>
        /// Applies the PropertyGroup iProperties to the specified Inventor Document.  This version will not overwrite any existing iProperties in the document.
        /// </summary>
        /// <param name="document">Inventor Document</param>
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

        /// <summary>
        /// Determines if the PropertyGroup&lt;string, string&gt; contained the specified key.
        /// </summary>
        /// <param name="key">PropertyGroup key</param>
        /// <returns>Returns true if the PropertyGroup&lt;string, string&gt; contains an element with the specified key, otherwise false.</returns>
        public bool ContainsKey(string key)
        {
            return Properties.ContainsKey(key);
        }

        /// <summary>
        /// Removes the specified value with the specified key from the PropertyGroup&lt;string, string&gt; dictionary.
        /// </summary>
        /// <param name="key">PropertyGroup key</param>
        /// <returns>true if the element is successfully found and removed; otherwise, false. This method returns false if the key is not found in the PropertyGroup&lt;string, string&gt;</returns>
        bool IDictionary<string, string>.Remove(string key)
        {
            return Remove(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">PropertyGroup key</param>
        /// <param name="value">PropertyGroup value</param>
        /// <returns>true if the PropertyGroup&lt;string, string&gt; contains an element with the specified key, otherwise false.</returns>
        public bool TryGetValue(string key, out string value)
        {
            return Properties.TryGetValue(key, out value);
        }

        /// <summary>
        /// Removes all of the keys and values from the PropertyGroup&lt;string, string&gt;.
        /// </summary>
        public void Clear()
        {
            Properties.Clear();
        }

        /// <summary>
        /// Determine whether the PropertyGroup&lt;string, string&gt; contains a specified KeyValuePair&lt;string, string&gt;.
        /// </summary>
        /// <param name="item">KeyValuePair&lt;string, string&gt;</param>
        /// <returns>true if the PropertyGroup&lt;string, string&gt; contains the specified KeyValuePair&lt;string, string&gt;</returns>
        public bool Contains(KeyValuePair<string, string> item)
        {
            return Properties.ContainsKey(item.Key) && Properties.ContainsValue(item.Value);
        }

        /// <summary>
        /// Not implemented for now.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the specified value with the specified KeyValuePair from the PropertyGroup&lt;string, string&gt; dictionary.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if the element is successfully found and removed; otherwise, false. This method returns false if the KeyValuePair is not found in the PropertyGroup&lt;string, string&gt;</returns>
        public bool Remove(KeyValuePair<string, string> item)
        {
            return Properties.Remove(item.Key);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the PropertyGroup&lt;string, string&gt;.
        /// </summary>
        /// <returns>A Dictionary&lt;string, string&gt;.Enumerator structure for the PropertyGroup&lt;string, string&gt;.</returns>
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
