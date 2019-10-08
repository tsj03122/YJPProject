using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float damage = 0f;
    public float attackSpeed = 0f;
    public float hp = 0f;
    public float Shield = 0f;
    public string skils1 = "";
    public string skils2 = "";

    public GameObject weapon;
    public WeaponInformation weaponItem;
    public ArmorInformation ArmorItem;
    public Slider hpBar;
    public Slider spBar;

    // Start is called before the first frame update
    void Start()
    {
        ChangeStats();
    }
    
    public void ChangeStats()
    {
        damage = weaponItem.damage;
        attackSpeed = weaponItem.attackSpeed;
        hp = 100 + ArmorItem.itemHP;
        skils1 = weaponItem.skils1;
        skils2 = weaponItem.skils2;
       
    }

    public void SetHP(float hp)
    {
        hpBar.maxValue = hp;
        hpBar.value = hp;
    }

    public void onDamagedHit(float damage)
    {
        hp -= damage;
        hpBar.value = hp;
        if (hp <= 0)
        {
            GameManager.m_instanceGM.playerDie = true;
            return;
        }
        
    }
}
