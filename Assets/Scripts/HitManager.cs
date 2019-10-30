using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public GameObject[] skillList;
    [SerializeField]
    private GMQueue skillCycle;
    private GMQueue skill2Cycle;

    public List<GameObject> abcd;
    void Awake()
    {
        GameManager.m_instanceGM.hitManager = this;
        skillCycle = new GMQueue(50);
        skill2Cycle = new GMQueue(50);
        SkillChange("Sword");
    }
    // Update is called once per frame
    public GMQueue getskillCycleGmQueue()
    {
        return skillCycle;
    }

    public GMQueue getskill2CycleGmQueue()
    {
        return skill2Cycle;
    }

    public void SkillChange(string WeaponType)
    {
        switch (WeaponType)
        {
            case "Sword":
                for (int i = 0; i < 50; i++)
                {
                    skillCycle.add(skillList[0].transform.GetChild(i).gameObject);
                    skill2Cycle.add(skillList[1].transform.GetChild(i).gameObject);
                }
                break;
            case "Arrow":
                break;
            case "Stick":
                break;
        }
    }
}
