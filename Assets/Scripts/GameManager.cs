using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instanceGM { get; private set; } = null;
    public PlayerControl playerControl;
    public GameObject dungeonStage;
    public bool playerDie = false;
    public bool playerSpecialSkill = false;
    float timer = 0;
    
    void Awake()
    {
        if (m_instanceGM == null)
        {
            m_instanceGM = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update()
    {
        if (playerSpecialSkill)
        {
            timer += Time.deltaTime;
            if(timer > 5)
            {
                playerSpecialSkill = false;
                timer = 0f;
            }
        }

        if (playerControl != null)
        {
            if (playerDie)
            {
                playerControl.gameObject.SetActive(false);
            }
        }    
    }

    public void PlayerEquipmentSetting()
    {
        string idUsers = DataSetting.m_instraintDS.id;
        string weaponItemName = DataSetting.m_instraintDS.selsql(
            "select * from testdbminiproject.PlayerEquipment where idUsers = '" + idUsers + "';").Rows[0].ItemArray[1].ToString();
        string armorItemName = DataSetting.m_instraintDS.selsql(
            "select * from testdbminiproject.PlayerEquipment where idUsers = '" + idUsers + "';").Rows[0].ItemArray[2].ToString();

        //무기
        playerControl.myStats.weaponItem.itemName = weaponItemName;
        DataTable dt = DataSetting.m_instraintDS.selsql(
            "SELECT* FROM testdbminiproject.WeaponItem where WeaponName = '" + weaponItemName + "';");
        playerControl.myStats.weaponItem.itemType = dt.Rows[0].ItemArray[1].ToString();
        playerControl.myStats.weaponItem.damage = float.Parse(dt.Rows[0].ItemArray[2].ToString());
        playerControl.myStats.weaponItem.attackSpeed = float.Parse(dt.Rows[0].ItemArray[3].ToString());
        playerControl.myStats.weaponItem.attackSize = float.Parse(dt.Rows[0].ItemArray[4].ToString());

        //방어구
        playerControl.myStats.ArmorItem.itemName = armorItemName;
        dt = DataSetting.m_instraintDS.selsql(
            "SELECT* FROM testdbminiproject.ArmorItem where ArmorItem = '" + armorItemName + "';");
        playerControl.myStats.ArmorItem.itemHP = float.Parse(dt.Rows[0].ItemArray[1].ToString());
        playerControl.myStats.ChangeStats();
    }
}
