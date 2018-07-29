using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AStarPathFindingTool
{
    public AStarPathFindingTool(int scaleType)
    {
        this.scaleType = scaleType;
    }
    private AStarPathFindingTool() { }

    public const int Small = 0;
    public const int Big = 1;

    private int scaleType = 0;

    private const int DefaultInt = -999;


    public Stack<Vector3> SumPathFromChunk(MapChunk[,] roomChunk, Vector3 curVec, Vector3 goalVec, float debugLineStayTime = 0)
    {
        MapChunk startMapVec = SolveMapChunk(roomChunk, curVec);
        MapChunk goalMapVec = SolveMapChunk(roomChunk, goalVec);
        //MapChunk startMapVec = roomChunk[(int)(Mathf.Round(curVec.x * 0.4f)), (int)(Mathf.Round(curVec.z * 0.4f))];
        //MapChunk goalMapVec = roomChunk[(int)(Mathf.Round(goalVec.x * 0.4f)), (int)(Mathf.Round(goalVec.z * 0.4f))];
        //Debug.Log("startMapVec:" + startMapVec.MapVector.X + "," + startMapVec.MapVector.Y + "  goalMapVec" + goalMapVec.MapVector.X + "," + goalMapVec.MapVector.Y);
        //bool inRange = JudgeMapVecInRoom(roomChunk, startMapVec.MapVector.X, startMapVec.MapVector.Y) && JudgeMapVecInRoom(roomChunk, goalMapVec.MapVector.X, goalMapVec.MapVector.Y);
        bool inRange = startMapVec != null && goalMapVec != null;

        //如果两个点都没有越界,并且两点都是可走空地则继续
        if (inRange)
        {
            bool canMove = (startMapVec.Weight != IBuilding.CannotWalkThrough && goalMapVec.Weight != IBuilding.CannotWalkThrough);
            if (canMove)
            {
                PriorityQueue mapVecAndCostPriorityQueue = new PriorityQueue();
                Dictionary<MapVector, MapChunk> comeFromDic = new Dictionary<MapVector, MapChunk>();
                Dictionary<MapVector, float> costDic = new Dictionary<MapVector, float>();
                //起始点入队
                mapVecAndCostPriorityQueue.Enqueue(new MapVecAndPriority(startMapVec, 0));
                //初始化花费
                costDic.Add(startMapVec.MapVector, 1);
                //当优先队列不为空时候
                while (mapVecAndCostPriorityQueue.Count > 0)
                {

                    //取出第一个点(花费最小的点)
                    MapVecAndPriority curMapVecAndCost = mapVecAndCostPriorityQueue.Dequeue();
                    //遍历其周围没有越界的可走的点
                    for (int i = curMapVecAndCost.MapChunk.MapVector.X - 1; i < curMapVecAndCost.MapChunk.MapVector.X + 2; i++)
                    {
                        for (int j = curMapVecAndCost.MapChunk.MapVector.Y - 1; j < curMapVecAndCost.MapChunk.MapVector.Y + 2; j++)
                        {
                            if (JudgeMapVecInRoom(roomChunk, i, j) && !(curMapVecAndCost.MapChunk.MapVector.X == i && curMapVecAndCost.MapChunk.MapVector.Y == j))
                            {
                                if (roomChunk[i, j].Weight != IBuilding.CannotWalkThrough)
                                {
                                    //根据体型判断是否能斜向通过
                                    if (scaleType == Small)
                                    {
                                        //计算当前花费
                                        float newCost = costDic[curMapVecAndCost.MapChunk.MapVector] + roomChunk[i, j].Weight;
                                        float priority = ManhattanAlgorithm(newCost, roomChunk[i, j], goalMapVec);
                                        if (i == goalMapVec.MapVector.X && j == goalMapVec.MapVector.Y)
                                        {
                                            //设置终点的来源
                                            comeFromDic.Add(roomChunk[i, j].MapVector, curMapVecAndCost.MapChunk);
                                            //资源释放
                                            mapVecAndCostPriorityQueue.Dispose();
                                            costDic = null;

                                            return GetPathsFromDic(comeFromDic, startMapVec, goalMapVec, debugLineStayTime);
                                        }
                                        //如果邻居已经在花费列表
                                        if (costDic.ContainsKey(roomChunk[i, j].MapVector))
                                        {
                                            //如果花费小于列表中的则更新Cost及ComeFrom
                                            if (newCost < costDic[roomChunk[i, j].MapVector])
                                            {
                                                costDic[roomChunk[i, j].MapVector] = newCost;
                                                comeFromDic[roomChunk[i, j].MapVector] = curMapVecAndCost.MapChunk;
                                                mapVecAndCostPriorityQueue.Enqueue(new MapVecAndPriority(roomChunk[i, j], priority));
                                            }
                                        }
                                        //如果不包含
                                        else
                                        {
                                            costDic.Add(roomChunk[i, j].MapVector, newCost);
                                            comeFromDic.Add(roomChunk[i, j].MapVector, curMapVecAndCost.MapChunk);
                                            mapVecAndCostPriorityQueue.Enqueue(new MapVecAndPriority(roomChunk[i, j], priority));
                                        }
                                    }
                                    //如果体型大
                                    else
                                    {
                                        //通过if过滤掉
                                        if (i == curMapVecAndCost.MapChunk.MapVector.X || j == curMapVecAndCost.MapChunk.MapVector.Y)
                                        {
                                            //计算当前花费
                                            float newCost = costDic[curMapVecAndCost.MapChunk.MapVector] + roomChunk[i, j].Weight;
                                            float priority = ManhattanAlgorithm(newCost, roomChunk[i, j], goalMapVec);
                                            if (i == goalMapVec.MapVector.X && j == goalMapVec.MapVector.Y)
                                            {
                                                //设置终点的来源
                                                comeFromDic.Add(roomChunk[i, j].MapVector, curMapVecAndCost.MapChunk);
                                                //资源释放
                                                mapVecAndCostPriorityQueue.Dispose();
                                                costDic = null;

                                                return GetPathsFromDic(comeFromDic, startMapVec, goalMapVec, debugLineStayTime);
                                            }
                                            //如果邻居已经在花费列表
                                            if (costDic.ContainsKey(roomChunk[i, j].MapVector))
                                            {
                                                //如果花费小于列表中的则更新Cost及ComeFrom
                                                if (newCost < costDic[roomChunk[i, j].MapVector])
                                                {
                                                    costDic[roomChunk[i, j].MapVector] = newCost;
                                                    comeFromDic[roomChunk[i, j].MapVector] = curMapVecAndCost.MapChunk;
                                                    mapVecAndCostPriorityQueue.Enqueue(new MapVecAndPriority(roomChunk[i, j], priority));
                                                }
                                            }
                                            //如果不包含
                                            else
                                            {
                                                costDic.Add(roomChunk[i, j].MapVector, newCost);
                                                comeFromDic.Add(roomChunk[i, j].MapVector, curMapVecAndCost.MapChunk);
                                                mapVecAndCostPriorityQueue.Enqueue(new MapVecAndPriority(roomChunk[i, j], priority));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return null;
            }
            else
            {
                Debug.Log("起始点或终点不可走");
                return null;
            }
        }
        //如果越界则直接跳出并打印信息
        else
        {
            if (!inRange)
            {
                Debug.Log("起始点或终点产生越界");
            }
            return null;
        }
    }


    /// <summary>
    /// 根据坐标处理地图块
    /// </summary>
    /// <param name="p"></param>
    /// <param name="roomChunk"></param>
    /// <param name="curVec"></param>
    /// <returns></returns>
    private MapChunk SolveMapChunk(MapChunk[,] roomChunk, Vector3 curVec)
    {
        int x = (int)(Mathf.Round(curVec.x * 0.4f));
        int y = (int)(Mathf.Round(curVec.z * 0.4f));
        //如果在合法区域内
        if (JudgeMapVecInRoom(roomChunk, x, y))
        {
            if (roomChunk[x, y].Building != null)
            {
                //如果建筑权重不可走
                if (roomChunk[x, y].Building.Weight == IBuilding.CannotWalkThrough)
                {
                    float curMinDis = float.MaxValue;
                    int curMinX = DefaultInt, curMinY = DefaultInt;
                    //遍历
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            //过滤掉地图外的点
                            if (JudgeMapVecInRoom(roomChunk, i, j))
                            {
                                if (roomChunk[i, j].Building != null)
                                {
                                    //过滤掉是建筑的点
                                    if (roomChunk[i, j].Building.Weight != IBuilding.CannotWalkThrough)
                                    {
                                        //如果是未赋值
                                        if (curMinX == DefaultInt && curMinY == DefaultInt)
                                        {
                                            curMinX = i;
                                            curMinY = j;
                                            curMinDis = Vector3.Distance(new Vector3(i * 2.5f, 0, j * 2.5f), curVec);
                                        }
                                        else
                                        {
                                            float dis = Vector3.Distance(new Vector3(i * 2.5f, 0, j * 2.5f), curVec);
                                            if (dis < curMinDis)
                                            {
                                                curMinX = i;
                                                curMinY = j;
                                                curMinDis = dis;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //如果是未赋值
                                    if (curMinX == DefaultInt && curMinY == DefaultInt)
                                    {
                                        curMinX = i;
                                        curMinY = j;
                                        curMinDis = Vector3.Distance(new Vector3(i * 2.5f, 0, j * 2.5f), curVec);
                                    }
                                    else
                                    {
                                        float dis = Vector3.Distance(new Vector3(i * 2.5f, 0, j * 2.5f), curVec);
                                        if (dis < curMinDis)
                                        {
                                            curMinX = i;
                                            curMinY = j;
                                            curMinDis = dis;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (curMinX == DefaultInt || curMinY == DefaultInt)
                    {
                        Debug.Log("赋值失败，周围没有合法地图块");
                        return null;
                    }
                    else
                    {
                        return roomChunk[curMinX, curMinY];
                    }
                }
                else
                {
                    return roomChunk[x, y];
                }
            }
            else
            {
                return roomChunk[x, y];
            }

        }
        else
        {
            Debug.Log("当前坐标越界");
            return null;
        }

    }

    private Stack<Vector3> GetPathsFromDic(Dictionary<MapVector, MapChunk> comeFromDic, MapChunk start, MapChunk goal, float debugLineStayTime)
    {

        List<MapChunk> pathList = new List<MapChunk>();
        Stack<Vector3> mapVectorStack = new Stack<Vector3>();
        MapChunk mapVec = goal;
        //Debug.Log(comeFromDic.ContainsKey(start.MapVector));
        int n = 0;
        while (comeFromDic.ContainsKey(mapVec.MapVector))
        {
            pathList.Add(mapVec);
            mapVec = comeFromDic[mapVec.MapVector];
            n++;
            if (n > 300)
            {
                throw new Exception("Stack Boom!!!!");
            }
        }

        CoroutineManager.Instance.StartOneCoroutine(DebugLine(pathList, debugLineStayTime));
        mapVectorStack.Push(new Vector3(pathList[0].MapVector.X * 2.5f, 0, pathList[0].MapVector.Y * 2.5f));
        for (int i = 1; i < pathList.Count - 1; i++)
        {
            if (!((pathList[i + 1].MapVector - pathList[i].MapVector) == (pathList[i].MapVector - pathList[i - 1].MapVector)))
            {
                mapVectorStack.Push(new Vector3(pathList[i].MapVector.X * 2.5f, 0, pathList[i].MapVector.Y * 2.5f));
            }
        }
        mapVectorStack.Push(new Vector3(pathList[pathList.Count - 1].MapVector.X * 2.5f, 0, pathList[pathList.Count - 1].MapVector.Y * 2.5f));

        //资源释放
        comeFromDic = null;
        return mapVectorStack;
    }





    private IEnumerator DebugLine(List<MapChunk> mapChunks, float drawLineTime)
    {
        if (!(drawLineTime < 0.2f))
        {
            float endTime = Time.time + drawLineTime;
            while (Time.time < endTime)
            {
                for (int i = 0; i < mapChunks.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(mapChunks[i].MapVector.X, 0.4f, mapChunks[i].MapVector.Y) * 2.5f, new Vector3(mapChunks[i + 1].MapVector.X, 0.4f, mapChunks[i + 1].MapVector.Y) * 2.5f, Color.yellow);
                }
                yield return null;
            }
        }

    }


    private float ManhattanAlgorithm(float cost, MapChunk cur, MapChunk goal)
    {
        float returnValue = cost + Mathf.Abs(goal.MapVector.X - cur.MapVector.X) + Mathf.Abs(goal.MapVector.Y - cur.MapVector.Y);
        return returnValue;
    }
    private bool JudgeMapVecInRoom(MapChunk[,] roomChunk, int x, int y)
    {
        if (x > 0 && x < roomChunk.GetLength(0) && y > 0 && y < roomChunk.GetLength(1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
