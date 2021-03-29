using UnityEngine;
using System.Collections;

public class BloodSplat : MonoBehaviour
{
    private ContactPoint Collision_Point;

    public GameObject bloodSplat;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision Collided)
    {
        if (Collided.gameObject.tag == "Block")
        {
            Collision_Point = Collided.contacts[0]; //Returns the FIRST Point of Contact.

            //Holds a Reference to the newly created Bullet Mark.
            GameObject Temporary_splat_Handler;

            //Instantiate the Bullet Mark, the Magic Phrase that makes it all happen correctly here is: Quaternion.LookRotation(Collision_Point.normal)
            Temporary_splat_Handler = Instantiate(bloodSplat, Collision_Point.point, Quaternion.LookRotation(Collision_Point.normal)) as GameObject;

            //Compensate for the Rotation Error, this may or may not be required for your Bullet Mark Mesh depending
            //on how it was created.
            Temporary_splat_Handler.transform.Rotate(Vector3.right * 90);

            //We have to "Push" the Bullet Texture out of the wall a bit [just a little] least it be hidden inside the walls of contact.
            Temporary_splat_Handler.transform.Translate(Vector3.up * 0.005f);

            Destroy(Temporary_splat_Handler, 10.0f); //Destroy the Bullet Mark after 3 Seconds.
            Destroy(gameObject);
        }
    }

}