using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRecoil : MonoBehaviour
{
    public float rotationSpeed = 6f;
    public float returnSpeed = 25f;

    public Vector3 sprintCam = new Vector3(4f, 0.5f, 0.5f);
    //public Vector3 RecoilRotationAiming = new Vector3(0.5f, 0.5f, 0.5f);

    public bool aiming;
    public bool sprinting;
    public bool ground;
    public bool reload;
    private bool isGrenade;
    private bool isRocket;
    private Vector3 currentRotation;
    private Vector3 Rot;
    private GameObject currentWeapon;

    void FixedUpdate()
    {
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        Rot = Vector3.Slerp(Rot, currentRotation, rotationSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(Rot);
    }

    public void sprint()
    {
        currentRotation += new Vector3(Random.Range(-sprintCam.x, sprintCam.x), Random.Range(-sprintCam.y, sprintCam.y), Random.Range(-sprintCam.z, sprintCam.z));
    }


    // Update is called once per frame
    void Update()
    {
        sprinting = GameObject.Find("Player").GetComponent<PlayerMovementRigid>().sprinting;
        ground = GameObject.Find("Player").GetComponent<PlayerMovementRigid>().grounded;

        if (GameObject.Find("Weapon Holder").GetComponentInChildren<InInventory>() != null)
        {
            isGrenade = GameObject.Find("Weapon").GetComponent<GunRecoil>().isGrenade;
            isRocket = GameObject.Find("Weapon").GetComponent<GunRecoil>().isRocket;
            if (GameObject.Find("Weapon").GetComponent<ProjectileShooter>() != null)
            {
                reload = GameObject.Find("Weapon").GetComponent<ProjectileShooter>().reloading;
            }
            else if (GameObject.Find("Weapon").GetComponent<ShotgunProjectileShooter>() != null)
            {
                reload = GameObject.Find("Weapon").GetComponent<ShotgunProjectileShooter>().reloading;
            }
        }

        if (Input.GetMouseButton(1))
        {
            aiming = true;
        }
        if (sprinting && ground == true)
        {
            sprint();
        }
        else
        {
            aiming = false;
        }
       
    }

    public void fire()
    {
        Vector3 currentWeaponRR = GameObject.Find("Weapon").GetComponent<GunRecoil>().camRecoilRotation;
        Vector3 currentWeaponRRA = GameObject.Find("Weapon").GetComponent<GunRecoil>().camRecoilRotationAiming;

        if (aiming)
        {
            currentRotation += new Vector3(Random.Range(-currentWeaponRRA.x, currentWeaponRRA.x), Random.Range(-currentWeaponRRA.y, currentWeaponRRA.y), Random.Range(-currentWeaponRRA.z, currentWeaponRRA.z));
        }
        else
        {
            currentRotation += new Vector3(Random.Range(-currentWeaponRR.x, currentWeaponRR.x), Random.Range(-currentWeaponRR.y, currentWeaponRR.y), Random.Range(-currentWeaponRR.z, currentWeaponRR.z));
        }
    }
}
