using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSet : MonoBehaviour {

    [HideInInspector]
    public int tileSetX;
    [HideInInspector]
    public int tileSetY;
    [HideInInspector]
    public int size;

    public TileSet(int x, int y, int s)
    {
        tileSetX = x;
        tileSetY = y;
        size = s;
    }
}
