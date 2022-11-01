using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TMPro;

public class MyScene_DOTweenTestForSR : MonoBehaviour
{
    private SpriteRenderer sr;
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startScale;

    public Ease ease = Ease.Linear;

    private float timer;
    private float value;

    private void Awake()
    {
        DOTween.defaultEaseType = ease;
        sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.localScale;
        timer = 0f;
        value = 0f;
    }

    private Tween pathTween;
    private Tween yTween;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Grab a free Sequence to use
            Sequence mySequence = DOTween.Sequence();
            // Add a movement tween at the beginning
            mySequence.Append(transform.DOMoveY(-5, 1)).SetRelative();
            // Add a rotation tween as soon as the previous one is finished
            Tween tween = transform.DORotate(new Vector3(0, 180, 0), 1).SetDelay(2f);
            mySequence.Append(tween);
            // Delay the whole Sequence by 1 second
            mySequence.PrependInterval(1);
            // Insert a scale tween for the whole duration of the Sequence
            //mySequence.Insert(0, transform.DOScale(new Vector3(3, 3, 3), mySequence.Duration()));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.DOMoveY(0, 2).SetEase(ease);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.DOMoveY(-2, 2).SetId(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.DOMoveY(-2, 2).SetId(0).SetRelative();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Transform[] transList = GetComponentsInChildren<Transform>();
            var posList = transList.Select(x => x.position).ToArray();
            pathTween = transform.DOPath(posList, 5f, PathType.CatmullRom).SetEase(ease);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            //transform.DOMoveY(-5, 1).SetEase(Ease.Linear);
            transform.DOBlendableMoveBy(Vector3.right * 5f, 2f).OnComplete(OnCompleteXTween);
            yTween = transform.DOBlendableMoveBy(Vector3.down * 0.5f, 0.2f).SetLoops(-1, LoopType.Yoyo);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            transform.DOMoveY(-5, 1).SetSpeedBased().SetEase(Ease.Linear).OnUpdate(OnUpdate);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            DOTween.FlipAll();
            //DOTween.SmoothRewindAll();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log(DOTween.IsTweening(transform));
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            transform.position = startPos;
            transform.rotation = startRot;
            transform.localScale = startScale;
        }
    }

    private void OnUpdate()
    {
        timer += Time.deltaTime;
        value += Time.deltaTime;
        if (timer > 0.1f)
        {
            timer = 0f;
            Debug.Log(value);
        }
    }

    private void OnCompleteXTween()
    {
        if (yTween.active)
        {
            Debug.Log(yTween.CompletedLoops());
            yTween.Kill();
        }
    }
}
