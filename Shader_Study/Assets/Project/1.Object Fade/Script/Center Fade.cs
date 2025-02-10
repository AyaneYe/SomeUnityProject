using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CenterFade : MonoBehaviour
{
    //ʵʱ��������λ��
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
