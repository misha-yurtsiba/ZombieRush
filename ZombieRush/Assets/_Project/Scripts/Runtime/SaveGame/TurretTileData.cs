[System.Serializable]
public class TurretTileData
{
    public int turretTileId;
    public int turretLevel;

    public TurretTileData(int turretTileId, int turretLevel)
    {
        this.turretTileId = turretTileId;
        this.turretLevel = turretLevel;
    }
}
