using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class TurretShopView : MonoBehaviour
{
    [SerializeField] private Button buyTurretButton;
    [SerializeField] private TextMeshProUGUI turretPriceText;

    private TurretSpawner turretSpawner;
    private Money money;
    [Inject]
    private void Construct(TurretSpawner turretSpawner, Money money)
    {
        this.turretSpawner = turretSpawner;
        this.money = money;
    }
    private void Start()
    {
        buyTurretButton.onClick.AddListener(turretSpawner.BuyTurret);

        turretPriceText.text = turretSpawner.turretPrice.ToString();
        ChangeTextColor(money.PlayerMoney);
    }
    private void OnEnable() 
    {
        money.onValueChanget += ChangeTextColor;
        turretSpawner.turretSpawned += ChangeTurretPriceText;
    }

    private void OnDisable()
    {
        money.onValueChanget -= ChangeTextColor;
        turretSpawner.turretSpawned -= ChangeTurretPriceText;
    }

    private void ChangeTextColor(int mewMoney)
    {
        if(mewMoney < turretSpawner.turretPrice)
            turretPriceText.color = Color.red;
        else
            turretPriceText.color = Color.green;
    }

    private void ChangeTurretPriceText(int newTurretPrice)
    {
        turretPriceText.text = newTurretPrice.ToString();
    }
}