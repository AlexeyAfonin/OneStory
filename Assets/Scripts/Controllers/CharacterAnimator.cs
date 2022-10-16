using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OneStory.Core.Utils.Enums;

public sealed class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;
    private CharacterAnimations _previousAnimation;
    private CharacterAnimations _currentAnimation;

    public void Init(Animator animatorController) => 
        _animator = animatorController;

    public void PlayAnimation(CharacterAnimations animation)
    {
        _previousAnimation = _currentAnimation;
        _currentAnimation = animation;


        if ((_currentAnimation != _previousAnimation) || 
            ((_previousAnimation.Equals(CharacterAnimations.Idle) && (_currentAnimation.Equals(CharacterAnimations.Idle)))))
        {
            ResetAnimatorParameters();

            switch (_currentAnimation)
            {
                case CharacterAnimations.Walk:
                    _animator.SetBool(CharacterAnimations.Walk.ToString(), true);
                    break;
                case CharacterAnimations.Attack:
                    _animator.SetTrigger(CharacterAnimations.Attack.ToString());
                    break;
                case CharacterAnimations.Hit:
                    _animator.SetTrigger(CharacterAnimations.Hit.ToString());
                    break;
                case CharacterAnimations.Dying:
                    _animator.Play(CharacterAnimations.Dying.ToString());
                    break;
                case CharacterAnimations.Idle:
                default:
                    break;
            }
        }
    }

    private void ResetAnimatorParameters()
    {
        _animator.SetBool(CharacterAnimations.Walk.ToString(), false);
        _animator.ResetTrigger(CharacterAnimations.Attack.ToString());
        _animator.ResetTrigger(CharacterAnimations.Hit.ToString());
    }
}
