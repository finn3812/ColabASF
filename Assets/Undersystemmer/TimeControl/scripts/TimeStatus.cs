using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeStatus : MonoBehaviour
{
    public TMP_Text clockText;
    bool IsStatusActive=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            IsStatusActive = !IsStatusActive;
            
        }
        if (IsStatusActive == true)
        {
            clockText.gameObject.SetActive(true);
            clockText.text = Tid.instance.GetTimeStatus();
        }
        else
        {
            clockText.gameObject.SetActive(false);
        }

        

    }
}
