using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeSphere : MonoBehaviour
{
    public GameObject Caller;
    public GameObject Events;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        // when the player goes to the quest it should remove the current script
        if (Events.GetComponent<RDTrigger>().bHasRecievedQuest == true)
        {
            Debug.Log(this + " has been destroyed.");
            Destroy(this);
        }
    }

    public void GetReferences()
    {
        Caller = GameObject.FindGameObjectWithTag("Caller");
        Events = GameObject.FindGameObjectWithTag("ThresholdB");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRoot" || other.tag == "Player")
        {
            Debug.Log("Player has entered" + gameObject + "collider.");
            Caller.GetComponent<InvokePlayer>().bIsPlayerNearby = true;
            Caller.GetComponent<InvokePlayer>().bIsCalling = true;

        }
        else
        {
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerRoot" || other.tag == "Player")
        {
            Debug.Log("Player has left" + gameObject + "collider.");
            Caller.GetComponent<InvokePlayer>().bIsPlayerNearby = false;
            Caller.GetComponent<InvokePlayer>().bIsCalling = false;
        }
        else
        {
            return;
        }
    }

}
