# iPropertyGroup

A library for Autodesk Inventor that lets you manage user-defined groups of iProperties.

### What does it do?

Lets say you have a group of iProperties that you want to apply to various Inventor files.  Typically you'd hard-code a bunch of iProperties and default expressions/values into your code.  Then you'd write custom logic to manage and apply these to documents.

This library aims to take most of that management drudgery out of your hands. You will be able to define a group of iProperties and default values dynamically (and simply) into an PropertyGroup object.  The definitions are a dictionary of simple strings.  If you have multiple PropertyGroup objects, they can be managed from an PropertyGroups object.  You will also be able to define PropertyGroup(s) objects from json files.  (Perhaps the user could also load PropertyGroups from json files at http(s) URIs?)

This library will not do any checks against what documents are suitable to be modified (e.g. content center files, library files, etc).


## Class Structure Ideas

### PropertyGroups

+ Groups : (Dictionary<string,PropertyGroup>)
+ this[string key] : PropertyGroup
+ Add (string Name, List<PropertyGroupEntry>)
+ Remove (string Name)
+ Count() : int
+ SaveJson (string jsonFile)
+ LoadJson (string jsonFile)

### PropertyGroup

+ PropertyGroup () : IDictionary<string, string>
+ PropertyGroup (string Name, Dictionary<string,string>)
+ Group : (Dictionary<string,string>)
+ this[string key] : string
+ Add (string key, string value)
+ Remove (string key)
+ Count () : int
+ ApplyTo(Inventor.Document document)
+ ApplyToAndOverwrite(Inventor.Document document)

### PropertyEditor (internal class)

- Manages writing the property to the document, keeps rest of code isolated from inventor & testable
- a renamed copy of [PropertyShim.cs]https://github.com/InventorCode/InventorShims/blob/master/src/InventorShims/PropertyShim.cs) from [InventorShims](https://github.com/InventorCode/InventorShims)

## Using a single PropertyGroup

Constructor:

    iPropertyGroup propGroup = new iPropertyGroup;

Load the properties individually...

    propGroup.Add("Property1","Default Value");
    propGroup.Add("Property2","=<Title>");
    propGroup.Add("Property3","=<Property2>");
    propGroup.Add("Property4","Default Value");
    propGroup.Add("Property5","Default Value");
    propGroup.Add("Title","This Cool Part");
    propGroup.Add("Property6","Default Value");

or load in a constructor manually...

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

    groups.LoadJson("custom definitions.json");

Or perhaps load from a json file in a static constructor like so....

    PropertyGroups groups = PropertyGroups.LoadJson("iprop-map-definitions.json");


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

    groups.Item("Stock Part").ApplyTo(document)



## JSON file operability

This is a way to share definitions across code-bases, and update the PropertyGroup definitions without touching deployed code.

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

## Build

* Build Target: .net Framework 4.7
* Tests Build Target: .net 5.0

This project aims to be developed primarily via Test Driven Development. The only bits of code that will not be driven by TDD are the bits that interface with Inventor itself.