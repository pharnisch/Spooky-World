using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    private bool active = true;
    public float pauseTime;

    private float pauseTimer = 0;

    public float durationTime;

    private float durationTimer = 0;

    private GameObject beam;
        // Start is called before the first frame update
    void Start()
    {
        beam = GameObject.Find("EnemyProjectileBeamEXTREME");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (active)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer >= durationTime)
            {
                // pause
                durationTimer = 0;
                active = false;
                beam.SetActive(active);
            }
        }
        else
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseTime)
            {
                // activate
                pauseTimer = 0;
                active = true;
                beam.SetActive(active);
            }
        }
    }
}
