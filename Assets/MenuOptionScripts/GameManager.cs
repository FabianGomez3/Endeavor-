using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public float health;
    public void SavePlayer()
    {   

        // Enemy[] enemies = FindObjectsOfType<Enemy>();
        // List<Enemy> allEnemies = new List<Enemy>(enemies);
        //SaveData.SavePlayer(this, allEnemies);
        SaveData.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveData.LoadPlayer();

        //health = data.health;
        
        Vector3 position;
        position.x =  data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
