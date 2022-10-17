using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            HelpWindow.Instance.ShowPrompt($"Нажмите E чтобы поговорить с {Name}");

            DialogueSystemController.Instance.AddDialogue(dialogue);

            player.IsCanInteract = true;
            player.Dialogue = dialogue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            HelpWindow.Instance.Hide();

            player.IsCanInteract = false;
            player.Dialogue = null;
        }
    }
}
