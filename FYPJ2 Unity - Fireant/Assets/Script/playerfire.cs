﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerfire : MonoBehaviour
{
    [SerializeField]
    GameObject bulletObj;//points to bullet pref
                         // Use this for initialization
    float destroybullet = 0;
                       
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletObj, transform.position, transform.rotation);
        }
        Destroy(gameObject, 3);
    }
}