using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    public Slider hpBar;
    public Slider spBar;
    public Text hpText;
    public Text spText;

    void Start()
    {
        GameManager.m_instanceGM.playerControl.myStats.hpBar = hpBar;
        GameManager.m_instanceGM.playerControl.myStats.hpText = hpText;
        GameManager.m_instanceGM.playerControl.myStats.spBar = spBar;
        GameManager.m_instanceGM.playerControl.myStats.spText = spText;
    }
}
