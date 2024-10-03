using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
public class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private Money money;

    [Inject]
    private void Construct(Money money)
    {
        this.money = money;
    }

    private void OnEnable()
    {
        money.onValueChanget += ChangeMoneyText;
        moneyText.text = money.PlayerMoney.ToString();
    }
    private void OnDisable()
    {
        money.onValueChanget -= ChangeMoneyText;
    }

    public void ChangeMoneyText(int newMoneyCount)
    {
        moneyText.text = newMoneyCount.ToString();
    }
}
