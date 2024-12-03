using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //public float health;
    public float [] position;
    //public List<EnemyData> enemies;
    //public PlayerData (GameManager gameManager, List<Enemy> allEnemies)
    public PlayerData (GameManager gameManager)
    {
        

        //health = gameManager.health;

        position = new float [3];
        position[0]  = gameManager.transform.position.x;
        position[1]  = gameManager.transform.position.y;
        position[2]  = gameManager.transform.position.z;

        
        // foreach (Enemy enemy in allEnemies)
        // {   
        //     if (enemy != null)
        //     {
        //         enemies.Add(new EnemyData(enemy));

        //     }
        // }
    }
}
