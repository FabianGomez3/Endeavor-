using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string sceneName = "";
    //Loads whatever scene is in qoutes (in this case the MainMenu)
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    //Quits the application
    public void Quit()
    {
        Application.Quit();
    }

}