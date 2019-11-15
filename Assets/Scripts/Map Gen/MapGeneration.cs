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

    public List<TileSet> tileSets;

	void Update () {
        LoadTileSets();
	}
    
    void LoadTileSets()
    {
        playerPos = viewPoint.transform.position;
        
        if (prevPlayerPos != playerPos)
        {
            for (int x = -renderDist * tileSetSize; x < renderDist * tileSetSize; x += tileSetSize)
            {
                for (int z = -renderDist * tileSetSize; z < renderDist * tileSetSize; z += tileSetSize)
                {
                    Vector3 newPos = new Vector3(x, 0, z);
                    Debug.Log(newPos);
                }
            }
            prevPlayerPos = playerPos;
        }
    }


    public static Vector3 RoundVector(Vector3 vector, float roundSize)
    {
        return new Vector3(
            Mathf.Round(vector.x / roundSize) * roundSize,
            Mathf.Round(vector.y / roundSize) * roundSize,
            Mathf.Round(vector.z / roundSize) * roundSize);
    }
}
