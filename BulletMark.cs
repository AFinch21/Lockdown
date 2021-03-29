using UnityEngine;
using System.Collections;

public class BulletMark : MonoBehaviour
{
    private ContactPoint Collision_Point;

    public GameObject Bullet_Mark_Prefab;
    public GameObject Bullet_Splat_Prefab;
    public GameObject blood;
    public ParticleSystem spurt;
    public float force = 10000f;
    public float radius = 10f;


    void Start ()
    {
  
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter (Collision Collided)
    {
        if (Collided.gameObject.tag == "Block" || Collided.gameObject.tag == "Building")
        {
            Collision_Point = Collided.contacts[0]; //Returns the FIRST Point of Contact.

            Transform go = Collided.transform;

            //Holds a Reference to the newly created Bullet Mark.
            GameObject Temporary_Bullet_Mark_Handler;

            //Instantiate the Bullet Mark, the Magic Phrase that makes it all happen correctly here is: Quaternion.LookRotation(Collision_Point.normal)
            Temporary_Bullet_Mark_Handler = Instantiate(Bullet_Mark_Prefab, Collision_Point.point,Quaternion.LookRotation(Collision_Point.normal)) as GameObject;

            //Compensate for the Rotation Error, this may or may not be required for your Bullet Mark Mesh depending
            //on how it was created.
            //Temporary_Bullet_Mark_Handler.transform.Rotate(Vector3.right * 90);

            //We have to "Push" the Bullet Texture out of the wall a bit [just a little] least it be hidden inside the walls of contact.
            Temporary_Bullet_Mark_Handler.transform.Translate(Vector3.up * 0.001f);

            Temporary_Bullet_Mark_Handler.transform.SetParent(go);


            Destroy(Temporary_Bullet_Mark_Handler, 3.0f); //Destroy the Bullet Mark after 3 Seconds.
            Destroy(this.gameObject); //Destroy the Bullet itself.
        }

        if (Collided.gameObject.tag == "EnemyWeak" || Collided.gameObject.tag == "DeadEnemy")
        {

            Collision_Point = Collided.contacts[0]; //Returns the FIRST Point of Contact.
            Debug.Log("hit" + Collided.gameObject);
            //Holds a Reference to the newly created Bullet Mark.
            GameObject Temporary_Blood_Handler;
            //GameObject Temporary_Splat_Handler;
            ParticleSystem Temporary_Bullet_Spurt_Handler;


            //Instantiate the Bullet Mark, the Magic Phrase that makes it all happen correctly here is: Quaternion.LookRotation(Collision_Point.normal)
            Temporary_Blood_Handler = Instantiate(blood, Collision_Point.point, Quaternion.LookRotation(Collision_Point.normal)) as GameObject;
            Temporary_Blood_Handler = Instantiate(blood, Collision_Point.point, Quaternion.LookRotation(Collision_Point.normal)) as GameObject;
            Temporary_Blood_Handler = Instantiate(blood, Collision_Point.point, Quaternion.LookRotation(Collision_Point.normal)) as GameObject;


            Transform go = Collided.transform;

            //Holds a Reference to the newly created Bullet Mark.

            //Instantiate the Bullet Mark, the Magic Phrase that makes it all happen correctly here is: Quaternion.LookRotation(Collision_Point.normal)
            Temporary_Bullet_Spurt_Handler = Instantiate(spurt, Collision_Point.point, Quaternion.LookRotation(Collision_Point.normal)) as ParticleSystem;

            //if(Collided.gameObject.tag == "DeadEnemy")
            //{
            //    Collided.gameObject.Two
            //}
            //Temporary_Bullet_Mark_Handler.transform.Rotate(Vector3.right * 90);
            //Temporary_Bullet_Mark_Handler.transform.Translate(Vector3.up * -0.05f);



            Destroy(this.gameObject); //Destroy the Bullet itself.

            ParticleSystem sp = Instantiate(spurt, Collision_Point.point, Quaternion.LookRotation(Collision_Point.normal));

        }


    }

}