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
    [Range(1, 5)]
    public int tileSize;
    [Range(32, 256)]
    public int tileSetSize;
    [Range(1, 4)]
    public int renderDist;

    //Perlin Noise Properties
    public bool perlinAlwaysRandomPos; //DEFAULT: True
    [Range(0.1f, 1.0f)]
    public float perlinSnowTransition; //DEFAULT: 0.1f
    [Range(0.1f, 1.0f)]
    public float perlinTreeTransition; //DEFAULT: 0.5f
    [Range(0.0f, 0.3f)]
    public float perlinMaxTransitionOffset; //DEFAULT: 0.1f;
    [Range(1.0f, 10.0f)]
    public float perlinDensityMultiplier; //DEFAULT: 5
    [Range(0.0f, 1.0f)]
    public float perlinNoiseExtra; //DEFAULT: 0.15f
    public float perlinOffsetX; //DEFAULT: 20000
    public float perlinOffsetZ; //DEFAULT: 40000

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

        for (int x = (-renderDist * tileSetSize * tileSize) + tileSetSize * tileSize; x < renderDist * tileSetSize * tileSize; x += tileSetSize * tileSize)
        {
            for (int z = (-renderDist * tileSetSize * tileSize) + tileSetSize * tileSize; z < renderDist * tileSetSize * tileSize; z += tileSetSize * tileSize)
            {
                Vector3 newTileSetPos = RoundVector(new Vector3(x, 0, z) + playerTransform.position, tileSetSize * tileSize);
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

        int min = -tileSetSize * tileSize / 2;
        int max = tileSetSize * tileSize / 2;

        float perlinDensity = tileSetSize / perlinDensityMultiplier;

        for (int x = min; x < max; x += tileSize)
        {
            for (int z = min; z < max; z += tileSize)
            {
                Tile tile = null;

                Vector3 newTilePos = new Vector3(x, 0, z) + tileSet.transform.position;

                float perlinSample = Mathf.PerlinNoise(
                    (newTilePos.x/perlinDensity) + perlinOffsetX + Random.Range(-perlinNoiseExtra, perlinNoiseExtra), 
                    (newTilePos.z/perlinDensity) + perlinOffsetZ + Random.Range(-perlinNoiseExtra, perlinNoiseExtra));

                float perlinSnowOffset = Random.Range(perlinSnowTransition - perlinMaxTransitionOffset / 2, 
                    perlinSnowTransition + perlinMaxTransitionOffset / 2);
                float perlinTreeOffset = Random.Range(perlinTreeTransition - perlinMaxTransitionOffset / 2,
                    perlinTreeTransition + perlinMaxTransitionOffset / 2);

                if (perlinSample < perlinSnowOffset)
                {
                    tile = new Tile((int)newTilePos.x, (int)newTilePos.z, (Type)2);
                } else if(perlinSample > perlinSnowTransition && perlinSample < perlinTreeTransition) {
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
        tileObject.tag = "Tile";
        tileObject.GetComponent<MeshRenderer>().enabled = false;
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
