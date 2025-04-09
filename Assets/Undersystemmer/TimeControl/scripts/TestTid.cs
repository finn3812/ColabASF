using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tid.instance.TimeHasChanged.AddListener(ModtagEvent); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ModtagEvent(Tid.Modul time)
    {
        Debug.Log(time);
    }
}
