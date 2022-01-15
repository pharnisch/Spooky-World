using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private PlayerController _playerController;
    public KeyController.KeyColor keyColor;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey(gameObject.name))
        {
            Destroy(gameObject);
        }
        
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
        // color the key sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = KeyController.GetColor(keyColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (_playerController.RemoveKey(keyColor))
            {
                // persist information if black door was opened
                if (keyColor == KeyController.KeyColor.Black)
                {
                    String doorName = gameObject.name;
                    print(doorName);
                    PlayerPrefs.SetInt(doorName, 1); // 1=opened, 0=closed
                    PlayerPrefs.Save();
                }

                
                Destroy(gameObject);
            }
        }
    }
}
