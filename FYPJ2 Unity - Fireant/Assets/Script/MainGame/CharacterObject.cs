using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterObject : MonoBehaviour {

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
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        characterTexture = GetComponent<Image>();
    }
    // Update is called once per frame
    public void MainCharacterUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 a = new Vector3(0, 0, -90);
            //Create a bullet and add it as child to Scene control and object List
            GameObject BulletObj = Instantiate(Resources.Load("GenericBullet") as GameObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent.transform);

            BulletObj.transform.position.Set(gameObject.transform.position.x, gameObject.transform.position.y, 0);
            
            BulletObj.transform.Rotate(a);
            BulletObj.GetComponent<BulletObject>().BulletObjectInit();

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
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.position -= transform.up * characterMovementSpeed * Time.deltaTime;
            generalMovementScript.moveRight(characterTexture, characterMovementSpeed);
        }

        //Update bullets shot by player
        for(int i = 0; i < BulletList.Count; ++i)
        {
            BulletList[i].GetComponent<BulletObject>().BulletObjectUpdate();
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
