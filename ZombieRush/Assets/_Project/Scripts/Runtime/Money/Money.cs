using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Money
{
    public Action<int> onValueChanget;

    private int playerMoney;
    public int PlayerMoney
    {
        get { return playerMoney; }
        private set
        {
            playerMoney = value;
            onValueChanget?.Invoke(value);
        }
    }

    public void Buy(int price)
    {
        if(PlayerMoney - price >= 0)
        {
            PlayerMoney -= price;
        }
    }

    public void AddMoney(int money)
    {
        PlayerMoney += money;
    }

    public void SetStartMoney(int startMoneyCount)
    {
        PlayerMoney = startMoneyCount;
    }
}
