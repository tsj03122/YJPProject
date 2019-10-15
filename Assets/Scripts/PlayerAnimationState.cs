using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationState : MonoBehaviour
{
    public Animator playerAnimator;
    public enum CharacterState
    {
        Idle = 0,
        Move = 1,
        Attack1 = 2,
        Attack2 = 3,
        Attack3 = 4,
        Skill1 = 5,
        Skill2 = 6,
        Dead = 7,
        //디버그 찍으면 Move나옴 형변환 시 옆 인트값 나옴)
    }
    
    public void AnimationChange(CharacterState temp)
    {
        switch (temp)
        {
            case CharacterState.Idle:
                playerAnimator.SetInteger("State", (int)CharacterState.Idle);
                break;
            case CharacterState.Move:
                playerAnimator.SetInteger("State", (int)CharacterState.Move);
                break;
            case CharacterState.Attack1:
                playerAnimator.SetInteger("State", (int)CharacterState.Attack1);
                break;
            case CharacterState.Attack2:
                playerAnimator.SetInteger("State", (int)CharacterState.Attack2);
                break;
            case CharacterState.Attack3:
                Debug.Log(CharacterState.Idle);
                break;
            case CharacterState.Skill1:
                Debug.Log(CharacterState.Idle);
                break;
            case CharacterState.Skill2:
                Debug.Log(CharacterState.Idle);
                break;
            case CharacterState.Dead:
                Debug.Log(CharacterState.Idle);
                break;
        }
            
    }   
}
