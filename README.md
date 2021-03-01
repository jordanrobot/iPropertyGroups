# iPropertyGroups

A library for Autodesk Inventor that lets you manage user-defined groups of iProperties.

### What does it do?

Lets say you have a group of iProperties that you want to apply to various Inventor files.  Typically you'd hard-code a bunch of iProperties and default expressions/values into your code.  Then you'd write custom logic to manage and apply these to documents.

This library takes most of that management drudgery out of your hands. You can define a group of iProperties into an PropertyGroup object.  The PropertyGroup objects are a dictionaries of simple strings with some extra methods.  If you have multiple PropertyGroup objects, they can be managed from an PropertyGroups object.  You can load PropertyGroups objects from json files.

This library will not do any checks against what documents are suitable to be modified (e.g. content center files, library files, etc).


* Here is a simple iLogic rule showing the library in use:

    ``` VB
    AddReference "iPropertyGroups.dll"
    Imports iPropertyGroups

    'Create a PropertyGroups object and load a json file into the object.
    Dim oGroups As PropertyGroups = PropertyGroups.LoadJson("C:\Path\to\your\propertygroups-definition.json")

    'Get active document object...
    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument

    'Apply PropertyGroups object to the document...
    oGroups.Item("Stock Part").ApplyToAndOverwrite(oDoc)
    ```

### Documentation

Find the detailed instructions and API reference [here](https://jordanrobot.github.io/iPropertyGroups/index.html).

## License

This code is under an MIT license.
