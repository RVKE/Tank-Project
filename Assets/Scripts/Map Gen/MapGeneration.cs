using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    public int tileSetSize;

    public GameObject viewPoint;
    public GameObject mapParent;

    Vector3 playerPos;
    Vector3 prevPlayerPos;

    [Range(1f, 4f)]
    public int renderDist;

    public Dictionary<TileSet, Vector3> tileSets = new Dictionary<TileSet, Vector3>();

	void Update () {
        LoadTileSets();
	}
    
    void LoadTileSets()
    {
        playerPos = viewPoint.transform.position;
        
        //check if new tileSet needs to be placed

        if (prevPlayerPos != playerPos)
        {
            for (int x = -renderDist * tileSetSize; x < renderDist * tileSetSize; x += tileSetSize)
            {
                for (int z = -renderDist * tileSetSize; z < renderDist * tileSetSize; z += tileSetSize)
                {
                    Vector3 newTileSetPos = RoundVector(new Vector3(x, 0, z), tileSetSize);
                    if (!tileSets.ContainsValue(newTileSetPos))
                    {
                        CreateTileSet(x, z);
                    }
                }
            }
            prevPlayerPos = playerPos;
        }
    }

    public void CreateTileSet(int x, int z)
    {
        //place new tileSet
    }


    public static Vector3 RoundVector(Vector3 vector, float roundSize)
    {
        return new Vector3(
            Mathf.Round(vector.x / roundSize) * roundSize,
            Mathf.Round(vector.y / roundSize) * roundSize,
            Mathf.Round(vector.z / roundSize) * roundSize);
    }
}
