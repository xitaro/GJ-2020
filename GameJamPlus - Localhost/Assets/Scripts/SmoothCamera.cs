using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class SmoothCamera : NetworkBehaviour
{
    [Header("Reference")]
    // Player or any other object to track with camera
    [SerializeField] private Transform lookAt = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private CinemachineTransposer transposer;

    // If the player is ours
    public override void OnStartAuthority()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        //enable camera
        virtualCamera.gameObject.SetActive(true);

        enabled = true;
    }
}
