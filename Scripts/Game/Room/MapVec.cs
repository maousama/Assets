using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class MapChunk
{
    private MapVector mapVector;
    private IBuilding building;
    private float weight;

    public MapChunk(int x, int y)
    {
        MapVector = new MapVector(x, y);
        Weight = 1;
    }

    public IBuilding Building
    {
        get
        {
            return building;
        }

        set
        {
            building = value;
            if (value != null)
            {
                Weight = building.Weight;
            }
        }
    }

    public float Weight
    {
        get
        {
            return weight;
        }

        set
        {
            weight = value;
        }
    }

    public MapVector MapVector
    {
        get
        {
            return mapVector;
        }

        set
        {
            mapVector = value;
        }
    }
}


public struct MapVector
{
    public MapVector(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X;
    public int Y;
    public static bool operator ==(MapVector v1, MapVector v2)
    {
        return v1.Equals(v2);
    }
    public static bool operator !=(MapVector v1, MapVector v2)
    {
        return !(v1 == v2);
    }
    public static MapVector operator -(MapVector v1, MapVector v2)
    {
        return new MapVector(v1.X - v2.X, v1.Y - v2.Y);
    }
    public static MapVector operator +(MapVector v1, MapVector v2)
    {
        return new MapVector(v1.X + v2.X, v1.Y + v2.Y);
    }
    public override bool Equals(object obj)
    {
        return X == ((MapVector)obj).X && Y == ((MapVector)obj).Y;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode() + X.GetHashCode() + Y.GetHashCode();
    }

}





public class MapVecAndPriority : IComparable
{
    public MapVecAndPriority(MapChunk mapVec, float priority)
    {
        MapChunk = mapVec;
        Priority = priority;
    }
    private MapVecAndPriority() { }

    private MapChunk mapVec;
    private float priority;

    public MapChunk MapChunk
    {
        get
        {
            return mapVec;
        }

        set
        {
            mapVec = value;
        }
    }


    public float Priority
    {
        get
        {
            return priority;
        }

        set
        {
            priority = value;
        }
    }

    public int CompareTo(object obj)
    {
        MapVecAndPriority mapVecAndCost = obj as MapVecAndPriority;
        return Priority.CompareTo(mapVecAndCost.Priority);
    }
}