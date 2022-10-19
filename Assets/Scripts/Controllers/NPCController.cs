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

    public override void TakeDamage(int damage) { } //Ignore 

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            HelpWindow.Instance.ShowPrompt($"Нажмите E чтобы поговорить с {Name}");
            player.IsCanInteract = true;
            player.ActiveDialogue = dialogue;
            player.InteractebleNPC = this;

            CameraController.Instance.SpeakersGroup.AddMember(transform, 1, 1);
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            if (player.State == CharacterState.Interact)
            {
                if (DialogueWindow.Instance.IsVisiable)
                {
                    HelpWindow.Instance.Hide();
                    player.EnableDialogueCamera();
                    player.IsInteracting = true;
                }
                else
                {
                    HelpWindow.Instance.Show();
                    player.DisableDialogueCamera();
                    player.IsInteracting = false;
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
            player.ActiveDialogue = null;
            player.InteractebleNPC = null;
            player.State = CharacterState.Free;

            CameraController.Instance.SpeakersGroup.RemoveMember(transform);
        }
    }
}
