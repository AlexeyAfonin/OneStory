using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehaviour : StateMachineBehaviour
{
    private EnemyController character;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.transform.GetComponentInParent<EnemyController>();
        character.StartCoroutine(character.IAttackPlayer());
        character.IsCanAttack = false;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.IsCanAttack = true;
    }
}
