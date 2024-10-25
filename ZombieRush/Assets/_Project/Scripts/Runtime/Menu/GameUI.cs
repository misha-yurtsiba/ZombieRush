using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private TurretShopView turretShopView;
    [SerializeField] private MoneyView moneyView;
    [SerializeField] private GameOverView gameOverView;

    private IStartGame startGame;
    private IRestart restartGame;
    private IGameOver gameOver;
    
    [Inject]
    private void Construct(IStartGame startGame, IRestart restartGame, IGameOver gameOver)
    {
        this.startGame = startGame;
        this.restartGame = restartGame;
        this.gameOver = gameOver;
    }
    private void Start()
    {
        mainMenu.newGameButton.onClick.AddListener(StartNewGame);
        gameOverView.exitButton.onClick.AddListener(BackToMenu);
    }

    private void OnEnable()
    {
        gameOver.gameOver += LoseGame;
        restartGame.restart += StartNewGame;
    }

    private void OnDisable()
    {
        gameOver.gameOver -= LoseGame;
        restartGame.restart -= StartNewGame;
    }

    private void StartNewGame()
    {
        mainMenu.gameObject.SetActive(false);
        turretShopView.gameObject.SetActive(true);
        moneyView.gameObject.SetActive(true);
        gameOverView.DisactiveLosePanel();

        startGame.StartGameplay();
    }

    private void LoseGame()
    {
        turretShopView.gameObject.SetActive(false);

    }

    private void BackToMenu()
    {
        mainMenu.gameObject.SetActive(true);
        turretShopView.gameObject.SetActive(false);
        moneyView.gameObject.SetActive(false);
        gameOverView.DisactiveLosePanel();
    }
}
