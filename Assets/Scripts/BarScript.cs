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

    void Awake()
    {
        GameManager.m_instanceGM.uiManager.hpSlider = hpBar;
        GameManager.m_instanceGM.uiManager.hpText = hpText;
        GameManager.m_instanceGM.uiManager.spSlider = spBar;
        GameManager.m_instanceGM.uiManager.spText = spText;
    }
}
