using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadEvents : MonoBehaviour
{
    // Variables
    public Light shelterLight;
    public GameObject heavyGate;
    public GameObject waterCollectable;
    public GameObject RewardObject;

    // Bools
    public bool bFirstEncounter = false;
    public bool bGotWater = false;
    public bool bGotFetchQuest = false;
    public bool bGotFetchComplete = false;
    public bool bIsGateOpen = false;
    public bool bHasGateSound = false;
    public bool isCompleted = false;

    // Audio
    public AudioSource progressionLocker;
    public AudioClip gateLiftFX;
    public AudioClip sWaterPickUp;
    public AudioClip sThanksMurmur;
    public AudioClip sDoMurmur;


    private void Start()
    {
        // Finds References to these Objects
        waterCollectable = GameObject.FindGameObjectWithTag("WaterBottle");
        heavyGate = GameObject.FindGameObjectWithTag("HeavyGate");
        shelterLight = GameObject.Find("ProgressionLocker/ProgressionManager/sLight").GetComponent<Light>();

        RewardObject.SetActive(false);
        shelterLight.enabled = false;
        GetQuestAudio();
    }

    private void Update()
    {
        WaterQuest();
        OpenTheGate();
    }

    private void WaterQuest()
    {
        if (waterCollectable.activeInHierarchy == false)
        {
            bGotWater = true;
            ChangeLight();           
        }
        else
        {
            return;
        }
    }

    public void ChangeLight()
    {
        shelterLight.intensity = 0;
        shelterLight.color = Color.clear;
    }

    private void OpenTheGate()
    {
        // If josh got the water continue
        if (bGotFetchComplete == true)
        {
            bIsGateOpen = true;

            if (bIsGateOpen == true && bHasGateSound == false)
            {
                progressionLocker.PlayOneShot(gateLiftFX);
                bHasGateSound = true;
            }

            if (heavyGate.activeSelf && !isCompleted)
            {
                Debug.Log("AmActive");
                heavyGate.SetActive(false);
                isCompleted = true;
            }


        }
    }

    public void CloseTheGate()
    {
        if (heavyGate.activeSelf == false && isCompleted)
        {
            heavyGate.SetActive(true);
        }
    }

    public void GetQuestAudio()
    {
        progressionLocker = GetComponent<AudioSource>();
    }

    public void PlayItemDelivered()
    {
        progressionLocker.PlayOneShot(sThanksMurmur); // Thank you spoken for delivering item
        RewardObject.SetActive(true); // Item the player is awarded for delivering item.
    }

    public void NearQuestGiver()
    {
        progressionLocker.PlayOneShot(sDoMurmur);
        // Plays when the player is in the range of the quest giver
    }

    public void PlayItemGrabbed()
    {
        // sound that plays when water is picked up
        progressionLocker.PlayOneShot(sWaterPickUp);
    }

    public void WelcomeLight()
    {
        // Light that beckons the player near
        StartCoroutine(LightPFI());
    }

    IEnumerator LightPFI()
    {
        shelterLight.enabled = true;
        yield return new WaitForSeconds(1);
        shelterLight.enabled = false;
        yield return new WaitForSeconds(1);
        shelterLight.enabled = true;
        yield return new WaitForSeconds(1);
        shelterLight.enabled = false; // makes sure the light is turned off

    }

} 
