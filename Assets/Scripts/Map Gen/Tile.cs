using System.Collections;
using System.Collections.Generic;
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

    public int tileX;
    public int tileY;
    public Type type;

	public Tile (int x, int y, Type t) {
        tileX = x;
        tileY = y;
        type = t;
	}
}
