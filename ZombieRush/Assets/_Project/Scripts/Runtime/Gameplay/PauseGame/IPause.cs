using System;

public interface IPause
{
    public event Action<bool> pause;
}
