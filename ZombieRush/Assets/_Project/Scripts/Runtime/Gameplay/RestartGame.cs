using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public class RestartGame : IRestart
{
    public event Action restart;

    public void Restart()
    {
        restart?.Invoke();
    }
}

