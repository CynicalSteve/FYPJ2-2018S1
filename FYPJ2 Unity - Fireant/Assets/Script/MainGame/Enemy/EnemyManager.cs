using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    List<GameObject> EnemyList = new List<GameObject>();

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
        for (int i = 0; i < EnemyList.Count; ++i)
        {
            EnemyList[i].GetComponent<EnemyObject>().EnemyObjectUpdate();
        }

    }
}
