using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class CannonBallFire : MonoBehaviour
{
    public GameObject ball;
    public GameObject spawnPos;
    public int shootingForce = 700;
    public float rotSpeed = 10f;
    public GameObject tracer;
    public int lineResolution = 20;
    public float timeStep = 0.1f;

    private Bowling scoreSystem;
    private LineRenderer trajectoryRenderer;


    void Start()
    {
        scoreSystem = FindObjectOfType<Bowling>();

        // Initialize and set up the LineRenderer for the trajectory
        GameObject _tracer = Instantiate(tracer, transform.position, Quaternion.identity);
        trajectoryRenderer = _tracer.GetComponent<LineRenderer>();
        trajectoryRenderer.positionCount = lineResolution;
        trajectoryRenderer.startColor = Color.blue;
        trajectoryRenderer.endColor = Color.cyan;
    }

    void Update()
    {
        // Update the trajectory line each frame
        UpdateTrajectory();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject _ball = Instantiate(ball, spawnPos.transform.position, spawnPos.transform.rotation);
            _ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0f, shootingForce, 0f)); // gets ragdoll
            //_ball.GetComponentInChildren<Rigidbody>().AddRelativeForce(new Vector3(0f, shootingForce, 0f)); // apply same force to non rag body 
        }


        HandleRotation();
    }

    void UpdateTrajectory()
    {
        // store points in line 
        Vector3[] points = new Vector3[lineResolution];

        // start point
        Vector3 startingPosition = transform.position;


        // gets starting vel 
        Vector3 startingVelocity = transform.up * shootingForce * Time.fixedDeltaTime;

        // loops over each point in line 
        for (int i = 0; i < lineResolution; i++)
        {
            // get the time at this point in the 
            float t = i * timeStep;

            //calculates the cannon ball postion using time^2
            points[i] = startingPosition + t * startingVelocity + 0.5f * Physics.gravity * t * t;
        }

        // sets postion for each point in arc 
        trajectoryRenderer.SetPositions(points);
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.right, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.left, rotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.parent.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.parent.Rotate(Vector3.back, rotSpeed * Time.deltaTime);
        }
    }


}



