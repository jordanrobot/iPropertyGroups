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
            PropertyGroup group = new PropertyGroup("", props);

            //validate
            Assert.IsNotNull(group);
        }

        [TestMethod]
        public void New2_constructorSetsName_success()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
                { "name1", "value1" }, { "name2", "value2" } };
            const string testName = "Test Name";

            //execute
            PropertyGroup group = new PropertyGroup(testName, props);

            //validate
            Assert.AreEqual(group.Name, testName);
        }

        [TestMethod]
        public void New2_constructorSetsDictionary_success()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
                { "name1", "value1" }, { "name2", "value2" } };

            //execute
            PropertyGroup group = new PropertyGroup("", props);

            //validate
            Assert.AreEqual(group.Properties["name1"], "value1");
        }
    }

    [TestClass]
    public class Properties
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
        [ExpectedException(typeof(System.Collections.Generic.KeyNotFoundException))]
        public void Remove_RemovesValidValueFromDictionary_EntryRemoved()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            PropertyGroup group = new PropertyGroup("", props);

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
            PropertyGroup group = new PropertyGroup("", props);

            //execute
            group.Remove("name16");

            //validate
            Assert.AreEqual(group.Properties.Count, testCount);
        }

        [TestMethod]
        public void Count_CountsEntriesInDictionary_ReturnsCorrectCount()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };
            var testCount = props.Count;

            //execute
            PropertyGroup group = new PropertyGroup("", props);

            //validate
            Assert.AreEqual(group.Count(), testCount);
        }
    }

    [TestClass]
    public class Name
    {
        [TestMethod]
        public void Name_SetsName_success()
        {
            //setup
            PropertyGroup group = new PropertyGroup();
            const string test = "Test Value";

            //execute
            group.Name = test;

            //validate
            Assert.AreEqual(group.Name, test);
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
            PropertyGroup group = new PropertyGroup("test", props);

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
            PropertyGroup group = new PropertyGroup("test", props);

            //execute
            group["name1"] = "Bob";

            //validate
            var test = group["name1"];
            Assert.AreEqual("Bob", test);
        }

    }
}
