using UnityEngine;
using Zenject;
public class GameInstaller : MonoInstaller
{
    [SerializeField] private LoadingUI loadingUI;
    public override void InstallBindings()
    {
        BindLoadingUI();
        BindSceneLoader();
    }

    private void BindLoadingUI()
    {
        Container
            .BindInstance(loadingUI)
            .AsSingle()
            .NonLazy();
    }
    private void BindSceneLoader()
    {
        Container
            .BindInterfacesAndSelfTo<SceneLoader>()
            .AsSingle()
            .NonLazy();
    }
}
