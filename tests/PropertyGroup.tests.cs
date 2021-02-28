using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using iPropertyGroups;

namespace PropertyGroupTests
{
    [TestClass]
    public class Constructors
    {
        [TestMethod]
        public void New1_constructorCreatesObject_success()
        {
            //execute
            PropertyGroup group = new PropertyGroup();

            //validate
            Assert.IsNotNull(group);
        }

        [TestMethod]
        public void New2_constructorCreatesObject_success()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
                { "name1", "value1" }, { "name2", "value2" } };

            //execute
            PropertyGroup group = new PropertyGroup(props);

            //validate
            Assert.IsNotNull(group);
        }

        [TestMethod]
        public void New2_constructorSetsDictionary_success()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
                { "name1", "value1" }, { "name2", "value2" } };

            //execute
            PropertyGroup group = new PropertyGroup(props);

            //validate
            Assert.AreEqual(group.Properties["name1"], "value1");
        }
    }

    [TestClass]
    public class Properties
    {

        [TestMethod]
        [ExpectedException(typeof(System.Collections.Generic.KeyNotFoundException))]
        public void Remove_RemovesValidValueFromDictionary_EntryRemoved()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            PropertyGroup group = new PropertyGroup(props);

            //execute
            group.Remove("name1");

            //validate
            Assert.Fail(_ = group.Properties["name1"]);
        }

        [TestMethod]
        public void Remove_RemovesInvalidValueFromDictionary_EntryIgnored()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            var testCount = props.Count;
            PropertyGroup group = new PropertyGroup(props);

            //execute
            group.Remove("name16");

            //validate
            Assert.AreEqual(group.Properties.Count, testCount);
        }

        [TestMethod]
        public void RemoveKeyValuePair_returnsTrue()
        {
            //setup
            PropertyGroup group = new PropertyGroup();
            const string testKey = "Test Key";
            const string testValue = "Test Value";
            KeyValuePair<string, string> item = new KeyValuePair<string, string>(testKey, testValue);

            //execute
            group.Add(item);

            //validate
            Assert.IsTrue(group.Remove(item));
        }

        [TestMethod]
        public void RemoveKeyValuePair_returnsFalse()
        {
            //setup
            PropertyGroup group = new PropertyGroup();
            const string testKey = "Test Key";
            const string testValue = "Test Value";
            KeyValuePair<string, string> item = new KeyValuePair<string, string>(testKey, testValue);
            KeyValuePair<string, string> itemFake = new KeyValuePair<string, string>("Bob", "Dole");
            group.Add(item);

            //execute
            //validate
            Assert.IsFalse(group.Remove(itemFake));
        }
    }

    [TestClass]
    public class Count {
        [TestMethod]
        public void Count_CountsEntriesInDictionary_ReturnsCorrectCount()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            var testCount = props.Count;

            //execute
            PropertyGroup group = new PropertyGroup(props);

            //validate
            Assert.AreEqual(group.Count(), testCount);
        }
    }

    [TestClass]
    public class This
    {
        [TestMethod]
        public void this_Get_ReturnsValue()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            PropertyGroup group = new PropertyGroup(props);

            //execute
            var test = group["name1"];

            //validate
            Assert.AreEqual("value1", test);
        }

        [TestMethod]
        public void this_Set_SetsValue()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            PropertyGroup group = new PropertyGroup(props);

            //execute
            group["name1"] = "Bob";

            //validate
            var test = group["name1"];
            Assert.AreEqual("Bob", test);
        }
    }

    [TestClass]
    public class Contains
    {
        [TestMethod]
        public void ContainsKeyValuePair_returnsTrue()
        {
            //setup
            PropertyGroup group = new PropertyGroup();
            const string testKey = "Test Key";
            const string testValue = "Test Value";
            KeyValuePair<string, string> item = new KeyValuePair<string, string>(testKey, testValue);

            //execute
            group.Add(item);

            //validate
            Assert.IsTrue(group.Contains(item));
        }

        [TestMethod]
        public void ContainsKeyValuePair_returnsFalse()
        {
            //setup
            PropertyGroup group = new PropertyGroup();
            const string testKey = "Test Key";
            const string testValue = "Test Value";
            KeyValuePair<string, string> item = new KeyValuePair<string, string>(testKey, testValue);
            KeyValuePair<string, string> itemFake = new KeyValuePair<string, string>("Bob", "Dole");

            //execute
            group.Add(item);

            //validate
            Assert.IsFalse(group.Contains(itemFake));
        }
    }
    [TestClass]
    public class ContainsKey
    {
        [TestMethod]
        public void ContainsKey_ReturnsTrue()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            PropertyGroup group = new PropertyGroup(props);

            //execute

            //validate
            Assert.IsTrue(group.ContainsKey("name1"));
        }

        [TestMethod]
        public void ContainsKey_ReturnsFalse()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            PropertyGroup group = new PropertyGroup(props);

            //execute

            //validate
            Assert.IsFalse(group.ContainsKey("name0"));
        }
    }

    [TestClass]
    public class TryGetValue
    {
        [TestMethod]
        public void TryGetValue_ReturnsTrue()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            PropertyGroup group = new PropertyGroup(props);
            //execute

            //validate
            Assert.IsTrue(group.TryGetValue("name1", out _));
        }

        [TestMethod]
        public void ContainsKey_ReturnsFalse()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            PropertyGroup group = new PropertyGroup(props);

            //execute

            //validate
            Assert.IsFalse(group.TryGetValue("name0", out _));
        }
    }

    [TestClass]
    public class Add
    {
        [TestMethod]
        public void Add_AddsValueToDictionary_success()
        {
            //setup
            PropertyGroup group = new PropertyGroup();
            const string testKey = "Test Key";
            const string testValue = "Test Value";

            //execute
            group.Add(testKey, testValue);

            //validate
            Assert.AreEqual(group.Properties[testKey], testValue);
        }

        [TestMethod]
        public void AddKeyValuePair_success()
        {
            //setup
            PropertyGroup group = new PropertyGroup();
            const string testKey = "Test Key";
            const string testValue = "Test Value";
            KeyValuePair<string, string> item = new KeyValuePair<string, string>(testKey, testValue);

            //execute
            group.Add(item);

            //validate
            Assert.AreEqual(group.Properties[testKey], testValue);
        }
    }

    [TestClass]
    public class Clear
    {
        [TestMethod]
        public void Clear_success()
        {
            //setup
            PropertyGroup group = new PropertyGroup();
            const string testKey = "Test Key";
            const string testValue = "Test Value";

            //execute
            group.Add(testKey, testValue);
            group.Clear();

            //validate
            Assert.IsFalse(group.ContainsKey(testKey));
        }

        [TestMethod]
        public void TryGetValue_success()
        {
            //setup
            PropertyGroup group = new PropertyGroup();
            const string testKey = "Test Key";
            const string testValue = "Test Value";
            KeyValuePair<string, string> item = new KeyValuePair<string, string>(testKey, testValue);

            //execute
            group.Add(item);

            //validate
            Assert.AreEqual(group.Properties[testKey], testValue);
        }
    }
}
