using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {

    public void level1()
    {
        SceneManager.LoadScene("Tutorial Level");
    }

    public void schootingRange()
    {
        SceneManager.LoadScene("Shooting Range");
    }

    public void level2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void level3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void LevelBack()
    {
        SceneManager.LoadScene("Menu");
    }
}
