using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public Transform ClosedTransform;
    public Transform OpenTransform;


    public Vector3 ClosedPosition {
        get { return ClosedTransform ? ClosedTransform.position : Vector3.zero;  }
    }

    public Vector3 OpenPosition {
        get { return OpenTransform ? OpenTransform.position : Vector3.zero; }
    }
    public bool StartOpened = false;
    public float MoveSpeed = 5f;

    private DoorState doorState = DoorState.Shut;

    private float startTime;
    private float journeyLength;

    void Start()
    {
        startTime = Time.time;
        doorState = StartOpened ? DoorState.Open : DoorState.Shut;
    }

    void FixedUpdate()
    {
        Vector3 beforePos = transform.position;
        float distCovered = (Time.time - startTime) * MoveSpeed;

        if (doorState == DoorState.Opening)
        {
            journeyLength = Vector3.Distance(ClosedPosition, OpenPosition);
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(ClosedPosition, OpenPosition, fracJourney);
            if (Mathf.Abs(journeyLength - distCovered) < .05f)
            {
                startTime = Time.time;
                doorState = DoorState.Open;
            }
        }
        else if (doorState == DoorState.Shutting)
        {
            journeyLength = Vector3.Distance(OpenPosition, ClosedPosition);
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(OpenPosition, ClosedPosition, fracJourney);
            if (Mathf.Abs(journeyLength - distCovered) < .05f)
            {
                startTime = Time.time;
                doorState = DoorState.Shut;
            }
        }
    }

    public void Open()
    {
        startTime = Time.time;
        doorState = DoorState.Opening;
    }

    public void Close()
    {
        startTime = Time.time;
        doorState = DoorState.Shutting;
    }

    public void OnDrawGizmos()
    {
        Color bCol = new Color(255, 0, 0, .5f);
        Color aCol = new Color(0, 255, 0, .5f);
        Gizmos.color = aCol;
        Gizmos.DrawCube(ClosedPosition, transform.lossyScale);

        Gizmos.color = bCol;
        Gizmos.DrawCube(OpenPosition, transform.lossyScale);
    }
}


public enum DoorState
{
    Shut,
    Shutting,
    Opening,
    Open
}