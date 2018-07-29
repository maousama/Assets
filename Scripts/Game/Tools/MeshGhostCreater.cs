using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGhostCreater : MonoBehaviour {

    public List<GhostObjectInfo> ghostInfoList;

    public static MeshGhostCreater Instance;

    private void Awake()
    {
        Instance = this;
        ghostInfoList = new List<GhostObjectInfo>();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < ghostInfoList.Count; i++)
        {
            if (ghostInfoList[i].isClear)
            {
                ghostInfoList.RemoveAt(i);
                i--;
                continue;
            }

            ghostInfoList[i].ManageShadowsInUpdate();
        }
    }

    /// <summary>
    /// 创建单个幻影
    /// </summary>
    /// <param name="meshRoot">网格节点</param>
    /// <param name="singleStayTime">单个影子的持续时间</param>
    /// <param name="ghostColor">颜色</param>
    /// <param name="beginAlpha">起始透明度</param>
    /// <param name="endAlpha">结束时的透明度</param>
    public void CreateSingleGhost(Transform meshRoot, float singleStayTime, Color ghostColor, float beginAlpha = 1, float endAlpha = 0)
    {
        GhostObjectInfo info = new GhostObjectInfo(meshRoot, 0.01f, singleStayTime, 0.01f, ghostColor, beginAlpha, endAlpha);
        ghostInfoList.Add(info);
    }

    /// <summary>
    /// 创建幻影
    /// </summary>
    /// <param name="ghostObjectInfo">幻影信息</param>
    public void BeginCreateGhost(GhostObjectInfo ghostObjectInfo)
    {
        ghostInfoList.Add(ghostObjectInfo);
    }

    /// <summary>
    /// 拖影将一直创建，需要手动关闭
    /// </summary>
    /// <param name="meshRoot">网格根节点，一般为挂载Anmator的物体</param>
    /// <param name="totalTime">单个影子的存在时间</param>
    /// <param name="timeBetweenTwoGhost">两个影子的间隔时间</param>
    /// <param name="color">颜色</param>
    /// <param name="beginAlpha">起始透明度</param>
    /// <param name="endAlpha">结束时的透明度</param>
    /// <returns></returns>
    public GhostObjectInfo BeginCreateGhost(Transform meshRoot, float totalTime, float timeBetweenTwoGhost, Color color, float beginAlpha = 1, float endAlpha = 0)
    {
        GhostObjectInfo info = new GhostObjectInfo(meshRoot, float.MaxValue, totalTime, timeBetweenTwoGhost, color, beginAlpha, endAlpha);
        ghostInfoList.Add(info);
        return info;
    }


    /// <summary>
    /// 创建幻影并在lastTime后停止创建
    /// </summary>
    /// <param name="meshRoot">网格根节点，一般为挂载Anmator的物体</param>
    /// <param name="totalTime">持续创建的时间</param>
    /// <param name="singleShadowLastTime">单个影子的存在时间</param>
    /// <param name="timeBetweenTwoGhost">两个影子的间隔时间</param>
    /// <param name="color">颜色</param>
    /// <param name="beginAlpha">起始透明度</param>
    /// <param name="endAlpha">结束时的透明度</param>
    /// <returns></returns>
    public GhostObjectInfo BeginCreateGhost(Transform meshRoot, float totalTime, float singleShadowLastTime, float timeBetweenTwoGhost, Color color, float beginAlpha = 1, float endAlpha = 0)
    {
        GhostObjectInfo info = new GhostObjectInfo(meshRoot, totalTime, singleShadowLastTime, timeBetweenTwoGhost, color, beginAlpha, endAlpha);
        ghostInfoList.Add(info);
        return info;
    }


    public void StopCreateGhost(GhostObjectInfo info)
    {
        info.totalTime = -1;
    }



}



[Serializable]
public class GhostObjectInfo
{
    //总时间
    public float totalTime;
    //当前已经运行时间
    public float currentTime;
    //是否幻影已经全部消失
    public bool isClear;
    //需要绘制mesh的根节点
    private Transform meshRoot;
    //每个幻影需要保留的时间
    private float stayTime;
    //绘制的间隔
    private float waitTime;
    //起始的透明度
    private float beginAlpha;
    //结束透明度
    private float endAlpha;
    //每秒下降的透明度
    private float subAlphaPerSencend;
    //影子颜色
    private Color color;

    private float createShadowTimes;

    private List<MeshInfo> meshSets;



    class MeshInfo
    {
        public Mesh mesh;
        public float alpha;
        public Material material;
        public Matrix4x4 matrix4X4;
    }
    /// <summary>
    /// 产生一个拖影请求
    /// </summary>
    /// <param name="meshRoot">拖影的mesh根节点</param>
    /// <param name="totalTime">拖影总持续时间</param>
    /// <param name="stayTime">单个拖影存在时间</param>
    /// <param name="waitTime">两个拖影之间的时间间隔</param>
    /// <param name="beginAlpha">开始时的透明度</param>
    public GhostObjectInfo(Transform meshRoot, float totalTime, float stayTime, float waitTime, Color color, float beginAlpha, float endAlpha)
    {
        this.meshRoot = meshRoot;
        this.totalTime = totalTime;
        this.color = color;
        this.stayTime = stayTime;
        this.waitTime = waitTime;
        this.beginAlpha = beginAlpha;
        this.endAlpha = endAlpha;
        meshSets = new List<MeshInfo>();
        currentTime = 0;
        createShadowTimes = 0;
        isClear = false;

        subAlphaPerSencend = endAlpha - beginAlpha / stayTime;

        AddMeshNow();
        RefreshShadow();
    }

    public void ManageShadowsInUpdate()
    {
        currentTime += Time.deltaTime;
        createShadowTimes += Time.deltaTime;
        AddMeshAfterDurationTime();
        RefreshShadow();
    }

    public void DrawShadowInBegining()
    {
        AddMeshAfterDurationTime();
        RefreshShadow();
    }


    public void AddMeshAfterDurationTime()
    {

        if (createShadowTimes >= waitTime && currentTime <= totalTime)
        {
            AddMeshNow();
            createShadowTimes = 0;

        }
    }

    public void AddMeshNow()
    {
        foreach (SkinnedMeshRenderer skinnedMesh in meshRoot.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            Mesh newMesh = new Mesh();
            skinnedMesh.BakeMesh(newMesh);
            Material newMaterial = new Material(skinnedMesh.material);

            newMaterial.shader = Shader.Find("Standard");



            //设置透明渲染
            newMaterial = SetMaterial(newMaterial);
            //设置颜色
            newMaterial.color = color;
            meshSets.Add(new MeshInfo
            {
                mesh = newMesh,
                matrix4X4 = skinnedMesh.transform.localToWorldMatrix,
                material = newMaterial,
                alpha = beginAlpha
            });

        }
    }




    public Material SetMaterial(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

        return material;
    }

    public void RefreshShadow()
    {

        for (int i = 0; i < meshSets.Count; i++)
        {
            meshSets[i].alpha += Time.deltaTime * subAlphaPerSencend;

            if ((subAlphaPerSencend < 0 && meshSets[i].alpha <= endAlpha) || (subAlphaPerSencend > 0 && meshSets[i].alpha >= endAlpha))
            {
                meshSets.RemoveAt(i);
                i--;
                if (meshSets.Count == 0)
                {
                    isClear = true;
                }
                continue;

            }

            Color color = meshSets[i].material.color;
            color.a = meshSets[i].alpha;
            meshSets[i].material.color = color;
            Graphics.DrawMesh(meshSets[i].mesh, meshSets[i].matrix4X4, meshSets[i].material, 0);

        }
    }

}