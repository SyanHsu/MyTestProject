using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogNodeUtility
{
    public static bool IsSequential(this DialogNode dialogNode)
    {
        return dialogNode as SequentialDialogNode != null;
    }

    public static bool IsBranch(this DialogNode dialogNode)
    {
        return dialogNode as BranchDialogNode != null;
    }
}
