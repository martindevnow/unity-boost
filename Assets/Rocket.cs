using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource thrusters;

    [SerializeField]
    private float rcsRotation = 320f;

    [SerializeField]
    private float rcsThrust = 1800f;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        thrusters = GetComponent<AudioSource>();
        thrusters.Stop();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsRotation * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (!thrusters.isPlaying)
            {
                thrusters.Play();
            }
            float speedThisFrame = rcsThrust * Time.deltaTime;
            rigidBody.AddRelativeForce(Vector3.up * speedThisFrame);
        }
        else
        {
            thrusters.Stop();
        }
    }
}
