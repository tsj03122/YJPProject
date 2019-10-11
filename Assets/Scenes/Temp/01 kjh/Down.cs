using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : MonoBehaviour
{
    private PlatformEffector2D effetor;
    public float waitTime;
    public float ReversTime;
   
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
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            waitTime = 0.4f;
            ReversTime = 0.1f;

        }
       
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (waitTime <= 0.1f)
            {
                effetor.rotationalOffset = 180f;
                waitTime = 0.4f;
                
            }
            else { waitTime -= waitTime*Time.deltaTime; } 
        }
        if (Input.GetKey(KeyCode.Space)||ReversTime>=0.135f )
        {
            effetor.rotationalOffset = 0f;
        }
    }
}
