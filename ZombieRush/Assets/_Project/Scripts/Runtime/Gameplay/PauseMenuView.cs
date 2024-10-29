using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

public class PauseMenuView : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pauseMenuPanel;

    [SerializeField] private Sprite pauseActiveIcon;
    [SerializeField] private Sprite pauseDisactiveIcon;

    private PauseGame pauseGame;

    [Inject]
    private void Construct(PauseGame pauseGame)
    {
        this.pauseGame = pauseGame;
    }

    private void Start()
    {
        pauseButton.onClick.AddListener(ActivePausePanel);
        continueButton.onClick.AddListener(DisctivePausePanel);

        pauseMenuPanel.SetActive(false);

    }
    public void ActivePausePanel()
    {
        pauseButton.onClick.RemoveListener(ActivePausePanel);
        pauseButton.onClick.AddListener(DisctivePausePanel);

        pauseButton.image.sprite = pauseActiveIcon;
        pauseGame.Pause();

        pauseMenuPanel.SetActive(true);
        pauseMenuPanel.transform.localScale = Vector3.zero;

        pauseMenuPanel.transform
            .DOScale(1, 0.3f);

    }

    public void DisctivePausePanel()
    {
        pauseButton.onClick.RemoveListener(DisctivePausePanel);
        pauseButton.onClick.AddListener(ActivePausePanel);

        pauseButton.image.sprite = pauseDisactiveIcon;
        pauseGame.Pause();

        pauseMenuPanel.transform
            .DOScale(0, 0.3f)
            .OnComplete(() => pauseMenuPanel.SetActive(false));
    }
}
