using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;


public class PlayerController : MonoBehaviour
{
    
    public float speed = 2.5f;
    private int speedUpLevel = 0;
    private Rigidbody2D _rb;

    private int healthPoints = 100;
    private int energyPoints = 100;
    private TextMeshProUGUI healthText;
    private int maxHealth = 100;
    private TextMeshProUGUI manaText;
    private int maxEnergy = 100;
    private Image healthBar;
    private Image manaBar;
    private float secondsPerEnergy = 0.15f;
    private float secondsPerHealth = 0.5f;
    private float secondsPerHealthTimer = 0f;
    private float secondsPerEnergyTimer = 0f;
    //private int _experience_points;
    
    private int[] keyCounts = new int[Enum.GetNames(typeof(KeyController.KeyColor)).Length];
    private int mainAttackLevel = 1;
    public GameObject projectilePrefab;
    public GameObject specialProjectilePrefab;
    public float shootForce = 200f;
    public GameObject mainCamera;
    private Rigidbody2D _cameraRb;
    private Vector2 _movementVector = Vector2.zero;
    private Vector2 _facingDirection = Vector2.right;

    private FixedJoystick joystick;
    private AudioSource audioSource;
    
    private int difficulty = 0;

    private bool dead = false;
    public AudioClip dieSound;
    public AudioClip bossSound;
    public AudioClip winSound;
    public AudioClip powerUpSound;
    public AudioClip hitSound;
    public AudioClip immuneSound;
    public AudioClip shootSound;
    public AudioClip shootSound2;
    
    
    public GameObject damageDisplayPrefab;

    private SpriteRenderer barrierSpriteRenderer;
    private bool isImmune = false;
    private float barrierTime = 10f;
    private float barrierTimer = 0f;

    private bool isHidden = false;
    private SpriteRenderer leafSpriteRenderer;

    private bool isMenu = false;

    private Image enemyHealthBar;
    private TextMeshProUGUI enemyHealthText;
    private TextMeshProUGUI enemyName;
    private float enemyInfoTimer = 0f;
    private float enemyInfoTime = 10f;
    private GameObject enemyInfo;

    // Start is called before the first frame update
    void Start()
    {
        // INIT VARIABLES
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        manaBar = GameObject.Find("ManaBar").GetComponent<Image>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        manaText = GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>();
        joystick = GameObject.Find("Joystick").GetComponent<FixedJoystick>();
        enemyHealthBar = GameObject.Find("HealthBarEnemy").GetComponent<Image>();
        enemyHealthText = GameObject.Find("HealthTextEnemy").GetComponent<TextMeshProUGUI>();
        enemyName = GameObject.Find("EnemyName").GetComponent<TextMeshProUGUI>();
        enemyInfo = GameObject.Find("EnemyInfo");
        HideEnemyInfo();
        
        _rb = GetComponent<Rigidbody2D>();
        _cameraRb = mainCamera.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        barrierSpriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        leafSpriteRenderer = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        
        // START LOGIC
        
        isMenu = (SceneManager.GetActiveScene().name == "Menu");
        if (isMenu)
        {
            if (PlayerPrefs.HasKey("Menu.x") && PlayerPrefs.GetFloat("Menu.x") < -200f)
            {
                transform.position = new Vector3(
                    PlayerPrefs.GetFloat("Menu.x"),
                    PlayerPrefs.GetFloat("Menu.y"), 
                    0); 
            }
            else
            {
                transform.position = new Vector3(
                    -271.5f,
                    0,
                    0); 
            }
            
        }

        keyCounts[(int) KeyController.KeyColor.Black] = PlayerPrefs.GetInt("BlackKeys");
        UpdateKeyPanel();

        StartCoroutine(HideLoadingScreenCoroutine());
    }


    private void Update()
    {
        // change from pull to push from joystick, to hopefully have less calls
        float x = joystick.Horizontal; //Input.GetAxis("Horizontal");
        float y = joystick.Vertical; //Input.GetAxis("Vertical");
        _movementVector = new Vector2(x, y);
        if (_movementVector != Vector2.zero)
        {
            _facingDirection = _movementVector.normalized;
        }

        
        // if (Input.GetKeyDown("space"))
        // {
        //     MainAttack();
        // }


    }
    
