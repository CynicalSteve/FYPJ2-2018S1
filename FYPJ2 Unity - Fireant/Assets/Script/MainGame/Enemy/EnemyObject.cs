using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyObject : MonoBehaviour {
    
    [SerializeField]
    float DetectionRadius = 500;
    [SerializeField]
    float AttackRadius = 50;
    [SerializeField]
    float Health = 100;
    [SerializeField]
    float MovementSpeed = 10;

    enum STATE_ENEMY
    {
        STATE_IDLE,
        STATE_CHASE,
        STATE_ATTACK,

        TOTAL_ENEMY_STATES
    }

    STATE_ENEMY enemyState;

    CharacterObject theCharacter;
    GeneralMovement generalMovementScript;
    Image enemyTexture;

    // Use this for initialization
    public void EnemyObjectInit() {
        //gameObject.SetActive(false);
        enemyState = STATE_ENEMY.STATE_IDLE;

        theCharacter = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<CharacterObject>();
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        enemyTexture = GetComponent<Image>();

        gameObject.transform.Translate(0, theCharacter.transform.position.y, 0);
    }
	
	public void EnemyObjectUpdate() {

        float playerEnemyDistance = (theCharacter.transform.position - gameObject.transform.position).magnitude;

        switch (enemyState)
        {
            
            case STATE_ENEMY.STATE_IDLE:
                {
                    //Check distance from player
                    if (playerEnemyDistance <= DetectionRadius)
                    {
                        enemyState = STATE_ENEMY.STATE_CHASE;
                        goto case STATE_ENEMY.STATE_CHASE;
                    }

                    break;
                }
            case STATE_ENEMY.STATE_CHASE:
                {
                    //Chase the player
                    generalMovementScript.moveTo(enemyTexture, MovementSpeed, theCharacter.transform.position);

                    //If player is close enough, attack player
                    if (playerEnemyDistance <= AttackRadius)
                    {
                        enemyState = STATE_ENEMY.STATE_ATTACK;
                        goto case STATE_ENEMY.STATE_ATTACK;
                    }

                    break;
                }
            case STATE_ENEMY.STATE_ATTACK:
                {
                    //Attack the player (spawn bullet)

                    break;
                }

            default:
                break;
        }

	}
}
