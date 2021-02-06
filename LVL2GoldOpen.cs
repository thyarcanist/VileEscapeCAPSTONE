using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Resources;

public class LVL2GoldOpen : MonoBehaviour
{
    /// <summary>
    /// Purpose: Room Interactions Manager for Level2
    /// Room the player enters RoomB it will open the passage to get to the SubwayTrainArea, should be expanded in later iterations.
    /// Adding: Key UI Image Update
    /// Adding: Backtracking Zombie
    /// Credit: Datorien Anderson
    /// </summary>

    // Variables --- Start
    public bool bHasKey = false;
    public bool bGreenKey = false;

    // GameObjects
    public GameObject Shutter;
    public GameObject DoorShut;
    public GameObject DoorOpen;

    // Backtracking Add-On
    public GameObject hiddenZombie;

    //Audio Components
    public AudioSource LVL2GOFX;
    public AudioClip goDoorOpen;

    // Key Image
    public GameObject theKeyIndicator;

    public Image KeyImage;
    public Sprite EmptyKey;
    public Sprite BlueKey;
    public Sprite GreenKey;

    // Green Key
    public GameObject GreenDoor;

    public int nWaitTime = 1;

    private bool Imaging = true;

    // Variables --- End

    public void Start()
    {
        // Gets Reference To These Items
        Shutter = GameObject.Find("ProgressionDoor/ShutterClosed");
        DoorShut = GameObject.Find("ProgressionDoor/DoorShut");
        DoorOpen = GameObject.Find("ProgressionDoor/DoorOpen");

        // Backtracking Reference
        hiddenZombie = GameObject.Find("Enemies/AfterKeyGrab");

        // Sets Door to Open/False as game starts
        DoorShut.SetActive(false);

        // AudioSource
        LVL2GOFX = GetComponent<AudioSource>();

        // Reference To KeyImage
        //GameObject theKeyIndicator = GameObject.Find("GameCanvas_PFI/keyIndicator");
        GameObject theKeyIndicator = GameObject.FindGameObjectWithTag("Level2Image");

        if (theKeyIndicator != null)
        {
            KeyImage = theKeyIndicator.GetComponent<Image>();
            KeyImage.sprite = EmptyKey;
        }

        hiddenZombie.SetActive(false);

    }

    public void Update()
    {
        OpenUp();
        KeyImageCheck();

    }
    public void OpenUp()
    {
        // If Key Has been Grabbed, open one door and close the other
        if (bHasKey == true)
        {
            // Play One Shot
            //LVL2GOFX.PlayOneShot(goDoorOpen);

            if (DoorShut.activeInHierarchy == false)
            {
                // Play One Shot
                //LVL2GOFX.PlayOneShot(goDoorOpen);
            }

            hiddenZombie.SetActive(true);

            DoorShut.SetActive(true);
            DoorOpen.SetActive(false);
        }
    }

    // method checks to see if player has key, updates UI
    public void KeyImageCheck()
    {
        // only run this is the level is Level2
        if (Imaging)
        {
            // used to update GameCanvas_PFI but ONLY for level2, otherwise it will cause errors
            if (bHasKey == false && bGreenKey == false)
            {
                // uses empty key UI 
                KeyImage.sprite = EmptyKey;
                return;
            }
            else if (bGreenKey == true && bHasKey == false)
            {
                KeyImage.sprite = GreenKey;
                return;
            }
            else if (bHasKey == true && bGreenKey == false)
            {
                // uses key found UI
                KeyImage.sprite = BlueKey;
                return;
            }
            else if (bHasKey == true && bGreenKey == true)
            {
                // extra precaution
                bGreenKey = false;
                bGreenKey = false;
                return;
            }
        }
        else // else return null / disable this 
        {
            KeyImage.sprite = null;
            return;
        }
    }

    public void OnPickUp()
    {
        // Sets bHasKey to true
        bHasKey = true;
    }

    public void greenKeyPickUp()
    {
        bGreenKey = true;
    }

    IEnumerator TakeAwayGreen()
    {
        yield return new WaitForSeconds(nWaitTime);
        bGreenKey = false;
    }

    IEnumerator TakeAwayBlue()
    {
        yield return new WaitForSeconds(nWaitTime);
        bHasKey = false;
    }
}
