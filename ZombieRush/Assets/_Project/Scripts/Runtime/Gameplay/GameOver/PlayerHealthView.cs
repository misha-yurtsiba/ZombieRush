using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private Image healthBarBackground;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI healthText;

    private Sequence tween;
    private CanvasGroup healthBar;
    private float maxHealth; 

    private void Start()
    {
        
    }

    public void Init(float maxHealth)
    {
        this.maxHealth = maxHealth;

        healthBar = healthBarBackground.GetComponent<CanvasGroup>();

        CreateSequence();
        tween.Play();
    }
    private void CreateSequence()
    {
        tween = DOTween.Sequence();

        tween.Append(healthBar.DOFade(1, 0.25f))
            .AppendInterval(3)
            .Append(healthBar.DOFade(0, 2))
            .OnComplete(WaitForEnd);
    }
    public void UpdateHealthBar(float newHealth)
    {
        healthBarBackground.gameObject.SetActive(true);
        healthText.text = $"{newHealth}/{maxHealth}";
        healthBarImage.fillAmount = newHealth / maxHealth;

        if (tween.IsActive() && tween.IsComplete())
        {
            tween.Restart(true);
        }
        else
        {
            tween.Kill();
            CreateSequence();
            tween.Play();
        }
    }

    private void WaitForEnd()
    {
        healthBarBackground.gameObject.SetActive(false);
    }
}
