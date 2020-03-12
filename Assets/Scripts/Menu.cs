using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Camera cam;

    public void GoToPanel(Transform panelTransform)
    {
        cam.transform.rotation = panelTransform.rotation;
        cam.transform.position = panelTransform.position - panelTransform.transform.position/2;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }
}
