using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField]
    float BulletMovementSpeed = 50;

    GeneralMovement generalMovementScript;

    public void BulletObjectInit()
    {
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
    }

    // Update is called once per frame
    public void BulletObjectUpdate()
    {
        generalMovementScript.moveRight(gameObject, BulletMovementSpeed);
    }
}
