﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed = 1000f;
    public Vector3 targetPosition;

    public BoxCollider2D bound;

    [SerializeField]
    private Vector3 minBound;
    [SerializeField]
    private Vector3 maxBound;

    [SerializeField]
    private float halfWidth;
    [SerializeField]
    private float halfHeight;

    public Camera theCamera;

    public void CameraSetting()
    {
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if(target.gameObject != null)
        {
            if(theCamera.orthographicSize < target.transform.position.y)
            {
                halfHeight = target.transform.position.y;
            }

            targetPosition.Set(target.transform.position.x, target.transform.position.y + 2, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);
            this.transform.position = new Vector3(clampedX, this.transform.position.y, this.transform.position.z);
        }
    }
}
