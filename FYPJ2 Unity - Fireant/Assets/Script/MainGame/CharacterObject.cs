using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterObject : MonoBehaviour {
    private Animator animator; 
    [SerializeField]
    float characterMovementSpeed = 10;

    [SerializeField]
    float characterHealth = 100;

    [SerializeField]
    float characterHealthLimit = 100;
    
    [SerializeField]
    float characterAttackDamage = 10;

    [SerializeField]
    float secondsBetweenShots = 1.0f;

    [SerializeField]
    float InvincibilityTimeLimit = 5;
    [SerializeField]
    Text charhealth;
    public enum CHARACTER_STATE
    {
        CHARACTERSTATE_NORMAL,
        CHARACTERSTATE_INVINCIBLE,

        TOTAL_CHARACTERSTATE
    }

    CHARACTER_STATE characterState;

    GeneralMovement generalMovementScript;
    Image characterTexture;
    
    List<GameObject> BulletList = new List<GameObject>();

    float InvincibilityTimer = 0;
    float fireRateTimer = 0;
    bool canShoot = true;

    bool facingLeft = false;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        characterTexture = GetComponent<Image>();
        gameObject.transform.GetChild(0).GetComponent<Image>();
    }
    // Update is called once per frame
    public void MainCharacterUpdate()
    {
        animator.SetInteger("states", 1);
        charhealth.text = "Health : " + characterHealth.ToString() +"/"  + "100"; 
        //Crosshair snap to mouse position
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        Vector3 mousePos= Camera.main.ScreenToWorldPoint(screenPoint);
        gameObject.transform.GetChild(0).transform.position = mousePos;

        //Character faces direction of crosshair
        if (mousePos.x < gameObject.transform.position.x && !facingLeft)
        {
            gameObject.transform.right = -gameObject.transform.right;
            facingLeft = true;
        }
        else if (mousePos.x > gameObject.transform.position.x && facingLeft)
        {
            gameObject.transform.right = -gameObject.transform.right;
            facingLeft = false;
        }
        //Shoot Pistol
        if (Input.GetMouseButtonDown(0))
        {
            if (canShoot)
            {
                Shoot();
                canShoot = false;
            }
        }
        {
            if (!canShoot)
            {
                fireRateTimer += Time.deltaTime;

                if (fireRateTimer >= secondsBetweenShots)
                {
                    canShoot = true;
                    fireRateTimer = 0;
                }
            }
        }
        //Shoot Machine Gun
        //if (Input.GetMouseButton(0))
        //{
        //    if (canShoot)
        //    {
        //        Shoot();
        //        canShoot = false;
        //    }
        //}
        //{
        //    if (!canShoot)
        //    {
        //        fireRateTimer += Time.deltaTime;

        //        if (fireRateTimer >= secondsBetweenShots)
        //        {
        //            canShoot = true;
        //            fireRateTimer = 0;
        //        }
        //    }
        //}
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += transform.up * characterMovementSpeed * Time.deltaTime;
            generalMovementScript.moveUp(characterTexture, characterMovementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.position += transform.right * characterMovementSpeed * Time.deltaTime;
            generalMovementScript.moveDown(characterTexture, characterMovementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.position -= transform.right * characterMovementSpeed * Time.deltaTime;
            generalMovementScript.moveLeft(characterTexture, characterMovementSpeed);
            animator.SetInteger("states", 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.position -= transform.up * characterMovementSpeed * Time.deltaTime;
            generalMovementScript.moveRight(characterTexture, characterMovementSpeed);
            animator.SetInteger("states", 0);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400);
        }

        //Update bullets shot by player
        for(int i = 0; i < BulletList.Count; ++i)
        {
            GameObject BulletObj = BulletList[i];

            if (BulletObj.activeInHierarchy)
            {
                BulletObj.GetComponent<BulletObject>().BulletObjectUpdate();
            }
        }

        //Update Character States
        if(characterState == CHARACTER_STATE.CHARACTERSTATE_INVINCIBLE)
        {
            InvincibilityTimer += Time.deltaTime;

            if(InvincibilityTimer >= InvincibilityTimeLimit)
            {
                InvincibilityTimer = 0;
                characterState = CHARACTER_STATE.CHARACTERSTATE_NORMAL;
            }
        }
    }

    private void Shoot()
    {
        //Create a bullet and add it as child to Scene control and object List
        GameObject BulletObj = Instantiate(Resources.Load("GenericBullet") as GameObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent.transform);

        //Init Bullet variables
        BulletObj.GetComponent<BulletObject>().BulletObjectInit();

        //Set the pos of bullet to the character pos
        BulletObj.transform.position.Set(gameObject.transform.position.x, gameObject.transform.position.y, 0);

        //Get mouse position by converting the pos from screen space to world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        //Rotate the bullet in the direction of destination
        Vector3 normalizedDir = (mousePos - BulletObj.transform.position).normalized;
        normalizedDir.z = 0;
        BulletObj.transform.up = normalizedDir;

        //Set the bullet's destination to cursor
        BulletObj.GetComponent<BulletObject>().SetDestination(mousePos);

        //Add bullet obj to list
        BulletList.Add(BulletObj);
    }

    //Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "GenericBullet":
                {
                    if (collision.gameObject.GetComponent<BulletObject>().CanHitPlayer)
                    {
                        collision.gameObject.SetActive(false);

                        if (characterState == CHARACTER_STATE.CHARACTERSTATE_NORMAL)
                        {
                            characterHealth -= 10;
                        }
                    }

                    goto default;
                }
            case "InvincibilityPowerup":
                {
                    characterState = CHARACTER_STATE.CHARACTERSTATE_INVINCIBLE;
                    Destroy(collision.gameObject);

                    goto default;
                }
            case "HealthPowerup":
                {
                    if(characterHealth >= characterHealthLimit)
                    {
                        return;
                    }

                    characterHealth += 20;

                    if(characterHealth > characterHealthLimit)
                    {
                        characterHealth = characterHealthLimit;
                    }

                    Destroy(collision.gameObject);

                    goto default;
                }
            default:
                {
                    return;
                }
        }
    }

    //Getters & Setters
    public void setPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public Vector2 getPosition()
    {
        return transform.position;
    }
    
    public float GetCharacterMovementSpeed()
    {
        return characterMovementSpeed;
    }

    public void SetCharacterMovementSpeed(float newMovementSpeed)
    {
        characterMovementSpeed = newMovementSpeed;
    }
    
    public float GetCharacterHealth()
    {
        return characterHealth;
    }
    public void DecreaseCharacterHealth(float reduceAmount)
    {
        characterHealth -= reduceAmount;
    }
    public void IncreaseCharacterHealth(float increaseAmount)
    {
        characterHealth += increaseAmount;
    }

    public void SetCharacterHealth(float newHealth)
    {
        characterHealth = newHealth;
    }
    
    public float GetCharacterAttackDamage()
    {
        return characterAttackDamage;
    }

    public void SetCharacterAttackDamage(uint newAttackDamage)
    {
        characterAttackDamage = newAttackDamage;
    }
}
