using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy04 : EnemyBase {
    
    // Use this for initialization
    override public void EnemyInit()
    {
        //Ignore collision with own Enemy layer
        Physics2D.IgnoreLayerCollision(9, 9);

        //gameObject.SetActive(false);
        enemyState = STATE_ENEMY.STATE_IDLE;

        theCharacter = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<CharacterObject>();
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        enemyTexture = GetComponent<Image>();

        gameObject.transform.Translate(0, theCharacter.transform.position.y, 0);

        fireRateTimer = Random.Range(0, secondsBetweenShots);
    }
	
	override public void EnemyUpdate() {

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
                    if(canShoot)
                    {
                        Shoot();
                        canShoot = false;
                    }
                    else
                    {
                        fireRateTimer += Time.deltaTime;

                        if(fireRateTimer >= secondsBetweenShots)
                        {
                            canShoot = true;
                            fireRateTimer = 0;
                        }
                    }

                    break;
                }

            default:
                break;
        }

        //Face player direction
        if (theCharacter.transform.localPosition.y - gameObject.transform.localPosition.y < 0)
        {
            if (enemyDirection != DIRECTION.DIRECTION_LEFT)
            {
                enemyDirection = DIRECTION.DIRECTION_LEFT;
                gameObject.transform.Rotate(Vector3.up, 180);
            }
        }
        else if (enemyDirection != DIRECTION.DIRECTION_RIGHT)
        {
            enemyDirection = DIRECTION.DIRECTION_RIGHT;
        }
    }
    
    //Collision
    void OnTriggerEnter2D(Collider2D other)
    {
        //Projectile Layer
        if (other.gameObject.layer == 8)
        {
            if (other.gameObject.tag == "GenericBullet")
            {
                if (!other.gameObject.GetComponent<BulletObject>().CanHitPlayer)
                {
                    Destroy(other.gameObject);
                    DecreaseHealth(other.gameObject.GetComponent<BulletObject>().BulletDamage);
                }
            }
            else if (other.gameObject.tag == "GenericRocket")
            {
                if (!other.gameObject.GetComponent<RocketObject>().CanHitPlayer)
                {
                    Destroy(other.gameObject);
                    DecreaseHealth(other.gameObject.GetComponent<RocketObject>().RocketDamage);
                }
            }
        }
    }
}
