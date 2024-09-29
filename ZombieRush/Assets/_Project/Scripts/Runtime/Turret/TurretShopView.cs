using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TurretShopView : MonoBehaviour
{
    [SerializeField] private Button buyTurretButton;

    private TurretSpawner turretSpawner;
    [Inject]
    private void Construct(TurretSpawner turretSpawner)
    {
        this.turretSpawner = turretSpawner;
    }
    private void Start()
    {
        buyTurretButton.onClick.AddListener(turretSpawner.BuyTurret);
    }
}