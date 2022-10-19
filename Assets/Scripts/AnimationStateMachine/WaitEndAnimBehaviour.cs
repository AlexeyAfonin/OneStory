using UnityEngine;

public class WaitEndAnimBehaviour : StateMachineBehaviour
{
    private CharacterController _character;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _character = animator.transform.GetComponentInParent<CharacterController>();
        _character.IsWaitingEndAnimation = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _character.IsWaitingEndAnimation = false;
    }
}
