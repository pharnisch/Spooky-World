using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalController : MonoBehaviour
{

    public enum Level
    {
        Menu,
        LevelIntro,
        LevelLostIsle,
        LevelSpookyCastle,
        LevelRiverEscape,
        LevelFlowerFields,
        LevelMagmaWalk,
        LevelSwampsOfDeath,
        LevelFireCave,
    }
    public Level level;
    // Start is called before the first frame update
    void Start()
    {
        String levelName = Enum.GetName(typeof(Level), level);
        if (levelName.StartsWith("Level"))
        {
            levelName = levelName.Substring(5);
        }

        if (levelName == "Menu")
        {
            levelName = "HomeCastle";
        }

        //Color c = GetComponent<SpriteRenderer>().color;
        //Color nc = new Color((1f - c.r)/2f + 0.5f, (1f - c.g)/2f + 0.5f, (1f - c.b)/2f + 0.5f);

        Text text = transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();
        text.text = levelName;
        //text.color = nc;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            GameObject btnObj = GameObject.Find("InteractionButton");
            Button btn = btnObj.GetComponent<Button>();
            btn.interactable = true;
            Text txt = btnObj.transform.GetChild(0).GetComponent<Text>();
            txt.text = "Teleport";
            //txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 1);
            
            btnObj.GetComponent<InteractionButtonController>().SetLevel(level);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            GameObject btnObj = GameObject.Find("InteractionButton");
            Button btn = btnObj.GetComponent<Button>();
            btn.interactable = false;
            Text txt = btnObj.transform.GetChild(0).GetComponent<Text>();
            txt.text = "";
            //txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 1);
        }
    }
    
    //StartCoroutine(ExampleCoroutine());
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(Enum.GetName(typeof(Level), level));
    }
}
