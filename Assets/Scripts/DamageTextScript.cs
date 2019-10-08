using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextScript : MonoBehaviour
{
    public float moveSpeed;
    //이동 속도인거같고

    public float destroyTime;
    //사라지는 순간

    public Text text;
    public Color textColor;
    private Vector3 vector;
    private Vector3 vector2;
    public GameObject target;
    public float yValue;

    void Start()
    {
        text = GetComponent<Text>();
        textColor = text.color;
    }
    // Update is called once per frame
    void Update()
    {
        yValue += 1;
        vector = Camera.main.WorldToScreenPoint(target.transform.position);
        text.transform.position = new Vector3(vector.x, vector.y + yValue, vector.z);
        destroyTime -= Time.deltaTime;
        textColor.a = textColor.a - 0.004f;
        text.color = textColor;
        if (destroyTime <= 0)
        {
            yValue = 0f;
            text.gameObject.SetActive(false);
        }
    }
}
