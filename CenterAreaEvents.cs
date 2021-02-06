using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterAreaEvents : MonoBehaviour
{
    public GameObject RoadRef;
    public GameObject BillboardLight;
    public GameObject GuidedLight;

    public int nTimeToDelete = 2;
    public bool bDidExit = false;
    public BoxCollider LeaveTrigger;


    public void Start()
    {
        LeaveTrigger = GetComponent<BoxCollider>();
        SetLightsToFalse();
        GetEventReferences();
    }

    public void Update()
    {
        if (RoadRef.GetComponent<RoadEvents>().bGotFetchComplete == true)
        {
            GuidedLight.SetActive(true);
            BillboardLight.SetActive(false);
        }
        else if (RoadRef.GetComponent<RoadEvents>().bGotFetchQuest == true)
        {
            BillboardLight.SetActive(true);
        }
        if (bDidExit == true)
        {
            RoadRef.GetComponent<RoadEvents>().CloseTheGate();
            SetLightsToFalse();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRoot")
        {
            bDidExit = true;
        }
    }

    public void SetLightsToFalse()
    {
        GuidedLight.SetActive(false);
        BillboardLight.SetActive(false);
    }
    public void GetEventReferences()
    {
        RoadRef = GameObject.Find("ProgressionLocker/ProgressionManager");
        BillboardLight = GameObject.Find("ProgressionLocker/Wire2RestStop/Lights");
        GuidedLight = GameObject.Find("ProgressionLocker/Wire2Gate/Lights");
    }
}
