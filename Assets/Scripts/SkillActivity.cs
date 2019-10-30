using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActivity : MonoBehaviour
{
    //0은 On 1은 Off
    public void SkillOnOff(int theValue)
    {
        if(theValue == 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
