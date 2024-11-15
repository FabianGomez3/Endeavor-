using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstactDungeonGen : MonoBehaviour
{
    [SerializeField]
    protected TileMapVisual tileMapVisual = null;
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tileMapVisual.Clear();
        RunProceduralGen();
    }
    protected abstract void RunProceduralGen();
    
}   
