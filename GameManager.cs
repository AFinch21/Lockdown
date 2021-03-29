
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject prefab;
    Transform body;
    public float gameOverScreen = 3;
    public GameObject deathCam;
    public GameObject playerBody;
    public GameObject player;
    public GameObject weaponHolder;


    public void EndGame()
    {
        Debug.Log("Game Over");
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        Debug.Log("Dead");
        player.SetActive(false);
        weaponHolder.SetActive(false);
        deathCam.SetActive(true);
        playerBody.SetActive(true);
        yield return new WaitForSeconds(gameOverScreen);
        Restart();

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
