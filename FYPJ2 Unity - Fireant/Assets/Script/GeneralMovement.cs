using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {	
	}

    public void moveUp(Image theImage, uint movementSpeed)
    {
        theImage.transform.position += transform.up * movementSpeed * Time.deltaTime;
    }

    public void moveDown(Image theImage, uint movementSpeed)
    {
        theImage.transform.position -= theImage.transform.up * movementSpeed * Time.deltaTime;
    }

    public void moveLeft(Image theImage, uint movementSpeed)
    {
        theImage.transform.position -= theImage.transform.right * movementSpeed * Time.deltaTime;
    }

    public void moveRight(Image theImage, uint movementSpeed)
    {
        theImage.transform.position += theImage.transform.right * movementSpeed * Time.deltaTime;
    }
}
