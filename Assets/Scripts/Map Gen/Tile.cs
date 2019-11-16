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
    public int tileZ;
    public Type type;

	public Tile (int x, int z, Type t) {
        tileX = x;
        tileZ = z;
        type = t;
	}
}
