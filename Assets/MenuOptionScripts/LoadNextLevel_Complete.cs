using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel_Complete : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();

    }
    void Update()
    {
        if(player.coins >= 10)
        {
            LoadNextLevel();
        }
        if(player.keys >= 3)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
