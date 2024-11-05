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
    [SerializeField] private PauseMenuView pauseMenuView;

    private StartGame startGame;
    private SaveHandler saveHandler;
    private IRestart restartGame;
    private IGameOver gameOver;
    
    [Inject]
    private void Construct(StartGame startGame, SaveHandler saveHandler, IRestart restartGame, IGameOver gameOver)
    {
        this.startGame = startGame;
        this.saveHandler = saveHandler;
        this.restartGame = restartGame;
        this.gameOver = gameOver;
    }
    private void Start()
    {
        mainMenu.newGameButton.onClick.AddListener(StartNewGame);
        mainMenu.continueGameButton.onClick.AddListener(ContinueGame);
        gameOverView.exitButton.onClick.AddListener(BackToMenu);
        pauseMenuView.exitButton.onClick.AddListener(BackToMenu);

        if (saveHandler.IsSaveExist())
            mainMenu.continueGameButton.gameObject.SetActive(true);
        else
            mainMenu.continueGameButton.gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        gameOver.gameOver += LoseGame;
        restartGame.restart += RestartGame;
    }

    private void OnDisable()
    {
        gameOver.gameOver -= LoseGame;
        restartGame.restart -= RestartGame;
    }

    private void StartNewGame()
    {
        ActiveGameUi();
        if (saveHandler.IsSaveExist()) saveHandler.DeleteSave();

        startGame.StartGameplay();
    }

    private void ContinueGame()
    {
        ActiveGameUi();
        startGame.ContinueGame();
    }
    private void RestartGame()
    {
        startGame.ExitGame();
        StartNewGame();
    }
    private void LoseGame()
    {
        turretShopView.gameObject.SetActive(false);
        pauseMenuView.SetActivePauseIcon(false);
    }

    private void ActiveGameUi()
    {
        mainMenu.gameObject.SetActive(false);
        turretShopView.gameObject.SetActive(true);
        moneyView.gameObject.SetActive(true);
        gameOverView.DisactiveLosePanel();
        pauseMenuView.SetActivePauseIcon(true);
        pauseMenuView.Init();
    }

    private void BackToMenu()
    {
        startGame.ExitGame();
        mainMenu.gameObject.SetActive(true);
        turretShopView.gameObject.SetActive(false);
        moneyView.gameObject.SetActive(false);
        gameOverView.DisactiveLosePanel();

        if (saveHandler.IsSaveExist())
            mainMenu.continueGameButton.gameObject.SetActive(true);
        else
            mainMenu.continueGameButton.gameObject.SetActive(false);
    }

    
}
