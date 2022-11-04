using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;

[CustomNodeGraphEditor(typeof(DialogGraph))]
public class DialogGraphEditor : NodeGraphEditor
{
    public override string GetNodeMenuName(System.Type type)
    {
        if (type.Namespace == null)
        {
            return base.GetNodeMenuName(type);
        }
        else return null;
    }
}
