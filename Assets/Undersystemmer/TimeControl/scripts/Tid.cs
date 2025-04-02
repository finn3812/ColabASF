using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class Tid : MonoBehaviour
{
    public TMP_Text clockText; // Tilknyt din UI Text her
    DateTime specificTime;
    public float TimeScale = 10;
    public float waitTime = 5f; // Tiden i sekunder
    private float timer = 0f;
    Time time;
    public UnityEvent TimeHasChanged = new UnityEvent();
    public static Tid instance { get; private set; }
    void Start()
    {
        // Formatér og vis det specificerede tidspunkt

        specificTime = new DateTime(2025, 3, 26, 14, 30, 00);
        clockText.text = specificTime.ToString("HH:mm:ss");
    }

    private void Update()
    {
        /* specificTime=specificTime.AddSeconds(Time.deltaTime*TimeScale);
         clockText.text = specificTime.ToString("HH:mm:ss");*/
        timer += Time.deltaTime; // Øger timeren med tid siden sidste frame

        if (timer >= waitTime)
        {
            Debug.Log("Den specificerede tid er gået!");
            //enabled = false; // Stopper Update fra at køre yderligere
            timer = 0f;
            TimeHasChanged.Invoke();
        }
    }
   
    void Awake()
    {
        if (instance != null && this != instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

}
