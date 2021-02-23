# iPropertyGroup

A library for Autodesk Inventor that lets you manage user-defined groups of iProperties.

### Rough Thoughts

Lets say you have a group of iProperties that you want to apply to various inventor files.  Typically you'd hard-code the groups of iProperties, the default expressions and values into iLogic scripts with the logic to manage all of this.

This library aims to take most of that management drudgery out of your hands. You will be able to define a group of iProperties and default values dynamically into an iPropertyGroup object.  The iPropertyGroup objects are housed and managed from an iPropertyGroups object. You will be able to define iPropertyGroups from json files.

Perhaps the user could also load PropertyGroups from json files at http\[s] URIs?

## Using a single PropertyGroup

    iPropertyGroup propGroup = new iPropertyGroup;

Load from a json file:

    propGroup.Load("custom definitions.json");

or load the properties individually...

    propGroup.Add("Property1","Default Value");
    propGroup.Add("Property2","=<Title>");
    propGroup.Add("Property3","=<Property2>");
    propGroup.Add("Property4","Default Value");
    propGroup.Add("Property5","Default Value");
    propGroup.Add("Title","This Cool Part");
    propGroup.Add("Property6","Default Value");

or load in a constructor...

    PropertyGroup propGroup = new iPropertyGroup {
        {"Property1","Default Value"},
        {"Property2","=<Title>"},
        {"Property3","=<Property2>"},
        {"Property4","Default Value"},
        {"Property5","Default Value"},
        {"Title","This Cool Part"},
        {"Property6","Default Value"}
    };

Test if propGroup contains a property value...

    if (propGroup.Contains("Property1"))
        //do something

Get value of one of the properties...

    var temp = propGroup["Property1"];

Set the value of one of the properties...

    propGroup["Property1"] = "=<Description>";

Add iProperties to a document (without overwriting existing properties):

    propGroup.ApplyTo(document);

Add iProperties to a document (with overwriting existing properties):

    propGroup.ApplyToAndOverwrite(document);


## Using multiple PropertyGroups

You can use multiple PropertyGroup objects by utilizing the PropertyGroups object. 

    PropertyGroups groups = new PropertyGroups();

Load from a json file:

    groups.Load("custom definitions.json");

Or perhaps load from a json file in a static constructor like so....

    PropertyGroups groups = PropertyGroups.Load("iprop-map-definitions.json");


or load each PropertyGroup individually...

    groups.Add("name", PropertyGroup);
    groups.Add("name2", PropertyGroup);
    groups.Add("name3", PropertyGroup);

remove a PropertyGroup...

    groups.Remove("name", PropertyGroup);


To apply a PropertyGroup to a document:

(C# example):

    groups["Stock Part"].ApplyTo(document)

(VBA example):

    groups("Stock Part").ApplyTo(document)



## JSON file operability

This is a way to share definitions across code-bases, and update the PropertyGroup definitions without touching deployed code.

iPropertyGroup definition:

    {
        "Stock Part": {
            "Property1": "Default Value",
            "Property2": "=<Title>",
            "Property3": "=<Property2>",
            "Property4": "Default Value",
            "Property5": "Default Value",
            "Title": "This Cool Part",
            "Property6": "Default Value"
        }
    }

iPropertyGroups definition:

    {
        "Groups": {
            "Stock Part": {
                "Property1": "Default Value",
                "Property2": "=<Title>",
                "Property3": "=<Property2>",
                "Property4": "Default Value",
                "Property5": "Default Value",
                "Title": "This Cool Part",
                "Property6": "Default Value"
            }
            "Fabricated Part": {
                "Property1": "Default Value",
                "Property2": "=<Title>",
                "Property3": "=<Property2>",
                "Property4": "Default Value",
                "Property5": "Default Value",
                "Title": "This Cool Part",
                "Property6": "Default Value"
            }
        }
    }

## Structure

Target: .net Standard 2.0

PropertyGroups : <IDictionary>?
    - List<iPropertyGroup> groups
    + Add (string Name, List<PropertyGroupEntry>)
    + this[string key]
    + Load (string jsonFile)
        (internal code idea...)
        Deserialize JSON
        var response = JsonConvert.DeserializeObject<Dictionary<string,Dictionary<string,string>>>(json);
        foreach (string key in response.keys){
            PropertyGroup group = new PropertyGroup(key, response[key])
            groups.Add(group)
        }
    + Save (string jsonFile)?
        Serialize JSON?

PropertyGroup
    + PropertyGroup ()
    + PropertyGroup (string Name, Dictionary<string,string>)
    + this[string key]
    + Add (string key, string value)
    + Remove (string key)
    + Count () : long
    + ApplyTo(Inventor.Document document)
    + ApplyToAndOverwrite(Inventor.Document document)
    + Load???? (string jsonFile)

PropertyEditor
    // Manages writing the property to the document, keeps rest of code isolated from inventor & testable
    // a renamed copy of [PropertyShim.cs]https://github.com/InventorCode/InventorShims/blob/master/src/InventorShims/PropertyShim.cs) from [InventorShims](https://github.com/InventorCode/InventorShims)



## Singleton Idea?

Perhaps the iPropertyGroup are loaded into a singleton iPropertyGroups?  This way they can be recalled after loaded from a single instance?
e.g. 

    iPropertyGroups groups = iPropertyGroups.Instance();

* How would you know the singleton had been loaded and configured before you ran it?
* Would the user define the singleton loading procedure in an instance loader?  This seems messy.
