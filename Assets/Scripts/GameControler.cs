﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void Ruch();
    public static event Ruch ic;
    [SerializeField] GameObject hero;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ic();
           
        }
    //Camera.main.transform.LookAt(hero.transform.position,Vector3.up);

    }


}
