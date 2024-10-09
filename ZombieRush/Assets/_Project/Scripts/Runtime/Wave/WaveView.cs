using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WaveView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] float showTime;

    private float timer;
    private bool isActive;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (!isActive) return;

        if(timer < showTime)
            timer += Time.deltaTime;
        else
        {
            isActive = false;
            waveText.DOFade(0, 1)
                .OnComplete(() => waveText.gameObject.SetActive(false));
        }

    }
    public void ChangeWaveText(int newWave)
    {
        waveText.gameObject.SetActive(true);
        waveText.text = $"Wave {newWave + 1}";

        waveText.DOFade(1, 1)
            .OnComplete(StartTextTimer);
    }

    private void StartTextTimer()
    {
        isActive = true;
        timer = 0;
    }
}
