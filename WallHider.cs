using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHider : MonoBehaviour
{
    //public GameObject brokenWall;

    public void deletethis()
    {
        //brokenWall.SetActive(true);
        Destroy(gameObject);
    }
}
