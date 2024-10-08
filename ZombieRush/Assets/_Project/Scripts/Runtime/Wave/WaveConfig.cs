using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable , CreateAssetMenu(menuName = "Wave Config")]
public class WaveConfig : ScriptableObject
{
    public SubWave[] subWaves;

    public int timeBetweenSubwave;
}
