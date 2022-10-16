using UnityEngine;

public class HitBehaviour : StateMachineBehaviour
{
    private CharacterController character;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.transform.GetComponentInParent<CharacterController>();
        character.IsWaitAnim = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.IsWaitAnim = false;
    }
}
