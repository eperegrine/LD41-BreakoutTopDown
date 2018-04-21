using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed = 5f;
    public Camera PlayerCamera;

    private Rigidbody _rb;

    public float LerpTime = .3f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (!PlayerCamera) PlayerCamera = Camera.main;
    }

    private void Update()
    {
        float HorzInput = Input.GetAxis("Horizontal");
        float VertInput = Input.GetAxis("Vertical");

        float HorzSpeed = HorzInput * Speed;
        float VertSpeed = VertInput * Speed;

        _rb.velocity = new Vector3(HorzSpeed, _rb.velocity.y, VertSpeed);

        //Rotate
        Vector3 ObjPos = PlayerCamera.WorldToScreenPoint(transform.position); //Map the obj onto the Screen
        Vector3 dir = Input.mousePosition - ObjPos; //Find the Direction
        float rotZ = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg; //Turn the direction into an angle
        
        this.transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, rotZ, 0), LerpTime); //Rotate
    }

}
