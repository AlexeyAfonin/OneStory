using OneStory.DialogueSystem.SO;
using static OneStory.DialogueSystem.Core.DialogueEnums;

namespace OneStory.DialogueSystem.Core
{
    [System.Serializable]
    public sealed class DialogueContainer
    {
        public DialogueContainerSO Container;
        public StateDialogue State;

        public void EditState(StateDialogue state) => State = state;
    }
}
