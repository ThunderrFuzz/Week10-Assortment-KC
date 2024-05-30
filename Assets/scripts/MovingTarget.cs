using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public int xBound;
    public Vector3 movespeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > xBound || transform.position.x < -xBound)
        {
            movespeed = -movespeed;
        }
        transform.Translate(movespeed * Time.deltaTime);
    }
}
