using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour
{
    public Sprite pauseSprite;

    public Sprite playSprite;

    private Image image;
    private bool pause = false;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePlayPause()
    {
        pause = !pause;
        if (pause)
        {
            image.sprite = playSprite;
            Time.timeScale = 0;
            audioSource.Pause();
        }
        else
        {
            image.sprite = pauseSprite;
            Time.timeScale = 1f;
            audioSource.UnPause();
        }
    }
}
