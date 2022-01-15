using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Boss1Controller : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootTime = 1f;
    private float shootTimer = 0f;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        shootTimer = shootTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Vector3 direction = player.transform.position - transform.position;
            Vector3 normalizedDirection = direction.normalized;
            Vector3 scaledDirection = normalizedDirection * 1f;
            GameObject projectile = Instantiate(projectilePrefab, transform.position + scaledDirection, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(normalizedDirection * 100f);
            //Destroy(projectile, 3f);
            
            shootTimer = shootTime;
        }
    }
}
