using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTile : MonoBehaviour
{
    public Turret curentTurret;
    public int TuretTileId { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private Color endColor;

    private MeshRenderer renderer;
    private bool isBlinking;
    private void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
    }
    public void Init(int id)
    {
        TuretTileId = id;
    }

    public bool TryGetTurret(out Turret turret)
    {
        turret = curentTurret;

        return (curentTurret == null) ? false : true;
    }

    private void Update()
    {
        if (!isBlinking) return;

        renderer.material.color = Color.Lerp(Color.white, endColor, Mathf.PingPong(Time.time * speed, 1));
    }

    public void SetActiveBlinking(bool value)
    {
        if (value)
            isBlinking = true;
        else
        {
            isBlinking = false;
            renderer.material.color = Color.white;
        }
    }
}
