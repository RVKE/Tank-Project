using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationV3 : MonoBehaviour {

    public GameObject viewBox;
    public GameObject viewPoint;
    public GameObject mapParent;
    public GameObject viewBlock;

    [Range(50f, 500f)]
    public int mapLength;
    [Range(50f, 500f)]
    public int mapWidth;
    public int viewWidth;
    private int boundX;
    private int boundZ;
    private int viewBound;

    public bool showViewBoundary;

    [HideInInspector]
    public List<Vector3> tilePosList = new List<Vector3>();

    public List<GameObject> snowTiles;

    void Awake()
    {
        //generate viewpoint tiles

        viewBound = (viewWidth - 1) / 2;

        for (int x = -viewBound; x <= viewBound; x++)
        {
            for (int z = -viewBound; z <= viewBound; z++)
            {
                GameObject block = Instantiate(viewBlock, new Vector3(x, 0, z), viewBlock.transform.rotation);
                block.transform.parent = viewBox.transform;
                block.GetComponent<MeshRenderer>().enabled = showViewBoundary;
            }
        }

        //add maptile positions

        boundX = (mapLength - 1) / 2;
        boundZ = (mapWidth - 1) / 2;

        for (int x = -boundX; x <= boundX; x++)
        {
            for (int z = -boundZ; z <= boundZ; z++)
            {
                tilePosList.Add(new Vector3(x, 0, z));
            }
        }
    }

    void Start()
    {
        GenerateMap();
    }

	void Update () {
        viewBox.transform.position = RoundVector3(viewPoint.transform.position);
    }

    void GenerateMap()
    {
        foreach (Vector3 v in tilePosList)
        {
            PlaceNewTile(v);
        }
    }

    void PlaceNewTile(Vector3 pos)
    {
        GameObject randomSnowTile = snowTiles[UnityEngine.Random.Range(0, snowTiles.Count)];
        GameObject newSnowTile = Instantiate(randomSnowTile, pos, randomSnowTile.transform.rotation);
        newSnowTile.transform.parent = mapParent.transform;
    }


    public static Vector3 RoundVector3(Vector3 vector)
    {
        return new Vector3(
            Mathf.Round(vector.x),
            Mathf.Round(vector.y),
            Mathf.Round(vector.z));
    }
}
