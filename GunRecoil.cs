using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    public Transform recoilPosition;
    public Transform rotationPoint;
    public float positionalRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;
    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 38f;

    public Vector3 camRecoilRotation = new Vector3(2, 2, 2);
    public Vector3 camRecoilRotationAiming = new Vector3(0.5f, 0.5f, 0.5f);

    public Vector3 RecoilRotation = new Vector3(10, 5, 7);
    public Vector3 RecoilKickBack = new Vector3(0.015f, 0f, -0.2f);

    public Vector3 RecoilRotationAim = new Vector3(10, 4, 6);
    public Vector3 RecoilKickBackAim = new Vector3(0.015f, 0f, -0.2f);

    Vector3 rotationalRecoil;
    Vector3 positionalRecoil;
    Vector3 Rot;
    public bool aiming;
    public bool sprint;
    public bool reload;
    public bool isGrenade;
    public bool isRocket;

    void FixedUpdate()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.fixedDeltaTime);
        Rot = Vector3.Slerp(Rot, rotationalRecoil, rotationalRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(Rot);
    }

    // Update is called once per frame
    void Update()
    {
        sprint = GameObject.Find("Player").GetComponent<PlayerMovementRigid>().sprinting;

        if (GameObject.Find("Weapon").GetComponent<ProjectileShooter>() != null)
        {
            reload = GameObject.Find("Weapon").GetComponent<ProjectileShooter>().reloading;
        }

        if (Input.GetMouseButton(1))
        {
            aiming = true;
        }
        else
        {
            aiming = false;
        }
    }

    public void FireGun()
    {
        if (aiming)
        {
            rotationalRecoil += new Vector3(-RecoilRotationAim.x, Random.Range(-RecoilRotationAim.y, RecoilRotationAim.y), Random.Range(-RecoilRotationAim.z, RecoilRotationAim.z));
            positionalRecoil += new Vector3(Random.Range(-RecoilKickBackAim.x, RecoilKickBackAim.x), Random.Range(-RecoilKickBackAim.y, RecoilKickBackAim.y), RecoilKickBackAim.z);
        }
        else
        {
            rotationalRecoil += new Vector3(-RecoilRotation.x, Random.Range(-RecoilRotation.y, RecoilRotation.y), Random.Range(-RecoilRotation.z, RecoilRotation.z));
            positionalRecoil += new Vector3(Random.Range(-RecoilKickBack.x, RecoilKickBack.x), Random.Range(-RecoilKickBack.y, RecoilKickBack.y), RecoilKickBack.z);
        }
    }
}
