using Cinemachine;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineImpulseSource impulseSource;

    public CinemachineVirtualCamera VirtualCamera { get; private set; }

    private void Awake()
    {
        VirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            impulseSource.GenerateImpulse();
        }
    }
}
