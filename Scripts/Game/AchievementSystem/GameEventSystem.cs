using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enum_GameEvent
{
    Null,
    EnemyKilled,
    WeaponCollectioned,
    TrapTriggered,

}
public class GameEventSystem : MonoBehaviour
{

    private Dictionary<Enum_GameEvent, ISubject> gameevents = new Dictionary<Enum_GameEvent, ISubject>();

    //释放
    //public override void Release()
    //{
    //    gameevents.Clear();
    //}
    public void RegisterObserver(Enum_GameEvent gameEvent, IObserver observer)
    {
        ISubject subject = GetGameEvent(gameEvent);
        if(subject!=null)
        {
            subject.Attach(observer);
            observer.Subject = subject;
        }
    }
    private ISubject GetGameEvent(Enum_GameEvent gameEvent)
    {
        if (gameevents.ContainsKey(gameEvent))
        {
            return gameevents[gameEvent];

        }
        ISubject subject = null;
        switch (gameEvent)
        {

            case Enum_GameEvent.EnemyKilled:
                subject = new EnemyKilledSubject();
                break;
            case Enum_GameEvent.WeaponCollectioned:
                subject = new WeaponCollectionedSubject();
                break;
            case Enum_GameEvent.TrapTriggered:
                subject = new TrapTriggeredSubject();
                break;
            default:
                Debug.Log("还没有产生指定的主题类");
                return null;
        }
        gameevents.Add(gameEvent, subject);
        return subject;
    }
    public void NotifySubject(Enum_GameEvent gameEvent,Object param)
    {
        if(gameevents.ContainsKey(gameEvent)==false)
        {
            return;
           
        }
        gameevents[gameEvent].SetParam(param);

    }
}
