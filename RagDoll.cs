using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour {

    public Collider MainCollider;
    public Collider[] AllColliders;


	// Use this for initialization
	void Awake ()
    {
        MainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        DoRagdoll(false);
    }

    public void DoRagdoll(bool isRagdoll)
    {
        foreach (var col in AllColliders)
            col.enabled = isRagdoll;
        MainCollider.enabled = !isRagdoll;
        GetComponent<Rigidbody>().useGravity = !isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
        GetComponent<EnemyControllerShooting>().enabled = !isRagdoll;
    }
}