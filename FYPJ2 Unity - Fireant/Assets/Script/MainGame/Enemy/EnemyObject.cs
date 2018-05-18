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
    [SerializeField]
    float secondsBetweenShots = 1;

    float fireRateTimer = 0;
    bool canShoot = true;

    STATE_ENEMY enemyState;

    CharacterObject theCharacter;
    GeneralMovement generalMovementScript;
    Image enemyTexture;

    List<GameObject> BulletList = new List<GameObject>();

    enum STATE_ENEMY
    {
        STATE_IDLE,
        STATE_CHASE,
        STATE_ATTACK,

        TOTAL_ENEMY_STATES
    }

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

        //Update bullets shot by enemy
        for (int i = 0; i < BulletList.Count; ++i)
        {
            GameObject BulletObj = BulletList[i];

            if (BulletObj.activeInHierarchy)
            {
                BulletObj.GetComponent<BulletObject>().BulletObjectUpdate();
            }
        }
    }

    void Shoot()
    {
        //Create a bullet and add it as child to Scene control and object List
        GameObject BulletObj = Instantiate(Resources.Load("GenericBullet") as GameObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent.transform);

        //Init Bullet variables
        BulletObj.GetComponent<BulletObject>().BulletObjectInit();

        //Set the bullet to be able to hit player
        BulletObj.GetComponent<BulletObject>().CanHitPlayer = true;

        //Set the pos of bullet to the enemy pos
        BulletObj.transform.position.Set(gameObject.transform.position.x, gameObject.transform.position.y, 0);

        //Rotate the bullet in the direction of destination
        Vector3 normalizedDir = (theCharacter.transform.position - BulletObj.transform.position).normalized;
        normalizedDir.z = 0;
        BulletObj.transform.up = normalizedDir;

        //Set the bullet's destination to cursor
        BulletObj.GetComponent<BulletObject>().SetDestination(theCharacter.transform.position);

        //Add bullet obj to list
        BulletList.Add(BulletObj);
    }

    //Collsion
    //Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GenericBullet")
        {
            collision.gameObject.SetActive(false);
            Health -= 10;
        }
    }

    //Getters & setters
    public float GetHealth()
    {
        return Health;
    }
    public void SetHealth(float newHealth)
    {
        Health = newHealth;
    }
    public void DecreaseHealth(float reduceAmount)
    {
        Health -= reduceAmount;
    }
    public void IncreaseHealth(float increaseAmount)
    {
        Health += increaseAmount;
    }
}
