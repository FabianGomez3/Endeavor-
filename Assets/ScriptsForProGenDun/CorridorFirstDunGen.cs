using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CorridorFirstDunGen : SimpleRanWalkMapGen
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;

    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;
    

    protected override void RunProceduralGen()
    {
        CorridorFirstGen();

    }

    private void CorridorFirstGen()
    {   
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPos, potentialRoomPos);

        HashSet<Vector2Int> roomPos = CreateRooms(potentialRoomPos);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPos);

        CreateRoomsAtDeadEnd(deadEnds, roomPos);

        floorPos.UnionWith(roomPos);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
            floorPos.UnionWith(corridors[i]);
            
            
        }
         
        tileMapVisual.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tileMapVisual);

    }

    public List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;
        for (int i = 1; i < corridor.Count; i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
            if(previousDirection != Vector2Int.zero && directionFromCell != previousDirection)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(x,y));
                    }
                }
                previousDirection = directionFromCell;
            }
            else
            {
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);

            }
        }
        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if(direction == Vector2Int.up)
            return Vector2Int.right;
        if(direction == Vector2Int.right)
            return Vector2Int.down;
        if(direction == Vector2Int.down)
            return Vector2Int.left;
        if(direction == Vector2Int.left)
            return Vector2Int.up;
        return Vector2Int.zero;
        
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkData, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPos)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPos)
        {
            int nieghboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if(floorPos.Contains(position + direction))
                    nieghboursCount++;
            }
            if (nieghboursCount == 1)
            deadEnds.Add(position);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPos)
    {
        HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent);

        List<Vector2Int> roomToCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkData, roomPosition);
            roomPos.UnionWith(roomFloor);
        }
        return roomPos;
    }

    private List<List<Vector2Int>>CreateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos)
    {
        var curPos = startPos;
        potentialRoomPos.Add(curPos);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProGenAlgorithm.RandomWalkCorridor(curPos, corridorLength);
            corridors.Add(corridor);
            curPos = corridor[corridor.Count - 1];
            potentialRoomPos.Add(curPos);
            floorPos.UnionWith(corridor);
        }
        return corridors;
    }
}
