using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProjectileController : MonoBehaviour
{
    public int damage = 30;

    public GameObject prefab;

    private bool didNotHitYet = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.ChangeHealthPoints(-damage);
            didNotHitYet = false;
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        if (prefab.name != "Placeholder" && didNotHitYet)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
    
}
