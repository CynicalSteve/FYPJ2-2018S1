using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField]
    const float BulletMovementSpeed = 50;

    GeneralMovement generalMovementScript;
    Vector3 Destination;

    public void BulletObjectInit()
    {
        generalMovementScript = GameObject.FindGameObjectWithTag("GeneralScripts").GetComponent<GeneralMovement>();
        Destination = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    public void BulletObjectUpdate()
    {
        if (generalMovementScript.moveTo(gameObject, BulletMovementSpeed, Destination))
        {
            gameObject.SetActive(false);
        }
    }

    public void SetDestination(Vector3 newDestination)
    {
        Destination = newDestination;
    }

    public Vector3 GetDesitination()
    {
        return Destination;
    }
}
