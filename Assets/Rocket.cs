using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource thrusters;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        thrusters = GetComponent<AudioSource>();
        thrusters.Stop();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!thrusters.isPlaying) {
                thrusters.Play();
            }
            rigidBody.AddRelativeForce(Vector3.up);
        } else {
            thrusters.Stop();
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
