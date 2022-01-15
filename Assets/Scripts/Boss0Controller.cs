using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss0Controller : MonoBehaviour
{
    public GameObject portalPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDestroy()
    {
        EnemyController enemyController = GetComponent<EnemyController>();
        if (enemyController.GetHealthPoints() <= 0)
        {
            // spawn portal back
            Instantiate(portalPrefab, transform.position, Quaternion.identity);
        }

    }
}
