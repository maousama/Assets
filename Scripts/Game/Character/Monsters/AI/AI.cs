using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AI : MonoBehaviour
{
    private Scener scener;
    private IStrategy strategy;
    private IMonster owner;

    public IMonster Owner
    {
        get
        {
            return owner;
        }

        set
        {
            owner = value;
        }
    }
    public IStrategy Strategy
    {
        get
        {
            return strategy;
        }

        set
        {
            strategy = value;

        }
    }
    public int MonsterScaleType
    {
        get
        {
            return monsterScaleType;
        }

        set
        {
            monsterScaleType = value;
            Scener.aStarPathFindingTool = new AStarPathFindingTool(MonsterScaleType);
        }
    }
    public Scener Scener
    {
        get
        {
            return scener;
        }

        set
        {
            scener = value;
        }
    }


    #region Inspector
    [Header("===== Setting =====")]
    public LayerMask sceneCheckRayLayerMask;
    public Transform playerTarget;
    public float updateInterval = 1f;
    private int monsterScaleType = AStarPathFindingTool.Small;

    [Header("===== Controller =====")]
    public bool run = false;

    [Header("===== ScenerMessage =====")]
    public int canTakeDownBuildingCount;
    public RaycastHit[] sceneRaycastHit;
    public Stack<Vector3> aStarDestinationStack;

    [Header("===== StrategyMessage =====")]
    public StrategyType strategyType;
    public IBuilding curBuildingTarget;
    public Vector3 curDestination = NullVector;
    #endregion

    public static Vector3 NullVector = -999f * Vector3.one;


    #region 策略库
    /// <summary>
    /// 锁建筑策略
    /// </summary>
    public LockBuildingStrategy LockBuildingStrategy;
    /// <summary>
    /// 锁人策略
    /// </summary>
    public LockPlayerStrategy LockPlayerStrategy;
    #endregion



    private void Awake()
    {
        Owner = transform.GetComponent<IMonster>();
        Scener = new Scener(this);
        LockBuildingStrategy = new LockBuildingStrategy(this);
        LockPlayerStrategy = new LockPlayerStrategy(this);
        Strategy = LockPlayerStrategy;
        Scener.aStarPathFindingTool = new AStarPathFindingTool(MonsterScaleType);

    }
    private void Start()
    {
        StartCoroutine(AIUpdate());
    }


    public IEnumerator AIUpdate()
    {
        while (true)
        {
            if (run)
            {
                if (updateInterval < 0.2f)
                {
                    yield return null;
                }
                else
                {
                    yield return new WaitForSeconds(updateInterval);
                }
                //更新视觉逻辑
                Scener.ScenerLoop();
                //更新游戏逻辑
                Strategy.StrategyLoop();
            }
            else
            {
                yield return new WaitUntil(() => run);
            }
        }
    }

}
































//private void ActionUpdateFunc()
//{
//    float curDisFormCurTarget = Vector3.Distance(Owner.transform.position, curTarget.position);
//    //Rotate();
//    IAIState iAIState = Owner.CurrentState as IAIState;
//    bool inputAttack = false;
//    //判断是否在攻击范围内
//    for (int i = 0; i < iAIState.skillInfoList.Count; i++)
//    {
//        List<Transform> trList = iAIState.skillInfoList[i].RangeChecker.Check();
//        //如果敌人进入攻击范围并且攻击可以使用
//        if (trList.Count > 0 && iAIState.skillInfoList[i].CanUse)
//        {
//            //lastChaker = iAIState.skillInfoList[i].RangeChecker;
//            Owner.inputManager.inputBoolDic[iAIState.skillInfoList[i].InputStr] = true;
//            inputAttack = true;
//            break;
//        }
//    }
//    if (!inputAttack)
//    {
//        if (aStarDestinationStack != null)
//        {
//            if (aStarDestinationStack.Count != 0)
//            {
//                if (reachAstarDestination)
//                {
//                    curAStarDestination = aStarDestinationStack.Pop();
//                    reachAstarDestination = false;
//                }
//                else
//                {
//                    Vector3 goVec = curAStarDestination - Owner.transform.position;
//                    Vector3 goLocalVec = Owner.transform.InverseTransformDirection(goVec);
//                    goLocalVec -= goLocalVec.y * Vector3.up;
//                    goLocalVec = goLocalVec.normalized;
//                    Owner.inputManager.Horizontal = goLocalVec.x;
//                    Owner.inputManager.Vertical = goLocalVec.z;
//                    if ((curAStarDestination - Owner.transform.position).magnitude < 0.1f)
//                    {
//                        reachAstarDestination = true;
//                    }
//                }
//            }
//            else
//            {
//                Vector3 goVec = curTarget.position - Owner.transform.position;
//                Vector3 goLocalVec = Owner.transform.InverseTransformDirection(goVec);
//                goLocalVec -= goLocalVec.y * Vector3.up;
//                goLocalVec = goLocalVec.normalized;
//                Owner.inputManager.Horizontal = goLocalVec.x;
//                Owner.inputManager.Vertical = goLocalVec.z;
//            }
//        }
//        else
//        {
//            Vector3 goVec = curTarget.position - Owner.transform.position;
//            Vector3 goLocalVec = Owner.transform.InverseTransformDirection(goVec);
//            goLocalVec -= goLocalVec.y * Vector3.up;
//            goLocalVec = goLocalVec.normalized;
//            Owner.inputManager.Horizontal = goLocalVec.x;
//            Owner.inputManager.Vertical = goLocalVec.z;
//        }
//    }


//}

///// <summary>
///// 根据目标位置平滑旋转
///// </summary>
//private void Rotate()
//{
//    Vector3 lookPosDest = (curTarget.position - Owner.transform.position).normalized;
//    lookPosDest -= lookPosDest.y * Vector3.up;
//    float destAngle = Vector3.Angle(Vector3.forward, lookPosDest);
//    float curAngle = Owner.transform.rotation.eulerAngles.y;
//    curAngle = curAngle % 360;
//    float toAngle = 0;
//    if ((destAngle - curAngle) > 0)
//    {
//        toAngle = curAngle + Owner.RotSpeed;
//        if ((destAngle - curAngle) < 0)
//        {
//            toAngle = destAngle;
//        }
//    }
//    else
//    {
//        toAngle = curAngle - Owner.RotSpeed;
//        if ((destAngle - curAngle) > 0)
//        {
//            toAngle = destAngle;
//        }
//    }
//    Owner.transform.rotation = Quaternion.Euler(0, toAngle, 0);
//}

//}
