AddReference "iPropertyGroups.dll"
Imports System.Collections.Generic
Imports iPropertyGroups

Dim props As Dictionary(Of String, String) = New Dictionary(Of String, String) From {
	{"name1", "value1" },
	{"name2", "value2"},
	{"variableThing", "valuesssss"},
	{"Title", "stuff stuff!"}}

props.Add("Mirror", "=<Title>")


Dim oGroup As PropertyGroup = New PropertyGroup("", props)
Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
oGroup.ApplyToAndOverwrite(oDoc)