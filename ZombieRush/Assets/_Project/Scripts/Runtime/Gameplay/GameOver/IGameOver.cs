using System;

public interface IGameOver
{
    public bool isGameOver { get; set; }

    public event Action gameOver;
}
