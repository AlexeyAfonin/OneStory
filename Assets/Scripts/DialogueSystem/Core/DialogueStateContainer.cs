using DialogueSystem.SO;
using static DialogueSystem.Core.DialogueEnums;

namespace DialogueSystem.Core
{
    [System.Serializable]
    public sealed class DialogueStateContainer
    {
        public DialogueContainerSO Container;
        public StateDialogue State;

        public DialogueStateContainer(DialogueContainerSO container, StateDialogue state)
        {
            Container = container;
            State = state;
        }
    }
}
