using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlantController : MonoBehaviour
{
    public GameObject spawn;
    //public int amount = 6;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        
        //for (int i=0;i<amount;i++)
        //{
        //    Vector3 pos = new Vector3((float)(x+i*0.1), y, 0);
        //    Instantiate(spawn, pos, Quaternion.identity);
        //}
        
        Instantiate(spawn, new Vector3((float)(x+0.1), (float)(y+0.1), 0), Quaternion.identity);
        Instantiate(spawn, new Vector3((float)(x+0.1), (float)(y-0.1), 0), Quaternion.identity);
        Instantiate(spawn, new Vector3((float)(x-0.1), (float)(y+0.1), 0), Quaternion.identity);
        Instantiate(spawn, new Vector3((float)(x-0.1), (float)(y-0.1), 0), Quaternion.identity);
        
        Instantiate(spawn, new Vector3((float)(x+0.05), (float)(y+0.05), 0), Quaternion.identity);
        Instantiate(spawn, new Vector3((float)(x+0.05), (float)(y-0.05), 0), Quaternion.identity);
        Instantiate(spawn, new Vector3((float)(x-0.05), (float)(y+0.05), 0), Quaternion.identity);
        Instantiate(spawn, new Vector3((float)(x-0.05), (float)(y-0.05), 0), Quaternion.identity);

    }
}
