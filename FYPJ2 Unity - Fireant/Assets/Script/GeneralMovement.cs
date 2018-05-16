using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralMovement : MonoBehaviour {

    private const double EPSILON = 0.0001;
    Vector3 left = new Vector3(-1, 0, 0);
    Vector3 right = new Vector3(1, 0, 0);
    Vector3 up = new Vector3(0, 1, 0);
    Vector3 down = new Vector3(0, -1, 0);

    public void moveUp(Image theImage, float movementSpeed)
    {
        theImage.transform.position += right * movementSpeed * Time.deltaTime;
    }

    public void moveDown(Image theImage, float movementSpeed)
    {
        theImage.transform.position += down * movementSpeed * Time.deltaTime;
    }

    public void moveLeft(Image theImage, float movementSpeed)
    {
        theImage.transform.position += left * movementSpeed * Time.deltaTime;
    }

    public void moveRight(Image theImage, float movementSpeed)
    {
        theImage.transform.position += right * movementSpeed * Time.deltaTime;
    }

    //Move to a specific location
    public void moveTo(Image theImage, float movementSpeed, Vector3 Destination)
    {
        Vector3 dir = Destination - theImage.transform.position;
        
        if(dir.magnitude <= EPSILON)
        {
            return;
        }

        theImage.transform.position += dir.normalized * movementSpeed * Time.deltaTime;
    }

    public void moveUp(GameObject theGameObject, float movementSpeed)
    {
        theGameObject.transform.position += up * movementSpeed * Time.deltaTime;
    }

    public void moveDown(GameObject theGameObject, float movementSpeed)
    {
        theGameObject.transform.position -= down * movementSpeed * Time.deltaTime;
    }

    public void moveLeft(GameObject theGameObject, float movementSpeed)
    {
        theGameObject.transform.position -= left * movementSpeed * Time.deltaTime;
    }

    public void moveRight(GameObject theGameObject, float movementSpeed)
    {
        theGameObject.transform.position += right * movementSpeed * Time.deltaTime;
    }

    //Move to a specific location
    public void moveTo(GameObject theGameObject, float movementSpeed, Vector3 Destination)
    {
        Vector3 dir = Destination - theGameObject.transform.position;

        if (dir.magnitude <= EPSILON)
        {
            return;
        }

        theGameObject.transform.position += dir.normalized * movementSpeed * Time.deltaTime;
    }
}
