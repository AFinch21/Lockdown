using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
    public GameObject target;
    private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator = target.GetComponent<Animator>();
            animator.SetBool("PlayerArrivesTarget", true);
        }
    }
}
