using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CharacterObject : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    Canvas theCanvas;

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
    [SerializeField]
    Text moneytext;
    [SerializeField]
    public float money = 0;
    public int lol = 0;
    public Vector3 respawnpoint;
    public enum CHARACTER_STATE
    {
        CHARACTERSTATE_NORMAL,
        CHARACTERSTATE_INVINCIBLE,

        TOTAL_CHARACTERSTATE
    }

    CHARACTER_STATE characterState;

    GeneralMovement generalMovementScript;
    Image characterTexture;
    SceneTransition test;

    List<GameObject> BulletList = new List<GameObject>();

    float InvincibilityTimer = 0;
    float fireRateTimer = 0;
    bool canShoot = true, canJump = true;
    //lol1 is to pass the boolean to the scenetransition script to let it know whether it has touched the checkpoint
    public float lol1 = 0;

    bool facingLeft = false;
    double moretest;
    // Use this for initialization
    void Start()
    {
        //Ignore collision with own Enemy layer
        Physics2D.IgnoreLayerCollision(0, 9);

        animator = this.GetComponent<Animator>();
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        characterTexture = GetComponent<Image>();
        gameObject.transform.GetChild(0).GetComponent<Image>();
    }
    // Update is called once per frame
    public void MainCharacterUpdate()
    {
        animator.SetInteger("states", 1);
        charhealth.text = "Health : " + characterHealth.ToString() + "/" + "100";
        moneytext.text = "Money : " + money.ToString();
        Debug.Log("THIS IS THE THINGY "+PlayerPrefs.GetFloat("respawntocheckpoint"));
        Debug.Log("WHERE THE PLAYER IS GOING TO SPAWN "+respawnpoint);
        Debug.Log("THE CHARACTER POSITION"+transform.position);
        if (PlayerPrefs.GetFloat("respawntocheckpoint") >= 1)
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("respawnhere"),PlayerPrefs.GetFloat("respawnhere1"),0);
        }

        //Crosshair snap to mouse position
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
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

        KeyInputs();

        //Update bullets shot by player
        foreach (GameObject BulletObj in BulletList)
        {
            if (!BulletObj)
            {
                BulletList.Remove(BulletObj);
                continue;
            }

            if (BulletObj.activeSelf)
            {
                BulletObj.GetComponent<BulletObject>().BulletObjectUpdate();
            }
        }

        //Update Character States
        if (characterState == CHARACTER_STATE.CHARACTERSTATE_INVINCIBLE)
        {
            InvincibilityTimer += Time.deltaTime;

            if(InvincibilityTimer >= InvincibilityTimeLimit)
            {
                InvincibilityTimer = 0;
                characterState = CHARACTER_STATE.CHARACTERSTATE_NORMAL;
            }
        }
    }

    private void KeyInputs()
    {
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
            generalMovementScript.moveUp(characterTexture, characterMovementSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            generalMovementScript.moveDown(characterTexture, characterMovementSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (gameObject.GetComponent<RectTransform>().localPosition.x > theCanvas.transform.position.x - (theCanvas.GetComponent<RectTransform>().rect.width * 0.5f))
            {
                
                generalMovementScript.moveLeft(characterTexture, characterMovementSpeed);
                animator.SetInteger("states", 0);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            //If character is out of left dead zone
            if (gameObject.transform.position.x >= theCanvas.transform.position.x)
            {
                //Move all other objects in the scene
                for (int i = 0; i < theCanvas.transform.childCount; ++i)
                {
                    GameObject theGameObject = theCanvas.transform.GetChild(i).gameObject;

                    //If the gameobject is UI layer or the character object
                    if (theGameObject.layer == 5 || theGameObject.tag == "MainCharacter")
                    {
                        continue;
                    }

                    generalMovementScript.moveLeft(theGameObject, characterMovementSpeed);
                }

            }
            else //Move the character
            {
                generalMovementScript.moveRight(characterTexture, characterMovementSpeed);
            }

            animator.SetInteger("states", 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400);
            canJump = false;
        }

        //Update Character States
        if (characterState == CHARACTER_STATE.CHARACTERSTATE_INVINCIBLE)
        {
            InvincibilityTimer += Time.deltaTime;

            if (InvincibilityTimer >= InvincibilityTimeLimit)
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
    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "GenericBullet":
                {
                    if (other.gameObject.GetComponent<BulletObject>().CanHitPlayer)
                    {
                        Destroy(other.gameObject);

                        if (characterState == CHARACTER_STATE.CHARACTERSTATE_NORMAL)
                        {
                            characterHealth -= 10;
                            if(characterHealth<=0)
                                SceneManager.LoadScene("GameOver");
                        }
                    }

                    goto default;
                }
            case "Ground":
                {
                    if (Physics2D.GetIgnoreCollision(gameObject.GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>()))
                    {
                        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>(), false);
                    }

                    goto default;
                }
            case "InvincibilityPowerup":
                {
                    characterState = CHARACTER_STATE.CHARACTERSTATE_INVINCIBLE;
                    Destroy(other.gameObject);

                    goto default;
                }
            case "HealthPowerup":
                {
                    if (characterHealth >= characterHealthLimit)
                    {
                        return;
                    }

                    characterHealth += 20;

                    if (characterHealth > characterHealthLimit)
                    {
                        characterHealth = characterHealthLimit;
                    }

                    Destroy(other.gameObject);

                    goto default;
                }
            default:
                break;
        }

         if(other.gameObject.name == "IgnoreCollisionTrigger")
        {
            if (!Physics2D.GetIgnoreCollision(gameObject.GetComponent<Collider2D>(), other.gameObject.transform.parent.GetComponent<Collider2D>()))
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), other.gameObject.transform.parent.GetComponent<Collider2D>());
            }
        }
        if (other.gameObject.tag == "Checkpoint")
        {
            respawnpoint = other.transform.position;
            Destroy(other.gameObject);
            lol++;
            PlayerPrefs.SetFloat("savecheckpoint", lol);
            PlayerPrefs.SetFloat("respawnhere", respawnpoint.x);
            PlayerPrefs.SetFloat("respawnhere1", respawnpoint.y);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "InvincibilityPowerup":
                {
                    characterState = CHARACTER_STATE.CHARACTERSTATE_INVINCIBLE;
                    Destroy(collision.gameObject);

                    goto default;
                }
            case "HealthPowerup":
                {
                    if (characterHealth >= characterHealthLimit)
                    {
                        return;
                    }

                    characterHealth += 20;

                    if (characterHealth > characterHealthLimit)
                    {
                        characterHealth = characterHealthLimit;
                    }

                    Destroy(collision.gameObject);

                    goto default;
                }
            case "Ground":
                {
                    canJump = true;
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
