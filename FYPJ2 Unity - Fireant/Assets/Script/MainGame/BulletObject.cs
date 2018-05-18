using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    [SerializeField]
    const float BulletMovementSpeed = 50;

    GeneralMovement generalMovementScript;
    EnemyManager enemyManager;
    CharacterObject theCharacter;
    Vector3 Destination;

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
        if (generalMovementScript.moveTo(gameObject, BulletMovementSpeed, Destination))
        {
            gameObject.SetActive(false);
        }

        //for(int i = 0; i < enemyManager.EnemyList.Count; ++i)
        //{
        //    if((enemyManager.EnemyList[i].transform.position - gameObject.transform.position).magnitude <= 0.3f)
        //    {
        //        gameObject.SetActive(false);
        //        enemyManager.EnemyList[i].GetComponent<EnemyObject>().DecreaseHealth(10);
        //    }
        //}

        //if(CanHitPlayer)
        //{
        //    if ((theCharacter.transform.position - gameObject.transform.position).magnitude <= 0.3f)
        //    {
        //        gameObject.SetActive(false);
        //        theCharacter.GetComponent<CharacterObject>().DecreaseCharacterHealth(10);
        //    }
        //}
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
