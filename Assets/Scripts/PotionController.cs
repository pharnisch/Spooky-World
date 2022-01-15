using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public int healthChange = 0;

    public int energyChange = 0;
    
    
    
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
        if (other.name == "Player")
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.ChangeHealthPoints(healthChange);
            playerController.ChangeEnergyPoints(energyChange);
            playerController.PlayAudioOneShot(playerController.powerUpSound);
            Destroy(gameObject);
        }
    }
}
