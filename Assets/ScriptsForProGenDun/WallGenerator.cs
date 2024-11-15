using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public static class WallGenerator 
{   
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TileMapVisual tileMapVisual)
    {
        var basicWallPos = FindWallsInDirections(floorPos, Direction2D.cardinalDirectionList);
        var cornerWallPos = FindWallsInDirections(floorPos, Direction2D.digaonalDirectionList);
        CreateBasicWall(tileMapVisual, basicWallPos, floorPos);
        CreateCornerWalls(tileMapVisual, cornerWallPos, floorPos);

    }

    private static void CreateCornerWalls(TileMapVisual tileMapVisual, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var position in cornerWallPos)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionList)
            {
                var nieghbourPos = position + direction;
                if(floorPos.Contains(nieghbourPos))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tileMapVisual.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall (TileMapVisual tileMapVisual, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach(var position in basicWallPos)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                var nieghbourPos = position + direction;
                if (floorPos.Contains(nieghbourPos))
                {
                    neighboursBinaryType += "1";
                }
                else 
                {
                    neighboursBinaryType += "0";
                }
            }
            tileMapVisual.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var position in floorPos)
        {
            foreach(var direction in directionList)
            {
                var neighbourPos = position + direction;
                if(floorPos.Contains(neighbourPos) == false)
                    wallPos.Add(neighbourPos);
            }
        }
        return wallPos;
    }
}
