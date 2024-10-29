using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public class PauseGame : IPause
{
    public event Action<bool> pause;

    private bool isGamePaused = false;

    public void Pause()
    {
        isGamePaused = !isGamePaused;
        pause?.Invoke(isGamePaused);
    }
}
