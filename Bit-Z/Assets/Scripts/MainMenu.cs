using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string startLevel;

    public void NewGame()
    {
        SceneManager.LoadScene(startLevel);
    }

    public void QuitGame()
    {
        DestroyAll();
        Application.Quit();
    }

    public void DestroyAll()
    {
        var objects = GameObject.FindObjectsOfType<GameObject>();

        foreach(GameObject o in objects)
        {
            Destroy(o.gameObject);
        }
    }
}
