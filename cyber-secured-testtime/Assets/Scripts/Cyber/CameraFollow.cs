﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject followMe;
    Vector3 previousSpot;
    void Start()
    {
        followMe = GameObject.Find("CyberCharacter");
        previousSpot = followMe.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {


        float changeX, changeY;
        changeX = (previousSpot.x - followMe.GetComponent<Transform>().position.x);
        changeY = (previousSpot.y - followMe.GetComponent<Transform>().position.y);
        this.gameObject.GetComponent<Transform>().position = new Vector3(this.gameObject.GetComponent<Transform>().position.x - changeX,
            this.gameObject.GetComponent<Transform>().position.y - changeY,
            this.gameObject.GetComponent<Transform>().position.z);
        previousSpot = followMe.GetComponent<Transform>().position;
    }
}
