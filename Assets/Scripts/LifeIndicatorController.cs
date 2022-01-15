using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIndicatorController : MonoBehaviour
{
    private GameObject lifeCircle;
    
    // Start is called before the first frame update
    void Start()
    {
        lifeCircle = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRatio(float ratio)
    {
        lifeCircle.transform.localScale = new Vector3(ratio,ratio,1);
    }
}
