using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider hpSlider;
    public Slider spSlider;
    public Text hpText;
    public Text spText;

    void Start()
    {
        GameManager.m_instanceGM.uiManager = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void HpSpSetting()
    {
        hpSlider.maxValue = GameManager.m_instanceGM.playerControl.playerStats.hp;
        hpSlider.value = GameManager.m_instanceGM.playerControl.playerStats.hp;
        hpText.text = "HP : " + hpSlider.maxValue;
        spSlider.value = 0;
        spText.text = "SP : " + spSlider.value;
    }

    public void HpChange()
    {
        hpSlider.value = GameManager.m_instanceGM.playerControl.playerStats.hp;
        hpText.text = "HP : " + hpSlider.maxValue;
    }

    public void SpChange()
    {
        spText.text = "SP : " + spSlider.value;
    }
}
