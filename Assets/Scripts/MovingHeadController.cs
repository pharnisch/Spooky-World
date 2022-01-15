using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHeadController : MonoBehaviour
{
    public float interval = 2f;
    private float distance = 0.5f;
    private float timer = 0f;

    private int directionX = 0;

    private int directionY = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer <= interval)
        {
            // move out 
            Vector3 pos = transform.localPosition;
            transform.localPosition = new Vector3(pos.x + distance * directionX * Time.deltaTime/interval, pos.y + distance * directionY * Time.deltaTime/interval, 0);
        } else if (timer <= interval * 2)
        {
            // move in
            Vector3 pos = transform.localPosition;
            transform.localPosition = new Vector3(pos.x - distance * directionX * Time.deltaTime/interval, pos.y - distance * directionY * Time.deltaTime/interval, 0);

        }
        else
        {
            SetNewDirection();
            transform.localPosition = new Vector3(0, 0, 0);
            timer = 0;
        }
        

        print(transform.localPosition);
        
    }

    private void SetNewDirection()
    {
        if (ThrowCoin())
        {
            if (ThrowCoin())
            {
                directionX = 1;
                directionY = 0;
            }
            else
            {
                directionX = -1;
                directionY = 0;
            }
        }
        else
        {
            if (ThrowCoin())
            {
                directionY = 1;
                directionX = 0;
            }
            else
            {
                directionY = -1;
                directionX = 0;
            }
        }
    }

    private bool ThrowCoin()
    {
        return (Random.Range(0f,1f) > 0.5f);
    }
}
