using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponHolder;
    public GameObject prefab;
    WeaponSwitching ws;
    private bool grabbing;


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("poo");
            ws = weaponHolder.GetComponent<WeaponSwitching>();
            ws.hasM4A4 = true;
            prefab.GetComponent<InInventory>().hasWeapon = true;

            Debug.Log("Picked Up M4A4!");
            Destroy(gameObject);
        }
    }

}
