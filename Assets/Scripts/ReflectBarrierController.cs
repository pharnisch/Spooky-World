using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBarrierController : MonoBehaviour
{
    private EnemyController bossctrl;

    public float reflectTime = 10f;

    private float reflectTimer = 0f;

    public float pauseTime = 15f;

    private float pauseTimer = 0f;

    private bool reflect = false;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        bossctrl = GameObject.Find("EnemyFireCaveBoss").GetComponent<EnemyController>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (reflect)
        {
            reflectTimer += Time.deltaTime;
            if (reflectTimer >= reflectTime)
            {
                reflectTimer = 0f;
                reflect = false;
                bossctrl.SetReflect(reflect);
                sr.color = Color.clear;
            }

        }
        else
        {
            
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseTime)
            {
                pauseTimer = 0f;
                reflect = true;
                bossctrl.SetReflect(reflect);
                sr.color = Color.white;
            }
        }
    }
}
