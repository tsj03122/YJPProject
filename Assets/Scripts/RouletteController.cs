
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RouletteController : MonoBehaviour
{
    public float rotSpeed = 0;

    //보상텍스트
    public Text ClearText;
    public bool compensation = false;
    int numb = 0;

    int stop_num = 0;

    public Text num1;
    public Text num2;
    public Text num3;
    public Text num4;
    public Text num5;
    public Text num6;
    public Text num7;
    public Text num8;
    public Text text;
    public Text Bonus_text;
    
    public int Clear_bonus;

    private void Start()
    {
        num1.text = Random.Range(1f, 3f).ToString("N1");
        num2.text = Random.Range(1f, 3f).ToString("N1");
        num3.text = Random.Range(1f, 3f).ToString("N1");
        num4.text = Random.Range(1f, 3f).ToString("N1");
        num5.text = Random.Range(1f, 3f).ToString("N1");
        num6.text = Random.Range(1f, 3f).ToString("N1");
        num7.text = Random.Range(1f, 3f).ToString("N1");
        if (SceneManager.GetActiveScene().name == "Dungeon")
        {
            Clear_bonus = 500;
        }


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

                switch(Clear_bonus)
                {
                    case 500:
                        if (text.text != "해금1")
                        {
                            ClearText.text = (Clear_bonus * float.Parse(text.text)).ToString();

                            if (float.Parse(ClearText.text) > Clear_bonus)
                            {
                                text.GetComponent<Text>().enabled = true;
                                ClearText.GetComponent<Text>().enabled = true;
                                Bonus_text.GetComponent<Text>().enabled = true;
                                Bonus_text.text = "*   " + Clear_bonus.ToString();
                                village.Time_num = int.Parse(ClearText.text);
                                Debug.Log(village.Time_num);
                            }

                        }
                        else if (text.text == "해금1")
                        {
                            text.GetComponent<Text>().enabled = true;
                            ClearText.GetComponent<Text>().enabled = true;
                            ClearText.text = "해금1 + " + Clear_bonus.ToString();
                        }
                        break;

                    case 1000:
                        if (text.text != "해금2")
                        {
                            ClearText.text = (Clear_bonus * float.Parse(text.text)).ToString();

                            if (float.Parse(ClearText.text) > Clear_bonus)
                            {
                                text.GetComponent<Text>().enabled = true;
                                ClearText.GetComponent<Text>().enabled = true;
                                Bonus_text.GetComponent<Text>().enabled = true;
                                Bonus_text.text = "*   " + Clear_bonus.ToString();
                                village.Time_num = int.Parse(ClearText.text);
                            }

                        }
                        else if (text.text == "해금2")
                        {
                            text.GetComponent<Text>().enabled = true;
                            ClearText.GetComponent<Text>().enabled = true;
                            ClearText.text = "해금2 + " + Clear_bonus.ToString();
                        }
                        break;

                    case 1500:
                        if (text.text != "해금3")
                        {
                            ClearText.text = (Clear_bonus * float.Parse(text.text)).ToString();

                            if (float.Parse(ClearText.text) > Clear_bonus)
                            {
                                text.GetComponent<Text>().enabled = true;
                                ClearText.GetComponent<Text>().enabled = true;
                                Bonus_text.GetComponent<Text>().enabled = true;
                                Bonus_text.text = "*   " + Clear_bonus.ToString();
                                village.Time_num = int.Parse(ClearText.text);
                            }

                        }
                        else if (text.text == "해금3")
                        {
                            text.GetComponent<Text>().enabled = true;
                            ClearText.GetComponent<Text>().enabled = true;
                            ClearText.text = "해금3 + " + Clear_bonus.ToString();
                        }
                        break;
                }
                

                rotSpeed = 0f;

                
            }
          
        }
        if (Input.anyKeyDown && ClearText.GetComponent<Text>().enabled == true)
        {
            
            SceneManager.LoadScene("Main");
            GameObject mainCanvas = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
            BoxCollider2D mapColider = GameObject.Find("Map").transform.GetChild(2).gameObject.GetComponent<BoxCollider2D>();
            GameManager.m_instanceGM.playerCamera.GetComponent<CameraManager>().bound = mapColider;

        }
    }
    public void RuletStart()
    {
        if (stop_num == 0)
        {
            stop_num++;
            this.rotSpeed = Random.Range(950, 1000);
            compensation = true;
        }

    }
}