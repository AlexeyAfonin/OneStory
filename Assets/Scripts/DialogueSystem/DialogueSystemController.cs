using Core.Utils;
using DialogueSystem.Core;
using DialogueSystem.SO;
using DialogueSystem.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DialogueSystem.Core.DialogueEnums;

namespace DialogueSystem
{
    [AddComponentMenu(menuName: "DialogueSystem/Controller/DialogueSystemContoller", order: 1)]
    public sealed class DialogueSystemController : MonobehSingleton<DialogueSystemController>
    {
        [SerializeField] private DialogueWindow dialogueWindow;
        [Space(10f)]
        [SerializeField] private List<DialogueContainer> dialogues;

        public List<DialogueContainer> Dialogues => dialogues;

        private DialogueContainer _currentDialogue;

        public void AddDialogue(DialogueContainerSO dialogue)
        {
            bool isNewDialogue = dialogues.FindAll(d => d.Container == dialogue).Count() < 1;

            if (isNewDialogue)
            {
                dialogues.Add(new DialogueContainer(dialogue, StateDialogue.NotStarted));
            }
        }

        public void ShowDialogue(string name)
        {
            _currentDialogue = dialogues.FirstOrDefault(d => d.Container.NameContainer == name);

            switch (_currentDialogue.State)
            {
                case StateDialogue.NotStarted:
                    dialogueWindow.Show();
                    dialogueWindow.StartDialogue(_currentDialogue.Container, _currentDialogue.State);
                    _currentDialogue.EditState(StateDialogue.InProgress);
                    break;
                case StateDialogue.InProgress:
                    dialogueWindow.Show();
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
    }
}
