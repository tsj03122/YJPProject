using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    
    public FloatingManager fm;
    public HitManager hm;
    public Vector3 monsterCanvers;
    public float specialTimer;
    public Vector3 monsterPositionCenter;

    //몬스터 기본 스탯
    public float hp = 100f;                    //체력
    public float speed = 1.0f;              //이동속도
    public int damage;                      //공격력
    public float coolTime = 4.0f;           //공격속도
    public float attckTiming = 3.0f;        //공격 타이밍
    public float visualRange = 2.0f;        //몬스터 시야
    public bool isDead = false;             //사망 판정

    //이동관련
    Rigidbody2D rigid;                      //자신의 리지드바디
    private int nextMove;                   //대기상태에서 하는일(좌,우,정지)을 담는 변수 !!임의 변경 금지
    private float nextTinkTime;             //Invoke 랜덤 실행변수
    private int vision = 1;                 //몬스터 시선

    //추적 관련
    private float distance;                 //플레이어와 거리
    private bool attackFlag = false;        //공격상태유무
    private bool soleFlag = true;           //추적,대기상태유무
    private GameObject searchTarget;        //추적 대상
    private Transform target_position;      //플레이어 위치
    private Vector2 direction;              //플레이어 방향

    //공격관련
    private GameObject attackArea_obj;      //공격판정 범위 변수
    [SerializeField]
    public GameObject attackTarget;         //공격 대상

    //기타
    private Animator myAnimator;            //내 애니메이션
    private float monsterRadius = 0.5f;     //몬스터 반지름


    //피격 이펙트
    public GameObject SwordSkillEffect; //스킬 이펙트
    public GameObject SwordSkill2Effect; //스킬2 이펙트
    public BoxCollider2D monsterCollider2D; // 몬스터콜라이더2d

    // Start is called before the first frame update
    void Start()
    {
        //자신 리지드 컴포넌트
        rigid = GetComponent<Rigidbody2D>();
        monsterCollider2D = GetComponent<BoxCollider2D>();
        Think();                                //랜덤행동함수

        attackArea_obj = transform.GetChild(0).gameObject;

        // Player의 현재 위치를 받아오는 Object
        searchTarget = GameObject.Find("Player");

        myAnimator = GetComponent<Animator>();
        attackFlag = false;
        StartCoroutine("Attack");

        fm = GameManager.m_instanceGM.floatingManager;
        hm = GameManager.m_instanceGM.hitManager;

    }
    private void FixedUpdate()
    {
        //if (GameManager.m_instanceGM.playerSpecialSkill)
        //{
        //    specialTimer += Time.deltaTime;
        //    if (specialTimer >= 5)
        //    {
        //        GameManager.m_instanceGM.playerSpecialSkill = false;
        //        specialTimer = 0f;
        //    }
        //    return;
        //}

        monsterPositionCenter = transform.position;

        //사망체크
        if (hp <= 0 || isDead)
        {
            isDead = true;
            CancelInvoke();
            StopCoroutine("Attack");
            myAnimator.SetBool("Attack", false);
            myAnimator.SetBool("Move", false);
            myAnimator.SetBool("Die", true);
            StartCoroutine("End");

        }

        if (!isDead)
        {
            //몬스터 시선
            if (nextMove != 0)
            {
                vision = nextMove;
            }

            //공격명령
            if (attackTarget != null && attackFlag == false)
            {
                //공격플래그
                attackFlag = true;
                Debug.Log("몬스터_공격 플래그 :" + attackFlag);
            }

            //플레이어 인식_Distance
            target_position = searchTarget.transform;
            // Player와 객체 간의 거리 계산
            distance = Vector3.Distance(target_position.position, transform.position) - monsterRadius;
            // Player의 위치와 이 객체의 위치를 빼고 단위 벡터화 한다.
            direction = (target_position.position - this.transform.position);

            if (distance <= visualRange && !attackFlag && !isDead)
            {
                soleFlag = true;        //시야 이탈 플래그

                //Debug.Log("몬스터_거리 :" + distance);
                //Debug.Log("몬스터_플레이어 확인");
                CancelInvoke();

                //Debug.Log("몬스터_거리벡터값 : " + direction);
                if (direction.x > 0)
                {
                    nextMove = 1;
                    //Debug.Log("몬스터_추적오른쪽 :" + nextMove);
                }
                else if (direction.x < 0)
                {
                    nextMove = -1;
                    //Debug.Log("몬스터_추적왼쪽 :" + nextMove);
                }
            }

            if (distance > visualRange)
            {
                //Debug.Log("몬스터_플레이어 탐색중");

                if (soleFlag && !attackFlag)
                {
                    soleFlag = false;
                    CancelInvoke();
                    Invoke("Think", 5);
                }
            }


            //이동

            if (nextMove != 0)
            {
                myAnimator.SetBool("Move", true);
                myAnimator.SetBool("Idle", false);
                myAnimator.SetBool("Attack", false);
                transform.localScale = new Vector3(-vision, 1f, 1f);     //Flip
                rigid.velocity = new Vector2(nextMove * speed, rigid.velocity.y);
            }
            else
            {
                myAnimator.SetBool("Move", false);
                myAnimator.SetBool("Idle", true);
                myAnimator.SetBool("Attack", false);
            }

            //플랫폼 체크
            Vector2 buttomVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);

            //Debug.DrawRay(buttomVec, Vector3.down, new Color(1, 0, 0));

            RaycastHit2D rayButtom = Physics2D.Raycast(buttomVec, Vector2.down, 1.0f, LayerMask.GetMask("Foreground"));
            if (rayButtom.collider == null)
            {
                //Debug.Log("몬스터_방향전환");
                nextMove *= -1;
                CancelInvoke();
                Invoke("Think", 5);
            }
        }


    }

    //랜덤 행동 명령 함수
    void Think()
    {
        nextMove = Random.Range(-1, 2);
        nextTinkTime = Random.Range(2f, 6f);
        //Debug.Log("몬스터_nextMove :" + nextMove);
        Invoke("Think", nextTinkTime);
    }

    //코루틴 공격함수(공격모션시작, 실제 공격타이밍)
    IEnumerator Attack()
    {
        while (true)
        {
            if(attackTarget != null)
            {
                float temp = coolTime - attckTiming;

                //랜덤이동 함수 정지
                CancelInvoke();
                //nextMove = 0 으로 고정(정지상태)
                nextMove = 0;
                //Debug.Log("코루틴_공격 시작모션");
                myAnimator.SetBool("Move", false);
                myAnimator.SetBool("Attack", true);
                myAnimator.SetBool("Idle", false);
                yield return new WaitForSeconds(attckTiming);
                //Debug.Log("거리 : " + distance + ", 공격사거리 : " + attackRange);
                //Debug.Log("시선 : " + vision + ", 방향 : " + direction);

                //Debug.Log("공격시도");
                if (attackTarget != null)
                {
                    Debug.Log("몬스터 공격_공격력 : " + damage);
                    attackTarget.GetComponent<PlayerStats>().onDamagedHit(damage);
                }

                myAnimator.SetBool("Move", false);
                myAnimator.SetBool("Attack", false);
                myAnimator.SetBool("Idle", false);
                Invoke("Think", nextTinkTime);
                //Debug.Log("몬스터_공격 타이밍! :" + temp);
                yield return new WaitForSeconds(temp);
                //공격플래그 해제
                attackFlag = false;
                Debug.Log("몬스터_공격 플래그 :" + attackFlag);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator End()
    {
        //nextMove = 0 으로 고정(정지상태)
        nextMove = 0;

        yield return new WaitForSeconds(0.9f);

        //몬스터 사망 애니메이션 체크
        while (myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f)
        {

        }

        gameObject.SetActive(false);

    }


    public void OnDamageHit(float damage, int AttackWay)
    {
        GameManager.m_instanceGM.playerControl.myStats.SetSP(3);
        monsterCanvers = Camera.main.WorldToScreenPoint(this.transform.position);
        hp -= damage;
        GMQueue gmQueue = fm.getGmQueue();
        GMQueue effectSkillGmQueue = hm.getskillCycleGmQueue();
        GMQueue effectSkill2GmQueue = hm.getskill2CycleGmQueue();
        GameObject textObj = gmQueue.Enqueue();
        DamageTextScript textObjText = textObj.GetComponent<DamageTextScript>();
        textObjText.target = this.gameObject;
        textObjText.text.transform.position = monsterCanvers;
        textObjText.text.text = damage.ToString();
        textObjText.textColor.a = 1;
        textObjText.destroyTime = 2f;
        textObjText.text.gameObject.SetActive(true);
        gmQueue.add(textObj);

        //공격 방법에 따른 (스킬, 스킬2, 평타)
        switch (AttackWay)
        {
            //평타
            case 0:
                break;
            //스킬
            case 1:
                GameObject effectSkill = effectSkillGmQueue.Enqueue();
                SwordSkillEffect = effectSkill;
                effectSkill.transform.position = transform.position;
                effectSkill.SetActive(true);
                effectSkillGmQueue.add(effectSkill);
                break;
            //스킬2
            case 2:
                GameObject effectSkill2 = effectSkill2GmQueue.Enqueue();
                SwordSkill2Effect = effectSkill2;
                effectSkill2.transform.position = this.transform.position;
                effectSkill2.SetActive(true);
                effectSkill2GmQueue.add(effectSkill2);
                break;
        }

    }


}
