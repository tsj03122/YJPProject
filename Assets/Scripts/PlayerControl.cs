using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public FloatingManager fm;
    public Rigidbody2D myRigidbody;
    public PlayerStats myStats;
    public Weapon weapon;
    public PlayerAnimationState myPlayerAnimationState;

    public float playerSpeed = 5f;
    public float attackTimer;
    public float move = 0f;
    public float jumpSpeed = 7f;
    public float jumpTimer = 1f;
    public bool canJump = true;
    public bool potal = false;
    public bool attack = false;
    public float comboAttackTimer = 0f;

    //던전 Start 확인 hp와 sp때문에 사용
    public bool dungeonStart = false;

    void Start()
    {
        fm = GameManager.m_instanceGM.floatingManager;
        attackTimer = 2f;
        GameManager.m_instanceGM.PlayerEquipmentSetting();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GameManager.m_instanceGM.playerDie)
        {
            return;
        }

        if (dungeonStart)
        {
            myStats.ChangeStats();
            dungeonStart = false;
        }

        canJump = false;
        move = Input.GetAxis("Horizontal");
        attackTimer += Time.deltaTime;
        jumpTimer += Time.deltaTime;
        Move();

        //스페셜 스킬
        if (Input.GetKeyDown(KeyCode.F) && myStats.spBar.value == 100 && SceneManager.GetActiveScene().name.Equals("Dungeon"))
        {
            myStats.SetSP(0);
            GameManager.m_instanceGM.playerSpecialSkill = true;
        }

        //스킬1
        if (Input.GetKeyDown(KeyCode.S) && !myStats.playerskill.skill.activeSelf && SceneManager.GetActiveScene().name.Equals("Dungeon"))
        {
            StartCoroutine("Skill1");
        }

        //스킬2
        if (Input.GetKeyDown(KeyCode.D) && !myStats.playerskill.Skill2.activeSelf && SceneManager.GetActiveScene().name.Equals("Dungeon"))
        {
            StartCoroutine("Skill2");
        }

        //점프 부분
        if (!canJump)
        {
            Debug.DrawRay(transform.position, new Vector2(0, -0.8f), new Color(255, 0, 0));
            //RaycastHit2D hit2d = Physics2D.Raycast(transform.position, Vector2.down, size, 1 << LayerMask.NameToLayer("Ground"));
            RaycastHit2D hit2d = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << LayerMask.NameToLayer("Foreground"));
            if (hit2d)
            {
                canJump = true;
            }
        }

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && canJump && jumpTimer >= 0.8f)
        {
            myRigidbody.AddForce(transform.up * jumpSpeed * 100);
            canJump = false;
            jumpTimer = 0;
        }

        //공격
        if (Input.GetKey(KeyCode.A) && !attack && SceneManager.GetActiveScene().name.Equals("Dungeon"))
        {
            if (attackTimer > myStats.attackSpeed)
            {
                StartCoroutine("Attack");
            }
        }

        //공격 키 마을에서 활성화
        else if (Input.GetKey(KeyCode.A) && SceneManager.GetActiveScene().name.Equals("Main") && potal)
        {
            GameManager.m_instanceGM.dungeonStage.SetActive(true);
        }
    }
    public void Move()
    {
        if (attack){return;}

        if (Input.GetKey(KeyCode.RightArrow))
        {
            myPlayerAnimationState.AnimationChange(PlayerAnimationState.CharacterState.Move);
            myRigidbody.velocity = new Vector2(move * playerSpeed, myRigidbody.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            myPlayerAnimationState.AnimationChange(PlayerAnimationState.CharacterState.Move);
            myRigidbody.velocity = new Vector2(move * playerSpeed, myRigidbody.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            myPlayerAnimationState.AnimationChange(PlayerAnimationState.CharacterState.Idle);
        }
    }

    IEnumerator Attack()
    {
        attack = true;
        attackTimer = 0f;
        while (attackTimer < myStats.attackSpeed)
        {
            if(myPlayerAnimationState.playerNowState == PlayerAnimationState.CharacterState.Attack3)
            {
                yield return new WaitForSeconds(0.8f);
            }
            else if (Input.GetKeyDown(KeyCode.A) && myPlayerAnimationState.playerNowState == PlayerAnimationState.CharacterState.Attack2 && attackTimer > 0.4f)
            {
                attackTimer = 0f;
                myPlayerAnimationState.AnimationChange(PlayerAnimationState.CharacterState.Attack3);
                AttackPlay();
            }
            else if(Input.GetKeyDown(KeyCode.A) && myPlayerAnimationState.playerNowState == PlayerAnimationState.CharacterState.Attack1 && attackTimer > 0.4f)
            {
                attackTimer = 0f;
                myPlayerAnimationState.AnimationChange(PlayerAnimationState.CharacterState.Attack2);
                AttackPlay();
            }
            else if(myPlayerAnimationState.playerNowState == PlayerAnimationState.CharacterState.Idle || myPlayerAnimationState.playerNowState == PlayerAnimationState.CharacterState.Move)
            {
                myPlayerAnimationState.AnimationChange(PlayerAnimationState.CharacterState.Attack1);
                AttackPlay();
            }
            yield return null;
        }
        attack = false;
    }

    public void AttackPlay()
    {
        if (weapon.monsterList.Count > 0)
        {
            for (int i = 0; i < weapon.monsterList.Count; i++)
            {
                int damage = (int)Random.Range((myStats.damage - (myStats.damage * 0.2f)), (myStats.damage + (myStats.damage * 0.2f)));
                weapon.monsterList[i].OnDamageHit(damage, 0);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potal"))
        {
            potal = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Potal"))
        {
            potal = false;
        }
    }

    IEnumerator Skill1()
    {
        attack = true;
        myStats.playerskill.skill.SetActive(true);
        myPlayerAnimationState.AnimationChange(PlayerAnimationState.CharacterState.Skill);
        yield return new WaitForSeconds(0.1f);
        //공격 횟수
        for (int range = 0; range < 4; range++)
        {
            for (int i = 0; i < myStats.playerskill.skillScript.monsterList.Count; i++)
            {
                //데미지 계산식 = 안의 랜덤수치 (무기데미지80% ) 에서 (무기데미지 120%) 의 17배
                float damage = (float)Random.Range((myStats.damage - (myStats.damage * 0.214f)), (myStats.damage + (myStats.damage * 0.216f)));
                myStats.playerskill.skillScript.monsterList[i].OnDamageHit((int)damage*17 , 1);
            }
            yield return new WaitForSeconds(0.20f);
        }
        attack = false;
    }

    IEnumerator Skill2()
    {
        attack = true;
        myStats.playerskill.Skill2.SetActive(true);
        myPlayerAnimationState.AnimationChange(PlayerAnimationState.CharacterState.Skill2);
        yield return new WaitForSeconds(0.1f);
        //공격 횟수
        for (int range = 0; range < 8; range++)
        {
            for (int i = 0; i < myStats.playerskill.skill2Script.monsterList.Count; i++)
            {
                //데미지 계산식 = 안의 랜덤수치 (무기데미지80% ) 에서 (무기데미지 120%) 의 17배
                float damage = (float)Random.Range((myStats.damage - (myStats.damage * 0.214f)), (myStats.damage + (myStats.damage * 0.216f)));
                myStats.playerskill.skill2Script.monsterList[i].OnDamageHit((int)damage * 17, 2);
            }
            yield return new WaitForSeconds(0.12f);
        }
        attack = false;
    }
}
