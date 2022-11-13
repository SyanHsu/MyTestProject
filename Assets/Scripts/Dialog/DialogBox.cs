using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class DialogBox : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 200, 0);

    public Text dialogText;
    public GameObject buttons;
    public GameObject buttonPrefab;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public bool Visible => canvasGroup.alpha == 1;
    
    public DialogGraph CurrentDialogGraph { get; private set; }
    private DialogNode CurrentDialogNode => CurrentDialogGraph.currentNode;
    private Tweener textTween;

    private bool currentStyle;
    private bool waitingForSelecting;

    private void Awake()
    {
        rectTransform = dialogText.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        textTween = dialogText.DOText("", 5f).SetSpeedBased(true)
            .SetEase(Ease.Linear).SetAutoKill(false).Pause();
        currentStyle = true;
        waitingForSelecting = false;
    }

    private void Update()
    {
        if (Visible && Input.GetMouseButtonDown(0) && !waitingForSelecting)
        {
            if (textTween.IsPlaying()) textTween.Complete();
            else
            {
                ContinueDialog();
            }
        }
    }

    public void SetCurrentDialog(DialogGraph dialogGraph)
    {
        CurrentDialogGraph = dialogGraph;
    }

    public void StartDialog(DialogGraph dialogGraph = null)
    {
        canvasGroup.alpha = 1f;
        if (dialogGraph != null) SetCurrentDialog(dialogGraph);
        else if (CurrentDialogGraph == null)
        {
            Debug.LogError("当前无可用对话！");
        }
        CurrentDialogGraph.StartDialog();
        ContinueDialog();
    }

    private void ContinueDialog()
    {
        if (CurrentDialogNode == null)
        {
            EndDialog();
        }
        else if (CurrentDialogNode.IsSequential())
        {
            SequentialDialogNode sNode = 
                CurrentDialogGraph.currentNode as SequentialDialogNode;
            ShowDialog(sNode.CurrentDialog, true);
            if (!sNode.MoveOn())
            {
                CurrentDialogGraph.ContinueDialog();
            }
        }
        else if (CurrentDialogNode.IsBranch())
        {
            if (waitingForSelecting)
            {
                waitingForSelecting = false;
                CurrentDialogGraph.ContinueDialog();
                ContinueDialog();
            }
            else
            {
                ShowDialog(((BranchDialogNode)CurrentDialogGraph.currentNode).dialog, false);
            }
        }
    }

    private void EndDialog()
    {
        canvasGroup.alpha = 0;
        CurrentDialogGraph = null;
    }

    private void ShowDialog(Dialog dialog, bool sequential)
    {
        ChangeStyle(sequential);

        Transform speakerTransform = SpeakerManager.Get(dialog.speaker).transform;
        transform.position = Camera.main.WorldToScreenPoint(
            speakerTransform.position) + offset;

        textTween.ChangeEndValue(dialog.dialog, false).Restart();

        if (sequential) textTween.OnComplete(null);
        else textTween.OnComplete(ShowButtons);
    }

    private void ChangeStyle(bool sequential)
    {
        if (sequential == currentStyle) return;
        currentStyle = sequential;
        if (sequential)
        {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 30);
        }
        else
        {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 60);
        }
    }

    private void ShowButtons()
    {
        buttons.SetActive(true);
        SetButtons((CurrentDialogNode as BranchDialogNode).Branches);
        waitingForSelecting = true;
    }

    private void HideButtons()
    {
        buttons.SetActive(false);
        ContinueDialog();
    }

    private void SetButtons(List<string> branches)
    {
        int count = branches.Count;
        List<Button> buttonList = buttons.
            GetComponentsInChildren<Button>(true).ToList();
        if (buttonList.Count < count)
        {
            while (buttonList.Count < count)
            {
                Button newButton = Instantiate(buttonPrefab, buttons.transform)
                    .GetComponent<Button>();
                int choice = buttonList.Count;
                newButton.onClick.AddListener(() => {
                    CurrentDialogGraph.SetChoice(choice);
                });
                newButton.onClick.AddListener(HideButtons);
                buttonList.Add(newButton);
            }
        }
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (i < count)
            {
                buttonList[i].gameObject.SetActive(true);
                buttonList[i].GetComponentInChildren<Text>().text = branches[i];
            }
            else
            {
                buttonList[i].gameObject.SetActive(false);
            }
        }
    }
}
