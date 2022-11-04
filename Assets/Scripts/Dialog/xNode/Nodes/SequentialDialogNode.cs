using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint(100, 100, 50)]
public class SequentialDialogNode : DialogNode
{
    [Input] public Empty Enter;
    [SerializeField] private List<Dialog> dialogs;
    private int currentDialogIndex;
    public Dialog CurrentDialog => dialogs[currentDialogIndex];
    [Output] public Empty Next;

    public override bool IsLastNode => GetOutputPort("Next").IsConnected;

    public bool MoveOn()
    {
        if (++currentDialogIndex >= dialogs.Count)
        {
            currentDialogIndex = 0;
            return false;
        }
        return true;
    }

    public override void MoveNextNode()
    {
        DialogGraph dialogGraph = graph as DialogGraph;
        NodePort exitPort = GetOutputPort("Next");
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
        currentDialogIndex = 0;
    }
}
