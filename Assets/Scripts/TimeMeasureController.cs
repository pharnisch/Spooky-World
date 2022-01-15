using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeMeasureController : MonoBehaviour
{
    private TextMeshProUGUI text;

    private float seconds = 0f;
    private int fullSeconds = 0;

    private bool stop = true;

    private bool preload = true;

    private bool isMenu;
    // Start is called before the first frame update
    void Start()
    {
        isMenu = SceneManager.GetActiveScene().name == "Menu";
        text = GetComponent<TextMeshProUGUI>();
        if (isMenu)
        {
            stop = true;
        }
        else
        {
            text.text = "01234567m, 89s";
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (preload && !isMenu)
        {
            text.text = "0m, 0s";
        }
        
        if (!stop)
        {
            seconds += Time.deltaTime;
            if (fullSeconds < (int) seconds)
            {
                fullSeconds = (int) seconds;
                text.SetText("" + (int) (fullSeconds / 60) + "m, " + fullSeconds % 60 + "s");

            }

        }



    }

    public void StartTime()
    {
        stop = false;
        preload = false;
    }

    public void Stop()
    {
        stop = true;
    }

    public int GetSeconds()
    {
        return fullSeconds;
    }
}
