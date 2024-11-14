using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using DG.Tweening;

public class GameOverView : MonoBehaviour
{
    public Button exitButton;

    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Button restartButton;

    private RestartGame restartGame;

    [Inject]
    private void Construct(RestartGame restartGame)
    {
        this.restartGame = restartGame;
    }
    public void ActiveLosePanel(int curentWave)
    {
        losePanel.SetActive(true);
        losePanel.transform.localScale = Vector3.zero;

        restartButton.onClick.AddListener(restartGame.Restart);

        losePanel.transform
            .DOScale(1, 0.3f)
            .SetDelay(3);

        waveText.text = $"Wave {curentWave}";
    }


    public void DisactiveLosePanel()
    {
        losePanel.SetActive(false);
        restartButton.onClick.RemoveListener(restartGame.Restart);
    }
}
