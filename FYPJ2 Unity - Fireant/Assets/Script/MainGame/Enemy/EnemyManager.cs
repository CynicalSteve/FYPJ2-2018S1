using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public List<GameObject> EnemyList = new List<GameObject>();

    public List<GameObject> BulletList = new List<GameObject>();

    GameObject theCharacter;

    // Use this for initialization
    public void EnemyManagerInit() {

        //If you want to spawn randomly for debug purpose
        //for (short i = 0; i < 1; ++i)
        //{
        //    Vector2 spawnPosition = new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y), 0);

        //    GameObject enemyObj = Instantiate(Resources.Load("TestEnemy"), spawnPosition, transform.rotation, transform) as GameObject;
        //    enemyObj.GetComponent<EnemyObject>().EnemyObjectInit();

        //    EnemyList.Add(enemyObj);
        //}

        theCharacter = GameObject.FindGameObjectWithTag("MainCharacter");

        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            GameObject enemyObj = gameObject.transform.GetChild(i).gameObject;
            enemyObj.GetComponent<EnemyBase>().EnemyInit();

            EnemyList.Add(enemyObj);
        }
    }
	
    public void EnemyManagerUpdate()
    {
        foreach(GameObject Enemy in EnemyList)
        {
            if(!Enemy || !Enemy.activeSelf)
            {
                EnemyList.Remove(Enemy);
                continue;
            }

            Enemy.GetComponent<EnemyBase>().EnemyUpdate();
        }

        //Update bullets shot by enemy
        foreach (GameObject BulletObj in BulletList)
        {
            if(!BulletObj)
            {
                BulletList.Remove(BulletObj);
                continue;
            }

            if (BulletObj.activeSelf)
            {
                BulletObj.GetComponent<BulletObject>().BulletObjectUpdate();
            }

            //if (BulletObj.transform.localPosition.x > theCanvas.transform.localPosition.x + Screen.width || BulletObj.transform.localPosition.x < theCanvas.transform.localPosition.x - Screen.width ||
            //    BulletObj.transform.localPosition.y > theCanvas.transform.localPosition.y + Screen.height || BulletObj.transform.localPosition.y < theCanvas.transform.localPosition.y - Screen.height)
            //{
            //    Destroy(BulletObj);
            //}

        if((BulletObj.transform.position - theCharacter.transform.position).magnitude > 500)
            {
                Destroy(BulletObj);
            }
        }
    }
}
