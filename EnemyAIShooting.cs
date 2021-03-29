using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIShooting : MonoBehaviour {

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public Transform player;
    public float rotationSpeed = 5f;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {

        //for head movement
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        //for body movement
        //Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //transform.LookAt(playerPosition);

        if(Vector3.Distance(transform.position, player.position) > stoppingDistance){

            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        } else if(Vector3.Distance(transform.position, player.position) < stoppingDistance) {
            
            transform.position = this.transform.position;

        } else if(Vector3.Distance(transform.position, player.position) < retreatDistance){
            
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }
}
