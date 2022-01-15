using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class InteractionButtonController : MonoBehaviour
{
    private PortalController.Level level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(PortalController.Level lvl)
    {
        this.level = lvl;
    }

    public void Teleport()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Vector3 pos = GameObject.Find("Player").transform.position;
            PlayerPrefs.SetFloat("Menu.x", pos.x);
            PlayerPrefs.SetFloat("Menu.y", pos.y);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(Enum.GetName(typeof(PortalController.Level), level));
    }

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
