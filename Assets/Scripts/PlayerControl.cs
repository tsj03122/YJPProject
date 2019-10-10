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
    public Animator myAnimator;

    //좌측이 트루
    public bool playerDirection = false;
    public float specialTime = 0f;
    public float playerSpeed = 4.5f;
    public float attackTimer = 0f;
    public float move = 0f;
    public float jumpSpeed = 3f;
    public bool canJump = true;
    public bool potal = false;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        GameManager.m_instanceGM.playerControl = this;
        GameManager.m_instanceGM.PlayerEquipmentSetting();
    }

    // Update is called once per frame
    void Update()
    {
        canJump = false;
        move = Input.GetAxis("Horizontal");
        attackTimer += Time.deltaTime;
        //공격 쿨타임
        if (attackTimer >= 0.5f && SceneManager.GetActiveScene().name.Equals("Dungeon"))
        {
            //myStats.weapon.transform.localPosition = new Vector3(0.057f, 0, 0);
            if (playerDirection)
            {
                myStats.weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 40f));
            }
            else
            {
                myStats.weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -40f));
            }
        }

        //점프 부분
        if(!canJump)
        {
            Debug.DrawRay(transform.position, new Vector2(0,-0.6f),new Color(255,0,0));
            //RaycastHit2D hit2d = Physics2D.Raycast(transform.position, Vector2.down, size, 1 << LayerMask.NameToLayer("Ground"));
            RaycastHit2D hit2d = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << LayerMask.NameToLayer("Foreground"));
            if (hit2d)
            {
                    canJump = true;
            }
        }

        //움직임
        if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.velocity = new Vector2(move * playerSpeed, myRigidbody.velocity.y);
            playerDirection = false;
            myAnimator.SetBool("Idle", false);
            myAnimator.SetBool("Move", true);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidbody.velocity = new Vector2(move * playerSpeed, myRigidbody.velocity.y);
            playerDirection = true;
            myAnimator.SetBool("Idle", false);
            myAnimator.SetBool("Move", true);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            myAnimator.SetBool("Move", false);
            myAnimator.SetBool("Idle", true);
        }

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            myRigidbody.AddForce(transform.up * jumpSpeed * 100);
            canJump = false;
        }

        //공격
        if (Input.GetKey(KeyCode.A) && attackTimer > myStats.attackSpeed && SceneManager.GetActiveScene().name.Equals("Dungeon"))
        {
            //myStats.weapon.transform.localPosition = new Vector3(0.057f, -0.106f, 0);
            if (playerDirection)
            {
                myStats.weapon.transform.Rotate(new Vector3(180, 0, 90f));
            }
            else if(!playerDirection)
            {
                myStats.weapon.transform.Rotate(new Vector3(-180, 0, -90f));
            }
            Attack();
            attackTimer = 0f;
        }
        //공격 키 마을에서 활성화
        else if(Input.GetKey(KeyCode.A) && SceneManager.GetActiveScene().name.Equals("Main") && potal)
        {
            GameManager.m_instanceGM.dungeonStage.SetActive(true);
        }

        //스페셜 스킬
        if(Input.GetKeyDown(KeyCode.F) && specialTime == 100 && SceneManager.GetActiveScene().name.Equals("Dungeon"))
        {
            GameManager.m_instanceGM.playerSpecialSkill = true;
        }
        
    }
    public void Attack()
    {
        myAnimator.SetTrigger("Attack");
        if (weapon.monsterList.Count > 0)
        {
            for(int i = 0; i < weapon.monsterList.Count; i++)
            {
                int damage = (int) Random.Range((myStats.damage - (myStats.damage * 0.2f)), (myStats.damage + (myStats.damage * 0.2f)));
                weapon.monsterList[i].OnDamageHit(damage);
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
}
