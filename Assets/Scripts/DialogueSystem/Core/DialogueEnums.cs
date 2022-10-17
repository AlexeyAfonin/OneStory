namespace DialogueSystem.Core
{
    public class DialogueEnums
    {
        public enum StateDialogueGroup
        {
            None = 0,
            Start = 1,
            Progress = 2,
            Complete = 3,
        }

        public enum StateDialogue
        {
            NotStarted = 0,
            InProgress = 1,
            Completed = 2,
        }
    }
}
