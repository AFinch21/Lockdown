using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject door;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int numTargets = GameObject.FindGameObjectsWithTag("PopUpTarget").Length;
        if (numTargets == 0)
        {
            animator = door.GetComponent<Animator>();
            animator.SetBool("DoorUp", true);
        }
    }

}
