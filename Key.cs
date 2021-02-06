using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Will Refactory Key & GreenKey Script later

    public GameObject KeyScript;
    public int nDestroyObject = 1;
    ResetScript LifeSpan;
    public AudioSource KeyAudio;
    public AudioClip keyPickUpFX;

    public void Start()
    {
        KeyScript = GameObject.Find("Triggers/RoomBTrigger");
        KeyAudio = GetComponent<AudioSource>();
        LifeSpan = gameObject.GetComponent<ResetScript>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRoot")
        {
            KeyAudio.PlayOneShot(keyPickUpFX);
            KeyScript.GetComponent<LVL2GoldOpen>().OnPickUp();
            StartCoroutine(InInventory());
        }
        
    }

    IEnumerator InInventory()
    {
        yield return new WaitForSeconds(nDestroyObject);
        LifeSpan.MoveObject();
        //Destroy(gameObject);
    }


}
