using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    #region Variables

    [Header("References")]
    public GameObject mapParent;
    public GameObject tileSetParent;
    public Transform playerTransform;

    [Header("Tile Objects")]
    public GameObject[] snowTile;
    public GameObject[] treeTile;
    public GameObject[] rockTile;

    public Dictionary<TileSet, Vector3> tileSets = new Dictionary<TileSet, Vector3>();

    //Map Settings
    [Header("Map Generation Settings")]
    [Range(32f, 256f)]
    public int tileSetSize;
    [Range(1f, 4f)]
    public int renderDist;

    //Perlin Noise
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

    #endregion

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
        //check if new tileSet needs to be placed

        for (int x = (-renderDist * tileSetSize) + tileSetSize; x < renderDist * tileSetSize; x += tileSetSize)
        {
            for (int z = (-renderDist * tileSetSize) + tileSetSize; z < renderDist * tileSetSize; z += tileSetSize)
            {
                Vector3 newTileSetPos = RoundVector(new Vector3(x, 0, z) + playerTransform.position, tileSetSize);
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
        //fill tileSet with individual tiles

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
                    tile = new Tile((int)newTilePos.x, (int)newTilePos.z, 0);
                } else
                {
                    tile = new Tile((int)newTilePos.x, (int)newTilePos.z, (Type)1);
                }

                if (tile != null)
                    FillTile(tileSet, tile, newTilePos);
            }
        }
    }

    private void FillTile(TileSet tileSet, Tile tile, Vector3 tilePos)
    {
        //check what type the tile is

        if (tile.type == Type.Snow)
        {
            InstantiateTile(snowTile, tileSet, tile, tilePos);
        }
        else if (tile.type == Type.Tree)
        {
            InstantiateTile(treeTile, tileSet, tile, tilePos);
        }
        else if (tile.type == Type.Rock)
        {
            InstantiateTile(rockTile, tileSet, tile, tilePos);
        }
    }

    private void InstantiateTile(GameObject[] tileArray, TileSet tileSet, Tile tile, Vector3 tilePos)
    {
        //instantiate the tile

        GameObject tileObject = Instantiate(RandomGameObject(tileArray), tilePos, tileSet.transform.rotation);
        tileObject.transform.parent = tileSet.transform;
        tileObject.name = (tile.type + " (" + tileObject.transform.position.x + ", " + tileObject.transform.position.z + ")");
    }

    public static GameObject RandomGameObject(GameObject[] array)
    {
        //choose random gameObject from list

        GameObject randomObject = array[Random.Range(0, array.Length)];
        return randomObject;
    }

    public static Vector3 RoundVector(Vector3 vector, float roundSize)
    {
        //round a position to desired roundSize

        return new Vector3(
            Mathf.Round(vector.x / roundSize) * roundSize,
            Mathf.Round(vector.y / roundSize) * roundSize,
            Mathf.Round(vector.z / roundSize) * roundSize);
    }
}
