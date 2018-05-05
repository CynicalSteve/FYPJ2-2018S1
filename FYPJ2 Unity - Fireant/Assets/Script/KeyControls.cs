using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControls : MonoBehaviour {
    
    //This is for key press functions that is not specific to a particular gameobject

	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.Escape))
        {
            //Closes the game, only works in exe not editor
            Application.Quit();
        }

	}
}
