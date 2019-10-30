using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationState : MonoBehaviour
{
    public Animator playerAnimator;
    public CharacterState playerNowState;
    public enum CharacterState
    {
        Idle = 0,
        Move = 1,
        Attack1 = 2,
        Attack2 = 3,
        Attack3 = 4,
        Skill = 5,
        Skill2 = 6,
        Die = 7,
        //디버그 찍으면 Move나옴 형변환 시 옆 인트값 나옴)
    }
    
    public void AnimationChange(CharacterState temp)
    {
        switch (temp)
        {
            case CharacterState.Idle:
                playerAnimator.SetInteger("State", (int)CharacterState.Idle);
                playerNowState = CharacterState.Idle;
                break;
            case CharacterState.Move:
                playerAnimator.SetInteger("State", (int)CharacterState.Move);
                playerNowState = CharacterState.Move;
                break;
            case CharacterState.Attack1:
                playerAnimator.SetInteger("State", (int)CharacterState.Attack1);
                playerNowState = CharacterState.Attack1;
                break;
            case CharacterState.Attack2:
                playerAnimator.SetInteger("State", (int)CharacterState.Attack2);
                playerNowState = CharacterState.Attack2;
                break;
            case CharacterState.Attack3:
                playerAnimator.SetInteger("State", (int)CharacterState.Attack3);
                playerNowState = CharacterState.Attack3;
                break;
            case CharacterState.Skill:
                playerAnimator.SetInteger("State", (int)CharacterState.Skill);
                playerNowState = CharacterState.Skill;
                break;
            case CharacterState.Skill2:
                playerAnimator.SetInteger("State", (int)CharacterState.Skill2);
                playerNowState = CharacterState.Skill2;
                break;
            case CharacterState.Die:
                playerAnimator.SetTrigger("Die");
                playerNowState = CharacterState.Die;
                break;
        }
            
    }   
}
