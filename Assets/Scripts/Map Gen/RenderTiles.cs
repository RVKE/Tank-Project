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
            //Debug.DrawLine(col.gameObject.transform.position, transform.position, Color.red);
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
