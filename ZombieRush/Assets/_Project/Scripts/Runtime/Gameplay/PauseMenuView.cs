using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

public class PauseMenuView : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public Button exitButton;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Sprite pauseActiveIcon;
    [SerializeField] private Sprite pauseDisactiveIcon;

    private PauseGame pauseGame;
    private RestartGame restartGame;


    [Inject]
    private void Construct(PauseGame pauseGame, RestartGame restartGame)
    {
        this.pauseGame = pauseGame;
        this.restartGame = restartGame;
    }

    private void Start()
    {
        pauseButton.onClick.AddListener(ActivePausePanel);
        continueButton.onClick.AddListener(DisactivePausePanel);
        restartButton.onClick.AddListener(Restart);
    }

    public void Init()
    {
        pauseButton.image.sprite = pauseDisactiveIcon;

        pauseButton.onClick.RemoveListener(DisactivePausePanel);
        pauseButton.onClick.AddListener(ActivePausePanel);
        pauseMenuPanel.SetActive(false);
    }
    public void SetActivePauseIcon(bool value) => pauseButton.gameObject.SetActive(value);
    public void ActivePausePanel()
    {
        Debug.Log("Active");
        pauseButton.onClick.RemoveListener(ActivePausePanel);
        pauseButton.onClick.AddListener(DisactivePausePanel);

        pauseButton.image.sprite = pauseActiveIcon;
        pauseGame.Pause(true);

        pauseMenuPanel.SetActive(true);
        pauseMenuPanel.transform.localScale = Vector3.zero;

        pauseMenuPanel.transform
            .DOScale(1, 0.3f);

    }

    public void DisactivePausePanel()
    {
        Debug.Log("Disactive");
        pauseButton.onClick.RemoveListener(DisactivePausePanel);
        pauseButton.onClick.AddListener(ActivePausePanel);

        pauseButton.image.sprite = pauseDisactiveIcon;
        pauseGame.Pause(false);

        pauseMenuPanel.transform
            .DOScale(0, 0.3f)
            .OnComplete(() => pauseMenuPanel.SetActive(false));
    }

    private void Restart()
    {
        DisactivePausePanel();
        restartGame.Restart();
    }
}
