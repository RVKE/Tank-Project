using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationV2 : MonoBehaviour {

    public GameObject spawnBox;
    public GameObject viewPoint;
    public GameObject mapParent;

    public GameObject midBlock;
    public GameObject upBlock;
    public GameObject leftBlock;
    public GameObject downBlock;
    public GameObject rightBlock;

    private Vector3 prevPos;

    private float mapUpdates;
    public int mapGenBoxWidth;
    public bool showBoundary;

    public List<Vector3> tilePosList = new List<Vector3>();

    public List<GameObject> snowTiles;

    [HideInInspector]
    public List<GameObject> midBlocks;
    [HideInInspector]
    public List<GameObject> upBlocks;
    [HideInInspector]
    public List<GameObject> leftBlocks;
    [HideInInspector]
    public List<GameObject> downBlocks;
    [HideInInspector]
    public List<GameObject> rightBlocks;


    void Awake () {

        int bound = (mapGenBoxWidth - 1) / 2;

        for (int x = -bound; x <= bound; x++)
        {
            for (int z = -bound; z <= bound; z++)
            {
                GameObject block = Instantiate(midBlock, new Vector3(x, 0, z), midBlock.transform.rotation);
                block.transform.parent = spawnBox.transform;
            }
        }

        //up
        for (int z = -bound; z <= bound; z++)
        {
            int x = bound + 1;
            GameObject block = Instantiate(upBlock, new Vector3(x, 0, z), upBlock.transform.rotation);
            block.transform.parent = spawnBox.transform;
        }
        //left
        for (int x = -bound; x <= bound; x++)
        {
            int z = bound + 1;
            GameObject block = Instantiate(leftBlock, new Vector3(x, 0, z), leftBlock.transform.rotation);
            block.transform.parent = spawnBox.transform;
        }
        //down
        for (int z = -bound; z <= bound; z++)
        {
            int x = -bound - 1;
            GameObject block = Instantiate(downBlock, new Vector3(x, 0, z), downBlock.transform.rotation);
            block.transform.parent = spawnBox.transform;
        }
        //right
        for (int x = -bound; x <= bound; x++)
        {
            int z = -bound - 1;
            GameObject block = Instantiate(rightBlock, new Vector3(x, 0, z), rightBlock.transform.rotation);
            block.transform.parent = spawnBox.transform;
        }


        foreach (Transform child in spawnBox.transform)
        {
            GameObject mapBlock = child.gameObject;
            mapBlock.GetComponent<MeshRenderer>().enabled = showBoundary;
            if (mapBlock.tag == "MapPoint")
            {
                midBlocks.Add(mapBlock);
            } else if (mapBlock.tag == "UMapPoint")
            {
                upBlocks.Add(mapBlock);
            } else if (mapBlock.tag == "LMapPoint")
            {
                leftBlocks.Add(mapBlock);
            } else if (mapBlock.tag == "DMapPoint")
            {
                downBlocks.Add(mapBlock);
            } else if (mapBlock.tag == "RMapPoint")
            {
                rightBlocks.Add(mapBlock);
            } 
        }

        if (upBlocks.Count != Mathf.Sqrt(midBlocks.Count))
        {
            Debug.LogWarning("Amount of up mapblocks (" + upBlocks.Count + ") must be " + Mathf.Sqrt(midBlocks.Count) + "!");
        } else if (leftBlocks.Count != Mathf.Sqrt(midBlocks.Count))
        {
            Debug.LogWarning("Amount of left mapblocks (" + leftBlocks.Count + ") must be " + Mathf.Sqrt(midBlocks.Count) + "!");
        } else if (downBlocks.Count != Mathf.Sqrt(midBlocks.Count))
        {
            Debug.LogWarning("Amount of down mapblocks (" + downBlocks.Count + ") must be " + Mathf.Sqrt(midBlocks.Count) + "!");
        } else if (rightBlocks.Count != Mathf.Sqrt(midBlocks.Count))
        {
            Debug.LogWarning("Amount of right mapblocks (" + rightBlocks.Count + ") must be " + Mathf.Sqrt(midBlocks.Count) + "!");
        }
    }

    void Start()
    {
        prevPos = new Vector3(0, 0, 0);
        GenerateMap();
    }

	void Update () {
        Debug.Log(tilePosList.Count);
        spawnBox.transform.position = RoundVector3(viewPoint.transform.position);

        if (spawnBox.transform.position != prevPos)
        {
            UpdateMap();
            prevPos = spawnBox.transform.position;
        }
    }

    void UpdateMap()
    {
        mapUpdates++;

        if (spawnBox.transform.position.x > prevPos.x)
        {
            //up
            foreach (GameObject g in upBlocks)
            {
                Vector3 newTilePos = g.transform.position;
                if (!tilePosList.Contains(newTilePos))
                {
                    tilePosList.Add(newTilePos);
                    PlaceNewTile(newTilePos);
                }
            }
        }
        else if (spawnBox.transform.position.z > prevPos.z)
        {
            //left
            foreach (GameObject g in leftBlocks)
            {
                Vector3 newTilePos = g.transform.position;
                if (!tilePosList.Contains(newTilePos))
                {
                    tilePosList.Add(newTilePos);
                    PlaceNewTile(newTilePos);
                }
            }
        }
        else if (spawnBox.transform.position.x < prevPos.x)
        {
            //down
            foreach (GameObject g in downBlocks)
            {
                Vector3 newTilePos = g.transform.position;
                if (!tilePosList.Contains(newTilePos))
                {
                    tilePosList.Add(newTilePos);
                    PlaceNewTile(newTilePos);
                }
            }
        }
        else if (spawnBox.transform.position.z < prevPos.z)
        {
            //right
            foreach (GameObject g in rightBlocks)
            {
                Vector3 newTilePos = g.transform.position;
                if (!tilePosList.Contains(newTilePos))
                {
                    tilePosList.Add(newTilePos);
                    PlaceNewTile(newTilePos);
                }
            }
        }
    }

    public static Vector3 RoundVector3(Vector3 vector)
    {
        return new Vector3(
            Mathf.Round(vector.x),
            Mathf.Round(vector.y),
            Mathf.Round(vector.z));
    }

    void GenerateMap()
    {


    }

    void PlaceNewTile(Vector3 pos)
    {
        GameObject randomSnowTile = snowTiles[UnityEngine.Random.Range(0, snowTiles.Count)];
        GameObject newSnowTile = Instantiate(randomSnowTile, pos, randomSnowTile.transform.rotation);
        newSnowTile.transform.parent = mapParent.transform;
    }
}
