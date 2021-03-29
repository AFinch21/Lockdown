using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupRPG7 : MonoBehaviour
{
    public GameObject player;
    public GameObject prefab;
    WeaponSwitching ws;
    private bool grabbing;


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            ws = player.GetComponent<WeaponSwitching>();
            ws.hasRPG7 = true;
            prefab.GetComponent<InInventory>().hasWeapon = true;

            Debug.Log("Picked Up RPG7!");
            Destroy(gameObject);
        }
    }

}
