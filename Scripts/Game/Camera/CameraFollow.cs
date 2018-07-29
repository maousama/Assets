using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public CameraPos cameraPos;

    public static CameraFollow Instance;


    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, cameraPos.transform.position, cameraPos.followSpeed);
        transform.rotation = cameraPos.transform.rotation;
    }
}
