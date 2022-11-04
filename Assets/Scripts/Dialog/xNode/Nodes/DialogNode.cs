using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using XNode;

public abstract class DialogNode : Node
{
    [Serializable]
    public class Empty { }

    public abstract bool IsLastNode { get; }

    public override object GetValue(NodePort port)
    {
        return null;
    }

    public abstract void MoveNextNode();

    public virtual void OnEnter() { }

    public override void OnCreateConnection(NodePort from, NodePort to)
    {
        if (!from.node.GetType().IsSubclassOf(typeof(DialogNode)) ||
            !to.node.GetType().IsSubclassOf(typeof(DialogNode)))
        {
            Debug.Log("不能将DialogNode与其他类型的Node连接");
            from.Disconnect(to);
        }
    }
}
