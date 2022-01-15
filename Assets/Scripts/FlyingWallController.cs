using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingWallController : MonoBehaviour
{
    public float speed = 2.5f;
    public Vector3 direction;
    private PlayerController pc;
    public int damage = 25;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        direction = direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + speed * Time.deltaTime * direction);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            direction = direction * (-1);
            
        }

        if (other.gameObject.CompareTag("Player"))
        {
            pc.ChangeHealthPoints(-damage);
        }

    }
}
