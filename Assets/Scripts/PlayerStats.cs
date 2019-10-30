using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public float damage = 0f;
    public float attackSpeed = 0f;
    public float hp = 0f;
    public bool hitOn = false;

    public GameObject weapon;
    public PlayerSkill playerskill;
    public WeaponInformation weaponItem;
    public ArmorInformation ArmorItem;
    public Slider hpBar;
    public Slider spBar;
    public Text hpText;
    public Text spText;
    public SpriteRenderer mySprite;

    public void ChangeStats()
    {
        damage = weaponItem.damage;
        attackSpeed = weaponItem.attackSpeed;
        switch (weaponItem.itemType)
        {
            case "Sword":

                break;
            case "Stick":

                break;
            case "Arrow":

                break;
        }

        hp = 100 + ArmorItem.itemHP;
        SetHP(hp);
        SetSP(0);
    }

    public void SetHP(float hp)
    {
        hpBar.maxValue = hp;
        hpBar.value = hp;
        hpText.text = "HP : " + hpBar.value;
    }
    
    public void SetSP(float sp)
    {
        if(sp == 0)
        {
            spBar.value = sp;
        }
        else
        {
            spBar.value += sp;
        }
        spText.text = "SP : " + spBar.value;
    }

    public void onDamagedHit(float damage)
    {
        if (!hitOn)
        {
            hp -= damage;
            hpBar.value = hp;
            hpText.text = "HP : " + hpBar.value;
            if (hp <= 0)
            {
                GameManager.m_instanceGM.playerDie = true;
                return;
            }
            StartCoroutine("Hitinvincibility");
        }
    }

    IEnumerator Hitinvincibility()
    {
        int countTimer = 0;
        hitOn = true;
        while (countTimer < 5)
        {
            if(countTimer % 2 == 0)
            {
                mySprite.color = new Color32(255, 255, 255, 90);
            }
            else
            {
                mySprite.color = new Color32(255, 255, 255, 180);
            }
            yield return new WaitForSeconds(0.2f);
            countTimer++;
        }
        mySprite.color = new Color32(255, 255, 255, 255);
        hitOn = false;
    }
}
