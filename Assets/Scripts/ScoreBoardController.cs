using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardController : MonoBehaviour
{
    public GameObject prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        int height = 50 * (Enum.GetValues(typeof(PortalController.Level)).Length - 1);
        transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0,height);
        
        foreach(PortalController.Level level in (PortalController.Level[]) Enum.GetValues(typeof(PortalController.Level)))
        {
            if ((int)level == 0) // skip "Menu"
            {
                continue;
            }

            string levelName = Enum.GetName(typeof(PortalController.Level), level);
            GameObject l = Instantiate(prefab, new Vector3(0, height/2 - ((int)level - 1) * 50 -25, 0), Quaternion.identity);
            l.transform.GetChild(0).GetComponent<Text>().text = levelName.Substring(5);
            
            for (int i = 0; i <= 1; i++)
            {
                int seconds = PlayerPrefs.GetInt(levelName + "+" + i);
                string timeString = "" + (int) (seconds / 60) + "m, " + seconds % 60 + "s";
                //string timeString = TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss");
                l.transform.GetChild(i+1).GetComponent<Text>().text = seconds > 0 ? timeString : "-";


            }
            bool hasKey = PlayerPrefs.HasKey(levelName);
            if (hasKey)
            {
                Color c = l.transform.GetChild(3).GetComponent<Image>().color;
                c.a = 1f;
                l.transform.GetChild(3).GetComponent<Image>().color = c;
            }
            
            l.transform.SetParent(transform.GetChild(0).GetChild(0), false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}
