using UnityEngine;
using XNodeEditor;

[CustomNodeEditor(typeof(DialogNode))]
public class DialogNodeEditor : NodeEditor
{
    public override void OnHeaderGUI()
    {
        GUI.color = Color.white;
        DialogNode node = target as DialogNode;
        DialogGraph graph = node.graph as DialogGraph;
        if (graph.currentNode == node) GUI.color = Color.blue;
        string title = target.name;
        GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
        GUI.color = Color.white;
    }
}
