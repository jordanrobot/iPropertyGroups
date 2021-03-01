using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace iPropertyGroups
{
    /// <summary>
    /// Represents a dictionary of PropertyGroup objects.
    /// </summary>
    [Serializable]
    public class PropertyGroups
    {
        /// <summary>
        /// Main dictionary object that holds the PropertyGroup objects.
        /// </summary>
        public Dictionary<string, PropertyGroup> Groups { get; set; } =
            new Dictionary<string, PropertyGroup>();

        /// <summary>
        /// Adds the specified key and value to the collection.
        /// </summary>
        /// <param name="item">Name of the PropertyGroup</param>
        /// <param name="props">The PropertyGroup object</param>
        public void Add(string item, PropertyGroup props)
        {
            Groups.Add(item, props);
        }

        /// <summary>
        /// Removes the specified value with the specified key from the PropertyGroups&lt;string, PropertyGroup&gt; dictionary.
        /// </summary>
        /// <param name="key">Name of PropertyGroup</param>
        /// <returns>true if the element is successfully found and removed; otherwise, false. This method returns false if the key is not found in the PropertyGroups&lt;string, PropertyGroup&gt;</returns>
        public bool Remove(string key)
        {
            return Groups.Remove(key);
        }

        /// <summary>
        /// Returns a Dictionary value for the provided key.
        /// </summary>
        /// <param name="key">Name of the PropertyGroup</param>
        /// <returns>PropertyGroup object</returns>
        public PropertyGroup this[string key]
        {
            get
            {
                // If this key is in the dictionary, return its value.
                if (Groups.ContainsKey(key))
                {
                    return Groups[key];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                // If this key is in the dictionary, change its value.
                Groups[key] = (PropertyGroup)value;
            }
        }

        /// <summary>
        /// Gets the number of key/value pairs in the PropertyGroups&lt;string, PropertyGroup&gt; object.
        /// </summary>
        /// <returns>the number of key/value pairs as long.</returns>
        public long Count()
        {
            return Groups.Count;
        }

        /// <summary>
        /// Saves the PropertyGroups&lt;string, PropertyGroup&gt; object to a json file.
        /// </summary>
        /// <param name="filePath">The file path to save the json file.</param>
        public void SaveJson(string filePath)
        {
            JsonSerializer serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            StreamWriter sw = new StreamWriter(filePath);
            JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, this);
            writer.Close();

            serializer = null;
            writer = null;
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// A static method to load a json file into a new PropertyGroups&lt;string, PropertyGroup&gt; object.
        /// </summary>
        /// <param name="filePath">The file path of the json file.</param>
        /// <returns>PropertyGroups&lt;string, PropertyGroup&gt; if successful; otherwise returns null.</returns>
        public static PropertyGroups LoadJson(string filePath)
        {
            var _configRaw = GetFileContents(filePath);
            return DeserializeConfiguration(_configRaw);
        }

        private static string GetFileContents(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception e)
            {
                throw new SystemException("There was an error reading the json configuration file from disk.  Process was aborted, press any key to continue...", e);
            }
        }

        private static PropertyGroups DeserializeConfiguration(string _configRaw)
        {
            try
            {
                return JsonConvert.DeserializeObject<PropertyGroups>(_configRaw);
            }
            catch (Exception e)
            {
                throw new SystemException("The configuration was invalid, please verify json file syntax.  Process was aborted, press any key to continue...", e);
            }
        }
    }
}
