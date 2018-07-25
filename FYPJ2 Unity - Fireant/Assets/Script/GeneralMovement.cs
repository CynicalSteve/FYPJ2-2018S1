using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralMovement : MonoBehaviour {
    
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
    public bool moveTo(Image theImage, float movementSpeed, Vector3 Destination)
    {
        Vector3 newDest = theImage.transform.position + ((Destination - theImage.transform.position) * 2);
        Vector3 vectorPos = Vector3.MoveTowards(theImage.transform.position, newDest, movementSpeed * Time.deltaTime);

        if (vectorPos == theImage.transform.position)
        {
            return true;
        }

        theImage.transform.position = vectorPos;
        return false;
    }

    public bool moveToXPos(Image theImage, float movementSpeed, Vector3 Destination)
    {
        Vector3 vectorPos = Vector3.MoveTowards(theImage.transform.position, Destination, movementSpeed * Time.deltaTime);

        if (vectorPos.x == theImage.transform.position.x)
        {
            return true;
        }

        theImage.transform.position.Set(vectorPos.x, theImage.transform.position.y, theImage.transform.position.z);
        return false;
    }

    public void moveUp(GameObject theGameObject, float movementSpeed)
    {
        theGameObject.transform.position += up * movementSpeed * Time.deltaTime;
    }

    public void moveDown(GameObject theGameObject, float movementSpeed)
    {
        theGameObject.transform.position += down * movementSpeed * Time.deltaTime;
    }

    public void moveLeft(GameObject theGameObject, float movementSpeed)
    {
        theGameObject.transform.position += left * movementSpeed * Time.deltaTime;
    }

    public void moveRight(GameObject theGameObject, float movementSpeed)
    {
        theGameObject.transform.position += right * movementSpeed * Time.deltaTime;
    }

    //Move to a specific location, returns true if it has reached the destination
    public bool moveTo(GameObject theGameObject, float movementSpeed, Vector3 Destination)
    {
        Vector3 vectorPos = Vector3.MoveTowards(theGameObject.transform.position, Destination, movementSpeed * Time.deltaTime);

        if(vectorPos == theGameObject.transform.position)
        {
            return true;
        }

        theGameObject.transform.position = vectorPos;
        return false;
    }

    //Move to a specific location, returns true if it has reached the destination
    public bool moveToXPos(GameObject theGameObject, float movementSpeed, Vector3 Destination)
    {
        Vector3 vectorPos = Vector3.MoveTowards(theGameObject.transform.position, Destination, movementSpeed * Time.deltaTime);

        if (vectorPos.x == theGameObject.transform.position.x)
        {
            return true;
        }

        theGameObject.transform.position.Set(vectorPos.x, theGameObject.transform.position.y, theGameObject.transform.position.z);
        return false;
    }

    //Move towards a direction
    public void moveBy(Image theGameObject, float movementSpeed, Vector3 Direction)
    {
        theGameObject.transform.position += Direction * Time.deltaTime * movementSpeed;
    }

    //Move towards a direction
    public void moveBy(GameObject theGameObject, float movementSpeed, Vector3 Direction)
    {
        theGameObject.transform.position += Direction * Time.deltaTime * movementSpeed;
    }
}
