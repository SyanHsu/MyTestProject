using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEngine.Events;

[NodeTint(100, 70, 70)]
public class BranchDialogNode : DialogNode
{
    [Input] public Empty Enter;
    public Dialog dialog;
    [Output(dynamicPortList = true), TextArea] public List<string> Branches;
    [HideInInspector] public int choice;

    // 使用前要先设定choice
    public override bool IsLastNode => choice < 0 ? false : 
        GetOutputPort("Branches" + choice).IsConnected;

    public override void MoveNextNode()
    {
        if (choice < 0)
        {
            Debug.LogError("未设定选项！");
            return;
        }

        DialogGraph dialogGraph = graph as DialogGraph;
        NodePort exitPort = GetOutputPort("Branches" + choice);
        if (!exitPort.IsConnected)
        {
            dialogGraph.SetCurrentNode(null);
        }
        else
        {
            dialogGraph.SetCurrentNode(exitPort.Connection.node as DialogNode);
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        choice = -1;
    }

    public void SetChoice(int choice)
    {
        this.choice = choice;
    }
}
