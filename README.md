# iPropertyMap

A library for Autodesk Inventor that lets you manage user-defined groups of iProperties.

### Rough Thoughts

Lets say you have a group of iProperties that you want to apply to various inventor files.  Typically you'd hard-code the groups of iProperties, the default expressions and values into iLogic scripts with the logic to manage all of this.

This library aims to take most of that management drudgery out of your hands. You will be able to define a group of iProperties and default values dynamically into an iPropertyMap object. You may also be able to define iPropertyMaps in json files (I need to think on this). This object will let you apply this group to documents in your code.


## Implementation Idea #1

    iPropertyMap propMap = new iPropertyMap;

Load from a json file:

    propMap.Load("custom definitions.json");

or load individually...

    propMap.Add("Property1","Default Value");
    propMap.Add("Property2","=<Title>");
    propMap.Add("Property3","=<Property2>");
    propMap.Add("Property4","Default Value");
    propMap.Add("Property5","Default Value");
    propMap.Add("Title","This Cool Part");
    propMap.Add("Property6","Default Value");

or load in a constructor...

    iPropertyMap propMap = new iPropertyMap {
        {"Property1","Default Value"},
        {"Property2","=<Title>"},
        {"Property3","=<Property2>"},
        {"Property4","Default Value"},
        {"Property5","Default Value"},
        {"Title","This Cool Part"},
        {"Property6","Default Value"}
    };

Test if propMap contains a property value...

    if (propMap.Contains("Property1"))
        //do something

Get value of one of the properties...

    var temp = propMap["Property1"];

Set the value of one of the properties...

    propMap["Property1"] = "=<Description>";

Add iProperties to a document:

    propMap.Apply(Document doc, bool overwrite);


## Implementation Idea #2

Perhaps the iPropertyMaps are loaded into a singleton iPropertyMapTable?  This way they can be recalled from any of your code?
Perhaps this could work with multiple definitions?
e.g. 

    iPropertyMapTable ipmt = iPropertyMapTable.Instance();
    ipmt.Add("Stock Part")
        .Load("stock-part-definition.json");
    ipmt.Add("Fabricated Part")
        .Load("fabricated-definition.json");

or perhaps the different maps can be located in a single json file...

    iPropertyMapTable ipmt = iPropertyMapTable.Instance();
    ipmt.Load("iprop-map-definitions.json");

or load in constructor:

    iPropertyMapTable ipmt = iPropertyMapTable.Instance();
    ipmt.Add("Stock Part")
        .Add {
            {"Property1","Default Value"},
            {"Property2","=<Title>"},
            {"Property3","=<Property2>"},
            {"Property4","Default Value"},
            {"Property5","Default Value"},
            {"Title","This Cool Part"},
            {"Property6","Default Value"}
        };

elsewhere:

    iPropertyMap stockPartMap = iPropertyMapTable["Stock Part"];
    stockPartMap.Apply(document, false)

or:

    iPropertyMapTable["Stock Part"].Apply(document, true)


## JSON file operability

I'm still not convinced that this is super useful, but it may be a way to share definitions across code-bases, and update the definitions without touching deployed code.

iPropertyMap definition:

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

iPropertyMapTable definition:

    {
        "Name": "The name of this iPropertyTableMap",
        "Table": {
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