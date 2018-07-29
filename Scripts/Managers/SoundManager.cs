using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{

    public SoundManager()
    {
        CoroutineManager.Instance.StartOneCoroutine(SoundCollectUpdate());
    }

    public float soundCollectUpdateTime = 15f;
    public List<SoundInfo> soundInfoList = new List<SoundInfo>();


    public SoundInfo PlaySound2D(AudioClip clip)
    {
        AudioSource curAudioSource = GetAudioSourceFromSoundGameObject();
        curAudioSource.clip = clip;
        curAudioSource.spatialBlend = 0;
        SoundInfo soundInfo = new SoundInfo(SoundType._2DSound, curAudioSource.gameObject, curAudioSource.clip);
        soundInfo.canCollected = true;
        if (soundInfo.audioSource.loop == false)
        {
            soundInfoList.Add(soundInfo);
        }
        curAudioSource.Play();
        return soundInfo;
    }
    public void PauseSound2D(SoundInfo soundInfo)
    {
        soundInfo.audioSource.Pause();
    }
    public void StopSound2D(SoundInfo soundInfo)
    {
        soundInfo.audioSource.Stop();
        soundInfo.audioSource.clip = null;
        soundInfo.canCollected = false;
        PutSoundGameObject(soundInfo.soundGameObject);
    }

    

    public SoundInfo PlaySound3D(AudioClip clip, Vector3 soundPos)
    {
        AudioSource curAudioSource = GetAudioSourceFromSoundGameObject();
        curAudioSource.clip = clip;
        curAudioSource.spatialBlend = 1;
        curAudioSource.transform.position = soundPos;
        SoundInfo soundInfo = new SoundInfo(SoundType._3DSound, curAudioSource.gameObject, curAudioSource.clip);
        soundInfo.canCollected = true;
        if (soundInfo.audioSource.loop == false)
        {
            soundInfoList.Add(soundInfo);
        }
        curAudioSource.Play();
        return soundInfo;
    }

    public void PauseSound3D(SoundInfo soundInfo)
    {
        soundInfo.audioSource.Pause();
    }

    public void StopSound3D(SoundInfo soundInfo)
    {
        soundInfo.audioSource.Stop();
        soundInfo.audioSource.clip = null;
        soundInfo.canCollected = false;
        PutSoundGameObject(soundInfo.soundGameObject);
    }


    private IEnumerator SoundCollectUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(soundCollectUpdateTime);
            for (int i = 0; i < soundInfoList.Count; i++)
            {
                if (!soundInfoList[i].audioSource.isPlaying && soundInfoList[i].canCollected)
                {
                    StopSound2D(soundInfoList[i]);
                }
            }
        }
    }



    private AudioSource GetAudioSourceFromSoundGameObject()
    {
        GameObject soundGameObject = ObjectPoolManager.Instance.GetGameObject(FolderPaths.SoundGameObject, "SoundGameObject");
        if (soundGameObject.GetComponent<AudioSource>() != null)
        {
            return soundGameObject.GetComponent<AudioSource>();
        }
        else
        {
            return soundGameObject.AddComponent<AudioSource>();
        }
    }

    private void PutSoundGameObject(GameObject soundGameObject)
    {
        ObjectPoolManager.Instance.PutGameObject(soundGameObject);
    }

}





public class SoundInfo
{
    public SoundInfo(SoundType soundType, GameObject soundGameObject, AudioClip audioClip)
    {
        this.soundType = soundType;
        this.soundGameObject = soundGameObject;
        this.bindAudioClip = audioClip;
        this.audioSource = soundGameObject.GetComponent<AudioSource>();
    }
    private SoundInfo() { }
    public SoundType soundType;
    public GameObject soundGameObject;
    public AudioClip bindAudioClip;
    public AudioSource audioSource;

    public bool canCollected = true;
    
}
