using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using iPropertyGroups;

namespace PropertyGroupsTests
{
    [TestClass]
    public class Constructor_tests
    {
        [TestMethod]
        public void New_validEntry_success()
        {
            //setup
            PropertyGroups groups = new PropertyGroups();

            //validate
            Assert.IsNotNull(groups);
        }
    }

    [TestClass]
    public class Item_tests
    {
        [TestMethod]
        public void Item_Read_success()
        {
            //setup
            const string item = "Test Item";
            Dictionary<string, string> props = new Dictionary<string, string> {
                { "name1", "value1" }, { "name2", "value2" } };

            PropertyGroup group = new PropertyGroup(props);
            PropertyGroups groups = new PropertyGroups();
            groups.Add(item, group);

            //execute
            var test = groups[item];

            //validate
            Assert.AreEqual(test, test);
        }

        [TestMethod]
        public void ItemShortSyntax_Read_success()
        {
            //setup
            const string item = "Test Item";
            Dictionary<string, string> props = new Dictionary<string, string> {
            { "name1", "value1" }, { "name2", "value2" } };

            PropertyGroup group = new PropertyGroup(props);
            PropertyGroups groups = new PropertyGroups();
            groups.Add(item, group);

            //execute
            var test = (PropertyGroup)groups[item];

            //validate
            Assert.AreEqual(test, test);
        }
    }

    [TestClass]
    public class Add_tests
    {
        [TestMethod]
        public void Add_item_ItemExists()
        {
            //setup
            const string item = "Test Item";
            Dictionary<string, string> props = new Dictionary<string, string> {
                { "name1", "value1" }, { "name2", "value2" } };

            PropertyGroup group = new PropertyGroup(props);
            PropertyGroups groups = new PropertyGroups();

            //execute
            groups.Add(item, group);

            //validate
            Assert.IsNotNull(groups[item]);
        }

        [TestMethod]
        public void Add_PutsItemValues_AreCorrect()
        {
            //setup
            const string item = "Test Item";
            Dictionary<string, string> props = new Dictionary<string, string> {
                { "name1", "value1" }, { "name2", "value2" } };

            PropertyGroup group = new PropertyGroup(props);
            PropertyGroups groups = new PropertyGroups();

            //execute
            groups.Add(item, group);

            //validate
            Assert.AreEqual(groups[item].Properties["name1"], "value1");
        }
    }

    [TestClass]
    public class Remove_tests
    {
        [TestMethod]
        public void Remove_item_ItemDoesNotExist()
        {
            //setup
            const string item = "Test Item";
            Dictionary<string, string> props = new Dictionary<string, string> {
                { "name1", "value1" }, { "name2", "value2" } };

            PropertyGroup group = new PropertyGroup(props);
            PropertyGroups groups = new PropertyGroups();
            groups.Add(item, group);

            //execute
            groups.Remove(item);

            //validate
            Assert.IsNull(groups[item]);
        }
    }

    [TestClass]
    public class Count
    {
        [TestMethod]
        public void Count_returnsCorrectNumber()
        {
            //setup
            const string item = "Test Item";
            Dictionary<string, string> props = new Dictionary<string, string> {
                { "name1", "value1" }, { "name2", "value2" } };

            PropertyGroup group = new PropertyGroup(props);
            PropertyGroups groups = new PropertyGroups();
            groups.Add(item, group);

            //execute
            //validate
            Assert.AreEqual(groups.Count(), 1);
        }
    }
}