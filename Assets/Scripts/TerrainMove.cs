using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 startPosition;
    public Vector3[] moveToPoints;
    public Vector3 currentPoint;

    public float moveSpeed;

    public int pointSelection;

    // Use this for initialization
    void Start()
    {

        //Sets the object to your starting point
        this.transform.position = startPosition;

    }

    // Update is called once per frame
    void Update()
    {

        Move();

    }


    void Move()
    {

        //Starts to move the object towards the first "moveToPoint" you set in inspector
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentPoint, Time.deltaTime * moveSpeed);

        //check to see if the object is at the next "moveToPoint"
        if (this.transform.position == currentPoint)
        {

            //if so it sets the next moveTo location
            pointSelection++;

            //if your object hits the last "moveToPoint it sends the object back to starting position to start the sequence over
            if (pointSelection == moveToPoints.Length)
            {
                pointSelection = 0;

            }

            //sets the destination of the "moveToPoint" destination
            currentPoint = moveToPoints[pointSelection];
        }
    }
}

