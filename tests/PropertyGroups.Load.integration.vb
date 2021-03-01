AddReference "iPropertyGroups.dll"
Imports iPropertyGroups

'Create a PropertyGroups object and load a json file into the object.
Dim oGroups As PropertyGroups = PropertyGroups.LoadJson("C:\Users\mjordan\_Sync\dev\github\iPropertyGroups\samples\sample-propertygroups-definition.json")

'Get active document object...
Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument

'Apply PropertyGroups object to the document...
oGroups.Item("Stock Part").ApplyToAndOverwrite(oDoc)

MsgBox("Number of Groups : " & oGroups.Count)
MsgBox("Number of Props : " & oGroups.Item("Stock Part").Count)

'For Each i As KeyValuePair(Of String, String) In oGroups.Item("Stock Part").Properties
'MsgBox(i.Key)
'next