    private void FixedUpdate()
    {
        // Move Player
        _rb.MovePosition(_rb.position + _movementVector * (speed * Time.fixedDeltaTime));
        // Move Camera
        _cameraRb.MovePosition(transform.position);
        // Rotate Player To Moving Direction
        if (_movementVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(_movementVector.y, _movementVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        // Regeneration
        if (secondsPerEnergyTimer <= 0)
        {
            secondsPerEnergyTimer = secondsPerEnergy;
            ChangeEnergyPoints(1);
            
        }
        else
        {
            secondsPerEnergyTimer -= Time.deltaTime;
        }
        if (secondsPerHealthTimer <= 0)
        {
            secondsPerHealthTimer = secondsPerHealth;
            ChangeHealthPoints(1);
            
        }
        else
        {
            secondsPerHealthTimer -= Time.deltaTime;
        }

        if (barrierTimer > 0)
        {
            barrierTimer -= Time.deltaTime;
            if (barrierTimer <= 0)
            {
                DisableBarrier();
            }
        }
        
        if (enemyInfoTimer > 0)
        {
            enemyInfoTimer -= Time.deltaTime;
            if (enemyInfoTimer <= 0)
            {
                HideEnemyInfo();
            }
        }
    }

    public void DisplayEnemyInfo(EnemyController ec)
    {
        enemyInfoTimer = enemyInfoTime;
        enemyHealthBar.fillAmount = ec.GetHealthPoints() / (float) ec.GetMaxHealthPoints();
        enemyHealthText.text = "" + ec.GetHealthPoints() + " Health";
        enemyName.text = "" + ec.enemyName;
        enemyInfo.SetActive(true);
    }

    public void HideEnemyInfo()
    {
        enemyInfo.SetActive(false);
        enemyInfoTimer = 0f;
        enemyHealthBar.fillAmount = 0;
        enemyHealthText.text = "";
        enemyName.text = "";
    }
    
    public bool Hide()
    {
        if (isHidden)
        {
            return false;
        }
        PlayAudioOneShot(powerUpSound);
        leafSpriteRenderer.enabled = true;
        isHidden = true;
        return true;
    }

    public bool IsHidden()
    {
        return isHidden;
    }

    public bool EnableBarrier()
    {
        if (isImmune)
        {
            return false;
        }
        PlayAudioOneShot(powerUpSound);
        barrierSpriteRenderer.enabled = true;
        barrierTimer = 10f;
        isImmune = true;
        return true;
    }
    
    public void DisableBarrier()
    {
        barrierTimer = 0;
        isImmune = false;
        barrierSpriteRenderer.enabled = false;
    }
    
    public void SetDifficulty(int d)
    {
        difficulty = d;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }
    
    public AudioClip ChangeAudio(AudioClip audioClip)
    {
        AudioClip previous = audioSource.clip;
        audioSource.clip = audioClip;
        audioSource.Play();
        return previous;
    }

    public void PlayAudioOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    
    public void IncreaseSpeed(float increase)
    {
        PlayAudioOneShot(powerUpSound);
        speed += increase;
        speedUpLevel += 1;
        GameObject.Find("SpeedUpLevel").GetComponent<Text>().text = "" + speedUpLevel;
    }

    public bool AddKey(KeyController.KeyColor keyColor)
    {
        
        if (keyCounts[(int) keyColor] >= 3)
        {
            return false;
        }
        PlayAudioOneShot(powerUpSound);
        keyCounts[(int)keyColor] += 1;

        PlayerPrefs.SetInt("BlackKeys", keyCounts[(int) KeyController.KeyColor.Black]);
        PlayerPrefs.Save();
        UpdateKeyPanel();
        return true;
    }

    private bool HasKey(KeyController.KeyColor keyColor)
    {
        return keyCounts[(int) keyColor] >= 1;
    }

    public bool RemoveKey(KeyController.KeyColor keyColor)
    {
        if (HasKey(keyColor))
        {
            keyCounts[(int) keyColor] -= 1;
            
            PlayerPrefs.SetInt("BlackKeys", keyCounts[(int) KeyController.KeyColor.Black]);
            PlayerPrefs.Save();
            UpdateKeyPanel();
            return true;
        }
        return false;
    }

    private void UpdateKeyPanel()
    {
        GameObject keyPanel = GameObject.Find("KeyPanel");
        GameObject key0 = GameObject.Find("Key0");

        for (int i = 0; i < keyPanel.transform.childCount; i++)
        {
            Destroy(keyPanel.transform.GetChild(i).gameObject);
        }

        int printedKeys = 0;
        for (int i = 0; i < keyCounts.Length; i++)
        {
            int keyCount = keyCounts[i];
            Color color = KeyController.GetColor((KeyController.KeyColor)i);
            for (int j = 0; j < keyCount; j++)
            {
                int xPos = -16 + (printedKeys * -32);
                GameObject keyGameObject = Instantiate(key0, new Vector3(xPos,0,0), Quaternion.identity);
                keyGameObject.transform.SetParent(keyPanel.transform, false);
                //keyGameObject.transform.localPosition.Set(xPos, 0, 0);
                
                Image keyImage = keyGameObject.GetComponent<Image>();
                keyImage.color = color;
                
                printedKeys++;
            }
        }
    }

    public void SpecialAttack(int specialAttackLevel)
    {
        //PlayAudioOneShot(shootSound2);
        float degreeStep = 360f / specialAttackLevel;
        for (int i = 0; i < specialAttackLevel; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(i * degreeStep, Vector3.forward) *
                                new Vector3(_facingDirection.x, _facingDirection.y, 0);
            MainAttack(direction, specialProjectilePrefab);
        }
    }

    
    public void MainAttack()
    {
        //PlayAudioOneShot(shootSound);
        float degreeStep = 360f / mainAttackLevel;
        for (int i = 0; i < mainAttackLevel; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(i * degreeStep, Vector3.forward) *
                                new Vector3(_facingDirection.x, _facingDirection.y, 0);
            MainAttack(direction, projectilePrefab);
        }
    }

    private void MainAttack(Vector3 direction, GameObject prefab)
    {
        Vector3 normalizedDirection = direction.normalized;
        Vector3 scaledDirection = normalizedDirection * 0.4f;
        
        GameObject projectile = Instantiate(prefab, transform.position + scaledDirection, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(normalizedDirection * shootForce);
        Destroy(projectile, 3f);
    }

    public void MainAttackUp()
    {
        PlayAudioOneShot(powerUpSound);
        mainAttackLevel += 1;
        GameObject.Find("MainAttackLevel").GetComponent<Text>().text = "" + mainAttackLevel;
       
    }
    
    
    
    
    
    
    
    // Getter and Setter
    public int GetEnergyPoints()
    {
        return energyPoints;
    }
    public bool ChangeEnergyPoints(int change)
    {
        if (dead)
        {
            return false;
        }

        int newPoints = Math.Min(energyPoints + change, 100);
        if (newPoints >= 0)
        {
            energyPoints = newPoints;
            manaText.text = "" + energyPoints + " Mana"; //+ maxEnergy;
            manaBar.fillAmount = energyPoints/100f;
            return true;
        }
        return false;
    }

    public void ChangeHealthPoints(int change)
    {
        if (dead)
        {
            return;
        }
        

        if (change < 0)
        {
            if (isMenu)
            {
                change = 0;
                //GameObject damageDisplay = Instantiate(damageDisplayPrefab, transform.position, Quaternion.identity);
                //damageDisplay.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "IMMUNE";
                //Destroy(damageDisplay, 1f);
            }
            else if (isImmune)
            {
                PlayAudioOneShot(immuneSound);
                change = 0;
                GameObject damageDisplay = Instantiate(damageDisplayPrefab, transform.position, Quaternion.identity);
                damageDisplay.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "IMMUNE";
                Destroy(damageDisplay, 1f);
            }
            else
            {
                PlayAudioOneShot(hitSound);
                change = (int)(change * (1+difficulty*3f));
            
                // display dmg at hitPosition
                GameObject damageDisplay = Instantiate(damageDisplayPrefab, transform.position, Quaternion.identity);
                damageDisplay.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "" + -change;
                Destroy(damageDisplay, 1f);
            }
            
        }
        healthPoints = Math.Min(healthPoints + change, 100);
        healthText.text = "" + healthPoints + " Health"; //"/" + maxHealth;
        healthBar.fillAmount = healthPoints / 100f;
        
        
        if (healthPoints <= 0)
        {
            _rb.simulated = false;
            dead = true;
            //SceneManager.LoadScene("Menu");
            PlayAudioOneShot(dieSound);
            StartCoroutine(DieCoroutine());
        }
    }

    public bool IsDead()
    {
        return dead;
    }
    
    //StartCoroutine(ExampleCoroutine());
    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
    
    
    IEnumerator HideLoadingScreenCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("LoadingScreen").SetActive(false);
        if (!isMenu)
        {
            GameObject.Find("TimeMeasure").GetComponent<TimeMeasureController>().StartTime();
        }
    }
    
}
