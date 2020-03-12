using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RenderTiles : MonoBehaviour
{

    protected void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Contains("Tile"))
        {
            col.gameObject.GetComponent<MeshRenderer>().enabled = true;
            if (col.gameObject.transform.childCount > 0)
                foreach (Renderer renderer in col.gameObject.GetComponentsInChildren<MeshRenderer>())
                    renderer.enabled = true;
        }
    }

    protected void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Contains("Tile"))
        {
            col.gameObject.GetComponent<MeshRenderer>().enabled = false;
            if (col.gameObject.transform.childCount > 0)
                foreach (Renderer renderer in col.gameObject.GetComponentsInChildren<MeshRenderer>())
                    renderer.enabled = false;
        }
    }

}
