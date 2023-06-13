using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    public Rigidbody rbRocket;
    public float upThrustForce;
    public float sideThrustForce;
    public float rotationRate;
    public GameObject child;
    public AudioSource myAudio;


    // Start is called before the first frame update
    void Start()
    {
        child = GameObject.Find("rocket");
        myAudio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Space pressed - thrust");
            StartThrust();


            //replace wit fadeout
        }
        else
        {
            StopThrust();

        }

    }


    private void StopThrust()
    {
        myAudio.Stop();
        mainBooster.Stop();
    }

    private void StartThrust()
    {
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }

        rbRocket.AddRelativeForce(0, upThrustForce * Time.deltaTime, 0);

        if (!myAudio.isPlaying)
        {
            myAudio.PlayOneShot(mainEngine);
        }
    }



    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else
        {
            StopRotation();
        }
    }


    private void StopRotation()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }

    private void RotateLeft()
    {
        //Debug.Log("A pressed - left");
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
        //rbRocket.AddForce(-sideThrustForce * Time.deltaTime, 0, 0);
        ApplyRotation(-rotationRate);
    }

    private void RotateRight()
    {
        //Debug.Log("D pressed - right");
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
        //rbRocket.AddForce(sideThrustForce * Time.deltaTime, 0,0);
        ApplyRotation(rotationRate);
    }

    private void ApplyRotation(float currentDirection) 
    {
        rbRocket.freezeRotation = true;
        child.transform.Rotate(0, 0, currentDirection * Time.deltaTime);
        rbRocket.freezeRotation = false;
    }
}
