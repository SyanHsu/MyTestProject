using Services;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameLauncher : MonoBehaviour
{
    public bool needConfirm;

    // 如果需要按键确认，则需要指定滑动窗和文字
    public Slider progressSlider;
    public Text reminderText;

    private SceneController sceneController;

    private void Start()
    {
        sceneController = ServiceLocator.Get<SceneController>();
        Invoke(nameof(StartGame), 1f);
    }

    private void StartGame()
    {
        if (needConfirm)
        {
            sceneController.AsyncLoadScene += OnLoadingScene;
        }
        sceneController.LoadScene(1, needConfirm);
    }

    private void OnLoadingScene(AsyncOperation asyncOperation)
    {
        StartCoroutine(WaitingLoadingScene(asyncOperation));
    }

    private IEnumerator WaitingLoadingScene(AsyncOperation asyncOperation)
    {
        do
        {
            progressSlider.value = asyncOperation.progress;
            yield return null;
        } while (asyncOperation.progress < 0.9f);
        progressSlider.value = 1f;
        reminderText.enabled = true;
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
        asyncOperation.allowSceneActivation = true;
        sceneController.AsyncLoadScene -= OnLoadingScene;
    }
}