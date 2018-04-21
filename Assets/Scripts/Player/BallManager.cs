using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public Transform BallSpawnTransform;
    public Ball ball;

    public float LaunchForce = 5f;

    private bool _held = true;

    private void Start()
    {
        if (!ball) {
            ball = FindObjectOfType<Ball>();
            if (!ball)
            {
                Debug.LogError("No Ball caould be found, Stopping...");
                Debug.Break();
            }
        }
        ReturnBall();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_held) Launch();
            else _held = true;
        }

        if (_held)
        {
            ball.ReturnToSpawn(BallSpawnTransform);
        }
    }

    private void ReturnBall()
    {
        _held = true;
    }

    public void Launch()
    {
        _held = false;
        ball.Launch(BallSpawnTransform.forward, LaunchForce);
    }
}
