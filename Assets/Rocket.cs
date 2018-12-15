using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource thrusters;

    [SerializeField]
    private float rcsRotation = 240f;

    [SerializeField]
    private float rcsThrust = 1750f;

    enum State { Alive, Dying, Trancending };
    State state = State.Alive;


    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        thrusters = GetComponent<AudioSource>();
        thrusters.Stop();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                 // do nothing
                break;
            case "Finish":
                state = State.Trancending;
                Invoke("LoadNextScene", 1f); // parameterize this time
                break;
            default:
                state = State.Dying;
                Invoke("KillPlayer", 1f);
                break;
        }
    }

    private void KillPlayer()
    {
        print("DEAD");
        SceneManager.LoadScene(0); // Send user back to level 1
    }

    private void LoadNextScene()
    {
        print("Hit Finish");
        SceneManager.LoadScene(1); // allow for more than 2 levels
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
