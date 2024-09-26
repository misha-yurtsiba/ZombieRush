using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField] private GameUI gameUI;
    public override void InstallBindings()
    {
        BindGameUI();
    }

    private void BindGameUI()
    {
        Container
            .BindInstance(gameUI)
            .AsSingle()
            .NonLazy();
    }
}
