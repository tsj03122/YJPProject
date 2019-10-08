using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public Slider hpBar;
    public Slider spBar;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.m_instanceGM.playerControl.myStats.hpBar = hpBar;
        GameManager.m_instanceGM.playerControl.myStats.spBar = spBar;
        GameManager.m_instanceGM.playerControl.myStats.hpBar.maxValue = GameManager.m_instanceGM.playerControl.myStats.hp;
        GameManager.m_instanceGM.playerControl.myStats.hpBar.value = GameManager.m_instanceGM.playerControl.myStats.hp;

    }
}
