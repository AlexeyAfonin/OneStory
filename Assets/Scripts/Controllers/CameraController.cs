using Cinemachine;
using Core.Utils;
using UnityEngine;

public sealed class CameraController : MonobehSingleton<CameraController>
{
    [Header("Cameras")]
    [SerializeField] private Camera mainCamera;
    [Space(10f)]
    [Header("Cinemachine")]
    [SerializeField] private CinemachineFreeLook playerCinemachineCamera;
    [SerializeField] private CinemachineVirtualCamera dialogueCinemachineCamera;
    [Space(5f)]
    [SerializeField] private CinemachineTargetGroup speakersGroup;
    
    public CinemachineFreeLook PlayerCinemachineCamera => playerCinemachineCamera;
    public CinemachineVirtualCamera DialogueCinemachineCamera => dialogueCinemachineCamera;
    public CinemachineTargetGroup SpeakersGroup => speakersGroup;
    public Camera MainCamera => mainCamera;
}
