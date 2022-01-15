using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpController : MonoBehaviour
{
    public float speedIncrease = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerController>().IncreaseSpeed(speedIncrease);
            Destroy(gameObject);
        }
    }
}
