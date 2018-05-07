using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {

    enum GameStates
    {
        STATE_RUNNING,
        STATE_PAUSED
    }

    CharacterObject theCharacter;
    GameStates currentGameState;

    // Use this for initialization
    void Start () {
       theCharacter = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<CharacterObject>();
        currentGameState = GameStates.STATE_RUNNING;
    }
	
	// Update is called once per frame
	void Update () {

        if (currentGameState == GameStates.STATE_RUNNING)
        {
            theCharacter.MainCharacterUpdate();

            if (Input.GetKeyDown(KeyCode.P))
            {
                currentGameState = GameStates.STATE_PAUSED;
            }
        }
        else if (currentGameState == GameStates.STATE_PAUSED)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                currentGameState = GameStates.STATE_RUNNING;
            }
        }
    }
}
