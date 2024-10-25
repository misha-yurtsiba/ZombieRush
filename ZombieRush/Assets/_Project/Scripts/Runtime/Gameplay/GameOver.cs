using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public class GameOver : IGameOver
{
    public event Action gameOver;
    private int curentWave;

    private GameOverView gameOverView;

    public bool isGameOver { get; set; }
    public GameOver(GameOverView gameOverView)
    {
        this.gameOverView = gameOverView;
    }

    public void Init()
    {
        isGameOver = false;
    }

    public void Lose()
    {
        gameOver?.Invoke();
        isGameOver = true;
        gameOverView.ActiveLosePanel(curentWave);
    }

    public void SetCurentWave(int waveCount) => curentWave = waveCount;
}
