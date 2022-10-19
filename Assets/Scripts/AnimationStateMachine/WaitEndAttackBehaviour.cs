using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitEndAttackBehaviour : StateMachineBehaviour
{
    private CharacterController _character;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _character = animator.transform.GetComponentInParent<CharacterController>();
        _character.IsAttacking = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _character.IsAttacking = false;
    }
}
