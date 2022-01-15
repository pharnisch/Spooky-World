using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public bool clockwise = true;
    public float duration = 10f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        float partialTime = timer % duration;
        float degree = partialTime / duration * 360;
        if (clockwise)
        {
            degree -= -1;
        }
        transform.localRotation = Quaternion.AngleAxis(degree, Vector3.back);
    }
}
