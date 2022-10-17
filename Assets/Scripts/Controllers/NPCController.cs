using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OneStory.Core.Utils.Enums;

public class NPCController : CharacterController
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            HelpWindow.Instance.ShowPrompt($"Нажмите E чтобы поговорить с {Name}");

            DialogueSystemController.Instance.AddDialogue(dialogue);

            player.IsCanInteract = true;
            player.DialogueWithOther = dialogue;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            if (player.State == CharacterState.Interacts)
            {
                var cameraController = CameraController.Instance;

                if (cameraController.MainCamera.State == StateCamera.Unfreeze)
                {
                    cameraController.FreezeCamera();
                    cameraController.SetCameraPosition(dialogueCameraViewPosition.position, dialogueCameraViewPosition.rotation);
                }
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            HelpWindow.Instance.Hide();

            player.IsCanInteract = false;
            player.DialogueWithOther = null;

            var cameraController = CameraController.Instance;
            if (cameraController.MainCamera.State == StateCamera.Freeze)
            {
                cameraController.UnfreezeCamera();
            }
        }
    }
}
