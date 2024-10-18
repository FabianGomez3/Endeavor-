using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    public GameObject winScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        winScreen.SetActive(true);
    }

    
    
}
