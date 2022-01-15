using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudioController : MonoBehaviour
{
    private bool muted = false;
    public Sprite mutedSprite;

    public Sprite unmutedSprite;
    private Image image;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        muted = PlayerPrefs.GetInt("muted")==1;
        UpdateProcedure();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleAudio()
    {
        muted = !muted;
        PlayerPrefs.SetInt("muted", muted?1:0);
        PlayerPrefs.Save();
        UpdateProcedure();
    }

    private void UpdateProcedure()
    {
        if (muted)
        {
            image.sprite = mutedSprite;
        }
        else
        {
            image.sprite = unmutedSprite;
        }
        audioSource.mute = muted;
    }
}
