using System.Collections.Generic;
using UnityEngine;

public class TileSet : MonoBehaviour {

    #region Variables

    [HideInInspector]
    public int tileSetX;
    [HideInInspector]
    public int tileSetZ;
    [HideInInspector]
    public int size;
    [HideInInspector]
    public List<Tile> tiles;

    #endregion

    public TileSet(int x, int z, int s)
    {
        tileSetX = x;
        tileSetZ = z;
        size = s;
    }
}
