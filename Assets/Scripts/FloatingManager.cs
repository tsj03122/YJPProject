using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] textList;
    [SerializeField]
    private GMQueue GmQueue;
    [SerializeField]
    private GameObject canvas;

    void Awake()
    {
        GmQueue = new GMQueue(10);
        for (int i = 0; i < textList.Length; i++)
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
