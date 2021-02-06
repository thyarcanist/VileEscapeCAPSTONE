using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Assertions.Must;
using TMPro;

public class MainLightBreaker : MonoBehaviour
{
    // Credit: Datorien Anderson
    // Current Issue: Can't use trigger to start this, need to find a way to use an array of components
    // Interesting To Note: WentInTrigger bool doesn't get checked when walking into the Trigger but the player can still press "E" to Interact in the Editor, to start it.

    [Header("Countdown Until Powerdown")]
    public float downTime = 40f; // 40 in seconds
    public float resetFloat = 40f; // changeable, 40 by default
    public bool bIsRunning = false;
    private bool isDownTime;
    public bool WentInTrigger = false;

    [Header("Light Elements")]
    public GameObject[] Lights = new GameObject[0];
    public Light TestLight;
    public Light LightOn;
    public Light LightOff;
    public Light DiagnosticLight;

    [Header("Audio Elements")]
    public AudioSource BreakerSource;
    public AudioClip BreakerOn;
    public AudioClip BreakerOff;
    public AudioClip PowerDown;
    public AudioClip HumSFX;

    [Header("MainBreaker UI")]
    public GameObject GameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Get References 
        GameCanvas = GameObject.Find("GameCanvas_PFI/Text_Interact");
        //InteractText = GameCanvas.GetComponent<Text>();

        TestLight.GetComponent<Light>();
        BreakerSource = GetComponent<AudioSource>();

        GameCanvas.SetActive(false);
    }

    void Update()
    {
        Lights = GameObject.FindGameObjectsWithTag("MainLight");

        if (bIsRunning == true && isDownTime == false)
        {
            //Debug.Log("Powering Up");
            BreakerTurnOn();
            if (downTime <= 0)
            {
                //Debug.Log("Powering Down");
                BreakerTurnOff();
                BreakerSource.Play(4);
                
                return;
            }
            return;
        }
        LightUpdate();
        InputCommand();
    }

    public void LightUpdate()
    {
        if (bIsRunning == false)
        {
            LightOff.enabled = true;
            LightOn.enabled = false;
            DiagnosticLight.enabled = false;
            return;
        }
        else if (bIsRunning == true)
        {
            LightOn.enabled = true;
            LightOff.enabled = false;
            DiagnosticLight.enabled = false;
            return;
        }
        else if (isDownTime == true && bIsRunning == true)
        {
            LightOff.enabled = false;
            DiagnosticLight.enabled = true;
            DiagnosticLight.color = Color.Lerp(Color.yellow, Color.black, .3f);
            return;
        }
        else
        {
            return;
        }
    }

    public void BreakerTurnOn()
    {
        //Debug.Log("Powering Up");
        downTime -= Time.deltaTime;
        for (int i = 0; i < Lights.Length; i++)
        {
            Lights[i].GetComponent<Light>().enabled = true;
        }
        BreakerPlay();
    }
    public void BreakerTurnOff()
    {
        //Debug.Log("Powering Down");
        for (int i = 0; i < Lights.Length; i++)
        {
            Lights[i].GetComponent<Light>().enabled = false;
        }
        bIsRunning = false;
        BreakerSource.PlayOneShot(BreakerOff);
        ResetPower();
    }

    public void BreakerPlay()
    {
        if (!BreakerSource.isPlaying)
        {
            BreakerSource.PlayOneShot(BreakerOn);
        }
    }

    public void BreakerStop()
    {
        // when it's manually turned off
        BreakerSource.PlayOneShot(PowerDown);
    }

    public bool bCanInteract = false;
    private void OnTriggerEnter(Collider other)
    {
        bCanInteract = true;
        GameCanvas.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        bCanInteract = false;
        GameCanvas.SetActive(false);
    }


    public void InputCommand()
    {
        if (Input.GetKeyDown(KeyCode.E) && bCanInteract == true)
        {
            print("on");
            bIsRunning = true;
            BreakerSource.Stop();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.E) && bCanInteract == true)
        {
            bIsRunning = false;
            print("off");
            BreakerSource.Stop();
            BreakerSource.PlayOneShot(BreakerOff);
            ResetPower();
            return;
        }
        else
        {
            return;
        }
    }


    // Resetting Power back and checkpoint
    public void ResetPower()
    {
        isDownTime = false;
        downTime = resetFloat;
    }

    IEnumerator WaitToUseAgain()
    {
        yield return new WaitForSeconds(20);
        WentInTrigger = false;
    }
}