using Cinemachine;
using Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OneStory.Core.Utils.Enums;

public sealed class CameraController : MonobehSingleton<CameraController>
{
    [SerializeField] private CinemachineFreeLook playerCinemachineCamera;
    [SerializeField] private CameraStructure mainCamera;

    public CameraStructure MainCamera => mainCamera;

    public void SetCameraPosition(Vector3 position, Quaternion rotation)
    {
        mainCamera.Camera.transform.position = position;
        mainCamera.Camera.transform.rotation = rotation;
    }

    public void FreezeCamera()
    {
        mainCamera.Camera.GetComponent<CinemachineBrain>().enabled = false;
        mainCamera.State = StateCamera.Freeze;
    }

    public void UnfreezeCamera()
    {
        mainCamera.Camera.GetComponent<CinemachineBrain>().enabled = true;
        mainCamera.State = StateCamera.Unfreeze;
    }
}

[System.Serializable]
public struct CameraStructure
{
    public Camera Camera;
    public StateCamera State;
}
