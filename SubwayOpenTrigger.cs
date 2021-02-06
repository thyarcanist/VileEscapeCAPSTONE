using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayOpenTrigger : MonoBehaviour
{
    public GameObject GetKeyScript;
    public GameObject OpenThisDoor;

    // Audio Component
    public AudioSource SOTAudio;
    public AudioClip ShutterOpen;

    public void Start()
    {
        // Gets Reference To These Items
        GetKeyScript = GameObject.Find("Triggers/RoomBTrigger");
        OpenThisDoor = GameObject.Find("ProgressionDoor/ShutterClosed");

        SOTAudio = GetComponent<AudioSource>();

    }



    public void OnTriggerEnter(Collider other)
    {
        if (GetKeyScript.GetComponent<LVL2GoldOpen>().bHasKey == true)
        {
            if (other.gameObject.tag == "PlayerRoot")
            {
                // Play Sound
                SOTAudio.PlayOneShot(ShutterOpen);
                //Deactives ShutterClosed GameObject
                OpenThisDoor.SetActive(false);
                // sets to false so key is then consumed, from the LVL2GoldOpen Script
                GetKeyScript.GetComponent<LVL2GoldOpen>().bHasKey = false;
            }
        }
    }
}
