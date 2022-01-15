using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonController : MonoBehaviour
{

    private static int MAIN_ATTACK_COST = 5;
    public float cooldown = 1.5f;
    private float cooldownTimer = 0f;
    private PlayerController playerController;
    private Button button;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        button = gameObject.GetComponent<Button>();
    }

    private void Update()
    {
        
        
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        
        if (!button.interactable && cooldownTimer <= 0f)
        {
            button.interactable = true;
        }
        
        if (playerController.GetEnergyPoints() < 5)
        {
            button.interactable = false;
        }
    }

    public void Click()
    {
        if (button.interactable)
        {
            if (playerController.ChangeEnergyPoints(-MAIN_ATTACK_COST))
            {
                playerController.MainAttack();
                // button.interactable = false;
                // cooldownTimer = cooldown;
            }
            else
            {
                button.interactable = false;
            }

        }
    }
    
    
    
}
