﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movethingy1 : MonoBehaviour
{
    bool change1 = false;
    bool change3 = false;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(change3==true)
        {
            transform.Translate(-15 * Time.deltaTime, 0, 0);
            if(transform.position.x<20)
            {
                change3 = false;
            }
        }
        else if (transform.position.x < 80&&change3==false)
        {
            transform.Translate(15 * Time.deltaTime, 0, 0);
            if(transform.position.x>=80)
            {
                change3 = true;
            }
        }
        
        Debug.Log(transform.position.x);
    }
}