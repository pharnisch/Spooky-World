using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafController : MonoBehaviour
{
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
            bool success = GameObject.Find("Player").GetComponent<PlayerController>().Hide();
            if (success)
            {
                Destroy(gameObject);
            }
        }
    }
}
