using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogStarter : MonoBehaviour
{
    public DialogBox dialogBox;
    public DialogGraph dialogGraph;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        dialogBox.StartDialog(dialogGraph);
    }
}
