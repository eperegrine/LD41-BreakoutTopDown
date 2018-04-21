using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour {

    Rigidbody _rb;
    public float MaxSpeed = 50f;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();

        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
	}

    private void Update()
    {
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, MaxSpeed);
    }

    public void ReturnToSpawn(Transform spawn)
    {
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
        _rb.velocity = Vector3.zero;
        
    }

    public void Launch(Vector3 direction, float force = 5f)
    {
        _rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
