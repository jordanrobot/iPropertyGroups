# iPropertyGroup Usage Examples

## Using a Single PropertyGroup

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

    if (propGroup.ContainsKey("Property1"))
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
