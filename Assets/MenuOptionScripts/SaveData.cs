using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
public static class SaveData
{
    //public static void SavePlayer (GameManager gameManager, List<Enemy> enemies)
    public static void SavePlayer (GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        //PlayerData data = new PlayerData(gameManager, enemies);
        PlayerData data = new PlayerData(gameManager);
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
