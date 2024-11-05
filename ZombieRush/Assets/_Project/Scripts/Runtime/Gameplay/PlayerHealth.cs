using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject wallFragments;

    [SerializeField] private float maxHealth;
    [SerializeField] private float explosionForse;
    [SerializeField] private float explosionRadius;
    
    private PlayerHealthView healthView;
    private GameOver gameOver;

    private bool isLose;
    public float curentHealth { get; private set; }

    [Inject]
    private void Construct(PlayerHealthView healthView, GameOver gameOver)
    {
        this.healthView = healthView;
        this.gameOver = gameOver;
    }

    public void Init()
    {
        healthView.Init(maxHealth);
        curentHealth = maxHealth;
        wall.SetActive(true);
        transform.GetComponent<BoxCollider>().enabled = true;
        
        isLose = false;
    }

    public void LoadHealth(float loadHealth)
    {
        curentHealth = loadHealth;
    }
    public void TakeDamage(float damage, Vector3 damagePosition)
    {
        if (isLose) return;

        curentHealth -= damage;

        if(curentHealth > 0)
        {
            healthView.UpdateHealthBar(curentHealth);
        }
        else
        {
            healthView.UpdateHealthBar(0);
            transform.GetComponent<BoxCollider>().enabled = false;
            WallDestroy(damagePosition);
            gameOver.Lose();
            isLose = true;
        }
    }

    private void WallDestroy(Vector3 damagePosition)
    {
        wall.SetActive(false);
        Transform brokenWall = Instantiate(wallFragments, wall.transform.position, Quaternion.identity).transform;

        Rigidbody[] brokenWallFragments = brokenWall.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody wallFragment in brokenWallFragments)
        {
            wallFragment.AddExplosionForce(explosionForse, damagePosition + new Vector3(0, 0, 5), explosionRadius);
            wallFragment.gameObject.GetComponent<MeshRenderer>().material
                .DOFade(0,2)
                .SetDelay(2)
                .OnComplete(() => Destroy(wallFragment.gameObject));
        }


    }
}
