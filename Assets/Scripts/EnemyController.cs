using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private PlayerController _playerController;
    private Rigidbody2D rb;
    public float speed = 1f;
    public int maxHealthPoints = 100;
    private int healthPoints;
    private LifeIndicatorController _lifeIndicatorController;
    public GameObject LifeIndicatorPrefab;
    public float hitTime = 1f;
    private float hitTimer = 0f;
    public float maxFollowDistance = 10f;
    public float distanceGoal = 0f;
    public int collisionDamage = 10;
    public GameObject projectilePrefab = null;
    public float shootTime = 4f;
    private float shootTimer = 0f;
    public float shootForce = 100f;
    private bool inFight = false;
    public float projectileLifeTime = 5f;
    public bool rotate = false;
    public GameObject drop = null;
    public float dropChance = 0.2f;
    public bool relativeToParent = false;
    private Vector3 localPosition;
    public bool isImmune = false;
    public bool isBoss = false;
    public GameObject portalPrefab;
    public string enemyName;
    private bool reflect = false;
    
    void Start()
    {


        if (relativeToParent)
        {
            localPosition = transform.localPosition;
        }
        healthPoints = maxHealthPoints;
        player = GameObject.Find("Player");
        _playerController = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        
        if (isImmune)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        else
        {
            _lifeIndicatorController = transform.GetChild(0).gameObject.GetComponent<LifeIndicatorController>();

        }
        
        if (isBoss)
        {
            _playerController.ChangeAudio(_playerController.bossSound);
        }
        
        
    }

    private void FixedUpdate()
    {
        if (_playerController.IsDead())
        {
            return;
        }
        
        Vector2 playerPos = player.transform.position;
        Vector2 myPos = gameObject.transform.position;
        Vector2 movementVector = playerPos - myPos;
        float dist = movementVector.magnitude;
        float pullRange = maxFollowDistance;
        if (_playerController.IsHidden())
        {
            pullRange /= 2; // if Player hidden, enemies only see half distance far
        }
        if (relativeToParent)
        {
            transform.localPosition = localPosition;
            if (dist <= pullRange)
            {
                inFight = true;
            }
            else
            {
                inFight = false;
            }
        }
        else
        {
            if (dist <= pullRange)
            {
                inFight = true;
                if (dist > distanceGoal)
                {
                    rb.velocity = Vector2.zero; // stop moving
                    rb.MovePosition(rb.position + movementVector.normalized * (speed * Time.fixedDeltaTime));
                }
            }
            else
            {
                rb.velocity = Vector2.zero; // stop moving
                inFight = false;
            }
        }


        if (rotate)
        {
            // Rotate Enemy To Moving Direction
            if (movementVector != Vector2.zero)
            {
                float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        
        // TIMERS
        if (hitTimer > 0f)
        {
            hitTimer -= Time.deltaTime;
        }

        if ((projectilePrefab.name != "Placeholder") && inFight)
        {
            if (shootTimer > 0f)
            {
                shootTimer -= Time.deltaTime;
            }
            if (shootTimer <= 0f)
            {
                Shoot(movementVector);
                shootTimer = shootTime;
            }
        }

    }

    public void Shoot(Vector3 movementVector)
    {
        Vector3 normalizedDirection = movementVector.normalized;
        float scaleFactor = (projectilePrefab.GetComponent<CircleCollider2D>().radius + GetComponent<CircleCollider2D>().radius) - 0.1f; // 1f
        Vector3 scaledDirection = normalizedDirection * scaleFactor;
        GameObject projectile = Instantiate(projectilePrefab, transform.position + scaledDirection, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(normalizedDirection * shootForce);

        if (projectileLifeTime > 0)
        {
            Destroy(projectile, projectileLifeTime);
        }
        
    }

    public void SetReflect(bool reflect)
    {
        this.reflect = reflect;
    }

    public bool IsReflecting()
    {
        return this.reflect;
    }
    
    public void ChangeHealthPoints(int change)
    {
        if (reflect)
        {
            _playerController.ChangeHealthPoints((int) change/3);
        }
        else
        {
            healthPoints += change;
            healthPoints = Math.Max(0, healthPoints);
            _lifeIndicatorController.SetRatio(healthPoints/(float)maxHealthPoints);
        
            if (healthPoints <= 0)
            {
                Die();
                Destroy(gameObject);
            }
        }

    }

    public int GetHealthPoints()
    {
        return healthPoints;
    }

    public int GetMaxHealthPoints()
    {
        return maxHealthPoints;
    }
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        CollisionHandling(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        CollisionHandling(other);
    }

    private void CollisionHandling(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (hitTimer <= 0f)
            {
                _playerController.ChangeHealthPoints(-collisionDamage);
                hitTimer = hitTime;
            }
        }
    }

    private void Die()
    {
        // WIN MUSIC IF BOSS WAS KILLED
        if (isBoss)
        {
            TimeMeasureController tmc = GameObject.Find("TimeMeasure").GetComponent<TimeMeasureController>();
            tmc.Stop();
            
            int seconds = tmc.GetSeconds();
            int difficulty = PlayerPrefs.GetInt("difficulty");
            string scene = SceneManager.GetActiveScene().name;
            string key = scene + "+" + difficulty;

            if (PlayerPrefs.HasKey(key))
            {
                if (PlayerPrefs.GetInt(key) > seconds)
                {
                    
                    PlayerPrefs.SetInt(key, seconds);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                PlayerPrefs.SetInt(key, seconds);
                PlayerPrefs.Save();
            }
            
            _playerController.ChangeAudio(_playerController.winSound);
            Instantiate(portalPrefab, transform.position, Quaternion.identity);
        }
        
        // DROP
        if ((drop.name != "Placeholder"))
        {
            Random rand = new Random();
            float randomFloat = (float) rand.NextDouble();
            if (dropChance > randomFloat)
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }
        }

    }
}
