using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public class GameOver : IGameOver
{
    public event Action gameOver;
    private int curentWave;

    private readonly GameOverView gameOverView;
    private readonly RestartGame restartGame;

    public bool isGameOver { get; set; }
    public GameOver(GameOverView gameOverView, RestartGame restartGame)
    {
        this.gameOverView = gameOverView;
        this.restartGame = restartGame;

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
