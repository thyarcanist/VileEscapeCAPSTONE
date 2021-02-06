using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL2Trigger : MonoBehaviour
{
    /// <summary>
    /// This will open the door to leave once all enemies are eliminated then active a inactive spawner group. Will be expanded in later iterations.
    /// Credit: Datorien Anderson
    /// </summary>

    public GameObject HiddenSpawner;
    public GameObject DoorToExit;
    public bool bIsClear = false;
    private bool bForScript = true;
    public GameObject[] AreaZombies;

    // Fetches the Script that has GetZombieCount, in Level2 this is in the Triggers part of the Hierarchy
    private GameObject GrabScript;

    // Audio Componenets
    public AudioSource pteSound;
    public AudioClip gateExitFX;

    // Start is called before the first frame update
    void Start()
    {
        GrabScript = GameObject.Find("Triggers/SubwayAreaTrigger");
        HiddenSpawner = GameObject.Find("Enemies/SpawnerAfterOpen");
        DoorToExit = GameObject.Find("ProgressionDoor/ExitDoor");

        // Sets set of spawners false on game start
       // HiddenSpawner.SetActive(false);

        // Set AudioSource to Audio Component
        pteSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProceedToExit();
        IsClear();
    }

    public void ProceedToExit()
    {
        // Access GetZombieCount script and checks to see if all zombies have been eliminated prior to getting inside.
        if (GrabScript.GetComponent<GetZombieCount>().zombieCountInLocation.Length == 0)

        {


            // need to do something about subway area spawners keep popping out zeds
            // this does get called true in the inspector, but can't make object deactivated
            bIsClear = true;

        }
    }
   

    public void IsClear()
    {
        if (bIsClear == true && bForScript == true )
        {
            // set exit spawners to active
            //HiddenSpawner.SetActive(true);
            // opens door to exit
            //DoorToExit.SetActive(false);
            // play sound 
            pteSound.PlayOneShot(gateExitFX, .7f);
            bForScript = false;
            return;
        }
    }
}
