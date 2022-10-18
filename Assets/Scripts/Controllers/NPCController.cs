using DialogueSystem;
using DialogueSystem.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static OneStory.Core.Utils.Enums;

public class NPCController : CharacterController
{
    [SerializeField] private RigBuilder rigBuilder;

    public RigBuilder RigBuilder => rigBuilder;

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

    public void PlayAnimation(string nameAnimation)
    {
        animator.Play(nameAnimation);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            HelpWindow.Instance.ShowPrompt($"Нажмите E чтобы поговорить с {Name}");
            player.IsCanInteract = true;
            player.DialogueWithOther = dialogue;
            player.InteractebleNPC = this;
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            if (player.State == CharacterState.Interacts)
            {
                if (DialogueWindow.Instance.IsVisiable)
                {
                    HelpWindow.Instance.Hide();
                }
                else
                {
                    HelpWindow.Instance.Show();
                }

                var cameraController = CameraController.Instance;

                if (cameraController.MainCamera.State == StateCamera.Freeze && 
                    (cameraController.MainCamera.Camera.transform.position != dialogueCameraViewPosition.position))
                {
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
        }
    }
}
