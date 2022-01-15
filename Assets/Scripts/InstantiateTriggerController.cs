using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTriggerController : MonoBehaviour
{
    public GameObject obj;

    public float x;

    public float y;
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
            Instantiate(obj, new Vector3(x, y, 0f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}