using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketShooter : MonoBehaviour
{
    GameObject prefab;
    GameObject MagazineObj;
    public float BulletVelocity = 1000f;
    public float BulletMass;
    public float BulletDamage = 25f;
    public float fireRate = 0.02f;
    private float timer;
    public float MaxAmmo = 30f;
    public float reserveAmmo = 120f;
    public float Magazine;
    public float reloadTime = 2.5f;
    public float headShotMultiplier = 1.0f;
    public bool sprint;
    public bool reloading;
    public bool isShooting;
    public bool silenced;
    public Text ammoDisplay;
    public Text reserveDisplay;
    public Transform Muzzle;
    public ParticleSystem muzzleFlash;
    private Animator animator;
    public GameObject warhead;

    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load("Rocket") as GameObject;
        Magazine = MaxAmmo;
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("ADS", false);
        animator.SetBool("Reload", false);
        reloading = false;
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("ADS", false);
        animator.SetBool("Reload", false);
        animator.SetBool("Sprint", false);
        ammoDisplay.text = Magazine.ToString();
        reserveDisplay.text = reserveAmmo.ToString();
        timer += Time.deltaTime;
        sprint = GameObject.Find("Player").GetComponent<PlayerMovementRigid>().sprinting;

        isShooting = false;

        if (reloading)
        {
            return;
        }

        if (Input.GetMouseButton(0) && timer > fireRate && Magazine > 0 && !sprint && reloading == false)
        {
            fire();
            Magazine -= 1;
            timer = 0;
            if (!silenced)
            {
                isShooting = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButton(1))
        {
            ADS();
        }

        if (sprint)
        {
            animator.SetBool("Sprint", true);
        }

    }

    public void fire()
    {
        //isShooting = true;
        GameObject projectile = Instantiate(prefab, Muzzle.position, Muzzle.rotation);
        //ParticleSystem mf = Instantiate(muzzleFlash, Muzzle.position, Muzzle.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = Muzzle.transform.forward * BulletVelocity;
        rb.mass = BulletMass;

        warhead.SetActive(false);
        Destroy(projectile, 5);
    }


    IEnumerator Reload()
    {
        if (Magazine == MaxAmmo)
        {
            animator.SetBool("Reload", false);
            Debug.Log("Ammo is Max");
        }

        if (reserveAmmo == 0)
        {
            animator.SetBool("Reload", false);
            Debug.Log("No more Ammo!");
        }

        if (reserveAmmo > 0 && Magazine != MaxAmmo)
        {

            if (reserveAmmo > (MaxAmmo - Magazine))
            {
                reloading = true;
                animator.SetBool("Reload", true);
                yield return new WaitForSeconds(0.5f);
                warhead.SetActive(true);
                Vector3 temp = new Vector3(0, -1f, 0);
                yield return new WaitForSeconds(reloadTime);
                reserveAmmo -= (MaxAmmo - Magazine);
                Magazine = MaxAmmo;
            }
            else
            {
                reloading = true;
                animator.SetBool("Reload", true);
                yield return new WaitForSeconds(0.5f);
                warhead.SetActive(true);
                yield return new WaitForSeconds(reloadTime);
                Magazine += reserveAmmo;
                reserveAmmo -= reserveAmmo;
            }
        }
        reloading = false;
    }


    public void ADS()
    {
        animator.SetBool("ADS", true);
    }
}

