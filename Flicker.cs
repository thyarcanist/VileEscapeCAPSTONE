using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Flicker : MonoBehaviour
{
    // Credit: Datorien Anderson

    // To be placed on lights not controlled by main breaker
    [Header("Light Components")]
    [Range(0, 1)]
    public float flickerRate;
    [Range(0, 100)]
    public float spotAngle;
    public float flickerIntensity;
    public Light flickerLight;
    private float nReset = 16;


    // Start is called before the first frame update
    void Start()
    {
        flickerLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        // changes the flicker rate of the light
        flickerLight.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, flickerRate));
        // Changes the angle of the light
        flickerLight.spotAngle = spotAngle;
        // changes the intensity of the light
        flickerLight.intensity = flickerIntensity;

        LightFlickers();
        nReset -= Time.deltaTime;
    }

    public void LightFlickers()
    {
        if (nReset <= 0)
        {
            StartCoroutine(LightFlicker());
        }
    }

    IEnumerator LightFlicker()
    {
        yield return new WaitForSeconds(1);
        flickerLight.enabled = false;
        yield return new WaitForSeconds(1);
        flickerLight.enabled = true;
        yield return new WaitForSeconds(3);
        flickerLight.enabled = false;
        yield return new WaitForSeconds(1);
        flickerLight.enabled = true;
        yield return new WaitForSeconds(3);
        flickerLight.enabled = false;
        yield return new WaitForSeconds(2);
        flickerLight.enabled = true;
        yield return new WaitForSeconds(1);
        flickerLight.enabled = false;
        yield return new WaitForSeconds(.5f);
        flickerLight.enabled = true;
        yield return new WaitForSeconds(.5f);
        flickerLight.enabled = false;
        yield return new WaitForSeconds(1);
        flickerLight.enabled = true;
        yield return new WaitForSeconds(1);
        flickerLight.enabled = false;
        yield return new WaitForSeconds(1);
        flickerLight.enabled = true;
        nReset = 20;
    }
}
