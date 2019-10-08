
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteController : MonoBehaviour
{
    public float rotSpeed = 0;
    public GameObject btn;
    //보상텍스트
    public Text ClearText;
    public bool compensation = false;
    int numb = 0;

   public Text num1;
   public Text num2;
   public Text num3;
   public Text num4;
   public Text num5;
   public Text num6;
   public Text num7;
   public Text num8;
    public Text text;

    private void Start()
    {
        num1.text =Random.Range(1f, 3f).ToString("N1");
        num2.text =Random.Range(1f, 3f).ToString("N1");
        num3.text =Random.Range(1f, 3f).ToString("N1");
        num4.text =Random.Range(1f, 3f).ToString("N1");
        num5.text =Random.Range(1f, 3f).ToString("N1");
        num6.text =Random.Range(1f, 3f).ToString("N1");
        num7.text =Random.Range(1f, 3f).ToString("N1");
    }
    void Update()
    {
        if (compensation)
        {
            rotSpeed *= 0.96f;
            transform.Rotate(0, 0, this.rotSpeed);

            if (this.rotSpeed <= 0.1)
            {
                if (0 <= transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 45) numb = 1;
                else if (45 <= transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 90) numb = 2;
                else if (90 <= transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 135) numb = 3;
                else if (135 <= transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 180) numb = 4;
                else if (180 <= transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 225) numb = 5;
                else if (225 <= transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 270) numb = 6;
                else if (270 <= transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 315) numb = 7;
                else if (315 <= transform.rotation.eulerAngles.z && transform.rotation.eulerAngles.z < 360) numb = 8;
                switch (numb)
                {
                    case 1:
                        text.text = num2.text;
                        break;
                    case 2:
                        text.text = num3.text;
                        break;
                    case 3:
                        text.text = num4.text;
                        break;
                    case 4:
                        text.text = num5.text;
                        break;
                    case 5:
                        text.text = num6.text;
                        break;
                    case 6:
                        text.text = num7.text;
                        break;
                    case 7:
                        text.text = num8.text;
                        break;
                    case 8:
                        text.text = num1.text;
                        break;
                }
                compensation = false;
                Debug.Log(float.Parse(text.text));
                ClearText.text = (500 * float.Parse(text.text)).ToString();
                rotSpeed = 0f;

            }
            
        }
        
    }
    public void RuletStart()
    {
        this.rotSpeed = 1000;
        compensation = true;
    }
}