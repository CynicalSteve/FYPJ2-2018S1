using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneControl : MonoBehaviour {

    enum GameStates
    {
        STATE_RUNNING,
        STATE_PAUSED
    }

    CharacterObject theCharacter;
    EnemyManager enemyManager;
    PickupManager pickupManager;

    GameStates currentGameState;
    
    private void Awake()
    {
        //Seed random
        Random.InitState((int)System.DateTime.Now.Ticks);
    }

    // Use this for initialization
    void Start() {
        theCharacter = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<CharacterObject>();
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        pickupManager = GameObject.FindGameObjectWithTag("PickupManager").GetComponent<PickupManager>();

        currentGameState = GameStates.STATE_RUNNING;

        enemyManager.EnemyManagerInit();
    }
	
	// Update is called once per frame
	void Update () {

        if (currentGameState == GameStates.STATE_RUNNING)
        { 
            theCharacter.MainCharacterUpdate();
            enemyManager.EnemyManagerUpdate();

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
