using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class SimpleRanWalkMapGen : AbstactDungeonGen
{
    
     [SerializeField]
    protected SimpleRandomWalkData randomWalkData;
    

    protected override void RunProceduralGen()
    {
        HashSet<Vector2Int> floorPos = RunRandomWalk(randomWalkData, startPos);
        tileMapVisual.Clear();
        tileMapVisual.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tileMapVisual);
    
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData parameters, Vector2Int position)
    {
        var curPos = position; 
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProGenAlgorithm.RandomWalk(curPos, parameters.walkLength);
            floorPos.UnionWith(path);
            if(parameters.startRandomlyEachIteration)
                curPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
        }
        return floorPos;
    }

    
}

