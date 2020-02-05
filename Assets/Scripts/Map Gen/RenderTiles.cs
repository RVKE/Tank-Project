using UnityEngine;

public class RenderTiles : MonoBehaviour
{

    #region

    private Collider[] colliders;

    #endregion

    protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Tile")
        {
            col.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    protected void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Tile")
        {
            col.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
