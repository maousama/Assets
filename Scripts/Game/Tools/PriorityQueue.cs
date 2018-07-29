using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue : IDisposable
{
    private List<MapVecAndPriority> list = new List<MapVecAndPriority>();

    public int Count
    {
        get { return list.Count; }
    }

    public MapVecAndPriority Dequeue()
    {
        MapVecAndPriority retT = list[0];
        list.RemoveAt(0);
        return retT;
    }

    public void Enqueue(MapVecAndPriority item)
    {
        list.Add(item);
        list.Sort();
    }
    public void Clear()
    {
        list.Clear();
    }

    public MapVecAndPriority GetMapVecAndPriority(int x, int y)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].MapChunk.MapVector.X == x && list[i].MapChunk.MapVector.Y == y)
            {
                return list[i];
            }
        }
        return null;
    }

    public void Dispose()
    {
        list.Clear();
        list = null;
    }
}


