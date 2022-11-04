using UnityEngine;
using XNode;

[CreateAssetMenu(menuName = "xNode/DialogGraph")]
public class DialogGraph : NodeGraph
{
    public DialogNode startNode;
    public DialogNode currentNode;

    public bool ReachLastNode => currentNode.IsLastNode;

    public void StartDialog()
    {
        if (startNode == null)
        {
            Debug.LogError($"{name}的startNode未设置！");
        }
        SetCurrentNode(startNode);
    }

    public void ContinueDialog()
    {
        currentNode.MoveNextNode();
    }

    public void SetCurrentNode(DialogNode dialogNode)
    {
        currentNode = dialogNode;
        if (dialogNode != null) dialogNode.OnEnter();
    }

    public void SetChoice(int choice)
    {
        BranchDialogNode currentBranch = currentNode as BranchDialogNode;
        if (currentBranch == null)
        {
            Debug.LogError("当前节点不是分支节点!");
            return;
        }
        currentBranch.SetChoice(choice);
    }
}