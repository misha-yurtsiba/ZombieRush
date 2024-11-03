using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int waveCount;
    public int playerMoney;
    public float playerHealth;
    public List<TurretTileData> turretTileDatas;
}
