﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public bool white;
    public bool highlighted = false;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(highlighted == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {

            }
        }
    }
}
