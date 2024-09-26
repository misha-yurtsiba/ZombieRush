using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private ISceneLoad sceneLoad;

    [Inject]
    public void Construct(ISceneLoad sceneLoad)
    {
        this.sceneLoad = sceneLoad;
    }
    void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        sceneLoad.LoadScene(SceneName.Gameplay);
    }
}