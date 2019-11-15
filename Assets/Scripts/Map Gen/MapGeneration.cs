using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    public int tileSetSize;

    public GameObject viewPoint;
    public GameObject mapParent;

    //public GameObject tileSetObject;

    //public Dictionary<Vector3, TileSet> tileSetDict;

    public List<TileSet> tileSets;

	void Start () {
        
	}
	
	void Update () {
        LoadTileSets();
	}
    
    void LoadTileSets()
    {
        //check if camera is looking at new tile position
        int posX = (int)viewPoint.transform.position.x;
        int posZ = (int)viewPoint.transform.position.z;

        Vector3 pos = new Vector3(posX, 0, posZ);
        Vector3 newPos = RoundVector(pos, tileSetSize);

        if ()

        //Debug.Log("rounded to ten: " + newPos + " normal: " + pos);
    }

    public static Vector3 RoundVector(Vector3 vector, float roundSize)
    {
        return new Vector3(
            Mathf.Round(vector.x / roundSize) * roundSize,
            Mathf.Round(vector.y / roundSize) * roundSize,
            Mathf.Round(vector.z / roundSize) * roundSize);
    }
}
