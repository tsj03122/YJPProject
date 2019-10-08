using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Data : MonoBehaviour
{
    public Slider HPGage;
    public Slider SPGage;

    public float HP = 100f;
    public float SP = 100f;
    


    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.H)) {
            HPGage.value -= 10f;
            SPGage.value += 10f;
        }
    }

    public void SetHP() {
      
        HPGage.maxValue = HP;
        HPGage.value = HP;
    }
    public void SetSP()
    {

        SPGage.maxValue = SP;
        SPGage.value = 0f;
        
    }

}
