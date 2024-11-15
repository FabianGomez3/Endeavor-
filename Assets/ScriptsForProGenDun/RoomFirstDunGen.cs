using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
public class RoomFirstDunGen : SimpleRanWalkMapGen
{
    [SerializeField]
   private int minRoomWidth = 4, minRoomHieght = 4;
   [SerializeField]
   private int dungeonWidth = 20, dungeonHeight = 20;
   [SerializeField]
   [Range(0,10)]
   private int offset = 1;
   [SerializeField]
   private bool randomWalkRooms = false;

   [SerializeField]
   private int corridorWidth;
    protected override void RunProceduralGen()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProGenAlgorithm.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHieght);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRandomRooms(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }

        floor = CreateSimpleRooms(roomsList);

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tileMapVisual.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tileMapVisual);
        
}

    private HashSet<Vector2Int> CreateRandomRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for(int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkData, roomCenter);
            foreach (var position in roomFloor)
            {
                if(position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset)&& position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }               
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var curRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(curRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestointTo(curRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(curRoomCenter, closest);
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int curRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = curRoomCenter;
        corridor.Add(position);
        while(position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            for (int i = -corridorWidth / 2; i <= corridorWidth / 2; i++)
            {
            corridor.Add(position + new Vector2Int(i, 0)); 
            }
        }
        
        while (position.x != destination.x)
        {
            if(destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            for (int i = -corridorWidth / 2; i <= corridorWidth / 2; i++)
            {
            corridor.Add(position + new Vector2Int(0, i)); 
            }
        }
        return corridor;
    }

    private Vector2Int FindClosestointTo(Vector2Int curRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float curDistance = Vector2.Distance(position, curRoomCenter);
            if (curDistance < distance)
            {
                distance = curDistance;
                closest = position; 

            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    
}
