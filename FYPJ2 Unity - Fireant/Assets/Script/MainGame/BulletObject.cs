﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField]
    public float BulletMovementSpeed = 1500;
    public float BulletDamage = 10;

    GeneralMovement generalMovementScript;
    EnemyManager enemyManager;
    CharacterObject theCharacter;
    Vector3 Destination;
    Vector3 Direction;

    public bool CanHitPlayer = false;

    public void BulletObjectInit()
    {
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        Destination = new Vector3(0, 0, 0);

        theCharacter = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<CharacterObject>();
    }

    // Update is called once per frame
    public void BulletObjectUpdate()
    {
        generalMovementScript.moveBy(gameObject, BulletMovementSpeed, Direction);

        if (gameObject.transform.localPosition.x > theCharacter.theCanvas.transform.localPosition.x + theCharacter.theCanvas.GetComponent<RectTransform>().rect.width)
        {
            Destroy(gameObject);
        }
    }

    public void SetDestination(Vector3 newDestination)
    {
        Destination = newDestination;
    }

    public Vector3 GetDestination()
    {
        return Destination;
    }

    public void SetDirection(Vector3 newDirection)
    {
        Direction = newDirection;
    }
}
