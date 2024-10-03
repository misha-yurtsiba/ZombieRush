using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private TurretShopView turretShopView;
    [SerializeField] private MoneyView moneyView;

    private IStartGame startGame;
    
    [Inject]
    private void Construct(IStartGame startGame)
    {
        this.startGame = startGame;
    }
    private void Start()
    {
        mainMenu.newGameButton.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame()
    {
        mainMenu.gameObject.SetActive(false);
        startGame.StartGameplay();
        turretShopView.gameObject.SetActive(true);
        moneyView.gameObject.SetActive(true);
    }
}
