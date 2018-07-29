using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    /// <summary>
    /// 房间父物体
    /// </summary>
    private Transform roomParent;
    /// <summary>
    /// 房间父物体访问器
    /// </summary>
    public Transform RoomParent
    {
        get
        {
            if (roomParent == null)
            {
                roomParent = new GameObject("RoomParent").transform;
            }
            return roomParent;
        }
    }
    /// <summary>
    /// 随机地板数据
    /// </summary>
    /// <param name="floorArr"></param>
    /// <param name="floorTypeCount"></param>
    public void InitFloor(int[,] floorArr, int floorTypeCount)
    {
        for (int i = 0; i < floorArr.GetLength(0); i++)
        {
            for (int j = 0; j < floorArr.GetLength(1); j++)
            {
                floorArr[i, j] = Random.Range(0, floorTypeCount);
            }
        }
    }
    /// <summary>
    /// 根据地板数据创建生成地板物体
    /// </summary>
    /// <param name="floorArr"></param>
    public void CreateFloor(int[,] floorArr)
    {
        for (int i = 0; i < floorArr.GetLength(0); i++)
        {
            for (int j = 0; j < floorArr.GetLength(1); j++)
            {
                GameObject floorRes = ResourcesManager.Instance.Load<GameObject>(FolderPaths.Floor, "Floor_" + floorArr[i, j], true);
                //GameObject floor = 
                GameObject.Instantiate<GameObject>(floorRes, Vector3.right * 5f * i + Vector3.forward * 5f * j, Quaternion.identity, RoomParent);
            }
        }
    }
    /// <summary>
    /// 直接随机生成墙壁
    /// </summary>
    /// <param name="floorArr"></param>
    /// <param name="wallTypeCount"></param>
    public void InitAndCreateWall(int[,] floorArr, int wallTypeCount)
    {
        for (int h = 0; h < 3; h++)
        {
            //生成横向的墙
            for (int i = 0; i < floorArr.GetLength(0); i++)
            {
                //if (i != floorArr.GetLength(0) * 0.5)
                //{
                GameObject wallRes = ResourcesManager.Instance.Load<GameObject>(FolderPaths.Wall, "Wall_" + Random.Range(0, wallTypeCount), true);
                //生成上方的墙
                //GameObject upWall = 
                GameObject.Instantiate<GameObject>(wallRes, (Vector3.back * 2.5f + Vector3.right * 5f * i) + h * Vector3.up * 5f, Quaternion.identity, RoomParent);
                //GameObject downWall = 
                GameObject.Instantiate<GameObject>(wallRes, (Vector3.right * 5f * i + Vector3.forward * (5f * (floorArr.GetLength(1) - 1) + 2.5f)) + h * Vector3.up * 5f, Quaternion.Euler(0, 180, 0), RoomParent);
                //}
            }
            //生成竖向的墙
            for (int i = 0; i < floorArr.GetLength(1); i++)
            {
                GameObject wallRes = ResourcesManager.Instance.Load<GameObject>(FolderPaths.Wall, "Wall_" + Random.Range(0, wallTypeCount), true);
                //生成上方的墙
                //GameObject leftWall = 
                GameObject.Instantiate<GameObject>(wallRes, (Vector3.left * 2.5f + i * Vector3.forward * 5f) + h * Vector3.up * 5f, Quaternion.Euler(0, 90, 0), RoomParent);
                //GameObject rightWall = 
                GameObject.Instantiate<GameObject>(wallRes, (Vector3.right * (5f * (floorArr.GetLength(0) - 1) + 2.5f) + i * Vector3.forward * 5f) + h * Vector3.up * 5f, Quaternion.Euler(0, -90, 0), RoomParent);
            }
        }

    }
}
