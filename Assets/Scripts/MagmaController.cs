using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaController : MonoBehaviour
{

    private PlayerController playerController;

    private float inMagmaTimer = 0f;
    private float timeProcessed = 0f;
    public float timeStep = 0.5f;
    public int dmg = 5;
    private bool inMagma = false;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (inMagma)
        {
            inMagmaTimer += Time.deltaTime;

            if (inMagmaTimer >= timeProcessed + timeStep)
            {
                playerController.ChangeHealthPoints(-dmg);
                timeProcessed += timeStep;
            }
        }
        
    }


    
    private void OnTriggerEnter2D(Collider2D other)
    {
        inMagma = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inMagma = false;
        inMagmaTimer = 0f;
        timeProcessed = 0f;
        
    }
}
