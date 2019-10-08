using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMQueue : MonoBehaviour
{
    int MaxCount;
    //출력 위치
    int front = 0;

    //입력 위치
    int rear = 0;
    public GameObject[] Queue;

    public GMQueue(int length)
    {
        MaxCount = length;
        Queue = new GameObject[length];
    }

    //입력
    public void add(GameObject gameObject)
    {
        if((rear+1) % MaxCount == front)
        {
            return;
        }
        Queue[rear++] = gameObject;
        Logic();
    } 

    //출력
    public GameObject Enqueue()
    {
        if (front == rear)
        {
            return null;
        }
        GameObject returnObj = Queue[front];
        Queue[front++] = null;
        Logic();
        return returnObj;
    }

    public int Size()
    {
        return Queue.Length;
    }

    public void Logic()
    {
        if(front >= MaxCount)
        {
            front = 0;
        }
        if(rear >= MaxCount)
        {
            rear = 0;
        }
    }
}
