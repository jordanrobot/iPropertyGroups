using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace iPropertyGroups
{
    [Serializable]
    public class PropertyGroups
    // : IEnumerable
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

        //public PropertyGroup Item(string key)
        //{
        //    if (Groups.ContainsKey(key))
        //        return Groups[key];

        //    return null;
        //}

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
    }
}
