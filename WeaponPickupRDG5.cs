using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupRDG5 : MonoBehaviour
{
    public GameObject player;
    public GameObject prefab;
    WeaponSwitching ws;
    InInventory inv;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            ws = player.GetComponent<WeaponSwitching>();
            ws.hasRGD5 = true;
            prefab.GetComponent<InInventory>().hasWeapon = true;

            Debug.Log("Picked Up RDG-5!");
            Destroy(gameObject);
        }
    }

}
