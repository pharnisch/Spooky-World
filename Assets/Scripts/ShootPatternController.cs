using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPatternController : MonoBehaviour
{

    public GameObject projectilePrefab;
    private int fibonac = 1;
    private int fibonacLast = 1;
    private int fibonacMax = 200;
    private float patternTimer = 0f;
    private float patternTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (patternTimer <= 0f)
        {
            Pattern1();
            int tmp = fibonac;
            fibonac = fibonac + fibonacLast;
            fibonacLast = tmp;
            if (fibonac > fibonacMax)
            {
                fibonac = 1;
                fibonacLast = 1;
            }
            
            patternTime = fibonac / 10f;
            patternTimer = patternTime;
        }
        else
        {
            patternTimer -= Time.deltaTime;
        }
    }
    

    void Pattern1()
    {
        float degreeStep = 360f / fibonac;
        for (int i = 0; i < fibonac; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(i * degreeStep, Vector3.forward) *
                                new Vector3(1, 1, 0);
            Shoot(direction);
        }
    }

    void Shoot(Vector3 direction)
    {
        Vector3 normalizedDirection = direction.normalized;
        Vector3 scaledDirection = normalizedDirection * 0.4f;
        
        GameObject projectile = Instantiate(projectilePrefab, transform.position + scaledDirection, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(normalizedDirection * 100f);
        Destroy(projectile, 10f);
    }
}
