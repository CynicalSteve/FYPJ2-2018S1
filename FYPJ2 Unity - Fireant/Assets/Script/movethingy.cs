using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movethingy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	//this script is to move the platform from top to bottom
	// Update is called once per frame
	void Update () {
        if (transform.position.y <20)
        {
            transform.Translate(0, 5 * Time.deltaTime, 0);
        }
        else
            transform.Translate(0, -40, 0);
    }
}
