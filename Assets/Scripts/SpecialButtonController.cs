using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialButtonController : MonoBehaviour
{
    private PlayerController _playerController;
    private int hitCounter = 0;

    private Button specialAttackButton;
    private Text specialAttackText;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
        specialAttackButton = GetComponent<Button>();
        specialAttackText = transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpecialAttack()
    {
        _playerController.SpecialAttack(hitCounter/10);
        
        specialAttackButton.interactable = false;
        hitCounter = 0;
        specialAttackText.text = "";
    }

    public void IncrementCounter()
    {
        hitCounter++;
        if (hitCounter == 10)
        {
            specialAttackButton.interactable = true;
        }

        if (hitCounter >= 10)
        {
            specialAttackText.text = "" + ((int) hitCounter/10);
        }
    }
}
