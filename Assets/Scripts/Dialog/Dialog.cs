using System;
using static XNode.Node;
using UnityEngine;

[Serializable]
public class Dialog
{
    public ESpeaker speaker;
    [TextArea]
    public string dialog;
}