using UnityEngine;

public enum Type
{
    Snow,
    Tree,
    Rock,
    House,
    Vehicle,
    Mast,
}

public class Tile {

    #region Variables

    [HideInInspector]
    public int tileX;
    [HideInInspector]
    public int tileZ;
    [HideInInspector]
    public Type type;

    #endregion

    public Tile (int x, int z, Type t) {
        tileX = x;
        tileZ = z;
        type = t;
	}
}
