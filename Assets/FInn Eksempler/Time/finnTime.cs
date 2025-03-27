using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finnTime : MonoBehaviour
{
    DateTime tid;
    float timeStep=10;
    // Start is called before the first frame update
    void Start()
    {
        tid = new DateTime(2025, 4, 22, 14, 30, 15);
        Debug.Log(tid.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        tid=tid.AddSeconds(Time.deltaTime*timeStep);
        Debug.Log(tid.ToString());
    }
}
