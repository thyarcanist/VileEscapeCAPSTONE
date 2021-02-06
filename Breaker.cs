using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    // Needs to access script where MainLightBreaker is
    public GameObject mainBreaker;
    public string linkBreaker;
    public GameObject GameCanvas;

    public void Start()
    {
        mainBreaker = GameObject.Find(linkBreaker);
        GameCanvas = GameObject.Find("GameCanvas_PFI/Text_Interact");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // can turn breaker on and off
            mainBreaker.GetComponent<MainLightBreaker>().bIsRunning = !mainBreaker.GetComponent<MainLightBreaker>().bIsRunning;
        }
    }

    public void UIUpdate()
    {
        return;
    }

}
