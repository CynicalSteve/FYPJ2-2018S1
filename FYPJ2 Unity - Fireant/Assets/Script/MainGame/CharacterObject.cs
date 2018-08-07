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
    public Canvas theCanvas;

    [SerializeField]
    float characterMovementSpeed = 10;

    [SerializeField]
    float characterHealth = 100;

    [SerializeField]
    float characterHealthLimit = 100;
    
    [SerializeField]
    float InvincibilityTimeLimit = 5;

    [SerializeField]
    float BerserkTimeLimit = 8;

    [SerializeField]
    Text charhealth;
    [SerializeField]
    Text moneytext;
    [SerializeField]
    Image HealthBar;

    public float money = 0;
    public int lol = 0;
    public Vector3 respawnpoint;

    SpriteRenderer spriteRenderer = null;
    private Color defaultColour;
    private Color BerserkColour;
    private float BerserkMovementModifer = 1, BerserkDamageModifier = 1, BerserkShootSpeedModifier = 1;

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
    private List<WeaponBase> WeaponsEquipped = new List<WeaponBase>();
    private int CurrentWeapon = 0;

    bool isBerserk = false;
    float InvincibilityTimer = 0, BerserkTimer = 0;
    float fireRateTimer = 0;
    bool canShoot = true, canJump = true;
    //lol1 is to pass the boolean to the scenetransition script to let it know whether it has touched the checkpoint
    public float lol1 = 0;

    bool facingLeft = false;
    double moretest;

    Transform playerWeapon;
    // Use this for initialization
    void Start()
    {
        //Ignore collision with own Enemy layer
        Physics2D.IgnoreLayerCollision(0, 9);

        animator = this.GetComponent<Animator>();
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        characterTexture = GetComponent<Image>();
        gameObject.transform.GetChild(0).GetComponent<Image>();

        WeaponsEquipped.Add(new Pistol());
        WeaponsEquipped.Add(new RPG());

        //Init player weapon sprite
        playerWeapon = gameObject.transform.Find("PlayerWeapon");
        playerWeapon.GetComponent<Image>().sprite = WeaponsEquipped[CurrentWeapon].WeaponSprite;

        playerWeapon.localPosition = gameObject.transform.position;

        //Get player color
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColour = spriteRenderer.color;
        BerserkColour = new Color(0.7803f, 0.1490f, 0.1490f, 1);
    }
    // Update is called once per frame
    public void MainCharacterUpdate()
    {
        animator.SetInteger("states", 1);
        charhealth.text = "Health : " + characterHealth.ToString() + "/" + "100";
        moneytext.text = "Money : " + money.ToString();
        Debug.Log("THIS IS THE THINGY " + PlayerPrefs.GetFloat("respawntocheckpoint"));
        Debug.Log("WHERE THE PLAYER IS GOING TO SPAWN " + respawnpoint);
        Debug.Log("THE CHARACTER POSITION" + transform.position);

        //-------PROBLEMATIC CODE------//
        if (PlayerPrefs.GetFloat("respawntocheckpoint") >= 1)
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("respawnhere"), PlayerPrefs.GetFloat("respawnhere1"), 0);
            PlayerPrefs.DeleteAll();
        }
        //----------------------------//

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

        //Weapon Rot
        Vector3 normalizedDir = (mousePos - gameObject.transform.position).normalized;
        normalizedDir.z = 0;
        playerWeapon.up = normalizedDir;

        KeyInputs();

        //Update bullets shot by player
        foreach (GameObject BulletObj in BulletList)
        {
            if (!BulletObj || !BulletObj.activeSelf)
            {
                BulletList.Remove(BulletObj);
                continue;
            }

            if (BulletObj.activeSelf)
            {
                if (BulletObj.tag == "GenericBullet")
                {
                    BulletObj.GetComponent<BulletObject>().BulletObjectUpdate();
                }
                else if(BulletObj.tag == "GenericRocket")
                {
                    BulletObj.GetComponent<RocketObject>().RocketObjectUpdate();
                }
            }

            //Check if out of screen
            if(BulletObj.transform.position.x > theCanvas.transform.localPosition.x + Screen.width || BulletObj.transform.position.x < theCanvas.transform.localPosition.x - Screen.width ||
                BulletObj.transform.position.y > theCanvas.transform.localPosition.y + Screen.height || BulletObj.transform.position.y < theCanvas.transform.localPosition.y - Screen.height)
            {
                Destroy(BulletObj);
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
                Destroy(gameObject.transform.Find("shield(Clone)").gameObject);
            }
        }
        if(isBerserk)
        {
            BerserkTimer += Time.deltaTime;

            if(BerserkTimer >= BerserkTimeLimit)
            {
                BerserkTimer = 0;
                isBerserk = false;
                spriteRenderer.color = defaultColour;

                //Modifiers
                BerserkDamageModifier = BerserkMovementModifer = BerserkShootSpeedModifier = 1;
            }
        }
    }

    private void KeyInputs()
    {
        //Shoot
        if (Input.GetMouseButton(0))
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
                fireRateTimer += Time.deltaTime * BerserkShootSpeedModifier;

                if (fireRateTimer >= WeaponsEquipped[CurrentWeapon].SecondsBetweenShots)
                {
                    canShoot = true;
                    fireRateTimer = 0;
                }
            }
        }

        //Switching weapons
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //forward
        {
            if (CurrentWeapon + 1 == WeaponsEquipped.Count)
            {
                //Wrap around
                CurrentWeapon = 0;
            }
            else
            {
                ++CurrentWeapon;
            }

            playerWeapon.GetComponent<Image>().sprite = WeaponsEquipped[CurrentWeapon].WeaponSprite;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) //backwards
        {
            if (CurrentWeapon == 0)
            {
                //Wrap around
                CurrentWeapon = WeaponsEquipped.Count - 1;
            }
            else
            {
                --CurrentWeapon;
            }

            playerWeapon.GetComponent<Image>().sprite = WeaponsEquipped[CurrentWeapon].WeaponSprite;
        }
        
        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            generalMovementScript.moveUp(characterTexture, characterMovementSpeed);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            if (gameObject.GetComponent<RectTransform>().localPosition.x > theCanvas.transform.position.x - (theCanvas.GetComponent<RectTransform>().rect.width * 0.5f))
            {
                generalMovementScript.moveLeft(characterTexture, characterMovementSpeed * BerserkMovementModifer);
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

                    generalMovementScript.moveLeft(theGameObject, characterMovementSpeed * BerserkMovementModifer);
                }

            }
            else //Move the character
            {
                generalMovementScript.moveRight(characterTexture, characterMovementSpeed * BerserkMovementModifer);
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
        GameObject Projectile = null;

        //Get mouse position by converting the pos from screen space to world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        //Create a projectile and add it as child to Scene control and object List
        if (WeaponsEquipped[CurrentWeapon].WeaponName == "RPG")
        {
            Projectile = Instantiate(Resources.Load("GenericRocket") as GameObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent.transform);

            //Init Rocket variables
            Projectile.GetComponent<RocketObject>().RocketObjectInit();

            //Set the rocket's damage
            Projectile.GetComponent<RocketObject>().RocketDamage= WeaponsEquipped[CurrentWeapon].WeaponDamage;

            //Rotate the bullet in the direction of destination
            Vector3 normalizedDir = (mousePos - Projectile.transform.position).normalized;
            normalizedDir.z = 0;
            Projectile.transform.up = normalizedDir;

            //Set the bullet's destination to cursor
            Projectile.GetComponent<RocketObject>().SetDirection(mousePos - gameObject.transform.position);
        }
        else 
        {
            Projectile = Instantiate(Resources.Load("GenericBullet") as GameObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent.transform);

            //Init Bullet variables
            Projectile.GetComponent<BulletObject>().BulletObjectInit();

            //Set the bullet's damage
            Projectile.GetComponent<BulletObject>().BulletDamage = WeaponsEquipped[CurrentWeapon].WeaponDamage;

            //Rotate the bullet in the direction of destination
            Vector3 normalizedDir = (mousePos - Projectile.transform.position).normalized;
            normalizedDir.z = 0;
            Projectile.transform.up = normalizedDir;

            //Set the bullet's destination to cursor
            Projectile.GetComponent<BulletObject>().SetDirection(mousePos - gameObject.transform.position);
        }

        //Set the pos of bullet to the character pos
        Projectile.transform.position.Set(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        
        //Add bullet obj to list
        BulletList.Add(Projectile);
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
                            DecreaseCharacterHealth(other.gameObject.GetComponent<BulletObject>().BulletDamage * BerserkDamageModifier);

                            if (characterHealth <= 0)
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
                    if(characterState == CHARACTER_STATE.CHARACTERSTATE_INVINCIBLE)
                    {
                        //If player already invincible, reset the timer
                        InvincibilityTimer = InvincibilityTimeLimit;
                        return;
                    }

                    characterState = CHARACTER_STATE.CHARACTERSTATE_INVINCIBLE;
                    Destroy(other.gameObject);

                    //Create a shield around player
                    GameObject shield = Instantiate(Resources.Load("shield") as GameObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
                    Image shieldImage = shield.GetComponent<Image>();
                    var tempcolour = shieldImage.color;
                    tempcolour.a = 0.5f;
                    shieldImage.color = tempcolour;
                    
                    goto default;
                }
            case "HealthPowerup":
                {
                    if (characterHealth >= characterHealthLimit)
                    {
                        return;
                    }

                    IncreaseCharacterHealth(20);

                    if (characterHealth > characterHealthLimit)
                    {
                        characterHealth = characterHealthLimit;
                    }

                    Destroy(other.gameObject);

                    goto default;
                }
            case "BerserkPowerup":
                {
                    if(isBerserk)
                    {
                        return;
                    }

                    isBerserk = true;
                    spriteRenderer.color = BerserkColour;
                    Destroy(other.gameObject);

                    //Modifiers
                    BerserkDamageModifier = 0.75f;
                    BerserkMovementModifer = 1.25f;
                    BerserkShootSpeedModifier = 1.25f;

                    goto default;
                }
            case "fall":
                {
                    SceneManager.LoadScene("GameOver");
                    break;
                }
            case "Weapon":
                {
                    //Check if player already has the weapon
                    foreach(WeaponBase Weapon in WeaponsEquipped)
                    {
                        if(Weapon.WeaponName == other.name)
                        {
                            return;
                        }
                    }
                    
                    if(other.name == "LMG")
                    {
                        WeaponsEquipped.Add(new LMG());
                    }
                    else if(other.name == "Minigun")
                    {
                        WeaponsEquipped.Add(new Minigun());
                    }
                    else if (other.name == "Pistol")
                    {
                        WeaponsEquipped.Add(new Pistol());
                    }
                    else if (other.name == "Revolver")
                    {
                        WeaponsEquipped.Add(new Revolver());
                    }
                    else if (other.name == "Rifle")
                    {
                        WeaponsEquipped.Add(new Rifle());
                    }
                    else if (other.name == "AK47")
                    {
                        WeaponsEquipped.Add(new AK47());
                    }
                    else if (other.name == "Carbine")
                    {
                        WeaponsEquipped.Add(new Carbine());
                    }
                    else if (other.name == "RPG")
                    {
                        WeaponsEquipped.Add(new RPG());
                    }

                    //Equip the newly picked up weapon
                    CurrentWeapon = WeaponsEquipped.Count - 1;

                    break;
                }
            case "LevelEnd":
                {
                    SceneTransition sceneTransition = new SceneTransition();
                    sceneTransition.Tolevelend();
                    break;
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
        //Update Health Bar Appearance
        HealthBar.fillAmount = characterHealth / characterHealthLimit;

        characterHealth -= reduceAmount;
    }
    public void IncreaseCharacterHealth(float increaseAmount)
    {
        //Update Health Bar Appearance
        HealthBar.fillAmount = characterHealth / characterHealthLimit;

        characterHealth += increaseAmount;
    }

    public void SetCharacterHealth(float newHealth)
    {
        //Update Health Bar Appearance
        HealthBar.fillAmount = characterHealth / characterHealthLimit;

        characterHealth = newHealth;
    }
}
