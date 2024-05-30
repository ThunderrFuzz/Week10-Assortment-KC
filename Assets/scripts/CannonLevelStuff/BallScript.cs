using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Bowling scoreSystem;
    // Start is called before the first frame update
    void Start()
    {
        scoreSystem = FindObjectOfType<Bowling>();
    }
    private void Update()
    {
        if(transform.position.y < 0) Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pin"))
        {
            Destroy(other.transform.gameObject);
            scoreSystem.AddToFrameScore(1);
        }
    }
}
