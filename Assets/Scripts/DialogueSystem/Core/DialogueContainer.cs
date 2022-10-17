using DialogueSystem.SO;
using static DialogueSystem.Core.DialogueEnums;

namespace DialogueSystem.Core
{
    [System.Serializable]
    public sealed class DialogueContainer
    {
        public DialogueContainerSO Container;
        public StateDialogue State;

        public void EditState(StateDialogue state) => State = state;
    }
}
