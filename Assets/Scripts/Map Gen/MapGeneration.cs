using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGeneration : MonoBehaviour {

    #region Variables

    public Text tileCountText;

    [Header("References")]
    public GameObject mapParent;
    public GameObject tileSetParent;
    public Transform playerTransform;

    [Header("Tile Objects")]
    public GameObject[] snowTile;
    public GameObject[] treeTile;
    public GameObject[] rockTile;

    public GameObject[] radioTowerTile;
    public GameObject[] woodStorageTile;

    [Header("Loot Prefabs")]
    public GameObject scrapMetalResource;
    public GameObject woodResource;
    public GameObject coalResource;
    public GameObject unrefinedOilResource;

    public GameObject debugTile;

    [Header("Tile Decorations")]
    public GameObject[] tileDecoration;

    public Dictionary<TileSet, Vector3> tileSets = new Dictionary<TileSet, Vector3>();

    //Map Settings
    [Header("Map Generation Settings")]
    [Range(1, 5)]
    public int tileSize;
    [Range(32, 256)]
    public int tileSetSize;
    [Range(1, 4)]
    public int renderDist;
    [Range(3, 13)]
    public int megaTileSize;
    [Range(13, 60)]
    public int megaTileDistanceMin;
    [Range(0.0f, 1.0f)]
    public float tileDecorationSpawnChance;
    [Range(0.0f, 1.0f)]
    public float resourceSpawnChance;

    //Perlin Noise Properties
    public bool perlinRandomizeAllValues; //DEFAULT: False
    public bool perlinAlwaysRandomPos; //DEFAULT: True
    [Range(0.0f, 1.0f)]
    public float perlinSnowTransition; //DEFAULT: 0.05f
    [Range(0.0f, 1.0f)]
    public float perlinTreeTransition; //DEFAULT: 0.6f
    [Range(0.0f, 0.5f)]
    public float perlinMaxTransitionOffset; //DEFAULT: 0.2f;
    [Range(1.0f, 10.0f)]
    public float perlinDensityMultiplier; //DEFAULT: 4.0f
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
        if (perlinRandomizeAllValues)
        {
            perlinSnowTransition = Random.Range(0.0f, 0.1f);
            perlinTreeTransition = Random.Range(0.2f, 0.8f);
            perlinMaxTransitionOffset = Random.Range(0.0f, 0.3f);
            perlinDensityMultiplier = Random.Range(0.5f, 5.0f);
            perlinNoiseExtra = Random.Range(0.0f, 0.5f);
        }
    }

	void Update () {
        if (tileSetParent)
            LoadTileSets();
        tileCountText.text = "TILESETS LOADED: " + mapParent.transform.childCount;

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

        tileSet.size = tileSetSize;
        tileSet.tileSetX = x;
        tileSet.tileSetZ = z;
        tileSet.tiles = new List<Tile>();

        tileSets.Add(tileSet, new Vector3(x, 0, z));

        AddTiles(tileSet);
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
                    (newTilePos.x / perlinDensity) + perlinOffsetX + Random.Range(-perlinNoiseExtra, perlinNoiseExtra),
                    (newTilePos.z / perlinDensity) + perlinOffsetZ + Random.Range(-perlinNoiseExtra, perlinNoiseExtra));

                float perlinSnowOffset = Random.Range(perlinSnowTransition - perlinMaxTransitionOffset / 2,
                    perlinSnowTransition + perlinMaxTransitionOffset / 2);
                float perlinTreeOffset = Random.Range(perlinTreeTransition - perlinMaxTransitionOffset / 2,
                    perlinTreeTransition + perlinMaxTransitionOffset / 2);

                if (perlinSample < perlinSnowOffset)
                {
                    tile = new Tile((int)newTilePos.x, (int)newTilePos.z, (Type)2);
                }
                else if (perlinSample > perlinSnowTransition && perlinSample < perlinTreeTransition)
                {
                    tile = new Tile((int)newTilePos.x, (int)newTilePos.z, 0);

                    SpawnTileResource(new Vector3((int)newTilePos.x, 0, (int)newTilePos.z));
                }
                else
                {
                    tile = new Tile((int)newTilePos.x, (int)newTilePos.z, (Type)1);
                }

                if (tile != null)
                    FillTile(tileSet, tile, newTilePos);
            }
        }

        AddMegaTiles(tileSet);
    }

    private void AddMegaTiles(TileSet tileSet)
    {
        //replace random snowtiles with mega tile

        foreach (Transform tileTransform in tileSet.transform)
        {
            Collider[] colliders = Physics.OverlapBox(tileTransform.position, 
                new Vector3(megaTileDistanceMin, megaTileDistanceMin, megaTileDistanceMin));
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Tile_Mega")
                {
                    return;
                }
            }
            if (tileTransform.tag == "Tile_Snow")
            {
                float megaTileBoxExtent = megaTileSize * tileSize / 2;
                Collider[] nearColliders = Physics.OverlapBox(tileTransform.position, 
                    new Vector3(megaTileBoxExtent, megaTileBoxExtent, megaTileBoxExtent));
                List<GameObject> snowTiles = new List<GameObject>();

                foreach (Collider nearCollider in nearColliders)
                {
                    if (nearCollider.gameObject.tag == "Tile_Snow")
                    {
                        snowTiles.Add(nearCollider.gameObject);
                    }
                }
                if (snowTiles.Count == megaTileSize * megaTileSize)
                {
                    //Debug.Log("colliders: " + nearColliders.Length + " snowtiles: " + snowTiles.Count);
                    Tile tile = null;

                    //Type randomTile = (Type)Random.Range(3, (int)Type.NumberOfTypes);   < ---- deze shit moet je hebben

                    tile = new Tile((int)tileTransform.position.x, (int)tileTransform.position.z, (Type)Random.Range(3, 5));

                    Instantiate(debugTile, tileTransform.position, tileTransform.rotation);

                    foreach (GameObject snowTile in snowTiles)
                    {
                        Destroy(snowTile);
                    }

                    if (tile != null)
                        FillTile(tileSet, tile, tileTransform.position);
                }
            }
        }
    }

    private void FillTile(TileSet tileSet, Tile tile, Vector3 tilePos)
    {
        //check what type the tile is

        if (tile.type == Type.Snow)
        {
            InstantiateTile(snowTile, tileSet, tile, tilePos, "Tile_Snow");
        }
        else if (tile.type == Type.Tree)
        {
            InstantiateTile(treeTile, tileSet, tile, tilePos, "Tile_Tree");
        }
        else if (tile.type == Type.Rock)
        {
            InstantiateTile(rockTile, tileSet, tile, tilePos, "Tile_Rock");
        }
        else if (tile.type == Type.RadioTower)
        {
            InstantiateTile(radioTowerTile, tileSet, tile, tilePos, "Tile_Mega");
        }
        else if (tile.type == Type.WoodStorage)
        {
            InstantiateTile(woodStorageTile, tileSet, tile, tilePos, "Tile_Mega");
        }
    }

    private void InstantiateTile(GameObject[] tileArray, TileSet tileSet, Tile tile, Vector3 tilePos, string tileTag)
    {
        //instantiate the tile

        GameObject tileObject = Instantiate(RandomGameObject(tileArray), tilePos, tileSet.transform.rotation);
        tileObject.transform.parent = tileSet.transform;
        tileObject.name = (tile.type + " (" + tileObject.transform.position.x + ", " + tileObject.transform.position.z + ")");
        tileObject.tag = tileTag;
        tileObject.GetComponent<MeshRenderer>().enabled = false;
        AddTileDecoration(tileObject.transform);
        if (tileObject.transform.childCount > 0)
            foreach (Renderer renderer in tileObject.GetComponentsInChildren<MeshRenderer>())
                renderer.enabled = false;
    }

    private void AddTileDecoration(Transform tileTransform)
    {
        //randomly spawn decoration on tile

        if (Random.value < tileDecorationSpawnChance)
        {
            float randomX = Random.Range(tileTransform.position.x - tileSize / 2, tileTransform.position.x + tileSize / 2);
            float randomZ = Random.Range(tileTransform.position.z - tileSize / 2, tileTransform.position.z + tileSize / 2);
            Vector3 decorationPos = new Vector3(randomX, tileTransform.position.y + 0.5f, randomZ);
            GameObject decoration = Instantiate(RandomGameObject(tileDecoration), decorationPos, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            decoration.transform.parent = tileTransform;
        }
    }

    private void SpawnTileResource(Vector3 tilePosition)
    {
        if (Random.value < resourceSpawnChance)
        {
            Instantiate(woodResource, tilePosition + new Vector3(0, 1.0f, 0), transform.rotation);
        }
    }

    public static GameObject RandomGameObject(GameObject[] array)
    {
        //choose random gameObject from array

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
