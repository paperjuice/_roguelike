﻿using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {


    public static float dungeonLevel = 0f;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = Vector3.zero;

        print(dungeonLevel.ToString());
    }





}
