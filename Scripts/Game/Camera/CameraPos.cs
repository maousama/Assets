using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraPos : MonoBehaviour
{
    [Header("===== Reqiure =====")]
    public Transform playerHandler;
    public Transform viewPoint;
    public InputManager input;
    public Transform modle;



    [Header("Sensitive")]
    [Range(0, 2)]
    public float xSensitive;
    [Range(0, 2)]
    public float ySensitive;
    [Range(0, 1)]
    public float followSpeed;
    [Space(10)]
    [Header("===== Angle =====")]
    [Range(0, 90)]
    public float maxAngle;
    [Range(0, -50)]
    public float minAngle;
    public float currentLocalAngle;
    [Header("===== Distance ======")]
    [Range(0, 10)]
    public float minDistance;
    [Range(0, 10)]
    public float midleDistance;
    [Range(0, 10)]
    public float maxDistance;
    [Header("===== LockInfo =====")]
    [Range(0, 10)]
    public float lockedCameraDistance;
    [Range(-2, 2)]
    public float lockedCameraHeight;
    public float maxLoseLockDistance;
    public float maxCanlockDistance;
    [Range(0, 1)]
    public float lockScreenXLimits;
    [Range(0, 1)]
    public float lockScreenYLimits;

    public bool isLocked;

    [Space(20)]
    //可以删除的字段，测试用
    [Header("Show")]
    public bool showCursor;
    public bool shownLockCanDistance;



    private Image lockPoint;
    private Transform lockTarget;
    private Vector3 playerHalfOrigin;
    private int screenLimits;

    private void Start()
    {
        //lockPoint = player.lockPoint;
        //lockPoint.enabled = false;
    }

    //处理视角转动的输入
    //如果视角没有锁，由input决定角色面向的方向，否则始终面向target
    private void RotatePlayer()
    {
        if (!isLocked)
        {
            var modleRotation = modle.rotation;
            playerHandler.Rotate(Vector3.up, Input.GetAxis("Mouse X") * xSensitive);
            modle.rotation = modleRotation;
        }
        else
        {
            Vector3 lookVect = lockTarget.position - playerHandler.position;
            playerHandler.forward = new Vector3(lookVect.x, 0, lookVect.z);
        }

    }


    private void OnDrawGizmos()
    {
        if (shownLockCanDistance)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerHandler.position, maxCanlockDistance);
        }

    }

    /// <summary>
    /// 需要精简
    /// </summary>
    private void LateUpdate()
    {
        if (showCursor)
        {
            //Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        RotatePlayer();

        //Vector3 playerHalfOrigin = playerHandler.position + playerHandler.GetComponent<CharacterController>().center;

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (isLocked)
            {
                isLocked = false;
                //lockPoint.enabled = false;
            }
            else
            {
                Transform target = FindNearestTarget(LayerMask.GetMask("Monster"), maxCanlockDistance, lockScreenXLimits, lockScreenYLimits);

                if (target != null)
                {
                    LockThisTarget(target);
                }
            }
        }

        if (isLocked)
        {
            transform.forward = lockTarget.position - viewPoint.position;

            //处理距离和高度
            transform.position = viewPoint.position + (-transform.forward * lockedCameraDistance);
            transform.position = transform.position + lockedCameraHeight * transform.up;

        }
        else
        {
            //处理角度

            //将0-360转换为±180
            currentLocalAngle = GetPosNeg180RotationAngle();

            float needRotateAngle = Input.GetAxis("Mouse Y") * ySensitive;

            //如果超过最大或者最小角度，规范化输入
            if (needRotateAngle + currentLocalAngle > maxAngle)
            {
                needRotateAngle = maxAngle - currentLocalAngle;
            }

            if (needRotateAngle + currentLocalAngle < minAngle)
            {
                needRotateAngle = minAngle - currentLocalAngle;
            }

            transform.RotateAround(viewPoint.position, transform.right, needRotateAngle);

            transform.LookAt(viewPoint);


            //处理距离
            currentLocalAngle = GetPosNeg180RotationAngle();



            float needDistance = 0;

            if (currentLocalAngle > 0)
            {
                needDistance = Mathf.Lerp(midleDistance, maxDistance, currentLocalAngle / maxAngle);
            }

            if (currentLocalAngle <= 0)
            {
                needDistance = Mathf.Lerp(midleDistance, minDistance, currentLocalAngle / minAngle);
            }

            transform.position = viewPoint.position + (-transform.forward * needDistance);
        }




        //如果有遮挡前移
        Ray ray = new Ray(viewPoint.position, -transform.forward);
        RaycastHit raycast;
        if (Physics.Raycast(ray, out raycast, Vector3.Distance(transform.position, viewPoint.position), LayerMask.GetMask("Building")))
        {
            transform.position = raycast.point + transform.forward * 0.1f;
        }



        //如果大于追踪距离，取消锁定
        if (isLocked && Vector3.Distance(playerHandler.position, lockTarget.position) > maxLoseLockDistance)
        {
            isLocked = false;
            //lockPoint.enabled = false;
        }


        if (isLocked && Mathf.Abs(GetPosNeg180RotationAngle()) > maxAngle)
        {
            isLocked = false;
            //lockPoint.enabled = false;
        }

    }


    public void LockThisTarget(Transform trans)
    {
        isLocked = true;
        //lockPoint.enabled = true;
        lockTarget = trans;
        transform.forward = lockTarget.position - viewPoint.position;
        return;
    }

    /// <summary>
    /// 将当前角度从360转化为±180
    /// </summary>
    /// <returns></returns>
    public float GetPosNeg180RotationAngle()
    {
        return transform.rotation.eulerAngles.x > 180 ? transform.rotation.eulerAngles.x - 360 : transform.rotation.eulerAngles.x;
    }

    /// <summary>
    /// 获得摄像机半径内所有在屏幕中的物体
    /// </summary>
    /// <param name="layerMask">选择的物体layermask</param>
    /// <param name="distance">半径</param>
    /// <param name="screenXLimits">筛选在屏幕半径内，1为全屏，最小是0</param>
    /// <returns></returns>
    public Transform FindNearestTarget(int layerMask, float distance, float screenXLimits = 1, float screenYLimits = 1)
    {
        Collider[] colliders = Physics.OverlapSphere(playerHandler.position, distance, layerMask);
        //Collider nearestColliders = FindNearestColliderFromScreenCenter(colliders, screenXLimits, screenYLimits);
        //return nearestColliders == null ? null : nearestColliders.transform;
        if (colliders == null || colliders.Length == 0)
        {
            return null;
        }
        else
        {
            return colliders[0].transform;
        }

    }

    Collider FindNearestColliderFromScreenCenter(Collider[] colliders, float screenXLimits, float screenYLimits)
    {
        Collider nearestCollider = null;
        float nearestDistance = float.MaxValue;
        Vector2 camerCenter = new Vector2(0.5f, 0.5f);


        foreach (Collider col in colliders)
        {
            Vector3 colScreenPos = Camera.main.WorldToViewportPoint(col.transform.position);

            if (isInScreen(colScreenPos, screenXLimits, screenYLimits))
            {

                float newDistance = Vector2.Distance(colScreenPos, camerCenter);

                if (newDistance < nearestDistance)
                {
                    nearestDistance = newDistance;
                    nearestCollider = col;
                }

            }
        }

        return nearestCollider;
    }


    public bool isInScreen(Vector2 viewportPointPos, float screenXLimits, float screenYLimits)
    {
        float center = 0.5f;
        float halfXLimits = screenXLimits / 2;
        float halfYLimits = screenYLimits / 2;
        if (viewportPointPos.x >= center - halfXLimits && viewportPointPos.x <= center + halfXLimits)
        {
            if (viewportPointPos.y >= center - halfYLimits && viewportPointPos.y <= center + halfYLimits)
            {
                return true;
            }
        }

        return false;
    }



}
