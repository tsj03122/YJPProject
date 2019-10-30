using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public GameObject skill;
    public Skill skillScript;

    public GameObject Skill2;
    public Skill skill2Script;

    public enum PlayerWeaponSkill
    {
        Sword = 0,
        Arrow = 1,
        Stick = 2
    }
}
