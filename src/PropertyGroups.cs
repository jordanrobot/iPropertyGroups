using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace iPropertyGroups
{
    [Serializable]
    public class PropertyGroups
    {
        private Dictionary<string, PropertyGroup> _groups =
            new Dictionary<string, PropertyGroup>();
        public Dictionary<string, PropertyGroup> Groups
        {
            get { return _groups; }
            set { _groups = value; }
        }

        public void Add(string item, PropertyGroup props)
        {
            _groups.Add(item, props);
        }

        public bool Remove(string key)
        {
            return _groups.Remove(key);
        }

        public PropertyGroup this[string key]
        {
            get
            {
                // If this key is in the dictionary, return its value.
                if (_groups.ContainsKey(key))
                {
                    return _groups[key];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                // If this key is in the dictionary, change its value.
                if (_groups.ContainsKey(key))
                {
                    // The key was found; change its value.
                    _groups[key] = (PropertyGroup)value;
                }
                else
                {
                    // This key is not in the dictionary; add this key/value pair.
                    _groups.Add(key, (PropertyGroup)value);
                }
            }
        }

        public long Count()
        {
            return Groups.Count;
        }

        public void SaveJson(string filePath)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            StreamWriter sw = new StreamWriter(filePath);
            JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, this);
            writer.Close();
        }

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
