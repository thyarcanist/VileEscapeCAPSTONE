using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokePlayer : MonoBehaviour
{
    public GameObject playerRef;
    public AudioSource strangerAudio;
    public AudioClip audioToPlayer;

    public GameObject zoneToSee;
    public GameObject Events;

    // ints
    public int nSetCallSeconds = 3;

    // bools
    public bool bIsPlayerNearby = false;
    public bool bIsCalling = false;
    private bool bHalt = true;


    // Start is called before the first frame update
    void Start()
    {
        GetObjectReferences();        
    }

    // Update is called once per frame
    void Update()
    {
        CallOutToPlayer();

        // when the player goes to the quest it should remove the current script
        if (Events.GetComponent<RDTrigger>().bHasRecievedQuest == true)
        {
            Debug.Log("Quest has been marked as true so remove " + this);
            Destroy(this);
        }
    }

    public void CallOutToPlayer()
    {
        if (bIsCalling && bIsPlayerNearby == true)
        {
            //strangerAudio.Play();
            if (bHalt)
            {
                strangerAudio.PlayOneShot(audioToPlayer);
                bHalt = false;
                StartCoroutine(SetCallToFalse());
                return;
            }
        }
        else
        {
            strangerAudio.Stop();
        }
    }


    public void GetObjectReferences()
    {
        playerRef = GameObject.FindGameObjectWithTag("PlayerRoot");
        strangerAudio = GetComponent<AudioSource>();
        Events = GameObject.FindGameObjectWithTag("ThresholdB");
    }

    IEnumerator SetCallToFalse()
    {
        yield return new WaitForSeconds(1);
        bHalt = false;
        yield return new WaitForSeconds(nSetCallSeconds);
        bHalt = true;
    }
}




//if (bIsPlayerNearby == true)
//{
//    IsPlaying();
//}
//else if (bIsPlayerNearby == false && bIsCalling == false)
//{
//    StopPlaying();
//}