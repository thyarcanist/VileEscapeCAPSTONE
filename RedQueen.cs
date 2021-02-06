using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedQueen : MonoBehaviour
{
    // Controls the Alarm --
    // Alarm is on 

    public AudioSource klaxon;
    public AudioClip alarmRing;

    // Starts the Game with the Alarm Ringing as intended
    private bool isAlarm;

    // GetZombieCount GetScripts
    public GameObject zedCountScript;

    // Material
    public Light alarmLight;

    public void Start()
    {
        // Gets Reference to GameObjects and Components
        klaxon = GetComponent<AudioSource>();
        zedCountScript = GameObject.Find("Triggers/SubwayAreaTrigger");

        alarmLight.GetComponent<Light>();
        isAlarm = true;
    }

    public void Update()
    {
        AlarmLogic();
    }
    public void AlarmLogic()
    {
        // if isAlarm is not True
        if (isAlarm)
        {
            if (zedCountScript.GetComponent<GetZombieCount>().zombieCountInLocation.Length >= 1)
            {
                AlarmPlay();
            }

        }
        else if (zedCountScript.GetComponent<GetZombieCount>().zombieCountInLocation.Length == 0)
        {
            AlarmStop();
        }
        else
        {
            return;
        }
    }

    public void AlarmPlay()
    {
        // Play Alarm
        if (!klaxon.isPlaying)
        {
            klaxon.Play();
        }      
        alarmLight.color = Color.Lerp(Color.red, Color.black, Mathf.PingPong(Time.time, 1));
    }

    public void AlarmStop()
    {
        klaxon.Stop();
        alarmLight.color = Color.green;
    }
}
