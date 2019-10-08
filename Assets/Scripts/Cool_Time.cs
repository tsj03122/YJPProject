using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cool_Time : MonoBehaviour
{
    public Image skillFilter;
    public float coolTime;
    private float currentCoolTime; //남은 쿨타임을 추적 할 변수s
    private bool canUseSkill = true; //스킬을 사용할 수 있는지 확인하는 변수
    public PlayerStats myStats;
    public PlayerControl myControl;

    private void Awake()
    {
        
       

       
        {
            skillFilter.fillAmount = 0; //처음에 스킬 버튼을 가리지 않음
        }

    }

    public void Update()
    {
        myStats = FindObjectOfType<PlayerStats>();
        switch (gameObject.name)
        {
            case "1":
                if (Input.GetKey(KeyCode.A))
                {
                    if (canUseSkill)
                    {
                        coolTime = myStats.attackSpeed;
                        Debug.Log("ATT");
                    }
                    else
                        Debug.Log("fail");
                    UseSkill();
                }
                break;

            case "2":
                if (Input.GetKey(KeyCode.S))
                {
                    if (canUseSkill)
                    {
                        coolTime = 2;
                        Debug.Log("ATT");
                    }
                    else
                        Debug.Log("fail");
                    UseSkill();
                }
                break;
            case "3":
                if (Input.GetKey(KeyCode.D))
                {
                    if (canUseSkill)
                    {
                        coolTime = 3;
                        Debug.Log("ATT");
                    }
                    else
                        Debug.Log("fail");
                    UseSkill();
                }
                break;
            
        }
    }
      
    public void UseSkill()
    {
       
        if (canUseSkill)
        {
           
           
            Debug.Log("Use Skill");
            skillFilter.fillAmount = 1; //스킬 버튼을 가림
            StartCoroutine("Cooltime");

            currentCoolTime = coolTime;
            

            StartCoroutine("CoolTimeCounter");

            canUseSkill = false; //스킬을 사용하면 사용할 수 없는 상태로 바꿈
        }
        else
        {
            Debug.Log("아직 스킬을 사용할 수 없습니다.");
        }
    }

    IEnumerator Cooltime()
    {
        while (skillFilter.fillAmount > 0)
        {
            skillFilter.fillAmount -= 1 * Time.deltaTime / coolTime;

            yield return null;
        }

        canUseSkill = true; //스킬 쿨타임이 끝나면 스킬을 사용할 수 있는 상태로 바꿈
        
        yield break;
    }

    //남은 쿨타임을 계산할 코르틴을 만들어줍니다.
    IEnumerator CoolTimeCounter()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            
            
        }

        yield break;
    }
}
