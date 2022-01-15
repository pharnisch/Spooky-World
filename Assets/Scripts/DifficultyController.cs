using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultyController : MonoBehaviour
{
    private Dropdown dropdown;

    private PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GameObject.Find("DifficultyDropdown").GetComponent<Dropdown>();
        int difficulty = PlayerPrefs.GetInt("difficulty");
        dropdown.SetValueWithoutNotify(difficulty);
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        pc.SetDifficulty(difficulty);

        if (SceneManager.GetActiveScene().name != "Menu")
        {
            dropdown.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseDifficulty()
    {
        PlayerPrefs.SetInt("difficulty", dropdown.value);
        PlayerPrefs.Save();
        pc.SetDifficulty(dropdown.value);
    }
}
