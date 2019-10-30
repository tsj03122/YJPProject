using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : MonoBehaviour
{
    private PlatformEffector2D effetor;
    public float waitTime;
    public float ReversTime;
    PlayerControl playerControl;
   
    void Start()
    {
        effetor = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (effetor.rotationalOffset == 180f)
        {

            ReversTime += ReversTime*Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)&&playerControl.canJump==true)
        {
            waitTime = 0.4f;
            ReversTime = 0.1f;

        }
       
        if (Input.GetKey(KeyCode.DownArrow) && playerControl.canJump == true)
        {
            if (waitTime <= 0.1f)
            {
                effetor.rotationalOffset = 180f;
                waitTime = 0.4f;
                
            }
            else { waitTime -= waitTime*Time.deltaTime; } 
        }
        if (ReversTime>=0.155f || Input.GetKeyDown(KeyCode.Space))
        {
            effetor.rotationalOffset = 0f;
            ReversTime = 0.1f;
        }
    }
}
