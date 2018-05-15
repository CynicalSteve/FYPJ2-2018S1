using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralMovement : MonoBehaviour {

    private const double EPSILON = 0.0001;

    public void moveUp(Image theImage, float movementSpeed)
    {
        theImage.transform.position += transform.up * movementSpeed * Time.deltaTime;
    }

    public void moveDown(Image theImage, float movementSpeed)
    {
        theImage.transform.position -= theImage.transform.up * movementSpeed * Time.deltaTime;
    }

    public void moveLeft(Image theImage, float movementSpeed)
    {
        theImage.transform.position -= theImage.transform.right * movementSpeed * Time.deltaTime;
    }

    public void moveRight(Image theImage, float movementSpeed)
    {
        theImage.transform.position += theImage.transform.right * movementSpeed * Time.deltaTime;
    }

    //Move to a specific location
    public void moveTo(Image theImage, float movementSpeed, Vector3 Destination)
    {
        Vector3 dir = Destination - theImage.transform.position;
        float ImageDestinationDistance = dir.magnitude;

        if(ImageDestinationDistance <= EPSILON)
        {
            return;
        }

        theImage.transform.position += dir.normalized * movementSpeed * Time.deltaTime;
    }
}
