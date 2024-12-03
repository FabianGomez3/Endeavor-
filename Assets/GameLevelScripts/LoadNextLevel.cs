using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public bool isNextScene = true;
    [SerializeField]
    public SavingHealth savingHealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            savingHealth.isNextScene = isNextScene;
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }

    void Update()
    {
        if (LevelCompleted()) 
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex + 1);
        }
    }

    bool LevelCompleted()
    {
        return false;
    }
}

