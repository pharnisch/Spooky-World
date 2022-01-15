using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAttackUpController : MonoBehaviour
{

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
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collisionGameObject = other.gameObject;
        if (collisionGameObject.name == "Player")
        {
            collisionGameObject.GetComponent<PlayerController>().MainAttackUp();
            Destroy(gameObject);
        }
    }
}
