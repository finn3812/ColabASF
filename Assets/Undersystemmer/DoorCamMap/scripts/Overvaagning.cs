using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overvaagning : MonoBehaviour
{
    private List<Camera> cameraList = new List<Camera>();
    // Start is called before the first frame update
    void Start()
    {
   //     Camera[] cameras = GameObject.FindGameObjectsWithTag("VideoCamera").Select(obj => obj.GetComponent<Camera>()).ToArray();
    //    cameraList.AddRange(cameras);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
