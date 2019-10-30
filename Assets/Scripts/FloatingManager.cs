using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingManager : MonoBehaviour
{
    [SerializeField]
    private GMQueue GmQueue;
    [SerializeField]
    private GameObject canvas;

    void Awake()
    {
        GameManager.m_instanceGM.floatingManager = this;
        GmQueue = new GMQueue(50);
        for (int i = 0; i < 50; i++)
        {
            GmQueue.add(canvas.transform.GetChild(i).gameObject);
        }
    }
    // Update is called once per frame
    public GMQueue getGmQueue()
    {
        return GmQueue;
    }
}
