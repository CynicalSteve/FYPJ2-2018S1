using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterObject : MonoBehaviour {

    [SerializeField]
    uint characterMovementSpeed = 10;

    [SerializeField]
    uint characterHealth = 100;

    [SerializeField]
    uint characterAttackSpeed = 10;

    [SerializeField]
    uint characterAttackDamage = 10;

    GeneralMovement generalMovementScript = new GeneralMovement();
    Image characterTexture;

    // Use this for initialization
    void Start()
    {
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        characterTexture = GetComponent<Image>();
    }
    // Update is called once per frame
    public void MainCharacterUpdate()
    {
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
    }

    public void setPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public Vector2 getPosition()
    {
        return transform.position;
    }
    
    public uint GetCharacterMovementSpeed()
    {
        return characterMovementSpeed;
    }

    public void SetCharacterMovementSpeed(uint newMovementSpeed)
    {
        characterMovementSpeed = newMovementSpeed;
    }
    
    public uint GetCharacterHealth()
    {
        return characterHealth;
    }

    public void SetCharacterHealth(uint newHealth)
    {
        characterHealth = newHealth;
    }
    
    public uint GetCharacterAttackSpeed()
    {
        return characterAttackSpeed;
    }

    public void SetCharacterAttackSpeed(uint newAttackSpeed)
    {
        characterAttackSpeed = newAttackSpeed;
    }
    
    public uint GetCharacterAttackDamage()
    {
        return characterAttackDamage;
    }

    public void SetCharacterAttackDamage(uint newAttackDamage)
    {
        characterAttackDamage = newAttackDamage;
    }
}
