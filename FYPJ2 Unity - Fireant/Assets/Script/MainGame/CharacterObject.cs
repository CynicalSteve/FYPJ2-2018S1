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
    float characterAttackSpeed = 10;

    [SerializeField]
    float characterAttackDamage = 10;

    GeneralMovement generalMovementScript;
    Image characterTexture;
    
    List<GameObject> BulletList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        characterTexture = GetComponent<Image>();
    }
    // Update is called once per frame
    public void MainCharacterUpdate()
    {
        animator.SetInteger("states", 1);

        //Shoot
        if (Input.GetMouseButtonDown(0))
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
            animator.SetInteger("states", 2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.position -= transform.up * characterMovementSpeed * Time.deltaTime;
            generalMovementScript.moveRight(characterTexture, characterMovementSpeed);
            animator.SetInteger("states", 0);
        }
        if(Input.GetKey(KeyCode.Z))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
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
    }

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

    public void SetCharacterHealth(float newHealth)
    {
        characterHealth = newHealth;
    }
    
    public float GetCharacterAttackSpeed()
    {
        return characterAttackSpeed;
    }

    public void SetCharacterAttackSpeed(float newAttackSpeed)
    {
        characterAttackSpeed = newAttackSpeed;
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
