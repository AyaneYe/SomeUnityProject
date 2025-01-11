using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public VoidEventSO afterSceneLoadEvent;
    private CinemachineConfiner2D confiner2D;

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        afterSceneLoadEvent.onEventRaised += GetCameraBounds;
    }

    private void OnDisable()
    {
        afterSceneLoadEvent.onEventRaised -= GetCameraBounds;
    }

    //void Start()
    //{
    //    GetCameraBounds();
    //}

    void GetCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
            return;

        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();

        confiner2D.InvalidateCache();
    }
}
