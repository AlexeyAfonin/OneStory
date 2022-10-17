using OneStory.Core.Utils;
using OneStory.DialogueSystem.Core;
using OneStory.DialogueSystem.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static OneStory.DialogueSystem.Core.DialogueEnums;

namespace OneStory.DialogueSystem
{
    public sealed class DialogueSystemController : MonobehSingleton<DialogueSystemController>
    {
        [Header("GUI")]
        [SerializeField] private Canvas ui;
        [SerializeField] private DialogueWindow dialogueWindow;
        [Space(10f)]
        [Header("Dialogues")]
        [SerializeField] private DialogueContainer[] dialogues;

        public DialogueContainer[] Dialogues => dialogues;

        private DialogueContainer _currentDialogue;

        public void ShowDialogue(string name)
        {
            _currentDialogue = dialogues.FirstOrDefault(d => d.Container.NameContainer == name);

            switch (_currentDialogue.State)
            {
                case StateDialogue.NotStarted:
                    ShowWindow();
                    dialogueWindow.StartDialogue(_currentDialogue.Container, _currentDialogue.State);
                    _currentDialogue.EditState(StateDialogue.InProgress);
                    break;
                case StateDialogue.InProgress:
                    ShowWindow();
                    dialogueWindow.StartDialogue(_currentDialogue.Container, _currentDialogue.State);
                    break;
                case StateDialogue.Completed:
                default:
                    break;
            }
        }

        public void CompleteDialogue(string name)
        {
            _currentDialogue = dialogues.FirstOrDefault(d => d.Container.NameContainer == name);
            _currentDialogue.State = StateDialogue.Completed;
        }

        public void ShowWindow() => dialogueWindow.gameObject.SetActive(true);
        public void HideWindow() => dialogueWindow.gameObject.SetActive(false);
    }
}
