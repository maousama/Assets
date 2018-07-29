using UnityEngine;

public class RoomController : MonoBehaviour
{
    [Header("MapElementSetting")]
    public int mapDensity = 30;
    [SerializeField, HideInInspector]
    private float pillarWeight;
    [SerializeField, HideInInspector]
    private float chestWeight;
    public float allWeight;



    public IMonster monster;

    private int[,] floorArr;
    private MapChunk[,] mapChunk;

    public MapChunk[,] MapChunk
    {
        get
        {
            return mapChunk;
        }
        private set
        {
            mapChunk = value;
        }
    }

    public float PillarWeight
    {
        get
        {
            return pillarWeight;
        }

        set
        {
            pillarWeight = value;
            allWeight = PillarWeight + ChestWeight;
        }
    }

    public float ChestWeight
    {
        get
        {
            return chestWeight;
        }

        set
        {
            chestWeight = value;
            allWeight = PillarWeight + ChestWeight;
        }
    }



    /// <summary>
    /// 生成房间环境
    /// </summary>
    public void CreateRoomEnvironment(int floorCountX, int floorCountY)
    {
        floorArr = new int[floorCountX, floorCountY];
        MapChunk = new MapChunk[2 * floorCountX - 1, floorCountY * 2 - 1];

        for (int i = 0; i < MapChunk.GetLength(0); i++)
        {
            for (int j = 0; j < MapChunk.GetLength(1); j++)
            {
                MapChunk[i, j] = new MapChunk(i, j);
            }
        }
        MapManager.Instance.InitFloor(floorArr, 23);
        MapManager.Instance.CreateFloor(floorArr);
        MapManager.Instance.InitAndCreateWall(floorArr, 10);
    }



    public void PutBuildingInRoom()
    {
        for (int i = 0; i < MapChunk.GetLength(0); i++)
        {
            for (int j = 0; j < MapChunk.GetLength(1); j++)
            {
                MapChunk curMapVec = MapChunk[i, j];
                float ranValue = Random.Range(0, allWeight);
                int ranInt = Random.Range(0, 100);
                if (ranInt < mapDensity)
                {
                    if (ranValue < pillarWeight)
                    {
                        GameObject go = Instantiate(ResourcesManager.Instance.Load<GameObject>(FolderPaths.Pillars, "Pillar_" + Random.Range(0, 14)), new Vector3(i * 2.5f, 0f, j * 2.5f), Quaternion.identity, null);
                        curMapVec.Building = go.GetComponent<IBuilding>();
                    }
                    else if (ranValue < ChestWeight + PillarWeight)
                    {
                        GameObject go = Instantiate(ResourcesManager.Instance.Load<GameObject>(FolderPaths.Chests, "Chest_" + Random.Range(0, 5)), new Vector3(i * 2.5f, 0f, j * 2.5f), Quaternion.identity, null);
                        curMapVec.Building = go.GetComponent<IBuilding>();
                    }
                }
            }
        }
    }




    private void Awake()
    {
        CreateRoomEnvironment(7, 11);
        PutBuildingInRoom();
        monster.InRoom = transform.GetComponent<RoomController>();
    }


}
