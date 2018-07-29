using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public abstract class IMagic : MonoBehaviour
{
    const float DefultValue =float.MinValue;

    public ICharacter owner;

    [Range(0,100)]
    public float speed;
    
    public float atk;

    public int throughGoCount;

    public IWeapon weapon;

    [Tooltip("每秒绕z轴旋转的速度")]
    public float rotatePerSencond;

    [Space(10)]
    public GameObject createGoOnStart;

    public GameObject createGoOnTrigger;

    public GameObject createGoOnDestroyed;

    [HideInInspector]
    public TimeLine TimeLine = new TimeLine();
    [HideInInspector]
    public AttackInfo info;

    public Action OnDestroyed;
    public Action OnStart;
    public Action OnTrigger;

    protected Rigidbody rig;

    protected SphereCollider col;
   
    virtual protected void Start()
    {
        rig = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();

        rig.useGravity = false;

        col.isTrigger = true;

        foreach (TimeLineEvent timeEvent in TimeLine)
        {
            StartCoroutine(BeginTimeLineEvent(timeEvent));
        }

        OnStart?.Invoke();
        if (createGoOnStart != null)
        {
            CreateGo(createGoOnStart);
        }
        
    }

    virtual protected void Update()
    {
        transform.Rotate(transform.forward, rotatePerSencond * Time.deltaTime);
    }

    #region Destroy
    public void DestroyMagicEndFram()
    {
        StartCoroutine(DestroyWaitForSectond(-1));
    }

    public void DestroyMagicEndFram(float waitTime)
    {
        StartCoroutine(DestroyWaitForSectond(waitTime));
    }


    IEnumerator DestroyWaitForSectond(float waitTime)
    {
        if (waitTime >= 0)
        {
            yield return new WaitForSeconds(waitTime);
        }
       
        yield return new WaitForEndOfFrame();
        DestroyMagicNow();
    }

    void DestroyMagicNow()
    {
        StopAllCoroutines();
        OnDestroyed?.Invoke();
        if (createGoOnDestroyed != null)
        {
            CreateGo(createGoOnDestroyed);
        }
        GameObject.Destroy(gameObject);
    }

    #endregion

    #region init
    public virtual void Init(AttackInfo info)
    {
        this.info = info;
    }


    public virtual void Init(AttackInfo info, IWeapon weapon, float speed = DefultValue)
    {
        Init(info);
        this.weapon = weapon;
        if (speed != DefultValue)
        {
            this.speed = speed;
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (createGoOnTrigger != null && throughGoCount > 1)
        {
            CreateGo(createGoOnTrigger);
        }

        throughGoCount--;
        OnTrigger?.Invoke();
        
        if (throughGoCount <= 0)
        {
            DestroyMagicEndFram();
        }
    }

    public IEnumerator BeginTimeLineEvent(TimeLineEvent lineEvent)
    {
        yield return new WaitForSeconds(lineEvent.time);
        if (lineEvent.createGo != null)
        {
            CreateGo(lineEvent.createGo);
        }
        lineEvent?.act();
    }

    protected void CreateGo(GameObject gameObject)
    {
        Instantiate(gameObject).transform.position=transform.position;
    }



}


public class TimeLine:IEnumerable
{
    List<TimeLineEvent> timeLineEvents;
    
    public TimeLine()
    {
        timeLineEvents = new List<TimeLineEvent>();
    }

    public void Add(TimeLineEvent timeEvent)
    {
        timeLineEvents.Add(timeEvent);
        Sort();
    }

    public void AddLast(int count)
    {
        Sort();
        if (timeLineEvents.Count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                var timeEvent = new TimeLineEvent() { time = timeLineEvents[timeLineEvents.Count- 1].time };
                timeLineEvents.Add(timeEvent);
            }
            
        }
        else
        {
            timeLineEvents.Add(new TimeLineEvent());
            AddLast(count - 1);
        }
    }

    public int Count { get { return timeLineEvents.Count; } }

    public void Remove(int index)
    {
        timeLineEvents.RemoveAt(index);
        Sort();
    }

    public void RemoveLast(int count)
    {
        int removeCount = count > timeLineEvents.Count ? timeLineEvents.Count : count;

        for (int i = 0; i < removeCount; i++)
        {
            timeLineEvents.RemoveAt(timeLineEvents.Count-1);
        }
        Sort();
    }

    public IEnumerator GetEnumerator()
    {
        return timeLineEvents.GetEnumerator();
    }

    public TimeLineEvent this[int index]
    {
        get { Sort(); return timeLineEvents[index]; }
        set { timeLineEvents[index] = value; Sort(); }
    }

    public void Sort()
    {
        timeLineEvents.Sort((left, right) => left.time.CompareTo(right.time));
    }

}



[Serializable]
public class TimeLineEvent
{
    public float time;
    public Action act;
    public GameObject createGo;
}
