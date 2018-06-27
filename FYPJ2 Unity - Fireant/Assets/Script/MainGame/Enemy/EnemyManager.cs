using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public List<GameObject> EnemyList = new List<GameObject>();

    public List<GameObject> BulletList = new List<GameObject>();
    // Use this for initialization
    public void EnemyManagerInit() {

        for (short i = 0; i < 5; ++i)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y), 0);

            GameObject enemyObj = Instantiate(Resources.Load("TestEnemy"), spawnPosition, transform.rotation, transform) as GameObject;
            enemyObj.GetComponent<EnemyObject>().EnemyObjectInit();

            EnemyList.Add(enemyObj);
        }
    }
	
    public void EnemyManagerUpdate()
    {
        foreach(GameObject Enemy in EnemyList)
        {
            if(!Enemy)
            {
                EnemyList.Remove(Enemy);
                continue;
            }

            Enemy.GetComponent<EnemyObject>().EnemyObjectUpdate();
        }

        //Update bullets shot by enemy
        foreach (GameObject BulletObj in BulletList)
        {
            if (BulletObj.activeInHierarchy)
            {
                BulletObj.GetComponent<BulletObject>().BulletObjectUpdate();
            }
        }
    }
}
