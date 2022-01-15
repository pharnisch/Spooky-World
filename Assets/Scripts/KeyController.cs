using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class KeyController : MonoBehaviour
{
    private PlayerController _playerController;
    public KeyColor keyColor;

    public enum KeyColor
    {
        Magenta,
        Yellow,
        Cyan,
        Red,
        Blue,
        Green,
        Black
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        // only one black key per level possible
        if (keyColor == KeyColor.Black)
        {
            String sceneName = SceneManager.GetActiveScene().name;
            if (PlayerPrefs.HasKey(sceneName))
            {
                Destroy(gameObject);
            }
        }
        
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
        // color the key sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = GetColor(keyColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            String sceneName = SceneManager.GetActiveScene().name;
            // only one black key per level possible
            if (keyColor == KeyColor.Black)
            {
                if (PlayerPrefs.HasKey(sceneName))
                {
                    return;
                }
            }
            bool successfull = _playerController.AddKey(keyColor);
            if (successfull)
            {
                // persist info about key
                if (keyColor == KeyColor.Black)
                {
                    
                    PlayerPrefs.SetInt(sceneName, 1);
                    PlayerPrefs.Save();
                }
                Destroy(this.gameObject);
            }
        }
    }

    public static Color GetColor(KeyColor keyColor)
    {
        Color color;
        switch (keyColor)
        {
            case KeyColor.Magenta:
                color = Color.magenta;
                break;
            case KeyColor.Yellow:
                color = Color.yellow;
                break;
            case KeyColor.Cyan:
                color = Color.cyan;
                break;
            case KeyColor.Red:
                color = Color.red;
                break;
            case KeyColor.Blue:
                color = Color.blue;
                break;
            case KeyColor.Green:
                color = Color.green;
                break;
            case KeyColor.Black:
                color = Color.black;
                break;
            default:
                color = Color.white;
                break;
        }
        Color lighterVersionOfColor = (color + Color.white)/2;
        return lighterVersionOfColor;
    }
}
