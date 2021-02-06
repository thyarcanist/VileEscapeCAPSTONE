using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightUsage : MonoBehaviour
{
    // To use this script effectively, drop the flashlightmount prefab into Player/Player


    // number variables
    [Header("NOTE: set lightCharge to fSetMax")]
    public float lightCharge;
    public float fSetWhenFlicker;
    public float fSetNotFlicker;

    [Header("Set Light Charge Minimum and Maximum")]
    public float fSetMin;
    public float fSetMax;

    [Header("Set the decay or increate rate for flashlight")]
    public float fChangeBy;
    [Header("Set Flashlight flicker rate - inbetween 0.0f and 1f")]
    public float fSetFlickerRate; // .5 is a little too slow



    [Header("Flashlight Ray/Shaft Components")]
    // normal variables
    public Light flashLightBeam;
    public GameObject LightCone;

    [Header("Audio Elements")]
    public AudioSource flashlightSource;
    public AudioClip flashlightClick;
    public AudioClip lightGoesOut;

    [Header("Flashlight Boolean")]
    // set to false if scene will use flashlight
    public bool isActiveForScene = false;
    // Bottom for Flashlight itself
    private bool canUseLight = false; // set to true if want to use flashlight in scene
    public bool bOn = true;
    [Header("Low Battery Functionality")]
    // 2020-11-07: bWentDead is a QOL Feature, is the flashlight goes dead the player will have to wait a certain time to use it.
    public bool bWentDead = false;
    public int Wait;

    // Start is called before the first frame update
    void Start()
    {
        flashlightSource = GetComponent<AudioSource>();
        LightCone = GameObject.Find("Player/Player/Shoulder/FlashlightMount/LightBeam/LightCone");
        lightCharge = fSetMax;

        // base flashlight set to false on scene start
        bOn = false; 
        LightCone.SetActive(false); 
        flashLightBeam.enabled = false;
    }
     
    // Update is called once per frame
    void Update()
    {
        lightCharge = Mathf.Clamp(lightCharge, fSetMin, fSetMax);

        FlashLight();
        UsageRules();
        LightTruths();
        LightChargeRating();
    }

    // now it needs to turn off the lightCone gameObject when
    void LightOn()
    {
        flashLightBeam.enabled = true;
        bOn = true;
        LightCone.SetActive(true);
        flashlightSource.PlayOneShot(flashlightClick);
    }

    void LightOff()
    {
        flashLightBeam.enabled = false;
        bOn = true;
        LightCone.SetActive(false);
        flashlightSource.PlayOneShot(flashlightClick);
    }

    // Light Values
    public void OnBatteryEmpty() // QOL Feature
    {
        if (lightCharge == 0)
        {
            // when the light is dead, set bool going dead to true
            bWentDead = true;
        }

        else if (lightCharge <= 25f && bWentDead == true)
        {
            // if LightCharge is greater or equal to 25 and bWentDead is True
            // Player can now turn the flashlight back on
            return;
        }
        return;
    }

    void LightChargeRating()
    {
        if (bOn == true)
        {
            lightCharge = lightCharge - fChangeBy;
            return;
        }
        else if (bOn == false)
        {
            lightCharge = lightCharge + fChangeBy;
            return;
        }
        return;
    }

    // still am trying to figure out something

    public void defunct_LightTruths()
    {
        // if charge is less than or equal to setWhenFlicker make lights flicker on and off > = greater than < = less than
        if (lightCharge <= fSetWhenFlicker && bOn == true)
        {
            flashLightBeam.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, fSetFlickerRate));
            Debug.Log("Flashing lights");
            return;
        }
        else if (lightCharge <= 10f && bOn == true) // this and the other "lightCharge <=" is not triggering at all.
        {
            flashlightSource.PlayOneShot(lightGoesOut);
            Debug.Log("Light is going out.");
            return;
        }
        // turn lights off, play a different sound than the clickOff and set bOn to false
        else if (lightCharge <= 0f && bOn == true)
        {
            flashLightBeam.enabled = false;
            bOn = false; // this isn't triggering
            LightCone.SetActive(false);
            Debug.Log("Light is turning off.");
            return;
        }
        // when it's not flickering anymore
        else if (lightCharge > fSetNotFlicker)
        {
            flashLightBeam.color = Color.white;
            Debug.Log("No longer flickering.");
            return;
        }
        else
        {
            return;
        }
    }

    public void LightTruths() // didn't work until I nested this
    {
        // if charge is less than or equal to setWhenFlicker make lights flicker on and off > = greater than < = less than
        if (lightCharge <= fSetWhenFlicker && bOn == true)
        {
            flashLightBeam.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, fSetFlickerRate));          
            if (lightCharge <= .5f && bOn == true) // this and the other "lightCharge <=" is not triggering at all.
            {
                flashlightSource.PlayOneShot(lightGoesOut);             
                // new problem, the oneshot plays multiple times
            if (lightCharge <= 0f && bOn == true)
                {
                    flashLightBeam.enabled = false;
                    bOn = false; // this isn't triggering
                    LightCone.SetActive(false);
                            // when it's not flickering anymore
                    if (lightCharge > fSetNotFlicker)
                    {
                        flashLightBeam.color = Color.white;
                        return;
                    }
                    return;
                }
                return;
            }

        }
        else
        {
            return;
        }
    }

    public void FlashLight()
    {
        if (canUseLight == true && Input.GetKeyDown(KeyCode.F) && bOn == false)
        {
            LightOn();
            return;
        }
        else if (canUseLight == true && Input.GetKeyDown(KeyCode.F) && bOn == true)
        {
            LightOff();
            bOn = false; // added this on after creating initial script, because turning on and then off, at first worked but couldn't turn back on
            return;
        }
        else if (canUseLight == true && Input.GetKeyDown(KeyCode.F) && bOn == false & bWentDead == true)
        {
            return;
        }
    }

    // ENUMERATOR
    IEnumerator WaitToUse()
    {
        yield return new WaitForSeconds(Wait);
    }

    // For Different Scenes
    void UsageRules()
    {
        if (isActiveForScene == true)
        {
            canUseLight = true;
            return;
        }
        else
        {
            canUseLight = false;
            flashLightBeam.enabled = false;
            return;
        }        
    }
}
