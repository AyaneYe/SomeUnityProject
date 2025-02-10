using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CenterFade : MonoBehaviour
{
    //实时更新中心位置
    //Realtime update center position
    public Transform target;
    public Material material;

    private void Update()
    {
        if (target && material)
        {
            material.SetVector("_Center", target.position);
        }
    }
}
