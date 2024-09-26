using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoad,IDisposable
{
    private AsyncOperation loadingAsyncOperation;
    private LoadingUI loadingUI;

    private string nextScene;

    public SceneLoader(LoadingUI loadingUI)
    {
        this.loadingUI = loadingUI;
        loadingUI.loadingPanelActive += StartLoad;
    }

    public void Dispose()
    {
        loadingUI.loadingPanelActive -= StartLoad;
    }

    public void LoadScene(SceneName sceneName)
    {
        nextScene = sceneName.ToString();

        loadingUI.gameObject.SetActive(true);
        loadingUI.StartActivationAnim();
    }

    private async void StartLoad()
    {
        loadingAsyncOperation = SceneManager.LoadSceneAsync(nextScene);
        loadingAsyncOperation.allowSceneActivation = false;
        float curentProgres = 0;

        do
        {
            await Task.Delay(25);

            if(loadingAsyncOperation.progress > 0.9)
                curentProgres = Mathf.MoveTowards(curentProgres, loadingAsyncOperation.progress,0.01f);
            else
                curentProgres = Mathf.MoveTowards(curentProgres, 1, 0.01f);

            loadingUI.LoadingProgress = curentProgres;
        }
        while (curentProgres != 1);

        loadingAsyncOperation.allowSceneActivation = true;
        loadingUI.StartDeactivationAnim();
    }
}
