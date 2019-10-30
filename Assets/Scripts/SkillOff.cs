using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOff : MonoBehaviour
{
    public void Off(int temp)
    {
        transform.gameObject.SetActive(false);
    }
}
