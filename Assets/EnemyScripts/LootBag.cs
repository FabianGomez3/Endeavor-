using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<LootDrops> lootList = new List<LootDrops>(); 

    LootDrops GetDroppedItem()
    {
        int ranNum = Random.Range(1,101);
        List<LootDrops> possibleItems = new List<LootDrops>();
        foreach (LootDrops item in lootList)
        {
            if(ranNum <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if(possibleItems.Count > 0)
        {
            LootDrops droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;
    }
    public void InstantiateLoot(Vector3 spawnPos)
    {
        LootDrops droppedItem = GetDroppedItem();
        if(droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPos, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            float dropFroce = 50f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f,1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropFroce, ForceMode2D.Impulse);
        }
    }
}
