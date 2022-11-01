using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;

public class MyScene_DOTweenTestForImage : MonoBehaviour
{
    private Image image;
    private RectTransform rectTransform;

    private float timer;
    private float value;

    public Vector2 center;
    public float degree;

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        timer = 0f;
        value = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            float aimX = rectTransform.anchoredPosition.x + 100f;
            rectTransform.DOAnchorPosX(aimX, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            float aimX = rectTransform.anchoredPosition.x - 100f;
            rectTransform.DOAnchorPosX(aimX, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            rectTransform.DOPunchAnchorPos(Vector2.one * 100f, 2f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            rectTransform.DOShakeAnchorPos(2f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            rectTransform.DOShapeCircle(center, degree, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            rectTransform.DOComplete();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            DOTween.To(() => value, x => value = x, 5f, 1f).OnUpdate(OnUpdate);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    private void OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 0.1f)
        {
            timer = 0f;
            Debug.Log(value);
        }
    }
}
