using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileController : MonoBehaviour
{

    public int damage = 50;
    private SpecialButtonController specialButtonController;
    private PlayerController pc;
    public GameObject damageDisplayPrefab;
    public AudioClip hitSound;
    public AudioClip reflectSound;
    
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        damage = (int) (damage * (1 - (pc.GetDifficulty()* 0.599f)));
        specialButtonController = GameObject.Find("SpecialAttackButton").GetComponent<SpecialButtonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.name.Contains("Enemy"))
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector3 hitPosition = transform.position;
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();
            if (enemyController.isImmune)
            {
                return;
            }
            specialButtonController.IncrementCounter();
            enemyController.ChangeHealthPoints(-damage);
            Destroy(gameObject);
            
            // display dmg at hitPosition
            GameObject damageDisplay = Instantiate(damageDisplayPrefab, hitPosition, Quaternion.identity);
            if (enemyController.IsReflecting())
            {
                damageDisplay.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "REFLECT";
                pc.PlayAudioOneShot(reflectSound);
            }
            else
            {
                damageDisplay.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "" + damage;
                pc.PlayAudioOneShot(hitSound);
            }
            Destroy(damageDisplay, 1f);
            
            // Display info about enemy for short period
            pc.DisplayEnemyInfo(enemyController);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }
}
