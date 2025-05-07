using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Tid : MonoBehaviour
{
    public enum Modul
    {
        PU,//0
        Matematik,//1
        Programmering,//2
        Idehistorie,//3
        Færdig//4
    }

    public enum Hverdage
    {
        Mandag,//0
        Tirsdag,//1
        Onsdag,//2
        Torsdag,//3
        Fredag//4
    }
    public Modul[] Skema;
    int counter = 0;
     
    DateTime specificTime;
    public float TimeScale = 10;
    public float waitTime = 5f; // Tiden i sekunder
    private float timer = 0f;
    Time time;
    public UnityEvent<Modul> TimeHasChanged = new UnityEvent<Modul>();
    public static Tid instance { get; private set; }
    bool IsRunning = true;
    

    private void Update()
    {
        /* specificTime=specificTime.AddSeconds(Time.deltaTime*TimeScale);
         clockText.text = specificTime.ToString("HH:mm:ss");*/
        timer += Time.deltaTime; // Øger timeren med tid siden sidste frame
        if (IsRunning == true)
        {
            if (timer >= waitTime)
            {
                Debug.Log("Den specificerede tid er gået!");
                //enabled = false; // Stopper Update fra at køre yderligere
                timer = 0f;
                TimeHasChanged.Invoke(Skema[counter]);
                counter++;
                if(counter >= Skema.Length)
                    IsRunning= false;   
            }
        }
        
    }

    public string GetTimeStatus()
    {
        int a = (int)counter / 4;
        return "Dag: "+((Hverdage)a).ToString() + " " +" Fag: "+ ((Modul)Skema[counter]).ToString();
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
