using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    List<GameObject> EnemyList = new List<GameObject>();

    // Use this for initialization
    void Start () {

        for (short i = 0; i < 5; ++i)
        {
            GameObject enemyObj = Instantiate(Resources.Load("TestEnemy"), gameObject.transform, true) as GameObject;
            enemyObj.transform.position.Set(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);

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
