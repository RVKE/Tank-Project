using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGeneration : MonoBehaviour {

    public GameObject viewPoint;
    public GameObject mapParent;
    public GameObject tileSetParent;

    private Vector3 playerPos;

    [Header("Tile Objects")]
    public GameObject[] snowTile;
    public GameObject[] treeTile;
    public GameObject[] rockTile;

    public Dictionary<TileSet, Vector3> tileSets = new Dictionary<TileSet, Vector3>();

    //Perlin Noise
    [Header("Map Generation Settings")]
    [Range(32f, 256f)]
    public int tileSetSize;
    [Range(1f, 4f)]
    public int renderDist;
    public bool perlinAlwaysRandomPos; //Default: True
    [Range(0.1f, 1.0f)]
    public float perlinLimitMin; //Default: 0.5
    [Range(0.1f, 1.0f)]
    public float perlinLimitMax; //Default: 0.6
    [Range(1.0f, 10.0f)]
    public float perlinDensityMultiplier; //Default: 5
    [Range(0.0f, 1.0f)]
    public float perlinNoiseExtra; //Default: 0.15
    public float perlinOffsetX; //Default: 20000
    public float perlinOffsetZ; //Default: 40000

    void Start()
    {
        if (perlinAlwaysRandomPos)
        {
            perlinOffsetX = Random.Range(perlinOffsetX - perlinOffsetX / 4, perlinOffsetX + perlinOffsetX / 4);
            perlinOffsetZ = Random.Range(perlinOffsetZ - perlinOffsetZ / 4, perlinOffsetZ + perlinOffsetZ / 4);
        }
    }

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

        tileSet.size = tileSetSize;
        tileSet.tileSetX = x;
        tileSet.tileSetZ = z;
        tileSet.tiles = new List<Tile>();

        tileSets.Add(tileSet, new Vector3(x, 0, z));
    }

    private void AddTiles(TileSet tileSet)
    {
        int min = -tileSetSize / 2;
        int max = tileSetSize / 2;

        float perlinDensity = tileSetSize / perlinDensityMultiplier;

        for (int x = min; x < max; x++)
        {
            for (int z = min; z < max; z++)
            {
                Tile tile = null;

                Vector3 newTilePos = new Vector3(x, 0, z) + tileSet.transform.position;

                float perlinSample = Mathf.PerlinNoise(
                    (newTilePos.x/perlinDensity) + perlinOffsetX + Random.Range(-perlinNoiseExtra, perlinNoiseExtra), 
                    (newTilePos.z/perlinDensity) + perlinOffsetZ + Random.Range(-perlinNoiseExtra, perlinNoiseExtra));

                if (perlinSample < Random.Range(perlinLimitMin, perlinLimitMax))
                {
                    //spawn snow
                    tile = new Tile((int)newTilePos.x, (int)newTilePos.z, (Type)0);
                } else
                {
                    //spawn tree
                    tile = new Tile((int)newTilePos.x, (int)newTilePos.z, (Type)1);
                }

                if (tile != null)
                    FillTile(tileSet, tile, newTilePos);
            }
        }
    }

    private void FillTile(TileSet tileSet, Tile tile, Vector3 tilePos)
    {
        if (tile.type == Type.Snow)
        {
            GameObject tileObject = Instantiate(RandomGameObject(snowTile), tilePos, tileSet.transform.rotation);
            tileObject.transform.parent = tileSet.transform;
        }
        else if (tile.type == Type.Tree)
        {
            GameObject tileObject = Instantiate(RandomGameObject(treeTile), tilePos, tileSet.transform.rotation);
            tileObject.transform.parent = tileSet.transform;
        }
        else if (tile.type == Type.Rock)
        {
            GameObject tileObject = Instantiate(RandomGameObject(rockTile), tilePos, tileSet.transform.rotation);
            tileObject.transform.parent = tileSet.transform;
        }
    }

    public static GameObject RandomGameObject(GameObject[] array)
    {
        GameObject randomObject = array[Random.Range(0, array.Length)];
        return randomObject;
    }

    public static Vector3 RoundVector(Vector3 vector, float roundSize)
    {
        return new Vector3(
            Mathf.Round(vector.x / roundSize) * roundSize,
            Mathf.Round(vector.y / roundSize) * roundSize,
            Mathf.Round(vector.z / roundSize) * roundSize);
    }
}
