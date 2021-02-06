using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadLevel : MonoBehaviour
{
    public GameObject GetWeapon;
    public int StartingAmmo;
    public int shotgunPellets;


    // Start is called before the first frame update
    void Start()
    {
        // Gets FireWeapon Script & Gives the Player 4 shotgun ammo to start with
        GetWeapon = GameObject.Find("Player/");
        GetWeapon.GetComponent<FireWeapon>().shotgunAmmo = StartingAmmo;
        GetWeapon.GetComponent<FireWeapon>().rifleAmmo = StartingAmmo;
        GetWeapon.GetComponent<FireWeapon>().shotgunPelletCount = shotgunPellets;
    }

    // Update is called once per frame
    void Update()
    {
        // if Scene is, give these weapons and set the others to false
        if (SceneManager.GetActiveScene().name == "LVL9_Road")
        {
            GetWeapon.GetComponent<FireWeapon>().hasPistol = true;
            GetWeapon.GetComponent<FireWeapon>().hasRifle = true;
            GetWeapon.GetComponent<FireWeapon>().hasSMG = false;
            GetWeapon.GetComponent<FireWeapon>().hasShotgun = true;
        }
        else
        {
            return;
        }
    }
}
