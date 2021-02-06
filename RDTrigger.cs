using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDTrigger : MonoBehaviour
{
    public GameObject Roadie;
    public GameObject Stairs;
    public GameObject ZombiesInCamp;
    public GameObject fetchableItem;

    // Quest Finish
    public bool bIsThanked;
    public bool bHasBeenThanked;

    // Quest Get
    public bool bIsNearGiver;
    public bool bHasRecievedQuest;

    // Bool Hook
    private bool bHook;

    // Adding GameObjects To DeActivate ObjCompletionTriggers once hit
    public GameObject Obj1;
    public GameObject Obj2;
    public GameObject Obj3;
    public GameObject Obj4;
    public GameObject Rail;

    public void Start()
    {
        Roadie = GameObject.Find("ProgressionLocker/ProgressionManager");
        Stairs = GameObject.Find("ProgressionLocker/Gateway");
        ZombiesInCamp = GameObject.Find("ProgressionLocker/Campsite/Zombies");
        fetchableItem = GameObject.FindGameObjectWithTag("WaterBottle");
        Rail = GameObject.Find("ProgressionLocker/GatedObject");
        // Objective References
        GetObjReferences();
        StartCoroutine(WaitToExecute());

        // Set After Getting References otherwise NULL ERROR will occur
        bIsThanked = false;
        bHasBeenThanked = false;
    }

    private void Update()
    {
        if (fetchableItem.activeInHierarchy == false && bHook == false)
        {
            Obj4.SetActive(true);
            bHook = true;
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (gameObject.tag == "ThresholdA" && other.tag == "PlayerRoot")
        {
            // When the player first enters the area && deactive completion obj near this
            Roadie.GetComponent<RoadEvents>().bFirstEncounter = true;
            Rail.SetActive(true);
            Roadie.GetComponent<RoadEvents>().WelcomeLight();
            Obj1.SetActive(false);
            return;
        }
        else if (gameObject.tag == "ThresholdB" && other.tag == "PlayerRoot")
        {
            // when the player gets close to the Kiosk && enables stairs next to red barrels
            Obj2.SetActive(false);
            Roadie.GetComponent<RoadEvents>().bGotFetchQuest = true;
            Stairs.SetActive(false);
            ZombiesInCamp.SetActive(true);
            Obj3.SetActive(false);
            bIsNearGiver = true;
            if (bIsNearGiver == true && bHasRecievedQuest == false)
            {
                Roadie.GetComponent<RoadEvents>().NearQuestGiver();
                bHasRecievedQuest = true;
                return;
            }
            else
            {
                return;
            }
        }
        else if (gameObject.tag == "ThresholdC" && other.tag == "PlayerRoot")
        {
            // if bGot Water is True and bGotFetchQuest is true, and player is here it will mark bGotFetchComplete as true
            if (Roadie.GetComponent<RoadEvents>().bGotWater == true && Roadie.GetComponent<RoadEvents>().bGotFetchQuest == true)
            {
                Roadie.GetComponent<RoadEvents>().bGotFetchComplete = true;

                if (bIsThanked == false && bHasBeenThanked == false)
                {
                    Roadie.GetComponent<RoadEvents>().PlayItemDelivered();
                    bIsThanked = true;
                    bHasBeenThanked = true;
                }
                else
                {
                    return;
                }

            }
            else
            {
                return;
            }
            return;
        }
    }

    private void GetObjReferences()
    {
        Obj1 = GameObject.Find("ObjCompletionTriggers/ObjectiveCompletion");
        Obj2 = GameObject.Find("ObjCompletionTriggers/ObjectiveCompletion (1)");
        Obj3 = GameObject.Find("ObjCompletionTriggers/ObjectiveCompletion (2)");
        Obj4 = GameObject.Find("ObjCompletionTriggers/ObjectiveCompletion (3)");
        return;
    }

    public void SetLastObjTrue()
    {
        if (fetchableItem.activeInHierarchy == false)
        {
            Obj4.SetActive(true);
        }
        else
        {
            return;
        }
    }
    IEnumerator WaitToExecute()
    {
        // Waits to Execute this to not get a null reference exception // removed Stairs from being set to false
        yield return new WaitForSeconds(2);
        ZombiesInCamp.SetActive(false);
        Obj4.SetActive(false);
        Rail.SetActive(false);
    }


}
