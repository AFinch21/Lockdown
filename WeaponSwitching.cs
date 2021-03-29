using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public bool noWeapon;
    public bool hasKnife;
    public bool hasM4A4;
    public bool hasM14;
    public bool hasAK47;
    public bool hasBennelliM4;
    public bool hasM107;
    public bool hasMP5SD;
    public bool hasRGD5;
    public bool hasRPG7;
    public string[] weapons = new string[] { "Unarmed", "Knife", "M4A4", "M14", "MP5SD", "RGD-5", "RPG-7"};

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
        noWeapon = false;
        hasKnife = false;
        hasM4A4 = false;
        hasM14 = false;
        hasAK47 = false;
        hasBennelliM4 = false;
        hasM107 = false;
        hasMP5SD = false;
        hasRGD5 = false;
        hasRPG7 = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(weapons.Length);
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && hasKnife)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && hasM4A4)
        {
            selectedWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4 && hasM14)
        {
            selectedWeapon = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5 && hasAK47)
        {
            selectedWeapon = 4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6 && hasMP5SD)
        {
            selectedWeapon = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7) && transform.childCount >= 7 && hasBennelliM4)
        {
            selectedWeapon = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) && transform.childCount >= 8 && hasM107)
        {
            selectedWeapon = 7;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9) && transform.childCount >= 8 && hasRPG7)
        {
            selectedWeapon = 8;
        }

        if (Input.GetKeyDown(KeyCode.G) && transform.childCount >= 9 && hasRGD5)
        {
            selectedWeapon = 9;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                if (weapon.GetComponent<InInventory>().hasWeapon)
                {
                    weapon.gameObject.SetActive(true);

                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
