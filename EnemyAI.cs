using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    Transform playerTransform;
    UnityEngine.AI.NavMeshAgent myNavmesh;
    public float checkRate = 0.01f;
    public float enemyDamage = 25;
    float nextCheck;
    public float stoppingDistance;
    public float retreatDistance;
    public float speed;
    public Transform player;


    // Start is called before the first frame update
    void Start () {

        if (GameObject.FindGameObjectWithTag("Player").activeInHierarchy)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        myNavmesh = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () 
    {

        Debug.Log(transform.position-player.position);

        if (Time.time > nextCheck)
        {
            if(Vector3.Distance(transform.position, player.position) > stoppingDistance) 
            {
                nextCheck = Time.time + checkRate;
                FollowPlayer();
            } 
            else if(Vector3.Distance(transform.position, player.position) < stoppingDistance)
            {
                nextCheck = Time.time + checkRate;
                myNavmesh.destination = this.transform.position;
            } 
        }
    }

    void FollowPlayer()
    {
        myNavmesh.transform.LookAt(playerTransform);
        myNavmesh.destination = playerTransform.position;
    }

    void RunFromPlayer()
    {
        myNavmesh.transform.LookAt(playerTransform);
        myNavmesh.destination = this.transform.position;
    }

}
