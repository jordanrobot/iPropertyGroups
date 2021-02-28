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

    [TestClass]
    public class Load_tests
    {
        [TestMethod]
        public void Load_LoadFile_WithoutException()
        {
            //setup

            //execute
            PropertyGroups groups = PropertyGroups.LoadJson(@"resources\test-config.json");

            //validate
            var _ = groups["Stock Part"].Count();
        }

        [TestMethod]
        public void Load_CountPropertyInside_CorrectNumber()
        {
            //setup
            PropertyGroups groups = PropertyGroups.LoadJson(@"resources\test-config.json");

            //execute
            var testCount = groups["Stock Part"].Count();

            //validate
            Assert.AreEqual(testCount, 7);
        }

        [TestMethod]
        public void SaveTest_createsFile()
        {
            //setup
            Dictionary<string, string> props = new Dictionary<string, string> {
                { "name1", "value1" }, { "name2", "value2" },{"name3","value3" }};
            PropertyGroup group = new PropertyGroup(props);

            Dictionary<string, string> props2 = new Dictionary<string, string> {
                { "nameA", "valueA" }, { "nameB", "valueB" },{"nameC","valueC" }};
            PropertyGroup group2 = new PropertyGroup(props2);


            PropertyGroups groups = new PropertyGroups();
            groups.Add("Group1", group);
            groups.Add("Group2", group2);
            var _executableLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var file = System.IO.Path.Combine(_executableLocation, "output.json");

            //execute
            groups.SaveJson(file);

            //validate
            try
            {
                Assert.IsTrue(System.IO.File.Exists(file));
            }
            finally
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch { }
            }
        }
    }
}