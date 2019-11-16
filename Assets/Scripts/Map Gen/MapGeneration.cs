using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    [Range(32f, 256f)]
    public int tileSetSize;
    [Range(1f, 4f)]
    public int renderDist;

    public GameObject viewPoint;
    public GameObject mapParent;
    public GameObject tileSetParent;

    Vector3 playerPos;
    Vector3 prevPlayerPos;

    public Dictionary<TileSet, Vector3> tileSets = new Dictionary<TileSet, Vector3>();

	void Update () {
        LoadTileSets();
	}
    
    void LoadTileSets()
    {
        playerPos = viewPoint.transform.position;

        //check if new tileSet needs to be placed

        for (int x = (-renderDist * tileSetSize) + tileSetSize; x < renderDist * tileSetSize; x += tileSetSize)
        {
            for (int z = (-renderDist * tileSetSize) + tileSetSize; z < renderDist * tileSetSize; z += tileSetSize)
            {
                Vector3 newTileSetPos = RoundVector(new Vector3(x, 0, z) + playerPos, tileSetSize);
                if (!tileSets.ContainsValue(newTileSetPos))
                {
                    CreateTileSet((int)newTileSetPos.x, (int)newTileSetPos.z);
                }
            }
        }
    }

    public void CreateTileSet(int x, int z)
    {
        //place new tileSet

        GameObject tileSetGO = Instantiate(tileSetParent, new Vector3(x, 0, z), tileSetParent.transform.rotation);
        tileSetGO.transform.parent = mapParent.transform;
        tileSetGO.name = ("TileSet (" + tileSetGO.transform.position.x + ", " + tileSetGO.transform.position.z + ")");
        TileSet tileSet = tileSetGO.GetComponent<TileSet>();

        AddTiles(tileSet);

        tileSets.Add(tileSet, new Vector3(x, 0, z));
    }

    private void AddTiles(TileSet tileSet)
    {
        int min = -tileSetSize / 2;
        int max = tileSetSize / 2;

        //method one
        for (int x = min; x < max; x++)
        {
            for (int z = min; z < max; z++)
            {
                Tile tile = null;

                Vector3 newTilePos = new Vector3(x, 0, z) + tileSet.transform.position;

                /*GameObject tileGO = Instantiate(new GameObject("Tile (" + x + ", " + z + ")"), newTilePos, transform.rotation);
                tileGO.transform.parent = tileSet.transform;*/
            }
        }


        //method two

        /*for (int i = 0; i < Mathf.Pow(tileSetSize, 2f); i++)
        {
            Debug.Log(Mathf.Pow(tileSetSize, 2f));
        }*/

    }


    public static Vector3 RoundVector(Vector3 vector, float roundSize)
    {
        return new Vector3(
            Mathf.Round(vector.x / roundSize) * roundSize,
            Mathf.Round(vector.y / roundSize) * roundSize,
            Mathf.Round(vector.z / roundSize) * roundSize);
    }
}
