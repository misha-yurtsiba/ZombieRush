using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LoadingUI : MonoBehaviour
{
    public Action loadingPanelActive;
    public Action loadingPanelDisactive;

    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private GameObject progressaBarObject;
    [SerializeField] private Image loadingProgressBar;

    private Image panelImage;
    
    public float LoadingProgress
    {
        set
        {
            loadingText.text = $"Loading... {Mathf.RoundToInt(value * 100)}%";
            loadingProgressBar.fillAmount = value;
        }
    }
    void Start()
    {
        panelImage = GetComponent<Image>();
        loadingText.text = " ";

        progressaBarObject.SetActive(false);
    }

    public void StartActivationAnim()
    {
        loadingText.gameObject.SetActive(true);
        progressaBarObject.SetActive(true);

        panelImage.DOFade(1, 1)
            .OnComplete(() => loadingPanelActive?.Invoke()); 
    }
    public void StartDeactivationAnim()
    {
        loadingText.gameObject.SetActive(false);
        progressaBarObject.SetActive(false);

        panelImage.DOFade(0, 1)
            .SetEase(Ease.OutSine)
            .OnComplete(() => gameObject.SetActive(false)); 
    }
}
