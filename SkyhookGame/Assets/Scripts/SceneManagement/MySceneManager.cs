using MyUtilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton<MySceneManager>
{
    public const string LoadingSceneName = "LoadingScene";
    public const string EarthSceneName = "EarthSettlement";
    public const string MoonSceneName = "MoonSettlement";
    //[SerializeField] private string marsSceneName = default;

    private bool isInLoading;

    public int LoadingOperationProgress { get; private set; }


    protected override void Awake()
    {
        SetInstance(this);
    }

    public void LoadNewSettlement(Planet planet)
    {
        Action callback = new Action(() =>
        {
            switch (planet)
            {
                case Planet.Earth:
                    LoadScene(EarthSceneName);
                    break;

                case Planet.Moon:
                    LoadScene(MoonSceneName);
                    break;

                    //case Planet.Mars:
                    //    LoadScene(marsSceneName);
                    //    break;
            }
        });

        PlayerDataManager.Instance.SaveSettlementData(Settlement.Instance.Planet, callback);
    }

    public void LoadScene(string sceneName)
    {
        if (isInLoading)
            return;

        StartCoroutine(LoadSceneCo(sceneName));
    }

    private IEnumerator LoadSceneCo(string sceneToLoad)
    {
        isInLoading = true;

        yield return null;

        AsyncOperation loadingSceneAsync = SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);

        while (!loadingSceneAsync.isDone)
            yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            LoadingOperationProgress = (int)(asyncOperation.progress * 100f);

            if (asyncOperation.progress >= .9f)
            {
                asyncOperation.allowSceneActivation = true;
                isInLoading = false;
            }

            yield return null;
        }
    }
}
