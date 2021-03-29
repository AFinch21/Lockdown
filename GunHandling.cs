using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunHandling : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("ADS", false);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("ADS", false);
        if (Input.GetMouseButton(1))
        {
            ADS();
        }
    }

    public void ADS()
    {
        animator.SetBool("ADS", true);
        Debug.Log("ADS on");
    }

}
