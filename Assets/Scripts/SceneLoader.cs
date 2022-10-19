using Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonobehSingleton<SceneLoader>
{
    [SerializeField] private bool isLoadNextSceneWithStart;
    [Space(10f)]
    [Header("UI")]
    [SerializeField] private CanvasGroup loadingScreen;
    [SerializeField] private Slider progressBar;

    private AsyncOperation _loadingSceneOperation;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (isLoadNextSceneWithStart)
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadScene(string sceneName)
    {
        ShowSreen();
        Instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(IELoadScene());
    }

    public void LoadScene(int indexBuild)
    {
        ShowSreen();
        Instance._loadingSceneOperation = SceneManager.LoadSceneAsync(indexBuild);
        StartCoroutine(IELoadScene());
    }

    private IEnumerator IELoadScene()
    {
        while (!_loadingSceneOperation.isDone)
        {
            progressBar.value = Mathf.Clamp01(_loadingSceneOperation.progress / 0.9f);
            yield return new WaitForSecondsRealtime(0.1f);
        }

        HideSreen();
    }

    private void ShowSreen()
    {
        loadingScreen.alpha = 1;
        loadingScreen.interactable = true;
        loadingScreen.blocksRaycasts = true;
    }

    private void HideSreen()
    {
        loadingScreen.alpha = 0;
        loadingScreen.interactable = false;
        loadingScreen.blocksRaycasts = false;
    }
}
