using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitch : MonoBehaviour
{
    public GameObject obj01; // Object One
    public GameObject obj02; // Object Two

    // strings
    [Header("Object One")]
    public string oneString;
    [Header("Object Two")]
    public string twoString;

    // Trigger
    public GameObject Trigger;

    // Bool
    public bool isReached = false;

    // Start is called before the first frame update
    void Start()
    {
        GetObjectReferences();

        obj02.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isReached == true)
        {
            obj02.SetActive(true);
            obj01.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRoot" || other.tag == "Player")
        {
            isReached = true;
        }
    }

    public void GetObjectReferences()
    {
        Trigger = gameObject;

        obj01 = GameObject.Find(oneString); // BeforeEndCollapse
        obj02 = GameObject.Find(twoString); // EndCollapse
    }
}
